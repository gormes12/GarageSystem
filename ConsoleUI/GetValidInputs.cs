using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;

namespace ConsoleUI
{
    internal class GetValidInputs
    {
        public static int GetValidInputNumber(int i_MinRange, int i_MaxRange)
        {
            string inputStr;
            int inputNum;
            bool parseSuccessed;

            inputStr = Console.ReadLine();
            parseSuccessed = int.TryParse(inputStr, out inputNum);
            while (!parseSuccessed || !isInNumberRange(i_MinRange, i_MaxRange, inputNum))
            {
                if (!parseSuccessed)
                {
                    Console.WriteLine("You must enter digits only! Try again!");
                }

                inputStr = Console.ReadLine();
                parseSuccessed = int.TryParse(inputStr, out inputNum);
            }

            return inputNum;
        }

        public static string GetValidLengthString(int i_MinRange, int i_MaxRange)
        {
            string inputStr;
            int inputNum;
            bool parseSuccessed;

            inputStr = Console.ReadLine();
            parseSuccessed = int.TryParse(inputStr, out inputNum);
            while (!parseSuccessed || !isInNumberRange(i_MinRange, i_MaxRange, inputStr.Length))
            {
                if (!parseSuccessed)
                {
                    Console.WriteLine("You must enter digits only! Try again!");
                }

                inputStr = Console.ReadLine();
                parseSuccessed = int.TryParse(inputStr, out inputNum);
            }

            return inputStr;
        }

        public static float GetValidInputNumber(float i_MinRange, float i_MaxRange)
        {
            string inputStr;
            float inputNum;
            bool parseSuccessed;

            inputStr = Console.ReadLine();
            parseSuccessed = float.TryParse(inputStr, out inputNum);
            while (!parseSuccessed || !isInNumberRange(i_MinRange, i_MaxRange, inputNum))
            {
                if (!parseSuccessed)
                {
                    Console.WriteLine("You must enter digits only! Try again!");
                }

                inputStr = Console.ReadLine();
                parseSuccessed = float.TryParse(inputStr, out inputNum);
            }

            return inputNum;
        }

        private static bool isInNumberRange(float i_MinRange, float i_MaxRange, float i_Input)
        {
            bool isValid = i_Input >= i_MinRange && i_Input <= i_MaxRange;

            if (!isValid)
            {
                Console.WriteLine("You must enter a number between {0} and {1} . Please try again!", i_MinRange, i_MaxRange);
            }

            return isValid;
        }

        public static string GetValidPhoneNumber()
        {
            int phoneNumberInInt;
            StringBuilder phoneNumber = new StringBuilder(11);
            string tempPhoneNumber = Console.ReadLine();

            while (tempPhoneNumber.Length != 10 || !tempPhoneNumber[0].Equals('0') || !int.TryParse(tempPhoneNumber, out phoneNumberInInt))
            {
                Console.WriteLine(
@"The phone number must contain exactly 10 digits that begin with '0'.
No other characters are allowed! Please try again!");
                tempPhoneNumber = Console.ReadLine();
            }

            phoneNumber.Append(tempPhoneNumber);
            phoneNumber.Insert(3, '-');

            return phoneNumber.ToString();
        }

        public static string GetValidStringOnlyLetters(int i_MinRange, int i_MaxRange)
        {
            string inputString;
            bool isValid = false;

            do
            {
                inputString = Console.ReadLine();
                if (inputString.Length < i_MinRange || inputString.Length > i_MaxRange)
                {
                    Console.WriteLine("The name must be at least {0} and maximum {1} characters. Please try again!", i_MinRange, i_MaxRange);
                }
                else if (!doesContainOnlyLetters(inputString))
                {
                    Console.WriteLine("The name must contain only letters.Please try again!");
                }
                else
                {
                    isValid = true;
                }
            }
            while (!isValid);

            return inputString;
        }

        private static bool doesContainOnlyLetters(string i_Str)
        {
            bool isOnlyLetters = true;

            foreach (char c in i_Str)
            {
                if (!char.IsLetter(c))
                {
                    isOnlyLetters = false;
                    break;
                }
            }

            return isOnlyLetters;
        }

        public static string GetValidLicenseNumber()
        {
            return GetValidLengthString(Vehicle.k_MinLicenseNumber, Vehicle.k_MaxLicenseNumber);
        }
    }
}
