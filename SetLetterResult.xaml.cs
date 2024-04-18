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

#if true
        //LetterStateEnum SetLetterRadioResult;
       // LetterState letterState;

        public SetLetterResult()
        {
            //this.letterState = letter;
            InitializeComponent();
        }

        

        private void SetLetterOkClick(object sender, RoutedEventArgs e)
        {
            //if (Match.IsChecked == true) { SetLetterRadioResult = LetterStateEnum.eMatch; }
            //else if (ValidWrongSpot.IsChecked == true) { SetLetterRadioResult = LetterStateEnum.eValidWrongSpot; }
            //else if (InvalidLetter.IsChecked == true) { SetLetterRadioResult = LetterStateEnum.eInvalid; }

            // do not exit unless choice is made
            if ((Match.IsChecked == true) || (ValidWrongSpot.IsChecked == true) || ( InvalidLetter.IsChecked == true))
            {
                Window parentWindow = (Window)this.Parent;
                //Window.GetWindow(this).DialogResult = true;
                parentWindow.Close();
            }
        }

        private void Match_Checked(object sender, RoutedEventArgs e)
        {
            Match.IsChecked = true;
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }

        private void ValidWrongSpot_Checked(object sender, RoutedEventArgs e)
        {
            ValidWrongSpot.IsChecked = true;
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }

        private void InvalidLetter_Checked(object sender, RoutedEventArgs e)
        {
            InvalidLetter.IsChecked = true;
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }

#else
        //public int SetLetterRadioResult = 0;  //1=Match, 2=ValidWrongSpot, 3=Invalid
        LetterStateEnum SetLetterRadioResult = LetterStateEnum.eUnknown;  //1=Match, 2=ValidWrongSpot, 3=Invalid
        //public SetLetterResult(ref LetterState result)
        public SetLetterResult()
        {
            //SetLetterRadioResult = result;
            InitializeComponent();
        }

        private void SetLetterOkClick(object sender, RoutedEventArgs e)
        {
            if (Match.IsChecked == true)                { SetLetterRadioResult = LetterStateEnum.eMatch; }
            else if (ValidWrongSpot.IsChecked == true)  { SetLetterRadioResult = LetterStateEnum.eValidWrongSpot; }
            else if (InvalidLetter.IsChecked == true)   { SetLetterRadioResult = LetterStateEnum.eInvalid; }

            Window parentWindow = (Window)this.Parent;
            //Window.GetWindow(this).DialogResult = true;
            parentWindow.Close();
        }

#endif
    }
}
