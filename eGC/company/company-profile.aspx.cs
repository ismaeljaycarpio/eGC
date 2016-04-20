using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.company
{
    public partial class company_profile : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                txtSearch.Focus();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void CompanyDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            var q = (from g in db.Guests
                     where g.IsCompany == true
                     select g).ToList();

            e.Result = q;
        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}