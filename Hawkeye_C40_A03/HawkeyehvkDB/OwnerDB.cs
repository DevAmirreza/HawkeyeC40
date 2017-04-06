using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkeyehvkDB
{
    public class OwnerDB
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSet listOwnersDB()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT OWNER_NUMBER, OWNER_LAST_NAME, OWNER_FIRST_NAME, OWNER_STREET, OWNER_CITY, OWNER_PROVINCE, OWNER_POSTAL_CODE, OWNER_PHONE, OWNER_EMAIL, EMERGENCY_CONTACT_FIRST_NAME, EMERGENCY_CONTACT_LAST_NAME, EMERGENCY_CONTACT_PHONE FROM HVK_OWNER ORDER BY OWNER_LAST_NAME";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("ownerDataSet");
            da.Fill(ds, "hvk_owner");
            return ds;
        }

        public DataSet listOwnersDB(int ownerNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT OWNER_NUMBER, OWNER_LAST_NAME, OWNER_FIRST_NAME, OWNER_STREET, OWNER_CITY, OWNER_PROVINCE, OWNER_POSTAL_CODE, OWNER_PHONE, OWNER_EMAIL, EMERGENCY_CONTACT_FIRST_NAME, EMERGENCY_CONTACT_LAST_NAME, EMERGENCY_CONTACT_PHONE FROM HVK_OWNER WHERE OWNER_NUMBER = :OWNER_NUMBER ORDER BY OWNER_LAST_NAME";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER", ownerNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("ownerDataSet");
            da.Fill(ds, "hvk_owner");
            return ds;
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]

        public DataSet listOwnersDB(string email)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT OWNER_NUMBER, OWNER_LAST_NAME, OWNER_FIRST_NAME, OWNER_STREET, OWNER_CITY, OWNER_PROVINCE, OWNER_POSTAL_CODE, OWNER_PHONE, OWNER_EMAIL, EMERGENCY_CONTACT_FIRST_NAME, EMERGENCY_CONTACT_LAST_NAME, EMERGENCY_CONTACT_PHONE FROM HVK_OWNER WHERE OWNER_EMAIL = :OWNEMAIL ORDER BY OWNER_LAST_NAME";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNEMAIL", email);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("ownerDataSet");
            da.Fill(ds, "hvk_owner");
            return ds;
        }

        public void addOwnerDB(string fName, string lName, string _street, string _city, string _province, string _postalCode, string _phone, string _email, string _emerFName, string _emerLName, string _emerPhone)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"INSERT INTO HVK_OWNER
                              (
                                OWNER_NUMBER,
                                OWNER_FIRST_NAME,
                                OWNER_LAST_NAME,
                                OWNER_STREET,
                                OWNER_CITY,
                                OWNER_PROVINCE,
                                OWNER_POSTAL_CODE,
                                OWNER_PHONE,
                                OWNER_EMAIL,
                                EMERGENCY_CONTACT_FIRST_NAME,
                                EMERGENCY_CONTACT_LAST_NAME,
                                EMERGENCY_CONTACT_PHONE
                                )
                                VALUES
                                (
                                HVK_OWNER_SEQ.NEXTVAL,
                                :ownFName,
                                :ownLName,
                                :ownStreet,
                                :ownCity,
                                :ownProvince,
                                :ownPostal,
                                :ownPhone,
                                :ownEmail,
                                :emerFname,
                                :emerLName,
                                :emerPhone
                                )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("ownFName", fName);
            cmd.Parameters.Add("ownLName", lName);
            cmd.Parameters.Add("ownStreet", _street);
            cmd.Parameters.Add("ownCity", _city);
            cmd.Parameters.Add("ownProvince", _province);
            cmd.Parameters.Add("ownPostal", _postalCode);
            cmd.Parameters.Add("ownPhone", _phone);
            cmd.Parameters.Add("ownEmail", _email);
            cmd.Parameters.Add("emerFName", _emerFName);
            cmd.Parameters.Add("emerLName", _emerLName);
            cmd.Parameters.Add("emerPhone", _emerPhone);
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

        public void updateOwnerDB(int ownNum, string fName, string lName, string _street, string _city, string _province, string _postalCode, string _phone, string _email, string _emerFName, string _emerLName, string _emerPhone)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"UPDATE HVK_OWNER
                                SET OWNER_FIRST_NAME = :ownFName,
                                  OWNER_LAST_NAME = :ownLName,
                                  OWNER_STREET = :ownStreet,
                                  OWNER_CITY = :ownCity,
                                  OWNER_PROVINCE = :ownProvince,
                                  OWNER_POSTAL_CODE = :ownPostal,
                                  OWNER_PHONE = :ownPhone,
                                  OWNER_EMAIL = :ownEmail,
                                  EMERGENCY_CONTACT_FIRST_NAME = :emerFName,
                                  EMERGENCY_CONTACT_LAST_NAME = :emerLName,
                                  EMERGENCY_CONTACT_PHONE = :emerPhone
                                  WHERE OWNER_NUMBER = :ownNum";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("ownFName", fName);
            cmd.Parameters.Add("ownLName", lName);
            cmd.Parameters.Add("ownStreet", _street);
            cmd.Parameters.Add("ownCity", _city);
            cmd.Parameters.Add("ownProvince", _province);
            cmd.Parameters.Add("ownPostal", _postalCode);
            cmd.Parameters.Add("ownPhone", _phone);
            cmd.Parameters.Add("ownEmail", _email);
            cmd.Parameters.Add("emerFName", _emerFName);
            cmd.Parameters.Add("emerLName", _emerLName);
            cmd.Parameters.Add("emerPhone", _emerPhone);
            cmd.Parameters.Add("ownNum", ownNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.UpdateCommand = cmd;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
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
