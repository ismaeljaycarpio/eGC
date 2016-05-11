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
                //chk for roles
                if(!Roles.RoleExists("Admin"))
                {
                    Roles.CreateRole("Admin");
                }

                if(!Roles.RoleExists("CanApprove"))
                {
                    Roles.CreateRole("CanApprove");
                }

                if(!Roles.RoleExists("CanCreateGC"))
                {
                    Roles.RoleExists("CanCreateGC");
                }

                if (!Roles.RoleExists("CanFO"))
                {
                    Roles.CreateRole("CanFO");
                }


                //create user
                if(Membership.GetUser("admin") == null)
                {
                    Membership.CreateUser("admin", "pa$$word");
                }
                
                //assign user to role
                if(!Roles.IsUserInRole("admin", "Admin"))
                {
                    Roles.AddUserToRole("admin", "Admin");
                }

                Console.Write("success!");
            }
        }

    }
}