using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyHelpers;
using System.Web.Services;

public partial class DataMigration : System.Web.UI.Page
{
    public string[] Messages = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (SelectedFile.Value.Trim().Length > 0)
            {
                Messages = new string[] { String.Format("{0} uploaded to '{1}' region successfully.", SelectedFile.Value, TargetRegion.Value) };  
            }
        }
        InitializeComponents();
    }


    public void InitializeComponents()
    {
        //populate region list
        RegionList.DataSource = DatabaseFactory.GetDBRegions();
        RegionList.DataValueField = "Key";
        RegionList.DataTextField = "Value";
        RegionList.DataBind();

        
        //populate region list
        SecondRegionList.DataSource = DatabaseFactory.GetDBRegions();
        SecondRegionList.DataValueField = "Key";
        SecondRegionList.DataTextField = "Value";
        SecondRegionList.DataBind();
    }



    protected void DownloadPolicy_Click(object sender, EventArgs e)
    {
        ExceedPolicy policy = ExceedPolicy.DownloadPolicyAndSave(UserIdTextBox.Text.Trim(), PasswordTextBox.Text.Trim(), 
                                                        RegionList.SelectedValue.Trim(), PolicyNumberTextBox.Text.Trim());
    }

    [WebMethod]
    public static DataMigrationList[] GetList()
    {
        string[] files = ExceedPolicy.GetSavedPoliciesList();
        DataMigrationList[] list = new DataMigrationList[files.Length];

        for(int i=0; i < files.Length ; i ++)
        {
            list[i] = new DataMigrationList(files[i]);
        }
        return list;
    }

}