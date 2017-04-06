using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL;
using System.Globalization;

namespace AYadollahibastani_C40A02
{
    public partial class manageReservation : System.Web.UI.Page
    {
        Reservation editedReservation;
        Owner newOwner = null ;
        enum UserType
        {
            Clerk,
            Owner
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["manageReservationObject"] != null)
            {
                editedReservation = (Reservation)Session["manageReservationObject"];
            }
            else {
                Session["manageReservationObject"] = new Reservation();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                validatDates();
            }
            newOwner = ((Application)Master).owner;
            Reservation resOnPage = new Reservation();
            //Switch To Clerk - Default : Customer
            if (!IsPostBack)
            {
                changeState(true);
                if ((UserType)Session["UserType"] == UserType.Owner)
                {
                    //ddlChooseRun.Visible = false;
                    //lblChooseRun.Visible = false;
                    if (Session["selectedReservation"] != null)
                    {
                        bool resFound = false;
                        int resNum = Convert.ToInt32(Session["selectedReservation"]);
                        newOwner.reservationList.ForEach(delegate (Reservation res)
                        {
                            if (resNum == res.reservationNumber)
                            {
                                resOnPage = res;
                                resFound = true;
                            }
                        });
                        if (!resFound)
                        {
                            //Error, invalid reservation number passed....
                        }
                        else
                        {
                            pageTitle.InnerText = "Editing Reservation";
                            btnDeleteRes.Enabled = true;
                        }
                            loadData(resOnPage, ((Application)Master).owner);
                        Session["manageReservationObject"] = resOnPage;


                    }
                    else
                    {//new Reservation, owner
                        pageTitle.InnerText = "New Reservation";
                        btnDeleteRes.Enabled = false;
                        editedReservation = new Reservation();
                        Session["manageReservationObject"] = editedReservation;
                        Owner own = (Owner)Session["owner"];
                        own.petList.ForEach(delegate (Pet pet) {
                            ddlAddPet.Items.Add(new ListItem(pet.name, pet.petNumber.ToString()));
                        });
                    }
                }
                else
                {// clerk
                    //update run ddl with availible runs for that day

                    //ddlChooseRun.Visible = false;//false for now
                    //lblChooseRun.Visible = false;//false for now
                    if (Session["selectedReservation"] != null)// edit reservation / clerk
                    {
                        Reservation curRes = new Reservation();
                        int resNum = Convert.ToInt32(Session["selectedReservation"]);

                        curRes = Reservation.getReservation(resNum);
                        if (curRes != null)
                        {
                            btnDeleteRes.Enabled = true;
                            Owner own =Owner.getOwner(curRes.ownerNumber);
                            loadData(curRes, own);
                            pageTitle.InnerText = "Editing Reservation for " + own.firstName + " " + own.lastName;
                            Session["manageReservationObject"] = curRes;
                            
                        }
                        else
                        {
                            //Error, invalid reservation number or owner number passed....

                        }
                        //pull res from DB
                    }
                    else
                    { // new Reservation, clerk

                        Owner own = ((Owner)(Session["selectedOwner"]));
                        pageTitle.InnerText = "New Reservation for " + own.firstName + " " + own.lastName;
                        btnDeleteRes.Enabled = false;
                        editedReservation = new Reservation();
                        Session["manageReservationObject"] = editedReservation;
                        own.petList.ForEach(delegate (Pet pet) {
                            ddlAddPet.Items.Add(new ListItem(pet.name, pet.petNumber.ToString()));
                        });
                    }
                }
            }
            else {
               // is a postback
            }
        }

  

        protected void changeState(Boolean State)
        {
            ((TextBox)UCstartDate.FindControl("txtDate")).Enabled = State;
            ((TextBox)UCendDate.FindControl("txtDate")).Enabled = State;
            reservationPanel.Enabled = State;
        }

