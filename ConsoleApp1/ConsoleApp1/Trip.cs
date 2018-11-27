using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProjectIteration3
{
    //enum TripStateType
    //{
    //    AddTravelers,
    //    AddPackages,
    //    ChoosePayment,
    //    AcceptCash,
    //    AcceptCard,
    //    AcceptCheck,
    //    AddNote,
    //    Complete
    //}

    public class Trip
    {
        //variables
        
        public int _ID { get; private set; }
        public string _note;
        public TripStateType _stateType;
        public IState _state;
        public List<Reservation> _reservations;
        public List<Person> _travelers;
        public Payment Payment { get; set; }        
        public TravelAgent Agent { get; set; }
        //constructor
        public Trip(TravelAgent owner) //TravelAgent agent
        {
            Agent = owner;
            //will need to change this to something like GlobalTrips.Trips.Count(); 
            //tripNum++;
            Random random = new Random();
            _ID = random.Next(1000,10000);//Double.//DateTime.Now.ToOADate();
            _note = null;
            _stateType = TripStateType.AddTravelers;
            _reservations = new List<Reservation> { };
            _travelers = new List<Person> { };
        }

        public bool AddTraveler(Person traveler)
        {
            if (_travelers.Contains(traveler))
                return false;
            else
            {
                _travelers.Add(traveler);
                return true;
            }
        }

        public bool AddReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
            return true;
        }

        public bool SetNote(string note)
        {
            if (note.Length >= 25)
            {
                _note = note;
                return true;
            }
            else
                return false;
        }

        public decimal TotalBill()
        {
            if (_reservations.Count == 0)
                return 0m;
            else
            {
                decimal result = 0m;
                for (var i = 0; i < _reservations.Count; i++)
                {
                    result += _reservations[i]._package._price;
                }
                return result;
            }
        }
    }

    public class TripPersistence
    {
        public List<Trip> _globalTrips { get; set; }

        public TripPersistence()
        {
            _globalTrips = new List<Trip>();
        }

        public bool AddTrip(Trip trip)
        {
            _globalTrips.Add(trip);
            return true;
        }

        //public bool Save()
        //{
        //    Jso
        //    return true;
        //}

        //public List<Trip> Load()
        //{
        
        //}

    }

    
}
