using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkeyehvkBLL
{
    public class KennelLog
    {
        public DateTime date { get; set; }

        public int sequenceNumber { get; set; }

        public string notes { get; set; }

        public List<PetReservation> petResList { get; protected set; }

        public KennelLog()
        {
            this.date = DateTime.MinValue;
            this.sequenceNumber = 0;
            this.notes = "";
            this.petResList = new List<PetReservation>();
        }
        
        public KennelLog(DateTime date, int sequence, string notes)
        {
            this.date = date;
            this.sequenceNumber = sequence;
            this.notes = notes;
            this.petResList = new List<PetReservation>();
        }

        public bool addPetRes(PetReservation petRes)
        {
            this.petResList.Add(petRes);
            return true;
        }

        public bool removePetRes(PetReservation petRes)
        {
            return this.petResList.Remove(petRes);
        }
    }
}
