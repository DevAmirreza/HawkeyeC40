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
    public class SearchDB
    {
        public int searchDB(String cmdStr, String parameterName, int parmNum)
        {

            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add(parameterName, parmNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            try
            {
                con.Open();
                return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch
            {
                return -1;
            }
            finally
            {
                con.Close();
            }

        }

        public int searchConflictingReservations(int petNumber, DateTime startDate, DateTime endDate)
        {// checks if pet already has vaccination on those days
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT COUNT(*)
                                FROM HVK_RESERVATION R
                                JOIN HVK_PET_RESERVATION PR
                                ON R.RESERVATION_NUMBER=PR.RES_RESERVATION_NUMBER
                                WHERE (R.RESERVATION_START_DATE BETWEEN :dateStart AND :dateEnd OR R.RESERVATION_END_DATE BETWEEN :dateStart AND :dateEnd OR R.RESERVATION_START_DATE < :dateStart  AND R.RESERVATION_END_DATE > :dateEnd)
                                AND PR.PET_PET_NUMBER = :petNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("dateStart", startDate);
            cmd.Parameters.Add("dateEnd", endDate);
            cmd.Parameters.Add("petNum", petNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            try
            {
                con.Open();
                return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.Write(e);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public char getPetSize(int petNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT DOG_SIZE FROM HVK_PET WHERE PET_NUMBER = :petNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petNum", petNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            try
            {
                con.Open();
                return Convert.ToChar(cmd.ExecuteScalar());
            }
            catch
            {
                return 'U';
            }
            finally
            {
                con.Close();
            }
        }
        public int searchPetDB(int petNumber)
        {
            string cmdStr = @"SELECT COUNT(*)
                                FROM HVK_PET
                                WHERE
                                PET_NUMBER = :PET_NUMBER
                                GROUP BY PET_NUMBER";

            return searchDB(cmdStr, "PET_NUMBER", petNumber);

        }




        public int searchOwnerDB(int ownerNumber)
        {
            string cmdStr = @"SELECT COUNT(*)
FROM HVK_OWNER
WHERE
OWNER_NUMBER = :OWNER_NUMBER
GROUP BY OWNER_NUMBER
";

            return searchDB(cmdStr, "OWNER_NUMBER", ownerNumber);

        }

        public int searchReservationDB(int resNum)
        {
            string cmdStr = @"SELECT COUNT(*)
FROM HVK_RESERVATION
WHERE
Reservation_number = :RESERVATION_NUMBER
GROUP BY RESERVATION_NUMBER
";

            return searchDB(cmdStr, "RESERVATION_NUMBER", resNum);
        }


        public int searchVaccDB(int vacNum)
        {
            string cmdStr = @"SELECT COUNT(*)
FROM HVK_VACCINATION
WHERE
VACCINATION_NUMBER = :VACCINATION_NUMBER
GROUP BY VACCINATION_NUMBER
";

            return searchDB(cmdStr, "VACCINATION_NUMBER", vacNum);
        }




        public int searchPetResDB(int petRes)
        {
            string cmdStr = @"SELECT COUNT(*)
FROM HVK_PET_RESERVATION
WHERE
PET_RES_NUMBER = :PET_RES_NUMBER
GROUP BY 
PET_RES_NUMBER
";

            return searchDB(cmdStr, "PET_RES_NUMBER", petRes);
        }


        //check if pet has already a reservation in the range of date passed in 
        public int searchReservationForPet(int petNum, int resNum)
        {

            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"select count(*) from hvk_reservation
                            where RESERVATION_NUMBER = :resnum
                            and RESERVATION_NUMBER in 
                            (select res_reservation_number 
                            from hvk_pet_reservation
                            where PET_PET_NUMBER = :petNum)";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("resNum", resNum);
            cmd.Parameters.Add("petNum", petNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            
            try {
                DataSet ds = new DataSet("AvailableRuns");
                da.Fill(ds);
                int returned = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                if (returned > 0)
                    returned = 1;
                return returned;
            }
            catch {
                return -2;
            }           
        }



        public int searchPetOwner(int resNum, int petNumber)
        {

            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT PET_PET_NUMBER
FROM HVK_PET_RESERVATION
WHERE RES_RESERVATION_NUMBER = :RES_RESERVATION_NUMBER
AND ROWNUM = 1";

            string cmdStr2 = @"SELECT OWN_OWNER_NUMBER FROM HVK_PET WHERE PET_NUMBER  = :RES_PET";
            string cmdStr3 = @"SELECT OWN_OWNER_NUMBER FROM HVK_PET WHERE PET_NUMBER  = :NEW_PET";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            OracleCommand cmd2 = new OracleCommand(cmdStr2, con);
            OracleCommand cmd3 = new OracleCommand(cmdStr3, con);

            cmd.BindByName = true;
            cmd.Parameters.Add("RES_RESERVATION_NUMBER", resNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            try
            {
                con.Open();
                int tempPetNumber = Convert.ToInt16(cmd.ExecuteScalar());

                cmd2.Parameters.Add("RES_PET", tempPetNumber);
                OracleDataAdapter da2 = new OracleDataAdapter(cmd2);
                da2.SelectCommand = cmd2;
                int realOwner = Convert.ToInt16(cmd2.ExecuteScalar());


                cmd3.Parameters.Add("NEW_PET", petNumber);
                OracleDataAdapter da3 = new OracleDataAdapter(cmd2);
                da3.SelectCommand = cmd3;
                int newOwner = Convert.ToInt16(cmd3.ExecuteScalar());

                if (newOwner != realOwner)
                    return -4;


                return 1;



            }
            catch (Exception e)
            {
                Console.Write(e);
                return -1;
            }
            finally
            {
                con.Close();
            }








        }
    }
}