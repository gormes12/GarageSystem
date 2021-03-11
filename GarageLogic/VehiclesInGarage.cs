using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public enum eVehicleStatuses
    {
        InRepair = 1,
        Repaired,
        Paid
    }

    public class VehiclesInGarage
    {
        public const int k_MinCharactersForOwnersName = 3;
        public const int k_MaxCharactersForOwnersName = 50;
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private eVehicleStatuses m_VehicleStatus;
        private Vehicle m_VehicleInfo;

        public VehiclesInGarage(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleInfo)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = eVehicleStatuses.InRepair;
            m_VehicleInfo = i_VehicleInfo;
        }

        // public properies
        public Vehicle VehicleInfo
        {
            get
            {
                return m_VehicleInfo;
            }
        }

        public eVehicleStatuses VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return r_OwnerName;
            }
        }

        public string OwnersPhoneNumber
        {
            get
            {
                return r_OwnerPhoneNumber;
            }
        }

        public override string ToString()
        {
            StringBuilder toString = new StringBuilder();

            toString.Append(string.Format(
@"-----------Details-----------
Vehicle owner's name: {0}
Vehicle owner's phone number: {1}
Vehicle status in garage: {2}",
                      r_OwnerName,
                      r_OwnerPhoneNumber,
                      m_VehicleStatus));

            return toString.ToString();
        }
    }
}
