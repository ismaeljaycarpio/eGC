using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.fo
{
    public partial class frontoffice : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.gvGC.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvGC.DataBind();
            txtSearch.Focus();
        }

        protected void gvGC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("selectGuest"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvGC.Rows[index].FindControl("lblRowId")).Text;
                
                //load guest profile
                var gu = (from g in db.Guests
                         join tran in db.GCTransactions
                         on g.GuestId equals tran.GuestId
                         where tran.Id == Convert.ToInt32(rowId)
                         select new
                         {
                             FullName = g.LastName + ", " + g.FirstName + " " + g.MiddleName,
                             GuestId = g.GuestId,
                             ArrivalDate = String.Format("{0: MM/dd/yyyy}", tran.ArrivalDate),
                             CheckoutDate = String.Format("{0: MM/dd/yyyy}", tran.CheckOutDate),
                             StatusGC = tran.StatusGC,
                             ExpirationDate = String.Format("{0:MM/dd/yyyy}", tran.ExpiryDate)
                         }).FirstOrDefault();

                txtName.Text = gu.FullName;
                txtGuestId.Text = gu.GuestId;
                txtArrival.Text = gu.ArrivalDate.ToString();
                txtCheckout.Text = gu.CheckoutDate.ToString();
                txtStatus.Text = gu.StatusGC;
                txtGCExpirationDate.Text = gu.ExpirationDate;

                //load pics
                //load guest
                if (!File.Exists(Server.MapPath("~/ProfilePic/") + gu.GuestId + "_Profile.png"))
                {
                    imgProfile.ImageUrl = "~/ProfilePic/noImage.png";
                }
                else
                {
                    imgProfile.ImageUrl = "~/ProfilePic/" + gu.GuestId + "_Profile.png";
                }

                if (!File.Exists(Server.MapPath("~/IDPic/") + gu.GuestId + "_IDPic.png"))
                {
                    IDPic.ImageUrl = "~/IDPic/noImage.png";
                }
                else
                {
                    IDPic.ImageUrl = "~/IDPic/" + gu.GuestId + "_IDPic.png";
                }
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
                        ArrivalDate = gctran.ArrivalDate,
                        CheckoutDate = gctran.CheckOutDate,
                        Status = gctran.StatusGC,
                        TotalValue = db.GCRooms.Where(x => x.GCTransactionId == gctran.Id).Sum(t => t.Total)
                    };

            gvGC.DataSource = q.ToList();
            gvGC.DataBind();

            txtSearch.Focus();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string strSearch = txtSearch.Text;
            var products = (from guest in db.Guests
                           join gctran in db.GCTransactions
                           on guest.GuestId equals gctran.GuestId
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
                               GuestId = guest.GuestId,
                               FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                               CompanyName = guest.CompanyName,
                               Number = guest.ContactNumber,
                               GCNumber = gctran.GCNumber,
                               ArrivalDate = gctran.ArrivalDate,
                               CheckoutDate = gctran.CheckOutDate,
                               Status = gctran.StatusGC,
                               TotalValue = db.GCRooms.Where(x => x.GCTransactionId == gctran.Id).Sum(t => t.Total)
                           }).ToList();

            GridView1.DataSource = products;
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
                    var product = products.ElementAt(j - 1);
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

            tran.StatusGC = "Used";
            db.SubmitChanges();

            this.gvGC.DataBind();

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
                     on guest.GuestId equals gctran.GuestId
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
                         Id = gctran.Id,
                         GuestId = guest.GuestId,
                         FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                         CompanyName = guest.CompanyName,
                         Number = guest.ContactNumber,
                         GCNumber = gctran.GCNumber,
                         ArrivalDate = gctran.ArrivalDate,
                         CheckoutDate = gctran.CheckOutDate,
                         Status = gctran.StatusGC,
                         TotalValue = db.GCRooms.Where(x => x.GCTransactionId == gctran.Id).Sum(t => t.Total)
                     }).ToList();

            e.Result = q;
        }

        protected void gvGC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGCStatus = e.Row.FindControl("lblGCStatus") as Label;
                Button btnUse = e.Row.FindControl("btnUsed") as Button;

                if(lblGCStatus.Text == "Used")
                {
                    btnUse.Visible = false;
                }
            }
        }
    }
}