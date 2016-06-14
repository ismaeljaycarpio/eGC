﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

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
                    hfGCNumber.Value = tGC.GCNumber;
                    txtRecommendingApproval.Text = tGC.RecommendingApproval;
                    txtDateIssued.Text = tGC.DateIssued.Value.ToString("MM/dd/yyyy");
                    txtRequestedBy.Text = tGC.RequestedBy;
                    txtRemarks.Text = tGC.Reason;
                    ddlGCType.SelectedValue = tGC.GCType;
                    txtExpirationDate.Text = String.Format("{0:MM/dd/yyyy}", tGC.ExpirationDate);

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

                    //load related table
                    bindRooms();
                    bindDinings();

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
                    ////lazy load rooms
                    //var rooms = (from r in db.Rooms
                    //             select r).ToList();

                    //if (rooms.Count > 0)
                    //{
                    //    ddlAddRoom.DataSource = rooms.ToList();
                    //    ddlAddRoom.DataTextField = "Room1";
                    //    ddlAddRoom.DataValueField = "Id";
                    //    ddlAddRoom.DataBind();

                    //    ddlEditRoom.DataSource = rooms.ToList();
                    //    ddlEditRoom.DataTextField = "Room1";
                    //    ddlEditRoom.DataValueField = "Id";
                    //    ddlEditRoom.DataBind();
                    //}

                    ////lazy load dining
                    //var dining = (from d in db.Dinings
                    //              select d).ToList();

                    //if (dining.Count > 0)
                    //{
                    //    ddlAddDining.DataSource = dining;
                    //    ddlAddDining.DataTextField = "Name";
                    //    ddlAddDining.DataValueField = "Id";
                    //    ddlAddDining.DataBind();

                    //    ddlEditDining.DataSource = dining;
                    //    ddlEditDining.DataTextField = "Name";
                    //    ddlEditDining.DataValueField = "Id";
                    //    ddlEditDining.DataBind();
                    //}

                    //chk if company
                    if (guest.IsCompany == true)
                    {
                        panelName.Visible = false;
                        lblForGuestId.InnerText = "Company ID";
                    }

                    hlPrintForm.NavigateUrl = "~/tran/print-form.aspx?gcId=" + Request.QueryString["gcId"];
                    txtRecommendingApproval.Focus();
                }
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

        protected void gvDining_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editDining"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load dining
                //var q = (from r in db.GCRooms
                //         where r.Id.Equals((int)gvDining.DataKeys[index].Value)
                //         select r).FirstOrDefault();

                //lblEditDiningId.Text = q.Id.ToString();
                //ddlEditDining.SelectedValue = q.DiningId.ToString();
                //ddlEditDiningType.SelectedValue = q.DiningType;
                //txtEditDiningHeadCount.Text = q.HowManyDiningPerson.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editDining').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteDining"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //string rowId = ((Label)gvDining.Rows[index].FindControl("lblDiningId")).Text;
                //hfDeleteDiningId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteDining').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //chk if modified gc number from prev
            if(txtGCNumber.Text != hfGCNumber.Value)
            {
                var gcs = (from gctran in db.GCTransactions
                           where gctran.GCNumber == txtGCNumber.Text.Trim()
                           select gctran).ToList();

                if (gcs.Count > 0)
                {
                    //show duplicate gc
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#duplicateGCModal').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);               
                }
                else
                {
                    var tran = (from tr in db.GCTransactions
                                where tr.GCNumber == Request.QueryString["gcId"]
                                select tr).FirstOrDefault();


                    tran.GCNumber = txtGCNumber.Text;
                    tran.RecommendingApproval = txtRecommendingApproval.Text;
                    //tran.AccountNo = txtAccountNo.Text;
                    //tran.Remarks = txtRemarks.Text;
                    tran.GCType = ddlGCType.SelectedValue;

                    if (txtExpirationDate.Text != String.Empty)
                    {
                        tran.ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text);
                    }
                    else
                    {
                        tran.ExpirationDate = null;
                    }

                    db.SubmitChanges();
                    Response.Redirect("~/gcapproval/default.aspx");
                }
            }
            else
            {
                var tran = (from tr in db.GCTransactions
                            where tr.GCNumber == Request.QueryString["gcId"]
                            select tr).FirstOrDefault();


                //tran.GCNumber = txtGCNumber.Text;
                tran.RecommendingApproval = txtRecommendingApproval.Text;
                //tran.AccountNo = txtAccountNo.Text;
                //tran.Remarks = txtRemarks.Text;
                tran.GCType = ddlGCType.SelectedValue;

                if (txtExpirationDate.Text != String.Empty)
                {
                    tran.ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text);
                }
                else
                {
                    tran.ExpirationDate = null;
                }

                db.SubmitChanges();
                Response.Redirect("~/gcapproval/default.aspx");
            }
        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            //add to gcroom table
            //GCRoom tmp = new GCRoom();
            //tmp.GCTransactionId = Convert.ToInt32(hfTransactionId.Value);
            //tmp.RoomId = Convert.ToInt32(ddlAddRoom.SelectedValue);
            //tmp.WithBreakfast = ddlAddRoomBreakfast.SelectedValue;
            //tmp.HowManyPerson = Convert.ToInt32(txtAddRoomHeadCount.Text);

            //db.GCRooms.InsertOnSubmit(tmp);
            db.SubmitChanges();

            bindRooms();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addRoom').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdateRoom_Click(object sender, EventArgs e)
        {
            //var r = (from room in db.GCRooms
            //         where room.Id == Convert.ToInt32(lblEditRoomId.Text)
            //         select room).FirstOrDefault();

            //r.RoomId = Convert.ToInt32(ddlEditRoom.SelectedValue);
            //r.WithBreakfast = ddlEditRoomBreakfast.SelectedValue;
            //r.HowManyPerson = Convert.ToInt32(txtEditRoomHeadCount.Text);

            //db.SubmitChanges();

            bindRooms();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editRoom').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            //var q = (from r in db.GCRooms
            //         where r.Id == Convert.ToInt32(hfDeleteRoomId.Value)
            //         select r).FirstOrDefault();

            //db.GCRooms.DeleteOnSubmit(q);
            //db.SubmitChanges();

            bindRooms();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteRoom').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }

        protected void btnAddDining_Click(object sender, EventArgs e)
        {
            //add to temp table
            //GCRoom tmp = new GCRoom();
            //tmp.GCTransactionId = Convert.ToInt32(hfTransactionId.Value);
            //tmp.DiningId = Convert.ToInt32(ddlAddDining.SelectedValue);
            //tmp.DiningType = ddlAddDiningType.SelectedValue;
            //tmp.HowManyDiningPerson = Convert.ToInt32(txtAddDiningHeadCount.Text);

            //db.GCRooms.InsertOnSubmit(tmp);
            db.SubmitChanges();

            bindDinings();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addDining').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnEditDining_Click(object sender, EventArgs e)
        {
            //var d = (from dining in db.GCRooms
            //         where dining.Id == Convert.ToInt32(lblEditDiningId.Text)
            //         select dining).FirstOrDefault();

            //d.DiningId = Convert.ToInt32(ddlEditDining.SelectedValue);
            //d.DiningType = ddlEditDiningType.SelectedValue;
            //d.HowManyDiningPerson = Convert.ToInt32(txtEditDiningHeadCount.Text);

            //db.SubmitChanges();

            bindDinings();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editDining').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnDeleteDining_Click(object sender, EventArgs e)
        {
            //var q = (from r in db.GCRooms
            //         where r.Id == Convert.ToInt32(hfDeleteDiningId.Value)
            //         select r).FirstOrDefault();

            //db.GCRooms.DeleteOnSubmit(q);
            db.SubmitChanges();

            bindDinings();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteDining').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/gcapproval/default.aspx");
        }

        private void bindRooms()
        {
            //var q = from room in db.Rooms
            //        join gcroom in db.GCRooms
            //        on room.Id equals gcroom.RoomId
            //        join tr in db.GCTransactions 
            //        on gcroom.GCTransactionId equals tr.Id
            //        where tr.GCNumber == Request.QueryString["gcId"].ToString()
            //        select new
            //        {
            //            Id = gcroom.Id,
            //            Type = room.Type,
            //            Room = room.Room1,
            //            WithBreakfast = gcroom.WithBreakfast,
            //            HowManyPerson = gcroom.HowManyPerson
            //        };

            //gvRoom.DataSource = q.ToList();
            //gvRoom.DataBind();
        }

        private void bindDinings()
        {
            //var q = from dining in db.Dinings
            //        join gcdining in db.GCRooms
            //        on dining.Id equals gcdining.DiningId
            //        join tr in db.GCTransactions
            //        on gcdining.GCTransactionId equals tr.Id
            //        where tr.GCNumber == Request.QueryString["gcId"].ToString()
            //        select new
            //        {
            //            Id = gcdining.Id,
            //            Name = dining.Name,
            //            DiningType = gcdining.DiningType,
            //            HeadCount = gcdining.HowManyDiningPerson
            //        };

            //gvDining.DataSource = q.ToList();
            //gvDining.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            //chk gc
            var gc = (from g in db.GCTransactions
                      where g.GCNumber == Request.QueryString["gcId"]
                      select g).FirstOrDefault();

            //check if cancelled
            if (gc.StatusGC == "Cancelled")
            {
                gc.IsArchive = true;
            }

            gc.ApprovalStatus = "Approved";
            db.SubmitChanges();
            Response.Redirect("~/gcapproval/default.aspx");
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
            }
            else
            {
                txtExpirationDate.Enabled = false;
                RequiredFieldValidator1.Enabled = false;
                txtExpirationDate.Text = String.Empty;
            }
        }
    }
}