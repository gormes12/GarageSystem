using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public enum eTypeOfVehicle
    {
        RegularMotorcycle = 1,
        ElectricMotorcycle,
        RegularCar,
        ElectricCar,
        Truck
    }


    // $G$ CSS-999 (-3) Each enum\struct\class which is non nested should be in separate file

    public class CreateVehicles
    {
        public static Vehicle CreateVehicle(eTypeOfVehicle i_TypeOfVehicle, string i_LicenseNumber)
        {
            switch (i_TypeOfVehicle)
            {
                case eTypeOfVehicle.ElectricCar:
                    {
                        Tire[] electricCarTires = new Tire[4];

                        for (int i = 0; i < 4; i++)
                        {
                            electricCarTires[i] = new Tire(Car.k_MaxPsiInElectricCar);
                        }

                        return new Car(i_LicenseNumber, new BatterySystem(Car.k_MaxBatteryHoursInElectricCar), electricCarTires);
                    }

                case eTypeOfVehicle.ElectricMotorcycle:
                    {
                        Tire[] electricMotorocycleTires = new Tire[2];
                        for (int i = 0; i < 2; i++)
                        {
                            electricMotorocycleTires[i] = new Tire(Motorcycle.k_MaxPsiInElectricMotorcycle);
                        }

                        return new Motorcycle(i_LicenseNumber, new BatterySystem(Motorcycle.k_MaxBatteryHoursInElectricMotorcycle), electricMotorocycleTires);
                    }

                case eTypeOfVehicle.RegularCar:
                    {
                        Tire[] regularCarTires = new Tire[4];
                        for (int i = 0; i < 4; i++)
                        {
                            regularCarTires[i] = new Tire(Car.k_MaxPsiInFuelCar);
                        }

                        return new Car(i_LicenseNumber, new FuelSystem(Car.k_MaxLitersInFuelCar, eFuelType.Octan96), regularCarTires);
                    }

                case eTypeOfVehicle.RegularMotorcycle:
                    {
                        Tire[] RegularMotorcycleTires = new Tire[2];

                        for (int i = 0; i < 2; i++)
                        {
                            RegularMotorcycleTires[i] = new Tire(Motorcycle.k_MaxPsiInRegularMotorcycle);
                        }

                        return new Motorcycle(i_LicenseNumber, new FuelSystem(Motorcycle.k_MaxLitersInFuelMotorcycle, eFuelType.Octan95), RegularMotorcycleTires);
                    }

                default: // eTypeOfVehicle.Truck:
                    {
                        Tire[] TruckTires = new Tire[16];

                        for (int i = 0; i < 16; i++)
                        {
                            TruckTires[i] = new Tire(Truck.k_MaxPsiInTruck);
                        }

                        return new Truck(i_LicenseNumber, new FuelSystem(Truck.k_MaxLitersInFuelTruck, eFuelType.Soler), TruckTires);
                    }
            }
        }
    }
}
