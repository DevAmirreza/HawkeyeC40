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
    }
}