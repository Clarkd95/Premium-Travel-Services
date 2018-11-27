using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectIteration3
{
    public enum TransportType
    {
        PrivateJet,
        Helicopter,
        Yacht,
        Limousine
    }

    public class Package
    {
        public decimal _price;
        public TimeSpan _hoursOfTravelTime;
        public string _origin;
        public string _destination;
        public TransportType _transport;

        public Package(decimal price, TimeSpan hours, string origin, string destination, TransportType transport)
        {
            _price = price;
            _hoursOfTravelTime = hours;
            _origin = origin;
            _destination = destination;
            _transport = transport;
        }

        public override string ToString()
        {
            return $"{_transport} from {_origin} to {_destination}";
        }
    }

    public class Reservation
    {

        public DateTime _departureDateTime;
        public DateTime _arrivalDateTime;
        public Package _package;

        public Reservation(DateTime departure, Package package)
        {
            _departureDateTime = departure;
            _arrivalDateTime = departure + package._hoursOfTravelTime;
            _package = package;
        }

        public override string ToString()
        {
            return _package.ToString() + $", departing {_departureDateTime.ToString()}, arriving {_arrivalDateTime.ToString()}.";
            
        }
    }

    class PackageList
    {
        private static readonly object SyncLock = new object();
        private static volatile PackageList _packageList;
        private IReadOnlyList<Package> _startupPackageList { get; set; }

        private PackageList()
        {

            Package p1 = new Package(4000.00m, new TimeSpan(1,0,0), "Casa Pickles","Los Angeles Airport",TransportType.Helicopter);
            Package p2 = new Package(17000.00m, new TimeSpan(10, 0, 0), "Los Angeles Airport", "Fiji Airport", TransportType.PrivateJet);
            Package p3 = new Package(500.00m, new TimeSpan(1, 0, 0), "Fiji Airport", "Fiji Marina", TransportType.Limousine);
            Package p4 = new Package(12000.00m, new TimeSpan(10, 0, 0), "Fiji Marina", "Paradise Pickles Private Island", TransportType.Yacht);
            Package p5 = new Package(12000.00m, new TimeSpan(8, 0, 0), "Paradise Pickles Private Island", "Fiji Marina", TransportType.Yacht);
            Package p6 = new Package(500.00m, new TimeSpan(1, 0, 0), "Fiji Marina", "Fiji Airport", TransportType.Limousine);
            Package p7 = new Package(23000.00m, new TimeSpan(8, 0, 0), "Fiji Airport", "San Diego Airport", TransportType.PrivateJet);
            Package p8 = new Package(5000.00m, new TimeSpan(1, 0, 0), "San Diego Airport", "Palace Pickles", TransportType.Helicopter);



            _startupPackageList = new List<Package>
            {
                p1,
                p2,
                p3,
                p4,
                p5,
                p6,
                p7,
                p8
            };
        }

        public static IReadOnlyList<Package> GetStartupPackageList()
        {
            //Double Checked, Thread Safe Singleton returns
            if (_packageList == null)
            {
                lock (SyncLock)
                {
                    if (_packageList == null) { _packageList = new PackageList(); }
                }
            }
            return _packageList._startupPackageList;
        }
    }
}
