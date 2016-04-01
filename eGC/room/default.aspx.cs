using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.room
{
    public partial class _default : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindGridview();
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
                txtEditRegular.Text = q.Regular.ToString();
                txtEditPeak.Text = q.Peak.ToString();

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

        protected void gvRoom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRoom.PageIndex = e.NewPageIndex;
            bindGridview();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Room r = new Room();
            r.Type = txtAddType.Text;
            r.Room1 = txtAddRoom.Text;
            r.Regular = Convert.ToDecimal(txtAddRegular.Text);
            r.Peak = Convert.ToDecimal(txtAddPeak.Text);

            db.Rooms.InsertOnSubmit(r);

            db.SubmitChanges();

            bindGridview();

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
            q.Regular = Convert.ToDecimal(txtEditRegular.Text);
            q.Peak = Convert.ToDecimal(txtEditPeak.Text);

            db.SubmitChanges();

            bindGridview();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);   
        }

        protected void bindGridview()
        {
            var q = from r in db.Rooms
                    select r;
            gvRoom.DataSource = q.ToList();
            gvRoom.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from r in db.Rooms
                     select r).FirstOrDefault();

            db.Rooms.DeleteOnSubmit(q);
            db.SubmitChanges();

            bindGridview();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false); 
        }
    }
}