using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleSover
{
    public enum LetterStateEnum {eUnknown, eNotClassifiedYet, eMatch, eValidWrongSpot, eInvalid };
    public class LetterState
    {
        LetterStateEnum letterState;    // the state of this letter
        char letter;    // the letter in the lable (ie row/column)
        int column;     // which column the letter is in, needed for exact match filtering


        public void SetState(LetterStateEnum e)
        {
            letterState = e;
        }
        public LetterStateEnum GetState()
        {
            return letterState;
        }

        public char GetLetter()
        {
            return letter;
        }
        public void SetLetter(char l)
        {
            letter = l;
        }

        public int GetColumn()
        {
            return column;
        }
        public void SetColumn(int col)
        {
            column = col;
        }


    }

}
