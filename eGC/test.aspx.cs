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
                //UserAccountsDataContext dbUser = new UserAccountsDataContext(@"C:\dbUserAccounts.mdf");
                GiftCheckDataContext dbGC = new GiftCheckDataContext(@"C:\dbGC.mdf");

                //if (dbUser.DatabaseExists())
                //{
                //    dbUser.DeleteDatabase();
                //}

                if(dbGC.DatabaseExists())
                {
                    dbGC.DeleteDatabase();
                }

                //dbUser.CreateDatabase();
                dbGC.CreateDatabase();

                if(!Roles.RoleExists("Admin-GC"))
                {
                    Roles.CreateRole("Admin-GC");
                }

                if (!Roles.RoleExists("can-create-gc"))
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

                //user
                if(Membership.GetUser("admin-GC") == null)
                {
                    Membership.CreateUser("admin-GC", "pa$$word");
                }

                if(!Roles.IsUserInRole("admin-GC", "Admin-GC"))
                {
                    Roles.AddUserToRole("admin-GC", "Admin-GC");
                }

                //create site status
                var status = (from s in dbGC.StatusSites
                              where s.Id == 1
                              select s).ToList();

                if(status.Count < 1)
                {
                    StatusSite ss = new StatusSite();
                    ss.Status = false;
                    dbGC.StatusSites.InsertOnSubmit(ss);
                    dbGC.SubmitChanges();
                }
            }
        }
    }
}