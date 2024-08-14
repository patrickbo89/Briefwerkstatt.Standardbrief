#region License
/* 
MIT License

Copyright (c) 2024 patrickbo89

https://github.com/patrickbo89

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BriefWerkstatt.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BriefWerkstatt.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BriefWerkstatt.Controls;assembly=BriefWerkstatt.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomTextBox/>
    ///
    /// </summary>
    public class CustomTextBox : System.Windows.Controls.TextBox
    {
        static CustomTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBox), new FrameworkPropertyMetadata(typeof(CustomTextBox)));
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                Text = Text.Trim();
            }

            HasErrors = Validation.GetHasError(this);

            base.OnLostFocus(e);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.ClickCount == 3)
            {
                SelectAll();
            }

            base.OnPreviewMouseLeftButtonDown(e);
        }

        public bool HasErrors
        {
            get { return (bool)GetValue(HasErrorsProperty); }
            set { SetValue(HasErrorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasErrors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasErrorsProperty =
            DependencyProperty.Register("HasErrors", typeof(bool), typeof(CustomTextBox), new PropertyMetadata(false));

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            HasErrors = Validation.GetHasError(this);

            base.OnTextChanged(e);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[^<>?/:|*\"]*$");
            if (!regex.IsMatch(e.Text) || e.Text.Contains('\\'))
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }
    }
}
