using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Security;

namespace eGC.DAL
{
    public class AccountManagement
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = String.Empty;

        public void DeactivateUser(Guid UserId)
        {
            strSql = "UPDATE Memberships SET IsApproved = 'False' WHERE UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["connMembership"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void ActivateUser(Guid UserId)
        {
            strSql = "UPDATE Memberships SET IsApproved = 'True' WHERE UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["connMembership"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void ResetPassword(Guid UserId)
        {
            MembershipUser mu = Membership.GetUser(UserId);

            mu.ChangePassword(mu.ResetPassword(), "pass123");
        }

        public void ChangeRole(Guid UserId, string roleName)
        {
            //get user
            MembershipUser _user = Membership.GetUser(UserId);

            //remove user from all his/her roles
            foreach (string role in Roles.GetRolesForUser(_user.UserName))
            {
                Roles.RemoveUserFromRole(_user.UserName, role);
            }

            //assign user to new role
            if (!Roles.IsUserInRole(_user.UserName, roleName))
            {
                Roles.AddUserToRole(_user.UserName, roleName);
            }
        }

        public void LockUser(Guid UserId)
        {
            strSql = "UPDATE Memberships SET IsLockedOut = 'True' WHERE UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["connMembership"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
    }
}