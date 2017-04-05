using HawkeyehvkBLL;
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
       private Owner newOwner;
       private Owner clerk;
        enum UserType
        {
            Clerk,
            Owner,
            NewOwner
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            //Application master = Master as Application;
           // newOwner = master.owner;
            

            //Switch to clerk Mode

            changeState(false);
            if ((UserType)(Session["UserType"]) == UserType.Clerk)
            {
                clerk = (Owner)Session["owner"];
                if (Session["SelectedOwner"] != null)
                    newOwner = (Owner)Session["SelectedOwner"];
                btnAdd.Visible = true;
                
            }
            else if((UserType)(Session["UserType"]) == UserType.Owner)
            {
                btnAdd.Visible = false;
                newOwner = (Owner)Session["owner"];

            }
            else
            {
                newOwner = (Owner)Session["owner"];
                btnPassedEdit.Visible = false;
                btnAdd.Visible = false;
                displayPasswords(true);
                changeState(true);

            }
            
            
            //reset status notifiation 
            lblMsg.Text = "";

        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            if ((UserType)Session["UserType"] != UserType.Clerk)
            {
                //set sessions
                if (Session["owner"] == null)
                {
                    newOwner = new Owner();
                    Session["owner"] = newOwner;
                    //displayPasswords(true);
                    //btnPassedEdit.Visible = false;
                }
                else
                {
                    Application master = Master as Application;
                    newOwner = master.owner;
                    Session["owner"] = newOwner;
                    if (!IsPostBack)
                        displayPasswords(false);

                }

                if (!IsPostBack)
                    loadData();
            }
            else
            {
                if (Session["SelectedOwner"] == null)
                {
                    newOwner = new Owner();
                    Session["SelectedOwner"] = newOwner;
                    //displayPasswords(true);
                    //btnPassedEdit.Visible = false;
                    Session["owner"] = clerk;
                }
                else
                {
                    //Application master = Master as Application;
                    //newOwner = master.owner;
                    Session["SelectedOwner"] = newOwner;
                    Session["owner"] = clerk;

                    if (!IsPostBack)
                        displayPasswords(false);

                }
                if (!IsPostBack)
                    loadData();
            }
            
        }

        //load data into form
        public void loadData()
        {
            txtfName.Text = newOwner.firstName;
            txtlName.Text = newOwner.lastName;
            txtEmail.Text = newOwner.email;
            txtEmrgfName.Text = newOwner.emergencyFirstName;
            txtEmrglName.Text = newOwner.emergencyLastName;
            txtEmrgPhone.Text = newOwner.emergencyPhone;
            txtCity.Text = newOwner.address.city;
            if(newOwner.address.province == "")
            {
                DropDownProvince.SelectedIndex = 0;
            }
            else
                DropDownProvince.SelectedIndex = DropDownProvince.Items.IndexOf(DropDownProvince.Items.FindByValue(newOwner.address.province));
                txtaddress.Text = newOwner.address.street;
                txtPostal.Text = newOwner.address.postalCode;
                txtHomePhone.Text = newOwner.phoneNumber;
        }


        public void updateFields()
        {
            newOwner.firstName = Request.Form[txtfName.UniqueID];
            newOwner.lastName = Request.Form[txtlName.UniqueID];
            newOwner.email = Request.Form[txtEmail.UniqueID];
            newOwner.emergencyFirstName = Request.Form[txtEmrgfName.UniqueID];
            newOwner.emergencyLastName = Request.Form[txtEmrglName.UniqueID];
            newOwner.emergencyPhone = Request.Form[txtEmrgPhone.UniqueID];
            newOwner.address.city = Request.Form[txtCity.UniqueID];
            newOwner.address.street = Request.Form[txtaddress.UniqueID];
            newOwner.address.postalCode = Request.Form[txtPostal.UniqueID];
            newOwner.address.province = DropDownProvince.SelectedItem.ToString();
            newOwner.phoneNumber = Request.Form[txtHomePhone.UniqueID];

            //Session["owner"] = newOwner;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            //update
            //if (newOwner == null)
            //{
            //    newOwner = new Owner();
           //}
            updateFields();
            //reload data
            loadData();
            //Session["owner"] = newOwner;
            btnPassedEdit.Visible = true;
            displayPasswords(false);
            if (txtEmrgfName.Text != "" ) {
                if (txtEmrgPhone.Text == "")
                {
                    valRequiredEmg.IsValid = false;
                    changeState(true);
                }
            }

            //btnPassedEdit.Visible = true;
            //displayPasswords(false);
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

           

            if(newOwner.petList.Count == 0 && ((UserType)Session["UserType"] == UserType.NewOwner))
            {
                Server.Transfer("~/ManagePet.aspx");
            }
        }//Save Btn


        protected void changeState(bool State)
        {
            formPanel.Enabled = State;
        }


        public void clear()
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
                btnPassedEdit.Visible = false;
                btnEdit.Visible = false;
                //loadData();
                clear();
                displayPasswords(true);
            }

        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            loadData();
            changeState(false);
            displayPasswords(false);
            btnPassedEdit.Visible = true;
        }

        protected void displayPasswords(bool display)
        {
            passwordPanel.Visible = display;
        }

        protected void btnPassedEdit_Click(object sender, EventArgs e)
        {
            changeState(true);
            displayPasswords(true);
            btnPassedEdit.Visible = false;
        }
    }
}