using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace HawkeyehvkDB {
    public class ReservedServiceDB {

        public int addReservedServiceDB(int petResNum, int serviceNum) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"INSERT INTO HVK_PET_RESERVATION_SERVICE VALUES
                                (
                                    NULL,
                                    :petResNum,
                                    :serviceNum
                                )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petResNum", petResNum);
            cmd.Parameters.Add("serviceNum", serviceNum);
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

        public int deleteReservedServiceDB(int petResNum, int serviceNum) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"DELETE FROM HVK_PET_RESERVATION_SERVICE
                                WHERE PR_PET_RES_NUMBER = :petResNum
                                AND SERV_SERVICE_NUMBER = :serviceNumber";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petResNum", petResNum);
            cmd.Parameters.Add("serviceNum", serviceNum);
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

        public DataSet listReservedService(int petResNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT PRS.SERVICE_FREQUENCY,
                              S.SERVICE_NUMBER,
                              S.SERVICE_DESCRIPTION
                            FROM HVK_PET_RESERVATION_SERVICE PRS
                            JOIN HVK_SERVICE S
                            ON PRS.SERV_SERVICE_NUMBER = S.SERVICE_NUMBER
                            WHERE PRS.PR_PET_RES_NUMBER = :petResNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petResNum", petResNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsListReservedService");
            da.Fill(ds, "hvk_res_service");

            return ds;


        }

    }
}
