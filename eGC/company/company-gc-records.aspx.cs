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
            if (e.CommandName.Equals("redirectGC"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGC.Rows[index].FindControl("lblGCNo")).Text;
                Response.Redirect("~/tran/editgcform.aspx?gcId=" + rowId);
            }
        }

        protected void GCRecordDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;
            var q = from guest in db.Guests
                     join gctran in db.GCTransactions
                     on guest.Id equals gctran.GuestId
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
                     (guest.CompanyId == Convert.ToInt32(Request.QueryString["companyId"]))
                     select new
                     {
                         Id = gctran.Id,
                         GuestId = guest.GuestId,
                         FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                         CompanyName = (from gu in db.Guests where guest.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                         Number = guest.ContactNumber,
                         GCNumber = gctran.GCNumber,
                         ExpiryDate = gctran.ExpirationDate,
                         Status = gctran.StatusGC,
                         Approval = gctran.ApprovalStatus,
                         CancellationReason = gctran.CancellationReason,
                         CancelledDate = gctran.CancelledDate,
                         Type = gctran.GCType
                     };


            e.Result = q;
            txtSearch.Focus();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string strSearch = txtSearch.Text;
            var q = (from guest in db.Guests
                    join gctran in db.GCTransactions
                    on guest.Id equals gctran.GuestId
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
                    (guest.CompanyId == Convert.ToInt32(Request.QueryString["companyId"]))
                    select new
                    {
                        GuestId = guest.GuestId,
                        FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                        CompanyName = (from gu in db.Guests where guest.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                        Number = guest.ContactNumber,
                        GCNumber = gctran.GCNumber,
                        ExpiryDate = gctran.ExpirationDate,
                        Status = gctran.StatusGC,
                        Approval = gctran.ApprovalStatus,
                        CancellationReason = gctran.CancellationReason,
                        CancelledDate = gctran.CancelledDate,
                        Type = gctran.GCType
                    }).ToList();


            DataTable dt = new DataTable();
            dt = q.ToDataTable();

            var products = dt;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Company GC Records");
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
                Response.AddHeader("content-disposition", "attachment;  filename=company-gc-records.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}