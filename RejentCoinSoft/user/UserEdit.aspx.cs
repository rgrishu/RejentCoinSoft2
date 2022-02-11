﻿using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_UserEdit : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsBank objbank = new clsBank();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {

                loadcountry();
                //  loadbank();
                loaddata();


            }
            else
            {
                Server.Transfer("logout.aspx");
            }
        }
    }
    void loaddata()
    {
        objUser.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();
        dt = objUser.get_UserDetailNew(objUser);
        if (dt.Rows.Count > 0)
        {
            txtsponserid.Text = dt.Rows[0]["sponserid"].ToString();
            loadsusername();

            txtname.Text = dt.Rows[0]["username"].ToString();
            txtmobile.Text = dt.Rows[0]["mobile"].ToString();
            txtemail.Text = dt.Rows[0]["email"].ToString();
            //ddgender.SelectedValue = dt.Rows[0]["gender"].ToString();
            txtaddress.Text = dt.Rows[0]["address"].ToString();
            ddcountry.SelectedValue = dt.Rows[0]["countryid"].ToString();
            loadstate();
            ddstate.SelectedValue = dt.Rows[0]["stateid"].ToString();

            txtcityname.Text = dt.Rows[0]["cityname"].ToString();
            txttrustwalletaddress.Text = dt.Rows[0]["TrustWalletAddress"].ToString();
            //txtareaname.Text = dt.Rows[0]["areaname"].ToString();
            //txtpincode.Text = dt.Rows[0]["pincode"].ToString();
            //try
            //{
            //    txtdateofbirth.Text = Convert.ToDateTime(dt.Rows[0]["dateofbirth"].ToString()).ToString("dd/MM/yyyy");
            //}
            //catch { }
            //txtnomineename.Text = dt.Rows[0]["nomineename"].ToString(); ;
            //txtnomineerelation.Text = dt.Rows[0]["nomineerelation"].ToString(); ;
            //txtaccountholdername.Text = dt.Rows[0]["accountholdername"].ToString(); ;
            //txtaccountno.Text = dt.Rows[0]["accountno"].ToString(); 
            //txtpan.Text = dt.Rows[0]["pannumber"].ToString(); 
            //txtifsccode.Text = dt.Rows[0]["ifsccode"].ToString(); 
            //txtbranchname.Text = dt.Rows[0]["branchname"].ToString(); 
            //ddbank.SelectedValue = dt.Rows[0]["bankname"].ToString();
            //txtpaytmmobileno.Text = dt.Rows[0]["PaytmMobileNo"].ToString();

            if (dt.Rows[0]["activestatus"].ToString() == "1")
            {
                txtmobile.Enabled = false;
                txtemail.Enabled = false;
                txtname.Enabled = false;
            }
            //if (txtifsccode.Text.Trim() != "")
            //    {
            //        txtifsccode.Enabled = false;
            //    }
            //    if (txtaccountholdername.Text.Trim() != "")
            //    {
            //        txtaccountholdername.Enabled = false;
            //    }
            //    if (txtaccountno.Text.Trim() != "")
            //    {
            //        txtaccountno.Enabled = false;
            //    }
            //    if (txtbranchname.Text.Trim() != "")
            //    {
            //        txtbranchname.Enabled = false;
            //    }
            //    if (txtpan.Text.Trim() != "")
            //    {
            //        txtpan.Enabled = false;
            //    }

            ltemailverifystatus.Text = dt.Rows[0]["emailverifystatus2"].ToString();
            ltmobileverifystatus.Text = dt.Rows[0]["mobileverifystatus2"].ToString();


        }
    }
    //void loadbank()
    //{
    //    ddbank.Items.Clear();
    //    DataTable dt = new DataTable();
    //    dt = objbank.getBank();
    //    ddbank.DataSource = dt;
    //    ddbank.DataTextField = "BankName";
    //    ddbank.DataValueField = "BankID";
    //    ddbank.DataBind();
    //    ListItem li = new ListItem("Select Bank", "0");
    //    ddbank.Items.Insert(0, li);
    //}
    void loadcountry()
    {
        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CountryName";
        ddcountry.DataValueField = "CountryID";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountry.Items.Insert(0, li);
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountry.SelectedValue.ToString();
        dt = objState.getState(objState);

        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // objUser.Password = txtuserpassword.Text;

        // objUser.UserId = Session["userid"].ToString();
        //string resnew = objUser.ValidatePassword(objUser);
        //if (resnew == "t")
        //{
        //    objUser.Mobile = Session["usermobile"].ToString();
        //    string res = objUser.SendOTP(objUser);
        //    if (res != "0" && res != "f")
        //    {
        //        Session["userotp"] = res;
        //        string popupScript2 = "showModal();";
        //        ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        //    }
        //    else
        //    {
        //        Message.Show("Invalid Login Detail");
        //    }
        //   // updatedetails();
        //}
        //if (resnew == "f")
        //{
        //    string popupScript = "toastr.error('Error', Invalid Password');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
        updatedetails();
    }
    void updatedetails()
    {
        objUser.UserId = Session["userid"].ToString();
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        //objUser.Gender = ddgender.SelectedValue.ToString();
        objUser.Address = txtaddress.Text;
        objUser.CityName = txtcityname.Text;
        objUser.CountryId = ddcountry.SelectedValue.ToString();
        objUser.StateId = ddstate.SelectedValue.ToString();
        //objUser.AreaName = txtareaname.Text;
        //objUser.Pincode = txtpincode.Text;
        //objUser.DateOfBirth = Message.GetIndianDate(txtdateofbirth.Text);
        objUser.MentionBy = Session["userid"].ToString();
        //objUser.NomineeName = txtnomineename.Text;
        //objUser.NomineeRelation = txtnomineerelation.Text;
        //objUser.AccHolderName = txtaccountholdername.Text;
        //objUser.AccNo = txtaccountno.Text;
        //objUser.IFSCCode = txtifsccode.Text;
        //objUser.PanCardNo = txtpan.Text;
        //objUser.BankName = ddbank.SelectedValue.ToString();
        //objUser.BranchName = txtbranchname.Text;
        //objUser.PaytmMobileNo = txtpaytmmobileno.Text;
        objUser.ImageName = "";
        objUser.TrustWalletAddress = txttrustwalletaddress.Text;
        string res = objUser.Update_UserProfilenew(objUser);
        if (res == "f")
        {
            string popupScript = "toastr.error('Error', 'User Not Found.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
            if (res == "0")
        {
            string popupScript = "toastr.error('Error', 'Unknow error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "toastr.success('Success', 'User Details Updated Successfully.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        string popupScript2 = "Closepopup();";
        ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstate();
    }

    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = Session["userid"].ToString();
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtsponsername.Text = dt.Rows[0]["username"].ToString();
            if (txtsponserid.Text.ToUpper() == "INDIA01")
            {
                txtsponserid.Text = "******";
                txtsponsername.Text = "******";
            }
        }
        else
        {
            txtsponsername.Text = "";
            txtsponserid.Text = "";
            string popupScript = "toastr.error('Error', 'Invalid User Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Server.Transfer("Dashboard.aspx");
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (Session["mobileverificationotp"].ToString() == txtotp.Text)
        {
            objUser.UserId = Session["userid"].ToString();
            string res = objUser.Update_MobileVerificationStatus(objUser);
            if (res == "t")
            {
                string popupScript = "toastr.success('Success', 'Mobile No Verified Successfully.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

                string popupScript2 = "Closepopup();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
                loaddata();
            }
            else
            {
                string popupScript = "toastr.error('Error', 'Unknown Error Occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

        }
        else
        {
            string popupScript = "toastr.error('Error', 'Invalid OTP');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "showModal();";
            ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
    }

    protected void lbVerifyMobile_Click(object sender, EventArgs e)
    {
        Random rnd = new Random();
        string str_otp = rnd.Next(100000, 999999).ToString();
        objUser.OTP = str_otp;
        objUser.UserId = Session["userid"].ToString();
        string res = objUser.SendOTP(objUser);
        if (res == "t")
        {
            Session["mobileverificationotp"] = str_otp;
            string popupScript2 = "showModal();";
            ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);

        }
        else if (res == "f")
        {
            string popupScript = "toastr.error('Error', 'No User Found');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "toastr.error('Error', 'Unknown Error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }


    }

    protected void lbVerifyEmail_Click(object sender, EventArgs e)
    {
        //string body = "";
        string str_data = Guid.NewGuid().ToString().Substring(0, 20);
        objUser.Data = str_data;
        objUser.UserId = Session["userid"].ToString();
        string rs = objUser.Insert_EmailVerification(objUser);
        if (rs == "t")
        {


            //using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/emailverify.html")))
            //{
            //    body = reader.ReadToEnd();
            //}
            objUser.UserId = Session["userid"].ToString();
            objUser.EmailSubject = "Rijent- Email Verification";
            objUser.EmailBody = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
   <head>
      <meta charset=""UTF-8"">
      <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
      <meta name=""x-apple-disable-message-reformatting"">
      <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
      <meta content=""telephone=no"" name=""format-detection"">
      <title></title>
   </head>
  

   <body>
       <style type=""text/css"">
           #outlook a {
               padding: 0
           }

           .ExternalClass {
               width: 100%
           }

           .brt {
               border-top: 1px solid #eaeaea
           }

           .verOTP {
               font-size: 32px;
               color: #ed8e20;
               font-weight: 600
           }

           .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
               line-height: 100%
           }

           .es-button {
               mso-style-priority: 100 !important;
               text-decoration: none !important
           }

           a[x-apple-data-detectors] {
               color: inherit !important;
               text-decoration: none !important;
               font-size: inherit !important;
               font-family: inherit !important;
               font-weight: inherit !important;
               line-height: inherit !important
           }

           .es-desk-hidden {
               display: none;
               float: left;
               overflow: hidden;
               width: 0;
               max-height: 0;
               line-height: 0;
               mso-hide: all
           }

           [data-ogsb] .es-button {
               border-width: 0 !important;
               padding: 15px 30px 15px 30px !important
           }

           s {
               text-decoration: line-through
           }

           html, body {
               width: 100%;
               font-family: 'open sans','helvetica neue',helvetica,arial,sans-serif;
               -webkit-text-size-adjust: 100%;
               -ms-text-size-adjust: 100%
           }

           table {
               mso-table-lspace: 0;
               mso-table-rspace: 0;
               border-collapse: collapse;
               border-spacing: 0
           }

               table td, html, body, .es-wrapper {
                   padding: 0;
                   Margin: 0
               }

           .es-content, .es-header, .es-footer {
               table-layout: fixed !important;
               width: 100%
           }

           img {
               display: block;
               border: 0;
               outline: 0;
               text-decoration: none;
               -ms-interpolation-mode: bicubic
           }

           table tr {
               border-collapse: collapse
           }

           p, hr {
               Margin: 0
           }

           h1, h2, h3, h4, h5 {
               Margin: 0;
               line-height: 120%;
               mso-line-height-rule: exactly;
               font-family: 'open sans','helvetica neue',helvetica,arial,sans-serif
           }

           p, ul li, ol li, a {
               -webkit-text-size-adjust: none;
               -ms-text-size-adjust: none;
               mso-line-height-rule: exactly
           }

           .es-left {
               float: left
           }

           .es-right {
               float: right
           }

           .es-p5 {
               padding: 5px
           }

           .es-p5t {
               padding-top: 5px
           }

           .es-p5b {
               padding-bottom: 5px
           }

           .es-p5l {
               padding-left: 5px
           }

           .es-p5r {
               padding-right: 5px
           }

           .es-p10 {
               padding: 10px
           }

           .es-p10t {
               padding-top: 10px
           }

           .es-p10b {
               padding-bottom: 10px
           }

           .es-p10l {
               padding-left: 10px
           }

           .es-p10r {
               padding-right: 10px
           }

           .es-p15 {
               padding: 15px
           }

           .es-p15t {
               padding-top: 15px
           }

           .es-p15b {
               padding-bottom: 15px
           }

           .es-p15l {
               padding-left: 15px
           }

           .es-p15r {
               padding-right: 15px
           }

           .es-p20 {
               padding: 20px
           }

           .es-p20t {
               padding-top: 20px
           }

           .es-p20b {
               padding-bottom: 20px
           }

           .es-p20l {
               padding-left: 20px
           }

           .es-p20r {
               padding-right: 20px
           }

           .es-p25 {
               padding: 25px
           }

           .es-p25t {
               padding-top: 25px
           }

           .es-p25b {
               padding-bottom: 25px
           }

           .es-p25l {
               padding-left: 25px
           }

           .es-p25r {
               padding-right: 25px
           }

           .es-p30 {
               padding: 30px
           }

           .es-p30t {
               padding-top: 30px
           }

           .es-p30b {
               padding-bottom: 30px
           }

           .es-p30l {
               padding-left: 30px
           }

           .es-p30r {
               padding-right: 30px
           }

           .es-p35 {
               padding: 35px
           }

           .es-p35t {
               padding-top: 35px
           }

           .es-p35b {
               padding-bottom: 35px
           }

           .es-p35l {
               padding-left: 35px
           }

           .es-p35r {
               padding-right: 35px
           }

           .es-p40 {
               padding: 40px
           }

           .es-p40t {
               padding-top: 40px
           }

           .es-p40b {
               padding-bottom: 40px
           }

           .es-p40l {
               padding-left: 40px
           }

           .es-p40r {
               padding-right: 40px
           }

           .es-menu td {
               border: 0
           }

               .es-menu td a img {
                   display: inline-block !important
               }

           a {
               text-decoration: none
           }

           p, ul li, ol li {
               font-family: 'open sans','helvetica neue',helvetica,arial,sans-serif;
               line-height: 150%
           }

           ul li, ol li {
               Margin-bottom: 15px
           }

           .es-menu td a {
               text-decoration: none;
               display: block;
               font-family: 'open sans','helvetica neue',helvetica,arial,sans-serif
           }

           .es-wrapper {
               width: 100%;
               height: 100%;
               background-image:;
               background-repeat: repeat;
               background-position: center top
           }

           .es-wrapper-color {
               background-color: #eee
           }

           .es-header {
               background-color: transparent;
               background-image:;
               background-repeat: repeat;
               background-position: center top
           }

           .es-header-body {
               background-image: linear-gradient(to right,#333 0,#495057 50%,#333 100%)
           }

               .es-header-body p, .es-header-body ul li, .es-header-body ol li {
                   color: #fff;
                   font-size: 14px
               }

               .es-header-body a {
                   color: #fff;
                   font-size: 14px
               }

           .es-content-body {
               background-color: #fff
           }

               .es-content-body p, .es-content-body ul li, .es-content-body ol li {
                   color: #333;
                   font-size: 16px
               }

               .es-content-body a {
                   color: #ed8e20;
                   font-size: 16px
               }

           .es-footer {
               background-color: transparent;
               background-image:;
               background-repeat: repeat;
               background-position: center top
           }

           .es-footer-body {
               background-color: #fff
           }

               .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li {
                   color: #333;
                   font-size: 14px
               }

               .es-footer-body a {
                   color: #333;
                   font-size: 14px
               }

           .es-infoblock, .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li {
               line-height: 120%;
               font-size: 12px;
               color: #ccc
           }

               .es-infoblock a {
                   font-size: 12px;
                   color: #ccc
               }

           h1 {
               font-size: 36px;
               font-style: normal;
               font-weight: bold;
               color: #333
           }

           h2 {
               font-size: 30px;
               font-style: normal;
               font-weight: bold;
               color: #333
           }

           h3 {
               font-size: 18px;
               font-style: normal;
               font-weight: normal;
               color: #333
           }

           .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {
               font-size: 36px
           }

           .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {
               font-size: 30px
           }

           .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {
               font-size: 18px
           }

           a.es-button, button.es-button {
               border-radius: 6px;
               font-size: 20px;
               font-family: 'open sans','helvetica neue',helvetica,arial,sans-serif;
               font-weight: normal;
               font-style: normal;
               color: #fff;
               width: auto;
               text-align: center;
               background: linear-gradient( 90deg,#ffbd39 0,#fa9531 100%);
               padding: 12px 25px;
           }

           .es-button-border {
               border-style: solid solid solid solid;
               border-color: transparent transparent transparent transparent;
               background: #66b3b7;
               border-width: 0;
               display: inline-block;
               border-radius: 5px;
               width: auto
           }

           @media only screen and (max-width:600px) {
               p, ul li, ol li, a {
                   line-height: 150% !important
               }

               h1, h2, h3, h1 a, h2 a, h3 a {
                   line-height: 120% !important
               }

               h1 {
                   font-size: 32px !important;
                   text-align: center
               }

               h2 {
                   font-size: 26px !important;
                   text-align: center
               }

               h3 {
                   font-size: 20px !important;
                   text-align: center
               }

               .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {
                   font-size: 32px !important
               }

               .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {
                   font-size: 26px !important
               }

               .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {
                   font-size: 20px !important
               }

               .es-menu td a {
                   font-size: 16px !important
               }

               .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {
                   font-size: 16px !important
               }

               .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a {
                   font-size: 16px !important
               }

               .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {
                   font-size: 16px !important
               }

               .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {
                   font-size: 12px !important
               }

               *[class=""gmail-fix""] {
                   display: none !important
               }

               .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {
                   text-align: center !important
               }

               .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {
                   text-align: right !important
               }

               .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {
                   text-align: left !important
               }

                   .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {
                       display: inline !important
                   }

               .es-button-border {
                   display: inline-block !important
               }

               a.es-button, button.es-button {
                   font-size: 16px !important;
                   display: inline-block !important;
                   border-width: 15px 30px 15px 30px !important
               }

               .es-btn-fw {
                   border-width: 10px 0 !important;
                   text-align: center !important
               }

               .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right {
                   width: 100% !important
               }

               .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {
                   width: 100% !important;
                   max-width: 600px !important
               }

               .es-adapt-td {
                   display: block !important;
                   width: 100% !important
               }

               .adapt-img {
                   width: 100% !important;
                   height: auto !important
               }

               .es-m-p0 {
                   padding: 0 !important
               }

               .es-m-p0r {
                   padding-right: 0 !important
               }

               .es-m-p0l {
                   padding-left: 0 !important
               }

               .es-m-p0t {
                   padding-top: 0 !important
               }

               .es-m-p0b {
                   padding-bottom: 0 !important
               }

               .es-m-p20b {
                   padding-bottom: 20px !important
               }

               .es-mobile-hidden, .es-hidden {
                   display: none !important
               }

               tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {
                   width: auto !important;
                   overflow: visible !important;
                   float: none !important;
                   max-height: inherit !important;
                   line-height: inherit !important
               }

               tr.es-desk-hidden {
                   display: table-row !important
               }

               table.es-desk-hidden {
                   display: table !important
               }

               td.es-desk-menu-hidden {
                   display: table-cell !important
               }

               .es-menu td {
                   width: 1% !important
               }

               table.es-table-not-adapt, .esd-block-html table {
                   width: auto !important
               }

               table.es-social {
                   display: inline-block !important
               }

                   table.es-social td {
                       display: inline-block !important
                   }
           }

           .es-p-default {
               padding-top: 20px;
               padding-right: 35px;
               padding-bottom: 0;
               padding-left: 35px
           }

           .es-p-all-default {
               padding: 0
           }

           .emailTitle p {
               line-height: 30px
           }

           .text-left {
               text-align: left;
           }
       </style>
       <div class=""es-wrapper-color"">
           <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"">
               <tbody>
                   <tr>
                       <td class=""esd-email-paddings"" valign=""top"">
                           <table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                               <tbody>
                                   <tr></tr>
                                   <tr>
                                       <td class=""esd-stripe"" esd-custom-block-id=""7681"" align=""center"">
                                           <table class=""es-header-body"" style=""background-image: linear-gradient(to right, #333 0%, #495057 50%, #333 100%);"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#044767"" align=""center"">
                                               <tbody>
                                                   <tr>
                                                       <td class=""esd-structure es-p15t es-p15b es-p35r es-p35l"" align=""left"">
                                                           <!--[if mso]>
                                                        <table width=""530"" cellpadding=""0"" cellspacing=""0"">
                                                           <tr>
                                                              <td width=""340"" valign=""top"">
                                                                 <![endif]-->
                                                           <table class=""es-left"" cellspacing=""0"" cellpadding=""0"" align=""left"">
                                                               <tbody>
                                                                   <tr>
                                                                       <td class=""es-m-p0r es-m-p20b esd-container-frame"" width=""340"" valign=""top"" align=""center"">
                                                                           <table width=""100%"" cellspacing=""0"" cellpadding=""0"">
                                                                               <tbody>
                                                                                   <tr>
                                                                                       <td class=""esd-block-text es-m-txt-c"" align=""left"">
                                                                                           <!--h1 style=""color: #ffffff; line-height: 100%;"">Beretun</h1-->
                                                                                           <img src=""http://teamrijent.in/EmailTemplate/images/Rijentlogo.png"" width=""240"" />
                                                                                       </td>
                                                                                   </tr>
                                                                               </tbody>
                                                                           </table>
                                                                       </td>
                                                                   </tr>
                                                               </tbody>
                                                           </table>
                                                           <table cellspacing=""0"" cellpadding=""0"" align=""right"">
                                                               <tbody>
                                                                   <tr class=""es-hidden"">
                                                                       <td class=""es-m-p20b esd-container-frame"" esd-custom-block-id=""7704"" width=""170"" align=""left"">
                                                                           <table width=""100%"" cellspacing=""0"" cellpadding=""0""></table>
                                                                       </td>
                                                                   </tr>
                                                               </tbody>
                                                           </table>
                                                           <!--[if mso]>
                                                              </td>
                                                           </tr>
                                                        </table>
                                                        <![endif]-->
                                                       </td>
                                                   </tr>
                                               </tbody>
                                           </table>
                                       </td>
                                   </tr>
                               </tbody>
                           </table>
                           <table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                               <tbody>
                                   <tr>
                                       <td class=""esd-stripe"" align=""center"">
                                           <table class=""es-content-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"">
                                               <tbody>
                                                   <tr>
                                                       <td class=""esd-structure es-p30t es-p35r es-p35l"" align=""left"">
                                                           <table width=""100%"" cellspacing=""0"" cellpadding=""0"">
                                                               <tbody>
                                                                   <tr>
                                                                       <td class=""esd-container-frame"" width=""530"" valign=""top"" align=""center"">
                                                                           <table width=""100%"" cellspacing=""0"" cellpadding=""0"">
                                                                               <tbody>
                                                                                   <tr>
                                                                                       <td class=""text-left esd-block-text es-p10b"" align=""center"">
                                                                                           <h2>Email Verification</h2><br />
                                                                                       </td>
                                                                                   </tr>
                                                                                   <tr>
                                                                                       <td class=""emailTitle esd-block-text es-p15t es-p20b"" align=""left"">
                                                                                           <p>
                                                                                               Hi <b>" + txtname.Text + @",</b> <br /><br />
                                                                                               We just need to verify your email address before you can access customer email address: <a href=""#!"">" + txtemail.Text + @"</a>
                                                                                           </p><br />
                                                                                           <p>Please confirm your email address.</p><br />
                                                                                           <p><span class=""es-button-border""><a href=""http://teamrijent.in/EmailVerification.aspx?d=" + str_data + @""" class=""es-button"" target=""_blank"">Verify Now</a></span></p><br /><br />
                                                                                           <p>If you don't recognize this activity, please contact us immediately at <a href=""https://teamrijent.in/"" target=""_blank"">https://teamrijent.in/</a> </p><br /><br />

                                                                                           <p><b>Rijent Coin Team</b><br />This is automated generated mail, please don't reply.</p>
                                                                                       </td>
                                                                                   </tr>
                                                                                   <br />
                                                                                   <tr>
                                                                                       <td class=""esd-block-image es-p15b"" align=""center"" style=""font-size:0""><a target=""_blank"" href=""""><img src=""http://teamrijent.in/EmailTemplate/images/play-store.png"" alt=""Beretun logo"" style=""display: block;"" title=""Beretun logo"" width=""180"" /></a><p><strong>Download Mobile App </strong></p></td>
                                                                                   </tr>
                                                                               </tbody>
                                                                           </table>
                                                                       </td>
                                                                   </tr>
                                                               </tbody>
                                                           </table>
                                                       </td>
                                                   </tr>
                                               </tbody>
                                           </table>
                                       </td>
                                   </tr>
                               </tbody>
                           </table>
                           <table cellpadding=""0"" cellspacing=""0"" class=""es-footer"" align=""center"">
                               <tbody>
                                   <tr>
                                       <td class=""esd-stripe"" esd-custom-block-id=""7684"" align=""center"">
                                           <table class=""es-footer-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                                               <tbody>
                                                   <tr>
                                                       <td class=""esd-structure es-p25t brt es-p20b es-p35r es-p35l"" align=""left"">
                                                           <table width=""100%"" cellspacing=""0"" cellpadding=""0"">
                                                               <tbody>
                                                                   <tr>
                                                                       <td class=""esd-container-frame"" width=""530"" valign=""top"" align=""center"">
                                                                           <table width=""100%"" cellspacing=""0"" cellpadding=""0"">
                                                                               <tbody>
                                                                                   <tr>
                                                                                       <td esdev-links-color=""#777777"" align=""left"" class=""esd-block-text es-m-txt-c es-p5b"">
                                                                                           <p style=""text-align: center;"">Copyright  2021 <a href=""https://teamrijent.in/"" target=""_blank""><b>   Rijent Coin</b></a> All Rights Reserved.</p>
                                                                                       </td>
                                                                                   </tr>
                                                                               </tbody>
                                                                           </table>
                                                                       </td>
                                                                   </tr>
                                                               </tbody>
                                                           </table>
                                                       </td>
                                                   </tr>
                                               </tbody>
                                           </table>
                                       </td>
                                   </tr>
                               </tbody>
                           </table>
                       </td>
                   </tr>
               </tbody>
           </table>
       </div>
   </body>
</html>";
            string res = objUser.SendEmailVerification(objUser);
            if (res == "t")
            {

                string popupScript = "toastr.success('Success', 'Verification Email Sent To Your Email.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "toastr.error('Error', 'Unknown Error Occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
    }

    protected void btnUpdateaddress_Click(object sender, EventArgs e)
    {

        objUser.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();
        dt = objUser.getEmailMobileVerifyStatus(objUser);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["emailverifystatus"].ToString() == "1" && dt.Rows[0]["mobileverifystatus"].ToString() == "1")
            {
                Random rnd = new Random();
                string str_otpemail = rnd.Next(100000, 999999).ToString();
                hdnOTP.Value = str_otpemail;
                objUser.OTP = str_otpemail;
                objUser.UserId = Session["userid"].ToString();
                objUser.EmailSubject = "Rijent- Trust Wallet Address OTP";
                objUser.EmailBody = @"Dear User OTP is " + str_otpemail;

                objUser.OTP = str_otpemail;
                objUser.UserId = Session["userid"].ToString();
                string res2 = objUser.SendOTP(objUser);

                string res = objUser.SendEmailVerification(objUser);
                {
                    string popupScript = "toastr.success('Success', 'OTP has been Sent To Your Email.');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    string popupScript2 = "showModalOTPAddress();";
                    ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
                }
            }
            else
            {
                string popupScript = "toastr.error('Error', 'please verify your email and mobile to update wallet address');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

        }
        else
        {
            string popupScript = "toastr.error('Error', 'please verify your email and mobile to update wallet address');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }

       
    }

    protected void btnOTPAddress_Click(object sender, EventArgs e)
    {
        if (hdnOTP.Value.ToString() == txtotpaddress.Text)
        {
            objUser.UserId = Session["userid"].ToString();
            objUser.TrustWalletAddress = txttrustwalletaddress.Text;
            string res = objUser.Update_TrustWalletAddress(objUser);
            if (res == "t")
            {
                string popupScript = "toastr.success('Success', 'Address Updated Sucessfully.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript2 = "ClosepopupOTPAddress();";
                ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            }

            else if (res == "f")
            {
                string popupScript2 = "showModalOTPAddress();";
                ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
                string popupScript = "toastr.error('Error', 'Wallet address already assinged to other id');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript2 = "showModalOTPAddress();";
                ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
                string popupScript = "toastr.error('Error', 'Unknown Error Occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        else
        {
            string popupScript2 = "showModalOTPAddress();";
            ScriptManager.RegisterStartupScript(uplMaster, uplMaster.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            string popupScript = "toastr.error('Error', 'Invalid OTP');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
}