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

namespace WordleSover
{
    /// <summary>
    /// Interaction logic for SetLetterResult.xaml
    /// </summary>
    public partial class SetLetterResult : UserControl
    {
        public SetLetterResult()
        {
            //this.letterState = letter;
            InitializeComponent();
        }

        private void Match_Checked(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }

        private void ValidWrongSpot_Checked(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }

        private void InvalidLetter_Checked(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }
    }
}
