using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace eGC.company
{
    public partial class company_gc_records : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvGC.DataBind();
        }

        protected void gvGC_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvGC_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GCRecordDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;
            var q = (from guest in db.Guests
                     join gctran in db.GCTransactions
                     on guest.GuestId equals gctran.GuestId
                     where
                     (
                     guest.GuestId.Contains(strSearch) ||
                     guest.LastName.Contains(strSearch) ||
                     guest.FirstName.Contains(strSearch) ||
                     guest.MiddleName.Contains(strSearch) ||
                     gctran.ApprovalStatus.Contains(strSearch) ||
                     gctran.GCNumber.Contains(strSearch)
                     ) 
                     &&
                     (guest.CompanyId == Convert.ToInt32(Request.QueryString["companyId"]))
                     select new
                     {
                         Id = gctran.Id,
                         GuestId = guest.GuestId,
                         FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                         CompanyName = (from gu in db.Guests where guest.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                         Number = guest.ContactNumber,
                         GCNumber = gctran.GCNumber,
                         ArrivalDate = gctran.ArrivalDate,
                         CheckoutDate = gctran.CheckOutDate,
                         Status = gctran.StatusGC,
                         TotalValue = String.Format(CultureInfo.GetCultureInfo("en-PH"), "{0:C}", db.GCRooms.Where(x => x.GCTransactionId == gctran.Id).Sum(t => t.Total)),
                         Approval = gctran.ApprovalStatus,
                         CancellationReason = gctran.CancellationReason,
                         CancelledDate = gctran.CancelledDate
                     }).ToList();

            e.Result = q;
            txtSearch.Focus();
        }
    }
}