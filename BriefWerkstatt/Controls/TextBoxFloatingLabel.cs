using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

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
    ///     <MyNamespace:TextBoxFloatingLabel/>
    ///
    /// </summary>
    public class TextBoxFloatingLabel : TextBox
    {
        public string FloatingLabelText
        {
            get { return (string)GetValue(FloatingLabelTextProperty); }
            set { SetValue(FloatingLabelTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FloatingLabelTextProperty =
            DependencyProperty.Register("FloatingLabelText", typeof(string), typeof(TextBoxFloatingLabel), new PropertyMetadata(string.Empty));


        // Property um zu ermitteln, ob der Text der TextBox leer ist. Da es sich bei dem DependencyProperty um ein
        // READONLY handelt (Erklärung siehe unten), muss der Setter den DependencyPropertyKey 'setten' und nicht das DependencyProperty selbst,
        // dieses wird nur beim 'Getten' gelesen.
        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(IsEmptyPropertyKey, value); }
        }

        // READONLY DependencyProperty, damit verhindert wird, dass jemand dieses im XAML einfach 'settet'. Nur OnTextChanged soll dies tun.
        // Readonly DependencyProperties benötigen einen DependencyPropertyKey und dieser wird als ReadOnly registriert.
        private static readonly DependencyPropertyKey IsEmptyPropertyKey =
            DependencyProperty.RegisterReadOnly("IsEmpty", typeof(bool), typeof(TextBoxFloatingLabel), new PropertyMetadata(true));

        // Nun wird dem eigentlichen DependencyProperty das DependencyProperty des DependencyPropertyKeys zugewiesen.
        public static readonly DependencyProperty IsEmptyProperty = IsEmptyPropertyKey.DependencyProperty;

        // Setzt die Style-Ressource der TextBox, die standardmäßig aus der /Themes/Generic.xaml entnommen wird. Im
        // Generic.xaml wird der Style vom Developer definiert.
        static TextBoxFloatingLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxFloatingLabel), new FrameworkPropertyMetadata(typeof(TextBoxFloatingLabel)));
        }





        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(TextBoxFloatingLabel), new PropertyMetadata(null));





        // Bei jeder Textänderung in der TextBox durch Nutzereingaben wird überprüft, ob der Text leer ist und IsEmpty entsprechend gesetzt.
        // Dient der <ControlTemplate.Trigger>-Setzung im Generic.xaml um zu entscheiden, ob das VerticalAlignment des
        // Floating Labels nach oben (Top) oder zentriert (Center) gesetzt wird.
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            IsEmpty = string.IsNullOrEmpty(Text);

            base.OnTextChanged(e);
        }
    }
}
