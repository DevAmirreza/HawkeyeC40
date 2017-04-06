using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkeyehvkBLL
{
    public class Clerk : User
    {
        public int clerkNumber { get; set; }

        public Clerk() : base()
        {
            base.role = 'C';
            this.clerkNumber = -1;
        }

        public Clerk(string fName, string lName, int clerkNumber) : base(fName, lName)
        {
            base.role = 'C';
            this.clerkNumber = clerkNumber;
        }
    }
}
