using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.room
{
    public partial class dining_type : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!User.IsInRole("can-create-gc") && !User.IsInRole("Admin-GC"))
                {
                    gvDiningType.Columns[3].Visible = false;
                    gvDiningType.Columns[4].Visible = false;
                }
            }
        }

        protected void gvDiningType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load room
                var q = (from r in db.DiningTypes
                         where r.Id.Equals((int)gvDiningType.DataKeys[index].Value)
                         select r).FirstOrDefault();

                lblRowId.Text = q.Id.ToString();
                txtEditDining.Text = q.DiningType1;
                rblAddActive.SelectedValue = q.Active.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvDiningType.Rows[index].FindControl("lblRowId")).Text;
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
            DiningType dt = new DiningType();
            dt.DiningType1 = txtAddDining.Text;
            dt.Active = true;

            db.DiningTypes.InsertOnSubmit(dt);
            db.SubmitChanges();

            this.gvDiningType.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var q = (from r in db.DiningTypes
                     where r.Id.Equals(Convert.ToInt32(lblRowId.Text))
                     select r).FirstOrDefault();

            q.DiningType1 = txtEditDining.Text;
            q.Active = Convert.ToBoolean(rblAddActive.SelectedValue);

            db.SubmitChanges();

            this.gvDiningType.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false); 
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from r in db.DiningTypes
                     select r).FirstOrDefault();

            db.DiningTypes.DeleteOnSubmit(q);
            db.SubmitChanges();

            this.gvDiningType.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false); 
        }

        protected void DiningTypeDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            var q = from r in db.DiningTypes
                    select r;

            e.Result = q.ToList();
        }
    }
}