using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//testing changes 2
namespace HawkeyehvkBLL
{
    public class Address
    {
        public string street { get; set; }

        public string city { get; set; }

        public string province { get; set; }

        public string postalCode { get; set; }

        public Address()
        {
            this.street = "";
            this.city = "";
            this.province = "";
            this.postalCode = "";
        }

        public Address(string street, string city, string province, string postalCode)
        {
            this.street = street;
            this.city = city;
            this.province = province;
            this.postalCode = postalCode;
        }

        public override string ToString()
        {
            return this.street + ", " + this.city + ", " + this.province + ", " + this.postalCode;
        }
    }
}
