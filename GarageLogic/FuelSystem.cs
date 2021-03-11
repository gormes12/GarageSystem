using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public enum eFuelType
    {
        Octan98 = 1,
        Octan96,
        Octan95,
        Soler
    }

    public class FuelSystem : EnergySourceSystem
    {
        private readonly float r_MaxFuelInLiters;
        public const int k_NumOfFuelTypes = 4;
        private float m_CurrFuelInLiters;

        private eFuelType m_FuelType;

        public FuelSystem(float i_MaxFuelInLiters, eFuelType i_FuelType)
        {
            r_MaxFuelInLiters = i_MaxFuelInLiters;
            m_FuelType = i_FuelType;
            m_CurrFuelInLiters = 0;
        }

        // public properties
        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public override float CurrEnergy
        {
            get
            {
                return m_CurrFuelInLiters;
            }

            set
            {
                m_CurrFuelInLiters = value;
            }
        }

        public override string EnergyType
        {
            get
            {
                return "Fuel System";
            }
        }

        public override void ProvideSourceEnergy(float i_HoursToAdd)
        {
            throw new ArgumentException("You tried to refuel a fuel vehicle with electricity!");
        }

        public override float MaxEnergyPossible 
        {
            get
            {
                return r_MaxFuelInLiters;
            }
        }

        public override void ProvideSourceEnergy(float i_FuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType == m_FuelType)
            {
                if (r_MaxFuelInLiters - m_CurrFuelInLiters >= i_FuelToAdd)
                {
                    m_CurrFuelInLiters += i_FuelToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(r_MaxFuelInLiters - m_CurrFuelInLiters, 0, eOutOfRangeTypes.Number);
                }
            }
            else
            {
                throw new ArgumentException(
                            string.Format(
@"You tried to refuel with different type fuel of that vehicle!
The vehicle type fuel is: {0}",
                            m_FuelType));
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Fuel type: {0}
Fuel gauge: {1} liters", 
            m_FuelType,
            m_CurrFuelInLiters);
        }
    }
}
