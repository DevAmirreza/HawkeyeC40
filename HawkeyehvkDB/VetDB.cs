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
    public class VetDB
    {
        public DataSet listVetsDB()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select vet_name from HVK_veterinarian";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vetDataSet");
            da.Fill(ds, "hvk_veterinarian");
            return ds;
        }

        public DataSet getVetByOwnerNumDB(int ownerNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select vet_name from HVK_veterinarian, HVK_owner where owner_number = :ownerNum and vet_vet_number = vet_number";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("ownerNum", ownerNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vetDataSet");
            da.Fill(ds, "hvk_veterinarian");
            return ds;
        }

        public void addVetDB(string vetName, string vetPhone, string vetStreet, string vetCity, string vetProvince, string vetPostalCode)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"INSERT INTO HVK_VETERINARIAN(vet_number,
                            vet_name,
                            vet_phone,
                            vet_street,
                            vet_city, 
                            vet_province, 
                            vet_postal_code)
                            VALUES(HVK_VET_SEQ.NEXTVAL, 
                            :vetName, 
                            :vetPhone,
                            :vetStreet,
                            :vetCity,
                            :vetProvince,
                            :vetPostalCode 
                            ";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("vetName", vetName);
            cmd.Parameters.Add("vetPhone", vetPhone);
            cmd.Parameters.Add("vetStreet", vetStreet);
            cmd.Parameters.Add("vetCity", vetCity);
            cmd.Parameters.Add("vetProvince", vetProvince);
            cmd.Parameters.Add("vetPostalCode", vetPostalCode);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.InsertCommand = cmd;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                con.Close();
            }

        }

    }
}
