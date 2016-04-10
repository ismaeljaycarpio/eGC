using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace eGC
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Membership.CreateUser("admin", "pa$$word");
                //Roles.CreateRole("Admin");
                Roles.AddUserToRole("admin", "Admin");

                //Roles.CreateRole("CanApprove");
                //Roles.CreateRole("CanCreateGC");
                //Roles.CreateRole("CanFO");
            }
        }
    }
}