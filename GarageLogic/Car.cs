using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public enum eCarParams
    {
        CarColor,
        NumOfDoors
    }
    
    public enum eCarColors
    {
        Red = 1,
        White,
        Black,
        Silver 
    }


    // $G$ CSS-999 (-3) Each enum\struct\class which is non nested should be in separate file

    public class Car : Vehicle
    {
        public const uint k_NumOfColorsOptions = 4;
        public const int k_MinNumOfDoors = 2;
        public const int k_MaxNumOfDoors = 5;
        public const int k_NumOfWheelsInCar = 4;
        public const int k_MaxPsiInFuelCar = 32;
        public const int k_MaxPsiInElectricCar = 32;
        public const float k_MaxLitersInFuelCar = 60;
        public const float k_MaxBatteryHoursInElectricCar = 2.1f;
        private eCarColors? m_CarColor;
        private uint m_NumOfDoors;

        public Car(string i_LicenseNumber, EnergySourceSystem i_EnergySourceSystem, Tire[] i_Tires) 
            : base(i_LicenseNumber, i_EnergySourceSystem, i_Tires)
        {
            m_CarColor = null;
            m_NumOfDoors = 0;
        }
        
        // public properties
        public eCarColors CarColor
        {
            get
            {
                return (eCarColors)m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }

        public uint NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }

            set
            {
                m_NumOfDoors = value;
            }
        }

        public override string VehilceType
        {
            get
            {
                return "Car";
            }
        }

        public override Dictionary<int, string> GetParams()
        {
            Dictionary<int, string> carParams = new Dictionary<int, string>();
            string msg = string.Format(
                                       "Car color: {0}1.{1}{0}2.{2}{0}3.{3}{0}4.{4}",
                                        Environment.NewLine,
                                        eCarColors.Red,
                                        eCarColors.White,
                                        eCarColors.Black,
                                        eCarColors.Silver);
            carParams.Add((int)eCarParams.CarColor, msg);
            carParams.Add((int)eCarParams.NumOfDoors, "Number of doors:");

            return carParams;
        }

        public override void SetSpecificTypeParams(int i_IndexInEnum, string i_Value)
        {
            switch (i_IndexInEnum)
            {
                case (int)eCarParams.NumOfDoors:
                    {
                        m_NumOfDoors = (uint)Validation.CheckNumberValidation(i_Value, k_MinNumOfDoors, k_MaxNumOfDoors);
                        break;
                    }

                case (int)eCarParams.CarColor:
                    {
                        m_CarColor = (eCarColors)Validation.CheckNumberValidation(i_Value, 1, k_NumOfColorsOptions);
                        break;
                    }
            }
        }

        public override string ToString()
        {
            StringBuilder carToString = new StringBuilder();

            carToString.Append(base.ToString());
            carToString.Append(string.Format(
@"Car color: {0}
Number of doors: {1}",
                      m_CarColor,
                      m_NumOfDoors));

            return carToString.ToString();
        }
    }
}
