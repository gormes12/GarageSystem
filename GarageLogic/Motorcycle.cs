using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public enum eMotorcycleParams
    {
        LicenseType,
        EngineVolume
    }

    public enum eLicenseTypes
    {
        A = 1,
        A1,
        AA,
        B
    }

    public class Motorcycle : Vehicle
    {
        public const int k_NumOfLicenseTypesOptions = 4;
        public const int k_NumOfWheelsInMotorcycle = 2;
        public const int k_MaxPsiInElectricMotorcycle = 30;
        public const int k_MaxPsiInRegularMotorcycle = 30;
        public const int k_MinEngineVolume = 50;
        public const int k_MaxEngineVolume = 1200;
        public const float k_MaxLitersInFuelMotorcycle = 7f;
        public const float k_MaxBatteryHoursInElectricMotorcycle = 1.2f;
        private eLicenseTypes? m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(string i_LicenseNumber, EnergySourceSystem i_EnergySourceSystem, Tire[] i_Tires)
            : base(i_LicenseNumber, i_EnergySourceSystem, i_Tires)
        {
            m_LicenseType = null;
            m_EngineVolume = 0;
        }

        public override string VehilceType
        {
            get
            {
                return "Motorcycle";
            }
        }

        public override Dictionary<int, string> GetParams()
        {
            Dictionary<int, string> motorcycleParams = new Dictionary<int, string>();
            string msg = string.Format(
                                       "License type: {0}1.{1}{0}2.{2}{0}3.{3}{0}4.{4}",
                                       Environment.NewLine,
                                       eLicenseTypes.A, 
                                       eLicenseTypes.A1,
                                       eLicenseTypes.AA,
                                       eLicenseTypes.B);

            motorcycleParams.Add((int)eMotorcycleParams.EngineVolume, "Engine volume: ");
            motorcycleParams.Add((int)eMotorcycleParams.LicenseType, msg);
            return motorcycleParams;
        }

        public override void SetSpecificTypeParams(int i_IndexInEnum, string i_Value)
        {
            switch (i_IndexInEnum)
            {
                case (int)eMotorcycleParams.EngineVolume:
                    {
                        EngineVolume = Validation.CheckNumberValidation(i_Value, k_MinEngineVolume, k_MaxEngineVolume);
                        break;
                    }

                case (int)eMotorcycleParams.LicenseType:
                    {
                        LicenseType = (eLicenseTypes)Validation.CheckNumberValidation(i_Value, 1, k_NumOfLicenseTypesOptions);
                        break;
                    }
            }
        }

        // public properties
        public eLicenseTypes LicenseType
        {
            get
            {
                return (eLicenseTypes)m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }

            set
            {
                m_EngineVolume = value;
            }
        }

        public override string ToString()
        {
            StringBuilder motorcycleToString = new StringBuilder();

            motorcycleToString.Append(base.ToString());
            motorcycleToString.Append(string.Format(
@"License type: {0}
Engine volume: {1}",
                      m_LicenseType,
                      m_EngineVolume));

            return motorcycleToString.ToString();
        }
    }
}
