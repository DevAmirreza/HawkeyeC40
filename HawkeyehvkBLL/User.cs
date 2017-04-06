using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkeyehvkBLL
{
    public class User
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public char role { get; protected set; }

        public User()
        {
            this.firstName = "";
            this.lastName = "";
            this.role = 'U';
        }

        public User(string fName, string lName)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.role = 'U';
        }

        public string getFullName()
        {
            return this.firstName + " " + this.lastName;
        }
    }
}
