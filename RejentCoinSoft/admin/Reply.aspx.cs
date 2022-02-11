using System;
using BusinessLogicTier;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_Reply : System.Web.UI.Page
{
    clsSupport objsupport = new clsSupport();
    //private object chkfintechservices;
    //private object objClient;


    protected void Page_Load(object sender, EventArgs e)
    {
        string TicketNo = string.Empty;
        if (Session["rijentadmin45852_4"] != null)
        {
            this.Focus();
            if (!IsPostBack)
            {

                if (Session["rijentadmin45852_4"] != null)
                {

                    TicketNo = Session["TicketNo"].ToString();
                    BindResponse(Int32.Parse(TicketNo));


                }
                else
                {
                    Response.Redirect("ViewTicket.aspx");

                }
            }
        }
        else
        {
            Server.Transfer("index.aspx");
        }
    }




    void BindResponse(int TicketNo)
    {

        var userid = Convert.ToInt32(Session["rijentadmin45852_4"]);
        DataTable dtResponse = new DataTable();
        dtResponse = objsupport.getMessage(userid,TicketNo);
       // dtResponse = objsupport.getMessage(userid);
        if (dtResponse.Rows.Count > 0)
        {
            DataList1.DataSource = dtResponse;
            DataList1.DataBind();

        }



        else
        {
            DataList1.DataSource = null;
            DataList1.DataBind();

        }

    }

    protected bool ValidateFileUpload(HttpPostedFile fileID, long sizeinKb, string extns)
    {
        bool IsFileValid = false;
        try
        {
            string ext = Path.GetExtension(fileID.FileName);

            if (extns.ToLower().Contains(ext.ToLower()))
            {
                IsFileValid = true;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert(' Error.. Only " + extns + " formats are allowed.');", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "$('.alert-danger').removeClass('hide').html('<strong>" + msgHeader + " Error</strong> Only " + extns + " formats are allowed.');", true);
                return false;
            }


        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('File does not exists');", true);
            IsFileValid = true;
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "$('.alert-danger').removeClass('hide').html('<strong>" + msgHeader + " Error</strong> File does not exists');", true);
        }
        return IsFileValid;
    }

    protected void btn_SubmitClose_Click(object sender, EventArgs e)
    {
        btn_Submit_Close_Click(sender, e);
        btn_Close_Click(sender, e);

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
         "err_msg",
         "alert('Ticket is Now Closed');window.location='Reply.aspx';",
         true);
        // Response.Redirect("Reply.aspx");

    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {

        DataTable dtResponse = new DataTable();
        var str_image = "";
        if (CKEditor1.Text.Trim() != "")
        {
            DataTable dt = new DataTable();
            //dbfunction db = new dbfunction();
            //ApiClass api = new ApiClass();
            if (FileUpload1.HasFile)
            {
                HttpPostedFile PostedFile = FileUpload1.PostedFile;
                if (ValidateFileUpload(PostedFile, 5000, ".jpg, .jpeg, .png, .pdf, .docx, .xlsx, .txt, .pdf") == false)
                {
                    return;
                }
                str_image = "support_" + Guid.NewGuid().ToString().Substring(0, 6) + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("../admin/userimage/") + str_image);
            }
            String UsID = Session["rijentadmin45852_4"].ToString();
            var ret = objsupport.InsertSupportTicketReply(Convert.ToInt32(UsID), Convert.ToInt32(Session["TicketNo"]), "", CKEditor1.Text, str_image);


            // Response.Redirect(Request.Page_Load);
            Response.Redirect("Reply.aspx");

        }
    }


    protected void btn_Submit_Close_Click(object sender, EventArgs e)
    {

        DataTable dtResponse = new DataTable();
        var str_image = "";
        if (CKEditor1.Text.Trim() != "")
        {
            DataTable dt = new DataTable();
            //dbfunction db = new dbfunction();
            //ApiClass api = new ApiClass();
            if (FileUpload1.HasFile)
            {
                HttpPostedFile PostedFile = FileUpload1.PostedFile;
                if (ValidateFileUpload(PostedFile, 5000, ".jpg, .jpeg, .png, .pdf, .docx, .xlsx, .txt, .pdf") == false)
                {
                    return;
                }
                str_image = "support_" + Guid.NewGuid().ToString().Substring(0, 6) + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("../admin/userimage/") + str_image);
            }
            String UsID = Session["userid"].ToString();
            var ret = objsupport.InsertSupportTicketReply(Convert.ToInt32(UsID), Convert.ToInt32(Session["TicketNo"]), "", CKEditor1.Text, str_image);


            // Response.Redirect(Request.Page_Load);
            // Response.Redirect("Reply.aspx");

        }
    }


    protected void btn_Close_Click(object sender, EventArgs e)
    {



        DataTable dtResponse = new DataTable();
        var str_image = "";
        if (CKEditor1.Text.Trim() != "")
        {
            DataTable dt = new DataTable();

            String UsID = Session["userid"].ToString();
            var tno = Convert.ToInt32(Session["TicketNo"]);
            var ret = objsupport.UpdateTicketStatus(tno, 2);


        }


    }





    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }
}