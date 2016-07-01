using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;


namespace eGC.DAL
{
    public class DBLogger
    {
        public static void Log(string action, string desc, string entityId)
        {
            using(var db = new GiftCheckDataContext())
            {
                AuditTrail log = new AuditTrail();
                log.User = HttpContext.Current.User.Identity.Name;
                log.Action = action;
                log.Description = desc;
                log.AssociatedId = entityId;
                log.ActionDate = DateTime.Now;
                
                db.AuditTrails.InsertOnSubmit(log);

                //commit
                db.SubmitChanges();
            }
        }
    }
}