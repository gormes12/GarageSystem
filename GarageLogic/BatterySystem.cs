using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class BatterySystem : EnergySourceSystem
    {
        private readonly float r_MaxBatteryTime;
        private float m_BatteryTimeRemaining;

        public BatterySystem(float i_MaxBatteryTime) // c'tor
        {
            r_MaxBatteryTime = i_MaxBatteryTime;
            m_BatteryTimeRemaining = 0;
        }

        // public properties
        public override float MaxEnergyPossible
        {
            get
            {
                return r_MaxBatteryTime;
            }
        }

        public override float CurrEnergy
        {
            get
            {
                return m_BatteryTimeRemaining;
            }

            set
            {
                m_BatteryTimeRemaining = value;
            }
        }

        public override string EnergyType
        {
            get
            {
                return "Battery System";
            }
        }

        public override void ProvideSourceEnergy(float i_FuelToAdd, eFuelType i_FuelType)
        {
            throw new ArgumentException("You tried to charge an elctric vehicle with fuel!");
        }

        public override void ProvideSourceEnergy(float i_HoursToAdd)
        {
            if(r_MaxBatteryTime - m_BatteryTimeRemaining >= i_HoursToAdd)
            {
                m_BatteryTimeRemaining += i_HoursToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(r_MaxBatteryTime - m_BatteryTimeRemaining, 0, eOutOfRangeTypes.Number);
            }
        }

        public override string ToString()
        {
            return string.Format("Battery gauge: {0} hours", m_BatteryTimeRemaining);
        }
    }
}
