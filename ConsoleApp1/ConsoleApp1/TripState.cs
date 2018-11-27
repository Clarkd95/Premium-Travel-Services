using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectIteration3
{
    public enum TripStateType
    {
        AddTravelers,
        AddPackages,
        ChoosePayment,
        AcceptCash,
        AcceptCard,
        AcceptCheck,
        AddNote,
        Complete
    }

    public interface IState
    {
        bool Execute();
    }

    abstract class TripState : IState
    {
        protected Trip _contextTrip;

        protected TripState(Trip contextTrip)
        {
            _contextTrip = contextTrip;
        }

        public abstract bool Execute();
    }

    public class TripStateFactory
    {
        public static IState Make(Trip context)
        {
            switch (context._stateType)
            {
                case TripStateType.AddTravelers:
                    context._state = new TripStateAddTravelers(context);
                    break;
                case TripStateType.AddPackages:
                    context._state = new TripStateAddPackages(context);
                    break;
                case TripStateType.ChoosePayment:
                    context._state = new TripStateChoosePayment(context);
                    break;
                case TripStateType.AcceptCash:
                    context._state = new TripStateAcceptCash(context);
                    break;
                case TripStateType.AcceptCard:
                    context._state = new TripStateAcceptCard(context);
                    break;
                case TripStateType.AcceptCheck:
                    context._state = new TripStateAcceptCheck(context);
                    break;
                case TripStateType.AddNote:
                    context._state = new TripStateAddNote(context);
                    break;
                case TripStateType.Complete:
                    context._state = new TripStateComplete(context);
                    break;
                default:
                    throw new InvalidOperationException("Unknown state " + context._state);
            }
            return context._state;
        }
    }


    class TripStateAddTravelers : TripState
    {
        public TripStateAddTravelers(Trip contextTrip) : base(contextTrip)
        {

        }

        public override bool Execute()
        {
            while (true)
            {
                
                if(_contextTrip._travelers.Count>0)
                {
                    Console.WriteLine($"There are currently {_contextTrip._travelers.Count} travelers booked on this trip.");
                    for(var traveler = 0; traveler < _contextTrip._travelers.Count; traveler++)
                    {
                        Console.WriteLine($"{traveler + 1}. {_contextTrip._travelers[traveler]._name}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Type an integer corresponding to the listed position of a person to add as a traveler to Trip #{_contextTrip._ID}");
                Console.WriteLine($"Type [done] when you have entered all travelers, type [later] to save and return later\n");


                //outputs contents of the Read-Only list
                IReadOnlyList<Person> startupList = PersonList.GetStartupPersonList();
                for (var person = 0; person < startupList.Count; person++)
                {
                    Console.WriteLine($"{person + 1}. {startupList[person]._name}, {startupList[person]._phoneNum}");
                }
                string input = Console.ReadLine();
                Console.WriteLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (input.Equals("done", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //this if statement prevents continuing  with no travelers
                        if (_contextTrip._travelers.Count > 0)
                        {
                            _contextTrip._stateType = TripStateType.AddPackages;
                            _contextTrip._state = TripStateFactory.Make(_contextTrip);
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Please enter at least one traveler to continue Trip creation.");
                            return true;
                        }
                    }
                    else if (input.Equals("later", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //Save()
                        return false;
                    }

                }
                else
                {
                    Console.WriteLine("Blank not accepted - enter [done] to move to next");
                }

                int output;  
                if (int.TryParse(input, out output) && (output > 0 && output <= startupList.Count) && !_contextTrip._travelers.Contains(startupList[output-1]))
                {
                    _contextTrip.AddTraveler(startupList[output-1]);
                    Console.WriteLine($"{startupList[output-1]._name} successfully added to trip.");
                    
                    return true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer corresponding to a person not yet included as a traveler");
                    return true;
                }


            }
        }
    }

    class TripStateAddPackages : TripState
    {
        public TripStateAddPackages(Trip contextTrip) : base(contextTrip)
        {

        }

        public override bool Execute()
        {
            while(true)
            {
                IReadOnlyList<Package> startupList = PackageList.GetStartupPackageList();

                if (_contextTrip._reservations.Count>0)
                {
                    Console.WriteLine($"There are {_contextTrip._reservations.Count} packages reserved on this trip:");
                }

                Console.WriteLine($"Enter the number next to a package to select it for reservation.");
                Console.WriteLine($"Type [done] to continue or type [later] to save and return later.");

                

                for (var package = 0; package < startupList.Count; package++)
                {
                    Console.WriteLine($"{package + 1}. {startupList[package]._transport} from {startupList[package]._origin} to {startupList[package]._destination}. Travel Time = {startupList[package]._hoursOfTravelTime.ToString()}");
                }

                string input1 = Console.ReadLine();
                Console.WriteLine();

                if (!string.IsNullOrWhiteSpace(input1))
                {
                    if (input1.Equals("done", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //this if statement prevents continuing  with no travelers
                        if (_contextTrip._reservations.Count > 0)
                        {
                            _contextTrip._stateType = TripStateType.ChoosePayment;
                            _contextTrip._state = TripStateFactory.Make(_contextTrip);
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Please reserve at least one package to continue Trip creation.");
                            return true;
                        }
                    }
                    else if ((input1.Equals("later", StringComparison.CurrentCultureIgnoreCase)))
                    {
                        //Save()
                        
                        return false;
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("Blank not accepted.");
                    return true;
                }

                int output;
                if (int.TryParse(input1, out output)&&(output > 0 && output <= startupList.Count))
                {
                    Console.WriteLine($"Enter departure time in mm/dd/yy hh:mm format");
                    string departure = Console.ReadLine();
                    Console.WriteLine();
                    DateTime departureDateTime;
                    if(DateTime.TryParse(departure, out departureDateTime))
                    {
                        Reservation newReservation = new Reservation(departureDateTime, startupList[output-1]);
                        _contextTrip.AddReservation(newReservation);
                        return true;

                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid date/time to reserve package for Trip.");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine($"Please enter a valid integer");
                    return true;
                }

            }
        }
    }

    class TripStateChoosePayment : TripState
    {
        public TripStateChoosePayment(Trip contextTrip) : base(contextTrip)
        {

        }

        public override bool Execute()
        {
            while (true)
            {
                Console.WriteLine("Enter [cash], [card], or [check]");
                Console.WriteLine("Enter [later] to save and return later.");
                string newItem = Console.ReadLine();
                Console.WriteLine();
                if (newItem.Equals("cash", StringComparison.CurrentCultureIgnoreCase))
                {
                    _contextTrip._stateType = TripStateType.AcceptCash;
                    _contextTrip._state = TripStateFactory.Make(_contextTrip);
                    return true;
                }
                else if (newItem.Equals("card", StringComparison.CurrentCultureIgnoreCase))
                {
                    _contextTrip._stateType = TripStateType.AcceptCard;
                    _contextTrip._state = TripStateFactory.Make(_contextTrip);
                    return true;
                }
                else if (newItem.Equals("check", StringComparison.CurrentCultureIgnoreCase))
                {
                    _contextTrip._stateType = TripStateType.AcceptCheck;
                    _contextTrip._state = TripStateFactory.Make(_contextTrip);
                    return true;
                }
                else if (newItem.Equals("later", StringComparison.CurrentCultureIgnoreCase))
                {
                    //Save();
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                    return true;
                }
            }
        }
    }

    class TripStateAcceptCash : TripState
    {
        public TripStateAcceptCash(Trip contextTrip) : base(contextTrip)
        {

        }

        public override bool Execute()
        {
            

            _contextTrip.Payment = new PaymentCash(_contextTrip);
            _contextTrip.Payment.Collect();

            _contextTrip._stateType = TripStateType.AddNote;
            _contextTrip._state = TripStateFactory.Make(_contextTrip);
            return true;
        }
    }

    class TripStateAcceptCard : TripState
    {
        public TripStateAcceptCard(Trip contextTrip) : base(contextTrip)
        {

        }

        public override bool Execute()
        {
            _contextTrip.Payment = new PaymentCard(_contextTrip);
            _contextTrip.Payment.Collect();

            _contextTrip._stateType = TripStateType.AddNote;
            _contextTrip._state = TripStateFactory.Make(_contextTrip);
            return true;
        }
    }

    class TripStateAcceptCheck : TripState
    {
        public TripStateAcceptCheck(Trip contextTrip) : base(contextTrip)
        {

        }

        public override bool Execute()
        {
            _contextTrip.Payment = new PaymentCheck(_contextTrip);
            _contextTrip.Payment.Collect();

            _contextTrip._stateType = TripStateType.AddNote;
            _contextTrip._state = TripStateFactory.Make(_contextTrip);
            return true;
        }
    }

    class TripStateAddNote : TripState
    {
        public TripStateAddNote(Trip contextTrip) : base(contextTrip)
        {

        }

        public override bool Execute()
        {
            while (true)
            {
                Console.WriteLine($"Please Enter a thank you note greater than 25 characters in length.");
                Console.WriteLine($"Enter [later] to save and return later.");
                string input = Console.ReadLine();
                Console.WriteLine();
                if (input.Equals("later", StringComparison.CurrentCultureIgnoreCase))
                {
                    //Save();
                    return false;
                }
                else if (_contextTrip.SetNote(input))
                {
                    Console.WriteLine($"Thank You Note successfully added.");
                    _contextTrip._stateType = TripStateType.Complete;
                    _contextTrip._state = TripStateFactory.Make(_contextTrip);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid input.");
                    return true;
                }


                
            }
        }
    }

    class TripStateComplete : TripState
    {
        public TripStateComplete(Trip contextTrip) : base(contextTrip)
        {

        }

        public override bool Execute()
        {
            ItineraryComponent decorator = new Itinerary(_contextTrip);
            decorator = new ItineraryDecoratorHeaderNote(decorator);
            decorator = new ItineraryDecoratorTravelerList(decorator);
            decorator = new ItineraryDecoratorPackageList(decorator);
            decorator = new ItineraryDecoratorBookingDetails(decorator);
            decorator = new ItineraryDecoratorBillingDetails(decorator);
            decorator = new ItineraryDecoratorFooter(decorator);

            decorator.Display();

            return false;
        }
    }
}
