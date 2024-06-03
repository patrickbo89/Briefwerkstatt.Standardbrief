#region License
/* 
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

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
    }
}
