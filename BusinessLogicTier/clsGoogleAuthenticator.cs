using DataTier;
using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTier
{
   

    public class GoogleAuthenticatorManager
    {
        private const string Issuer = "RoundpayVoiceTech";
        private readonly string ProjectName;
        Data ObjData = new Data();
        public GoogleAuthenticatorManager()
        {
            //ProjectName = this.GetType().Assembly.FullName.Split(',')[0];
            ProjectName = "RejentCoin";
        }

        public SetupCode1 LoadSharedKeyAndQrCodeUrl(string userId, string AuthenticatorKey = "")
        {
            var guId = Guid.NewGuid();
            StringBuilder sb = new StringBuilder(guId.ToString());
            sb.Replace("-", "").Replace("{", "").Replace("}", "");
            AuthenticatorKey = String.IsNullOrEmpty(AuthenticatorKey) ? sb.ToString() : AuthenticatorKey;
            TwoFactorAuthenticator Authenticator = new TwoFactorAuthenticator();
            //var SetupResult = Authenticator.GenerateSetupCode(Issuer, ProjectName, AuthenticatorKey, 250, 250);
            var SetupResult = Authenticator.GenerateSetupCode(ProjectName, userId, AuthenticatorKey, false, 250);
            return new SetupCode1() {SetupResult= SetupResult,AuthenticatorKey= AuthenticatorKey };


        }



        public DataTable Enable_GoogleAuthenticator(int userid,bool status,string AccountSecretKey)
        {

            string str_query = "";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                str_query = "update  logindetail set _Is_Google_2FA_Enable=@status,AccountSecretKey=IIF(@status=0,AccountSecretKey,@AccountSecretKey) where USERNAME=@username select 1 as status";
                SqlParameter[] parameter = {
                new SqlParameter("@username", userid),
                new SqlParameter("@status", status),
                new SqlParameter("@AccountSecretKey", AccountSecretKey),

                };

                dt = ObjData.RunDataTableParam(str_query, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;


        }


        public DataTable Reset_GoogleAuthenticator(int userid)
        {

            string str_query = "";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                str_query = "update  logindetail set _Is_Google_2FA_Enable=0,AccountSecretKey='' where USERNAME=@username select 1 as status";
                SqlParameter[] parameter = {
                new SqlParameter("@username", userid),
                };

                dt = ObjData.RunDataTableParam(str_query, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;


        }

        public string formattedString(string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (i % 4 == 0)
                    sb.Append(' ');
                sb.Append(input[i]);
            }
            return sb.ToString();
        }

    }

    public class GoogleAuthenticatorModal
    {
        public int StatusCode { get; set; }
        public string Msg { get; set; }
        public string Account { get; set; }
        public string AccountSecretKey { get; set; }
        public string ManualEntryKey { get; set; }
        public string QrCodeSetupImageUrl { get; set; }
        public bool AlreadyRegistered { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class SetupCode1
        {
        public SetupCode SetupResult { get; set; }
        public string AuthenticatorKey { get; set; }
        }

   



}
