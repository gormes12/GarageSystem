using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    internal class Validation
    {
        public static void CheckStringValid(string i_Input, int i_MinRangeOfChars, int i_MaxRangeOfChars)
        {
            if (i_Input.Length < i_MinRangeOfChars || i_Input.Length > i_MaxRangeOfChars)
            {
                throw new ValueOutOfRangeException(i_MaxRangeOfChars, i_MinRangeOfChars, eOutOfRangeTypes.StringLength);
            }
            else if (!doesContainOnlyLettersAndNumbers(i_Input))
            {
                throw new FormatException("A name must contain only letters or numbers. Please try again!");
            }
        }

        private static bool doesContainOnlyLettersAndNumbers(string i_Str)
        {
            bool isOnlyLettersAndNumbers = true;

            foreach (char c in i_Str)
            {
                if (!char.IsLetter(c) && !char.IsDigit(c))
                {
                    isOnlyLettersAndNumbers = false;
                    break;
                }
            }

            return isOnlyLettersAndNumbers;
        }

        public static int CheckNumberValidation(string i_Number, int i_MinRange, int i_MaxRange)
        {
            int inputNum;

            if (!int.TryParse(i_Number, out inputNum))
            {
                throw new FormatException("You must enter digits only! Try again!");
            }
            else if (!(inputNum >= i_MinRange && inputNum <= i_MaxRange))
            {
                throw new ValueOutOfRangeException(i_MaxRange, i_MinRange, eOutOfRangeTypes.Number);
            }

            return inputNum;
        }

        public static float CheckNumberValidation(string i_Number, float i_MinRange, float i_MaxRange)
        {
            float inputNum;

            if (!float.TryParse(i_Number, out inputNum))
            {
                throw new FormatException("You must enter digits only! Try again!");
            }
            else if (!(inputNum >= i_MinRange && inputNum <= i_MaxRange))
            {
                throw new ValueOutOfRangeException(i_MaxRange, i_MinRange, eOutOfRangeTypes.Number);
            }

            return inputNum;
        }
    }
}
