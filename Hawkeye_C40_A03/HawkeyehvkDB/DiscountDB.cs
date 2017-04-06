using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace HawkeyehvkDB
{
    public class DiscountDB
    {
        public DataSet listReservationDiscountsDB(int reservationNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT D.DISCOUNT_NUMBER, D.DISCOUNT_DESCRIPTION, D.DISCOUNT_PERCENTAGE, D.DISCOUNT_TYPE
                            FROM HVK_RESERVATION_DISCOUNT R
                            JOIN HVK_DISCOUNT D
                            ON R.DISC_DISCOUNT_NUMBER = D.DISCOUNT_NUMBER
                            WHERE R.RES_RESERVATION_NUMBER = :RESERVATION_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER", reservationNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("resDiscountDataSet");
            da.Fill(ds, "hvk_res_discount");
            return ds;
        }

        public DataSet listPetReservationDiscountsDB(int petReservationNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT D.DISCOUNT_NUMBER, D.DISCOUNT_DESCRIPTION, D.DISCOUNT_PERCENTAGE, D.DISCOUNT_TYPE
                            FROM HVK_PET_RESERVATION_DISCOUNT R
                            JOIN HVK_DISCOUNT D
                            ON R.DISC_DISCOUNT_NUMBER = D.DISCOUNT_NUMBER
                            WHERE R.PR_PET_RES_NUMBER = :PET_RESERVATION_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("PET_RESERVATION_NUMBER", petReservationNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("petResDiscountDataSet");
            da.Fill(ds, "hvk_pet_res_discount");
            return ds;
        }

        public void addReservationDiscountDB(int discType, int resNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"INSERT INTO HVK_RESERVATION_DISCOUNT(DISC_DISCOUNT_NUMBER, RES_RESERVATION_NUMBER) VALUES(:discType, :resNum)";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("discType", discType);
            cmd.Parameters.Add("resNum", resNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.InsertCommand = cmd;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.Write(e);
            }
            finally
            {
                con.Close();
            }
        }

        public void deleteReservationDiscountDB(int discType, int resNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"DELETE FROM HVK_RESERVATION_DISCOUNT WHERE RES_RESERVATION_NUMBER = :resNum AND DISC_DISCOUNT_NUMBER = :discType";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("resNum", resNum);
            cmd.Parameters.Add("discType", discType);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.DeleteCommand = cmd;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                    
            }
            catch(Exception e)
            {
                Console.Write(e);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
