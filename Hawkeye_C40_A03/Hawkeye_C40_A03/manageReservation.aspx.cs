using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02
{
    public partial class manageReservation : System.Web.UI.Page
    {
        Hvk.HvkPetReservation newReservation = null;
        Hvk.Owner newOwner = null ;
        const int X = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Switch To Clerk - Default : Customer
            Boolean clerk = false;
            ddlChooseRun.Visible = false;
            lblChooseRun.Visible = false;
            searchPanel.Visible = false;
            noReservationPanel.Visible = false;
            changeState(false);
            newOwner = (Hvk.Owner)Session["owner"];
            newReservation = (Hvk.HvkPetReservation)Session["reservation"];

            if (Session["owner"] == null && clerk)
            {
                searchPanel.Visible = true;
                editPanel.Visible = false;
            }
         
            if (clerk)
            {
                ddlChooseRun.Visible = true;
                lblChooseRun.Visible = true;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

            if (Session["reservation"] == null)
            {
                newReservation = new Hvk.HvkPetReservation();
            }
            else
            {
                newReservation = ((Hvk.HvkPetReservation)Session["reservation"]);
                newOwner = (Hvk.Owner)Session["owner"];
            }

            if (!IsPostBack)
                loadData();
        }


        //updating fields need to be change inroder to work with db
        protected void updateFields()
        {
            newReservation.pet = new List<Hvk.PetReservation>();
            newReservation.pet.Add(new Hvk.PetReservation());
            newReservation.pet[X].pet = new Hvk.Pet();
            newReservation.pet[X].pet.name = lbCurrentPets.Items[X].Text; 
            newReservation.pet[X].name = lbCurrentPets.Items[X].Text;
            newReservation.pet[X].note = Request.Form[txtResNote.UniqueID];
            newReservation.pet[X].medication = new List<Hvk.Medication>();
            newReservation.pet[X].medication.Add(new Hvk.Medication(100, Request.Form[txtMedication.UniqueID], Request.Form[txtMedDosage.UniqueID], Request.Form[txtMedicationNote.UniqueID], new DateTime(2017, 4, 3)));
            // newReservation.reservaion.startDate = DateTime.ParseExact(Request.Form[txtStartDate.ID],"ddmmyyyy", CultureInfo.InvariantCulture);
            //newReservation.reservaion.endDate = DateTime.ParseExact(Request.Form[txtEndDate.ID],"dd/mm/yy", CultureInfo.InvariantCulture);
            //run info - get it from db
            newReservation.pet[0].runAssigned = new Hvk.Run(100, 'L', 'c', 'D', 0);
            newReservation.pet[0].service = new List<Hvk.ReservationService>();
            Hvk.ReservationService service = new Hvk.ReservationService();
            newReservation.pet[0].service.Add(service);
            Hvk.PetFood food = new Hvk.PetFood(0, Request.Form[txtFoodQuantity.UniqueID], new Hvk.Food(100, ""));
            newReservation.pet[0].petFood = food;
        }



        protected void changeState(Boolean State)
        {
            txtResNote.Disabled = ((State == false) ? true : false);
            txtStartDate.Disabled = ((State == false) ? true : false);
            txtEndDate.Disabled = ((State == false) ? true : false);
            txtMedicationEndDate.Disabled = ((State == false) ? true : false);
            txtMedicationNote.Disabled = ((State == false) ? true : false);
            txtEndDate.Disabled = ((State == false) ? true : false);
            reservationPanel.Enabled = State;
        }

        protected void loadData()
        {            
                txtStartDate.Value = newReservation.reservaion.startDate.ToShortDateString();
                txtEndDate.Value = newReservation.reservaion.endDate.ToShortDateString();
            //loads pet list from object into dropdown
            if (!IsPostBack)
            {
                //Hawkeye : newOwner.reservation.petReservation.pet 
                foreach (var item in newOwner.pet)
                {
                    ddlChoosePet.Items.Add(item.name);
                    ddlChoosePet.Items[ddlChoosePet.Items.Count - 1].Value = item.petNumber.ToString(); 
                }
            }
            // Multiple Pet - to be implemented here
            //Hawkeye : newOwner.reservation.petReservation.pet.Count() 
                if (newReservation.pet.Count() > 0)
            {
                if (!IsPostBack)
                {
                    foreach (var item in newReservation.pet)
                    {
                        lbCurrentPets.Items.Add(item.pet.name);
                        lbCurrentPets.Items[lbCurrentPets.Items.Count - 1].Value = item.petNumber.ToString();
                        //newReservation.pet.Add();
                    }
                }

                chWalk.Checked = true;
                txtResNote.Value = newReservation.pet[0].note;
            }
            else
            {
                //clear fields
                chWalk.Checked = false;
                txtResNote.Value = "";
            }
        }//load info into fields 


        protected Boolean validateReservationPetList() {
            //verify if list is already on reservation pet list 
            Boolean validation = false;


            for (int i = 0; i < lbCurrentPets.Items.Count; i++)
            {
                if (lbCurrentPets.Items[i].Text.Equals(ddlChoosePet.SelectedItem.Text))
                {
                    //Pet already exists in list
                    valPetExists.IsValid = false;
                    validation = true;
                    i = lbCurrentPets.Items.Count;
                }
            }

            if (validation == false && ddlChoosePet.SelectedIndex != 0)
            {
                lbCurrentPets.Items.Add(ddlChoosePet.SelectedItem.Text);
                validation = true; 
            }
         

            return validation; 

        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            changeState(true);
            btnBook.Text = "Save";
        }

        protected void btnNewReservation_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                newReservation = new Hvk.HvkPetReservation();
                changeState(true);
                btnEdit.Visible = false;
                loadData();
                txtStartDate.Value = "";
                txtEndDate.Value = "";
                lbCurrentPets.Items.Clear();
            }

            btnBook.Text = "Book";
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
                if (txtMedication.Text != "" && txtMedDosage.Text == "")
                {
                valMedication.IsValid = false;
                changeState(true);
     
                }

            else if (txtMedication.Text == "" && txtMedDosage.Text != "")
            {
                valMedication.IsValid = false;
                changeState(true);
            }


            Boolean validationFlag = validateReservationPetList() ; 

            if (valMedication.IsValid == true && valEndDate.IsValid == true && validationFlag == false )
                {
                    btnEdit.Visible = true;
                    updateFields();
                    loadData();
                    changeState(false);
                }
            if (validationFlag)
                changeState(true);

         }


        protected void ddlChoosePet_TextChanged(object sender, EventArgs e)
        {
            changeState(validateReservationPetList()); 
        }

        protected void lbCurrentPets_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSelectedPet.Text = lbCurrentPets.SelectedItem.Text;
            ddlChoosePet.SelectedIndex = 0; 
            changeState(true);
        }
    }

}