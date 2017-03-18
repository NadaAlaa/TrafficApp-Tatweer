using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatweerTechnicalAssignment
{

    class Vehicle
    {
        public enum VehicleType {Car, Truck };
        public static double SpeedThreshold = 145;

        public VehicleType Type { get; set; }
        public double Speed { get; set; }
        public DateTime TimeStamp { get; set; }
        public string TrackerID { get; set; }
        public double Location { get; set; }

        public Vehicle() { }
    }
}
