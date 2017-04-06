using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class Pet
    {
        public int petNumber { get; set; }

        public string name { get; set; }

        public char gender { get; set; }

        public char isFixed { get; set; }

        public string breed { get; set; }

        public DateTime birthday { get; set; }

        public char size { get; set; }

        public string notes { get; set; }

        public List<PetVaccination> vaccinationList { get; protected set; }

        public List<PetReservation> petReservationList { get; protected set; }

        public Pet()
        {
            this.petNumber = -1;
            this.name = "";
            this.gender = 'M';
            this.isFixed = 'F';
            this.breed = "";
            this.birthday = DateTime.MinValue;
            this.size = 'S';
            this.notes = "";
            this.vaccinationList = new List<PetVaccination>();
            this.petReservationList = new List<PetReservation>();
        }

        public Pet(int petNum, string name, char gender, char isFixed)
        {
            this.petNumber = petNum;
            this.name = name;
            this.gender = gender;
            this.isFixed = isFixed;
            this.breed = "";
            this.birthday = DateTime.MinValue;
            this.size = 'S';
            this.notes = "";
            this.vaccinationList = new List<PetVaccination>();
            this.petReservationList = new List<PetReservation>();
        }

        public Pet(int petNum, string name, char gender, char isFixed, string breed, DateTime bday, char size, string notes)
        {
            this.petNumber = petNum;
            this.name = name;
            this.gender = gender;
            this.isFixed = isFixed;
            this.breed = breed;
            this.birthday = bday;
            this.size = size;
            this.notes = notes;
            this.vaccinationList = new List<PetVaccination>();
            this.petReservationList = new List<PetReservation>();
        }

        public bool addPetReservation(PetReservation petRes)
        {
            this.petReservationList.Add(petRes);
            if (!petRes.pet.Equals(this))
                petRes.pet = this;
            return true;
        }

        public bool removePetReservation(PetReservation petRes)
        {
            petRes.pet = new Pet();
            return this.petReservationList.Remove(petRes);
        }

        public bool addVaccination(PetVaccination petVaccination)
        {
            this.vaccinationList.Add(petVaccination);
            return true;
        }

        public bool removeVaccination(PetVaccination petVaccination)
        {
            return this.vaccinationList.Remove(petVaccination);
        }

        private static Pet fillFromDataRow(DataRow row)
        {
            Pet pet = new Pet();
            pet.petNumber = Convert.ToInt32(row["PET_NUMBER"].ToString());
            pet.name = row["PET_NAME"].ToString();
            pet.gender = Convert.ToChar(row["PET_GENDER"].ToString());
            pet.isFixed = Convert.ToChar(row["PET_FIXED"].ToString());
            pet.breed = row["PET_BREED"].ToString();
            string bday = row["PET_BIRTHDATE"].ToString();
            pet.birthday = (bday.Length > 0) ? Convert.ToDateTime(bday) : DateTime.MinValue;
            pet.size = Convert.ToChar(row["DOG_SIZE"].ToString());
            pet.notes = row["SPECIAL_NOTES"].ToString();
            return pet;
        }

        public static List<Pet> listPets(int ownerNumber)
        {
            List<Pet> petList = new List<Pet>();
            PetDB db = new PetDB();
            foreach(DataRow row in db.listPetsDB(ownerNumber).Tables["hvk_pet"].Rows)
            {
                Pet pet = fillFromDataRow(row);
                petList.Add(pet);
            }
            return petList;
        }
        public static Pet getOnePet(int petNumber)
        {
            List<Pet> petList = new List<Pet>();
            PetDB db = new PetDB();
            foreach (DataRow row in db.getOnePetDB(petNumber).Tables["hvk_pet"].Rows)
            {
                Pet pet = fillFromDataRow(row);
                petList.Add(pet);
            }
            return petList[0];
        }

        public static int checkPetsInRes(int resNum)
        {
            PetDB petDB = new PetDB();
            return petDB.checkPetsInReservation(resNum);
        }

        public static List<Pet> listPetsByReservation(int resNum) {
            List<Pet> petList = new List<Pet>();
            PetDB db = new PetDB();
            foreach (DataRow row in db.listPetsByReservationDB(resNum).Tables[0].Rows) {
                Pet pet = fillFromDataRow(row);
                petList.Add(pet);
            }
            return petList;
        }

        public static int addPet(string petName, char gender, char isFixed, string breed, DateTime birthday, char size, string notes) {
            PetDB db = new PetDB();
            if (db.addPetDB(petName, gender, isFixed, breed, birthday, size, notes) != 0) {
                return 1;
            }
            return 0;
        }

        public static int updatePet(int petNum, string petName, char gender, char isFixed, string breed, DateTime birthday, char size, string notes) {
            PetDB db = new PetDB();
            if (db.updatePetDB(petNum, petName, gender, isFixed, breed, birthday, size, notes) != 0) {
                return 1;
            }
            return 0;
        }

        public static int deletePet(int petNum) {
            PetDB db = new PetDB();
            if (db.deletePetDB(petNum) != 0) {
                return 1;
            }
            return 0;
        }
    }
}
