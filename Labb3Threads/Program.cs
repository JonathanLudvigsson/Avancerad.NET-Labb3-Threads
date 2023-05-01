using System.Xml.Linq;

namespace Labb3Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car c1 = Car.c1;
            Car c2 = Car.c2;

            Thread RaceHandler1 = new Thread(() =>
            {
                c1.StartDriving();
            });

            Thread RaceHandler2 = new Thread(() =>
            {
                c2.StartDriving();
            });

            Thread RandomErrorHandler1 = new Thread(() =>
            {
                c1.RandomErrors();
            });

            Thread RandomErrorHandler2 = new Thread(() =>
            {
                c2.RandomErrors();
            });

            Thread GetRaceStatus = new Thread(() =>
            {
                Car.WritePosition();
            });

            //Parallel.Invoke(() =>
            //{

            //    RaceHandler1.Start();
            //    RandomErrorHandler1.Start();
            //}, () =>
            //{
            //    RaceHandler2.Start();
            //    RandomErrorHandler2.Start();
            //});

            for (int i = 3; i >= 1; i--)
            {
                Console.Write("\rRace begins in : " + i);
                Thread.Sleep(1000);
            }
            Console.Clear();

            RaceHandler1.Start();
            RandomErrorHandler1.Start();
            RaceHandler2.Start();
            RandomErrorHandler2.Start();
            GetRaceStatus.Start();

        }
    }
}