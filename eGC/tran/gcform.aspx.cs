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
                        txtContactPerson.Text = guest.EmergencyContactPerson;

                        txtCompany.Text = (from c in db.Guests
                                           where guest.CompanyId == c.Id
                                           select c).FirstOrDefault().CompanyName;

                        txtEmail.Text = guest.Email;
                        txtContactNo.Text = guest.ContactNumber;

                        //load gcnumber
                        int maxId = db.GCTransactions.DefaultIfEmpty().Max(r => r == null ? 0 : r.Id);
                        maxId += 1;
                        hfGCNumber.Value = maxId.ToString();
                        //txtGCNumber.Text = "2600-" + DateTime.Now.Year.ToString() + "-" + maxId.ToString();

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


                        //lazy load dining type whos active only
                        var dining_type = (from d_t in db.DiningTypes
                                           where d_t.Active == true
                                           select d_t).ToList();

                        if(dining_type.Count > 0)
                        {
                            ddlAddDiningType.DataSource = dining_type;
                            ddlAddDiningType.DataTextField = "DiningType1";
                            ddlAddDiningType.DataValueField = "Id";
                            ddlAddDiningType.DataBind();

                            ddlEditDiningType.DataSource = dining_type;
                            ddlEditDiningType.DataTextField = "DiningType1";
                            ddlEditDiningType.DataValueField = "Id";
                            ddlEditDiningType.DataBind();
                        }

                        //chk if company
                        if(guest.IsCompany == true)
                        {
                            panelName.Visible = false;
                            lblForGuestId.InnerText = "ID";
                        }

                        txtDateIssued.Text = DateTime.Now.ToString("MM/dd/yyyy");
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
                txtEditRoomGCNumber.Text = q.GCNumber;
                lblEditRoomGCNumber_old.Text = q.GCNumber; //put old value here
                ddlEditRoom.SelectedValue = q.RoomId.ToString();
                rblEditRoomBreakfast.SelectedValue = q.WithBreakfast.ToString();
                txtEditRoomHeadCount.Text = q.HeadCount.ToString();
                lblEditRoomDuplicateGC.Text = String.Empty;

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
                txtEditDiningGCNumber.Text = q.GCNumber;
                lblEditDiningGCNumber_old.Text = q.GCNumber; //put old value here
                ddlEditDining.SelectedValue = q.DiningId.ToString();
                ddlEditDiningType.SelectedValue = q.DiningTypeId.ToString();
                txtEditDiningHeadCount.Text = q.HeadCount.ToString();
                lblEditDiningDuplicateGC.Text = String.Empty;

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
            var gcs = (from gctran in db.GCTransactions
                       where gctran.GCNumber == hfGCNumber.Value
                       select gctran).ToList();

            if(gcs.Count > 0)
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
                GCTransaction tran = new GCTransaction();
                tran.GuestId = Convert.ToInt32(Request.QueryString["guestid"]);
                //tran.GCNumber = txtGCNumber.Text;
                tran.RecommendingApproval = txtRecommendingApproval.Text;
                //tran.AccountNo = txtAccountNo.Text;
                //tran.Remarks = txtRemarks.Text;
                tran.GCType = ddlGCType.SelectedValue;
                tran.ApprovalStatus = "Pending";
                tran.StatusGC = "Waiting";

                if (txtExpirationDate.Text != String.Empty)
                {
                    tran.ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text);
                }
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
                    //room.WithBreakfast = r.WithBreakfast;
                    //room.HowManyPerson = r.HowManyPerson;
                    //room.DiningType = r.DiningType;
                    //room.HowManyDiningPerson = r.HowManyDiningPerson;

                    db.GCRooms.InsertOnSubmit(room);
                }

                db.SubmitChanges();
                Response.Redirect("~/guest/default.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/guest/default.aspx");
        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            //chk for duplicate gc number
            string gcNumber = txtAddRoomGCNumber.Text;

            if (hasDuplicate(gcNumber) == false)
            {
                //add to temp table
                tmpRoom tmp = new tmpRoom();
                tmp.UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                tmp.GCNumber = gcNumber;
                tmp.RoomId = Convert.ToInt32(ddlAddRoom.SelectedValue);
                tmp.WithBreakfast = Convert.ToBoolean(rblAddRoomBreakfast.SelectedValue);
                tmp.HeadCount = Convert.ToInt32(txtAddRoomHeadCount.Text);

                db.tmpRooms.InsertOnSubmit(tmp);
                db.SubmitChanges();

                lblAddRoomDuplicateGC.Text = String.Empty;

                bindRooms();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#addRoom').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
            else
            {
                lblAddRoomDuplicateGC.Text = "Duplicate GC Number.";
            }  
        }

        protected void btnUpdateRoom_Click(object sender, EventArgs e)
        {
            //chk for duplicate gc number
            string gcNumber = txtEditRoomGCNumber.Text;
            string old_gcNumber = lblEditRoomGCNumber_old.Text;

            var r = (from room in db.tmpRooms
                     where room.Id == Convert.ToInt32(lblEditRoomId.Text)
                     select room).FirstOrDefault();

            //chk if user edited gc number
            if(gcNumber != old_gcNumber)
            {
                if(hasDuplicate(gcNumber, old_gcNumber) == false)
                {
                    r.GCNumber = gcNumber;
                    r.RoomId = Convert.ToInt32(ddlEditRoom.SelectedValue);
                    r.WithBreakfast = Convert.ToBoolean(rblEditRoomBreakfast.SelectedValue);
                    r.HeadCount = Convert.ToInt32(txtEditRoomHeadCount.Text);

                    db.SubmitChanges();

                    lblEditRoomDuplicateGC.Text = String.Empty;

                    bindRooms();

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#editRoom').modal('hide');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
                }
                else
                {
                    lblEditRoomDuplicateGC.Text = "Duplicate GC Number.";
                }
            }
            else
            {
                r.GCNumber = gcNumber;
                r.RoomId = Convert.ToInt32(ddlEditRoom.SelectedValue);
                r.WithBreakfast = Convert.ToBoolean(rblEditRoomBreakfast.SelectedValue);
                r.HeadCount = Convert.ToInt32(txtEditRoomHeadCount.Text);

                db.SubmitChanges();

                lblEditRoomDuplicateGC.Text = String.Empty;

                bindRooms();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editRoom').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
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
            //chk for duplicate gc number
            string gcNumber = txtAddDiningGCNumber.Text;

            if(hasDuplicate(gcNumber) == false)
            {
                //add to temp table
                tmpRoom tmp = new tmpRoom();
                tmp.UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                tmp.DiningId = Convert.ToInt32(ddlAddDining.SelectedValue);
                tmp.DiningTypeId = Convert.ToInt32(ddlAddDiningType.SelectedValue);
                tmp.HeadCount = Convert.ToInt32(txtAddDiningHeadCount.Text);
                tmp.GCNumber = gcNumber;

                db.tmpRooms.InsertOnSubmit(tmp);
                db.SubmitChanges();

                lblAddDiningDuplicateGC.Text = String.Empty;

                bindDinings();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#addDining').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
            else
            {
                lblAddDiningDuplicateGC.Text = "Duplicate GC Number.";
            }            
        }

        protected void btnEditDining_Click(object sender, EventArgs e)
        {
            //chk for duplicate gc number
            string gcNumber = txtEditDiningGCNumber.Text;
            string old_gcNumber = lblEditDiningGCNumber_old.Text;

            var d = (from dining in db.tmpRooms
                     where dining.Id == Convert.ToInt32(lblEditDiningId.Text)
                     select dining).FirstOrDefault();

            if(gcNumber != old_gcNumber)
            {
                if(hasDuplicate(gcNumber, old_gcNumber) == false)
                {
                    d.GCNumber = gcNumber;
                    d.DiningId = Convert.ToInt32(ddlEditDining.SelectedValue);
                    d.DiningTypeId = Convert.ToInt32(ddlEditDiningType.SelectedValue);
                    d.HeadCount = Convert.ToInt32(txtEditDiningHeadCount.Text);

                    db.SubmitChanges();

                    bindDinings();

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#editDining').modal('hide');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
                }
                else
                {
                    lblEditDiningDuplicateGC.Text = "Duplicate GC Number.";
                }
            }
            else
            {
                d.GCNumber = gcNumber;
                d.DiningId = Convert.ToInt32(ddlEditDining.SelectedValue);
                d.DiningTypeId = Convert.ToInt32(ddlEditDiningType.SelectedValue);
                d.HeadCount = Convert.ToInt32(txtEditDiningHeadCount.Text);

                db.SubmitChanges();

                bindDinings();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editDining').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
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

        private void bindRooms()
        {
            var q = from room in db.Rooms
                    join gcroom in db.tmpRooms
                    on room.Id equals gcroom.RoomId
                    where gcroom.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())
                    select new
                    {
                        Id = gcroom.Id,
                        GCNumber = gcroom.GCNumber,
                        Type = room.Type,
                        Room = room.Room1,
                        WithBreakfast = gcroom.WithBreakfast,
                        HowManyPerson = gcroom.HeadCount
                    };

            gvRoom.DataSource = q.ToList();
            gvRoom.DataBind();
        }

        private void bindDinings()
        {
            var q = from dining in db.Dinings
                    join gcdining in db.tmpRooms
                    on dining.Id equals gcdining.DiningId
                    join dining_type in db.DiningTypes
                    on gcdining.DiningTypeId equals dining_type.Id
                    where gcdining.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())
                    select new
                    {
                        Id = gcdining.Id,
                        GCNumber = gcdining.GCNumber,
                        Name = dining.Name,
                        DiningType = dining_type.DiningType1,
                        HeadCount = gcdining.HeadCount
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

        protected void ddlGCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlGCType.SelectedValue == "Representation")
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

        protected void btnOpenRoomModal_Click(object sender, EventArgs e)
        {
            int maxId = db.tmpRooms.DefaultIfEmpty().Max(r => r == null ? 0 : r.Id);
            maxId += 1;

            txtAddRoomGCNumber.Text = "2600-" + DateTime.Now.Year.ToString() + "-" + maxId.ToString();

            lblAddRoomDuplicateGC.Text = String.Empty;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addRoom').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void btnOpenDiningModal_Click(object sender, EventArgs e)
        {
            int maxId = db.tmpRooms.DefaultIfEmpty().Max(r => r == null ? 0 : r.Id);
            maxId += 1;

            txtAddDiningGCNumber.Text = "2600-" + DateTime.Now.Year.ToString() + "-" + maxId.ToString();

            lblAddDiningDuplicateGC.Text = String.Empty;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addDining').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected bool hasDuplicate(string gcNumber)
        {
            var q = (from tmp in db.tmpRooms
                     where tmp.GCNumber == gcNumber
                     select tmp).ToList();

            if(q.Count > 0)
            {
                return true;
            }

            var qq = (from t in db.GCTransactions
                      where t.GCNumber == gcNumber
                      select t).ToList();

            if(qq.Count > 0)
            {
                return true;
            }

            return false;
        }

        protected bool hasDuplicate(string gcNumber, string old_gcNumber)
        {
            var q = (from tmp in db.tmpRooms
                     where 
                     (tmp.GCNumber == gcNumber) && (tmp.GCNumber != old_gcNumber)
                     select tmp).ToList();

            if (q.Count > 0)
            {
                return true;
            }

            var qq = (from t in db.GCTransactions
                      where 
                      (t.GCNumber == gcNumber) && (t.GCNumber != old_gcNumber)
                      select t).ToList();

            if (qq.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}