        protected void loadData(Reservation reservationOnPage,Owner owner)
        {
            editedReservation = reservationOnPage;
            try
                {
                ddlAddPet.Items.Clear();
                ddlPetsInRes.Items.Clear();
                ((TextBox)UCstartDate.FindControl("txtDate")).Text = reservationOnPage.startDate.ToShortDateString();
                    ((TextBox)UCendDate.FindControl("txtDate")).Text = reservationOnPage.endDate.ToShortDateString();
                    List<Pet> petList = new List<Pet>();
                    reservationOnPage.petReservationList.ForEach(delegate(PetReservation pres) {
                        petList.Add(pres.pet);
                    });
                    petList.ForEach(delegate(Pet pet) {
                        ddlPetsInRes.Items.Add(new ListItem(pet.name,pet.petNumber.ToString()));
                    });
                    bool inList;
                    owner.petList.ForEach(delegate (Pet pet) {
                        inList = false;// check if pet is already listed in other ddl
                        petList.ForEach(delegate (Pet petInDdl) {
                            if (petInDdl.name == pet.name) {
                                inList = true;
                            }
                        });
                        if (!inList) {// if pet not already listed add the the other ddl
                            ddlAddPet.Items.Add(new ListItem(pet.name, pet.petNumber.ToString()));
                        }
                    });

                // now populate the first pet reservations data
                setSelectedPetRes(reservationOnPage);

                }
                catch
                {
                    Console.Write("Error - Exception catched => load file => manage reservation ! ");
                }
            
        }//load info into fields 

        private void setSelectedPetRes(Reservation reservationOnPage) {
            List<ReservedService> resser = new List<ReservedService>();
            reservationOnPage.petReservationList.ForEach(delegate(PetReservation pres) {
                if (pres.pet.petNumber == Convert.ToInt32(ddlPetsInRes.SelectedValue)) {
                    resser = pres.serviceList;
                    
                    // populate run ddl with availible runs
                    //if (pres.pet.size == 'L') {
                    //    List<Run> runs = Run.getNumAvailableLargeRuns(reservationOnPage.startDate, reservationOnPage.endDate);
                    //}
                    //else {
                    //    Run.getNumAvailableRuns(reservationOnPage.startDate, reservationOnPage.endDate)
                    //}
                }
            });
            bool walk = false;
            bool play = false;
           resser.ForEach(delegate (ReservedService serv) {
               if (serv.service.descripion == "Walk")
               {
                   walk = true;
               }
               else if (serv.service.descripion == "Playtime")
               {
                   play = true;
               }
            });
            if (walk)
            {
                chWalk.Checked = true;
            }
            else
            {
                chWalk.Checked = false;
            }

            if (play)
            {
                chPlaytime.Checked = true;
            }
            else
            {
                chPlaytime.Checked = false;
            }
        }


        protected void btnEdit_Click1(object sender, EventArgs e)
        {
            changeState(true);
            btnBook.Text = "Save";
        }

        protected void btnAddDog_Click(object sender, EventArgs e)
        {
            string petName;
            int petNum;
            petNum = Convert.ToInt32(ddlAddPet.SelectedValue);
            petName = ddlAddPet.SelectedItem.Text;

            ddlAddPet.Items.Remove(ddlAddPet.SelectedItem);
            ddlPetsInRes.Items.Add(new ListItem(petName, petNum.ToString()));

            // add pet reservation
            PetReservation pres = new PetReservation();
            pres.pet = Pet.getOnePet(petNum);
            editedReservation.addPetReservation(pres);
            Session["manageReservationObject"] = editedReservation;
            setDropdownsFunctionality();
        }

        protected void btnRemovePet_Click(object sender, EventArgs e)
        {
            string petName;
            int petNum;
            petNum = Convert.ToInt32(ddlPetsInRes.SelectedValue);
            petName = ddlPetsInRes.SelectedItem.Text;

            ddlPetsInRes.Items.Remove(ddlPetsInRes.SelectedItem);
            ddlAddPet.Items.Add(new ListItem(petName, petNum.ToString()));

            // remove pet reservation
            PetReservation pres = new PetReservation();
            editedReservation.petReservationList.ForEach(delegate (PetReservation presloop)
            {
                if (presloop.pet.petNumber== petNum) {
                    pres = presloop;
                }
            });
            if (pres!=null) {
                editedReservation.removePetReservation(pres);
            }
            else {
                //if it wasnt removed something went wrong
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No pet reservation was removed..')", true);
            }

            Session["manageReservationObject"] = editedReservation;
            setDropdownsFunctionality();
            setSelectedPetRes(editedReservation);
            
        }
        private void setDropdownsFunctionality() {
            if (ddlAddPet.Items.Count == 0)
            {
                ddlAddPet.Enabled = false;
                btnAddDog.Enabled = false;
            }
            else {
                ddlAddPet.Enabled = true;
                btnAddDog.Enabled = true;
            }

            if (ddlPetsInRes.Items.Count == 0)
            {
                ddlPetsInRes.Enabled = false;
                btnRemovePet.Enabled = false;
            }
            else
            {
                ddlPetsInRes.Enabled = true;
                btnRemovePet.Enabled = true;
            }
            if (ddlPetsInRes.Items.Count == 1)
            {
                btnRemovePet.Enabled = false;

            }
            else {
                btnRemovePet.Enabled = true;
            }
        }

