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
        int X = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Switch To Clerk - Default : Customer
            Boolean clerk = false;
            ddlChooseRun.Visible = false;
            lblChooseRun.Visible = false;
            searchPanel.Visible = false;
            noReservationPanel.Visible = false;
            if (!IsPostBack) {
                changeState(false);
            }
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
                 Session["reservation"] = newReservation;
                Session["owner"] = newOwner;
            }

            if (!IsPostBack)
                loadData();
        }


        //updating fields need to be change inroder to work with db
        protected void updateFields(int petIndex)
        {
                //newReservation.pet = new List<Hvk.PetReservation>();
                //newReservation.pet.Add(new Hvk.PetReservation());
                //newReservation.pet[petIndex].pet = new Hvk.Pet();
                newReservation.pet[petIndex].pet.name = lbCurrentPets.Items[petIndex].Text;
                newReservation.pet[petIndex].name = lbCurrentPets.Items[petIndex].Text;
                newReservation.pet[petIndex].note = Request.Form[txtResNote.UniqueID];
                newReservation.pet[petIndex].medication = new List<Hvk.Medication>();
                // newReservation.reservaion.startDate = DateTime.ParseExact(Request.Form[txtStartDate.ID],"ddmmyyyy", CultureInfo.InvariantCulture);
                //newReservation.reservaion.endDate = DateTime.ParseExact(Request.Form[txtEndDate.ID],"dd/mm/yy", CultureInfo.InvariantCulture);
                //run info - get it from db
                newReservation.pet[petIndex].runAssigned = new Hvk.Run(100, 'L', 'c', 'D', 0);
                newReservation.pet[petIndex].service = new List<Hvk.ReservationService>();
                Hvk.ReservationService service = new Hvk.ReservationService();
                newReservation.pet[petIndex].service.Add(service);

            
            
        }



        protected void changeState(Boolean State)
        {
            txtResNote.Disabled = ((State == false) ? true : false);
            ((TextBox)UCstartDate.FindControl("txtDate")).Enabled = State;
            ((TextBox)UCendDate.FindControl("txtDate")).Enabled = State;
            reservationPanel.Enabled = State;
        }

        protected void loadData()
        {
            try
            {
                ((TextBox)UCstartDate.FindControl("txtDate")).Text = newReservation.reservaion.startDate.ToShortDateString();
                ((TextBox)UCendDate.FindControl("txtDate")).Text = newReservation.reservaion.endDate.ToShortDateString();
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

                        }
                    }

                    chWalk.Checked = true;
                    txtResNote.Value = newReservation.pet[(int)Session["PetID"]].note;
                }
                else
                {
                    //clear fields
                    chWalk.Checked = false;
                    txtResNote.Value = "";
                }
            }
            catch
            {
                Console.Write("Error - Exception catched => load file => manage reservation ! ");
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
                ((TextBox)UCstartDate.FindControl("txtDate")).Text = "";
                ((TextBox)UCendDate.FindControl("txtDate")).Text = "";
                lbCurrentPets.Items.Clear();
            }

            btnBook.Text = "Book";
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {

            Boolean validationFlag = validateReservationPetList() ; 

            if (valEndDate.IsValid == true && validationFlag == false )
                {
                    btnEdit.Visible = true;
                if (X >= 0)
                    updateFields(X);
                else
                    validationFlag = false;
                    loadData();
                    changeState(false);
                }
                changeState(validationFlag);

         }


        protected void ddlChoosePet_TextChanged(object sender, EventArgs e)
        {
            bool validation = validateReservationPetList(); 
            changeState(validation);
            if (validation)
            {
                //Add the pet to reservation 
                newReservation.pet.Add(new Hvk.PetReservation());
                newReservation.pet[newReservation.pet.Count - 1].pet = new Hvk.Pet();
                //possibly do a find on the current list to avoid duplication
                newReservation.pet[newReservation.pet.Count - 1].pet.name = ddlChoosePet.SelectedItem.ToString();
            }

        }

        protected void lbCurrentPets_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSelectedPet.Text = lbCurrentPets.SelectedItem.Text;
            ddlChoosePet.SelectedIndex = 0; //reset  
            changeState(true);//reset 
        }
    }

}