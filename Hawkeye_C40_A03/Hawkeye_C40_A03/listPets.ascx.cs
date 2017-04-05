using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HawkeyehvkBLL;
using System.Data;

namespace AYadollahibastani_C40A02
{
    public partial class listPets : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Pet> petList = Pet.listPets(((Owner)Session["owner"]).ownerNumber);
            populatePetGrid(petList);
            gvPetList.GridLines = GridLines.None;
        }


        public void populatePetGrid(List<Pet> petList)
        {
            if (petList != null && !Page.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("petNumber");
                dt.Columns.Add("name");
                dt.Columns.Add("gender");
                dt.Columns.Add("birthday");

                foreach (Pet pet in petList)
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = pet.name.ToString();

                    switch (Convert.ToChar(pet.gender))
                    {
                        case 'F':
                            dr["gender"] = "Female"; 
                            break;
                        case 'M':
                            dr["gender"] = "Male";
                            break;
                        default:
                            break;
                    }

                    if (pet.birthday.ToShortDateString() == "1/1/0001")
                        dr["birthday"] = "Not Available"; 
                    else
                    dr["birthday"] = pet.birthday.ToShortDateString();
                    dr["petNumber"] = Convert.ToInt16(pet.petNumber);
                    dt.Rows.Add(dr);
                }
                gvPetList.DataSource = dt;
                gvPetList.DataBind();
            }
        }


    
        protected void gvPetList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int petNum = Convert.ToInt16(e.CommandArgument);
            Session["petID"] = petNum;
            for (int i = 0; i < ((Owner)Session["owner"]).petList.Count; i++)
            {
                if (petNum == ((Owner)Session["owner"]).petList[i].petNumber)
                {
                    Session["SelectedPet"] = i;
                }
            }

            //Session["selectedPet"] = ((Owner)Session["owner"]).petList[gvPetList.SelectedIndex];

        }

        protected void gvPetList_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            int petNum = Convert.ToInt16(e.CommandArgument);
            Session["petID"] = petNum;
            for (int i = 0; i < ((Owner)Session["owner"]).petList.Count; i++)
            {
                if (petNum == ((Owner)Session["owner"]).petList[i].petNumber)
                {
                    Session["SelectedPet"] = i;
                }
            }


            //Session["selectedPet"] = ((Owner)Session["owner"]).petList[gvPetList.SelectedIndex];

        }


    }
}