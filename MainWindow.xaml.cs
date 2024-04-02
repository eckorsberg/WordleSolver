#define MatchDebugx

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string MATCH_WORD = "early";

        private int NextRow = 0;                                // next row of 6,5 matrix to fill
        string ValidWordleList;                                 // the working list of potential words
        string ValidWordleListOriginal;                         // the initial list of potential words
        LetterState[,] letterStates = new LetterState[6, 5];    // state of each letter in matrix
        Label[,] letterLabels = new Label[6, 5];                // ref to labels in matrix
        char[] Answer = new char[5];                            // container for potential answer
        string InvalidChars = "";                               // list of letters NOT in answer
        string ValidChars = "";                                 // list of letters somewhere in answer

        #region Init
        public MainWindow()
        {
            InitializeComponent();

            // create [5,6] array
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    letterStates[row, col] = new LetterState();
                    letterStates[row, col].SetColumn(col);
                }
            }
            letterLabels[0, 0] = LabelR1C1;
            letterLabels[0, 1] = LabelR1C2;
            letterLabels[0, 2] = LabelR1C3;
            letterLabels[0, 3] = LabelR1C4;
            letterLabels[0, 4] = LabelR1C5;

            letterLabels[1, 0] = LabelR2C1;
            letterLabels[1, 1] = LabelR2C2;
            letterLabels[1, 2] = LabelR2C3;
            letterLabels[1, 3] = LabelR2C4;
            letterLabels[1, 4] = LabelR2C5;

            letterLabels[2, 0] = LabelR3C1;
            letterLabels[2, 1] = LabelR3C2;
            letterLabels[2, 2] = LabelR3C3;
            letterLabels[2, 3] = LabelR3C4;
            letterLabels[2, 4] = LabelR3C5;

            letterLabels[3, 0] = LabelR4C1;
            letterLabels[3, 1] = LabelR4C2;
            letterLabels[3, 2] = LabelR4C3;
            letterLabels[3, 3] = LabelR4C4;
            letterLabels[3, 4] = LabelR4C5;

            letterLabels[4, 0] = LabelR5C1;
            letterLabels[4, 1] = LabelR5C2;
            letterLabels[4, 2] = LabelR5C3;
            letterLabels[4, 3] = LabelR5C4;
            letterLabels[4, 4] = LabelR5C5;

            letterLabels[5, 0] = LabelR6C1;
            letterLabels[5, 1] = LabelR6C2;
            letterLabels[5, 2] = LabelR6C3;
            letterLabels[5, 3] = LabelR6C4;
            letterLabels[5, 4] = LabelR6C5;


            //open wordle.txt
            ValidWordleListOriginal = System.IO.File.ReadAllText("../../../valid-wordle-words.txt");  //wordle-La

            Init();
        }
        private void Init()
        {
            NextRow = 0;

            ValidWordleList = System.IO.File.ReadAllText("../../../wordle-La.txt");  //wordle-La
            WordChoices.Text = ValidWordleList;
            int nlines = ComputeNumberOfLines(ValidWordleList);
            NwordsBox.Text = nlines.ToString();

            AnswerBox.Text = "?????";

            for (int col = 0; col < 5; col++)
            {
                Answer[col] = '?';
            }

            Answer = new char[5];                            // container for potential answer
            InvalidChars = "";                               // list of letters NOT in answer
            ValidChars = "";                                 // list of letters somewhere in answer

            GuessNext.Text = "";
            InvalidLettersBox.Text = "";

            SolidColorBrush br = new SolidColorBrush();
            br.Color = Colors.White;

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    letterLabels[row, col].Content = '?';

                    letterStates[row,col].SetState(LetterStateEnum.eMatch);
                    letterLabels[row, col].Background = br;     //set color of label
                }
            }


        }
        #endregion



        #region LabelClicks
        // row 1
        private void LabelR1C1MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[0, 0], ref letterLabels[0, 0]);
        }

        private void LabelR1C2MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[0, 1], ref letterLabels[0, 1]);
        }
        private void LabelR1C3MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[0, 2], ref letterLabels[0, 2]);
        }
        private void LabelR1C4MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[0, 3], ref letterLabels[0, 3]);
        }
        private void LabelR1C5MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[0, 4], ref letterLabels[0, 4]);
        }

        // row 2
        private void LabelR2C1MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[1, 0], ref letterLabels[1, 0]);
        }
        private void LabelR2C2MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[1, 1], ref letterLabels[1, 1]);
        }
        private void LabelR2C3MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[1, 2], ref letterLabels[1, 2]);
        }
        private void LabelR2C4MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[1, 3], ref letterLabels[1, 3]);
        }
        private void LabelR2C5MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[1, 4], ref letterLabels[1, 4]);
        }

        // row 3
        private void LabelR3C1MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[2, 0], ref letterLabels[2, 0]);
        }
        private void LabelR3C2MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[2, 1], ref letterLabels[2, 1]);
        }
        private void LabelR3C3MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[2, 2], ref letterLabels[2, 2]);
        }
        private void LabelR3C4MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[2, 3], ref letterLabels[2, 3]);
        }
        private void LabelR3C5MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[2, 4], ref letterLabels[2, 4]);
        }

        // row 4
        private void LabelR4C1MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[3, 0], ref letterLabels[3, 0]);
        }
        private void LabelR4C2MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[3, 1], ref letterLabels[3, 1]);
        }
        private void LabelR4C3MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[3, 2], ref letterLabels[3, 2]);
        }
        private void LabelR4C4MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[3, 3], ref letterLabels[3, 3]);
        }
        private void LabelR4C5MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[3, 4], ref letterLabels[3, 4]);
        }

        // row 5
        private void LabelR5C1MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[4, 0], ref letterLabels[4, 0]);
        }
        private void LabelR5C2MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[4, 1], ref letterLabels[4, 1]);
        }
        private void LabelR5C3MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[4, 2], ref letterLabels[4, 2]);
        }
        private void LabelR5C4MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[4, 3], ref letterLabels[4, 3]);
        }
        private void LabelR5C5MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetLetterStatus(ref letterStates[4, 4], ref letterLabels[4, 4]);
        }
        #endregion
        /// <summary>
        /// Given an input word, will search the list of valid Wordle words if this is a valid guess
        /// </summary>
        /// <param name="quess"></param>
        /// <returns>true if guess is valid, else false</returns>
        bool IsWordValid(string quess)
        {
            bool bResult = false;

            System.IO.StringReader strReader = new System.IO.StringReader(ValidWordleListOriginal);
            string aLine = strReader.ReadLine();
            while (aLine != null)
            {
                if (quess == aLine)
                {
                    bResult = true;
                    break;
                }
                aLine = strReader.ReadLine();
            }

            return bResult;
        }

        /// <summary>
        /// Shared handler to label click events, opens dialog box to mark state of entered letters
        /// this is the main UI portion of the app
        /// </summary>
        /// <param name="rLetter"></param>
        /// <param name="rLabel"></param>
        private void GetLetterStatus(ref LetterState rLetter, ref Label rLabel)
        {
            var c = rLabel.Content;
            if (c.ToString()[0] == '?')
            {
                return;
            }

            // Popup dialog to mark state of letter
            SetLetterResult se = new SetLetterResult();
            Window setLetterResult = new Window
            {
                Title = "My User Control Dialog",
                Content = se
            };

            setLetterResult.ShowDialog();
            //get results from setLetterResult

            if (se.Match.IsChecked == true)
            {
                //found exact match, should greatly reduce list
                Answer[rLetter.GetColumn()] = rLetter.GetLetter();
                UpdateAnswerBox();

                //set color to Green and enum
                SolidColorBrush br = new SolidColorBrush();
                rLetter.SetState(LetterStateEnum.eMatch);
                br.Color = Colors.Green;
                rLabel.Background = br;     //set color of label

                // next reduce list of potential words
                //basically will remove entries that do not have this letter in the correct column
                ValidWordleList = RemoveIfNotMatched(rLetter.GetLetter(), rLetter.GetColumn(), ValidWordleList);
                WordChoices.Text = ValidWordleList;
                int nlines = ComputeNumberOfLines(ValidWordleList);
                NwordsBox.Text = nlines.ToString();

            }
            else if (se.ValidWrongSpot.IsChecked == true)
            {
                //found a valid letter, remove any words that do NOT contain this
                ValidChars += rLetter.GetLetter();

                //set color to Yellow and enum
                SolidColorBrush br = new SolidColorBrush();
                rLetter.SetState(LetterStateEnum.eValidWrongSpot);
                br.Color = Colors.Yellow;
                rLabel.Background = br;     //set color of label

                //remove words missing this letter
                ValidWordleList = RemoveWordsMissingValidChars(ValidChars, ValidWordleList);
                WordChoices.Text = ValidWordleList;
                int nlines = ComputeNumberOfLines(ValidWordleList);
                NwordsBox.Text = nlines.ToString();


                //remove words missing this letter IN THIS COLUMN
                ValidWordleList = RemoveIfNotInCorrectColumn(rLetter.GetLetter(), rLetter.GetColumn(), ValidWordleList);
                WordChoices.Text = ValidWordleList;
                nlines = ComputeNumberOfLines(ValidWordleList);
                NwordsBox.Text = nlines.ToString();
            }
            else if (se.InvalidLetter.IsChecked == true)
            {
                //found invalid letter, reduce list
                InvalidChars += rLetter.GetLetter();

                //set color to Gray and enum
                SolidColorBrush br = new SolidColorBrush();
                rLetter.SetState(LetterStateEnum.eInvalid);
                br.Color = Colors.Gray;
                rLabel.Background = br;     //set color of label
                WordChoices.Text = "";  // clear list of potential words

                // next reduce list of potential words
                char invalidLetter = rLetter.GetLetter();
                string InvalidLetters = invalidLetter.ToString();
                ValidWordleList = RemoveInvalid(InvalidLetters, ValidWordleList);
                WordChoices.Text = ValidWordleList;
                int nlines = ComputeNumberOfLines(ValidWordleList);
                NwordsBox.Text = nlines.ToString();

                //update list of invalid letters
                InvalidLettersBox.Text = InvalidChars;
            }
        }
        /// <summary>
        /// Given a string with \n linefeeds, compute the number of lines in this string
        /// </summary>
        /// <param name="list"></param>
        /// <returns>The number of lines delimited by \n</returns>
        private int ComputeNumberOfLines(string list)
        {
            int nlines = 0;
            System.IO.StringReader strReader = new System.IO.StringReader(list);
            string aLine = strReader.ReadLine();
            while (aLine != null)
            {
                nlines++;
                aLine = strReader.ReadLine();
            }
            return nlines;
        }

        private void UpdateAnswerBox()
        {
            AnswerBox.Text = new string(Answer);
        }

        #region TextboxActions
        /// <summary>
        /// User entered a set of char's in the Invalid letter TextBox.
        /// Add to the list, remove the potential set of words containing an invalid letter and update the UI matrix color.
        /// </summary>
        private void EnterInvalidEntry()
        {
            InvalidChars = InvalidLettersBox.Text;
            ValidWordleList = RemoveInvalid(InvalidLettersBox.Text, ValidWordleList);
            WordChoices.Text = ValidWordleList;
            int nlines = ComputeNumberOfLines(ValidWordleList);
            NwordsBox.Text = nlines.ToString();


            //update label matrix
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    var c = letterLabels[row, col].Content;
                    //todo this might need refinement, if I enter letters before gui then all 5 letters are marked as invalid
                    if (c.ToString()[0] != '?')
                    {
                        for (int n = 0; n < InvalidChars.Length; n++)
                        {
                            if (InvalidChars.Contains(c.ToString()))
                            {
                                SolidColorBrush br = new SolidColorBrush();
                                letterStates[row, col].SetState(LetterStateEnum.eInvalid);
                                br.Color = Colors.Gray;
                                letterLabels[row, col].Background = br;     //set color of label
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called when Enter key pressed on guess textbox
        /// </summary>
        private void EnterQuess()
        {
            if (IsWordValid(GuessNext.Text) == false)
            {
                //clear field
                GuessNext.Text = "";
                return;
            }

            if (NextRow < 6)
            {
                int len = Math.Min(GuessNext.Text.Length, 5);

                for (int col = 0; col < len; col++)
                {
                    letterLabels[NextRow, col].Content = GuessNext.Text[col];
                    letterStates[NextRow, col].SetLetter(GuessNext.Text[col]);
                }

                //if not first row, color new lines if matched or invalid
                if (NextRow > 0)
                {
                    for (int col = 0; col < len; col++)
                    {
                        if (GuessNext.Text[col] == Answer[col])
                        {
                            SolidColorBrush br = new SolidColorBrush();
                            br.Color = Colors.Green;
                            letterLabels[NextRow, col].Background = br;     //set color of label
                        }
                        else if (InvalidChars.Contains(GuessNext.Text[col].ToString()))
                        {
                            SolidColorBrush br = new SolidColorBrush();
                            br.Color = Colors.Gray;
                            letterLabels[NextRow, col].Background = br;     //set color of label
                        }
                    }
                }
                NextRow++;
            }
        }
        #endregion

        #region RemoveWords
        /// <summary>
        /// Remove potential words that do NOT contain the valid characters
        /// </summary>
        /// <param name="validChars"></param>
        /// <param name="validWordleList"></param>
        /// <returns></returns>
        private string RemoveWordsMissingValidChars(string validChars, string validWordleList)
        {
            int len = Math.Min(validChars.Length, 26);

            //todo "share" should remove "abbot"
            //"ar" accepts "abbot" due to "a" but conversely the "r" as a valid letter should reject "abbot" since it contains no "r"
            //throw new NotImplementedException();
            //todo "share" removes "early"

            string aLine = null;
            string ReturnString = null;
            System.IO.StringReader strReader = new System.IO.StringReader(validWordleList);
            aLine = strReader.ReadLine();
            while (aLine != null)
            {
#if MatchDebug
                if (aLine == MATCH_WORD)
                {
                    int foo = 1;
                }
#endif
                bool allCharsFound = true;
                for (int i = 0; i < validChars.Length; i++)
                {
                    if (aLine.Contains(validChars[i]) == false)
                    {
                        allCharsFound = false;
                        //ReturnString += aLine + '\n';
                        break;
                    }
                }
                if (allCharsFound)
                {
                    ReturnString += aLine + '\n';
                }

                aLine = strReader.ReadLine();
            }

            return ReturnString;
        }

        /// <summary>
        /// remove words that contain invalid letters
        /// </summary>
        /// <param name="invalidLetters"></param>
        /// <param name="validWordleList"></param>
        /// <returns></returns>
        private string RemoveInvalid(string invalidLetters, string validWordleList)
        {
            int len = Math.Min(invalidLetters.Length, 26);

            string aLine = null;
            string ReturnString = null;
            System.IO.StringReader strReader = new System.IO.StringReader(validWordleList);
            aLine = strReader.ReadLine();
            while (aLine != null)
            {
#if MatchDebug
                if (aLine == MATCH_WORD)
                {
                    int foo = 1;
                }
#endif

                bool keepThisWord = true;
                for (int i = 0; i < len; i++)
                {
                    if (aLine.Contains(invalidLetters[i]))
                    {
                        keepThisWord = false;
                        break;
                    }
                }
                if (keepThisWord == true)
                {
                    ReturnString += aLine + '\n';
                }
                aLine = strReader.ReadLine();
            }
            return ReturnString;
        }

        /// <summary>
        /// remove words if a valid letter is not in the correct column
        /// </summary>
        /// <param name="matchedLetter"></param>
        /// <param name="column"></param>
        /// <param name="validWordleList"></param>
        /// <returns></returns>
        private string RemoveIfNotMatched(char matchedLetter, int column, string validWordleList)
        {
            string aLine = null;
            string ReturnString = null;

            if (matchedLetter != '?')
            {
                System.IO.StringReader strReader = new System.IO.StringReader(validWordleList);
                aLine = strReader.ReadLine();
                while (aLine != null)
                {
#if MatchDebug
                    if (aLine == MATCH_WORD)
                    {
                        int foo = 1;
                    }
#endif

                    if (aLine[column] == matchedLetter)
                    {
                        ReturnString += aLine + '\n';
                    }
                    aLine = strReader.ReadLine();
                }
            }
            return ReturnString;
        }

        /// <summary>
        /// remove words if a valid letter is not in the correct column
        /// </summary>
        /// <param name="matchedLetter"></param>
        /// <param name="column"></param>
        /// <param name="validWordleList"></param>
        /// <returns></returns>
        private string RemoveIfNotInCorrectColumn(char matchedLetter, int column, string validWordleList)
        {
            string aLine = null;
            string ReturnString = null;

            if (matchedLetter != '?')
            {
                System.IO.StringReader strReader = new System.IO.StringReader(validWordleList);
                aLine = strReader.ReadLine();
                while (aLine != null)
                {
#if MatchDebug
                    if (aLine == MATCH_WORD)
                    {
                        int foo = 1;
                    }
#endif

                    if (aLine[column] != matchedLetter)
                    {
                        ReturnString += aLine + '\n';
                    }
                    aLine = strReader.ReadLine();
                }
            }
            return ReturnString;
        }
        #endregion

        #region KeyDown
        /// <summary>
        /// called when Enter key on guess textbox called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //enter key is down
                EnterQuess();
            }
        }

        private void InvalidBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //enter key is down
                EnterInvalidEntry();
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // re-init
            Init();
        }

    }
}
