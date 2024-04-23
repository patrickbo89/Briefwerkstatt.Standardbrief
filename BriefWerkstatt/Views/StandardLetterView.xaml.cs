﻿using System;
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
using System.Windows.Shapes;

namespace BriefWerkstatt.Views
{
    /// <summary>
    /// Interaction logic for StandardLetterView.xaml
    /// </summary>
    public partial class StandardLetterView : Window
    {
        public StandardLetterView()
        {
            InitializeComponent();
            
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //var dialogResult = System.Windows.Forms.MessageBox.Show(
            //    "Programm wird beendet. Sicher?",
            //    "Programm beenden?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //if (dialogResult == System.Windows.Forms.DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
        }
    }
}
