using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;

namespace ConsoleUI
{

    public class GarageSystemConsoleUI
    {
        private const int k_NumTypesOfVehicles = 5;
        private const int k_ExitOption = 8;


        // $G$ NTT-999 (-3) This kind of field should be readonly.
        private GarageSystem m_GarageSystem;

        public GarageSystemConsoleUI() // c'tor
        {
            m_GarageSystem = new GarageSystem();
        }

        public void Menu()
        {
            int option;

            Console.WriteLine(
@"Hello!
Welcome to the garage system!");
            do
            {
                Console.WriteLine(
@"What would you like to do?
1. Insert a new car to garage
2. Present all of the vehicles license numbers in garage
3. Update status to vehicle in garage
4. Inflate vehicle tires to max
5. Refuel vehicle
6. Recharge vehicle
7. Print full details of specific vehicle in garage
8. Exit");
                option = GetValidInputs.GetValidInputNumber(1, 8);
                Console.Clear();
                try
                {
                    // $G$ CSS-018 (-3) You should have used enumerations here.

                    switch (option)
                    {
                        case 1:
                            {
                                AddNewVehicleToGarage();
                                break;
                            }

                        case 2:
                            {
                                PresentVehiclesInGarageLicenseNumbersToConsoleByStatus();
                                break;
                            }

                        case 3:
                            {
                                ChangeVehicleStatus();
                                break;
                            }

                        case 4:
                            {
                                InflateTires();
                                break;
                            }

                        case 5:
                            {
                                Refuel();
                                break;
                            }

                        case 6:
                            {
                                Recharge();
                                break;
                            }

                        case 7:
                            {
                                PrintAllDetailsToConsole();
                                break;
                            }

                        default:
                            {
                                Console.WriteLine("Bye-bye!");
                                Environment.Exit(0);
                                break;
                            }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("----------------Returning to menu----------------");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            while (option != k_ExitOption);
        }

        public void PrintAllDetailsToConsole()
        {
            string licenseNumber;
            VehiclesInGarage vehicleToPresent;
            
            getLicenseNumber(out licenseNumber);
            try
            {
                vehicleToPresent = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                Console.WriteLine(vehicleToPresent.ToString());
                Console.WriteLine(vehicleToPresent.VehicleInfo.ToString());
            }
            catch (KeyNotFoundException i_KeyNotFoundException)
            {
                Console.WriteLine(i_KeyNotFoundException.Message);
            }

            Console.WriteLine("----------------Returning to menu----------------");
        }
     
        public void Recharge()
        {
            string licenseNumber;
            bool isValid;
            float minutesAmountToAdd, maxAmount, maxPossibleMinutesToCharge;
            VehiclesInGarage vehicleToCharge;

            do
            {
                isValid = true;
                try
                {
                    getLicenseNumber(out licenseNumber);
                    vehicleToCharge = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                    if (vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem is BatterySystem)
                    {
                        maxAmount = vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem.MaxEnergyPossible;
                        maxPossibleMinutesToCharge = (maxAmount - vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem.CurrEnergy) * 60;
                        if (maxPossibleMinutesToCharge != 0)
                        {
                            Console.WriteLine(
@"You can recharge up to {0:0.0} minutes.
Please insert how many minutes you would like to recharge:",
                        maxPossibleMinutesToCharge);
                            minutesAmountToAdd = GetValidInputs.GetValidInputNumber(0, maxPossibleMinutesToCharge);
                            m_GarageSystem.ProvideSourceEnergyToVehicle(licenseNumber, minutesAmountToAdd, null);
                        }
                        else
                        {
                            throw new Exception("Your gauge is full!");
                        }
                    }
                    else
                    {
                        throw new FormatException("You tried to refuel a fuel vehicle with electricity");
                    }
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    isValid = false;
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message);
                    isValid = false;
                }
            }
            while (!isValid);

            Console.WriteLine("You recharge successfully!{0}----------------Returning to menu----------------", Environment.NewLine);
        }


        public void Refuel()
        {
            string licenseNumber;
            float fuelAmountToAdd, maxAmount, maxPossibleFuelToRefuel;
            bool isValid;
            eFuelType fuelType;
            VehiclesInGarage vehicleToRefuel;
            do
            {
                isValid = true;
                try
                {
                    getLicenseNumber(out licenseNumber);
                    vehicleToRefuel = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                    if (vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem is FuelSystem)
                    {
                        fuelType = getFuelType();
                        maxAmount = vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem.MaxEnergyPossible;
                        maxPossibleFuelToRefuel = maxAmount - vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem.CurrEnergy;
                        if (maxPossibleFuelToRefuel != 0)
                        {
                            Console.WriteLine(
  @"You can refuel up to {0:0.0} liters.
Please insert how many liters of fuel you would like to refuel:",
                          maxPossibleFuelToRefuel);
                            fuelAmountToAdd = GetValidInputs.GetValidInputNumber(0, maxPossibleFuelToRefuel);
                            m_GarageSystem.ProvideSourceEnergyToVehicle(licenseNumber, fuelAmountToAdd, fuelType);
                        }
                        else
                        {
                            throw new Exception("Your gauge is full!");
                        }
                    }
                    else
                    {
                        throw new FormatException("You tried to charge an elctric vehicle with fuel!");
                    }
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    isValid = false;
                }
                catch (ArgumentException i_ArgumentException)
                {
                    Console.WriteLine(i_ArgumentException.Message);
                    isValid = false;
                }
            }
            while (!isValid);

            Console.WriteLine("You refuel successfully!{0}----------------Returning to menu----------------", Environment.NewLine);
        }

        public void InflateTires()
        {
            string licenseNumber;

            getLicenseNumber(out licenseNumber);
            m_GarageSystem.InflateTiresToMax(licenseNumber);
            Console.WriteLine("The tires were inflated successfully!");
            Console.WriteLine("----------------Returning to menu----------------");
            System.Threading.Thread.Sleep(1000);
        }

        public void ChangeVehicleStatus()
        {
            int option;
            string licenseNumber;

            getLicenseNumber(out licenseNumber);
            Console.WriteLine(
@"Which of the following statuses you'd like to set the vehicle to:
{0}. In repair 
{1}. Repaired 
{2}. Paid ",
            (int)eVehicleStatuses.InRepair,
            (int)eVehicleStatuses.Repaired,
            (int)eVehicleStatuses.Paid);
            option = GetValidInputs.GetValidInputNumber(1, 3);
            try
            {
                m_GarageSystem.ChangeVehicleStatus(licenseNumber, (eVehicleStatuses)option);
                Console.WriteLine("The vehicle status has changed successfully!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("----------------Returning to menu----------------");
            System.Threading.Thread.Sleep(1000);
        }

        public void PresentVehiclesInGarageLicenseNumbersToConsoleByStatus()
        {
            int counter = 0, option;

            Console.WriteLine(
@"Which status filter would you like to present?
1. In repair status
2. Repaired status
3. Paid status
4. No filter - Present all");
            option = GetValidInputs.GetValidInputNumber(1, 4);
            Console.WriteLine("----------------The vehicles are:----------------");
            foreach (VehiclesInGarage vehicle in m_GarageSystem.VehiclesInTheGarage.Values)
            {
                if (option == 4 || vehicle.VehicleStatus == (eVehicleStatuses)option)
                {
                    Console.WriteLine(vehicle.VehicleInfo.LicenseNumber);
                    counter++;
                }
            }

            if(counter == 0)
            {
                Console.WriteLine("There are no vehicles in that status right now!");
            }

            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("---------------Returning to menu-----------------");
            System.Threading.Thread.Sleep(500);
        }

        public void AddNewVehicleToGarage()
        {
            string ownersName = getOwnersName();
            Console.WriteLine("Please insert vehicle's owner's phone number: ");
            string ownersPhoneNumber = GetValidInputs.GetValidPhoneNumber();
            Vehicle vehicleToAdd = getNewVehicleInfo();
            VehiclesInGarage newVehicle = new VehiclesInGarage(ownersName, ownersPhoneNumber, vehicleToAdd);
            m_GarageSystem.AddNewVehicleToGarage(newVehicle);
            Console.WriteLine("----------------Adding new vehicle to the system----------------");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("----------------The vehicle was added successfully!-------------");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }

        private string getOwnersName()
        {
            string firstName, lastName;
            StringBuilder ownersName = new StringBuilder();

            Console.WriteLine("Please insert the vehicle's owner's first name:");
            firstName = GetValidInputs.GetValidStringOnlyLetters(VehiclesInGarage.k_MinCharactersForOwnersName, VehiclesInGarage.k_MaxCharactersForOwnersName);
            Console.WriteLine("Please insert the vehicle's owner's last name:");
            lastName = GetValidInputs.GetValidStringOnlyLetters(VehiclesInGarage.k_MinCharactersForOwnersName, VehiclesInGarage.k_MaxCharactersForOwnersName);
            ownersName.Append(firstName);
            ownersName.Append(" ");
            ownersName.Append(lastName);

            return ownersName.ToString();
        }

        private Vehicle getNewVehicleInfo()
        {
            Vehicle newVehicle;
            eTypeOfVehicle typeOfVehicle;
            string licenseNumber;

            getVehiliceType(out typeOfVehicle);
            getLicenseNumber(out licenseNumber);
            if (m_GarageSystem.IsInGarageAndChangeToInRepairIfNecessary(licenseNumber))
            {
                throw new Exception(string.Format(
                                                  "The vehicle is already in garage!{0}The vehicle status has changed to 'In repair'",
                                                  Environment.NewLine));
            }

            newVehicle = CreateVehicles.CreateVehicle(typeOfVehicle, licenseNumber);
            Console.WriteLine("Please enter the following details: ");
            printMessageAndGetValuesForAllTypeVehicle(newVehicle);
            printMessageAndGetValuesForSpecificTypeVehicle(newVehicle);

            return newVehicle;
        }

        private void printMessageAndGetValuesForAllTypeVehicle(Vehicle i_NewVehicle)
        {
            string input;
            bool isValid;
            Dictionary<int, string> vehicleParams = i_NewVehicle.GetVehicleParams();

            foreach (KeyValuePair<int, string> param in vehicleParams)
            {
                do
                {
                    isValid = true;
                    Console.WriteLine(param.Value);
                    input = Console.ReadLine();
                    try
                    {
                        i_NewVehicle.SetVehicleParameters(param.Key, input);
                    }
                    catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                    {
                        Console.WriteLine(i_ValueOutOfRangeException.Message);
                        isValid = false;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine(i_ArgumentException.Message);
                        isValid = false;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine(i_FormatException.Message);
                        isValid = false;
                    }
                }
                while (!isValid);
            }
        }

        private void printMessageAndGetValuesForSpecificTypeVehicle(Vehicle i_NewVehicle)
        {
            string input;
            bool isValid;
            Dictionary<int, string> specificTypeParams = i_NewVehicle.GetParams();

            foreach (KeyValuePair<int, string> param in specificTypeParams)
            {
                do
                {
                    isValid = true;
                    Console.WriteLine(param.Value);
                    input = Console.ReadLine();
                    try
                    {
                        i_NewVehicle.SetSpecificTypeParams(param.Key, input);
                    }
                    catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                    {
                        Console.WriteLine(i_ValueOutOfRangeException.Message);
                        isValid = false;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine(i_ArgumentException.Message);
                        isValid = false;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine(i_FormatException.Message);
                        isValid = false;
                    }
                }
                while (!isValid);
            }
        }

        private eFuelType getFuelType()
        {
            Console.WriteLine(
@"Please insert vehicle's type fuel:
{0}.Octan98
{1}.Octan96
{2}.Octan95
{3}.Soler",
            (int)eFuelType.Octan98,
            (int)eFuelType.Octan96,
            (int)eFuelType.Octan95,
            (int)eFuelType.Soler);

            return (eFuelType)GetValidInputs.GetValidInputNumber(1, FuelSystem.k_NumOfFuelTypes);
        }

        // $G$ DSN-002 (-10) The UI should not know Car\Truck\Motorcycle
        private void getVehiliceType(out eTypeOfVehicle o_TypeOfVehicle)
        {
            Console.WriteLine(
@"Please insert vehicle's type:
{0}.Regular motorcycle
{1}.Electric motorcycle
{2}.Regular car
{3}.Electric car
{4}.Truck",
            (int)eTypeOfVehicle.RegularMotorcycle,
            (int)eTypeOfVehicle.ElectricMotorcycle,
            (int)eTypeOfVehicle.RegularCar,
            (int)eTypeOfVehicle.ElectricCar,
            (int)eTypeOfVehicle.Truck);
            o_TypeOfVehicle = (eTypeOfVehicle)GetValidInputs.GetValidInputNumber(1, k_NumTypesOfVehicles);
        }

        private void getLicenseNumber(out string o_LicenseNumber)
        {
            Console.WriteLine("Please insert the vehicle's license number:");
            o_LicenseNumber = GetValidInputs.GetValidLicenseNumber();
        }
    }
}