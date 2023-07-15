using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2_Petrol_Skeleton
{
    class Counter
    {

        private int _VehiclesReleased = 0;



        public int VehiclesReleased
        {
            get
            {
                return this._VehiclesReleased;
            }
            set
            {
                this._VehiclesReleased = value;

            }
        }


    }
}
