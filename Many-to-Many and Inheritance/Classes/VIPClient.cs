using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_Many_and_Inheritance.Classes
{
    public class VIPClient : Client
    {
        public int VIPNumber { get; set; }
        public int VIPPoints { get; set; }

        public VIPClient(string name, long creditCard, int vipNumber) : base(name, creditCard)
        {
            VIPNumber = vipNumber;
            VIPPoints = 0;
        }
    }
}
