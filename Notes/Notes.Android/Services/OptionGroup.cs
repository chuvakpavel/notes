using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Notes.Droid.Services
{
    public static class OptionGroup
    {
        public class Builder
        {
            readonly Context context;
            readonly Builder parent;
            readonly Android.Views.View view;

            public Builder(Context context)
            {
                this.context = context;
            }

            Builder(Context context, Builder parent, Android.Views.View view)
            {
                this.context = context;
                this.parent = parent;
                this.view = view;
            }

            public Builder AddOption(Drawable icon, string text, Action action)
            {
                var button = new Button(context);
                {
                    int[] attrs = { Android.Resource.Attribute.SelectableItemBackground };
                    var typedArray = context.ObtainStyledAttributes(attrs);
                    var backgroundResource = typedArray.GetResourceId(0, 0);
                    typedArray.Recycle();
                    button.SetBackgroundResource(backgroundResource);
                }
                //button.CompoundDrawablePadding = (int)16.DipToPx();
                button.Gravity = GravityFlags.Start | GravityFlags.CenterVertical;
                button.SetCompoundDrawablesRelativeWithIntrinsicBounds(icon, null, null, null);
                //button.SetPadding();SetPaddingDip(22, 11, 22, 11);
                //button.SetMargins(0, 0, 0, 0);
                button.Text = text;
                button.Click += (sender, e) => action?.Invoke();

                return new Builder(context, this, button);
            }

            public ViewGroup Build()
            {
                var layout = new LinearLayout(context);
                layout.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                layout.Orientation = Orientation.Vertical;
                //layout.SetPaddingDip(0, 16, 0, 0);
                var stack = new Stack<Android.Views.View>();
                for (var builder = this; builder != null && builder.view != null; builder = builder.parent)
                {
                    stack.Push(builder.view);
                }
                foreach (var item in stack)
                {
                    layout.AddView(item);
                }
                return layout;
            }
        }
    }
}