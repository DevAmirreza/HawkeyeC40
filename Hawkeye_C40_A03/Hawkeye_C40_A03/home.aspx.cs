using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02
{
    public partial class homePage : System.Web.UI.Page
    {
        Hvk.HvkPetReservation newReservation = null;
        Hvk.Owner newOwner = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            bool clerk = true;
            changeState(false);
            searchPanel.Visible = false;
            newReservation = ((Hvk.HvkPetReservation)Session["reservation"]);
            newOwner = (Hvk.Owner)Session["owner"];

            if (clerk == false)
            {
                clerkPanel.Visible = false;
            }
            else
            {
                customerPanel.Visible = false;
                searchPanel.Visible = true; 
            }
        }


        protected void loadReservationData() {
            lblStartTime.Text = newReservation.reservaion.startDate.ToShortDateString();
            lblEndTime.Text = newReservation.reservaion.endDate.ToShortDateString();
            if (newReservation != null) {
                foreach(var item in newReservation.pet)
                {
                    lblPetNames.Text += item.pet.name + " , ";  
                }
            } 
        }

        protected void loadData()
        {
            txtStartDate.Value = newReservation.reservaion.startDate.ToShortDateString();
            txtEndDate.Value = newReservation.reservaion.endDate.ToShortDateString();
            //loads pet list from object into dropdown
      
            if (newReservation.pet.Count() > 0)
            {
                if (IsPostBack && lbCurrentPets.Items.Count == 0)
                {
                    foreach (var item in newReservation.pet)
                    {
                        //to be fixed
                        lbCurrentPets.Items.Add(item.pet.name);
                    }
                }

                ddlFood.Items.Add(newReservation.pet[0].petFood.food.brand);
                txtFoodQuantity.Text = newReservation.pet[0].petFood.quantity;
                chWalk.Checked = true;
                txtMedication.Text = newReservation.pet[0].medication[0].name;
                txtMedDosage.Text = newReservation.pet[0].medication[0].dosage;
                txtMedicationEndDate.Value = newReservation.pet[0].medication[0].endDate.ToShortDateString();
                txtMedicationNote.Value = newReservation.pet[0].medication[0].specialNote;
                txtResNote.Value = newReservation.pet[0].note;
            }
            else
            {
                //clear fields
                ddlFood.Items.Clear();
                txtFoodQuantity.Text = "";
                chWalk.Checked = false;
                txtMedication.Text = "";
                txtMedDosage.Text = "";
                txtMedicationEndDate.Value = "";
                txtMedicationNote.Value = "";
                txtResNote.Value = "";
            }
        }

        protected void changeState(Boolean State)
        {
            txtResNote.Disabled = ((State == false) ? true : false);
            txtStartDate.Disabled = ((State == false) ? true : false);
            txtEndDate.Disabled = ((State == false) ? true : false);
            txtMedicationEndDate.Disabled = ((State == false) ? true : false);
            txtMedicationNote.Disabled = ((State == false) ? true : false);
            txtEndDate.Disabled = ((State == false) ? true : false);
            clerkPanel.Enabled = State;
        }

        protected void chReservationSelect_CheckedChanged(object sender, EventArgs e)
        {
            if(newReservation != null)
            loadData();
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
                loadReservationData();
        }

    }
}