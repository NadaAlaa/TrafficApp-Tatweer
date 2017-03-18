using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TatweerTechnicalAssignment
{
    class Tracker
    {
        private Queue<Vehicle> Vehicles { get; set; }
        private double MaxSpeed { get; set; }
        public string ID { get; set; }
        public int RelativeOrder { get; set; }

        public Tracker() { }
        public Tracker(string ID, int RelativeOrder)
        {
            this.ID = ID;
            this.RelativeOrder = RelativeOrder;
            Vehicles = new Queue<Vehicle>();
        }
        public int GetTrafficDensity()
        {
            return Vehicles.Count;
        }
        public double GetMaximumSpeed()
        {
            return Vehicles.Max(p=> p.Speed);
        }
        public void UpdateTracker(Vehicle Vehicle_)
        {
            Vehicles.Enqueue(Vehicle_);
            while((Vehicle_.TimeStamp - Vehicles.Peek().TimeStamp).TotalMinutes >= 1)
            {
                Vehicles.Dequeue();
            }
        }
        public string GetTrackerInfo()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
