using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class PetFood
    {
        public string brand { get; set; }

        public string variety { get; set; }

        public PetFood()
        {
            this.brand = "";
            this.variety = "";
        }

        public PetFood(string brand, string variety)
        {
            this.brand = brand;
            this.variety = variety;
        }

        public static DataSet listFood()
        {
            FoodDB foodDB = new FoodDB();
            DataSet vals = foodDB.listFoodDB();
            return vals;
        }

        public static DataSet listPetResFood(int petRes)
        {
            PetFoodDB pfDB = new PetFoodDB();
            DataSet vals = pfDB.listFoodByPetResDB(petRes);
            return vals;
        }
    }
}
