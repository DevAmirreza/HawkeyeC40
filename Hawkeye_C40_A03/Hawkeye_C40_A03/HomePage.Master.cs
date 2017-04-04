using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL;

namespace AYadollahibastani_C40A02
{
    public partial class HomePage : System.Web.UI.MasterPage
    {
        public Owner owner;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["owner"] == null)
            {
                if (HttpContext.Current.Request.Url.AbsolutePath != "/default.aspx")
                    Response.Redirect("/default.aspx");
            }
            else
            {
                owner = (Owner)Session["owner"];
            }
        }

        protected void logo_Click(object sender, EventArgs e) {
            Response.Redirect("main.aspx");
        }
    }
}