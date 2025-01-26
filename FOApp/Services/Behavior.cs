using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOApp.Services
{
    internal class Behavior
    {
        public class ImageTapBehavior : Behavior<Image>
        {
            public static readonly BindableProperty? CommandProperty =
                BindableProperty.Create(nameof(Command), typeof(Command), typeof(ImageTapBehavior));

            public Command Command
            {
                get => (Command)GetValue(CommandProperty);
                set => SetValue(CommandProperty, value);
            }

            protected override void OnAttachedTo(Image bindable)
            {
                base.OnAttachedTo(bindable);
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += OnImageTapped;
                bindable.GestureRecognizers.Add(tapGestureRecognizer);
            }

            protected override void OnDetachingFrom(Image bindable)
            {
                base.OnDetachingFrom(bindable);
                foreach (var recognizer in bindable.GestureRecognizers)
                {
                    if (recognizer is TapGestureRecognizer tapGestureRecognizer)
                    {
                        tapGestureRecognizer.Tapped -= OnImageTapped;
                    }
                }
            }

            private void OnImageTapped(object sender, EventArgs e)
            {
                Command?.Execute(null);
            }
        }
    }
}
