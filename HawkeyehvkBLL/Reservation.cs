using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class Reservation
    {
        public int reservationNumber { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public List<Discount> discountList { get; protected set; }

        public List<PetReservation> petReservationList { get; protected set; }

        public int ownerNumber;

        public Reservation()
        {
            this.reservationNumber = -1;
            this.startDate = DateTime.MinValue;
            this.endDate = DateTime.MaxValue;
            this.discountList = new List<Discount>();
            this.petReservationList = new List<PetReservation>();
            this.ownerNumber = 0;
        }

        public Reservation(int resNumber, DateTime start, DateTime end)
        {
            this.reservationNumber = resNumber;
            this.startDate = start;
            this.endDate = end;
            this.discountList = new List<Discount>();
            this.petReservationList = new List<PetReservation>();
            this.ownerNumber = 0;
        }

        public Reservation(int resNumber, DateTime start, DateTime end, int ownerNumber) {
            this.reservationNumber = resNumber;
            this.startDate = start;
            this.endDate = end;
            this.discountList = new List<Discount>();
            this.petReservationList = new List<PetReservation>();
            this.ownerNumber = ownerNumber;
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

        public bool addPetReservation(PetReservation petRes)
        {
            this.petReservationList.Add(petRes);
            if (!petRes.reservation.Equals(this))
                petRes.reservation = this;        
            return true;
        }

        public bool removePetReservation(PetReservation petRes)
        {
            petRes.reservation = new Reservation();
            return this.petReservationList.Remove(petRes);
        }


        public static List<Reservation> listReservations()
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listResevationsDB();

            List<Reservation> reservationList = fillReservationModified(ds);

            ReservedService rs = new ReservedService();
            reservationList.ForEach(delegate (Reservation res) {
                res.petReservationList.ForEach(delegate (PetReservation pres) {
                    List<ReservedService> ser = rs.listReservedService(pres.petResNumber);
                    if (ser.Count != 0) {
                        pres.serviceList = rs.listReservedService(pres.petResNumber);
                    }
                });
            });

            return reservationList;
        }

        public static List<Reservation> listReservations(int ownerNumber)
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listResevationsDB(ownerNumber);

            List<Reservation> reservationList = fillReservationModified(ds);

            ReservedService rs = new ReservedService();
            reservationList.ForEach(delegate (Reservation res) {
                res.petReservationList.ForEach(delegate (PetReservation pres) {
                    List<ReservedService> ser = rs.listReservedService(pres.petResNumber);
                    if (ser.Count != 0)
                    {
                        pres.serviceList = rs.listReservedService(pres.petResNumber);
                    }
                });
            });

            return reservationList;
        }

        public static Reservation getReservation(int resNum) {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.getReservationDB(resNum);
            Reservation res = fillReservationModified(ds)[0];

            ReservedService rs = new ReservedService();
            res.petReservationList.ForEach(delegate (PetReservation pres) {
                List<ReservedService> ser = rs.listReservedService(pres.petResNumber);
                if (ser.Count != 0) {
                    pres.serviceList = rs.listReservedService(pres.petResNumber);
                }
            });
            return res;
        }

        public static List<Reservation> listActiveReservations()
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listActiveReservationsDB();

            List<Reservation> reservationList = fillReservationModified(ds);

            ReservedService rs = new ReservedService();
            reservationList.ForEach(delegate (Reservation res) {
                res.petReservationList.ForEach(delegate (PetReservation pres) {
                    List<ReservedService> ser = rs.listReservedService(pres.petResNumber);
                    if (ser.Count != 0) {
                        pres.serviceList = rs.listReservedService(pres.petResNumber);
                    }
                });
            });

            return reservationList;
        }


        public static List<Reservation> listActiveReservations(int ownerNumber)
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listActiveReservationsDB(ownerNumber);

            List<Reservation> reservationList = fillReservationModified(ds);

            ReservedService rs = new ReservedService();
            reservationList.ForEach(delegate (Reservation res) {
                res.petReservationList.ForEach(delegate (PetReservation pres) {
                    List<ReservedService> ser = rs.listReservedService(pres.petResNumber);
                    if (ser.Count != 0) {
                        pres.serviceList = rs.listReservedService(pres.petResNumber);
                    }
                });
            });

            return reservationList;
        }

        public static List<Reservation> listUpcomingReservations(DateTime reservationDate)
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listUpcomingReservationsDB(reservationDate);

            List<Reservation> reservationList = fillReservationModified(ds);

            ReservedService rs = new ReservedService();
            reservationList.ForEach(delegate (Reservation res) {
                res.petReservationList.ForEach(delegate (PetReservation pres) {
                    List<ReservedService> ser = rs.listReservedService(pres.petResNumber);
                    if (ser.Count != 0) {
                        pres.serviceList = rs.listReservedService(pres.petResNumber);
                    }
                });
            });

            return reservationList;
        }


        public static int updateReservation(int resNum, DateTime startDate, DateTime endDate, int petNumber, int runNumber)
        {
            ReservationDB db = new ReservationDB();
            Search check = new Search();

            if (check.validateReservationNumber(resNum))
            {

                if (check.validatePetNumber(petNumber) == false)
                {
                    return -10;
                }
                //return -11 Start Date In the past
                if (startDate < DateTime.Now)
                {
                    return -11;
                }
                //return -12 Start Date After end date
                if (startDate > endDate)
                {
                    return -12;
                }
                //return -13 Dog has reservation for all or part of period
                if (check.validateConflictingReservations(petNumber, startDate, endDate) == false)
                {
                    return -13;
                }
                //return -14 No Run Available

                if (Run.checkRunAvailability(startDate, endDate, check.getPetSize(petNumber)) <= 0)
                    return -14;
                //return -15 Insert Failed
                if (Reservation.updateReservation(resNum, startDate, endDate, petNumber, runNumber) == 1)
                    return 1;
                else
                    return -15;


                //return -1 if expired or missing Vaccinations
                int count = PetVaccination.checkVaccinations(petNumber, endDate);
                if (count == -1)
                    return -1;
                else
                    return 0;

            }else
                    return -2; 


            }


        public static List<Reservation> fillReservation(DataSet ds )
        {
            Reservation res = new Reservation();
            List<Reservation> resList = new List<Reservation>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {

                try
                {
                    if (i != 0 && (Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]) == Convert.ToInt32(ds.Tables[0].Rows[i - 1]["RESERVATION_NUMBER"]))) {
                        res.petReservationList.Add(new PetReservation());
                        res.petReservationList[res.petReservationList.Count - 1].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString());
                        // res.petReservationList[i].run.runNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());

                    }
                    else
                    {
                        //Retrieve pet info , owner # , reservation detail 
                        res.reservationNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]);
                        res.startDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_START_DATE"].ToString());
                        res.endDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_END_DATE"].ToString());
                        res.petReservationList.Add(new PetReservation());
                        res.ownerNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["OWNER_NUMBER"].ToString());
                        res.petReservationList[res.petReservationList.Count - 1].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString());
                        //res.petReservationList[i].run.runNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());
                        resList.Add(res);
                        res = new Reservation();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());

                }

            }
            return resList;


        }

        public static List<Reservation> fillReservationModified(DataSet ds)
        {// this was modified to get pet info and run number
            Reservation res = new Reservation();
            List<Reservation> resList = new List<Reservation>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                try
                {
                    if (i != 0 && (Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]) == Convert.ToInt32(ds.Tables[0].Rows[i - 1]["RESERVATION_NUMBER"])))
                    {
                        res.petReservationList.Add(new PetReservation());
                        res.petReservationList[res.petReservationList.Count - 1].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString());
                        try
                        {
                            Run run = new HawkeyehvkBLL.Run();
                            run.runNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());
                            res.petReservationList[res.petReservationList.Count - 1].run = run;
                        }
                        catch
                        {
                            //run was null
                        }

                        Pet pet = new Pet();
                        pet.petNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString());
                        pet.name = ds.Tables[0].Rows[i]["PET_NAME"].ToString();
                        pet.gender = ds.Tables[0].Rows[i]["PET_GENDER"].ToString().ToCharArray()[0];
                        pet.isFixed = ds.Tables[0].Rows[i]["PET_FIXED"].ToString().ToCharArray()[0];
                        pet.breed = ds.Tables[0].Rows[i]["PET_BREED"].ToString();
                        try
                        {
                            pet.birthday = DateTime.Parse(ds.Tables[0].Rows[i]["PET_BIRTHDATE"].ToString());
                        }
                        catch
                        {
                            //birthday was null
                        }
                        pet.size = ds.Tables[0].Rows[i]["DOG_SIZE"].ToString().ToCharArray()[0];
                        pet.notes = ds.Tables[0].Rows[i]["SPECIAL_NOTES"].ToString();
                        res.petReservationList[res.petReservationList.Count - 1].pet = pet;
                        res.petReservationList[res.petReservationList.Count - 1].petResNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["PET_RES_NUMBER"]);

                    }
                    else
                    {
                         res = new Reservation();
                        //Retrieve pet info , owner # , reservation detail 
                        res.reservationNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]);
                        res.startDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_START_DATE"].ToString());
                        res.endDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_END_DATE"].ToString());
                        res.ownerNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["OWNER_NUMBER"].ToString());
                        res.petReservationList.Add(new PetReservation());
                       // int petNum = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString()); 
                        //res.petReservationList[res.petReservationList.Count - 1].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString());
                        Run run = new HawkeyehvkBLL.Run();
                        try
                        {
                            run.runNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());
                            res.petReservationList[res.petReservationList.Count - 1].run = run;
                        }
                        catch {
                            //run was null
                        }
                        
                        Pet pet = new Pet();
                        pet.petNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString());
                        pet.name = ds.Tables[0].Rows[i]["PET_NAME"].ToString();
                        pet.gender = ds.Tables[0].Rows[i]["PET_GENDER"].ToString().ToCharArray()[0];
                        pet.isFixed = ds.Tables[0].Rows[i]["PET_FIXED"].ToString().ToCharArray()[0];
                        pet.breed = ds.Tables[0].Rows[i]["PET_BREED"].ToString();
                        try {
                            pet.birthday = DateTime.Parse(ds.Tables[0].Rows[i]["PET_BIRTHDATE"].ToString());
                        } catch {
                            //birthday was null
                        }
                        pet.size = ds.Tables[0].Rows[i]["DOG_SIZE"].ToString().ToCharArray()[0];
                        pet.notes = ds.Tables[0].Rows[i]["SPECIAL_NOTES"].ToString();
                        res.petReservationList[res.petReservationList.Count - 1].pet = pet;
                        res.petReservationList[res.petReservationList.Count - 1].petResNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["PET_RES_NUMBER"]); 
                        resList.Add(res);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());

                }

            }
            return resList;


        }

        public static List<Reservation> fillReservationDetail(DataSet ds)
        {
            Reservation res = new Reservation();
            List<Reservation> resList = new List<Reservation>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                try

                {

                    if (i != 0 && (Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]) == Convert.ToInt32(ds.Tables[0].Rows[i - 1]["RESERVATION_NUMBER"])))
                    {
                        int tempIndex = res.petReservationList.Count - 1;
                        res.petReservationList.Add(new PetReservation());
                        res.petReservationList[tempIndex].pet.name = ds.Tables[0].Rows[i]["PET_NAME"].ToString();
                        res.petReservationList[tempIndex].pet.breed = ds.Tables[0].Rows[i]["pet_breed"].ToString();
                        res.petReservationList[tempIndex].pet.size = Convert.ToChar(ds.Tables[0].Rows[i]["dog_size"].ToString());
                        res.petReservationList[tempIndex].pet.isFixed = Convert.ToChar(ds.Tables[i].Rows[i]["pet_fixed"]);
                        res.petReservationList[tempIndex].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NAME"].ToString());
                        res.petReservationList[tempIndex].pet.notes = ds.Tables[0].Rows[i]["SPECIAL_NOTES"].ToString();
                        res.petReservationList[tempIndex].pet.gender = Convert.ToChar(ds.Tables[0].Rows[i]["PET_GENDER"].ToString());
                        res.petReservationList[tempIndex].run.runNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());
                    }
                    else
                    {
                        //Retrieve pet info , owner # , reservation detail 
                        res.reservationNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]);
                        res.startDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_START_DATE"].ToString());
                        res.endDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_END_DATE"].ToString());
                        res.petReservationList.Add(new PetReservation());
                        res.ownerNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["OWN_OWNER_NUMBER"].ToString());
                        res.petReservationList[i].pet.name = ds.Tables[0].Rows[i]["PET_NAME"].ToString();
                        res.petReservationList[i].pet.breed = ds.Tables[0].Rows[i]["pet_breed"].ToString();
                        res.petReservationList[i].pet.size = Convert.ToChar(ds.Tables[0].Rows[i]["dog_size"].ToString());
                        res.petReservationList[i].pet.isFixed = Convert.ToChar(ds.Tables[i].Rows[i]["pet_fixed"]);
                        res.petReservationList[i].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NAME"].ToString());
                        res.petReservationList[i].pet.notes = ds.Tables[0].Rows[i]["SPECIAL_NOTES"].ToString();
                        res.petReservationList[i].pet.gender = Convert.ToChar(ds.Tables[0].Rows[i]["PET_GENDER"].ToString());
                        res.petReservationList[i].run.runNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());
                        resList.Add(res);
                        res = new Reservation();
                    }

                      

                }
                catch
                {
                    Console.Write("Error");
                }

            }
            return resList;


        }
        public static List<Run> listAvailableRuns(DateTime start, DateTime end)
        {
            List<Run> runs = new List<Run>();
            ReservationDB db = new ReservationDB();
            foreach (DataRow row in db.listAvailableRunsDB(start, end).Tables["hvk_runsAvail"].Rows)
            {
                runs.Add(convertToRun(row));
            }
            return runs;
        }
        

        private static Run convertToRun(DataRow row)
        {
            Run run = new HawkeyehvkBLL.Run();
            run.runNumber = Convert.ToInt32(row[0]);
            if (((String)row[1]).ToUpper().Equals(("L")))
            {
                run.size = 'L';
            }
            else
            {
                run.size = 'R';
            }
            return run;
        }
        public static int addReservation(int petNumber, DateTime startDate, DateTime endDate)
        {
            Search check = new Search();
           
            //return -10 Invalid Pet Number
            if(check.validatePetNumber(petNumber) == false)
            {
                return -10;
            }
            //return -11 Start Date In the past
            if(startDate < DateTime.Now)
            {
                return -11;
            }
            //return -12 Start Date After end date
            if(startDate > endDate)
            {
                return -12;
            }
            //return -13 Dog has reservation for all or part of period
            if(check.validateConflictingReservations(petNumber, startDate, endDate) == false)
            {
                return -13;
            }
            //return -14 No Run Available
           
            if (Run.checkRunAvailability(startDate, endDate, check.getPetSize(petNumber)) <= 0)
                return -14;
            //return -15 Insert Failed
            
                ReservationDB reservation = new ReservationDB();
            if (reservation.addReservation(petNumber, startDate, endDate) == -1)
                return -15;
          

            //return -1 if expired or missing Vaccinations
            int count = PetVaccination.checkVaccinations(petNumber, endDate);
            if (count == -1)
                return -1;
            else
                return 0;
        }

        public static int addToReservation(int reservationNumber, int petNumber)
        {
            ReservationDB db = new ReservationDB();
            Search search = new Search();
            try
            {

                if (!search.validatePetNumber(petNumber))
                {
                    return -1;
                }
                else if (!search.validateReservationNumber(reservationNumber))
                {
                    return -2;
                }
                else if (Search.validateOwnerForPet(reservationNumber, petNumber) < 0)
                    return -4;
                else if (search.validateReservationForPet(petNumber, reservationNumber))
                {
                    return -3;
                }
               

                // add discount if we are adding a third pet reservation
                int count = PetReservation.listPetRes(reservationNumber).Count;
                if (count == 2)
                {
                    Discount.addReservationDiscount(2,reservationNumber);
                }

                db.addToReservationDB(reservationNumber, petNumber);
                    return 1;
             



            }
            catch
            {
                //Exception msg goes here 
                return -4;
            }


        }
        private static bool isReservationActive(int reservationNumber) {
            bool returned = false;
            listActiveReservations().ForEach(delegate (Reservation res) {
                if (res.reservationNumber == reservationNumber) {
                   returned= true;
                }
            });
            return returned;
        }
        public static int cancelReservation(int reservationNumber)
        {
            Search search = new HawkeyehvkBLL.Search();
            // check reservation number
            if (!search.validateReservationNumber(reservationNumber))// check reservation number
            {
                return 1;
            }
            else if (isReservationActive(reservationNumber))
            {
                return 4;
            }
            else
            {
                return ReservationDB.cancelReservationDB(reservationNumber);
            }
        }
        public static int deleteDogFromReservation(int reservationNumber, int petNumber)
        {



            Search search = new HawkeyehvkBLL.Search();

            if (!search.validateReservationNumber(reservationNumber))// check reservation number
            {
                return 1;
            }
            else if (!search.validatePetNumber(petNumber))// check pet number
            {
                return 2;
            }
            else if (!ReservationDB.isDogInReservation(reservationNumber, petNumber))// check that dog is in reservation
            {
                return 3;
            }
            else if (isReservationActive(reservationNumber))//check is res is active
            {
                return 4;
            }
            else
            {
                // before running check if the reservation is going from 3 to 2 dogs in order to remove the discount
                int count = PetReservation.listPetRes(reservationNumber).Count;
                if (count==3) {
                    Discount.deleteReservationDiscount(2,reservationNumber);
                }
                return ReservationDB.deleteDogFromReservationDB(reservationNumber, petNumber);
            }
        }
        public static int changeReservation(int reservationNumber, DateTime startDate, DateTime endDate)
        {
            Search search = new HawkeyehvkBLL.Search();
            if (!search.validateReservationNumber(reservationNumber))// check reservation number
            {
                return 1;
            } else if (startDate > endDate) { // check that dates are in a valid order
                return 2;
            }
            List<Pet> pets = Pet.listPetsByReservation(reservationNumber);
            int largeDogs = 0;
            foreach(Pet pet in pets) {
                if (pet.size == 'L')
                    largeDogs++;
            }
            // check that there are available runs

            if (Run.checkRunAvailability(startDate, endDate, 'L') < largeDogs ||
                Run.checkRunAvailability(startDate, endDate, 'R') < pets.Count) {
                return 3; 
            }
            // change the res dates
            try
            {
                ReservationDB db = new ReservationDB();
                db.changeReservationDB(reservationNumber, startDate, endDate);
            } catch
            {
                return 4;
            }
            return 0;
        }



        public static int checkVaccinations(int petNumber, DateTime byDate)
        {
            // check pet number
            return 0;
        }



    }
}
