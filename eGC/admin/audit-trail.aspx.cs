using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.admin
{
    public partial class audit_trail : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        int _TotalRecs = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AuditTrailDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text.Trim();

            e.Result = db.AuditTrails.Where(s => s.User.Contains(strSearch) ||
                s.Action.Contains(strSearch) ||
                s.Description.Contains(strSearch) ||
                s.AssociatedId.Contains(strSearch)).OrderByDescending(i => i.Id).ToList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvAuditTrail.DataBind();
        }

        protected void gvAuditTrail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.Footer)
            {
                int _CurrentRecStart = gvAuditTrail.PageIndex * gvAuditTrail.PageSize + 1;
                int _CurrentRecEnd = gvAuditTrail.PageIndex * gvAuditTrail.PageSize + gvAuditTrail.Rows.Count;

                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = string.Format("Displaying <b style=color:red>{0}</b> to <b style=color:red>{1}</b> of {2} records found", _CurrentRecStart, _CurrentRecEnd, _TotalRecs);
            }
        }

        protected void AuditTrailDataSource_Selected(object sender, LinqDataSourceStatusEventArgs e)
        {
            _TotalRecs = e.TotalRowCount;
        }
    }
}