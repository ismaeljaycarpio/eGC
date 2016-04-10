using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.admin
{
    public partial class UserConfig : System.Web.UI.Page
    {
        EHRISDataContextDataContext dbeHRIS = new EHRISDataContextDataContext();
        GiftCheckDataContext dbGc = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindGridview();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            bindGridview();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void bindGridview()
        {
            var q = from e in dbeHRIS.EMPLOYEEs
                    select new
                    {
                        UserId = e.UserId,
                        EmpId = e.Emp_Id,
                        FullName = e.LastName + " , " + e.FirstName + " " + e.MiddleName
                    };
            gvUsers.DataSource = q.ToList();
            gvUsers.DataBind();
        }
    }
}