using System;
using System.Collections.Generic;
using System.Reflection;
using System.Timers;

namespace Assignment2_Petrol_Skeleton
{
    class Data
    {
        //Only one random number generator is needed
        //As it's a public static, it can be used by other parts of the app by calling it directly: Data.random.Next(1,3) etc.
        public static Random random = new Random();
        public static List<Vehicle> vehiclesLeft = new List<Vehicle>();
        public static List<Vehicle> vehicles;
        static string fuelType;
        public static List<Vehicle> releasedVehicles = new List<Vehicle>();
        public static List<Pump> pumps;
        public static List<Pump> releasedVehiclesPumps = new List<Pump>();
        public static Counter counter = new Counter();

        private static Timer timer;

        //Initialises
        public static void Initialise()
        {
            InitialisePumps();
            InitialiseVehicles();
            //go to CreateVehicle
        }


        private static void InitialiseVehicles()
        {
            vehicles = new List<Vehicle>();

            // https://msdn.microsoft.com/en-us/library/system.timers.timer(v=vs.71).aspx
            timer = new Timer();
            timer.Interval = random.Next(1500, 2200);
            timer.AutoReset = true; // keep repeating every 1.5 seconds
            timer.Elapsed += CreateVehicle;
            timer.Enabled = true;
            timer.Start();
        }
        //The timer creates a vehicle every 1.5s

        private static void CreateVehicle(object sender, ElapsedEventArgs e)
        {
            // queue limit
            // diesel, 18 second fuel time
            if (vehicles.Count < 5)
            {
                // Create the values
                string vehicleType = VehicleType();
                string fuelType = FuelType(vehicleType);
                float[] fuelDefault = FuelDefault(vehicleType); // returns an array of 2 values

                Vehicle v = new Vehicle(fuelType, fuelDefault[1], vehicleType, fuelDefault[0]);
                vehicles.Add(v);

                Timer leavingTimer = new Timer();
                leavingTimer.Interval = random.Next(1000, 2000);
                leavingTimer.AutoReset = false;
                leavingTimer.Elapsed += (senderEvent, eEvent) =>
                {
                    // If we can remove it from the vehicles list
                    if (vehicles.Remove(v))
                    {
                        // Then add it to the vehicles left list
                        vehiclesLeft.Add(v);

                        // Maybe also increment a counter here?

                    } // No else needed, nothing else needs to happem
                };
                leavingTimer.Enabled = true;
                leavingTimer.Start(); // Needs this to start it
            }

            // Update timer interval

        }

        public static string VehicleType()
        {
            string[] typesVehicles = { "Car", "Van", "HGV" };

            return typesVehicles[random.Next(typesVehicles.Length)];
        }

        public static string FuelType(string vehicleType)
        {

            switch (vehicleType)
            {
                case "HGV": 
                    fuelType = "Diesel";
                    break;

                case "Van":
                    string[] typesFuelVan = { "Diesel", "LPG" };
                    fuelType = typesFuelVan[random.Next(typesFuelVan.Length)];
                    break;

                case "Car":
                    string[] typesFuelCar = { "Unleaded", "Diesel", "LPG" };
                    fuelType = typesFuelCar[random.Next(typesFuelCar.Length)];
                    break;
            }

            return fuelType;
        }

        /// <summary>
        /// Here we record the initial fuel amount of each vehicle AND the time it will take to fuel.
        /// </summary>
        public static float[] FuelDefault(string vehicleType)
        {
            float[] defaultFuel = new float[2];

            float carDefault = 40;
            float vanDefault = 80;
            float hgvDefault = 150;
            float hundred = 100;
            float percentage = random.Next(0, 26);

            switch (vehicleType)
            {
                case "Car":
                    defaultFuel[0] = carDefault * (percentage / hundred);
                    defaultFuel[1] = ((carDefault - defaultFuel[0]) / 1.5f) * 1000;
                    break;

                case "Van":
                    defaultFuel[0] = vanDefault * (percentage / hundred);
                    defaultFuel[1] = ((vanDefault - defaultFuel[0]) / 1.5f) * 1000;
                    break;

                case "HGV":
                    defaultFuel[0] = hgvDefault * (percentage / hundred);
                    defaultFuel[1] = ((hgvDefault - defaultFuel[0]) / 1.5f) * 1000;
                    break;
            }

            return defaultFuel;
        }

        private static void InitialisePumps()
        {
            pumps = new List<Pump>();

            Pump p;
            string[] typesFuelCar = { "Unleaded", "Diesel", "LPG" };

            for (int i = 0; i < 9; i++)
            {
                p = new Pump("diesel", i);
                pumps.Add(p);
            }
        }


        public static void AssignVehicleToPump()
        {
            Vehicle v;
            Pump p;

            if (vehicles.Count == 0) { return; }

            for (int i = 0; i < 9; i++)
            {
                p = pumps[i];

                // note: needs more logic here, don't just assign to first
                // available pump, but check for the last available pump



                if (i == 0)
                {
                    if (!pumps[1].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (i == 1)
                {
                    if (!pumps[2].IsAvailable() && pumps[0].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (i == 2)
                {
                    if (pumps[1].IsAvailable() && pumps[0].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (i == 3)
                {
                    if (!pumps[4].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (i == 4)
                {
                    if (!pumps[5].IsAvailable() && pumps[3].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (i == 5)
                {
                    if (pumps[3].IsAvailable() && pumps[4].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (i == 6)
                {
                    if (!pumps[7].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (i == 7)
                {
                    if (!pumps[8].IsAvailable() && pumps[6].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (i == 8)
                {
                    if (pumps[7].IsAvailable() && pumps[6].IsAvailable() && p.IsAvailable())
                    {
                        v = vehicles[0]; // get first vehicle
                        vehicles.RemoveAt(0); // remove vehicles from queue
                        p.AssignVehicle(v); // assign it to the pump
                        break;
                    }
                }
                if (vehicles.Count == 0) { break; }

            }

        }

    }
}