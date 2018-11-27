using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectIteration3
{
    //these first three classes are the top 3 classes in the decorator pattern from my Iteration 2 diagram
    abstract class ItineraryComponent
    {
        
        public Trip _trip { get; set; }
        
        public abstract void Display();

    }

    abstract class ItineraryDecorator : ItineraryComponent
    {
        //variables
        protected ItineraryComponent _itineraryComponent;
        //constructor
        public ItineraryDecorator(ItineraryComponent itineraryComponent)
        {
            _itineraryComponent = itineraryComponent;
            _trip = _itineraryComponent._trip;
        }
        //functions
        public override void Display()
        {
            _itineraryComponent.Display();//nothing to see here, still non-specific class
        }

    }

    class Itinerary : ItineraryComponent
    {
        public Itinerary(Trip trip)
        {
            _trip = trip;
        }
        public override void Display()
        {
            //nothing to see here, still non-specific class
        }
    }

    class ItineraryDecoratorHeaderNote : ItineraryDecorator
    {
        public ItineraryDecoratorHeaderNote(ItineraryComponent itineraryComponent) : base(itineraryComponent)
        {
            
        }

        public override void Display()
        {
            Console.WriteLine();
            Console.WriteLine($"Itinerary by {_trip.Agent._name}, {_trip.Agent._phoneNum}");
            Console.WriteLine();
            Console.WriteLine($"{_trip._note}");
            Console.WriteLine();
            //actual implementation for ItineraryDecoratorHeaderNote
        }
    }

    class ItineraryDecoratorTravelerList : ItineraryDecorator
    {
        public ItineraryDecoratorTravelerList(ItineraryComponent itineraryComponent) : base(itineraryComponent)
        {
            
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"{_trip._travelers.Count} Travelers");
            Console.WriteLine();
            for (int i = 0; i < _trip._travelers.Count; i++)
            {
                Console.WriteLine($"{i+1}. {_trip._travelers[i]._name}");
            }
            Console.WriteLine();
            //actual implementation for ItineraryDecoratorTravelerList
        }
    }

    class ItineraryDecoratorPackageList : ItineraryDecorator
    {
        public ItineraryDecoratorPackageList(ItineraryComponent itineraryComponent) : base(itineraryComponent)
        {
            
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine();
            Console.WriteLine($"Trip Details, beginning {_trip._reservations[0]._departureDateTime.ToString()} and ending {_trip._reservations[_trip._reservations.Count-1]._arrivalDateTime.ToString()}");
            for (int i = 0; i < _trip._reservations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_trip._reservations[i].ToString()}");
            }
            Console.WriteLine();
            //actual implementation for ItineraryDecoratorPackageList
        }
    }

    class ItineraryDecoratorBillingDetails : ItineraryDecorator
    {
        public ItineraryDecoratorBillingDetails(ItineraryComponent itineraryComponent) : base(itineraryComponent)
        {
            
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine();
            Console.WriteLine("Billing");
            Console.WriteLine();
            Console.WriteLine($"Total: {_trip.TotalBill()}");
            Console.WriteLine(_trip.Payment.ToString());
            Console.WriteLine();
            Console.WriteLine("Billing Details");
            Console.WriteLine();
            for (int i = 0; i < _trip._reservations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_trip._reservations[i].ToString()}");
                Console.WriteLine($"${_trip._reservations[i]._package._price}");
            }
            Console.WriteLine();
            //actual implementation for ItineraryDecoratorBillingDetails
        }
    }

    class ItineraryDecoratorBookingDetails : ItineraryDecorator
    {
        public ItineraryDecoratorBookingDetails(ItineraryComponent itineraryComponent) : base(itineraryComponent)
        {
           
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Booking");
            Console.WriteLine();
            Console.WriteLine($"Every detail your trip was booked with care by {_trip.Agent._name}. If you have any questions or problems, call {_trip.Agent._name} at {_trip.Agent._phoneNum} anytime, 24 hours a day");
            Console.WriteLine();
            //actual implementation for ItineraryDecoratorBookingDetails
        }
    }

    class ItineraryDecoratorFooter : ItineraryDecorator
    {
        public ItineraryDecoratorFooter(ItineraryComponent itineraryComponent) : base(itineraryComponent)
        {
            
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine();
            Console.WriteLine("----------Thank you for using Premium Travel Services, luxury travel made simple.----------");
            Console.WriteLine();
            //actual implementation for ItineraryDecoratorFooter
        }
    }
}
