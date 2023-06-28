using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_Many_and_Inheritance.Classes
{
    public class VIPRoom : Room
    {
        public string AdditionalAmenities { get; set; }
        public int VIPValue { get; set; }

        public VIPRoom(string number, int capacity, string additionalAmenities, int vipValue) : base(number, capacity)
        {
            AdditionalAmenities = additionalAmenities;
            VIPValue = vipValue;
        }
    }
}
