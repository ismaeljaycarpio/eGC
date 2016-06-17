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
            if (!Page.IsPostBack)
            {
                bindDropdown();
                this.gvGuests.DataBind();

                //hide from approver
                if (!User.IsInRole("can-create-gc") && !User.IsInRole("Admin-GC"))
                {
                    lbtnGuestProfile.Visible = false;
                    gvGuests.Columns[6].Visible = false;
                    gvGuests.Columns[7].Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvGuests.DataBind();
        }

        protected void gvGuests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = gvGuests.DataKeys[index].Value.ToString();
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
                string rowId = gvGuests.DataKeys[index].Value.ToString();
                Response.Redirect("~/guest/editguest.aspx?guestid=" + rowId);
            }
            else if(e.CommandName.Equals("addGC"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = gvGuests.DataKeys[index].Value.ToString();
                Response.Redirect("~/tran/gcform.aspx?guestid=" + rowId);
            }
            else if (e.CommandName.Equals("viewGCRecords"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = gvGuests.DataKeys[index].Value.ToString();
                Response.Redirect("~/guest/guest-gc-records.aspx?guestId=" + rowId);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from g in db.Guests
                     where g.Id.Equals(hfDeleteId.Value)
                     select g).FirstOrDefault();

            db.Guests.DeleteOnSubmit(q);
            db.SubmitChanges();

            //bindGridview();
            gvGuests.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false); 
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var q = (from g in db.Guests
                    where
                    (
                    g.GuestId.Contains(txtSearch.Text) ||
                    g.FirstName.Contains(txtSearch.Text) ||
                    g.MiddleName.Contains(txtSearch.Text) ||
                    g.LastName.Contains(txtSearch.Text) ||
                    g.ContactNumber.Contains(txtSearch.Text) ||
                    g.Email.Contains(txtSearch.Text)
                    )
                    &&
                    g.IsCompany == false
                    select new
                    {
                        GuestId = g.GuestId,
                        FullName = g.LastName + ", " + g.FirstName + " " + g.MiddleName,
                        CompanyName = (from gu in db.Guests where g.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                        Number = g.ContactNumber,
                        Email = g.Email
                    }).ToList();

            DataTable dt = new DataTable();
            dt = q.ToDataTable();

            var products = dt;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Individual-Profile");
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
                Response.AddHeader("content-disposition", "attachment;  filename=Individual-Profile.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        protected void GuestDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            //chk compay ddl
            if (ddlCompanyName.SelectedValue != "0")
            {
                var q = from g in db.Guests
                        where
                        (
                        g.GuestId.Contains(txtSearch.Text) ||
                        g.FirstName.Contains(txtSearch.Text) ||
                        g.MiddleName.Contains(txtSearch.Text) ||
                        g.LastName.Contains(txtSearch.Text) ||
                        g.ContactNumber.Contains(txtSearch.Text) ||
                        g.Email.Contains(txtSearch.Text)
                        )
                        &&
                        g.IsCompany == false
                        &&
                        g.CompanyId == Convert.ToInt32(ddlCompanyName.SelectedValue)
                        select new
                        {
                            Id = g.Id,
                            GuestId = g.GuestId,
                            FullName = g.LastName + ", " + g.FirstName + " " + g.MiddleName,
                            CompanyName = (from gu in db.Guests where g.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                            Number = g.ContactNumber,
                            Email = g.Email
                        };

                e.Result = q.ToList();
            }
            else
            {
                var q = from g in db.Guests
                        where
                        (
                        g.GuestId.Contains(txtSearch.Text) ||
                        g.FirstName.Contains(txtSearch.Text) ||
                        g.MiddleName.Contains(txtSearch.Text) ||
                        g.LastName.Contains(txtSearch.Text) ||
                        g.ContactNumber.Contains(txtSearch.Text) ||
                        g.Email.Contains(txtSearch.Text)
                        )
                        &&
                        g.IsCompany == false
                        select new
                        {
                            Id = g.Id,
                            GuestId = g.GuestId,
                            FullName = g.LastName + ", " + g.FirstName + " " + g.MiddleName,
                            CompanyName = (from gu in db.Guests where g.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                            Number = g.ContactNumber,
                            Email = g.Email
                        };

                e.Result = q.ToList();
            }
            
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
            ddlCompanyName.Items.Insert(0, new ListItem("--Select Company--", "0"));
        }
    }
}