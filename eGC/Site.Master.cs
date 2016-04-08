using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC
{
    public partial class SiteMaster : MasterPage
    {
        //EHRISDataContextDataContext db = new EHRISDataContextDataContext();

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!Page.IsPostBack)
            //{
            //    if (Page.User.Identity.IsAuthenticated)
            //    {
            //        Control financeOnly;
            //        Control foOnly;

            //        if (((financeOnly = LoginView1.FindControl("FinanceOnly")) != null) &&
            //            ((foOnly = LoginView1.FindControl("FrontOfficeOnly")) != null))
            //        {
            //            //chk if user is in finance
            //            var user = (from emp in db.EMPLOYEEs
            //                        join p in db.POSITIONs
            //                        on emp.PositionId equals p.Id
            //                        join d in db.DEPARTMENTs
            //                        on p.DepartmentId equals d.Id
            //                        where (emp.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())) &&
            //                        (d.Department1.Contains("Finance"))
            //                        select new
            //                        {
            //                            Id = emp.UserId
            //                        }).ToList();

            //            if (user.Count < 1)
            //            {
            //                financeOnly.Visible = false;
            //            }

            //            //chk if user is in fo
            //            var us = (from emp in db.EMPLOYEEs
            //                      join p in db.POSITIONs
            //                      on emp.PositionId equals p.Id
            //                      join d in db.DEPARTMENTs
            //                      on p.DepartmentId equals d.Id
            //                      where (emp.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())) &&
            //                      (d.Department1.Contains("Front Office"))
            //                      select new
            //                      {
            //                          Id = emp.UserId
            //                      }).ToList();

            //            if (us.Count < 1)
            //            {
            //                foOnly.Visible = false;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        FormsAuthentication.SignOut();
            //        Session.RemoveAll();
            //    }
            //}
        }
    }
}