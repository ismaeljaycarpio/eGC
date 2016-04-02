using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.gcapproval
{
    public partial class _default : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindGridview();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGridview();
        }

        protected void gvGC_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvGC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGC.PageIndex = e.NewPageIndex;
            bindGridview();
        }

        protected void gvGC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("redirectGuest"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGC.Rows[index].FindControl("lbtnGuestId")).Text;
                Response.Redirect("~/guest/editguest.aspx?guestid=" + rowId);
            }
            else if (e.CommandName.Equals("redirectGC"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGC.Rows[index].FindControl("lblGCNo")).Text;
                Response.Redirect("~/tran/editgcform.aspx?gcId=" + rowId);
            }
        }

        protected void gvGC_Sorting(object sender, GridViewSortEventArgs e)
        {
            
        }

        protected void bindGridview()
        {
            string strSearch = txtSearch.Text;

            var q = from guest in db.Guests
                    join gctran in db.GCTransactions
                    on guest.GuestId equals gctran.GuestId
                    where
                    (guest.GuestId.Contains(strSearch) ||
                    guest.LastName.Contains(strSearch) ||
                    guest.FirstName.Contains(strSearch) ||
                    guest.MiddleName.Contains(strSearch) ||
                    guest.CompanyName.Contains(strSearch) ||
                    gctran.ApprovalStatus.Contains(strSearch) ||
                    gctran.GCNumber.Contains(strSearch) &&
                    gctran.ApprovalStatus == "Pending")
                    select new
                    {
                        Id = gctran.Id,
                        GuestId = guest.GuestId,
                        FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                        CompanyName = guest.CompanyName,
                        Number = guest.ContactNumber,
                        GCNumber = gctran.GCNumber,
                        ArrivalDate = gctran.ArrivalDate,
                        CheckoutDate = gctran.CheckOutDate,
                        Status = gctran.ApprovalStatus,
                        TotalValue = String.Format(CultureInfo.GetCultureInfo("en-PH"), "{0:C}", db.GCRooms.Where(x => x.GCTransactionId == gctran.Id).Sum(t => t.Total)) 
                    };

            gvGC.DataSource = q.ToList();
            gvGC.DataBind();

            txtSearch.Focus();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}