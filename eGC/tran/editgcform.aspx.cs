using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using eGC.DAL;

namespace eGC.tran
{
    public partial class editgcform : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        UserAccountsDataContext dbUser = new UserAccountsDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string gcId = Request.QueryString["gcId"];
                if(gcId == String.Empty)
                {
                    Response.Redirect("~/gcapproval/default.aspx");
                }

                //chk gc
                var gc = (from g in db.GCTransactions
                          where g.GCNumber == gcId
                          select g).ToList();

                if(gc.Count < 1)
                {
                    Response.Redirect("~/gcapproval/default.aspx");
                }
                else
                {                    
                    TabName.Value = Request.Form[TabName.UniqueID];

                    //load gc
                    var tGC = gc.FirstOrDefault();

                    int id = tGC.Id;
                    hfTransactionId.Value = id.ToString();
                    txtGCNumber.Text = tGC.GCNumber;
                    hfGCNumber.Value = tGC.GCNumber; //store old gc number to server control in case of modification
                    txtDateIssued.Text = tGC.DateIssued.Value.ToString("MM/dd/yyyy");
                    txtRemarks.Text = tGC.Remarks;
                    ddlGCType.SelectedValue = tGC.GCType;
                    txtExpirationDate.Text = String.Format("{0:MM/dd/yyyy}", tGC.ExpirationDate);

                    //chk creator
                    if(tGC.CreatedBy != null)
                    {
                        var c = dbUser.UserProfiles.Where(n => n.UserId == tGC.CreatedBy).FirstOrDefault();
                        txtCreatedBy.Text = c.LastName + ", " + c.FirstName + " " + c.MiddleName;
                    }

                    //chk approver
                    if(tGC.ApprovedBy != null)
                    {
                        var u = (from user in dbUser.UserProfiles
                                 where user.UserId == tGC.ApprovedBy
                                 select user).FirstOrDefault();

                        if(u != null)
                        {
                            txtApprovedBy.Text = u.LastName + " , " + u.FirstName + " " + u.MiddleName;
                        }
                    }
                    else
                    {
                        pnlApprovedBy.Visible = false;
                    }

                    //chk gc type
                    if(tGC.GCType == "Representation")
                    {
                        txtExpirationDate.Enabled = true;
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

                    if(diningtype.Count > 0)
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
                    if(tGC.RoomId != null)
                    {
                        pnlRoom.Visible = true;
                        ddlRooms.SelectedValue = tGC.RoomId.ToString();
                        rblRoomBreakfast.SelectedValue = tGC.WithBreakfast.ToString();
                        txtRoomHeadCount.Text = tGC.HeadCount.ToString();
                    }
                    else if(tGC.DiningId != null)
                    {
                        pnlDining.Visible = true;
                        ddlDining.SelectedValue = tGC.DiningId.ToString();
                        ddlDiningType.SelectedValue = tGC.DiningTypeId.ToString();
                        txtDiningHeadCount.Text = tGC.HeadCount.ToString();
                    }

                    //chk if cancelled
                    if(tGC.StatusGC == "Cancelled")
                    {
                        pnlCancellation.Visible = true;
                        txtDateCancelled.Text = tGC.CancelledDate.Value.ToString("MM/dd/yyyy");
                        txtCancellationReason.Text = tGC.CancellationReason;
                    }

                    //chk user role
                    disableFields();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GCTransaction gc = (from tr in db.GCTransactions
                                    where tr.GCNumber == Request.QueryString["gcId"]
                                    select tr).FirstOrDefault();

            //chk if modified gc number from prev
            if(txtGCNumber.Text != hfGCNumber.Value)
            {
                var gcs = (from gctran in db.GCTransactions
                           where gctran.GCNumber == txtGCNumber.Text.Trim()
                           select gctran).ToList();

                if (gcs.Count > 0)
                {
                    //show duplicate gc
                    Javascript.ShowModal(this, this, "duplicateGCModal");            
                }
                else
                {
                    gc.GCNumber = txtGCNumber.Text;                    
                }
            }

            gc.DateIssued = Convert.ToDateTime(txtDateIssued.Text);
            gc.GCType = ddlGCType.SelectedItem.Text;

            if (txtExpirationDate.Text != String.Empty)
            {
                gc.ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text);
            }
            else
            {
                gc.ExpirationDate = null;
            }

            gc.Remarks = txtRemarks.Text;
            //gc.RequestedBy = txtRequestedBy.Text;

            //chk if rooms
            if (gc.RoomId != null)
            {
                gc.RoomId = Convert.ToInt32(ddlRooms.SelectedValue);
                gc.WithBreakfast = Convert.ToBoolean(rblRoomBreakfast.SelectedValue);
                gc.HeadCount = Convert.ToInt32(txtRoomHeadCount.Text);
            }
            else if (gc.DiningId != null)
            {
                gc.DiningId = Convert.ToInt32(ddlDining.SelectedValue);
                gc.DiningTypeId = Convert.ToInt32(ddlDiningType.SelectedValue);
                gc.HeadCount = Convert.ToInt32(txtDiningHeadCount.Text);
            }

            db.SubmitChanges();

            //audit trail
            DBLogger.Log("Update", "Updated GC", gc.GCNumber);

            Response.Redirect("~/gcapproval/default.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/gcapproval/default.aspx");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            //chk gc
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == Request.QueryString["gcId"]
                      select g).FirstOrDefault();

