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
        UserAccountsDataContext dbUser = new UserAccountsDataContext();
        DAL.AccountManagement accnt = new DAL.AccountManagement();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                this.gvUsers.DataBind();

                checkRoles();

                //load roles
                var roles = (from r in dbUser.Roles
                             where
                             r.RoleName == "can-create-gc" || 
                             r.RoleName == "can-approve-gc" || 
                             r.RoleName == "frontoffice" ||
                             r.RoleName == "Admin-GC"
                             select r).ToList();

                ddlRoles.DataSource = roles;
                ddlRoles.DataTextField = "RoleName";
                ddlRoles.DataValueField = "RoleId";
                ddlRoles.DataBind();

                ddlRoles.Items.Insert(0, new ListItem("-- Select a Role --", "0"));


                ddlCreateRoles.DataSource = roles;
                ddlCreateRoles.DataTextField = "RoleName";
                ddlCreateRoles.DataValueField = "RoleId";
                ddlCreateRoles.DataBind();

                ddlCreateRoles.Items.Insert(0, new ListItem("-- Select a Role --", "0"));

                txtSearch.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvUsers.DataBind();
            txtSearch.Focus();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lblStatus");
                LinkButton lnkReset = (LinkButton)e.Row.FindControl("lblReset");
                LinkButton lbtnLockedOut = (LinkButton)e.Row.FindControl("lbtnLockedOut");
                Button btnDelete = (Button)e.Row.FindControl("btnDelete");
                string userName = ((LinkButton)e.Row.FindControl("lblUsername") as LinkButton).Text;

                if (lnkStatus.Text == "Active")
                {
                    lnkStatus.Attributes.Add("onclick", "return confirm('Do you want to deactivate this user ? ');");
                }
                else
                {
                    lnkStatus.Attributes.Add("onclick", "return confirm('Do you want to activate this user ? ');");
                }

                lnkReset.Attributes.Add("onclick", "return confirm('Do you want to reset the password of this user ? ');");

                if (lbtnLockedOut.Text == "Yes")
                {
                    lbtnLockedOut.Attributes.Add("onclick", "return confirm('Do you want to Unlock this user ? ');");
                }
                else
                {
                    lbtnLockedOut.Attributes.Add("onclick", "return confirm('Do you want to Lock this user ? ');");
                }

                //cant delete your own account
                if(userName == Page.User.Identity.Name)
                {
                    btnDelete.Visible = false;
                }
            }
            else if(e.Row.RowType == DataControlRowType.Footer)
            {
                int _TotalRecs = rowCount();
                int _CurrentRecStart = gvUsers.PageIndex * gvUsers.PageSize + 1;
                int _CurrentRecEnd = gvUsers.PageIndex * gvUsers.PageSize + gvUsers.Rows.Count;

                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = string.Format("Displaying <b style=color:red>{0}</b> to <b style=color:red>{1}</b> of {2} records found", _CurrentRecStart, _CurrentRecEnd, _TotalRecs);
            }
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("editRole"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load user info
                lblUserId.Text = gvUsers.DataKeys[index].Value.ToString();
                lblUserName.Text = (gvUsers.Rows[index].FindControl("lblUsername") as LinkButton).Text;

                
                //set selected role
                var ro = (from m in dbUser.MembershipLINQs
                          join u in dbUser.Users
                          on m.UserId equals u.UserId
                          join up in dbUser.UserProfiles
                          on u.UserId equals up.UserId
                          join p in dbUser.Positions
                          on up.PositionId equals p.Id
                          join uir in dbUser.UsersInRoles
                          on up.UserId equals uir.UserId
                          join r in dbUser.Roles
                          on uir.RoleId equals r.RoleId
                          where
                          u.UserName == lblUserName.Text
                          select new
                          {
                              FirstName = up.FirstName,
                              MiddleName = up.MiddleName,
                              LastName = up.LastName,
                              RoleId = r.RoleId
                          }).ToList();
                
                if(ro.Count > 0)
                {
                    var user = ro.FirstOrDefault();

                    txtEditFirstName.Text = user.FirstName;
                    txtEditMiddleName.Text = user.MiddleName;
                    txtEditLastName.Text = user.LastName;
                    ddlRoles.SelectedValue = user.RoleId.ToString();

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
            else if(e.CommandName.Equals("deleteUser"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblDeleteUserId.Text = gvUsers.DataKeys[index].Value.ToString();
                lblDeleteUsername.Text = (gvUsers.Rows[index].FindControl("lblUsername") as LinkButton).Text;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteUser').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        private int rowCount()
        {
            string strSearch = txtSearch.Text;

            var q = (from m in dbUser.MembershipLINQs
                     join u in dbUser.Users
                     on m.UserId equals u.UserId
                     join up in dbUser.UserProfiles
                     on u.UserId equals up.UserId
                     join p in dbUser.Positions
                     on up.PositionId equals p.Id
                     join uir in dbUser.UsersInRoles
                     on up.UserId equals uir.UserId
                     join r in dbUser.Roles
                     on uir.RoleId equals r.RoleId
                     select new
                     {
                         UserId = m.UserId,
                         Username = u.UserName,
                         FullName = up.LastName + " , " + up.FirstName + " " + up.MiddleName,
                         RoleName = r.RoleName,
                         IsApproved = m.IsApproved,
                         IsLockedOut = m.IsLockedOut
                     }).ToList();

            return q.Count;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var user = (from us in dbUser.UserProfiles
                        where us.UserId == Guid.Parse(lblUserId.Text)
                        select us).FirstOrDefault();

            user.FirstName = txtEditFirstName.Text;
            user.MiddleName = txtEditMiddleName.Text;
            user.LastName = txtEditLastName.Text;

            //save to db
            dbUser.SubmitChanges();

            //update roles
            Roles.RemoveUserFromRoles(lblUserName.Text, Roles.GetRolesForUser(lblUserName.Text));

            Roles.AddUserToRole(lblUserName.Text, ddlRoles.SelectedItem.Text);

            //re-load gridview
            this.gvUsers.DataBind();

            //close modal
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editRole').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }

        protected void lbtnLockedOut_Click(object sender, EventArgs e)
        {
            LinkButton lbtnLockedOut_Click = sender as LinkButton;
            GridViewRow gvrow = lbtnLockedOut_Click.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            MembershipUser getUser = Membership.GetUser(UserId);

            if (lbtnLockedOut_Click.Text == "Yes")
            {
                //unlock
                getUser.UnlockUser();
            }
            else
            {
                //lock
                accnt.LockUser(UserId);
            }

            this.gvUsers.DataBind();
        }

        protected void lblReset_Click(object sender, EventArgs e)
        {
            LinkButton lnkReset = sender as LinkButton;
            GridViewRow gvrow = lnkReset.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            //pswd resets to own username
            accnt.ResetPassword(UserId);
            
            this.gvUsers.DataBind();
        }

        protected void lblStatus_Click(object sender, EventArgs e)
        {
            LinkButton lnkStatus = sender as LinkButton;
            GridViewRow gvrow = lnkStatus.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            if (lnkStatus.Text == "Active")
            {
                accnt.DeactivateUser(UserId);
            }
            else
            {
                accnt.ActivateUser(UserId);
            }

            //bindGridview();
            this.gvUsers.DataBind();
        }

        protected void UserDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;

            var q = (from m in dbUser.MembershipLINQs
                     join u in dbUser.Users
                     on m.UserId equals u.UserId
                     join up in dbUser.UserProfiles
                     on u.UserId equals up.UserId
                     join p in dbUser.Positions
                     on up.PositionId equals p.Id
                     join uir in dbUser.UsersInRoles
                     on up.UserId equals uir.UserId
                     join r in dbUser.Roles
                     on uir.RoleId equals r.RoleId
                     where
                     (r.RoleName == "can-create-gc" ||
                             r.RoleName == "can-approve-gc" ||
                             r.RoleName == "frontoffice" ||
                             r.RoleName == "Admin-GC") &&
                     (up.FirstName.Contains(strSearch) ||
                        up.MiddleName.Contains(strSearch) || 
                        up.LastName.Contains(strSearch) ||
                        u.UserName.Contains(strSearch))
                     select new
                     {
                         UserId = m.UserId,
                         Username = u.UserName,
                         FullName = up.LastName + " , " + up.FirstName + " " + up.MiddleName,
                         RoleName = r.RoleName,
                         IsApproved = m.IsApproved,
                         IsLockedOut = m.IsLockedOut
                     }).ToList();

            q = q.OrderByDescending(o => o.RoleName).ToList();
            e.Result = q;
        }

        protected void openCreateAccount_Click(object sender, EventArgs e)
        {
            txtCreateUsername.Focus();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#createUser').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            //chk if username already exist
            if(Membership.GetUser(txtCreateUsername.Text) != null)
            {
                lblErrorMsg.Text = "Username already exist.";
            }
            else
            {
                //register user
                Membership.CreateUser(txtCreateUsername.Text, 
                    txtCreateUsername.Text.Trim());
                
                //add to user profile
                UserProfile user = new UserProfile();
                user.UserId = Guid.Parse(Membership.GetUser(txtCreateUsername.Text).ProviderUserKey.ToString());
                user.FirstName = txtCreateFirstName.Text;
                user.MiddleName = txtCreateMiddleName.Text;
                user.LastName = txtCreateLastName.Text;
                dbUser.UserProfiles.InsertOnSubmit(user);
                dbUser.SubmitChanges();

                //assign role
                Roles.AddUserToRole(txtCreateUsername.Text, ddlCreateRoles.SelectedItem.Text);

                //re-load users
                this.gvUsers.DataBind();

                //hide modal
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#createUser').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void checkRoles()
        {
            if(!Roles.RoleExists("can-create-gc"))
            {
                Roles.CreateRole("can-create-gc");
            }

            if (!Roles.RoleExists("can-approve-gc"))
            {
                Roles.CreateRole("can-approve-gc");
            }

            if (!Roles.RoleExists("frontoffice"))
            {
                Roles.CreateRole("frontoffice");
            }

            if (!Roles.RoleExists("Admin-GC"))
            {
                Roles.CreateRole("Admin-GC");
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            Membership.DeleteUser(lblDeleteUsername.Text);

            this.gvUsers.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteUser').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }
    }
}