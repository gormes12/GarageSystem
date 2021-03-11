using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public enum eVehiclesParameters
    {
        Model,
        LicenseNumber,
        EnergyLeftInPrecents,
        EnergySourceSystem,
        ManufacturerName,
        CurrentPSI,
        CurrentEnergyLeft
    }

    public abstract class Vehicle
    {
        public const int k_MinLicenseNumber = 6;
        public const int k_MaxLicenseNumber = 8;
        public const int k_MinModelCharacters = 3;
        public const int k_MaxModelCharacters = 10;
        protected readonly string r_LicenseNumber;
        protected string m_Model;
        protected float m_EnergyLeftInPrecents;
        protected Tire[] m_Tires;
        protected EnergySourceSystem m_EnergySourceSystem;

        public Vehicle(string i_LicenseNumber, EnergySourceSystem i_EnergySourceSystem, Tire[] i_Tires)
        {
            r_LicenseNumber = i_LicenseNumber;
            m_Tires = i_Tires;
            m_EnergySourceSystem = i_EnergySourceSystem;
            m_Model = null;
            m_EnergyLeftInPrecents = 0;
        }

        /// public properties
        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public Tire[] Tires
        {
            get
            {
                return m_Tires;
            }
        }

        public string Model
        {
            get
            {
                return m_Model;
            }

            set
            {
                m_Model = value;
            }
        }

        public EnergySourceSystem VehicleEnergySourceSystem
        {
            get
            {
                return m_EnergySourceSystem;
            }
        }

        public float EnergyLeftInPrecents
        {
            get
            {
                return m_EnergyLeftInPrecents;
            }

            set
            {
                m_EnergyLeftInPrecents = value;
            }
        }

        public abstract string VehilceType
        {
            get;
        }

        public Dictionary<int, string> GetVehicleParams()
        {
            Dictionary<int, string> vehicleParams = new Dictionary<int, string>();

            vehicleParams.Add((int)eVehiclesParameters.Model, "vehicle's model:");
            vehicleParams.Add((int)eVehiclesParameters.ManufacturerName, "Tire's manufacturer name:");
            vehicleParams.Add((int)eVehiclesParameters.CurrentPSI, "Current PSI:");
            vehicleParams.Add((int)eVehiclesParameters.CurrentEnergyLeft, "Current Energy Left:");

            return vehicleParams;
        }

        public void SetVehicleParameters(int i_IndexInEnum, string i_Value)
        {
            switch (i_IndexInEnum)
            {
                case (int)eVehiclesParameters.Model:
                    {
                        Validation.CheckStringValid(i_Value, k_MinModelCharacters, k_MaxModelCharacters);
                        m_Model = i_Value;
                        break;
                    }

                case (int)eVehiclesParameters.ManufacturerName:
                    {
                        Validation.CheckStringValid(i_Value, Tire.k_MinCharsForTireManufacturerName, Tire.k_MaxCharsForTireManufacturerName);
                        foreach (Tire tire in Tires)
                        {
                            tire.ManufacturerName = i_Value;
                        }

                        break;
                    }

                case (int)eVehiclesParameters.CurrentPSI:
                    {
                        float currPSI = Validation.CheckNumberValidation(i_Value, 0, m_Tires[0].MaxPSI);
                        foreach (Tire tire in Tires)
                        {
                            tire.CurrentPSI = currPSI;
                        }

                        break;
                    }

                case (int)eVehiclesParameters.CurrentEnergyLeft:
                    {
                        float currEnergy = Validation.CheckNumberValidation(i_Value, 0, m_EnergySourceSystem.MaxEnergyPossible);
                        m_EnergySourceSystem.CurrEnergy = currEnergy; 
                        m_EnergyLeftInPrecents = currEnergy / m_EnergySourceSystem.MaxEnergyPossible; /// while printed it's multiplied by 100 
                        break;
                    }
            }
        }

        public abstract Dictionary<int, string> GetParams();

        public abstract void SetSpecificTypeParams(int i_IndexInEnum, string i_Value);

        public override string ToString()
        {
            StringBuilder vehicleToString = new StringBuilder();

            vehicleToString.Append(string.Format(
@"Vehicle's type: {0}
Energy system type: {1}
License number: {2}
Model name: {3}
Tires manufacturer name: {4}
Tires PSI: {5}
",
                this.VehilceType,
                m_EnergySourceSystem.EnergyType,
                r_LicenseNumber,
                m_Model,
                m_Tires[0].ManufacturerName,
                m_Tires[0].CurrentPSI));
            vehicleToString.Append(m_EnergySourceSystem.ToString());
            vehicleToString.Append(string.Format(" ({0:p}) {1}", m_EnergyLeftInPrecents, Environment.NewLine));

            return vehicleToString.ToString();
        }

        public void UpdateEnergyLeftInPrecents()
        {
            m_EnergyLeftInPrecents = m_EnergySourceSystem.CurrEnergy / m_EnergySourceSystem.MaxEnergyPossible;
        }
    }
}
