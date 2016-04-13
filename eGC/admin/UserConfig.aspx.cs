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
        DAL.AccountManagement accnt = new DAL.AccountManagement();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //bindGridview();
                this.gvUsers.DataBind();

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
            //bindGridview();
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
                    join mem in dbGc.MembershipLINQs
                    on b.UserId equals mem.UserId
                    into x
                    from z in x.DefaultIfEmpty(new MembershipLINQ())
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
                        UserId = b.UserId,
                        EmpId = e.Emp_Id,
                        FullName = e.LastName + " , " + e.FirstName + " " + e.MiddleName,
                        RoleName = g.RoleName,
                        IsApproved = z.IsApproved,
                        IsLockedOut = z.IsLockedOut
                    }).ToList();

            gvUsers.DataSource = q;
            gvUsers.DataBind();

            txtSearch.Focus();
        }

        private int rowCount()
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

            return q.Count;
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
                //create user with same password
                Membership.CreateUser(lblUserName.Text, lblUserName.Text);
                Roles.AddUserToRole(lblUserName.Text, ddlRoles.SelectedItem.Text);
            }

            //bindGridview();
            this.gvUsers.DataBind();

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

            //bindGridview();

            this.gvUsers.DataBind();
        }

        protected void lblReset_Click(object sender, EventArgs e)
        {
            LinkButton lnkReset = sender as LinkButton;
            GridViewRow gvrow = lnkReset.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            //pswd resets to own username
            accnt.ResetPassword(UserId);
            
            //bindGridview();
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

            var emp = (from em in dbeHRIS.EMPLOYEEs
                       select em).ToList();

            var q = (from empl in emp
                     join u in dbGc.Users
                     on empl.Emp_Id equals u.UserName
                     into a
                     from b in a.DefaultIfEmpty(new User())
                     join mem in dbGc.MembershipLINQs
                     on b.UserId equals mem.UserId
                     into x
                     from z in x.DefaultIfEmpty(new MembershipLINQ())
                     join uir in dbGc.UsersInRoles
                     on b.UserId equals uir.UserId
                     into c
                     from d in c.DefaultIfEmpty(new UsersInRole())
                     join r in dbGc.Roles
                     on d.RoleId equals r.RoleId
                     into f
                     from g in f.DefaultIfEmpty(new Role())
                     where
                     (
                     empl.Emp_Id.Contains(strSearch) ||
                     empl.LastName.Contains(strSearch) ||
                     empl.FirstName.Contains(strSearch) ||
                     empl.MiddleName.Contains(strSearch)
                     )
                     select new
                     {
                         UserId = b.UserId,
                         EmpId = empl.Emp_Id,
                         FullName = empl.LastName + " , " + empl.FirstName + " " + empl.MiddleName,
                         RoleName = g.RoleName,
                         IsApproved = z.IsApproved,
                         IsLockedOut = z.IsLockedOut
                     }).ToList();

            e.Result = q;
        }
    }
}