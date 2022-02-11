using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using RestSharp;
using System.Xml;
using Newtonsoft.Json;

public partial class CoinRateAutoUpdate : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        var client = new RestClient("https://api.koinbazar.com/cmc/price_conversion/RTC_INR/RTC-1");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Cookie", "csrf_cookie_name=2a005cb0820a7b5c2859e1ec8c43cfcd; ci_session=vget2d495bd6o7aa7742o5lpnkl6jele");
        request.AddParameter("application/json", "{\"wallet_address\":\"0x9788be3b5ae1f1b1bf6698fcddb95043279ed9db\",\"amount\":\"1\"}", ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        //Console.WriteLine(response.Content);
        try
        {
            DataSet ds = ConvertJSONToDataSet(response.Content);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Columns.Contains("status"))
                    {
                        if (ds.Tables[0].Rows[0]["status"].ToString() == "1")
                        {
                            if (ds.Tables[0].Columns.Contains("price"))
                            {
                                objuser.Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["price"].ToString());
                                string res = objuser.CoinRateUpdate(objuser);
                            }
                        }
                    }

                }
            }
        }
        catch { }
    }

    DataSet ConvertJSONToDataSet(string Resp)
    {
        DataSet jsonDataSet = new DataSet();
        try
        {
            //Resp = Resp.Replace("{", "[{");
            //Resp = Resp.Replace("}", "}]");
            //DataSet myDataSet = JsonConvert.DeserializeObject<DataSet>(Resp);
            //return myDataSet;
            XmlDocument xd1 = new XmlDocument();
            xd1 = (XmlDocument)JsonConvert.DeserializeXmlNode(Resp, "root");

            jsonDataSet.ReadXml(new XmlNodeReader(xd1));
        }
        catch { }
        return jsonDataSet;
    }
}