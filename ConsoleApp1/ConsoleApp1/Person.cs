using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectIteration3
{
    public class Person
    {
        //variables
        public string _name;
        public string _phoneNum;

        //constructor
        public Person(string name, string phoneNum)
        {
            _name = name;
            _phoneNum = phoneNum;
        }
    }

    public class TravelAgent : Person
    {
        //variables
        public int _agentID;
        //public List<Trip> _agentTrips;

        //constructor
        public TravelAgent(string name, string phoneNum, int agentID) : base(name, phoneNum)
        {
            _name = name;
            _phoneNum = phoneNum;
            _agentID = agentID;
            //_agentTrips = all trips that belong to this travel agent
        }    
    }


    class PersonList
    {
        private static readonly object SyncLock = new object();
        private static volatile PersonList _personList;
        private IReadOnlyList<Person> _startupPersonList { get; set; }

        private PersonList()
        {
            Person p1 = new Person("Mr. Poe Pickles","470-555-5578");
            Person p2 = new Person("Jeff Adkisson", "470-345-5678");
            Person p3 = new Person("Clark Wilson", "470-543-8764");
            Person p4 = new Person("Mr. Pickles' Friend #1", "470-000-0000");
            Person p5 = new Person("Mr. Pickles' Friend #2", "470-000-0001");
            Person p6 = new Person("Mr. Pickles' Friend #3", "470-000-0002");
            Person p7 = new Person("Mr. Pickles' Friend #4", "470-000-0003");
            Person p8 = new Person("Mr. Pickles' Friend #5", "470-000-0004");


            _startupPersonList = new List<Person>
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

        public static IReadOnlyList<Person> GetStartupPersonList()
        {
            //Double Checked, Thread Safe Singleton returns
            if (_personList == null)
            {
                lock (SyncLock)
                {
                    if (_personList == null) { _personList = new PersonList(); }
                }
            }
            return _personList._startupPersonList;
        }
    }

    class TravelAgentList
    {
        private static readonly object SyncLock = new object();
        private static volatile TravelAgentList _agentList;
        private IReadOnlyList<TravelAgent> _startupAgentList { get; set; }

        private TravelAgentList()
        {
            TravelAgent a1 = new TravelAgent("Michael Franklin", "470-525-5272",543);
            TravelAgent a2 = new TravelAgent("Demonte", "470-365-5666",123);
            TravelAgent a3 = new TravelAgent("Clark Wilson", "470-543-8764",117);



            _startupAgentList = new List<TravelAgent>
            {
                a1,
                a2,
                a3
            };
        }

        public static IReadOnlyList<TravelAgent> GetStartupAgentList()
        {
            //Double Checked, Thread Safe Singleton returns
            if (_agentList == null)
            {
                lock (SyncLock)
                {
                    if (_agentList == null) { _agentList = new TravelAgentList(); }
                }
            }
            return _agentList._startupAgentList;
        }
    }

    
}
