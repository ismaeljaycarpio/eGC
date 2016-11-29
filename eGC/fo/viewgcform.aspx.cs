using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using eGC.DAL;

namespace eGC.fo
{
    public partial class viewgcform : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        UserAccountsDataContext dbUser = new UserAccountsDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {            
                if (Request.QueryString["gcId"] == null)
                {
                    Response.Redirect("~/fo/frontoffice.aspx");
                }

                //chk gc
                string gcId = Request.QueryString["gcId"];

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
                    txtProperty.Text = tGC.Property;
                    txtGCNumber.Text = tGC.GCNumber;
                    txtDateIssued.Text = tGC.DateIssued.Value.ToString("MM/dd/yyyy");
                    ddlGCType.SelectedValue = tGC.GCType;
                    txtExpirationDate.Text = String.Format("{0:MM/dd/yyyy}", tGC.ExpirationDate);
                    txtRemarks.Text = tGC.Remarks;
                    lblCurrentGCStatus.Text = tGC.StatusGC;

                    //check-dates
                    txtCheckin.Text = tGC.CheckinDate.ToString();
                    txtCheckout.Text = tGC.CheckoutDate.ToString();
                    ddlGCStatus.SelectedValue = tGC.StatusGC;

                    //chk gc type
                    if(ddlGCType.SelectedItem.Text == "Representation")
                    {
                        ddlGCType.Enabled = true;
                        txtExpirationDate.Enabled = true;
                        txtRemarks.Enabled = true;
                    }

                    //chk creator
                    if(tGC.CreatedBy != null)
                    {
                        var c = dbUser.UserProfiles.Where(n => n.UserId == tGC.CreatedBy).FirstOrDefault();
                        txtCreatedBy.Text = c.LastName + ", " + c.FirstName + " " + c.MiddleName;
                    }

                    //chk approver
                    if (tGC.ApprovedBy != null)
                    {
                        var u = (from user in dbUser.UserProfiles
                                 where user.UserId == tGC.ApprovedBy
                                 select user).FirstOrDefault();

                        if (u != null)
                        {
                            txtApprovedBy.Text = u.LastName + " , " + u.FirstName + " " + u.MiddleName;
                        }
                    }
                    else
                    {
                        pnlApprovedBy.Visible = false;
                    }

                    //load guest
                    var guest = (from gu in db.Guests
                                 where gu.Id == tGC.GuestId
                                 select gu).FirstOrDefault();

                    txtName.Text = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName;
                    txtGuestId.Text = guest.GuestId;

                    txtCompany.Text = (from c in db.Guests
                                       where guest.CompanyId == c.Id
                                       select c).FirstOrDefault().CompanyName;

                    txtEmail.Text = guest.Email;
                    txtContactNo.Text = guest.ContactNumber;
                    txtContactPerson.Text = guest.EmergencyContactPerson;

                    //lazy load rooms
                    var rooms = (from r in db.Rooms
                                 select r).ToList();

                    if (rooms.Count > 0)
                    {
                        ddlRooms.DataSource = rooms.ToList();
                        ddlRooms.DataTextField = "Room1";
                        ddlRooms.DataValueField = "Id";
                        ddlRooms.DataBind();
                        ddlRooms.Items.Insert(0, new ListItem("-- Select Room --", "0"));
                    }

                    //lazy load dining
                    var dining = (from d in db.Dinings
                                  select d).ToList();

                    if (dining.Count > 0)
                    {
                        ddlDining.DataSource = dining;
                        ddlDining.DataTextField = "Name";
                        ddlDining.DataValueField = "Id";
                        ddlDining.DataBind();
                        ddlDining.Items.Insert(0, new ListItem("-- Select Dining --", "0"));
                    }

                    //lazy load dining type
                    var diningtype = (from dt in db.DiningTypes
                                      where dt.Active == true
                                      select dt).ToList();

                    if (diningtype.Count > 0)
                    {
                        ddlDiningType.DataSource = diningtype;
                        ddlDiningType.DataTextField = "DiningType1";
                        ddlDiningType.DataValueField = "Id";
                        ddlDiningType.DataBind();
                        ddlDiningType.Items.Insert(0, new ListItem("-- Select Dining Type --", "0"));
                    }

                    //chk if company
                    if (guest.IsCompany == true)
                    {
                        panelName.Visible = false;
                        lblForGuestId.InnerText = "Company ID";
                    }

                    hlPrintForm.NavigateUrl = "~/tran/print-form.aspx?gcId=" + Request.QueryString["gcId"];

