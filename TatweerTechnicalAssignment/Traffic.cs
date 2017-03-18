using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TatweerTechnicalAssignment
{
    class Traffic
    {
        private static double NormSpeed { get; set; }
        private static int NormTrafficDensity { get; set; }
        private static List<Tracker> Trackers;

        public static void InitializeTrafficNorms(double NormSpeed_, int NormTrafficDensity_)
        {
            NormSpeed = NormSpeed_;
            NormTrafficDensity = NormTrafficDensity_;
            Trackers = new List<Tracker>();
        }
        public static void CheckForSpeed(Vehicle vehicle)
        {
            if (vehicle.Speed >= Vehicle.SpeedThreshold)
            {
                string AlertMessage = JsonConvert.SerializeObject(new
                {
                    Message = "High Speed Alert!",
                    Info = vehicle
                });
                Publish.AlertForSpeed(new string[] { AlertMessage });
            }
        }
        public static void UpdateTracker(Vehicle vehicle)
        {
            Trackers.First(p => p.ID == vehicle.TrackerID).UpdateTracker(vehicle);
        }
        public static void CheckForAccident()
        {
            List<Tracker> SuspiciousTrackers = new List<Tracker>();
            foreach(Tracker T in Trackers)
            {
                if (T.GetTrafficDensity() < NormTrafficDensity || T.GetMaximumSpeed() < NormSpeed)
                    SuspiciousTrackers.Add(T);
            }
            
            if (SuspiciousTrackers.Count == 0) return;
            
            var AlertMessage = JsonConvert.SerializeObject(new
            {
                Message = "Accident!",
                Info = SuspiciousTrackers
            });

            Publish.AlertForAccident(new string[] { AlertMessage });
        }
        public static string GetMessage(string[] args)
        {
            return ((args.Length > 0)
                   ? string.Join(" ", args)
                   : "info: Alert!");
        }
    }
}
