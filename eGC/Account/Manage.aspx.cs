using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI.WebControls;

using Microsoft.AspNet.Membership.OpenAuth;
using System.Web.Security;

namespace eGC.Account
{
    public partial class Manage : System.Web.UI.Page
    {

        protected void Page_Load()
        {
            if (!IsPostBack)
            {


            }
        }

        protected void ChangePassword1_ChangedPassword(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}