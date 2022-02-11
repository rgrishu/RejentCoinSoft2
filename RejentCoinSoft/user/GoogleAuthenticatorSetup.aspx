<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="usermaster.master" CodeFile="GoogleAuthenticatorSetup.aspx.cs" Inherits="GoogleAuthenticatorSetup" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal {
            overflow-y: auto !important;
        }
    </style>

     <style type="text/css">
        .switch
        {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 24px;
        }
         
        .switch input
        {
            opacity: 0;
        }
         
        .slider
        {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }
         
        .slider:before
        {
            position: absolute;
            content: "";
            height: 16px;
            width: 16px;
            left: 4px;
            bottom: 4px;
            background-color: white;
            -webkit-transition: .4s;
            transition: .4s;
        }
         
        input:checked + .slider
        {
            background-color: #2196F3;
        }
         
        input:focus + .slider
        {
            box-shadow: 0 0 1px #2196F3;
        }
         
        input:checked + .slider:before
        {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }
         
        /* Rounded sliders */
        .slider.round
        {
            border-radius: 34px;
        }
         
        .slider.round:before
        {
            border-radius: 50%;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentHeader" runat="Server">
    <h4 class="page-title">Dashboard</h4>
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="Dashboard.aspx">Profile</a></li>
        <li class="breadcrumb-item active" aria-current="page">2FA</li>
    </ol>
</asp:Content>

<asp:Content ID="contentDesc" ContentPlaceHolderID="contentData" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="card" id="InsertOtpvalid" runat="server">
        <div class="card-header">
            <h5>OTP (Your otp is send on your email) </h5>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col">
                    <div class="mb-3 row">
                        <label class="col-sm-2 col-form-label">Enter OTP</label>
                        <div class="col-sm-5">
                            <asp:TextBox type="number" class="form-control" placeholder="Please enter otp" name="otp" ID="txtOTP" runat="server" />
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="Button1" runat="server" OnClick="InsertOTP_Click" CssClass="btn btn-light" Text="Submit" />
                        </div>
                    </div>
                </div>
            </div>

            
        </div>
    </div>

    <div class="col-md-12" id="myalert"></div>
    <div class="card" runat="server" id="ISOtpvalid">
        <div class="card-header">
            <h5>Google Authenticator</h5>
        </div>
        <div class="card-body">
            <h4>Enable authenticator powered by <span>
                <img src="img/googlelogo_color_272x92dp.png" style="height: 25px" />
            </span>
            </h4>
            <div>
                <p>To use an authenticator app go through the following steps:</p>
                <ol class="list">
                    <li>
                        <p class="mb-2">
                            Download a two-factor Google Authenticator for
                    <a href="https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2&amp;hl=en" target="_blank">Android</a> and
                    <a href="https://itunes.apple.com/us/app/google-authenticator/id388497605?mt=8" target="_blank">iOS</a>.
                        </p>
                    </li>
                    <li>
                        <p class="mb-2">
                            Scan the QR Code or enter this key <kbd>
                                <asp:Label ID="txtappkey" runat="server"></asp:Label></kbd> into your two factor authenticator app. Spaces and casing do not matter.
                        </p>
                        <asp:Image ID="Image1" runat="server" Style="width: 150px; object-fit: cover" />
                    </li>
                    <li>
                        <p class="mb-2">
                            Once you have scanned the QR code or input the key above, your two factor authentication app will provide you
                    with a unique code. Enter the code in the confirmation box below.
                        </p>


                        <div class="row" id="isRegestred" runat="server">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label" for="Code">Verification Code</label>
                                    <div class="input-group">
                                        <input class="form-control" id="txtGooglePin" autocomplete="off" type="text" />
                                        <div class="input-group-append">
                                            <button class="btn btn-outline-dark" id="btnVerifyGOTP">Verify</button>
                                        </div>
                                        <span class="text-danger field-validation-valid"></span>
                                    </div>

                                </div>
                                <input type="hidden" placeholder="Enter Google PIN" id="AccountSecretKey" value="@Model.AccountSecretKey" />
                            </div>
                        </div>

                        <div class="col-sm-4 float-right">
                            <div class="mb-2">
                                <div>
                               
                                       <label class="switch">
            <asp:CheckBox ID="chkEnable" OnCheckedChanged="chkEnable_CheckedChanged" AutoPostBack="true" runat="server"  />
            <span class="slider round"></span>
            </label>

                                    <span id="goptauth" runat="server"></span>
                                   
                                    
                                </div>
          

                                
                            </div>
                            <div class="form-group" runat="server" visible="false" id="de_Googlediv">
                                <div class="input-group">
                                    <asp:TextBox runat="server" class="form-control" TextMode="Number" MaxLength="7" placeholder="Enter Otp To Disabled 2FA" ID="txtOTPForDGAuth" />
                                    <div class="input-group-append">
                                        <asp:Button ID="dactivate_gAuth" runat="server" Text="Disabled 2FA" OnClick="dactivate_gAuth_Click" CssClass="btn btn-primary has-ripple" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Button class="btn btn-block btn-outline-dark" Text="Reset" OnClick="resetGoogleAuth_Click" runat="server" ID="resetGoogleAuth" />
                            </div>
                        </div>

                    </li>
                </ol>
            </div>
        </div>
    </div>

</asp:Content>

