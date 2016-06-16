using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace eGC.gcapproval
{
    public partial class _default : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindDropdown();
                checkExpiration();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvGC.DataBind();
        }

        protected void gvGC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGCStatus = e.Row.FindControl("lblGCStatus") as Label;
                Label lblApproval = e.Row.FindControl("lblApproval") as Label;
                Button btnDisapprove = e.Row.FindControl("btnDisapprove") as Button;

                if(lblGCStatus.Text == "Cancelled")
                {
                    lblGCStatus.ForeColor = Color.Red;
                    //btnDisapprove.Visible = false;
                }

                if (lblApproval.Text == "Disapproved")
                {
                    lblApproval.ForeColor = Color.DarkRed;
                    lblApproval.Font.Bold = true;
                }
            }
        }

        protected void gvGC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("redirectGuest"))
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
                    Response.Redirect("~/guest/editguest.aspx?guestid=" + rowId);
                }
            }
            else if (e.CommandName.Equals("redirectGC"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((LinkButton)gvGC.Rows[index].FindControl("lblGCNo")).Text;
                Response.Redirect("~/tran/editgcform.aspx?gcId=" + rowId);
            }
            else if (e.CommandName.Equals("approveRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvGC.Rows[index].FindControl("lblRowId")).Text;

                hfApproveGCId.Value = rowId;

                //check if gc is status and modify text
                var tran = (from gc in db.GCTransactions
                            where gc.Id == Convert.ToInt32(hfApproveGCId.Value)
                            select gc).FirstOrDefault();

                if(tran.StatusGC == "Cancelled")
                {
                    lblApproveTitle.Text = "Approve Cancelled Gift Check";
                    lblApproveContent.Text = "Are you sure you want to approve this Cancelled Gift Check and move it to History?";
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#approveModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("disapproveRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvGC.Rows[index].FindControl("lblRowId")).Text;

                hfDisapproveGCId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#disapproveModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvGC.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteGCId.Value = rowId;
                Javascript.ShowModal(this, this, "deleteModal");
            }
        }

        protected void btnConfirmApproveGC_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(hfApproveGCId.Value);
            var tran = (from gc in db.GCTransactions
                        where gc.Id == Id
                        select gc).FirstOrDefault();

            tran.ApprovalStatus = "Approved";
            tran.ApprovedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

            if(tran.StatusGC == "Cancelled")
            {
                tran.IsArchive = true;
            }

            db.SubmitChanges();

            this.gvGC.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#approveModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void btnConfirmDisapproveGC_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(hfDisapproveGCId.Value);
            var tran = (from gc in db.GCTransactions
                        where gc.Id == Id
                        select gc).FirstOrDefault();

            tran.ApprovalStatus = "Disapproved";
            db.SubmitChanges();

            this.gvGC.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#disapproveModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(hfDeleteGCId.Value);
            var gc = (from g in db.GCTransactions
                      where g.Id == Id
                      select g).FirstOrDefault();
            db.GCTransactions.DeleteOnSubmit(gc);
            db.SubmitChanges();

            this.gvGC.DataBind();
            Javascript.HideModal(this, this, "deleteModal");
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

        private void checkExpiration()
        {
            var trans = (from tran in db.GCTransactions
                         where 
                         (DateTime.Today >= tran.ExpirationDate) &&
                         (tran.StatusGC == "Waiting")
                         select tran).ToList();

            foreach(var tr in trans)
            {
                tr.StatusGC = "Expired";
                db.SubmitChanges();
            }
        }

        protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;

            if (ddlCompanyName.SelectedValue == "0")
            {
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
                         gctran.GCType.Contains(strSearch) ||
                         gctran.RequestedBy.Contains(strSearch) ||
                         gctran.RecommendingApproval.Contains(strSearch) ||
                         gctran.StatusGC.Contains(strSearch) ||
                         gctran.ApprovalStatus.Contains(strSearch) ||
                         gctran.CancellationReason.Contains(strSearch) ||
                         gctran.Room.Room1.Contains(strSearch) ||
                         gctran.Dining.Name.Contains(strSearch) ||
                         gctran.DiningType.DiningType1.Contains(strSearch)
                         ) &&
                         (gctran.ApprovalStatus == "Pending" || gctran.ApprovalStatus == "Disapproved") &&
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
                             Approval = gctran.ApprovalStatus,
                             CancellationReason = gctran.CancellationReason,
                             CancelledDate = gctran.CancelledDate,
                             Type = gctran.GCType
                         }).ToList();

                e.Result = q;
            }
            else
            {
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
                         gctran.GCType.Contains(strSearch) ||
                         gctran.RequestedBy.Contains(strSearch) ||
                         gctran.RecommendingApproval.Contains(strSearch) ||
                         gctran.StatusGC.Contains(strSearch) ||
                         gctran.ApprovalStatus.Contains(strSearch) ||
                         gctran.CancellationReason.Contains(strSearch) ||
                         gctran.Room.Room1.Contains(strSearch) ||
                         gctran.Dining.Name.Contains(strSearch) ||
                         gctran.DiningType.DiningType1.Contains(strSearch)
                         ) &&
                         (gctran.ApprovalStatus == "Pending" || gctran.ApprovalStatus == "Disapproved") &&
                         (gctran.IsArchive == false) &&
                         (guest.CompanyId == Convert.ToInt32(ddlCompanyName.SelectedValue))
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
                             Approval = gctran.ApprovalStatus,
                             CancellationReason = gctran.CancellationReason,
                             CancelledDate = gctran.CancelledDate,
                             Type = gctran.GCType
                         }).ToList();

                e.Result = q;
            }

            txtSearch.Focus();
        }
    }
}