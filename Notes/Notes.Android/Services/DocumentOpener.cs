

using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Android.App;
using Android.Content;
using Android.Support.V4.Content;
using Android.Webkit;
using Notes.Droid.Services;
using Notes.Interfaces;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(DocumentOpener))]
namespace Notes.Droid.Services
{
    public class DocumentOpener : IPlatformDocumentOpener
    {
        static int requestCounter = 1;

        public bool CanOpen(string path)
        {
            try
            {
                var requestIntent = CreateOpenIntentWithoutAction(path);

                if (requestIntent.SetAction(Intent.ActionEdit).ResolveActivity(CrossCurrentActivity.Current.Activity.PackageManager) != null)
                {
                    return true;
                }
                if (requestIntent.SetAction(Intent.ActionView).ResolveActivity(CrossCurrentActivity.Current.Activity.PackageManager) != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<PlatformDocumentOpening> Open(string path)
        {
            var baseIntent = CreateOpenIntentWithoutAction(path);
            try
            {
                var actionIntent = await ShowOpenDialog(baseIntent);
                if (actionIntent == null)
                {
                    return new PlatformDocumentOpening { DidChange = false, DidOpen = false };
                }
                return await StartActivity(actionIntent, path);
            }
            catch (TaskCanceledException)
            {
                return new PlatformDocumentOpening { DidChange = false, DidOpen = true };
            }
        }

        Intent CreateOpenIntentWithoutAction(string path)
        {
            var uri = FileProvider.GetUriForFile(
                CrossCurrentActivity.Current.Activity,
                "com.companyname.Notes.fileprovider",
                new Java.IO.File(path));
            var mime = MimeTypeMap.Singleton.GetMimeTypeFromExtension(Path.GetExtension(path));

            return new Intent()
                .AddFlags(ActivityFlags.GrantReadUriPermission)
                .SetDataAndType(uri, mime);
        }

        static Task<Intent> ShowOpenDialog(Intent baseIntent)
        {
            var taskCompletionSource = new TaskCompletionSource<Intent>();
            AlertDialog dialog = null;
            var context = CrossCurrentActivity.Current.Activity;

            var iconEdit = ContextCompat.GetDrawable(context, Resource.Drawable.ic_mr_button_connected_23_light);
            var iconView = ContextCompat.GetDrawable(context, Resource.Drawable.ic_mr_button_connected_24_light);

            var intentEdit = baseIntent.SetAction(Intent.ActionEdit);
            var intentView = baseIntent.SetAction(Intent.ActionView);

            var optionGroupBuilder = new OptionGroup.Builder(context);

            var canEdit = false;
            var canView = false;

            if (intentEdit.ResolveActivity(context.PackageManager) != null)
            {
                canEdit = true;
                optionGroupBuilder = optionGroupBuilder.AddOption(iconEdit, "Edit", () =>
                {
                    taskCompletionSource.SetResult(intentEdit);
                    dialog?.Dismiss();
                });
            }
            if (intentView.ResolveActivity(context.PackageManager) != null)
            {
                canView = true;
                optionGroupBuilder = optionGroupBuilder.AddOption(iconView, "View", () =>
                {
                    taskCompletionSource.SetResult(intentView);
                    dialog?.Dismiss();
                });
            }
            if (canEdit && canView)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    dialog = new AlertDialog.Builder(context)
                                            .SetTitle("Open on Device")
                                            .SetNegativeButton("Cancel", (sender, e) => taskCompletionSource.SetCanceled())
                                            .SetCancelable(true)
                                            .SetView(optionGroupBuilder.Build())
                                            .Show();
                });
            }
            else if (canEdit)
            {
                taskCompletionSource.SetResult(intentEdit);
            }
            else if (canView)
            {
                taskCompletionSource.SetResult(intentView);
            }
            else
            {
                taskCompletionSource.SetResult(null);
            }
            return taskCompletionSource.Task;
        }

        Task<PlatformDocumentOpening> StartActivity(Intent requestIntent, String path)
        {

            var taskCompletionSource = new TaskCompletionSource<PlatformDocumentOpening>();
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    StartActivityForResult(requestIntent, (result, responseIntent) =>
                    {
                        taskCompletionSource.SetResult(new PlatformDocumentOpening
                        {
                            DidChange = false,
                            DidOpen = true,
                        });
                    });
                }
                catch (Exception e)
                {
                    taskCompletionSource.SetException(e);
                }
            });
            return taskCompletionSource.Task;
        }

        private void StartActivityForResult(Intent intent, System.Action<Result, Intent> onResult)
        {
            int originalRequestCode;
            unchecked
            {
                if (requestCounter < 0)
                {
                    requestCounter = 1;
                }
                originalRequestCode = requestCounter++;
            }

            if (CrossCurrentActivity.Current.Activity is MainActivity mainActivity)
            {
                System.Action<int, Result, Intent> listener = null;

                listener = (requestCode, resultCode, data) =>
                {
                    if (originalRequestCode != requestCode)
                    {
                        return;
                    }
                    onResult?.Invoke(resultCode, data);
                    mainActivity.ActivityResult -= listener;
                };
                mainActivity.ActivityResult += listener;
                mainActivity.StartActivityForResult(intent, originalRequestCode);
            }

        }
    }
}