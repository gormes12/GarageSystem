using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class Tire
    {
        private readonly float r_MaxValidPSI;
        public const int k_MinCharsForTireManufacturerName = 4;
        public const int k_MaxCharsForTireManufacturerName = 50;
        private string m_ManufacturerName;
        private float m_CurrentPSI;

        public Tire(float i_MaxValidPSI) // c'tor
        {
            r_MaxValidPSI = i_MaxValidPSI;
            m_ManufacturerName = null;
            m_CurrentPSI = 0;
        }

        // properties
        public float CurrentPSI
        {
            get
            {
                return m_CurrentPSI;
            }

            set
            {
                m_CurrentPSI = value;
            }
        }

        public float MaxPSI
        {
            get
            {
                return r_MaxValidPSI;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public void InflateTire(float i_AmountOfPsiToAdd)
        {
            if(r_MaxValidPSI - m_CurrentPSI < i_AmountOfPsiToAdd)
            {
                throw new ValueOutOfRangeException(r_MaxValidPSI - m_CurrentPSI, 0, eOutOfRangeTypes.Number);
            }
            else
            {
                m_CurrentPSI += i_AmountOfPsiToAdd;
            }
        }
    }
}
