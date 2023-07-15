using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Assignment2_Petrol_Skeleton
{
    public class Display
    {



        public static void DrawVehicles()
        {
            Vehicle v;

            Console.WriteLine("Vehicles Queue:");

            for (int i = 0; i < Data.vehicles.Count; i++)
            {
                v = Data.vehicles[i];
                Console.Write("#{0} Fuel Type: {1}-Vehicle Type: {2}- Default Fuel: {3}| ", v.carID, v.fuelType, v.vehicleType, v.fuelDefault);

            }
        }


        public static void DrawPumps()
        {
            Pump p;

            Console.WriteLine("Pumps Status:");

            for (int i = 0; i < 9; i++)
            {
                p = Data.pumps[i];

                Console.Write("#{0} ", i + 1);
                if (p.IsAvailable()) { Console.Write("FREE"); }
                else { Console.Write("BUSY"); }
                Console.Write(" | ");

                // modulus -> remainder of a division operation
                // 0 % 3 => 0 (0 / 3 = 0 R=0)
                // 1 % 3 => 1 (1 / 3 = 0 R=1)
                // 2 % 3 => 2 (2 / 3 = 0 R=2)
                // 3 % 3 => 0 (3 / 3 = 1 R=0)
                // 4 % 3 => 1 (4 / 3 = 1 R=1)
                // 5 % 3 => 2 (5 / 3 = 1 R=2)
                // 6 % 3 => 0 (6 / 3 = 2 R=0)
                // ...
                if (i % 3 == 2) { Console.WriteLine(); }
            }

        }
        public static void DrawCounters()
        {
            int VehiclesReleased = Data.counter.VehiclesReleased;


            Console.WriteLine("Vehicles left: {0}", Data.vehiclesLeft.Count);
            Console.WriteLine("Vehicles Released: " + VehiclesReleased);
            Console.WriteLine("Litres Dispensed: " + LitresDispensed());
            Console.WriteLine("The price of all fuels/L is: 1.14 --> Total Income: £" + Income());
            Console.WriteLine("Total comission obtained: £" + Comission());


        }
        /*As above, all counters have to be shown, but with the addition of two extra
		counters totalling the number of litres dispensed for the other two fuel types
		(Diesel and LPG)*/

        public static float LitresDispensed()
        {
            float litres = 1.5f;
            float totaldispensed;


            float times = Data.releasedVehicles.Sum(v => Convert.ToSingle(v.fuelTime));
            totaldispensed = litres * (times / 1000);

            return totaldispensed;
        }

        public static void FuelsDispensed()
        {
            Vehicle v;
            float dieselLitres = 0;
            float unleadedLitres = 0;
            float lpgLitres = 0;

            for (int i = 0; i < Data.releasedVehicles.Count; i++)
            {
                v = Data.releasedVehicles[i];
                float litres = (Convert.ToSingle(Data.releasedVehicles[i].fuelTime) / 1000) * 1.5f;
                switch (v.fuelType)
                {
                    case "Diesel":

                        dieselLitres = dieselLitres + litres;

                        break;
                    case "Unleaded":

                        unleadedLitres = unleadedLitres + litres;
                        break;
                    case "LPG":
                        lpgLitres = lpgLitres + litres;
                        break;

                }
            }
            Console.WriteLine("{0}L Diesel", dieselLitres);
            Console.WriteLine("{0}L Unleaded", unleadedLitres);
            Console.WriteLine("{0}L LPG", lpgLitres);







        }



        public static float Income()
        {
            float totaldispensed = LitresDispensed();
            float price = 1.14f;
            float income;

            income = totaldispensed * price;

            return income;

        }
        public static float Comission()
        {
            float totalincome = Income();
            float comission;

            comission = totalincome * 0.1f;

            return comission;
        }

        public static void DrawReleasedVehicles()
        {
            Vehicle v;
            Pump p;

            Console.WriteLine("Released Vehicles: ");

            for (int i = 0; i < Data.releasedVehicles.Count; i++)
            {
                v = Data.releasedVehicles[i];
                p = Data.releasedVehiclesPumps[i];
                Console.WriteLine("Fuel Charged: {0}L {1} Income: £{2} Pump: #{3} Vehicle Type: {4} | ", (float)Math.Round(((v.fuelTime / 1000) * 1.5f), 2), v.fuelType, (float)Math.Round((((v.fuelTime / 1000) * 1.5f) * 1.14f), 2), p.id + 1, v.vehicleType);
            }
        }
    }
}





