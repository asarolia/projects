using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace RunBoard
{
    public partial class interfacedetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int[] yValue = { 1 };
            //Chart1.Series["Series1"].Points.DataBindY(yValue);
            Chart1.Series["Series1"].Points[0].Color = Color.Blue;
            Chart2.Series["Series1"].Points[0].Color = Color.Blue;
            Chart3.Series["Series1"].Points[0].Color = Color.Blue;
        }
        protected void unptrg_Click(object sender, EventArgs e)
        {
            Session["detail"] = "Unprocessed Interface Triggers";
            Session["date"] = dtlv3.Text;
            Response.Redirect("IntfTriggerDetail.aspx");
        }
        protected void errtrg_Click(object sender, EventArgs e)
        {
            Session["detail"] = "Errored Interface Triggers";
            Session["date"] = dtlv3.Text;
            Response.Redirect("IntfTriggerDetail.aspx");
        }
        protected void prnttrg_Click(object sender, EventArgs e)
        {
            Session["detail"] = "Pending Print Triggers";
            Session["date"] = dtlv3.Text;
            Response.Redirect("IntfTriggerDetail.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }
    }
}