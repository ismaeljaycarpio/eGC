using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                              where g.GuestId.Equals(guestId)
                              select g).ToList();

                    if (gu.Count < 1)
                    {
                        Response.Redirect("~/guest/default.aspx");
                    }
                    else
                    {
                        //load guest
                        TabName.Value = Request.Form[TabName.UniqueID];

                        var guest = gu.FirstOrDefault();
                        txtName.Text = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName;
                        txtGuestId.Text = guest.GuestId;
                        txtCompany.Text = guest.CompanyName;
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

                            //show rates
                            string roomId = ddlAddRoom.SelectedValue;
                            var roomSelected = (from r in db.Rooms
                                                where r.Id.Equals(roomId)
                                                select r).FirstOrDefault();
                            if (roomSelected != null)
                            {
                                lblAddRoomRegularRate.Text = roomSelected.Regular;
                                lblAddRoomPeakRate.Text = roomSelected.Peak;
                            }
                        }
                    }
                }
            }
        }

        protected void gvRoom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvRoom_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvDining_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvDining_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateRoom_Click(object sender, EventArgs e)
        {

        }

        protected void ddlAddRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show rates
            string roomId = ddlAddRoom.SelectedValue;
            var roomSelected = (from r in db.Rooms
                                where r.Id.Equals(roomId)
                                select r).FirstOrDefault();
            if (roomSelected != null)
            {
                lblAddRoomRegularRate.Text = roomSelected.Regular;
                lblAddRoomPeakRate.Text = roomSelected.Peak;
            }
        }
    }
}