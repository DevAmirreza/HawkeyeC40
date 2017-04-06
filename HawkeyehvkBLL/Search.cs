using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;


namespace HawkeyehvkBLL
{
    public class Search
    {
        public bool validatePetNumber(int petNum)
        {
            SearchDB db = new SearchDB();
            return db.searchPetDB(petNum) == 1 ? true : false;
        }


        public bool validateOwnerNumber(int ownNum)
        {
            SearchDB db = new SearchDB();
            return db.searchOwnerDB(ownNum) == 1 ? true : false;
        }

        public bool validateReservationNumber(int resNum)
        {
            SearchDB db  = new SearchDB();
            return db.searchReservationDB(resNum) == 1 ? true : false;

        }

        public bool validateVaccNumber(int vaccNum)
        {
            SearchDB db = new SearchDB();
            return db.searchVaccDB(vaccNum) == 1 ? true : false;
        }
         public bool validateConflictingReservations(int petNum, DateTime startDate, DateTime endDate) {// checks if pet already has vaccination on those days
            SearchDB db = new SearchDB();
            return db.searchConflictingReservations(petNum, startDate, endDate) == 0 ? true : false;
        }

        public char getPetSize(int petNumber)
        {
            SearchDB db = new SearchDB();
            return db.getPetSize(petNumber);
        }

        public bool validatePetResNum(int petResNum)
        {
            SearchDB db = new SearchDB();
            return db.searchPetResDB(petResNum) == 1 ? true : false;
        }

      public bool validateReservationForPet(int petNum, int resNum)
        {
            SearchDB db = new SearchDB();
            return db.searchReservationForPet(petNum, resNum) == 1 ? true : false;
        }


        public static int validateOwnerForPet(int resNum , int petNum)
        {
            SearchDB db = new SearchDB();
            return db.searchPetOwner(resNum, petNum); 
        }

    }
}
