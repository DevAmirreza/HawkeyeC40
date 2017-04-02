using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02
{
    public partial class Application : System.Web.UI.MasterPage
    {
        //BLL objects goes here
        private Hvk.HvkPetReservation newReservation = null;
        private Hvk.Pet newPet = null ;
        private Hvk.Pet newPet2 = null;
        private Hvk.Pet newPet3 = null;

        private Hvk.Owner newOwner = null;

        enum UserType {
            Clerk,
            Owner
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["owner"] == null)
            {
                newReservation = new Hvk.HvkPetReservation();
                newOwner = new Hvk.Owner();

                //setting reservation & owner session

                //setDummyData reservation if its not a clerk
                if ((UserType)(Session["UserType"]) == UserType.Owner) {
                    Session["reservation"] = newReservation;
                    setDummyData();
                    Session["owner"] = newOwner;
                }
                else {
                    
                    newOwner.firstName = "Jim";
                    newOwner.lastName = "Reed";
                    newOwner.email = "Reed@hvk.ca";
                    newOwner.emgFirstName = "steve";
                    newOwner.emgLastName = "jobs";
                    newOwner.emgPhoneNumber = "432455455";
                    newOwner.address.city = "Chelsea";
                    newOwner.address.province = 'Q';
                    newOwner.address.street = "123 scott road";
                    newOwner.address.postalCode = "J9b 2p8";
                    newOwner.phone = "4385566065";
                    Session["owner"] = newOwner;
                }
            }
            else
            {
                if ((UserType)(Session["UserType"]) == UserType.Owner) {
                    Session["reservation"] = (Hvk.HvkPetReservation)Session["reservation"];
                }
                Session["owner"] = (Hvk.Owner)Session["owner"]; 
            }

            //set Nav based on type of user
            btnNav1.Text = "Home";
            btnNav3.Text = "Profile";
            if ((UserType)(Session["UserType"]) == UserType.Owner) {
                btnNav2.Text = "Pets";
            }
            else {
                btnNav2.Text = "Owners";
            }

        }
        public void navClick(object sender, EventArgs e) {
            LinkButton btnLink = (LinkButton)sender;
            int clickedItem = CharUnicodeInfo.GetDecimalDigitValue(btnLink.ID[btnLink.ID.Length - 1]);
            switch (clickedItem) {
                case 1://new customer
                    Response.Redirect("home.aspx");
                    break;
                case 2: // reservations
                    
                        if ((UserType)(Session["UserType"]) == UserType.Owner) {
                            Response.Redirect("managePet.aspx");
                        }
                        else {
                            Response.Redirect("managePet.aspx");// this will be changed when Owners.aspx is added
                        }
                    
                    break;
                case 3:
                    Response.Redirect("manageCustomer.aspx");
                    break;
            }
        }

        protected void setDummyData()
        {
            /*
             Setting Owner Information Here 
             */
            newOwner.firstName = "Amirreza";
            newOwner.lastName = "Yadollahi";
            newOwner.email = "atnniya@gmail.com";
            newOwner.emgFirstName = "steve";
            newOwner.emgLastName = "jobs";
            newOwner.emgPhoneNumber = "432455455";
            newOwner.address.city = "Gatineau";
            newOwner.address.province = 'Q';
            newOwner.address.street = "115 rue de st-joseph";
            newOwner.address.postalCode = "J4B0B9";
            newOwner.phone = "4385566065";

            //drop downs get populated automaticly just add more pets here
            newPet = new Hvk.Pet(100, "Puppy", 'M', 'F', "German", 'L', "He is alergic to yummy food", new List<Hvk.PetVaccination>(), "");
            newPet2 = new Hvk.Pet(101, "Steve", 'M', 'F', "German", 'L', "He is alergic to yummy food", new List<Hvk.PetVaccination>(), "");
            newPet3 = new Hvk.Pet(102, "BarB", 'M', 'F', "German", 'L', "He is alergic to yummy food", new List<Hvk.PetVaccination>(), "");
            newOwner.pet.Add(newPet);
            newOwner.pet.Add(newPet2);
            newOwner.pet.Add(newPet3);



            //pet vaccination 
            newPet.vaccinations = new List<Hvk.PetVaccination>(); 
            Hvk.PetVaccination vacc = new Hvk.PetVaccination();
            vacc.name = "example vaccination";
            vacc.expiry = new DateTime(2017, 2, 1);
            newPet.vaccinations.Add(vacc);
           
          
            newReservation.pet = new List<Hvk.PetReservation>();
            newReservation.pet.Add(new Hvk.PetReservation());
            newPet = new Hvk.Pet(100, "Puppy", 'M', 'F', "German", 'L', "He is alergic to yummy food", new List<Hvk.PetVaccination>(), "");
            newReservation.pet = new List<Hvk.PetReservation>();
            Hvk.PetReservation newPetReservation = new Hvk.PetReservation(new Hvk.PetFood(2, "250g", new Hvk.Food(100, "Ralston Purina")), new List<Hvk.Medication>(), new List<Hvk.ReservationService>(), new Hvk.Run(),newPet);
            newReservation.pet.Add(newPetReservation);
            newPet.vaccinations = new List<Hvk.PetVaccination>();

            //vaccination 
            vacc.name = "example vaccination";
            vacc.expiry = new DateTime(2017, 2, 1);
            newPet.vaccinations.Add(vacc);
            newOwner.pet.Add(newPet);
           
            //Setting reservation 
            newReservation.reservaion.startDate = new DateTime(2017, 4, 4);
            newReservation.reservaion.endDate = new DateTime(2017, 4, 19);
            newReservation.pet[0].runAssigned = new Hvk.Run(100, 'L', 'c', 'D', 0);


            //set service
            newReservation.pet[0].service = new List<Hvk.ReservationService>();
            Hvk.ReservationService service = new Hvk.ReservationService();
            service.frequency = 2;
            service.description = "Daily Walk";
            newReservation.pet[0].service.Add(service);
        }



    }
}