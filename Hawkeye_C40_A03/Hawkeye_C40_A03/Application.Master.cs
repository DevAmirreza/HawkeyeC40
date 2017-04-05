using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL;

namespace AYadollahibastani_C40A02
{
    public partial class Application : System.Web.UI.MasterPage
    {
        //BLL objects goes here
        public Owner owner;

        enum UserType
        {
            Clerk,
            Owner,
            NewOwner
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            owner = (Owner)Session["owner"];
            //on load set the active page to active
            //potential bug 
            string file = HttpContext.Current.Request.Url.AbsolutePath;
            if (owner == null && file != "/default.aspx")
            {
                try
                {
                    if ((UserType)Session["UserType"] != UserType.NewOwner || file != "/manageOwner.aspx")
                        Response.Redirect("/default.aspx");
                }
                catch
                {
                    Response.Redirect("/default.aspx");
                }
            }
            if  (owner == null) {
                lblDisplayName.Text = "New User";
            } else {
                lblDisplayName.Text = "Logged in as " + owner.getFullName();
            }
            switch (file)
            {
                case "/home.aspx":
                    li1.Attributes["class"] = "active";
                    break;
                case "/managePet.aspx":
                    li2.Attributes["class"] = "active";
                    break;
                case "/owners.aspx":
                    li2.Attributes["class"] = "active";
                    break;
                case "/manageCustomer.aspx":
                    li3.Attributes["class"] = "active";
                    break;
            }

            //set Nav based on type of user
            btnNav1.Text = "Home";
            btnNav3.Text = "Profile";
            if ((UserType)(Session["UserType"]) == UserType.Owner)
            {
                btnNav1.Visible = true;
                btnNav2.Visible = true;
                btnNav2.Text = "Pets";
            }
            else if ((UserType)(Session["UserType"]) == UserType.Clerk)
            {
                btnNav1.Visible = true;
                btnNav2.Visible = true;
                btnNav2.Text = "Owners";
                btnNav2.Attributes["href"] = "/owners.aspx";
            }
            else
            {
                btnNav1.Visible = false; ;
                btnNav2.Visible = false;
            }

        }
        public void navClick(object sender, EventArgs e)
        {
            LinkButton btnLink = (LinkButton)sender;
            int clickedItem = CharUnicodeInfo.GetDecimalDigitValue(btnLink.ID[btnLink.ID.Length - 1]);
            switch (clickedItem)
            {
                case 1://new customer
                    Response.Redirect("home.aspx");
                    break;
                case 2: // reservations

                    if ((UserType)(Session["UserType"]) == UserType.Owner)
                    {
                        Response.Redirect("managePet.aspx");
                    }
                    else
                    {
                        Response.Redirect("owners.aspx");// this will be changed when Owners.aspx is added
                    }

                    break;
                case 3://Handle Clerk edit profile
                    if ((UserType)(Session["UserType"]) == UserType.Owner)
                    {
                        Response.Redirect("manageCustomer.aspx");
                    }
                    else
                    {
                        
                        Session["SelectedOwner"] = null;
                        Response.Redirect("manageCustomer.aspx");

                    }
                    break;
            }
        }

        protected void logo_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
}