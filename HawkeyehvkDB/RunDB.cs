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
    public class RunDB
    {
        public int totalLargeRunsDB()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT COUNT(*) FROM HVK_RUN WHERE RUN_SIZE = 'L'";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            int returned = -1;
            try
            {
                con.Open();
                returned = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally {
                con.Close();
            }
            return returned;
        }

        public int totalRegularRunsDB()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT COUNT(*) FROM HVK_RUN WHERE RUN_SIZE = 'R'";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            
            int returned = -1;
            try
            {
                con.Open();
                returned = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                con.Close();
            }
            return returned;
        }

        public int getNumAvailableRunsDB(DateTime start, DateTime end) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT 
                              (SELECT COUNT(*) FROM HVK_RUN
                              ) - Reservations AS Available_Runs
                            FROM
                              (SELECT NVL(MAX(COUNT(*)), 0) AS Reservations
                              FROM hvk_reservation r
                              JOIN hvk_pet_reservation pr
                              ON r.RESERVATION_NUMBER = pr.RES_RESERVATION_NUMBER
                              JOIN hvk_pet p
                              ON pr.PET_PET_NUMBER = p.PET_NUMBER
                              CROSS JOIN
                                (SELECT CAST(:endDate as DATE) - level + 1 AS DAY
                                FROM dual
                                  CONNECT BY LEVEL <= CAST(:endDate as DATE) - CAST(:startDate as DATE) + 1
                                )
                              WHERE DAY BETWEEN r.RESERVATION_START_DATE AND r.RESERVATION_END_DATE
                              GROUP BY DAY)";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("endDate", end);
            cmd.Parameters.Add("startDate", start);
            int returned = -1;
            try {
                con.Open();
                returned = Convert.ToInt32(cmd.ExecuteScalar());
            } finally {
                con.Close();
            }
            return returned;
        }

        public int getNumAvailableLargeRunsDB(DateTime start, DateTime end) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT MIN((
                              (SELECT COUNT(*)
                              FROM HVK_RUN
                              WHERE RUN_SIZE = 'L'
                              ) - (
                              CASE
                                WHEN REGULAR_RESERVATIONS >
                                  (SELECT COUNT(*)
                                  FROM HVK_RUN
                                  WHERE RUN_SIZE = 'R'
                                  )
                                THEN LARGE_RESERVATIONS + (REGULAR_RESERVATIONS -
                                  (SELECT COUNT(*) FROM HVK_RUN WHERE RUN_SIZE = 'R'
                                  ))
                                ELSE LARGE_RESERVATIONS
                              END))) AS Available_Runs
                            FROM
                              (SELECT COUNT(
                                CASE p.DOG_SIZE
                                  WHEN 'L'
                                  THEN 1
                                  ELSE NULL
                                END) AS LARGE_RESERVATIONS,
                                COUNT(
                                CASE p.DOG_SIZE
                                  WHEN 'L'
                                  THEN NULL
                                  ELSE 1
                                END) AS REGULAR_RESERVATIONS
                              FROM hvk_reservation r
                              JOIN hvk_pet_reservation pr
                              ON r.RESERVATION_NUMBER = pr.RES_RESERVATION_NUMBER
                              JOIN hvk_pet p
                              ON pr.PET_PET_NUMBER = p.PET_NUMBER
                              RIGHT OUTER JOIN
                                (SELECT CAST(:endDate as DATE) - level + 1 AS DAY
                                FROM dual
                                  CONNECT BY LEVEL <= CAST(:endDate as DATE) - CAST(:startDate as DATE) + 1
                                ) Calendar
                              ON Calendar.DAY BETWEEN r.RESERVATION_START_DATE AND r.RESERVATION_END_DATE
                              GROUP BY DAY
                              )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("endDate", end);
            cmd.Parameters.Add("startDate", start);
            int returned = -1;
            try {
                con.Open();
                returned = Convert.ToInt32(cmd.ExecuteScalar());
            } finally {
                con.Close();
            }
            return returned;
        }

        public int updateRunStatusDB(int runNum, char status) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"UPDATE HVK_RUN SET
                                RUN_STATUS = :status
                                WHERE RUN_NUMBER = :runNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("status", status);
            cmd.Parameters.Add("runNum", runNum);
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
    }
}
