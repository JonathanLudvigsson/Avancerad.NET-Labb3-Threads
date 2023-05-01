using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Threads
{
    internal class Car
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Speed { get; set; }
        public double DistanceDriven { get; set; }
        public int Position { get; set; }
        public int myFinishPlace { get; set; }

        private static int CursorPos = 6;

        private static int globalFinishPlace = 0;

        public Car(int id, string name)
        {
            ID = id;
            Name = name;
            Speed = 0D; // Meters per second
            DistanceDriven = 0D; // Meters
            Position = 0; // Position in race
        }

        public static Car c1 = new Car(1, "TurboRejser");
        public static Car c2 = new Car(2, "OmegaDriver");

        public static List<Car> racers = new List<Car>()
        {
            c1,
            c2
        };

        //public static List<Car> finishedRace = new List<Car>();

        public void RandomErrors()
        {
            Random rand = new Random();
            while (DistanceDriven < 10000D)
            {
                int randomNumber = rand.Next(1, 101);
                for (int i = 0; i < 30; i++)
                {
                    Thread.Sleep(1000);
                    if (DistanceDriven >= 10000)
                    {
                        return;
                    }
                }
                
                if (randomNumber <= 2)
                {
                    OutOfGas();
                }
                else if (randomNumber <= 4)
                {
                    PuncturedTire();
                }
                else if (randomNumber <= 10)
                {
                    BirdHitsWindshield();
                }
                else if (randomNumber <= 20)
                {
                    EngineFault();
                }
            }
        }

        public void UpdateDistanceDriven()
        {
            while (DistanceDriven < 10000D)
            {
                DistanceDriven += Speed;
                if (DistanceDriven > 10000d)
                {
                    DistanceDriven = 10000d;
                }
                UpdatePositions();
                Thread.Sleep(1000);
            }
            //finishedRace.Add(this);
            myFinishPlace = globalFinishPlace;
            globalFinishPlace++;
            Console.SetCursorPosition(0, CursorPos++);
            Console.WriteLine($"{Name} has finished the race at place #{myFinishPlace + 1}!");
            if (myFinishPlace == 0)
            {
                Console.SetCursorPosition(0, CursorPos++);
                Console.WriteLine($"{Name} has won the race!");
            }
        }

        public static void UpdatePositions()
        {
            int i = 1;
            foreach (Car c in racers.OrderByDescending(x => x.DistanceDriven))
            {
                c.Position = i;
                i++;
            }
        }

        public static void WritePosition()
        {
            while (racers.Min(x => x.DistanceDriven) < 10000d)
            {
                Thread.Sleep(1000);

                //if (Console.ReadLine() == "")
                //{
                //Thread.Sleep(1000);
                //Console.SetCursorPosition(0, 3);
                //Console.Write($"{c1.Name} has driven {Math.Round(c1.DistanceDriven),0}/10000 meters at the speed {Math.Round(c1.Speed * 60 * 60 / 1000),0}km/h");
                //Console.SetCursorPosition(0, 4);
                //Console.Write($"{c2.Name} is in position {Math.Round(c2.DistanceDriven),0}/10000 meters at the speed {Math.Round(c2.Speed * 60 * 60 / 1000),0}km/h");
                //Console.SetCursorPosition(0, CursorPos);
                //}

                //for (int i = 0; i < racers.Count; i++)
                //{
                //    Console.SetCursorPosition(0, 3 + i);
                //    Console.WriteLine($"{racers[i].Name} has driven {Math.Round(racers[i].DistanceDriven),0}/10000 meters at the speed {Math.Round(racers[i].Speed * 60 * 60 / 1000),0}km/h and is in place #{racers[i].Position}");
                //}

                int i = 0;

                foreach (Car c in racers.OrderByDescending(x => x.DistanceDriven))
                {
                    Console.SetCursorPosition(0, 3 + i);
                    Console.WriteLine($"{c.Name} has driven {Math.Round(c.DistanceDriven),0}/10000 meters at the speed {Math.Round(c.Speed * 60 * 60 / 1000),0}km/h and is in place #{c.Position}" + new String(' ', Console.BufferWidth));
                    Console.SetCursorPosition(0, CursorPos);
                    i++;
                }
            }
        }

        public void StartDriving()
        {
            DistanceDriven = 0;
            Speed = ConvertKphToMps(120);
            Console.WriteLine($"{Name} has started driving!");
            UpdateDistanceDriven();
        }

        public void StopDriving(int timeToStop)
        {
            double oldSpeed = Speed;
            Speed = 0D;
            Thread.Sleep(timeToStop);
            Speed = oldSpeed;
        }

        public void OutOfGas()
        {
            Console.SetCursorPosition(0, CursorPos++);
            Console.WriteLine($"{Name} has ran out of gas and needs to refill!");
            StopDriving(30000);
        }

        public void PuncturedTire()
        {
            Console.SetCursorPosition(0, CursorPos++);
            Console.WriteLine($"{Name}'s tire has been punctured!");
            StopDriving(20000);
        }

        public void BirdHitsWindshield()
        {
            Console.SetCursorPosition(0, CursorPos++);
            Console.WriteLine($"A bird has hit {Name}'s windshield!");
            StopDriving(10000);
        }

        public void EngineFault()
        {
            Console.SetCursorPosition(0, CursorPos++);
            Console.WriteLine($"{Name}'s engine has broken and his speed has dropped!");
            Speed -= ConvertKphToMps(1);
        }

        // Converts Kilometers per hour to Meters per second.
        public double ConvertKphToMps(double kph)
        {
            return kph * 1000 / 60 / 60;
        }

    }
}