            gc.ApprovedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            

            //check if cancelled
            if (gc.StatusGC == "Cancelled")
            {
                Javascript.ShowModal(this, this, "cancelledGCModal");
            }
            else
            {
                gc.ApprovalStatus = "Approved";
                db.SubmitChanges();
                Response.Redirect("~/gcapproval/default.aspx"); 
            }
        }

        protected void btnDisapprove_Click(object sender, EventArgs e)
        {
            //chk gc
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == Request.QueryString["gcId"]
                      select g).FirstOrDefault();

            gc.ApprovalStatus = "Disapproved";
            db.SubmitChanges();
            Response.Redirect("~/gcapproval/default.aspx");
        }

        protected void ddlGCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGCType.SelectedValue == "Representation")
            {
                txtExpirationDate.Enabled = true;
                RequiredFieldValidator1.Enabled = true;

                var q = (from gc in db.GCTransactions
                         where gc.GCNumber == Request.QueryString["gcId"]
                         select gc).FirstOrDefault().ExpirationDate;

                if(q.HasValue)
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
                RequiredFieldValidator1.Enabled = false;
                txtExpirationDate.Text = String.Empty;
            }
        }

        protected void gvRoom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRoom"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load room
                //var q = (from r in db.GCRooms
                //         where r.Id.Equals((int)gvRoom.DataKeys[index].Value)
                //         select r).FirstOrDefault();

                //lblEditRoomId.Text = q.Id.ToString();
                //ddlEditRoom.SelectedValue = q.RoomId.ToString();
                //ddlEditRoomBreakfast.SelectedValue = q.WithBreakfast;
                //txtEditRoomHeadCount.Text = q.HowManyPerson.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editRoom').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRoom"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //string rowId = ((Label)gvRoom.Rows[index].FindControl("lblRowId")).Text;
                //hfDeleteRoomId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteRoom').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void disableFields()
        {
            if(!User.IsInRole("Admin-GC") && !User.IsInRole("can-approve-gc"))
            {
                txtDateIssued.Enabled = false;
                txtRemarks.Enabled = false;
                ddlGCType.Enabled = false;
                txtExpirationDate.Enabled = false;
                txtGCNumber.Enabled = false;
                pnlRoom.Enabled = false;
                pnlDining.Enabled = false;
                pnlCancellation.Enabled = false;
                btnApprove.Visible = false;
                btnDisapprove.Visible = false;
                btnSave.Enabled = false;
            }
        }

        protected void btnConfirmCancelledGC_Click(object sender, EventArgs e)
        {
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == Request.QueryString["gcId"]
                      select g).FirstOrDefault();

            gc.ApprovalStatus = "Approved";
            gc.IsArchive = true;
            db.SubmitChanges();
            Response.Redirect("~/gcapproval/default.aspx"); 
        }
    }
}