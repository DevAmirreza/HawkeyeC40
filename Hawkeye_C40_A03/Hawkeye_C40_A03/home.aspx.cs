using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL;

namespace AYadollahibastani_C40A02
{
    public partial class homePage : System.Web.UI.Page
    {
        Owner owner;
        List<Reservation> resList;
        enum UserType
        {
            Clerk,
            Owner
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            changeState(false);
            searchPanel.Visible = false;
            if ((UserType)(Session["UserType"]) != UserType.Clerk)
            {
                clerkPanel.Visible = false;
            }
            else
            {
                customerPanel.Visible = false;
                searchPanel.Visible = true; 
            }
            detailPanel.Visible = false;
            
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
            Application master = Master as Application;
            owner = master.owner;
        }

        protected void btnMoreInfo_Click(object sender, EventArgs e)
        {
            //detailPanel.Visible = true;
            //if (newReservation != null)
            //    loadData();
        }
    }
}