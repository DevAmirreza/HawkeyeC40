using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;

namespace HawkeyehvkBLL
{
    public class Vaccination
    {
        public int vaccinationNumber { get; set; }
        public string name { get; set; }

        public Vaccination()
        {
            this.vaccinationNumber = -1;
            this.name = "";
        }

        public Vaccination(int vaccinationNumber, string name)
        {
            this.vaccinationNumber = vaccinationNumber;
            this.name = name;
        }

        public DataSet listAllVaccinations()
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.listVaccinationsDB();
            return vals;
        }
      
        public DataSet getVaccinations(int vacNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.getVaccinationDB(vacNum);
            return vals;
        }

        public static List<Vaccination> listNonPetVaccinations(int petNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            List<Vaccination> vaccList = new List<Vaccination>();
            foreach(DataRow row in vaccDB.listNonPetVaccinationsDB(petNum).Tables["hvk_vaccination"].Rows)
            {
                vaccList.Add(fillVaccination(row));
            }

            return vaccList;

        }

        private static Vaccination fillVaccination(DataRow row)
        {
            Vaccination vacc = new Vaccination();
            try
            {
                vacc.vaccinationNumber = Convert.ToInt32(row["VACCINATION_NUMBER"].ToString());
                vacc.name = row["VACCINATION_NAME"].ToString();
            }
            catch
            {
                Console.Write("Error");
            }
            return vacc;
        }

    }
}
