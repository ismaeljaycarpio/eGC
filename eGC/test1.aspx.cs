using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC
{
    public partial class test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            GiftCheckDataContext db = new GiftCheckDataContext();

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

            db.SubmitChanges();
        }
    }
}