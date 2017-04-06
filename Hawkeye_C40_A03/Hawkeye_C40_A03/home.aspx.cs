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
        protected void Page_Load(object sender, EventArgs e) {
            gvReservations.GridLines = GridLines.None;
            
            searchPanel.Visible = false;
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            detailPanel.Visible = false;
            Application master = Master as Application;
            owner = master.owner;
            List<Reservation> resList = null;
            switch ((UserType)(Session["UserType"])) {
                case UserType.Owner:
                    clerkPanel.Visible = false;
                    searchPanel.Visible = false;
                    resList = owner.reservationList;
                    gvReservations.Columns[1].Visible = false;
                    break;
                case UserType.Clerk:
                    customerPanel.Visible = false;
                    btnBookNow.Visible = false;
                    resList = Reservation.listUpcomingReservations(DateTime.Now);
                    break;                   
            }
            populateResGrid(resList);
        }

        public void populateResGrid(List<Reservation> resList) {          
            if (resList != null) {
                DataTable dt = new DataTable();
                dt.Columns.Add("Owner");
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
                    if ((UserType)Session["UserType"] == UserType.Owner)
                        dr["Owner"] = owner.getFullName();
                    else {
                        Owner own = Owner.getOwner(res.ownerNumber);
                        dr["Owner"] = own.getFullName();
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

        protected void gvReservations_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "selectReservation") {
                Session["selectedReservation"] = e.CommandArgument.ToString();
                Response.Redirect("/manageReservation.aspx");
            }
        }

        protected void btnBookNow_Click(object sender, EventArgs e) {
            Session["selectedReservation"] = null;
            Response.Redirect("/manageReservation.aspx");
        }
    }
}