using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_Many_and_Inheritance.Classes
{
    public class Reservation
    {
        public DateTime Date { get; set; }
        public int Occupants { get; set; }
        public bool IsCurrent { get; set; }
        public Client Client { get; set; }
        public Room Room { get; set; }

        public Reservation(DateTime date, int occupants, Client client, Room room)
        {
            Date = date;
            Occupants = occupants;
            IsCurrent = true;
            Client = client;
            Room = room;
        }
    }

}
