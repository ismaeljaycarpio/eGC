using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eGC.DAL;

namespace eGC.company
{
    public partial class company_profile : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from g in db.Guests
                     where g.Id == Convert.ToInt32(hfDeleteId.Value)
                     select g).FirstOrDefault();

            db.Guests.DeleteOnSubmit(q);
            db.SubmitChanges();

            this.gvCompany.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvCompany.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var q = (from g in db.Guests
                     where
                     (g.GuestId.Contains(txtSearch.Text) ||
                     g.CompanyName.Contains(txtSearch.Text) ||
                     g.ContactNumber.Contains(txtSearch.Text) ||
                     g.Email.Contains(txtSearch.Text)
                     )
                     &&
                     g.IsCompany == true
                     select new
                     {
                         ID = g.GuestId,
                         Company = g.CompanyName,
                         Number = g.ContactNumber,
                         Email = g.Email
                     }).ToList();

            DataTable dt = new DataTable();
            dt = q.ToDataTable();

            var products = dt;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Companies");
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
                Response.AddHeader("content-disposition", "attachment;  filename=companies.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        protected void CompanyDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            var q = (from g in db.Guests
                     where
                     (g.GuestId.Contains(txtSearch.Text) ||
                     g.CompanyName.Contains(txtSearch.Text) ||
                     g.ContactNumber.Contains(txtSearch.Text) ||
                     g.Email.Contains(txtSearch.Text)
                     ) 
                     &&
                     g.IsCompany == true
                     select g).ToList();

            e.Result = q;
            txtSearch.Focus();
        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = gvCompany.DataKeys[index].Value.ToString();
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvCompany.Rows[index].FindControl("lbtnCompanyId")).Text;
                Response.Redirect("~/company/edit-company.aspx?companyId=" + rowId);
            }
            else if (e.CommandName.Equals("addGC"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvCompany.Rows[index].FindControl("lbtnCompanyId")).Text;
                Response.Redirect("~/tran/gcform.aspx?guestid=" + rowId);
            }
            else if(e.CommandName.Equals("viewGCRecords"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string Id = gvCompany.DataKeys[index].Value.ToString();
                Response.Redirect("~/company/company-gc-records.aspx?companyId=" + Id);
            }
        }
    }
}