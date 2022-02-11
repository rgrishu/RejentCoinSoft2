using System;
using BusinessLogicTier;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ViewTicket : System.Web.UI.Page
{
   
       clsSupport objsupport = new clsSupport();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rijentadmin45852_4"] != null)
            {
            DataTable dt = new DataTable();
            objsupport.ToId = Session["rijentadmin45852_4"].ToString();
            objsupport.MentionBy = "admin";
            dt = objsupport.getInbox(objsupport,true);
            grdticket.DataSource = dt;
            grdticket.DataBind();
        }
            else
            {
                Server.Transfer("index.aspx");
            }

        }


        protected void btnrep_click(object sender, EventArgs e)
        {
         //  Server.Transfer("Reply.aspx");

        try
            {
            //Button btn = (Button)sender;
            //GridViewRow row = (GridViewRow)btn.NamingContainer;
            //HiddenField hidTecketId = (HiddenField)row.Cells[5].FindControl("hidTecketId");
            //string TicketId = hidTecketId.Value;
            //Session["TicketNo"] = TicketId.ToString();
            //Response.Redirect("Reply.aspx");

        }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label status = (Label)e.Row.Cells[3].FindControl("lblStatus");
            string lblStatus = status.Text;
            if (lblStatus == "Open")
            {
                status.CssClass = "badge badge-success";
                status.Font.Bold = true;
            }
            else
            {
                status.CssClass = "badge badge-dark";
                status.Font.Bold = true;
            }
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdticket, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
        foreach (GridViewRow row in grdticket.Rows)
        {
            if (row.RowIndex == grdticket.SelectedIndex)
            {
                //3 
                HiddenField hidTicketNo = (HiddenField)row.Cells[5].FindControl("hidTecketId");
                Session["TicketNo"] = hidTicketNo.Value.ToString();
                Response.Redirect("Reply.aspx");
            }
        }
    }



    protected void grdticket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hidTicketNo = (HiddenField)e.Row.Cells[5].FindControl("hidTecketId");
            Session["TicketNo"] = hidTicketNo.Value.ToString();
            Response.Redirect("Reply.aspx");
        }
    }
}
