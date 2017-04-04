using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL;
namespace AYadollahibastani_C40A02
{
    public partial class manageReservation : System.Web.UI.Page
    {
        Owner newOwner = null ;
        Reservation reservationOnPage;
        int currentPetNumber = -1;
        enum UserType
        {
            Clerk,
            Owner
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean clerk;
            //Switch To Clerk - Default : Customer
            if ((UserType)Session["UserType"] == UserType.Owner)
            {
                clerk = false;
            }
            else {
                clerk = true;
            }
            ddlChooseRun.Visible = false;
            lblChooseRun.Visible = false;
            searchPanel.Visible = false;
            noReservationPanel.Visible = false;
            if (!IsPostBack) {
                changeState(false);
            }
            newOwner = ((Application)Master).owner;
            /*
             * The below variable is the reservation that is being manipulated.
             * It will be set to a specific reservation number if editing or not
             */
            Reservation reservationOnPage;
            bool resSet = false;
            if (Session["reservation"] != null) {
                int resNum = Convert.ToInt32(Session["reservation"]);
                newOwner.reservationList.ForEach(delegate(Reservation res) {
                    if (resNum == res.reservationNumber) {
                        reservationOnPage = res;
                        resSet = true;
                    }
                });
            }

            if (!resSet) {
                reservationOnPage = new Reservation();
            }

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

            if (!IsPostBack)
                loadData();
        }


        //updating fields need to be change to work with db
        protected void updateFields(int petIndex)
        {            
                reservationOnPage.petReservationList[petIndex].pet.name = lbCurrentPets.Items[petIndex].Text;
                // get reservation
                // reservationOnPage.petReservationList[petIndex].run.runNumber = 
                ReservedService service = new ReservedService();
                reservationOnPage.petReservationList[petIndex].serviceList.Add(service);
            
            
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
                ((TextBox)UCstartDate.FindControl("txtDate")).Text = reservationOnPage.startDate.ToShortDateString();
                ((TextBox)UCendDate.FindControl("txtDate")).Text = reservationOnPage.endDate.ToShortDateString();
                //loads pet list from object into dropdown
                if (!IsPostBack)
                {
                    //Hawkeye : newOwner.reservation.petReservation.pet 
                    foreach (var item in newOwner.petList)
                    {
                        ddlChoosePet.Items.Add(item.name);
                        ddlChoosePet.Items[ddlChoosePet.Items.Count - 1].Value = item.petNumber.ToString();
                    }
                }
                // Multiple Pet - to be implemented here
                //Hawkeye : newOwner.reservation.petReservation.pet.Count() 
                if (reservationOnPage.petReservationList.Count() > 0)
                {
                    if (!IsPostBack)
                    {
                        foreach (var item in reservationOnPage.petReservationList)
                        {
                            lbCurrentPets.Items.Add(item.pet.name);
                            lbCurrentPets.Items[lbCurrentPets.Items.Count - 1].Value = item.pet.petNumber.ToString();

                        }
                    }

                    chWalk.Checked = true;
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

        protected void btnreservationOnPage_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                reservationOnPage = new Reservation();
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
                if (currentPetNumber >= 0)
                    updateFields(currentPetNumber);
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
                reservationOnPage.petReservationList.Add(new PetReservation());
                reservationOnPage.petReservationList[reservationOnPage.petReservationList.Count - 1].pet = new Pet();
                //possibly do a find on the current list to avoid duplication
                reservationOnPage.petReservationList[reservationOnPage.petReservationList.Count - 1].pet.name = ddlChoosePet.SelectedItem.ToString();
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