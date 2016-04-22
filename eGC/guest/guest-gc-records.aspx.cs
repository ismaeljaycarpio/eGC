using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using OfficeOpenXml;
using eGC.DAL;
using System.IO;

namespace eGC.guest
{
    public partial class guest_gc_records : System.Web.UI.Page
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

        protected void btnExport_Click(object sender, EventArgs e)
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
                     gctran.GCNumber.Contains(strSearch) ||
                     guest.CompanyName.Contains(strSearch)
                     )
                     &&
                     (guest.Id == Convert.ToInt32(Request.QueryString["guestId"]))
                     select new
                     {
                         GuestId = guest.GuestId,
                         FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                         CompanyName = (from gu in db.Guests where guest.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                         Number = guest.ContactNumber,
                         GCNumber = gctran.GCNumber,
                         ArrivalDate = gctran.ArrivalDate,
                         CheckoutDate = gctran.CheckOutDate,
                         ExpiryDate = gctran.ExpiryDate,
                         Status = gctran.StatusGC,
                         TotalValue = String.Format(CultureInfo.GetCultureInfo("en-PH"), "{0:C}", db.GCRooms.Where(x => x.GCTransactionId == gctran.Id).Sum(t => t.Total)),
                         Approval = gctran.ApprovalStatus,
                         CancellationReason = gctran.CancellationReason,
                         CancelledDate = gctran.CancelledDate
                     }).ToList();

            if (txtDateFrom.Text != String.Empty && txtDateTo.Text == String.Empty)
            {
                q = q.Where(a => a.ArrivalDate >= Convert.ToDateTime(txtDateFrom.Text)).ToList();
            }
            else if (txtDateFrom.Text != String.Empty && txtDateTo.Text != String.Empty)
            {
                q = q.Where(a => a.ArrivalDate >= Convert.ToDateTime(txtDateFrom.Text) &&
                    a.ArrivalDate <= Convert.ToDateTime(txtDateTo.Text)).ToList();
            }

            DataTable dt = new DataTable();
            dt = q.ToDataTable();

            var products = dt;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Guest GC Records");
            var totalCols = products.Columns.Count;
            var totalRows = products.Rows.Count;

            for (var col = 1; col <= totalCols; col++)
            {
                workSheet.Cells[1, col].Value = products.Columns[col - 1].ColumnName;
            }
            for (var row = 1; row <= totalRows; row++)
            {
                for (var col = 0; col < totalCols; col++)
                {
                    workSheet.Cells[row + 1, col + 1].Value = products.Rows[row - 1][col];
                }
            }
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=guest-gc-records.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        protected void GCRecordDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;
            var q = from guest in db.Guests
                    join gctran in db.GCTransactions
                    on guest.GuestId equals gctran.GuestId
                    where
                    (
                    guest.GuestId.Contains(strSearch) ||
                    guest.LastName.Contains(strSearch) ||
                    guest.FirstName.Contains(strSearch) ||
                    guest.MiddleName.Contains(strSearch) ||
                    gctran.ApprovalStatus.Contains(strSearch) ||
                    gctran.GCNumber.Contains(strSearch) ||
                    guest.CompanyName.Contains(strSearch)
                    )
                    &&
                    (guest.Id == Convert.ToInt32(Request.QueryString["guestId"]))
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
                        ExpiryDate = gctran.ExpiryDate,
                        Status = gctran.StatusGC,
                        TotalValue = String.Format(CultureInfo.GetCultureInfo("en-PH"), "{0:C}", db.GCRooms.Where(x => x.GCTransactionId == gctran.Id).Sum(t => t.Total)),
                        Approval = gctran.ApprovalStatus,
                        CancellationReason = gctran.CancellationReason,
                        CancelledDate = gctran.CancelledDate
                    };

            if (txtDateFrom.Text != String.Empty && txtDateTo.Text == String.Empty)
            {
                q = q.Where(a => a.ArrivalDate >= Convert.ToDateTime(txtDateFrom.Text));
            }
            else if (txtDateFrom.Text != String.Empty && txtDateTo.Text != String.Empty)
            {
                q = q.Where(a => a.ArrivalDate >= Convert.ToDateTime(txtDateFrom.Text) &&
                    a.ArrivalDate <= Convert.ToDateTime(txtDateTo.Text));
            }

            e.Result = q;
            txtSearch.Focus();
        }
    }
}