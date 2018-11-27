using System;
using System.Collections.Generic;
using Newtonsoft.Json;

//Clark Davis Wilson
//11-24-2018 Start
//SWE 4743 OOD - 01
//Jeff Adkisson

namespace ProjectIteration3
{
    class ProjectMain
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Login();
            program.SelectAction();
        }
    }

    public class Program
    {
        public TripPersistence persistence;// = new TripPersistence();
        public TravelAgent CurrentAgent;

        public Program()
        {
            persistence = new TripPersistence();
        }

        public void Login()
        {
            //persistence = new TripPersistence();
            bool success = false;
            while (success == false)
            {
                Console.WriteLine("Please select Travel Agent by typing Agent ID:");
                IReadOnlyList<TravelAgent> startupAgents = TravelAgentList.GetStartupAgentList();
                for (var person = 0; person < startupAgents.Count; person++)
                {
                    Console.WriteLine($"{person + 1}. {startupAgents[person]._name}, {startupAgents[person]._agentID}");
                }

                string input = Console.ReadLine();
                int id;
                if (int.TryParse(input, out id))
                {
                    for (int i = 0; i < startupAgents.Count; i++)
                    {
                        if (id == startupAgents[i]._agentID)
                        {
                            CurrentAgent = startupAgents[i];
                            Console.WriteLine($"Current Agent = {CurrentAgent._name}");
                            success = true;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer:");
                    success = false;
                }
            }
        }

        public void SelectAction()
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("Please enter an integer to select Workflow \n1: Create New Trip\n2: View Your Trips\n3: Resume Trip");
                string input = Console.ReadLine();
                Console.WriteLine();
                int result;
                if (int.TryParse(input, out result))
                {
                    switch (result)
                    {
                        case 1:
                            this.CreateNewTrip();
                            break;
                        case 2:
                            this.ViewTrips();
                            break;
                        case 3:
                            this.ResumeTrip();
                            break;
                        default:
                            Console.WriteLine($"Please enter 1, 2 or 3.");
                            break;
                    }


                }
                else
                    Console.WriteLine("Please enter a valid integer.");

            }
        }

        public void CreateNewTrip()
        {

            Trip trip = new Trip(CurrentAgent);
            persistence.AddTrip(trip);
            IState state = TripStateFactory.Make(trip);
            //while (trip._stateType != TripStateType.Complete)
            //{
                while (state.Execute())
                {
                    state = trip._state;
                }
            //}
        }

        public void ViewTrips()
        {
            List<Trip> trips = persistence._globalTrips;
            //List<Trip> trips = CurrentAgent.GetTrips(persistence);
            if (trips.Count > 0)
            {
                for (int i = 0; i < trips.Count; i++)
                    if (trips[i].Agent == CurrentAgent)
                    {
                        Console.WriteLine($"{i + 1}. Trip ID #: {trips[i]._ID}, Trip State: {trips[i]._stateType}");
                    }
            }
            else
                Console.WriteLine($"There are no trips associated with this Agent.\n");
        }

        public void ResumeTrip()
        {
            Console.WriteLine("Please enter an integer correspoding with a Trip ID # to resume that trip");
            string input = Console.ReadLine();
            Console.WriteLine();
            int result;
            List<Trip> trips = persistence._globalTrips;
            bool found = false;
            if (int.TryParse(input, out result))
            {
                for (int i = 0; i < trips.Count; i++)//(int i = 0; i < trips.Count; i++)
                {
                    if (result == trips[i]._ID)
                    {
                        Trip trip = trips[i];
                        IState state = TripStateFactory.Make(trip);
                        //while (trip._stateType != TripStateType.Complete)
                        //{
                        while (state.Execute())
                        {
                            state = trip._state;
                        }
                        found = true;
                        break;
                    }
                    //else
                        //break;
                }
                if(!found)
                    Console.WriteLine($"Trip ID #:{result} was not found.");
            }
            else
                Console.WriteLine("Please enter a valid integer.");
        }
    }
}
