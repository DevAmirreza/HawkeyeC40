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
    public class PetReservationDB
    {
        public DataSet listPetResDB(int ReservationNumber)
        {// used for testing
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "select PET_RES_NUMBER, PET_PET_NUMBER from hvk_pet_reservation where res_reservation_number = :RESERVATIONNUMBER ";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("RESERVATIONNUMBER ", ReservationNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("petResDataSet");
            da.Fill(ds, "hvk_pet_reservation");
            return ds;
        }
    }
}
