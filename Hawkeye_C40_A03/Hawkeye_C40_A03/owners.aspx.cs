using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02
{
    public partial class owner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            editDisplay.Visible = false;
            viewPet.Visible = false; 
        }

        protected void customerEdit_Click(object sender, EventArgs e)
        {
            
        }

        protected void cdOwnerSelected_CheckedChanged(object sender, EventArgs e)
        {
            //selects the owner id here

            if (cdOwnerSelected.Checked)
                editDisplay.Visible = true;
            else
                editDisplay.Visible = false;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            //ManageCustomer customer = new ManageCustomer();
            //customer.clear();
            Session["owner"] = null; 
        }

        protected void btnViewPet_Click(object sender, EventArgs e)
        {

            if (viewPet.Visible)
            {
                viewPet.Visible = false;
                btnViewPet.Text = "Hide List of pets ";
            }
            else
            {
                viewPet.Visible = true;
                btnViewPet.Text = "View List of pets ";
            }
            }
    }
}