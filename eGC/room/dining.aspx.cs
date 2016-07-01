using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eGC.DAL;

namespace eGC.room
{
    public partial class dining : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!User.IsInRole("can-create-gc") && !User.IsInRole("Admin-GC"))
                {
                    gvDining.Columns[2].Visible = false;
                    gvDining.Columns[3].Visible = false;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from r in db.Dinings
                     where r.Id == Convert.ToInt32(hfDeleteId.Value)
                     select r).FirstOrDefault();

            db.Dinings.DeleteOnSubmit(q);
            db.SubmitChanges();

            this.gvDining.DataBind();

            //audit trail
            DBLogger.Log("Delete", "Deleted Dining ", q.Name);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false); 
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var q = (from r in db.Dinings
                     where r.Id.Equals(Convert.ToInt32(lblRowId.Text))
                     select r).FirstOrDefault();

            q.Name = txtEditDining.Text;

            db.SubmitChanges();

            this.gvDining.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false); 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Dining dining = new Dining();
            dining.Name = txtAddDining.Text;

            db.Dinings.InsertOnSubmit(dining);
            
            db.SubmitChanges();

            this.gvDining.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void gvDining_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load room
                var q = (from r in db.Dinings
                         where r.Id.Equals((int)gvDining.DataKeys[index].Value)
                         select r).FirstOrDefault();

                lblRowId.Text = q.Id.ToString();
                txtEditDining.Text = q.Name;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvDining.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void DiningDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            var q = from r in db.Dinings
                    select r;

            e.Result = q.ToList();
        }
    }
}