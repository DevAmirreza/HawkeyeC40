using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkeyehvkDB
{
    public class VaccinationDB
    {
        public DataSet listVaccinationsDB()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select vaccination_name from HVK_vaccination";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }

        public DataSet listVaccinationsDB(int petNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select V.vaccination_number, V.vaccination_name, PV.vaccination_expiry_date, PV.vaccination_checked_status from HVK_vaccination V, HVK_pet_vaccination PV where PV.pet_pet_number = :pet and PV.vacc_vaccination_number = V.vaccination_number";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("pet", petNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }

        public DataSet listNonPetVaccinationsDB(int petNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT V.vaccination_number,
                                  V.vaccination_name
                                FROM HVK_vaccination V
                                WHERE V.vaccination_number NOT IN
                                  (SELECT PV.VACC_VACCINATION_NUMBER
                                  FROM HVK_PET_VACCINATION PV
                                  WHERE PV.PET_PET_NUMBER = :pet
                                  )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("pet", petNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;

        }

        public DataSet checkVaccinationsDB(int petNum, int resNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT V.vaccination_number,
                              V.vaccination_name,
                              PV.vaccination_expiry_date,
                              PV.vaccination_checked_status
                            FROM HVK_VACCINATION V,
                              HVK_PET_VACCINATION PV,
                              HVK_PET P,
                              HVK_RESERVATION R
                            WHERE R.RESERVATION_NUMBER       = :res
                            AND P.PET_NUMBER                 = :pet
                            AND P.PET_NUMBER                 = PV.PET_PET_NUMBER
                            AND (PV.VACCINATION_EXPIRY_DATE  < R.RESERVATION_END_DATE
                            OR PV.VACCINATION_CHECKED_STATUS = 'N')
                            AND PV.VACC_VACCINATION_NUMBER   = V.VACCINATION_NUMBER
                            UNION
                            SELECT V.VACCINATION_NUMBER,
                              V.VACCINATION_NAME,
                              NULL AS VACCINATION_EXPIRY_DATE,
                              NULL AS VACCINATION_CHECKED_STATUS
                            FROM HVK_VACCINATION V
                            WHERE V.VACCINATION_NAME NOT IN
                              (SELECT V.VACCINATION_NAME
                              FROM HVK_VACCINATION V,
                                HVK_PET_VACCINATION PV,
                                HVK_PET P,
                                HVK_RESERVATION R
                              WHERE R.RESERVATION_NUMBER       = :res
                              AND P.PET_NUMBER                 = :pet
                              AND P.PET_NUMBER                 = PV.PET_PET_NUMBER
                              AND (PV.VACCINATION_EXPIRY_DATE  < R.RESERVATION_END_DATE
                              OR PV.VACCINATION_CHECKED_STATUS = 'Y'
                              OR PV.VACCINATION_CHECKED_STATUS = 'N')
                              AND PV.VACC_VACCINATION_NUMBER   = V.VACCINATION_NUMBER
                              )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("res", resNum);
            cmd.Parameters.Add("pet", petNum);
            
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }

        public DataSet checkVaccinationsDB(int petNum, DateTime byDate)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT V.vaccination_number,
                              V.vaccination_name,
                              PV.vaccination_expiry_date,
                              PV.vaccination_checked_status
                            FROM HVK_VACCINATION V,
                              HVK_PET_VACCINATION PV,
                              HVK_PET P,
                              HVK_RESERVATION R
                            WHERE 
                            P.PET_NUMBER                 = :pet
                            AND P.PET_NUMBER                 = PV.PET_PET_NUMBER
                            AND (PV.VACCINATION_EXPIRY_DATE  < :byDate
                            OR PV.VACCINATION_CHECKED_STATUS = 'N')
                            AND PV.VACC_VACCINATION_NUMBER   = V.VACCINATION_NUMBER
                            UNION
                            SELECT V.VACCINATION_NUMBER,
                              V.VACCINATION_NAME,
                              NULL AS VACCINATION_EXPIRY_DATE,
                              NULL AS VACCINATION_CHECKED_STATUS
                            FROM HVK_VACCINATION V
                            WHERE V.VACCINATION_NAME NOT IN
                              (SELECT V.VACCINATION_NAME
                              FROM HVK_VACCINATION V,
                                HVK_PET_VACCINATION PV,
                                HVK_PET P,
                                HVK_RESERVATION R
                              WHERE
                              P.PET_NUMBER                 = :pet
                              AND P.PET_NUMBER                 = PV.PET_PET_NUMBER
                              AND (PV.VACCINATION_EXPIRY_DATE  < :byDate
                              OR PV.VACCINATION_CHECKED_STATUS = 'Y'
                              OR PV.VACCINATION_CHECKED_STATUS = 'N')
                              AND PV.VACC_VACCINATION_NUMBER   = V.VACCINATION_NUMBER
                              )";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("pet", petNum);
            cmd.Parameters.Add("byDate", byDate);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }

        public DataSet getVaccinationDB(int vaccNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select vaccination_name from HVK_vaccination where vaccination_number = :vacc";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("vacc", vaccNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }
        public static int addPetVaccinationDB(DateTime expiryDate, int vacNumber, int petNumber) {
            int result = 0;
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"Insert into HVK_PET_VACCINATION 
                            (VACCINATION_EXPIRY_DATE,VACC_VACCINATION_NUMBER,PET_PET_NUMBER) 
                            values (:EXPDATE,:VACNUMBER,:PETNUMBER)";
           

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("EXPDATE", expiryDate);
            cmd.Parameters.Add("VACNUMBER", vacNumber);
            cmd.Parameters.Add("PETNUMBER", petNumber);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.InsertCommand = cmd;
           

            try {
                con.Open();
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception e) {
                Console.Write(e);
                result = -1;
            }
            finally {
                con.Close();
            }

            return result;
        }
        public static int updatePetVaccinationExpiryDB(DateTime expiryDate, int vacNumber, int petNumber) {
            int result = 0;
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"update hvk_pet_vaccination
                                set VACCINATION_EXPIRY_DATE = :EXPDATE
                                where PET_PET_NUMBER = :PETNUMBER
                                and VACC_VACCINATION_NUMBER = :VACNUMBER";


            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("EXPDATE", expiryDate);
            cmd.Parameters.Add("PETNUMBER", petNumber);
            cmd.Parameters.Add("VACNUMBER", vacNumber);
            

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.UpdateCommand = cmd;


            try {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e) {
                Console.Write(e);
                result = -1;
            }
            finally {
                con.Close();
            }

            return result;
        }
        public static int updatePetVaccinationCheckedDB(char isChecked, int vacNumber, int petNumber) {
            int result = 0;
            if (isChecked!='Y' && isChecked!='N') {
                return -2;
            }
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"update hvk_pet_vaccination
                                set VACCINATION_CHECKED_STATUS = :CHECKED
                                where PET_PET_NUMBER = :PETNUMBER
                                and VACC_VACCINATION_NUMBER = :VACNUMBER";


            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("CHECKED", isChecked.ToString());
            cmd.Parameters.Add("PETNUMBER", petNumber);
            cmd.Parameters.Add("VACNUMBER", vacNumber);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.UpdateCommand = cmd;


            try {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e) {
                Console.Write(e);
                result = -1;
            }
            finally {
                con.Close();
            }

            return result;
        }
    }
}
