using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;

namespace HawkeyehvkDB
{
    public class PetDB
    {
        public DataSet listPetsDB(int ownerNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT PET_NUMBER, PET_NAME, PET_GENDER, 
                              PET_FIXED, PET_BREED, PET_BIRTHDATE, DOG_SIZE, SPECIAL_NOTES 
                              FROM HVK_PET WHERE OWN_OWNER_NUMBER = :OWNER_NUMBER 
                              ORDER BY PET_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER", ownerNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("petDataSet");
            da.Fill(ds, "hvk_pet");
            return ds;
        }
        public DataSet getOnePetDB(int petNum) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT PET_NUMBER, PET_NAME, PET_GENDER, 
                              PET_FIXED, PET_BREED, PET_BIRTHDATE, DOG_SIZE, SPECIAL_NOTES 
                              FROM HVK_PET WHERE Pet_number = :PETNUMBER 
                              ORDER BY PET_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("PETNUMBER", petNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("petDataSet");
            da.Fill(ds, "hvk_pet");
            return ds;
        }

        public DataSet listPetsByReservationDB(int resNum) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT PET_NUMBER, PET_NAME, PET_GENDER, 
                              PET_FIXED, PET_BREED, PET_BIRTHDATE, DOG_SIZE, SPECIAL_NOTES 
                              FROM HVK_PET P
                              JOIN HVK_PET_RESERVATION PR
                              ON P.PET_NUMBER = PR.PET_PET_NUMBER
                              JOIN HVK_RESERVATION R
                              ON PR.RES_RESERVATION_NUMBER = R.RESERVATION_NUMBER
                              WHERE R.RESERVATION_NUMBER = :resNum
                              ORDER BY PET_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("resNum", resNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("petDataSet");
            da.Fill(ds, "hvk_pet");
            return ds;
        }

        public int addPetDB(string petName, char gender, char isFixed, string breed, DateTime birthday, char size, string notes) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"INSERT INTO HVK_PET VALUES
                                (
                                    HVK_PET_SEQ.NEXTVAL,
                                    :petName,
                                    :gender,
                                    :isFixed,
                                    :breed,
                                    :birthday,
                                    :size,
                                    :notes
                                )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petName", petName);
            cmd.Parameters.Add("gender", gender);
            cmd.Parameters.Add("isFixed", isFixed);
            cmd.Parameters.Add("breed", breed);
            cmd.Parameters.Add("birthday", birthday);
            cmd.Parameters.Add("size", size);
            cmd.Parameters.Add("notes", notes);
            try {
                con.Open();
                cmd.ExecuteNonQuery();
                return 0;
            } catch {
                return -1;
            } finally {
                con.Close();
            }
        }

        public int updatePetDB(int petNum, string petName, char gender, char isFixed, string breed, DateTime birthday, char size, string notes) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"UPDATE HVK_PET SET
                                PET_NAME = :petName,
                                PET_GENDER = :gender,
                                PET_FIXED = :isFixed,
                                PET_BREED = :breed,
                                PET_BIRTHDAY = :birthday,
                                DOG_SIZE = :size,
                                SPECIAL_NOTES = :notes
                                WHERE PET_NUMBER = :petNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petName", petName);
            cmd.Parameters.Add("gender", gender);
            cmd.Parameters.Add("isFixed", isFixed);
            cmd.Parameters.Add("breed", breed);
            cmd.Parameters.Add("birthday", birthday);
            cmd.Parameters.Add("size", size);
            cmd.Parameters.Add("notes", notes);
            cmd.Parameters.Add("petNum", petNum);
            try {
                con.Open();
                cmd.ExecuteNonQuery();
                return 0;
            } catch {
                return -1;
            } finally {
                con.Close();
            }
        }

        public int deletePetDB(int petNum) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"DELETE FROM HVK_PET
                                WHERE PET_NUMBER = :petNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petNum", petNum);
            try {
                con.Open();
                cmd.ExecuteNonQuery();
                return 0;
            } catch {
                return -1;
            } finally {
                con.Close();
            }
        }

        public int checkPetsInReservation(int resNum)
        {
            int returnNum = 0;
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT COUNT(PR.PET_PET_NUMBER)
                                FROM HVK_PET_RESERVATION PR
                                JOIN HVK_RESERVATION R
                                ON PR.RES_RESERVATION_NUMBER = R.RESERVATION_NUMBER
                                WHERE R.RESERVATION_NUMBER = :reservationNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("reservationNum", resNum);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

           try
            {
                con.Open();
                returnNum = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            return returnNum;
        }
    }
}
