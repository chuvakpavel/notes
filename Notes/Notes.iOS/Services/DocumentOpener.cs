using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using Notes.iOS.Services;
using Notes.Interfaces;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DocumentOpener))]
namespace Notes.iOS.Services
{
    public class DocumentOpener : IPlatformDocumentOpener
    {
        #region IPlatformDocumentOpener implementation

        public bool CanOpen(string path)
        {
            /// iOS provides no convenient mechanism to determine if files can be opened, so we assume all files can be.
			return true;
        }

        public Task<PlatformDocumentOpening> Open(string path)
        {
            var taskCompletionSource = new TaskCompletionSource<PlatformDocumentOpening>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {

                    var url = path.StartsWith("file://", StringComparison.InvariantCultureIgnoreCase)
                                  ? new NSUrl(path)
                                  : NSUrl.FromFilename(path);

                    var items = new List<NSObject>();
                    items.Add(url);
                    var controller = new UIActivityViewController(items.ToArray(), null);

                    if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
                    {
                        UIPopoverController popover = new UIPopoverController(controller);
                        var presentedView = UIApplication.SharedApplication.KeyWindow.RootViewController.View;
                        var presentedRect = new CGRect(presentedView.Bounds.Width / 2, presentedView.Bounds.Height, 0, 0);
                        popover.PresentFromRect(presentedRect, presentedView, UIPopoverArrowDirection.Any, true);
                    }
                    else
                    {
                        UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(controller, true, null);
                    }
                    var didOpen = true;
                    taskCompletionSource.SetResult(new PlatformDocumentOpening { DidChange = false, DidOpen = didOpen });
                }
                catch (Exception e)
                {
                    taskCompletionSource.SetException(e);
                }
            });
            return taskCompletionSource.Task;
        }

        #endregion
    }
}