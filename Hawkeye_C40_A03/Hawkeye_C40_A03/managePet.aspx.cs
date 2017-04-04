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
        Hvk.PetReservation newPetReservation = new Hvk.PetReservation(new Hvk.PetFood(), new List<Hvk.Medication>(), new List<Hvk.ReservationService>(), new Hvk.Run(), new Hvk.Pet()); 
        Hvk.Owner newOwner = null ;
        //pet index 0 - handle multiple pets from a list using this index
         int x = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            editDisplay.Visible = true;
            addDisplay.Visible = false;
            viewDisplay.Visible = false;
            newOwner = (Hvk.Owner)Session["owner"];
            if(Session["PetID"] != null)
            x = (int)Session["PetID"]; 

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["owner"] == null)
                newOwner = new Hvk.Owner();
            else
            {
                newOwner = ((Hvk.Owner)Session["owner"]);
              
            }
            
            if (Session["PetID"] == null)
                x = 0;
            else
                x = (int)Session["PetID"]; 



            //loads data from objects into the fields
            if (!IsPostBack)
                loadData();
        }

        protected void changeState(bool State)
        {
            editPanel.Enabled = State;
            ((TextBox)UCexpDate.FindControl("txtDate")).Enabled = State;
            txtSpecialNote.Disabled = ((State == false) ? true : false);
        }

        //Load data into form
        protected void loadData()
        {
            try
            {
                int petIndex = (int)Session["PetID"];
                txtPetName.Text = newOwner.pet[petIndex].name;
                txtBreed.Text = newOwner.pet[petIndex].breed;
                txtSpecialNote.InnerText = newOwner.pet[petIndex].note;

                if (!IsPostBack)
                {
                    rdlPetSize.Items.FindByValue(selectSize(newOwner.pet[petIndex].size)).Selected = true ;
                }
                txtSpecialNote.Value = newOwner.pet[petIndex].note;
                if (!IsPostBack)
                {
                    foreach (var item in newOwner.pet[petIndex].vaccinations)
                    {
                        ddlVacc.Items.Add(item.name);
                        ((TextBox)UCexpDate.FindControl("txtDate")).Text = item.expiry.ToShortDateString();
                    }
                }
            }
            catch
            {
                Console.Write("Exception catched in loadDataMethod"); 
            }
            
        }

        protected void updateFields()
        {
            if (Session["PetID"] == null)
            {
                newOwner.pet.Add(new Hvk.Pet(0, Request.Form[txtPetName.UniqueID], 'n', 'f'));
                Session["PetID"] = newOwner.pet.Count - 1; 
            } else {
                int tempindex = (int)Session["PetID"]; 
            newOwner.pet[tempindex].name = Request.Form[txtPetName.UniqueID];
            newOwner.pet[tempindex].breed = Request.Form[txtBreed.UniqueID];
            newOwner.pet[tempindex].note = Request.Form[txtSpecialNote.UniqueID];
        }
           
                try
                {
                if (Session["reservation"] != null)
                {
                    ((Hvk.HvkPetReservation)Session["reservation"]).pet[((int)Session["PetID"])].pet.name = newOwner.pet[(int)Session["PetID"]].name;
                }
                    newOwner.pet[(int)Session["PetID"]].note = Request.Form[txtSpecialNote.UniqueID];
                }
                catch
                {
                    //ignore
                }

           
            //Populating vaccination drop down from db goes here

        }//update() 


        protected void clear()
        {
            txtBreed.Text = "";
            ((TextBox)UCexpDate.FindControl("txtDate")).Text = "";
            txtPetName.Text = "";
            txtSpecialNote.Value = "";
            ddlVacc.ClearSelection();
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

            //To be removed kept in case it breaks something 
            //if ((ddlVacc.SelectedValue != null)&& (txtExpiry.Value == "")) {
            //    if (txtExpiry.Value == "")
            //        valVacDate.IsValid = false;
               changeState(true); 
            //}

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

        protected void btnAddVaccine_Click(object sender, EventArgs e)
        {
            ListItem item = new ListItem();
            item.Text = ddlVacc.SelectedItem.ToString();
            item.Value = UCexpDate.vacDate;
            lbCurrentVacc.Items.Add(item);
            //adding vaccines to object here *****
        }

        protected void lbCurrentVacc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlVacc.SelectedIndex = ddlVacc.Items.IndexOf(ddlVacc.Items.FindByText(lbCurrentVacc.SelectedItem.ToString()));
            ((TextBox)UCexpDate.FindControl("txtDate")).Text = lbCurrentVacc.SelectedValue;
        }
    }
}