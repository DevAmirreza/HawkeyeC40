using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL; 
namespace AYadollahibastani_C40A02
{
    public partial class managePet : System.Web.UI.Page
    {
       // Hvk.PetReservation newPetReservation = new Hvk.PetReservation(new Hvk.PetFood(), new List<Hvk.Medication>(), new List<Hvk.ReservationService>(), new Hvk.Run(), new Hvk.Pet()); 
        Owner newOwner = null ;
        

         //pet index 0 - handle multiple pets from a list using this index
         int x = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            editDisplay.Visible = true;
            addDisplay.Visible = false;
            viewDisplay.Visible = false;
            
            if ((UserType)Session["UserType"] == UserType.Owner)
            {
                newOwner = (Owner)Session["owner"];
            }
            else
            {
                newOwner = (Owner)Session["SelectedOwner"];
            }

            //newOwner = (Owner)Session["owner"];
            //if(Session["PetID"] != null)
            //x = (int)Session["PetID"];
            gvPetVaccination.GridLines = GridLines.None;
          
            if(newOwner.petList.Count != 0 && Session["PetID"] == null)
            {
                Session["PetID"] = newOwner.petList[0].petNumber;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["owner"] == null)
            {
                Response.Redirect("~/ManageCustomer.aspx");
                //newOwner = new Owner();
            }
            else if (newOwner.petList.Count == 0)
            {

            }
            else { 
                //Application master = Master as Application;
                //newOwner = master.owner;
                //Session["PetId"] = newOwner.petList[0].petNumber;
                //Session["PetId"] = 7;
                

                if (!IsPostBack)
                    loadData();
            }


            //loads data from objects into the fields
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

            int petIndex ;
            if (Session["SelectedPet"] != null)
            {
                petIndex = (int)Session["SelectedPet"];
            }
            else
                petIndex = 0;

            if (newOwner.petList.Count != 0)
            {
                Pet currentPetSelected = newOwner.petList[petIndex];
                gvPetVaccination.DataBind();

                txtPetName.Text = currentPetSelected.name;
                txtBreed.Text = currentPetSelected.breed;
                txtSpecialNote.InnerText = currentPetSelected.notes;

                if (!IsPostBack)
                {
                    rdlPetSize.Items.FindByValue(selectSize(currentPetSelected.size)).Selected = true;
                    rdGender.SelectedIndex = rdGender.Items.IndexOf(rdGender.Items.FindByValue(currentPetSelected.gender.ToString()));

                }
                txtSpecialNote.Value = currentPetSelected.notes;
                //if (!IsPostBack)
                //{
                //    foreach (var item in currentPetSelected.vaccinationList)
                //    {
                //        ddlVacc.Items.Add(item.vaccination.name);
                //     //****   ((TextBox)UCexpDate.FindControl("txtDate")).Text = item.vaccination..ToShortDateString();
                //    }
                //}
            }
        }

        protected void updateFields()
        {
            if (Session["PetID"] == null)
            {
                newOwner.petList.Add(new Pet(0, Request.Form[txtPetName.UniqueID], 'n', 'f'));
                Session["PetID"] = newOwner.petList.Count - 1; 
            } else {
                int tempindex = (int)Session["PetID"]; 
            newOwner.petList[0].name  = Request.Form[txtPetName.UniqueID];
            newOwner.petList[0].breed = Request.Form[txtBreed.UniqueID];
            newOwner.petList[0].notes = Request.Form[txtSpecialNote.UniqueID];
        }
           
                try
                {
                if (Session["reservation"] != null)
                {
                    // ((Hvk.HvkPetReservation)Session["reservation"]).pet[((int)Session["PetID"])].pet.name = newOwner.petList[(int)Session["PetID"]].name;
                    //setting reservation pet name 
                }
                    newOwner.petList[(int)Session["PetID"]].notes = Request.Form[txtSpecialNote.UniqueID];
                   

                }
                catch
                {
                    //ignore
                }


            //Populating vaccination drop down from db goes here with sql data source

          



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

               changeState(true); 

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
            
            //adding vaccines to object here *****
        }
    }
}