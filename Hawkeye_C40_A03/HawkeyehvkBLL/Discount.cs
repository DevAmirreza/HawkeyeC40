using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HawkeyehvkDB;

namespace HawkeyehvkBLL
{
    public class Discount
    {

        public char type { get; set; }

        public int discountNumber { get; set; }

        public string description { get; set; }

        public decimal percentage { get; set; }

        public Discount()
        {
            this.discountNumber = -1;
            this.description = "";
            this.percentage = 0;
            this.type = 'D';
        }

        public Discount(int code, string desc, decimal percentage, char type)
        {
            this.discountNumber = code;
            this.description = desc;
            this.percentage = percentage;
            this.type = type;
        }

        private static Discount fillFromDataRow(DataRow row)
        {
            Discount discount = new Discount();
            discount.discountNumber = Convert.ToInt32(row["DISCOUNT_NUMBER"].ToString());
            discount.description = row["DISCOUNT_DESCRIPTION"].ToString();
            discount.percentage = Convert.ToDecimal(row["DISCOUNT_PERCENTAGE"].ToString());
            discount.type = Convert.ToChar(row["DISCOUNT_TYPE"].ToString());
            return discount;
        }

        public static List<Discount> listReservationDiscounts(int reservationNumber)
        {
            List<Discount> list = new List<Discount>();
            DiscountDB db = new DiscountDB();
            foreach(DataRow row in db.listReservationDiscountsDB(reservationNumber).Tables["hvk_res_discount"].Rows)
            {
                list.Add(fillFromDataRow(row));
            }
            return list;
        }

        public static List<Discount> listPetReservationDiscounts(int petReservationNumber)
        {
            List<Discount> list = new List<Discount>();
            DiscountDB db = new DiscountDB();
            foreach (DataRow row in db.listPetReservationDiscountsDB(petReservationNumber).Tables["hvk_pet_res_discount"].Rows)
            {
                list.Add(fillFromDataRow(row));
            }
            return list;
        }

        public static int addReservationDiscount(int discType, int resNum)
        {
            Search look = new Search();
            if (!look.validateReservationNumber(resNum))
            {
                return -19;
            }
            else
            {
                DiscountDB db = new DiscountDB();
                db.addReservationDiscountDB(discType, resNum);
                return 0;
            }
        }

        public static int deleteReservationDiscount(int discType, int resNum)
        {
            Search look = new Search();
            if (!look.validateReservationNumber(resNum))
            {
                return -19;
            }
            else
            {
                DiscountDB db = new DiscountDB();
                db.deleteReservationDiscountDB(discType, resNum);
                return 0;
            }
        }
    }
}
