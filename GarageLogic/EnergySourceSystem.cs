using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class EnergySourceSystem
    {
        public abstract void ProvideSourceEnergy(float i_FuelToAdd, eFuelType i_FuelType);

        public abstract void ProvideSourceEnergy(float i_HoursToAdd);

        public abstract float MaxEnergyPossible
        {
            get;
        }

        public abstract float CurrEnergy
        {
            get;
            set;
        }

        public abstract string EnergyType
        {
            get;
        }

        public abstract override string ToString();
    }
}
