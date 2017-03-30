using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clerkLogin.Visible = false; 
        }


        protected void changeLogin(bool clerk) {

            if (clerk)
            {
                clerkLogin.Visible = true;
                customerLogin.Visible = false;
            }
            else
            {
                clerkLogin.Visible = false;
                customerLogin.Visible = true;
            }
        }

        protected void btnClerk_Click(object sender, EventArgs e)
        {
            changeLogin(true); 
        }

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            changeLogin(false); 
        }
    }
}