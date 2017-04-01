using System;
using System.Collections.Generic;
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
        private Hvk.Owner newOwner = null; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["owner"] == null)
            {
                newReservation = new Hvk.HvkPetReservation();
                newOwner = new Hvk.Owner();
                setDummyData();
                //setting reservation & owner session
                Session["reservation"] = newReservation;
                Session["owner"] = newOwner; 
            }
            else
            {
                Session["reservation"] = (Hvk.HvkPetReservation)Session["reservation"];
                Session["owner"] = (Hvk.Owner)Session["owner"]; 
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

            newPet = new Hvk.Pet(100, "Puppy", 'M', 'F', "German", 'L', "He is alergic to yummy food", new List<Hvk.PetVaccination>(), "");

            //pet vaccination 
            newPet.vaccinations = new List<Hvk.PetVaccination>(); 
            Hvk.PetVaccination vacc = new Hvk.PetVaccination();
            vacc.name = "example vaccination";
            vacc.expiry = new DateTime(2017, 2, 1);
            newPet.vaccinations.Add(vacc);
            newOwner.pet.Add(newPet);

          
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