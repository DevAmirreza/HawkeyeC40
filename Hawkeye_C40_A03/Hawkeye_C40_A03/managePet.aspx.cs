using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02
{
    public partial class managePet : System.Web.UI.Page
    {
        Hvk.PetReservation newPetReservation = new Hvk.PetReservation(new Hvk.PetFood(2, "250g", new Hvk.Food(100, "Ralston Purina")), new List<Hvk.Medication>(), new List<Hvk.ReservationService>(), new Hvk.Run(), new Hvk.Pet()); 
        Hvk.Owner newOwner = null ;
        //pet index 0 - handle multiple pets from a list using this index
        const int x = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            editDisplay.Visible = true;
            addDisplay.Visible = false;
            viewDisplay.Visible = false;
            newOwner = (Hvk.Owner)Session["owner"];
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["owner"] == null )
            {
                newOwner = new Hvk.Owner();
            }
            else
            {
                newOwner = ((Hvk.Owner)Session["owner"]);
            }

            //loads data from objects into the fields
            if (!IsPostBack)
                loadData();
        }

        protected void changeState(bool State)
        {
            editPanel.Enabled = State;
            txtExpiry.Disabled = ((State == false) ? true : false);
            txtSpecialNote.Disabled = ((State == false) ? true : false);
        }

        //Load data into form
        protected void loadData()
        {
            txtPetName.Text = newOwner.pet[x].name;
            txtBreed.Text = newOwner.pet[x].breed;

            if (!IsPostBack)
            {
                ddlFood.Items.Add(newPetReservation.petFood.food.brand);
                rdlPetSize.Items.FindByValue(selectSize(newOwner.pet[x].size)).Selected = true ;
            }
            txtSpecialNote.Value = newOwner.pet[x].note;
            if (!IsPostBack)
            {
                foreach (var item in newOwner.pet[x].vaccinations)
                {
                    ddlVacc.Items.Add(item.name);
                    txtExpiry.Value = item.expiry.ToShortDateString();
                }
            }
        }

        protected void updateFields()
        {
            newOwner.pet[x].name = Request.Form[txtPetName.UniqueID];
            newOwner.pet[x].breed = Request.Form[txtBreed.UniqueID];
            if (Session["reservation"] != null)
            {
                ((Hvk.HvkPetReservation)Session["reservation"]).pet[x].pet.name = newOwner.pet[x].name;
            }
            newOwner.pet[x].note = Request.Form[txtSpecialNote.UniqueID];
            //Populating vaccination drop down from db goes here

        }//update() 


        protected void clear()
        {
            txtBreed.Text = "";
            txtExpiry.Value = "";
            txtPetName.Text = "";
            txtSpecialNote.Value = "";
            ddlVacc.ClearSelection();
            ddlVet.ClearSelection();
            changeState(true);
        }//reset 

        protected String selectSize(char size)
        {
            String petSize = "";

            switch (size)
            {
                case 'S':
                    petSize = "Large";
                    break; 
                case 'M':
                    petSize = "Medium" ;
                    break;
                case 'L':
                    petSize = "Large";
                    break;
              
            }


          return petSize ;
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            addDisplay.Visible = false;
            editDisplay.Visible = true;
            changeState(true);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addDisplay.Visible = false;
            editDisplay.Visible = true;
            clear();
        }


        protected void btnSave_Click1(object sender, EventArgs e)
        {

            if (ddlVacc.SelectedValue != null) {
                if (txtExpiry.Value == "")
                    valVacDate.IsValid = false;
                changeState(true); 
            }

            ////is not working
            //int result = DateTime.ParseExact(txtExpiry.Value, "dd-mm-yyyy").CompareTo(DateTime.ParseExact(txtExpiry.Value, "dd-mm-yyyy").AddYears(10));
            //if (result > 0)
            //    valCheckDate.IsValid = false;

            if (valVacDate.IsValid == true  && valCheckDate.IsValid == true)
            {
                updateFields();
                loadData();
                changeState(false); 
            }
            
        }

    
    }
}