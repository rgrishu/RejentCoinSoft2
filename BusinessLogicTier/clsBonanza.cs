using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTier;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Mime;

namespace BusinessLogicTier
{
    public class clsBonanza
    {
        Data ObjData = new Data();
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public string MentionBy { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        //00 Bonanza
        public string BonanzaID { get; set; }
        public string TBBonanzaID { get; set; }
        public string Designation { get; set; }
        public string Matching_Business { get; set; }
        public string Direct_Business { get; set; }
        public string Sel_Steking { get; set; }
        public string Reward { get; set; }
        public string UserID { get; set; }
        

        //00
        #region ReTopup 
        public string UserName { get; set; }
        public string CappingAmount { get; set; }
        public string CurrentAmount { get; set; }
        public string MentionDate { get; set; }
        public string Mobile { get; set; }

        #endregion


        public DateTime HolidayDate { get; set; }
        public string Test { get; set; }
        public string Test1 { get; set; }
        public class validateMobileApiDate
        {
            public string blocking { get; set; }
            public List<string> contacts { get; set; }
            public bool force_check { get; set; }
        }
        public class ValidateResponse
        {
            public List<Contact> contacts { get; set; }
            public Meta meta { get; set; }
        }
        public class Contact
        {
            public string input { get; set; }
            public string status { get; set; }
            public string wa_id { get; set; }
        }

        public class Meta
        {
            public string api_status { get; set; }
            public string version { get; set; }
        }

        #region Notification Message

        public class Root
        {
            public string to { get; set; }
            public string type { get; set; }
            public Template template { get; set; }
        }
        public class Language
        {
            public string policy { get; set; }
            public string code { get; set; }
        }

        public class Image
        {
            public string link { get; set; }
        }

        public class Parameter
        {
            public string type { get; set; }
            public Image image { get; set; }
            public string text { get; set; }
        }

        public class Component
        {
            public string type { get; set; }
            public List<Parameter> parameters { get; set; }
        }

        public class Template
        {
            public string @namespace { get; set; }
            public Language language { get; set; }
            public string name { get; set; }
            public List<Component> components { get; set; }
        }



        #endregion



        public DataTable getBonanzaAll()
        {
            string str_query = "Select bo.ID,   BonanzaName,Designation	,MatchingBusiness, DirectBusiness,	SelStaking,	Reward	 from [dbo].[Tbl_Bonanza] bo inner join  Master_Bonanza mb on mb.ID = bo.BonanzaID";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable getBonanzaAchieverAll()
        {
            string str_query = "Select ba.ID,ba.UserID , BonanzaName,Designation   ,MatchingBusiness, DirectBusiness,	SelStaking,	Reward from Tbl_BonanzaAchiever Ba inner join[Tbl_Bonanza] bo on bo.ID = Ba.TbBonanzaID inner join Master_Bonanza mb on mb.ID=Ba.MasterBonanzaID";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

      


        public DataTable GetTbBonanza()
        {
            string str_query = "Select bo.ID ,Mb.BonanzaName+' '+Designation as BonanzaName from Tbl_bonanza bo inner join Master_Bonanza Mb on Mb.ID = bo.BonanzaID";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public DataTable GetBonanzaMaster()
        {
            string str_query = "Select * from Master_Bonanza  order by ID";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public string Insert_Bonanza(clsBonanza objbonanza)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_add_Bonanza";
                SqlParameter[] parameter = {
                new SqlParameter("@BonanzaID",objbonanza.BonanzaID),
                new SqlParameter("@Designation",objbonanza.Designation),
                new SqlParameter("@MatchingBus",objbonanza.Matching_Business),
                new SqlParameter("@DirectBusiness",objbonanza.Direct_Business),
                new SqlParameter("@SelSteking",objbonanza.Sel_Steking),
                new SqlParameter("@Reward",objbonanza.Reward),
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "0";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }

        public string delete_Bonanza(clsBonanza objbonanza)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_Delete_Bonanza";
                SqlParameter[] parameter = {
                new SqlParameter("@TbBonanzaId",objbonanza.TBBonanzaID)
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "0";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }

        public string Insert_BonanzaAchiever(clsBonanza objbonanza)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                s2 = "sp_add_BonanzaAchiever";
                SqlParameter[] parameter = {
                new SqlParameter("@TBBonanzaID",objbonanza.TBBonanzaID),
                new SqlParameter("@UserID",objbonanza.UserID),
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "0";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }


        public DataTable GetUserwiseBonanza(clsBonanza objuser)
        {
            string s2 = "";
            DataTable dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "sp_getUserBonanza";
                SqlParameter[] parameter = {
                                               new SqlParameter("@userid",objuser.UserID),
                };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ObjData.EndConnection();

            }
            return dt;
        }

        #region Retopup Report
        public DataTable GetTopupList(clsBonanza objuser)
        {
            string s2 = "";
            DataTable dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "sp_get_RetopupList";
                SqlParameter[] parameter = {
                                               new SqlParameter("@LoginID",objuser.UserID),
                };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ObjData.EndConnection();

            }
            return dt;
        }
        public DataTable GetTopupListNotValidated(clsBonanza objuser)
        {
            string s2 = "";
            DataTable dt = new DataTable();
            ObjData.StartConnection();
            try
            {
                s2 = "sp_get_RetopupList";
                SqlParameter[] parameter = {
                                               new SqlParameter("@LoginID",objuser.UserID),
                };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ObjData.EndConnection();

            }
            return dt;
        }
        public DataTable GetMobileNoList(string Mobileno)
        {


            string s2 = "";
            DataTable dt = new DataTable();

            var _URL = "https://waba.360dialog.io/v1/contacts";
            var ApiKey = new Dictionary<string, string>
                {
                    { "D360-API-KEY", "HmD2GcPVzdOcUnocbk3GFVDPAK" },

                };

            var Data = new validateMobileApiDate
            {
                blocking = "wait",
                //contacts = new List<string> { "+918090078886" },
                contacts = new List<string> { Mobileno },
                force_check = true,
            };
            string response = string.Empty;

            response = PostJsonDataUsingHWRTLS(_URL, Data, ApiKey).Result;
            var ValidateMobile = JsonConvert.DeserializeObject<ValidateResponse>(response);
            foreach (var Data1 in ValidateMobile.contacts)
            {

                string res = "";
               
                SqlConnection cn;
                SqlTransaction tr = null;
                DataSet ds = new DataSet();
                cn = ObjData.StartConnectionInTransaction();
                tr = cn.BeginTransaction(IsolationLevel.Serializable);

                try
                {
                    s2 = "sp_UpdateMobilevalidate";
                    SqlParameter[] parameter = {
                new SqlParameter("@MobileNo",Data1.input),
                new SqlParameter("@MobileStatus",Data1.status)
                };
                  
                    res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                    tr.Commit();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    ObjData.EndConnection();

                }



            }
           
            return dt;
        }


        


        public async Task<string> PostJsonDataUsingHWRTLS(string URL, object PostData, IDictionary<string, string> headers)
        {
            string result = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest http = (HttpWebRequest)System.Net.WebRequest.Create(URL);
                http.Timeout = 3 * 60 * 1000;
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(PostData));
                http.Method = "POST";
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.MediaType = "application/json";
                http.ContentLength = data.Length;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        http.Headers.Add(item.Key, item.Value);
                    }
                }
                using (Stream stream = http.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                WebResponse response = await http.GetResponseAsync().ConfigureAwait(false);

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = await sr.ReadToEndAsync().ConfigureAwait(false);
                }
            }
            catch (UriFormatException ufx)
            {
                throw new Exception(ufx.Message);
            }
            catch (WebException wx)
            {
                if (wx.Response != null)
                {
                    using (var ErrorResponse = wx.Response)
                    {
                        using (StreamReader sr = new StreamReader(ErrorResponse.GetResponseStream()))
                        {
                            result = await sr.ReadToEndAsync();
                        }
                    }
                }
                else
                {
                    throw new Exception(wx.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        #endregion

    }
}
