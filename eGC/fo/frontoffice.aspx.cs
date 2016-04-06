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
                             ArrivalDate = tran.ArrivalDate.ToString(),
                             CheckoutDate = tran.CheckOutDate.ToString()
                         }).FirstOrDefault();

                txtName.Text = gu.FullName;
                txtGuestId.Text = gu.GuestId;
                txtArrival.Text = gu.ArrivalDate;
                txtCheckout.Text = gu.CheckoutDate;

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
                    guest.CompanyName.Contains(strSearch)) &&
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
                        Status = gctran.ApprovalStatus,
                        TotalValue = db.GCRooms.Where(x => x.GCTransactionId == gctran.Id).Sum(t => t.Total)
                    };

            gvGC.DataSource = q.ToList();
            gvGC.DataBind();

            txtSearch.Focus();
        }
    }
}