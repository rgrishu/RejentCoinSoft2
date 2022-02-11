using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using DataTier;

public partial class admin_index : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsLogin objlogin = new clsLogin();
    clsUser objuser = new clsUser();
    clsNews objnews = new clsNews();
    string _TID = string.Empty;
    string _Option1 = string.Empty;
    string _Option2 = string.Empty;
    string _Option3 = string.Empty;
    string _Option4 = string.Empty;
    string _Option5 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["TID"] != null && Request.QueryString["TID"] != string.Empty)
        {
            _TID = Request.QueryString["TID"].ToString();
            _Option1 = Request.QueryString["Option1"].ToString();
            _Option2 = Request.QueryString["Option2"].ToString();
            _Option3 = Request.QueryString["Option3"].ToString();
            _Option4 = Request.QueryString["Option4"].ToString();
            _Option5 = Request.QueryString["Option5"].ToString();
            int chkVal = CheckCoinTransaction(GetIPAddress(), _TID , _Option1 , _Option2 , _Option3 , _Option4 , _Option5);
            if (chkVal == 1)
            {
                
                Response.Write("{\"status\": " + chkVal.ToString() + ", \"msg\":\"SUCCESS!\"}");
            }
            else
            {
                
                Response.Write("{\"status\": " + chkVal.ToString() + ", \"msg\":\"TID or IP is invalid!\"}");
            }

        }

    }


    public string GetIPAddress()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }


    public int CheckCoinTransaction(String IP, String TransactionID , string Option1 , string Option2 , string Option3 , string Option4 , string Option5)
    {

        int jsonStr=-1;
        try
        {

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_CheckCoinTransactionID";
                SqlParameter[] parameter = {
                   new SqlParameter("@TrnsactionId",  TransactionID),
                   new SqlParameter("@Option1",  Option1),
                   new SqlParameter("@Option2",  Option2),
                   new SqlParameter("@Option3",  Option3),
                   new SqlParameter("@Option4",  Option4),
                   new SqlParameter("@Option5",  Option5),
                   new SqlParameter("@IP",  IP)
                };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {

            }
            ObjData.EndConnection();
            jsonStr = Convert.ToInt32(dt.Rows[0][0]);
        }
        catch (Exception ex)
        {
            //objB2BSecureServiceDL.B2B_Error_Log("SecureServiceLogin", ex.Message, "Login");
        }
        return jsonStr;
    }


}