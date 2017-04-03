using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02
{
    public partial class ManageCustomer : System.Web.UI.Page
    {
       private Hvk.Owner newOwner = null;
       private Hvk.Pet newPet = null;
        enum UserType
        {
            Clerk,
            Owner,
            NewOwner
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            newOwner = (Hvk.Owner)Session["owner"];
       
            //Switch to clerk Mode
           

            if ((UserType)(Session["UserType"]) == UserType.Clerk)
            {
                btnAdd.Visible = true;
            }
            else if((UserType)(Session["UserType"]) == UserType.Owner)
            {
                btnAdd.Visible = false;
            }
            else
            {
                btnPassdEdit.Visible = false;
                btnAdd.Visible = false;

            }
            changeState(false);
            
            //reset status notifiation 
            lblMsg.Text = "";

        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            //set sessions
            if (Session["owner"] == null)
            {
                newOwner = new Hvk.Owner();
            }
            else
            {
                newOwner = ((Hvk.Owner)Session["owner"]);
            }

            if (!IsPostBack)
                loadData();
            
        }

        //load data into form
        public void loadData()
        {
            txtfName.Text = newOwner.firstName;
            txtlName.Text = newOwner.lastName;
            txtEmail.Text = newOwner.email;
            txtEmrgfName.Text = newOwner.emgFirstName;
            txtEmrglName.Text = newOwner.emgLastName;
            txtEmrgPhone.Text = newOwner.emgPhoneNumber;
            txtCity.Text = newOwner.address.city;
            DropDownProvince.Text = Convert.ToString(newOwner.address.province);
            txtaddress.Text = newOwner.address.street;
            txtPostal.Text = newOwner.address.postalCode;
            txtHomePhone.Text = newOwner.phone;
        }


        public void updateFields()
        {
            newOwner.firstName = Request.Form[txtfName.UniqueID];
            newOwner.lastName = Request.Form[txtlName.UniqueID];
            newOwner.email = Request.Form[txtEmail.UniqueID];
            newOwner.emgFirstName = Request.Form[txtEmrgfName.UniqueID];
            newOwner.emgLastName = Request.Form[txtEmrglName.UniqueID];
            newOwner.emgPhoneNumber = Request.Form[txtEmrgPhone.UniqueID];
            newOwner.address.city = Request.Form[txtCity.UniqueID];
            newOwner.address.street = Request.Form[txtaddress.UniqueID];
            newOwner.address.postalCode = Request.Form[txtPostal.UniqueID];
            newOwner.phone = Request.Form[txtHomePhone.UniqueID];
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            //update
            if (newOwner == null)
            {
                newOwner = new Hvk.Owner();
            }
            updateFields();
            //reload data
            loadData();

            if (txtEmrgfName.Text != "" ) {
                if (txtEmrgPhone.Text == "")
                {
                    valRequiredEmg.IsValid = false;
                    changeState(true);
                }
            }

            //to be fixed
            //if (txtEmail.Text != "")
            //    valHomePhone.IsValid = true;
            //else if (txtHomePhone.Text != "")
            //    valReqEmail.IsValid = true; 


            if (IsPostBack && valRequiredEmg.IsValid == true)
            {
                btnEdit.Visible = true;
                lblMsg.Text = "You have sucessfully saved your information ! ";
            }

            Session["UserType"] = UserType.Owner;
            if(newOwner.pet.Count == 0)
            {
                Server.Transfer("~/ManagePet.aspx");
            }
        }//Save Btn


        protected void changeState(bool State)
        {
            formPanel.Enabled = State;
        }


        protected void clear()
        {
            txtfName.Text = "";
            txtlName.Text = "";
            txtEmail.Text = "";
            txtEmrgfName.Text = "";
            txtEmrglName.Text = "";
            txtEmrgPhone.Text = "";
            txtCity.Text = "";
            txtaddress.Text = "";
            txtPostal.Text = "";
            txtHomePhone.Text = "";
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            changeState(true);
            lblMsg.Text = "";//reset message box
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                changeState(true);
                //form1.Disabled = false;
                btnAdd.Visible = true;
                btnPassdEdit.Visible = false;
                btnEdit.Visible = false;
                loadData();
                clear();
            }

        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            clear();
            changeState(true);
        }

    }
}