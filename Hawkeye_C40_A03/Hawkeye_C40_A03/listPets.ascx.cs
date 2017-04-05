using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL;
namespace AYadollahibastani_C40A02
{
    public partial class listPets : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Pet> petList = Pet.listPets(((Owner)Session["owner"]).ownerNumber);
            gdPetList.DataSource = petList;
            gdPetList.DataBind();

        }
    }
}