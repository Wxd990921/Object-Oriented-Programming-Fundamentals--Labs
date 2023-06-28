using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_Many_and_Inheritance.Classes
{
    public class Hotel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Client> Clients { get; set; }

        public Hotel(string name, string address)
        {
            Name = name;
            Address = address;
            Rooms = new List<Room>();
            Clients = new List<Client>();
        }
    }
}
