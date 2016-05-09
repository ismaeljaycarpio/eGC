using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace eGC.tran
{
    public partial class gcform : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string guestId = Request.QueryString["guestid"];
                if (guestId == String.Empty)
                {
                    Response.Redirect("~/guest/default.aspx");
                }
                else
                {
                    var gu = (from g in db.Guests
                              where g.Id.Equals(guestId)
                              select g).ToList();

                    if (gu.Count < 1)
                    {
                        Response.Redirect("~/guest/default.aspx");
                    }
                    else
                    {
                        //clear content from tmp
                        flushTemp();

                        //maintain tab
                        TabName.Value = Request.Form[TabName.UniqueID];

                        //load guest
                        var guest = gu.FirstOrDefault();
                        txtName.Text = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName;
                        txtGuestId.Text = guest.GuestId;

                        txtCompany.Text = (from c in db.Guests
                                           where guest.CompanyId == c.Id
                                           select c).FirstOrDefault().CompanyName;

                        txtEmail.Text = guest.Email;
                        txtContactNo.Text = guest.ContactNumber;

                        //load gcnumber
                        int maxId = db.GCTransactions.DefaultIfEmpty().Max(r => r == null ? 0 : r.Id);
                        maxId += 1;
                        txtGCNumber.Text = "2600-" + DateTime.Now.Year.ToString() + "-" + maxId.ToString();

                        //lazy load rooms
                        var rooms = (from r in db.Rooms
                                     select r).ToList();

                        if (rooms.Count > 0)
                        {
                            ddlAddRoom.DataSource = rooms.ToList();
                            ddlAddRoom.DataTextField = "Room1";
                            ddlAddRoom.DataValueField = "Id";
                            ddlAddRoom.DataBind();

                            ddlEditRoom.DataSource = rooms.ToList();
                            ddlEditRoom.DataTextField = "Room1";
                            ddlEditRoom.DataValueField = "Id";
                            ddlEditRoom.DataBind();
                        }

                        //lazy load dining
                        var dining = (from d in db.Dinings
                                      select d).ToList();

                        if(dining.Count > 0)
                        {
                            ddlAddDining.DataSource = dining;
                            ddlAddDining.DataTextField = "Name";
                            ddlAddDining.DataValueField = "Id";
                            ddlAddDining.DataBind();

                            ddlEditDining.DataSource = dining;
                            ddlEditDining.DataTextField = "Name";
                            ddlEditDining.DataValueField = "Id";
                            ddlEditDining.DataBind();
                        }

                        //chk if company
                        if(guest.IsCompany == true)
                        {
                            panelName.Visible = false;
                            lblForGuestId.InnerText = "ID";
                        }

                        txtRecommendingApproval.Focus();
                    }
                }
            }
        }

        protected void gvRoom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRoom"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load room
                var q = (from r in db.tmpRooms
                         where r.Id.Equals((int)gvRoom.DataKeys[index].Value)
                         select r).FirstOrDefault();

                lblEditRoomId.Text = q.Id.ToString();
                ddlEditRoom.SelectedValue = q.RoomId.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editRoom').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRoom"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvRoom.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteRoomId.Value = rowId;

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
                var q = (from r in db.tmpRooms
                         where r.Id.Equals((int)gvDining.DataKeys[index].Value)
                         select r).FirstOrDefault();

                lblEditDiningId.Text = q.Id.ToString();
                ddlEditDining.SelectedValue = q.DiningId.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editDining').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteDining"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvDining.Rows[index].FindControl("lblDiningId")).Text;
                hfDeleteDiningId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteDining').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GCTransaction tran = new GCTransaction();
            tran.GuestId = Convert.ToInt32(Request.QueryString["guestid"]);
            tran.GCNumber = txtGCNumber.Text;
            tran.RecommendingApproval = txtRecommendingApproval.Text;
            //tran.ApprovedBy = txtApprovedBy.Text;
            tran.AccountNo = txtAccountNo.Text;
            tran.Remarks = txtRemarks.Text;
            tran.Reason = txtReason.Text;
            tran.ApprovalStatus = "Pending";
            tran.StatusGC = "Waiting";
            tran.ExpiryDate = Convert.ToDateTime(txtExpirationDate.Text);
            tran.IsArchive = false;

            db.GCTransactions.InsertOnSubmit(tran);
            db.SubmitChanges();

            int id = tran.Id;

            //insert rooms
            var tmpRoom = (from tmp in db.tmpRooms
                           where tmp.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())
                           select tmp).ToList();

            foreach (var r in tmpRoom)
            {
                //insert to tran
                GCRoom room = new GCRoom();
                room.GCTransactionId = id;
                room.RoomId = r.RoomId;
                room.DiningId = r.DiningId;

                db.GCRooms.InsertOnSubmit(room);
            }

            db.SubmitChanges();
            Response.Redirect("~/guest/default.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/guest/default.aspx");
        }

        protected void btnUpdateRoom_Click(object sender, EventArgs e)
        {
            var r = (from room in db.tmpRooms
                     where room.Id == Convert.ToInt32(lblEditRoomId.Text)
                     select room).FirstOrDefault();

            r.RoomId = Convert.ToInt32(ddlEditRoom.SelectedValue);

            db.SubmitChanges();

            bindRooms();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editRoom').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            //add to temp table
            tmpRoom tmp = new tmpRoom();
            tmp.UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            tmp.RoomId = Convert.ToInt32(ddlAddRoom.SelectedValue);

            db.tmpRooms.InsertOnSubmit(tmp);
            db.SubmitChanges();

            bindRooms();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addRoom').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        private void bindRooms()
        {
            var q = from room in db.Rooms
                    join gcroom in db.tmpRooms
                    on room.Id equals gcroom.RoomId
                    where gcroom.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())
                    select new
                    {
                        Id = gcroom.Id,
                        Type = room.Type,
                        Room = room.Room1
                    };

            gvRoom.DataSource = q.ToList();
            gvRoom.DataBind();
        }

        private void bindDinings()
        {
            var q = from dining in db.Dinings
                    join gcdining in db.tmpRooms
                    on dining.Id equals gcdining.DiningId
                    where gcdining.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())
                    select new
                    {
                        Id = gcdining.Id,
                        Name = dining.Name            
                    };

            gvDining.DataSource = q.ToList();
            gvDining.DataBind();
        }

        private void flushTemp()
        {
            //clear content of tmpRoom
            var q = (from r in db.tmpRooms
                    where r.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())
                    select r).ToList();

            foreach(var room in q)
            {
                db.tmpRooms.DeleteOnSubmit(room);
            }
            db.SubmitChanges();
        }

        protected void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            var q = (from r in db.tmpRooms
                     where r.Id == Convert.ToInt32(hfDeleteRoomId.Value)
                     select r).FirstOrDefault();

            db.tmpRooms.DeleteOnSubmit(q);
            db.SubmitChanges();

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
            tmpRoom tmp = new tmpRoom();
            tmp.UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            tmp.DiningId = Convert.ToInt32(ddlAddDining.SelectedValue);

            db.tmpRooms.InsertOnSubmit(tmp);
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
            var d = (from dining in db.tmpRooms
                     where dining.Id == Convert.ToInt32(lblEditDiningId.Text)
                     select dining).FirstOrDefault();

            d.DiningId = Convert.ToInt32(ddlEditDining.SelectedValue);
            db.SubmitChanges();

            bindDinings();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editDining').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnDeleteDining_Click(object sender, EventArgs e)
        {
            var q = (from r in db.tmpRooms
                     where r.Id == Convert.ToInt32(hfDeleteDiningId.Value)
                     select r).FirstOrDefault();

            db.tmpRooms.DeleteOnSubmit(q);
            db.SubmitChanges();

            bindDinings();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteDining').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }
    }
}