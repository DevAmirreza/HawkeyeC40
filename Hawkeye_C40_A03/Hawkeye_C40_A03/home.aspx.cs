using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL;
using System.Data;

namespace AYadollahibastani_C40A02
{
    public partial class homePage : System.Web.UI.Page
    {
        Owner owner;
        enum UserType
        {
            Clerk,
            Owner
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }


        protected void loadReservationData() {
            //lblStartTime.Text = newReservation.reservaion.startDate.ToShortDateString();
            //lblEndTime.Text = newReservation.reservaion.endDate.ToShortDateString();
            //if (newReservation != null) {
            //    foreach(var item in newReservation.pet)
            //    {
            //        lblPetNames.Text += item.pet.name + " , ";  
            //    }
            //} 
        }

        protected void loadData()
        {
            //((TextBox)UCstartDate.FindControl("txtDate")).Text = newReservation.reservaion.startDate.ToShortDateString();
            //((TextBox)UCendDate.FindControl("txtDate")).Text = newReservation.reservaion.endDate.ToShortDateString();
            ////loads pet list from object into dropdown
      
            //if (newReservation.pet.Count() > 0)
            //{
            //    if (IsPostBack && lbCurrentPets.Items.Count == 0)
            //    {
            //        foreach (var item in newReservation.pet)
            //        {
            //            //to be fixed
            //            lbCurrentPets.Items.Add(item.pet.name);
            //        }
            //    }

                
            //    chWalk.Checked = true;
            //    txtResNote.Value = newReservation.pet[0].note;
            //}
            //else
            //{
            //    //clear fields
            //    chWalk.Checked = false;
            //    txtResNote.Value = "";
            //}
        }

        protected void changeState(Boolean State)
        {
            //txtResNote.Disabled = ((State == false) ? true : false);
            //((TextBox)UCstartDate.FindControl("txtDate")).Enabled = ((State == false) ? true : false);
            //((TextBox)UCstartDate.FindControl("txtDate")).Enabled = ((State == false) ? true : false);
            //clerkPanel.Enabled = State;
        }

        protected void chReservationSelect_CheckedChanged(object sender, EventArgs e)
        {
            //detailPanel.Visible = true;
            //if (newReservation != null)
            //loadData();
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {

            //if (Session["reservation"] == null)
            //{
            //    newReservation = new Hvk.HvkPetReservation();
            //}
            //else
            //{
            //    newReservation = ((Hvk.HvkPetReservation)Session["reservation"]);
            //    newOwner = (Hvk.Owner)Session["owner"];
            //}

            //if (!IsPostBack)
            //    loadReservationData();
            changeState(false);
            detailPanel.Visible = false;
            Application master = Master as Application;
            owner = master.owner;
            List<Reservation> resList = null;
            switch ((UserType)(Session["UserType"])) {
                case UserType.Owner:
                    clerkPanel.Visible = false;
                    resList = owner.reservationList;
                    break;
                case UserType.Clerk:
                    customerPanel.Visible = false;
                    searchPanel.Visible = true;
                    resList = Reservation.listUpcomingReservations(DateTime.Now);
                    break;                   
            }
            populateResGrid(resList);
        }

        public void populateResGrid(List<Reservation> resList) {          
            if (resList != null) {
                DataTable dt = new DataTable();
                dt.Columns.Add("PetNames");
                dt.Columns.Add("StartDate");
                dt.Columns.Add("EndDate");
                dt.Columns.Add("ValidVaccinations");
                dt.Columns.Add("reservationId");
                foreach (Reservation res in resList) {
                    DataRow dr = dt.NewRow();
                    bool valid = true;
                    string petNames = "";
                    foreach (PetReservation pres in res.petReservationList) {
                        if (petNames.Length > 0)
                            petNames += ", ";
                        petNames += pres.pet.name;
                        if (valid) {
                            valid = (PetVaccination.checkVaccinations(pres.pet.petNumber, res.endDate) == 0);
                        }
                    }
                    dr["PetNames"] = petNames;
                    dr["StartDate"] = res.startDate.ToShortDateString();
                    dr["EndDate"] = res.endDate.ToShortDateString();
                    dr["ValidVaccinations"] = valid;
                    dr["reservationId"] = res.reservationNumber;
                    dt.Rows.Add(dr);
                }
                gvReservations.DataSource = dt;
                gvReservations.DataBind();
            }
        }

        protected void btnMoreInfo_Click(object sender, EventArgs e)
        {
            //detailPanel.Visible = true;
            //if (newReservation != null)
            //    loadData();
        }

        protected void gvReservations_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "selectReservation") {
                Session["selectedReservation"] = e.CommandArgument.ToString();
                Response.Redirect("/manageReservation.aspx");
            }
        }
    }
}