using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class PetVaccination
    {
        public DateTime expirationDate { get; set; }

        public Vaccination vaccination { get; set; }

        public char isValidated { get; protected set; }

        public PetVaccination()
        {
            this.expirationDate = DateTime.MinValue;
            this.vaccination = new Vaccination();
            this.isValidated = 'F';
        }
        public PetVaccination(DateTime expires, Vaccination vaccination)
        {
            this.expirationDate = expires;
            this.vaccination = vaccination;
            this.isValidated = 'F';
        }

        public PetVaccination(DateTime expires, Vaccination vaccination, char isValidated)
        {
            this.expirationDate = expires;
            this.vaccination = vaccination;
            this.isValidated = isValidated;
        }

        public bool validate()
        {
            this.isValidated = 'T';
            return true;
        }

        public static List<PetVaccination> checkVaccinations(int petNum, int resNum)
        {
            Search look = new Search();
            List<PetVaccination> petVaccList = new List<PetVaccination>();
            if (look.validatePetNumber(petNum) == false)
            {

            }
            else if(!look.validateReservationNumber(resNum)){

            }
            else
            {
                VaccinationDB vaccDB = new VaccinationDB();

                foreach (DataRow row in vaccDB.checkVaccinationsDB(petNum, resNum).Tables["hvk_vaccination"].Rows)
                {
                    petVaccList.Add(fillVaccination(row));
                }
            }
               
                 return petVaccList;
               
         
            
        }

        public static int checkVaccinations(int petNum, DateTime byDate)
        {
            Search look = new Search();

            if (!look.validatePetNumber(petNum))
            {
                return -10;
            }
            else
            {
                VaccinationDB vaccDB = new VaccinationDB();
                List<PetVaccination> petVaccList = new List<PetVaccination>();
                foreach (DataRow row in vaccDB.checkVaccinationsDB(petNum, byDate).Tables["hvk_vaccination"].Rows)
                {
                    petVaccList.Add(fillVaccination(row));
                }
                if (petVaccList.Count == 6)
                    return -1;
                else
                    return 0;
            }
        }

        public static List<PetVaccination> listVaccinations(int petNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            List<PetVaccination> vaccList = new List<PetVaccination>();
            foreach (DataRow row in vaccDB.listVaccinationsDB(petNum).Tables["hvk_vaccination"].Rows)
            {
                vaccList.Add(fillVaccination(row));
            }
            return vaccList;
        }

        public static PetVaccination fillVaccination(DataRow row)
        {
            PetVaccination petVacc = new PetVaccination();

            petVacc.vaccination.vaccinationNumber = Convert.ToInt32(row["vaccination_number"].ToString());
            petVacc.vaccination.name = row["vaccination_name"].ToString();
            string expireDate = row["VACCINATION_EXPIRY_DATE"].ToString();
            petVacc.expirationDate = (expireDate.Length > 0) ? Convert.ToDateTime(expireDate) : DateTime.MinValue;
            //petVacc.expirationDate = Convert.ToDateTime(row["VACCINATION_EXPIRY_DATE"].ToString());
            string valid = row["VACCINATION_CHECKED_STATUS"].ToString();
            petVacc.isValidated = (valid.Length > 0) ? Convert.ToChar(valid) : 'N';
            //petVacc.isValidated = Convert.ToChar(row["VACCINATION_CHECKED_STATUS"].ToString());
            return petVacc;

        }
        public static int updatePetVaccinationChecked(char isChecked, int vacNumber, int petNumber)
        {
            Search search = new Search();
            if (!search.validatePetNumber(petNumber))
            {
                return -2;
            }
            if (!search.validateVaccNumber(vacNumber))
            {
                return -3;
            }

            return VaccinationDB.updatePetVaccinationCheckedDB(isChecked, vacNumber, petNumber);
        }
        public static int updatePetVaccinationExpiry(DateTime expiryDate, int vacNumber, int petNumber)
        {
            Search search = new Search();
            if (!search.validatePetNumber(petNumber))
            {
                return -2;
            }
            if (!search.validateVaccNumber(vacNumber))
            {
                return -3;
            }
            return VaccinationDB.updatePetVaccinationExpiryDB(expiryDate, vacNumber, petNumber);
        }
        public static int addPetVaccination(DateTime expiryDate, int vacNumber, int petNumber)
        {
            
            Search search = new Search();
            if (!search.validatePetNumber(petNumber))
            {
                return -2;
            }
            else if (!search.validateVaccNumber(vacNumber))
            {
                return -3;
            }
            bool hasVacc = false;
            listVaccinations(petNumber).ForEach(delegate (PetVaccination vac) {
                if (vac.vaccination.vaccinationNumber == vacNumber)
                {
                    hasVacc = true;
                }
            });
            if (hasVacc) { // if the pet already has the vaccination just update it.
                updatePetVaccinationExpiry(expiryDate,vacNumber,petNumber);
            }
            return VaccinationDB.addPetVaccinationDB(expiryDate, vacNumber, petNumber);
        }
    }
}
