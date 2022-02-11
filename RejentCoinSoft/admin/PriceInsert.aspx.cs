using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class blue_Dashboard : System.Web.UI.Page
{
    clsNews objnews = new clsNews();
    clsUser objobject = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["rijentadmin45852_4"] != null)
            {
                
            }
            else
            {
                Server.Transfer("logout.aspx");
            }
        }
    }
    

    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtPrice.Text != "")
        {
            if (Convert.ToDecimal(txtPrice.Text) >= 0)
            {



                objobject.Amount = Convert.ToDecimal(txtPrice.Text);
                objnews.MentionBy = Session["rijentadmin45852_4"].ToString();
                string res = objobject.CoinRateUpdate(objobject);
                if (res == "t")
                {
                    string popupScript = "toastr.success('Success', 'Successfull');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    txtPrice.Text = "";

                }
                else
                {
                    string popupScript = "toastr.error('Error', 'Unknown error occurred');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                }

            }
            else
            {
                string popupScript = "toastr.error('Error', 'Enter Value');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        else
        {
            string popupScript = "toastr.error('Error', 'Enter Price');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Server.Transfer("Dashboard.aspx");
    }

   

 
}