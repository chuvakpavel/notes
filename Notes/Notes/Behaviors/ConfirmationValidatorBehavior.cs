using Xamarin.Forms;

namespace Notes.Behaviors
{
    public class ConfirmationValidatorBehavior : Behavior<Entry>
    {
        internal static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool), typeof(ConfirmationValidatorBehavior), false);
        public static readonly BindableProperty ConfirmationTextProperty = BindableProperty.Create(nameof(ConfirmationText), typeof(string), typeof(ConfirmationValidatorBehavior), string.Empty);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        private string EntryText { get; set; }
        public string ConfirmationText
        {
            get => (string)GetValue(ConfirmationTextProperty);
            set => SetValue(ConfirmationTextProperty, value);
        }

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            if (bindable is Entry entry)
            {
                entry.TextChanged += TextChanged;
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null) return;
            if (sender is Entry entry)
            {
                EntryText = entry.Text;
                IsValid = entry.Text.Equals(ConfirmationText);
                entry.TextColor = IsValid ? Color.Black : Color.Red;
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            if (bindable is Entry entry)
            {
                entry.TextChanged -= TextChanged;
            }
        }
    }
}
