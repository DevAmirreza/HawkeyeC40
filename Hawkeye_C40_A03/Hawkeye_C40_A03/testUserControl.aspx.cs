using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02 {
    public partial class testUserControl : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            TextBox txtbox = (TextBox)CalendarControl.FindControl("txtDate");
            lbl.Text = txtbox.Text;
        }
    }
}