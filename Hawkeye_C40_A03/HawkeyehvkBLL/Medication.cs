using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkeyehvkBLL
{
    public class Medication
    {
        public int medicationNumber { get; set; }

        public string name { get; set; }

        public string dosage { get; set; }

        public string specialInstructions { get; set; }

        public DateTime endDate { get; set; }

        public Medication()
        {
            this.medicationNumber = -1;
            this.name = "";
            this.dosage = "";
            this.specialInstructions = "";
            this.endDate = DateTime.MinValue;
        }

        public Medication(int medicationNumber, string name, string dosage, string instructions, DateTime end)
        {
            this.medicationNumber = medicationNumber;
            this.name = name;
            this.dosage = dosage;
            this.specialInstructions = instructions;
            this.endDate = end;
        }

        public bool hasExpired()
        {
            return DateTime.Now >= this.endDate;
        }
    }
}
