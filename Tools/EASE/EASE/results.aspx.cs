using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EASE
{
    public partial class results : System.Web.UI.Page
    {
        public String query;

        private HashSet<String> op = new HashSet<String>();

        public int i = 0;
        public string filename = "";
        public string filepath = "";

        //public PlaceHolder ph = new PlaceHolder();

        //public TableCell tc = new TableCell();
        //public TableCell tc1 = new TableCell();
        //public TableCell tc2 = new TableCell();
        //public TableRow tr = new TableRow();
        //public TableRow tr1 = new TableRow();
        //public TableRow tr2 = new TableRow();

        //public HyperLink link = new HyperLink();
        //public Label _lbl = new Label();
        //public Label lbl1 = new Label();
        //public Label lbl2 = new Label();

        protected void Page_Load(object sender, EventArgs e)
        {
          // label1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["search"]);
            query = Convert.ToString(System.Web.HttpContext.Current.Session["search"]);
            int flag = 0;

           
          //  Label lbl = new Label();

            Table tbldynamic = new Table();

            KMSearch obj = new KMSearch();
            
            obj.CreateIndexSearcher();

            op = obj.contentSearch(query);

            flag = op.Count;

            foreach (Object o in op)
            {
                if (op.Count >= 1)
                {
                    String[] s = o.ToString().Split('>');
                    int si1 = s[0].IndexOf(':');
                    int si2 = s[1].IndexOf(':');
 
                    filename = s[0].Substring(si1 + 1);
                    filepath = s[1].Substring(si2 + 1);

                    PlaceHolder ph = new PlaceHolder();

                    TableCell tc = new TableCell();
                    TableRow tr = new TableRow();
                    TableRow tr1 = new TableRow();
                    TableRow tr2 = new TableRow();

                    HyperLink link = new HyperLink();
                    Label _lbl = new Label();
                    Label lbl1 = new Label();
                    Label lbl2 = new Label();

                 //   ph.ID = "holder" + i;
                    

                    lbl1.Text = "FILENAME: ";
                    lbl1.Font.Bold = true;

                    lbl2.Text = "FILEPATH: ";
                    lbl2.Font.Bold = true;

             
                    _lbl.ID = "asplabel" + i;
                    _lbl.Text = filename;
                    
                    link.ID = "asplink" + i;
                    link.Text = filepath;
                    link.NavigateUrl = filepath;

                    //Controls.Add(_lbl);
                    //Controls.Add(link);

                    //PH1.Controls.Add(_lbl);
                    //PH1.Controls.Add(link);
// add filename 
                    //tc.Controls.Add(lbl1);
                    //tr.Cells.Add(tc);
                    //tbldynamic.Rows.Add(tr);

                    //tc.Controls.Add(_lbl);
                    //tr.Cells.Add(tc);
                    //tbldynamic.Rows.Add(tr);
// add filepath
                    tc.Controls.Add(lbl2);
                    tr.Cells.Add(tc);
             //       tr.ControlStyle.Height = 20;
                    tbldynamic.Rows.Add(tr);

                    tc.Controls.Add(link);
                    tr.Cells.Add(tc);
                    tbldynamic.Rows.Add(tr);
                    tbldynamic.Width = 700;
                    tbldynamic.CellPadding = 10;
                }
                i = i + 1;
            }

        //    Panel1.Controls.Add(tbldynamic);
            PH1.Controls.Add(tbldynamic);
            if (flag < 1)
            {
                label1.Text = " OOPS! Not able to locate with specified search criterion. Please modify your search.";
            }

        }
    }
}