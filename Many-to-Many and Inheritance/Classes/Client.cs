using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_Many_and_Inheritance.Classes
{
    public class Client
    {
        public string Name { get; set; }
        public long CreditCard { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Client(string name, long creditCard)
        {
            Name = name;
            CreditCard = creditCard;
            Reservations = new List<Reservation>();
        }
    }
}
