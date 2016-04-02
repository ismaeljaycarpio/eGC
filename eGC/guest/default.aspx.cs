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

namespace eGC.guest
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

        protected void gvGuests_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvGuests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGuests.PageIndex = e.NewPageIndex;
            bindGridview();
        }

        protected void gvGuests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvGuests.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGuests.Rows[index].FindControl("lbtnGuestId")).Text;
                Response.Redirect("~/guest/editguest.aspx?guestid=" + rowId);
            }
            else if(e.CommandName.Equals("addGC"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGuests.Rows[index].FindControl("lbtnGuestId")).Text;
                Response.Redirect("~/tran/gcform.aspx?guestid=" + rowId);
            }
        }

        protected void gvGuests_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void bindGridview()
        {
            var q = from g in db.Guests
                    where g.GuestId.Contains(txtSearch.Text) ||
                    g.FirstName.Contains(txtSearch.Text) ||
                    g.MiddleName.Contains(txtSearch.Text) ||
                    g.LastName.Contains(txtSearch.Text) ||
                    g.CompanyName.Contains(txtSearch.Text)
                    select new
                    {
                        Id = g.Id,
                        GuestId = g.GuestId,
                        FullName = g.LastName + ", " + g.FirstName + " " + g.MiddleName,
                        CompanyName = g.CompanyName,
                        Number = g.ContactNumber
                    };

            gvGuests.DataSource = q.ToList();
            gvGuests.DataBind();

            txtSearch.Focus();
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }

            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from g in db.Guests
                     where g.Id.Equals(hfDeleteId.Value)
                     select g).FirstOrDefault();

            db.Guests.DeleteOnSubmit(q);
            db.SubmitChanges();

            bindGridview();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false); 
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var q = (from g in db.Guests
                    where g.GuestId.Contains(txtSearch.Text) ||
                    g.FirstName.Contains(txtSearch.Text) ||
                    g.MiddleName.Contains(txtSearch.Text) ||
                    g.LastName.Contains(txtSearch.Text) ||
                    g.CompanyName.Contains(txtSearch.Text)
                    select new
                    {
                        GuestId = g.GuestId,
                        LastName = g.LastName,
                        FirstName = g.FirstName,
                        MiddleName = g.MiddleName,
                        CompanyName = g.CompanyName,
                        Number = g.ContactNumber
                    }).ToList();

            DataTable dt = new DataTable();
            dt = q.ToDataTable();

            var products = dt;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Guests");
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
                Response.AddHeader("content-disposition", "attachment;  filename=guests.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}