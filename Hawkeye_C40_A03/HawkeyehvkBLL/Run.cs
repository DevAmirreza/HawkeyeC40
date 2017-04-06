using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class Run
    {
        public int runNumber { get; set; }

        public char size { get; set; }

        public bool isCovered { get; set; }

        public char location { get; set; }

        public char status { get; set; }

        public Run()
        {
            this.runNumber = -1;
            this.size = 'S';
            this.isCovered = false;
            this.location = 'B';
            this.status = 'M'; //Maintenance
        }

        public Run(int number, char size, bool isCovered, char location, char status)
        {
            this.runNumber = number;
            this.size = size;
            this.isCovered = isCovered;
            this.location = location;
            this.status = status;
        }

        public static int getNumAvailableRuns(DateTime start, DateTime end) {
            RunDB db = new RunDB();
            return db.getNumAvailableRunsDB(start, end);
        }

        public static int getNumAvailableLargeRuns(DateTime start, DateTime end) {
            RunDB db = new RunDB();
            return db.getNumAvailableLargeRunsDB(start, end);
        }

        public static int checkRunAvailability(DateTime startDate, DateTime endDate, char runSize) {
            if (startDate > endDate)
            {
                return -1;
            }
           
            int count = -1;
            if (Char.ToUpper(runSize) == 'L') {
                count = Run.getNumAvailableLargeRuns(startDate, endDate);
            }
            else {
                count = Run.getNumAvailableRuns(startDate, endDate);
            }
            return count;
        }

        public static int updateRunStatus(int runNum, char status) {
            RunDB db = new RunDB();
            if (db.updateRunStatusDB(runNum, status) != 0) {
                return 1;
            }
            return 0;
        }
    }
}
