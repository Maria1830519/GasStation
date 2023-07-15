using System;
using System.Collections.Generic;
using System.Timers;

namespace Assignment2_Petrol_Skeleton
{
    class Pump
    {

        public Vehicle currentVehicle = null;
        public string fuelType;
        public int id;


        public Pump(string ftp, int i)
        {
            fuelType = ftp;
            id = i;
        }

        public bool IsAvailable()
        {
            // returns TRUE if currentVehicle is NULL, meaning available
            // returns FALSE if currentVehicle is NOT NULL, meaning busy
            return currentVehicle == null;
        }

        public void AssignVehicle(Vehicle v)
        {
            currentVehicle = v;

            Timer timer = new Timer();
            timer.Interval = v.fuelTime;
            timer.AutoReset = false; // don't repeat
            timer.Elapsed += ReleaseVehicle;
            timer.Enabled = true;
            timer.Start();
        }

        public void ReleaseVehicle(object sender, ElapsedEventArgs e)
        {
            Data.releasedVehicles.Add(currentVehicle);
            Data.releasedVehiclesPumps.Add(this);
            currentVehicle = null;

            // record transaction
            Data.counter.VehiclesReleased++;


        }
    }
}
