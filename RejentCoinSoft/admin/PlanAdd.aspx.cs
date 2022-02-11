﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using BusinessLogicTier;

public partial class admin_EPinAdd : System.Web.UI.Page
{
    clsEPin objepin = new clsEPin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["rijentadmin45852_4"] != null)
        {
            loaddata();
        }
        else
        {
            Server.Transfer("index.aspx");
        }
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        dt = objepin.getPlan();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objepin.PlanName = txtplanname.Text;
        objepin.Amount = Convert.ToDecimal( txtamountrupees.Text);
        objepin.AmountDollar = txtamountrupees.Text;
        objepin.ROIDays = txtroidays.Text;
        objepin.ROIAmount = txtroiamount.Text;
        objepin.CappingAmount = txtcappingrupees.Text;
        objepin.CappingAmountDollar = txtcappingrupees.Text;
        objepin.MentionBy = Session["rijentadmin45852_4"].ToString();
        string res = objepin.Insert_Plan(objepin);
        if (res == "t")
        {
            string popupScript = "toastr.success('Success', 'Plan Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtplanname.Text = "";
            txtamountrupees.Text = "";
            txtroidays.Text = "";
            txtroiamount.Text = "";
            txtcappingrupees.Text = "";
            loaddata();
        }
        else if (res == "f")
        {
            string popupScript = "toastr.error('Error', 'Plan Already Exists');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "toastr.error('Error', 'Unknow error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Server.Transfer("dashboard.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lblaccountholdername = (Label)GridView1.Rows[index].FindControl("lblaccountholdername");
            Label lblaccountno = (Label)GridView1.Rows[index].FindControl("lblaccountno");
            Label lblbankname = (Label)GridView1.Rows[index].FindControl("lblbankname");
            Label lblifsccode = (Label)GridView1.Rows[index].FindControl("lblifsccode");
            Label lblbranchname = (Label)GridView1.Rows[index].FindControl("lblbranchname");
            lblbankaccountid.Text = lblid.Text;
            txtaccholdernameedit.Text = lblaccountholdername.Text;
            txtaccountnoedit.Text = lblaccountno.Text;
            txtdepositbankedit.Text = lblbankname.Text;
            txtifscedit.Text = lblifsccode.Text;
            txtbranchnameedit.Text = lblbranchname.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //objbank.BankAccountId = lblbankaccountid.Text;
        //objbank.BankName = txtdepositbankedit.Text;
        //objbank.AccHolderName = txtaccholdernameedit.Text;
        //objbank.AccNo = txtaccountnoedit.Text;
        //objbank.IFSCCode = txtifscedit.Text;
        //objbank.BranchName = txtbranchnameedit.Text;
        //string res = objbank.Update_BankAccount(objbank);
        //if (res == "t")
        //{
        //    string popupScript = "toastr.success('Success', 'Bank Account Details Edited Successfully');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //    string popupScript2 = "Closepopup();";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        //    loaddata();
        //}
        //else
        //{
        //    string popupScript = "toastr.error('Error', 'Unknow error occurred');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
    }
}