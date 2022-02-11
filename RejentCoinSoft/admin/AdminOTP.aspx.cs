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
using Google.Authenticator;

public partial class admin_index : System.Web.UI.Page
{
    clsLogin objlogin = new clsLogin();
    clsUser objuser = new clsUser();
    clsNews objnews = new clsNews();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["adminloginotp"] == null)
            {
                Server.Transfer("index.aspx");
            }
        }
        lbl_otp.InnerText = Convert.ToBoolean(Session["Is_Google_2FA_Enable"]) ? "Enter 2FA OTP. " : "Enter OTP and set new password.";
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtotp.Text == Session["adminloginotp"].ToString() && !Convert.ToBoolean(Session["Is_Google_2FA_Enable"]))
            {

                Server.Transfer("Dashboard.aspx");

            }
            else if (Convert.ToBoolean(Session["Is_Google_2FA_Enable"]) && VerifyGoogleAuthenticatorSetup(txtotp.Text, Convert.ToString(Session["AuthenticatorKey"])))
            {
                Server.Transfer("Dashboard.aspx");
            }
            else
            {
                Message.Show("Invalid OTP");
            }
        }
        catch (Exception ep)
        {
            Message.Show("Unknown Error occurred");
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
    public bool VerifyGoogleAuthenticatorSetup(string googlePin, string accountSecretKey)
    {
        TwoFactorAuthenticator Authenticator = new TwoFactorAuthenticator();
        return Authenticator.ValidateTwoFactorPIN(accountSecretKey, googlePin, false);
    }

}