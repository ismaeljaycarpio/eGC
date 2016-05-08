using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.fo
{
    public partial class viewgcform : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        EHRISDataContextDataContext dbEHRIS = new EHRISDataContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string gcId = Request.QueryString["gcId"];
                if (gcId == String.Empty)
                {
                    Response.Redirect("~/fo/frontoffice.aspx");
                }

                //chk gc
                var gc = (from g in db.GCTransactions
                          where g.GCNumber == gcId
                          select g).ToList();

                if (gc.Count < 1)
                {
                    Response.Redirect("~/fo/frontoffice.aspx");
                }
                else
                {
                    TabName.Value = Request.Form[TabName.UniqueID];

                    //load gc
                    var tGC = gc.FirstOrDefault();

                    int id = tGC.Id;
                    hfTransactionId.Value = id.ToString();
                    txtGCNumber.Text = tGC.GCNumber;
                    txtRecommendingApproval.Text = tGC.RecommendingApproval;
                    //txtApprovedBy.Text = tGC.ApprovedBy;
                    txtAccountNo.Text = tGC.AccountNo;
                    txtRemarks.Text = tGC.Remarks;
                    txtReason.Text = tGC.Reason;
                    txtArrivalDate.Text = String.Format("{0:MM/dd/yyyy}", tGC.ArrivalDate);
                    txtCheckoutDate.Text = String.Format("{0:MM/dd/yyyy}", tGC.CheckOutDate);
                    txtExpirationDate.Text = String.Format("{0:MM/dd/yyyy}", tGC.ExpiryDate);
                    lblCurrentGCStatus.Text = tGC.StatusGC;

                    //chk approver
                    if (tGC.ApprovedBy != String.Empty)
                    {
                        var u = (from emp in dbEHRIS.EMPLOYEEs
                                 where emp.Emp_Id == tGC.ApprovedBy
                                 select emp).FirstOrDefault();

                        if (u != null)
                        {
                            txtApprovedBy.Text = u.LastName + " , " + u.FirstName + " " + u.MiddleName;
                        }
                    }

                    //load related table
                    bindRooms();
                    bindDinings();

                    //load guest
                    var guest = (from gu in db.Guests
                                 where gu.Id == tGC.GuestId
                                 select gu).FirstOrDefault();

                    txtName.Text = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName;
                    txtGuestId.Text = guest.GuestId;
                    txtCompany.Text = guest.CompanyName;
                    txtEmail.Text = guest.Email;
                    txtContactNo.Text = guest.ContactNumber;


                    //chk if company
                    if (guest.IsCompany == true)
                    {
                        panelName.Visible = false;
                        lblForGuestId.InnerText = "Company ID";
                    }
                }
            }
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/fo/frontoffice.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var tran = (from tr in db.GCTransactions
                        where tr.GCNumber == Request.QueryString["gcId"]
                        select tr).FirstOrDefault();


            tran.GCNumber = txtGCNumber.Text;
            tran.RecommendingApproval = txtRecommendingApproval.Text;
            tran.ApprovedBy = txtApprovedBy.Text;
            tran.AccountNo = txtAccountNo.Text;
            tran.Remarks = txtRemarks.Text;
            tran.Reason = txtReason.Text;
            tran.ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text);
            tran.CheckOutDate = Convert.ToDateTime(txtCheckoutDate.Text);

            db.SubmitChanges();
            Response.Redirect("~/fo/frontoffice.aspx");
        }



        private void bindRooms()
        {
            var q = from room in db.Rooms
                    join gcroom in db.GCRooms
                    on room.Id equals gcroom.RoomId
                    join tr in db.GCTransactions
                    on gcroom.GCTransactionId equals tr.Id
                    where tr.GCNumber == Request.QueryString["gcId"].ToString()
                    select new
                    {
                        Id = gcroom.Id,
                        Type = room.Type,
                        Room = room.Room1,
                        Status = gcroom.Status,
                        Nights = gcroom.Nights,
                        Value = gcroom.Value,
                        Total = gcroom.Total
                    };

            gvRoom.DataSource = q.ToList();
            gvRoom.DataBind();
        }

        private void bindDinings()
        {
            var q = from dining in db.Dinings
                    join gcdining in db.GCRooms
                    on dining.Id equals gcdining.DiningId
                    join tr in db.GCTransactions
                    on gcdining.GCTransactionId equals tr.Id
                    where tr.GCNumber == Request.QueryString["gcId"].ToString()
                    select new
                    {
                        Id = gcdining.Id,
                        Name = dining.Name,
                        Value = gcdining.Value
                    };

            gvDining.DataSource = q.ToList();
            gvDining.DataBind();
        }

        protected void btnUsed_Click(object sender, EventArgs e)
        {
            string gcId = Request.QueryString["gcId"];
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == gcId
                      select g).FirstOrDefault();

            gc.StatusGC = "Used";
            db.SubmitChanges();

            Response.Redirect("~/fo/frontoffice.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string gcId = Request.QueryString["gcId"];
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == gcId
                      select g).FirstOrDefault();

            gc.StatusGC = "Cancelled";
            db.SubmitChanges();

            Response.Redirect("~/fo/frontoffice.aspx");
        }

        protected void btnExpire_Click(object sender, EventArgs e)
        {
            string gcId = Request.QueryString["gcId"];
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == gcId
                      select g).FirstOrDefault();

            gc.StatusGC = "Expired";
            db.SubmitChanges();

            Response.Redirect("~/fo/frontoffice.aspx");
        }

        protected void btnWaiting_Click(object sender, EventArgs e)
        {
            string gcId = Request.QueryString["gcId"];
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == gcId
                      select g).FirstOrDefault();

            gc.StatusGC = "Waiting";
            db.SubmitChanges();

            Response.Redirect("~/fo/frontoffice.aspx");
        }
    }
}