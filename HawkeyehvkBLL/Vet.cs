using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;

namespace HawkeyehvkBLL
{
    public class Vet
    {
        public int vetNumber { get; set; }
        public string name { get; set; }

        public string phoneNumber { get; set; }

        public Address address { get; set; }

        public Vet()
        {
            this.vetNumber = -1;
            this.name = "";
            this.phoneNumber = "";
            this.address = new Address();
        }

        public Vet(int vetNumber, string name, string phone, Address address)
        {
            this.vetNumber = vetNumber;
            this.name = name;
            this.phoneNumber = phone;
            this.address = address;
        }

        public DataSet listVets()
        {
            VetDB vDB = new VetDB();
            DataSet vals = vDB.listVetsDB();
            return vals;
        }

        public DataSet getVetByOwnerNum(int ownerNum)
        {
            VetDB vDB = new VetDB();
            DataSet vals = vDB.getVetByOwnerNumDB(ownerNum);
            return vals;
        }

        public void addVet()
        {
            VetDB vDB = new VetDB();
            vDB.addVetDB(this.name, this.phoneNumber, this.address.street, this.address.city, this.address.province, this.address.postalCode);
            
        }
    }
}
