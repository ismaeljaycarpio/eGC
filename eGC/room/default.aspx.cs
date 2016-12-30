﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eGC.DAL;

namespace eGC.room
{
    public partial class _default : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(!User.IsInRole("Admin-GC"))
                {
                    gvRoom.Columns[4].Visible = false;
                    gvRoom.Columns[5].Visible = false;
                    btnAddModal.Visible = false;
                }
            }
        }

        protected void gvRoom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load room
                var q = (from r in db.Rooms
                         where r.Id.Equals((int)gvRoom.DataKeys[index].Value)
                         select r).FirstOrDefault();

                lblRowId.Text = q.Id.ToString();
                txtEditType.Text = q.Type;
                txtEditRoom.Text = q.Room1;
                txtEditLimit.Text = q.Limit.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvRoom.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Room r = new Room();
            r.Type = txtAddType.Text;
            r.Room1 = txtAddRoom.Text;
            r.Limit = Convert.ToInt32(txtAddLimit.Text);

            db.Rooms.InsertOnSubmit(r);

            db.SubmitChanges();
            this.gvRoom.DataBind();

            //audit trail
            DBLogger.Log("Create", "Create Room ", r.Room1);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var q = (from r in db.Rooms
                     where r.Id.Equals(Convert.ToInt32(lblRowId.Text))
                     select r).FirstOrDefault();

            q.Type = txtEditType.Text;
            q.Room1 = txtEditRoom.Text;
            q.Limit = Convert.ToInt32(txtEditLimit.Text);

            db.SubmitChanges();
            this.gvRoom.DataBind();

            //audit trail
            DBLogger.Log("Update", "Updated Room ", q.Room1);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);   
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from r in db.Rooms
                     where r.Id == Convert.ToInt32(hfDeleteId.Value)
                     select r).FirstOrDefault();

            db.Rooms.DeleteOnSubmit(q);
            db.SubmitChanges();

            this.gvRoom.DataBind();

            //audit trail
            DBLogger.Log("Delete", "Deleted Room ", q.Room1);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false); 
        }

        protected void RoomDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            var q = from r in db.Rooms
                    select new
                    {
                        Id = r.Id,
                        Type = r.Type,
                        Room1 = r.Room1,
                        Limit = r.Limit
                    };

            e.Result = q.ToList();
        }
    }
}