        protected void ddlPetsInRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSelectedPetRes(editedReservation);
        }
        private void updatePres() {
            // this updates the pet reservation services
            bool walk = chWalk.Checked;
            bool playtime = chPlaytime.Checked;
            int petId = Convert.ToInt32(ddlPetsInRes.SelectedValue);

            editedReservation.petReservationList.ForEach(delegate (PetReservation pres)
            {
                if (pres.pet.petNumber == petId)
                {
                    List<ReservedService> resser = new List<ReservedService>();
                    ReservedService res = new ReservedService();
                    Service ser = new Service();
                    ser.descripion = "Boarding";
                    ser.serviceNumber = 1;
                    res.service = ser;
                    resser.Add(res);
                    if (walk) {
                        Service ser2 = new Service();
                        ReservedService res2 = new ReservedService();
                        ser2.descripion = "Walk";
                        ser2.serviceNumber = 2;
                        res2.service = ser2;
                        resser.Add(res2);
                    }
                    if (playtime) {
                        Service ser3 = new Service();
                        ReservedService res3 = new ReservedService();
                        ser3.descripion = "Playtime";
                        ser3.serviceNumber = 5;
                        res3.service = ser3;
                        resser.Add(res3);
                    }
                    pres.serviceList = resser;                
                }
            });

            }
        protected void chWalk_CheckedChanged(object sender, EventArgs e)
        {
            updatePres();
        }

        protected void chPlaytime_CheckedChanged(object sender, EventArgs e)
        {
            updatePres();
        }
        public void validatDates()
        {
            if (((TextBox)UCstartDate.FindControl("txtDate")).Text!= ""&& ((TextBox)UCendDate.FindControl("txtDate")).Text != "") { 
            try
            {
                DateTime start = Convert.ToDateTime(UCstartDate.vacDate);
                DateTime end = Convert.ToDateTime(UCendDate.vacDate);
                if (end < start)
                {
                    valEndDate.IsValid = false;
                }
            }
            catch
            {
                valEndDate.IsValid = false;
            }
        }
        }
        protected void btnBook_Click(object sender, EventArgs e)
        {
            validatDates();
            
            if ((UserType)Session["UserType"] == UserType.Owner)
            {// save to session
                DateTime start = Convert.ToDateTime(((TextBox)UCstartDate.FindControl("txtDate")).Text);
                DateTime end = Convert.ToDateTime(((TextBox)UCendDate.FindControl("txtDate")).Text);
                
                editedReservation.startDate = start;
                editedReservation.endDate = end;
                int resNum = Convert.ToInt32(Session["selectedReservation"]);
                if (Session["selectedReservation"] != null) {
                    ((Application)Master).owner.reservationList.ForEach(delegate (Reservation res) {
                        if (res.reservationNumber == resNum) {
                            res = editedReservation;
                        }
                    });
                }
                else {
                    ((Application)Master).owner.addReservation(editedReservation);
                }
            }
            // regardless of user disable form on save
            reservationPanel.Enabled = false;
        }
        

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // change reservation to the un edited version
            int resNum = Convert.ToInt32(Session["selectedReservation"]);
            editedReservation = Reservation.getReservation(resNum);
            Session["manageReservationObject"] = editedReservation;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnDeleteRes_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Delete Funtionality not implemented')", true);
        }
    }

}