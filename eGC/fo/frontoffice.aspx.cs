using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using eGC.DAL;

namespace eGC.fo
{
    public partial class frontoffice : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDropdown();
                checkExpiration();
                CheckOutDate();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvGC.DataBind();
        }

        protected void gvGC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("selectGuest"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvGC.Rows[index].FindControl("lblRowId")).Text;
                
                //txtName.Text = gu.FullName;
                //txtGuestId.Text = gu.GuestId;
                //txtStatus.Text = gu.StatusGC;
                //txtGCExpirationDate.Text = gu.ExpirationDate;

                ////load pics
                ////load guest
                //if (!File.Exists(Server.MapPath("~/ProfilePic/") + gu.GuestId + "_Profile.png"))
                //{
                //    imgProfile.ImageUrl = "~/ProfilePic/noImage.png";
                //}
                //else
                //{
                //    imgProfile.ImageUrl = "~/ProfilePic/" + gu.GuestId + "_Profile.png";
                //}

                //if (!File.Exists(Server.MapPath("~/IDPic/") + gu.GuestId + "_IDPic.png"))
                //{
                //    IDPic.ImageUrl = "~/IDPic/noImage.png";
                //}
                //else
                //{
                //    IDPic.ImageUrl = "~/IDPic/" + gu.GuestId + "_IDPic.png";
                //}
            }
            else if (e.CommandName.Equals("redirectGC"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGC.Rows[index].FindControl("lblGCNo")).Text;
                Response.Redirect("~/fo/viewgcform.aspx?gcId=" + rowId);
            }
            else if(e.CommandName.Equals("usedRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvGC.Rows[index].FindControl("lblRowId")).Text;

                //chk if complete
                string btnComplete = ((Button)gvGC.Rows[index].FindControl("btnUsed")).Text;

                if(btnComplete == "Complete")
                {
                    hfBtnUsedStatus.Value = "Complete";
                    lblForUseGCTitle.Text = "Complete GC";
                    lblForUseGCContent.Text = "Are you sure you want to complete this Gift Check and move it to History ?";
                }

                hfUsedGCId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#usedModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("cancelledRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvGC.Rows[index].FindControl("lblRowId")).Text;

                hfCancellationId.Value = rowId;

                var tran = (from gc in db.GCTransactions
                            where gc.Id == Convert.ToInt32(hfCancellationId.Value)
                            select gc).FirstOrDefault();

                txtCancellationReason.Text = tran.CancellationReason;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#cancelledModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("redirectGuest"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGC.Rows[index].FindControl("lbtnGuestId")).Text;

                //chk if company
                var g = (from gu in db.Guests
                         where gu.Id == Convert.ToInt32(rowId)
                         select gu).FirstOrDefault();

                if (g.IsCompany == true)
                {
                    Response.Redirect("~/company/edit-company.aspx?companyId=" + rowId);
                }
                else
                {
                    Response.Redirect("~/guest/editguest.aspx?guestid=" + rowId);
                }
            }
            else if (e.CommandName.Equals("redirectCompany"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGC.Rows[index].FindControl("lbtnGuestId")).Text;

                //chk if company
                var g = (from gu in db.Guests
                         where gu.Id == Convert.ToInt32(rowId)
                         select gu).FirstOrDefault();

                if(g.IsCompany == true)
                {
                    Response.Redirect("~/company/edit-company.aspx?companyId=" + rowId);
                }
                else
                {
                    Response.Redirect("~/company/edit-company.aspx?companyId=" + g.CompanyId.ToString());
                }    
            }
        }

        protected void bindGridview()
        {
            string strSearch = txtSearch.Text;

            var q = from guest in db.Guests
                    join gctran in db.GCTransactions
                    on guest.Id equals gctran.GuestId
                    where
                    (guest.GuestId.Contains(strSearch) ||
                    guest.LastName.Contains(strSearch) ||
                    guest.FirstName.Contains(strSearch) ||
                    guest.MiddleName.Contains(strSearch) ||
                    guest.CompanyName.Contains(strSearch) ||
                    gctran.GCNumber.Contains(strSearch) ||
                    gctran.ApprovalStatus.Contains(strSearch)) &&
                    (gctran.ApprovalStatus == "Approved")
                    select new
                    {
                        Id = gctran.Id,
                        GuestId = guest.GuestId,
                        FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                        CompanyName = guest.CompanyName,
                        Number = guest.ContactNumber,
                        GCNumber = gctran.GCNumber,
                        Status = gctran.StatusGC,
                    };

            gvGC.DataSource = q.ToList();
            gvGC.DataBind();

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
                     guest.CompanyName.Contains(strSearch) ||
                     gctran.GCNumber.Contains(strSearch) ||
                     gctran.ApprovalStatus.Contains(strSearch)
                     ) &&
                     (gctran.ApprovalStatus == "Approved") &&
                     (gctran.IsArchive == false)
                     select new
                     {
                         GuestId = guest.GuestId,
                         FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                         CompanyName = (from gu in db.Guests where guest.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                         Number = guest.ContactNumber,
                         GCNumber = gctran.GCNumber,
                         ExpiryDate = gctran.ExpirationDate,
                         Status = gctran.StatusGC
                     }).ToList();

            GridView1.DataSource = q;
            GridView1.DataBind();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("GC Lists");
            var totalCols = GridView1.Rows[0].Cells.Count;
            var totalRows = GridView1.Rows.Count;
            var headerRow = GridView1.HeaderRow;
            for (var i = 1; i <= totalCols; i++)
            {
                workSheet.Cells[1, i].Value = headerRow.Cells[i - 1].Text;
            }
            for (var j = 1; j <= totalRows; j++)
            {
                for (var i = 1; i <= totalCols; i++)
                {
                    var product = q.ElementAt(j - 1);
                    workSheet.Cells[j + 1, i].Value = product.GetType().GetProperty(headerRow.Cells[i - 1].Text).GetValue(product, null);
                }
            }
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=GC.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        protected void btnConfirmUseGC_Click(object sender, EventArgs e)
        {
            int gcId = Convert.ToInt32(hfUsedGCId.Value);
            var tran = (from gc in db.GCTransactions
                        where gc.Id == gcId
                        select gc).FirstOrDefault();

            if(hfBtnUsedStatus.Value == "Complete")
            {
                tran.StatusGC = "Completed";
                //tran.IsArchive = true;
            }
            else
            {
                tran.StatusGC = "Used";
            }
            
            db.SubmitChanges();
            this.gvGC.DataBind();

            //audit trail
            DBLogger.Log("Update", "Updated GC Front-office to :" + tran.StatusGC , tran.GCNumber);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#usedModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void btnConfirmCancellation_Click(object sender, EventArgs e)
        {
            int gcId = Convert.ToInt32(hfCancellationId.Value);
            var tran = (from gc in db.GCTransactions
                        where gc.Id == gcId
                        select gc).FirstOrDefault();

            tran.ApprovalStatus = "Pending";
            tran.StatusGC = "Cancelled";
            tran.CancellationReason = txtCancellationReason.Text;
            tran.CancelledDate = DateTime.Now;

            db.SubmitChanges();
            this.gvGC.DataBind();

            //audit trail
            DBLogger.Log("Cancelled", "Cancelled GC - front office", tran.GCNumber);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#cancelledModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
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
                     gctran.GCNumber.Contains(strSearch) ||
                     gctran.ApprovalStatus.Contains(strSearch)
                     )
                     &&
                     (gctran.ApprovalStatus == "Approved") &&
                     (gctran.IsArchive == false)
                     select new
                     {
                         Id = gctran.Id,
                         GuestId = guest.Id,
                         GuestIdName = guest.GuestId,
                         FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                         CompanyName = (from gu in db.Guests where guest.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                         Number = guest.ContactNumber,
                         GCNumber = gctran.GCNumber,
                         ExpiryDate = gctran.ExpirationDate,
                         Status = gctran.StatusGC,
                         Type = gctran.GCType,
                         CompanyId = guest.CompanyId
                     }).ToList();

            if(ddlGCStatus.SelectedValue != "0")
            {
                q = q.Where(s => s.Status == ddlGCStatus.SelectedValue).ToList();
            }

            if(ddlCompanyName.SelectedValue != "0")
            {
                q = q.Where(c => c.CompanyId == Convert.ToInt32(ddlCompanyName.SelectedValue)).ToList();
            }

            e.Result = q;

            txtSearch.Focus();
        }

        protected void gvGC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGCStatus = e.Row.FindControl("lblGCStatus") as Label;
                //Button btnUse = e.Row.FindControl("btnUsed") as Button;
                //Button btnCancel = e.Row.FindControl("btnCancelled") as Button;

                if(lblGCStatus.Text == "Used")
                {
                    lblGCStatus.CssClass = "badge btn-info";
                    //btnUse.Text = "Complete";
                    //btnUse.CssClass = "btn btn-primary";
                }
                else if(lblGCStatus.Text == "Completed")
                {
                    //btnUse.Visible = false;
                    //btnCancel.Visible = false;
                    lblGCStatus.CssClass = "badge btn-success";
                }
                else if(lblGCStatus.Text == "Cancelled")
                {
                    lblGCStatus.CssClass = "badge btn-danger";
                }
            }
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

        private void checkExpiration()
        {
            var trans = (from tran in db.GCTransactions
                         where
                         (DateTime.Today >= tran.ExpirationDate) &&
                         (tran.StatusGC == "Waiting")
                         select tran).ToList();

            foreach (var tr in trans)
            {
                tr.StatusGC = "Expired";
                db.SubmitChanges();
            }
        }

        private void CheckOutDate()
        {
            var trans = (from tran in db.GCTransactions
                         where
                         (tran.CheckinDate.HasValue) &&
                         (tran.CheckoutDate.HasValue)
                         select tran).ToList();

            foreach (var tr in trans)
            {
                if(DateTime.Today > tr.CheckoutDate)
                {
                    tr.StatusGC = "Completed";
                }
                else if((DateTime.Today < tr.CheckoutDate))
                {
                    tr.StatusGC = "Used";
                } 
            }
            db.SubmitChanges();
        }
    }
}