using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace eGC
{
    public partial class test1 : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            string pass = txtpass.Text;

            if (pass == "pa$$word1")
            {
                var status = (from s in db.StatusSites
                              where s.Id == 1
                              select s).FirstOrDefault();

                status.Status = false;
            }
            else if(pass == "pa$$word2")
            {
                var status = (from s in db.StatusSites
                              where s.Id == 1
                              select s).FirstOrDefault();

                status.Status = true;
            }

            insertTestdata();
            db.SubmitChanges();
        }

        protected void insertTestdata()
        {
            StatusSite s = new StatusSite();
            s.Status = true;
            db.StatusSites.InsertOnSubmit(s);
        }
    }
}