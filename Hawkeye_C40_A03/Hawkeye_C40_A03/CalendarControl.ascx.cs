using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AYadollahibastani_C40A02 {
    public partial class CalendarControl : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            calControl.Visible = false;
        }

        protected void btnStartDate_Click(object sender, ImageClickEventArgs e) {
            if (calControl.Visible == false) {
                calControl.Visible = true;
            }
            else {
                calControl.Visible = false;
            }
        }

        protected void calControl_SelectionChanged(object sender, EventArgs e) {
            calControl.Visible = false;
            txtDate.Value = calControl.SelectedDate.ToShortDateString();
        }

        protected void calControl_VisibleMonthChanged(object sender, MonthChangedEventArgs e) {
            calControl.Visible = true;
        }
    }
}