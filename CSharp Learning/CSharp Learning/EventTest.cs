using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Learning
{
    class CarEventArgs : EventArgs
    {
        public readonly string msg;
        public CarEventArgs(string _msg)
        {
            msg = _msg;
        }
    }

    class Car
    {
        private float velocity_ = 0;
        public event EventHandler<CarEventArgs> overSpeedHandler_;

        public delegate void EngineDownHandler(string msg);
        public event EngineDownHandler engineDownHandler_;

        public void Run()
        {
            for (int i = 1; i < 6; i++)
            {
                velocity_ = i * 20;
                if(velocity_ >= 100)
                {
                    if (engineDownHandler_ != null)
                    {
                        engineDownHandler_("Engine Down!!!");
                    }
                }
                else if (velocity_ >= 80)
                {
                    if (overSpeedHandler_ != null)
                        overSpeedHandler_(this,new CarEventArgs("Over Speed!!!"));
                }
            }
        }
    }

    class EventTest
    {
        public static void Run()
        {
            Car car = new Car();
            //car.overSpeedHandler_ += delegate(Object obj, CarEventArgs args) { Console.WriteLine("{0}", args.msg); };
            //car.engineDownHandler_ += delegate(string msg) { Console.WriteLine("{0}", msg); };
            car.overSpeedHandler_ += new EventHandler<CarEventArgs>(OnCarOverSpeed);
            car.engineDownHandler_ += new Car.EngineDownHandler(OnEngineDown);
            car.Run();
        }

        public static void OnCarOverSpeed(Object obj, CarEventArgs args)
        {
            Console.WriteLine("{0}", args.msg); 
        }

        public static void OnEngineDown(string msg)
        {
            Console.WriteLine("{0}", msg); 
        }
    }
}
