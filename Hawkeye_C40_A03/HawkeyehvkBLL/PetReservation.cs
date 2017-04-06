using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using HawkeyehvkDB;

namespace HawkeyehvkBLL
{
    public class PetReservation
    {
        public int petResNumber { get; set; }

        private Pet thePet { get; set; }

        public Pet pet
        {
            get { return thePet; }
            set
            {
                thePet = value;
                if (!thePet.petReservationList.Contains(this))
                    thePet.addPetReservation(this);
            }
        }

        public List<Discount> discountList { get; protected set; }

        public Run run { get; set; }

        public List<Medication> medicationList { get; protected set; }

        public List<ReservedService> serviceList { get; set; }

        public PetFood food { get; set; }

        public int foodFrequency { get; set; }

        public string foodQuantity { get; set; }

        private Reservation res;

        public Reservation reservation
        {
            get { return res; }
            set
            {
                res = value;
                if (!res.petReservationList.Contains(this))
                    res.addPetReservation(this);
            }
        }

        public PetReservation()
        {
            this.petResNumber = -1;
            this.pet = new Pet();
            this.discountList = new List<Discount>();
            this.run = new Run();
            this.medicationList = new List<Medication>();
            this.serviceList = new List<ReservedService>();
            this.food = new PetFood();
            this.foodFrequency = 0;
            this.foodQuantity = "";
            this.reservation = new Reservation();
        }

        public PetReservation(int petResNum, Pet pet, Run run, PetFood food, int foodFrequency)
        {
            this.petResNumber = petResNum;
            this.pet = pet;
            this.discountList = new List<Discount>();
            this.run = run;
            this.medicationList = new List<Medication>();
            this.serviceList = new List<ReservedService>();
            this.food = food;
            this.foodFrequency = foodFrequency;
            this.foodQuantity = "";
            this.reservation = new Reservation();
        }

        public PetReservation(int petResNum, Pet pet, Run run, PetFood food, int foodFrequency, string foodQuantity)
        {
            this.petResNumber = petResNum;
            this.pet = pet;
            this.discountList = new List<Discount>();
            this.run = run;
            this.medicationList = new List<Medication>();
            this.serviceList = new List<ReservedService>();
            this.food = food;
            this.foodFrequency = foodFrequency;
            this.foodQuantity = foodQuantity;
            this.reservation = new Reservation();
        }

        public bool addDiscount(Discount discount)
        {
            this.discountList.Add(discount);
            return true;
        }

        public bool removeDiscount(Discount discount)
        {
            return this.discountList.Remove(discount);
        }

        public bool addMedication(Medication med)
        {
            this.medicationList.Add(med);
            return true;
        }

        public bool removeMedication(Medication med)
        {
            return this.medicationList.Remove(med);
        }

        public bool addService(ReservedService service)
        {
            this.serviceList.Add(service);
            return true;
        }

        public bool removeService(ReservedService service)
        {
            return this.serviceList.Remove(service);
        }
        public static List<PetReservation> listPetRes(int ReservationNumber) {
            PetReservationDB pres = new PetReservationDB();

            List<PetReservation> list = new List<PetReservation>();
            foreach (DataRow row in pres.listPetResDB(ReservationNumber).Tables["hvk_pet_reservation"].Rows)
            {

                PetReservation petRes = new PetReservation();
                try
                {
                    petRes.petResNumber = Convert.ToInt32(row["reservation_number"].ToString());
                    petRes.pet = Pet.getOnePet(Convert.ToInt32(row["PET_PET_NUMBER"]));
                }
                catch (Exception e){
                    Console.Write(e);
                }

            }

            return list;
        }


    }
}
