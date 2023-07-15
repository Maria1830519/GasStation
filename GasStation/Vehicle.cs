using System;

namespace Assignment2_Petrol_Skeleton
{
    class Vehicle
    {
        //Unleaded, diesel LPG
        public string fuelType;
        public double fuelTime;
        public string vehicleType;
        public double fuelDefault;
        public static int nextCarID = 0;
        //TYPE OF CAR: Car, Vanm HGV
        public int carID;


        public Vehicle(string ftp, double ftm, string vtp, double fdef)
        {
            fuelType = ftp;
            fuelTime = ftm;
            vehicleType = vtp;
            fuelDefault = fdef;
            carID = nextCarID++;
        }


    }
}
