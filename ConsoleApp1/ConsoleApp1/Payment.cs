using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectIteration3
{
    public abstract class Payment
    {
        protected Trip _trip;
        public decimal Amount { get; set; }
        public Person PaidBy { get; set; }

        public Payment(Trip context)
        {
            _trip = context;
            bool success = false;
            while (success == false)
            {
                Console.WriteLine($"Enter the integer next to a person from the list to select who pays the bill");
                IReadOnlyList<Person> startupList = PersonList.GetStartupPersonList();
                for (var person = 0; person < startupList.Count; person++)
                {
                    Console.WriteLine($"{person + 1}. {startupList[person]._name}, {startupList[person]._phoneNum}\n");
                }
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    int output;
                    if (int.TryParse(input, out output) && (output > 0 && output <= startupList.Count))
                    {
                        PaidBy = (startupList[output-1]);
                        Console.WriteLine($"{startupList[output-1]._name} selected to pay bill.");

                        success = true; break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer.");
                        success=false;
                    }
                }
                else
                {
                    Console.WriteLine("Blank not accepted.");
                    success = true;
                }
            }

        }

        public virtual void Collect()
        {
            decimal pay;
            while (true)
            {
                Console.WriteLine($"The total is ${_trip.TotalBill()} for {_trip._reservations.Count} legs of the trip");
                Console.WriteLine("Enter payment amount: ");
                if (decimal.TryParse(Console.ReadLine(), out pay))
                {
                    if (pay == _trip.TotalBill())
                    {
                        Console.WriteLine("Payment Successful!");
                        Amount = pay;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Payment Failed! Payment Amount does not match Total Price.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid payment amount.");
                }

            }
        }

        public override string ToString()
        {
            return $"Paid in full by {PaidBy._name} ";
        }
    }

    public class PaymentCash : Payment
    {
        public PaymentCash(Trip context) : base(context)
        {

        }
        public override void Collect()
        {
            
            base.Collect();
        }
        public override string ToString()
        {
            return base.ToString() + $"using Cash:\nAmount: ${Amount}";
        }
    }

    public class PaymentCard : Payment
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }

        public PaymentCard(Trip context) : base(context)
        {

        }
        public override void Collect()
        {
            
                Console.WriteLine("Scan Card: Enter Card #, then enter expiration date.");
                string cardNum = Console.ReadLine();
                CardNumber = cardNum;
                string exp = Console.ReadLine();
                ExpirationDate = exp;

                
            
            base.Collect();
            //card num and expiration
        }
        public override string ToString()
        {
            return base.ToString() + $"using Card:\nCard #: {CardNumber}\nExpiration Date: {ExpirationDate}\nAmount: ${Amount}"; //card num and expiration
        }
    }

    public class PaymentCheck : Payment
    {
        public int CheckNumber { get; private set; }

        public PaymentCheck(Trip context) : base(context)
        {

        }
        public override void Collect()
        {
           
            while (true)
            {
                Console.WriteLine("Enter Check number: ");
                int checknum = int.Parse(Console.ReadLine());
                if (checknum > 100)
                {
                    Console.WriteLine(checknum + " is good!");
                    CheckNumber = checknum;
                    break;
                }
                else
                {
                    Console.WriteLine("Check Number is invalid");
                }
            }
            base.Collect();

        }
        public override string ToString()
        {
            return base.ToString() + $"using Check:\nCheck #: {CheckNumber}\nAmount: ${Amount}";
        }
    }

}
