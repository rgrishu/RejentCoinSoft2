using BusinessLogicTier;
using System;
using System.Data;

public partial class GoogleAuthenticatorSetup : System.Web.UI.Page
{

    GoogleAuthenticatorManager guManager = new GoogleAuthenticatorManager();
    protected override void OnPreInit(EventArgs e)
    {
        if (Session["type"] != null)
        {
            MasterPageFile = "~/MasterPage2.master";
        }
        if (Session["type"] != null)
        {
            MasterPageFile = "~/MasterPage2.master";
        }



    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                InsertOtpvalid.Visible = true;
                ISOtpvalid.Visible = false;
                GenrateOtp();
            }
            else
            {
                Server.Transfer("logout.aspx");
            }

        }
        else
        {


        }



        //initializeGoogleAuth();
    }



    private void initializeGoogleAuth()
    {

        var userId = Convert.ToString((Session["userid"]));
        var SetupResult = guManager.LoadSharedKeyAndQrCodeUrl(userId, Convert.ToString(Session["AuthenticatorKey"]));
        var response = new GoogleAuthenticatorModal
        {
            AccountSecretKey = SetupResult.AuthenticatorKey,/////
            QrCodeSetupImageUrl = SetupResult.SetupResult.QrCodeSetupImageUrl,
            AlreadyRegistered = !string.IsNullOrEmpty(Convert.ToString(Session["AuthenticatorKey"])) ? true : false,
            IsEnabled = true
        };
        txtappkey.Text = response.AccountSecretKey;
        Session["AuthenticatorKey"] = response.AccountSecretKey;
        isRegestred.Visible = false;
        Image1.ImageUrl = response.QrCodeSetupImageUrl;
        ISOtpvalid.Visible = false;
        chkEnable.Checked = Convert.ToBoolean(Session["Is_Google_2FA_Enable"]);
        goptauth.InnerText = Convert.ToBoolean(Session["Is_Google_2FA_Enable"]) ? "Enabled" : "Disabled";


        //return response ?? new GoogleAuthenticatorModal();
    }



    protected void InsertOTP_Click(object sender, EventArgs e)
    {
        var _otp = true;
        var otp = txtOTP.Text;
        if (otp.Length>0 &&Convert.ToInt32(Session["Google_OTP"]) == Convert.ToInt32(otp))
        {
            initializeGoogleAuth();
            InsertOtpvalid.Visible = !_otp;
            ISOtpvalid.Visible = _otp;
        }
        else
        {
            InsertOtpvalid.Visible = _otp;
            ISOtpvalid.Visible = !_otp;

            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Plesae Fill OTP.');", true);
        }



    }

    public void GenrateOtp()
    {
        clsUser objuser = new clsUser();
        Random rnd = new Random();
        string str_otpemail = rnd.Next(100000, 999999).ToString();
        Session["Google_OTP"] = str_otpemail;
        objuser.OTP = str_otpemail;
        objuser.UserId = Session["userid"].ToString();
        objuser.EmailSubject = "Rijent-GooleAuthentictor OTp";
        objuser.EmailBody = @"Dear User OTP is " + str_otpemail;
        string res = objuser.SendEmailVerification(objuser);
    }

    public void Enable_GoogleAuthenticator()
    {
        var userid = Convert.ToInt32(Session["userid"]);
        guManager.Enable_GoogleAuthenticator(userid, true, string.Empty);
        string str_query = "";
        DataTable dt = null;



    }

    protected void chkEnable_CheckedChanged(object sender, EventArgs e)
    {
        var userid = Convert.ToInt32(Session["userid"]);
        if (chkEnable.Checked)
        {

           var a= guManager.Enable_GoogleAuthenticator(userid, true, Convert.ToString(Session["AuthenticatorKey"]));
            Session["Is_Google_2FA_Enable"] = true.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert(Authenticator Enabled.');", true);

        }
        else
        {
            GenrateOtp();
            de_Googlediv.Visible = true;
        }
    }

    protected void dactivate_gAuth_Click(object sender, EventArgs e)
    {
        

        if (txtOTPForDGAuth.Text.Length>0 && Convert.ToInt32(Session["Google_OTP"]) == Convert.ToInt32(txtOTPForDGAuth.Text))
        {
            guManager.Enable_GoogleAuthenticator(Convert.ToInt32(Session["userid"]), false, string.Empty);
            Session["Is_Google_2FA_Enable"] = false.ToString();
            Response.Redirect("GoogleAuthenticatorSetup.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Plesae Fill OTP.');", true);
        }

    }
    protected void resetGoogleAuth_Click(object sender, EventArgs e)
    {


            guManager.Reset_GoogleAuthenticator(Convert.ToInt32(Session["userid"]));
            Session["Is_Google_2FA_Enable"] = false.ToString();
            ///Response.Redirect("GoogleAuthenticatorSetup.aspx");
        
        

    }

   
}
