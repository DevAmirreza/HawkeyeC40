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
    public class PetFoodDB
    {
        public DataSet listFoodByPetResDB(int petRes)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT F.FOOD_BRAND
                              || ' '
                              || F.FOOD_VARIETY,
                              PF.PET_FOOD_FREQUENCY,
                              PF.PET_FOOD_QUANTITY
                            FROM HVK_FOOD F
                            JOIN HVK_PET_FOOD PF
                            ON F.FOOD_NUMBER = PF.FOOD_FOOD_NUMBER
                            WHERE PF.PR_PET_RES_NUMBER = :petResNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petResNum", petRes);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
           
            DataSet ds = new DataSet("petFoodDataSet");
            da.Fill(ds, "hvk_pet_food");
            return ds;
        }
    }
}