                    //chk if rooms
                    if (tGC.RoomId != null)
                    {
                        pnlRoom.Visible = true;
                        ddlRooms.SelectedValue = tGC.RoomId.ToString();
                        rblRoomBreakfast.SelectedValue = tGC.WithBreakfast.ToString();
                        txtRoomHeadCount.Text = tGC.HeadCount.ToString();
                    }
                    else if (tGC.DiningId != null)
                    {
                        pnlDining.Visible = true;
                        ddlDining.SelectedValue = tGC.DiningId.ToString();
                        ddlDiningType.SelectedValue = tGC.DiningTypeId.ToString();
                        txtDiningHeadCount.Text = tGC.HeadCount.ToString();
                    }

                    checkUserAcces();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var tran = (from tr in db.GCTransactions
                        where tr.GCNumber == Request.QueryString["gcId"]
                        select tr).FirstOrDefault();


            //tran.GCNumber = txtGCNumber.Text;
            //tran.RecommendingApproval = txtRecommendingApproval.Text;
            //tran.ApprovedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            //tran.AccountNo = txtAccountNo.Text;
            //tran.Remarks = txtRemarks.Text;
            //tran.GCType = ddlGCType.SelectedValue;

            tran.Remarks = txtRemarks.Text;
            tran.GCType = ddlGCType.SelectedValue;
            if(txtExpirationDate.Text != String.Empty)
            {
                tran.ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text);
            }
            else
            {
                tran.ExpirationDate = null;
            }

            if(txtCheckin.Text != String.Empty)
            {
                tran.CheckinDate = Convert.ToDateTime(txtCheckin.Text);
            }
            else
            {
                tran.CheckinDate = null;
            }

            if(txtCheckout.Text != String.Empty)
            {
                tran.CheckoutDate = Convert.ToDateTime(txtCheckout.Text);
            }
            else
            {
                tran.CheckoutDate = null;
            }

            if(ddlGCStatus.SelectedValue != "")
            {
                tran.StatusGC = ddlGCStatus.SelectedValue;
            }

            db.SubmitChanges();

            //audit trail
            DBLogger.Log("Update", "Updated GC Number: \n " +
                "Set Status: " + tran.StatusGC, tran.GCNumber);

            if(ddlGCStatus.SelectedValue == "Cancelled")
            {
                Javascript.ShowModal(this, this, "cancelledModal");
            }
            else
            {
                Response.Redirect("~/fo/frontoffice.aspx");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/fo/frontoffice.aspx");
        }

        protected void btnUsed_Click(object sender, EventArgs e)
        {
            string gcId = Request.QueryString["gcId"];
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == gcId
                      select g).FirstOrDefault();

            gc.StatusGC = "Used";
            db.SubmitChanges();

            //audit trail
            DBLogger.Log("Update", "Updated GC Number: View from Front Office, Set status to " + gc.StatusGC, 
                gc.GCNumber);

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

            //audit trail
            DBLogger.Log("Cancelled", "Cancelled GC Number: View from Front Office, Set status to " + gc.StatusGC,
                gc.GCNumber);

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

            //audit trail
            DBLogger.Log("Update", "Updated GC Number: View from Front Office, Set status to " + gc.StatusGC,
                gc.GCNumber);

            Response.Redirect("~/fo/frontoffice.aspx");
        }

        protected void btnConfirmCancellation_Click(object sender, EventArgs e)
        {
            var gc = (from tr in db.GCTransactions
                        where tr.GCNumber == Request.QueryString["gcId"]
                        select tr).FirstOrDefault();

            gc.ApprovalStatus = "Pending";
            gc.StatusGC = "Cancelled";
            gc.CancellationReason = txtCancellationReason.Text;
            gc.CancelledDate = DateTime.Now;

            db.SubmitChanges();

            //audit trail
            DBLogger.Log("Cancelled", "Cancelled GC Number: View from Front Office, Set status to " + gc.StatusGC,
                gc.GCNumber);

            Response.Redirect("~/fo/frontoffice.aspx");
        }

        protected void checkUserAcces()
        {
            if(!User.IsInRole("Admin-GC") && !User.IsInRole("frontoffice"))
            {
                txtCheckin.Enabled = false;
                txtCheckout.Enabled = false;
                ddlGCStatus.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }

        protected void ddlGCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGCType.SelectedValue == "Representation")
            {
                txtExpirationDate.Enabled = true;
                RequiredFieldValidator15.Enabled = true;

                var q = (from gc in db.GCTransactions
                         where gc.GCNumber == Request.QueryString["gcId"]
                         select gc).FirstOrDefault().ExpirationDate;

                if (q.HasValue)
                {
                    txtExpirationDate.Text = String.Format(q.Value.ToString("MM/dd/yyyy"));
                }
                else
                {
                    txtExpirationDate.Text = "";
                }
            }
            else
            {
                txtExpirationDate.Enabled = false;
                RequiredFieldValidator15.Enabled = false;
                txtExpirationDate.Text = String.Empty;
            }
        }
    }
}