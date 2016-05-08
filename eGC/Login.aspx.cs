using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Session.RemoveAll();
                Session.Clear();
                FormsAuthentication.SignOut();
                TextBox txtusername = loginControl.FindControl("UserName") as TextBox;
                txtusername.Focus();
            }
        }
    }
}