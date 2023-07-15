using System;
using System.Timers;

namespace Assignment2_Petrol_Skeleton
{
    class Program
    {

        static void Main(string[] args)

        {
            //Executes:
            Data.Initialise();
            //go to Data.cs>Initialise


            //This is the timer that says how much times goes between every loop
            Timer timer = new Timer();
            timer.Interval = 2050;
            timer.AutoReset = true; // repeat every 2 seconds
            timer.Elapsed += RunProgramLoop;
            timer.Enabled = true;
            timer.Start();

            Console.ReadLine();
        }

        static void RunProgramLoop(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            Display.DrawVehicles();
            Console.WriteLine();
            Console.WriteLine();
            Data.AssignVehicleToPump();
            Display.DrawPumps();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("                               INFO                                  ");
            Console.WriteLine("---------------------------------------------------------------------");
            Display.DrawCounters();
            Display.FuelsDispensed();
            Display.DrawReleasedVehicles();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");

        }
    }
}
