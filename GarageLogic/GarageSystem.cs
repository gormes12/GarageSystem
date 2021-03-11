using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class GarageSystem
    {
        private Dictionary<string, VehiclesInGarage> m_VehiclesInGarage;

        public GarageSystem()
        {
            m_VehiclesInGarage = new Dictionary<string, VehiclesInGarage>();
        }

        // public properties
        public Dictionary<string, VehiclesInGarage> VehiclesInTheGarage
        {
            get
            {
                return m_VehiclesInGarage;
            }
        }

        public VehiclesInGarage GetVehicleByLicenseNumber(string i_LicenseNumber)
        {
            VehiclesInGarage foundVehicle;

            if (!m_VehiclesInGarage.TryGetValue(i_LicenseNumber, out foundVehicle))
            {
                throw new KeyNotFoundException("There is no such vehicle in the system!");
            }

            return foundVehicle;
        }

        public bool IsInGarageAndChangeToInRepairIfNecessary(string i_LicenseNumber)
        {
            bool inGarage = false;

            foreach (KeyValuePair<string, VehiclesInGarage> vehicle in m_VehiclesInGarage)
            {
                if (vehicle.Key == i_LicenseNumber)
                {
                    inGarage = true;
                    vehicle.Value.VehicleStatus = eVehicleStatuses.InRepair;
                    break;
                }
            }

            return inGarage;
        }

        public void AddNewVehicleToGarage(VehiclesInGarage i_NewVehicleToGarage) 
        {
            VehiclesInGarage existVehicleInGarage;

            if (m_VehiclesInGarage.TryGetValue(i_NewVehicleToGarage.VehicleInfo.LicenseNumber, out existVehicleInGarage))
            {
                existVehicleInGarage.VehicleStatus = eVehicleStatuses.InRepair;
                throw new Exception(string.Format(
                                                  "The vehicle is already in garage!{0}The vehicle status has changed to 'In repair'",
                                                  Environment.NewLine));
            }
            else
            {
                m_VehiclesInGarage.Add(i_NewVehicleToGarage.VehicleInfo.LicenseNumber, i_NewVehicleToGarage);
            }
        }

        public void ChangeVehicleStatus(string i_VehicleLicenseNumber, eVehicleStatuses i_UpdatedStatus)
        {
            VehiclesInGarage vehicleToUpdate;

            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out vehicleToUpdate))
            {
                if (vehicleToUpdate.VehicleStatus == i_UpdatedStatus)
                {
                    throw new ArgumentException("The vehicle is already in that status!");
                }
                else
                {
                    vehicleToUpdate.VehicleStatus = i_UpdatedStatus;
                }
            }
            else
            {
                throw new KeyNotFoundException("There is no such vehicle in the system!");
            }
        }

        public void InflateTiresToMax(string i_VehicleLicenseNumber)
        {
            VehiclesInGarage vehicleToUpdate;

            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out vehicleToUpdate))
            {
                foreach (Tire tire in vehicleToUpdate.VehicleInfo.Tires)
                {
                    if(tire.CurrentPSI == tire.MaxPSI)
                    {
                        throw new ArgumentException("The tires PSI are already maximum!");
                    }
                    else
                    {
                        tire.CurrentPSI = tire.MaxPSI;
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException("There is no such vehicle in the system!");
            }
        }

        public void ProvideSourceEnergyToVehicle(string i_VehicleLicenseNumber, float i_AmountToAdd, eFuelType? i_FuelType)
        {
            VehiclesInGarage vehicleToUpdate;
            FuelSystem fuelSourceEnergyTypeSystem;
            BatterySystem batterySourceEnergyTypeSystem;

            if (i_AmountToAdd <= 0)
            {
                throw new ValueOutOfRangeException("You must provide energy with possitive number!");
            }
            else
            {
                if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out vehicleToUpdate))
                {
                    if (i_FuelType != null)
                    {
                        fuelSourceEnergyTypeSystem = vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem as FuelSystem;
                        if (fuelSourceEnergyTypeSystem != null)
                        {
                            if (fuelSourceEnergyTypeSystem.FuelType == i_FuelType)
                            {
                                fuelSourceEnergyTypeSystem.ProvideSourceEnergy(i_AmountToAdd, (eFuelType)i_FuelType);
                                vehicleToUpdate.VehicleInfo.UpdateEnergyLeftInPrecents();
                            }
                            else
                            {
                                throw new ArgumentException(
                                string.Format(
@"You tried to refuel with different type fuel of that vehicle!
The vehicle type fuel is: {0}",
                                fuelSourceEnergyTypeSystem.FuelType));
                            }
                        }
                        else
                        {
                            throw new ArgumentException("You tried to refuel a fuel vehicle with electricity!");
                        }
                    }
                    else
                    {
                        batterySourceEnergyTypeSystem = vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem as BatterySystem;
                        if (batterySourceEnergyTypeSystem != null)
                        {
                            batterySourceEnergyTypeSystem.ProvideSourceEnergy(i_AmountToAdd / 60);
                            vehicleToUpdate.VehicleInfo.EnergyLeftInPrecents = batterySourceEnergyTypeSystem.CurrEnergy / batterySourceEnergyTypeSystem.MaxEnergyPossible;
                        }
                        else
                        {
                            throw new ArgumentException("You tried to charge an elctric vehicle with fuel!");
                        }
                    }
                }
                else
                {
                    throw new KeyNotFoundException("There is no such vehicle in the system!");
                }
            }
        }
    }
}
