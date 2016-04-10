using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace eGC.admin
{
    public partial class UserConfig : System.Web.UI.Page
    {
        EHRISDataContextDataContext dbeHRIS = new EHRISDataContextDataContext();
        GiftCheckDataContext dbGc = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindGridview();

                //load roles
                var roles = (from r in dbGc.Roles
                             select r).ToList();

                ddlRoles.DataSource = roles;
                ddlRoles.DataTextField = "RoleName";
                ddlRoles.DataValueField = "RoleId";
                ddlRoles.DataBind();

                ddlRoles.Items.Insert(0, new ListItem("Select a Role", "0"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGridview();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            bindGridview();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("editRole"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load user info
                lblUserId.Text = gvUsers.DataKeys[index].Value.ToString();
                lblUserName.Text = (gvUsers.Rows[index].FindControl("lblEmpId") as LinkButton).Text;

                
                //set selected role
                var ro = (from u in dbGc.Users
                          join uir in dbGc.UsersInRoles
                          on u.UserId equals uir.UserId
                          join r in dbGc.Roles
                          on uir.RoleId equals r.RoleId
                          where
                          u.UserName == lblUserName.Text
                          select new{
                              RoleId = r.RoleId
                          }).ToList();
                
                if(ro.Count > 0)
                {
                    var userInRole = ro.FirstOrDefault();

                    ddlRoles.SelectedValue = userInRole.RoleId.ToString();
                }
                else
                {
                    ddlRoles.SelectedValue = "0";
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editRole').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void bindGridview()
        {
            string strSearch = txtSearch.Text;

            var emp = (from e in dbeHRIS.EMPLOYEEs
                       select e).ToList();

            var q = (from e in emp
                    join u in dbGc.Users
                    on e.Emp_Id equals u.UserName
                    into a
                    from b in a.DefaultIfEmpty(new User())
                    join uir in dbGc.UsersInRoles
                    on b.UserId equals uir.UserId
                    into c
                    from d in c.DefaultIfEmpty(new UsersInRole())
                    join r in dbGc.Roles
                    on d.RoleId equals r.RoleId
                    into f
                    from g in f.DefaultIfEmpty(new Role())
                    where
                    (e.Emp_Id.Contains(strSearch) || 
                    e.LastName.Contains(strSearch) ||
                    e.FirstName.Contains(strSearch) ||
                    e.MiddleName.Contains(strSearch))
                    select new
                    {
                        UserId = e.UserId,
                        EmpId = e.Emp_Id,
                        FullName = e.LastName + " , " + e.FirstName + " " + e.MiddleName,
                        RoleName = g.RoleName
                    }).ToList();

            gvUsers.DataSource = q;
            gvUsers.DataBind();

            txtSearch.Focus();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //chk if user is registered already
            if (Membership.GetUser(lblUserName.Text) != null)
            {
                if(chkDelete.Checked == true)
                {
                    Membership.DeleteUser(lblUserName.Text, true);
                }
                else
                {
                    //update roles
                    Roles.RemoveUserFromRoles(lblUserName.Text, Roles.GetRolesForUser(lblUserName.Text));

                    Roles.AddUserToRole(lblUserName.Text, ddlRoles.SelectedItem.Text);
                }
            }
            else
            {
                Membership.CreateUser(lblUserName.Text, lblUserName.Text);
                Roles.AddUserToRole(lblUserName.Text, ddlRoles.SelectedItem.Text);
            }

            bindGridview();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editRole').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);

        }
    }
}