using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class ReservedService
    {
        public int frequency { get; set; }

        public Service service { get; set; }

        public ReservedService()
        {
            this.frequency = 0;
            this.service = new Service();
        }

        public ReservedService(int frequency, Service service)
        {
            this.frequency = frequency;
            this.service = service;
        }

        public static int addReservedService(int petResNum, int serviceNum)
        {
            ReservedServiceDB db = new ReservedServiceDB();
            if (db.addReservedServiceDB(petResNum, serviceNum) != 0)
            {
                return 1;
            }
            return 0;
        }

        public static int deleteReservedService(int petResNum, int serviceNum)
        {
            ReservedServiceDB db = new ReservedServiceDB();
            if (db.deleteReservedServiceDB(petResNum, serviceNum) != 0)
            {
                return 1;
            }
            return 0;
        }

        public List<ReservedService> listReservedService(int petResNum)
        {
            ReservedServiceDB db = new ReservedServiceDB();

            List<ReservedService> resServList = new List<ReservedService>();
            foreach (DataRow row in db.listReservedService(petResNum).Tables[0].Rows)
            {
                resServList.Add(fillReservedService(row));
            }
            return resServList;
        }

        private ReservedService fillReservedService(DataRow row)
        {
            ReservedService resServ = new ReservedService();
            try
            {
                resServ.frequency = row["SERVICE_FREQUENCY"] is DBNull ? 0 : Convert.ToInt16(row["SERVICE_FREQUENCY"].ToString());
                resServ.service.serviceNumber = Convert.ToInt16(row["SERVICE_NUMBER"].ToString());
                resServ.service.descripion = row["SERVICE_DESCRIPTION"].ToString();
            }
            catch(Exception e)
            {
                Console.Write(e);
            }

            return resServ;
        }
    }
}
