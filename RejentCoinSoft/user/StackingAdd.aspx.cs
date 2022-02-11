using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using BusinessLogicTier;
using System.Xml;
using Newtonsoft.Json;
using RestSharp;

public partial class admin_EPinAdd : System.Web.UI.Page
{

    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();
    clsSetting objsetting = new clsSetting();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();
                loadsusername();
                // loadbalance();
                loadstakingPlan();
                loadStakingtype();
                fillform();

            }
        }
        else
        {
            Server.Transfer("index.aspx");
        }
    }
    void loadstakingPlan()
    {
        ddplan.Items.Clear();
        DataTable dt = new DataTable();
        dt = objUser.getStakingPlan();
        ddplan.DataSource = dt;
        ddplan.DataTextField = "PlanName2";
        ddplan.DataValueField = "PlanId";
        ddplan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        ddplan.Items.Insert(0, li);
    }
    void loadStakingtype()
    {

        objUser.UserId = Session["userid"].ToString();

        ddtype.Items.Clear();
        DataTable dt = new DataTable();
        dt = objUser.getStakingPlan(objUser);
        ddtype.DataSource = dt;
        ddtype.DataTextField = "ActiveStatus";
        ddtype.DataValueField = "ActiveStatus";
        ddtype.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        ddtype.Items.Insert(0, li);
    }


    void fillform()
    {
        DataTable dt = new DataTable();
        dt = objsetting.GetSystemSettings();
        if (dt.Rows.Count > 0)
        {
            lblqauerecoinrate.Text = dt.Rows[0]["qaurecointodollar"].ToString();
            //txtwalletaddress.Text = dt.Rows[0]["QauereWalletAddress"].ToString();
            //lbladdress.Text = dt.Rows[0]["QauereWalletAddress"].ToString();
            //ltimage.Text = @"<img src=""../admin/images/" + dt.Rows[0]["imagename"].ToString() + @""" style=""width:100%;"" />";
        }
    }


    //void loadbalance()
    //{
    //    objaccount.UserId = Session["userid"].ToString();
    //    DataTable dt = new DataTable();
    //    dt = objaccount.getUserWalletBalanceForStaking(objaccount);
    //    if (dt.Rows.Count > 0)
    //    {
    //        txtbalance.Text = dt.Rows[0]["ewalletbalance"].ToString();
    //    }
    //    else
    //    {
    //        txtbalance.Text = "0.00";
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (((ddtype.SelectedValue.ToString() == "Topup") && ((txtuserid.Text == "204793") || (txtuserid.Text == "1006913"))) || (ddtype.SelectedValue.ToString() != "Topup"))
        //{
            if (txtamount.Text != "")
            {
                if (Convert.ToDecimal(txtamount.Text) >= 50.00M)
                {
                    //if (Convert.ToDecimal(txtamount.Text) % 25.00M == 0)
                    //{

                    objUser.UserId = txtuserid.Text;
                    objUser.Amount = Convert.ToDecimal(txtamount.Text);
                    // objUser.WalletAddress = txtwalletaddress.Text;
                    objUser.DollarAmount = Convert.ToDecimal(txtdollaramount.Text);
                    objUser.QaureCoinToDollar = Convert.ToDecimal(lblqauerecoinrate.Text);
                    objUser.Type = ddtype.SelectedValue.ToString();
                    objUser.PlanId = ddplan.SelectedValue.ToString();
                    objUser.OnlineTransactionId = txthash.Text;
                    objUser.MentionBy = Session["userid"].ToString();

                DataTable dtres = new DataTable();
                dtres = objUser.InsertStacking(objUser);
                string rs = dtres.Rows[0][0].ToString();
                if (rs == "t")
                    {
                        string popupScript = "toastr.success('Success', 'Staking Request Added Successfully');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);



                    string str_response = "";
                    string str_status = "Pending";

                    var client = new RestClient("http://168.119.24.22:3004/stacking_transaction_details/" + txthash.Text + "");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    //Console.WriteLine(response.Content);
                    try
                    {
                        str_response = response.Content.ToString();
                        objUser.OnlineTransactionId = txthash.Text;
                        objUser.Response = str_response;

                        string res = objUser.Insert_StakingTransactionResponse(objUser);
                    }
                    catch { }
                    DataSet ds = ConvertJSONToDataSet(str_response);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Columns.Contains("success"))
                        {
                            if (ds.Tables[0].Rows[0]["success"].ToString() == "true")
                            {
                                str_status = "Approved";
                            }
                            else
                            {
                                str_status = "Pending";
                            }

                            try
                            {
                                objUser.Status = str_status;
                                objUser.Id = dtres.Rows[0]["id"].ToString();
                                objUser.MentionBy = Session["userid"].ToString();
                                string res = objUser.UpdateStckingRequest(objUser);
                            }
                            catch { }
                        }
                    }



                    //txtremark.Text = "";
                    txthash.Text = txtamount.Text = txtdollaramount.Text = "";
                        //txttransactionid.Text = "";

                        //txtuserid.Text = txtusername.Text = "";
                        ddplan.SelectedValue = "0";
                    }
                    else if (rs == "b")
                    {
                        string popupScript = "toastr.error('Error', 'There is already a pending staking request');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                    else if (rs == "a")
                    {
                        string popupScript = "toastr.error('Error', 'amount sholud be greater tha or equal to last topup amount');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                    else
                                if (rs == "f")
                    {
                        string popupScript = "toastr.error('Error', 'Please Upgrade for the first time for renewal');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                    else
                                if (rs == "p")
                    {
                        string popupScript = "toastr.error('Error', 'Please wait for some time untill your id placed in binary');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                    else
                    {
                        string popupScript = "toastr.error('Error', '"+rs+"');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }

                }
                else
                {
                    string popupScript = "toastr.error('Error', 'Amount should br in multiple of 25 $...!!!');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                }
                //}
                //else
                //{
                //    string popupScript = "toastr.error('Error', 'Minimum Staking Amount is 25 $...!!!');";
                //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

                //}
            }
            else
            {
                Message.Show("Enter amount...!!!");
            }

        //}
        //else
        //{
          //  string popupScript = "toastr.error('Error', 'You Are Not Eligible for Topup type');";
          //  ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

        //}
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Server.Transfer("dashboard.aspx");
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Server.Transfer("Dashboard.aspx");
    }


    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        loadamount();
    }
    void loadamount()
    {
        if (txtamount.Text != "")
        {
            if (Convert.ToDecimal(txtamount.Text) > 0.00M)
            {

                decimal dcQuareToDollar = Convert.ToDecimal(lblqauerecoinrate.Text);
                txtdollaramount.Text = (Convert.ToDecimal(txtamount.Text) * dcQuareToDollar).ToString();



            }
        }
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();

        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            string popupScript = "toastr.error('Error', 'Invalid User Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        // loadepin();
    }
    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
    }



}