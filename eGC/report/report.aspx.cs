﻿using System;
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

namespace eGC.report
{
    public partial class report : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindDropdown();
            }
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
                    guest.CompanyName.Contains(strSearch) ||
                    gctran.StatusGC.Contains(strSearch)
                    )
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
                        CancelledDate = gctran.CancelledDate,
                        CompanyId = guest.CompanyId
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

            //chk dropdown
            if (ddlCompanyName.SelectedValue != "0")
            {
                q = q.Where(a => a.CompanyId == Convert.ToInt32(ddlCompanyName.SelectedValue)).ToList();
            }

            DataTable dt = new DataTable();
            dt = q.ToDataTable();

            var products = dt;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("GC Records");
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
                Response.AddHeader("content-disposition", "attachment;  filename=reports-gc-records.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        protected void gvGC_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvGC_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvGC.DataBind();
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
                    guest.CompanyName.Contains(strSearch) ||
                    gctran.StatusGC.Contains(strSearch)
                    )
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
                        CancelledDate = gctran.CancelledDate,
                        CompanyId = guest.CompanyId
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

            //chk dropdown
            if(ddlCompanyName.SelectedValue != "0")
            {
                q = q.Where(a => a.CompanyId == Convert.ToInt32(ddlCompanyName.SelectedValue));
            }

            e.Result = q;
            txtSearch.Focus();
        }

        private void bindDropdown()
        {
            var q = (from g in db.Guests
                     where g.IsCompany == true
                     select new
                     {
                         Id = g.Id,
                         CompanyName = g.CompanyName
                     }).ToList();

            ddlCompanyName.DataSource = q;
            ddlCompanyName.DataTextField = "CompanyName";
            ddlCompanyName.DataValueField = "Id";
            ddlCompanyName.DataBind();
            ddlCompanyName.Items.Insert(0, new ListItem("-- Select Company --", "0"));
        }
    }
}