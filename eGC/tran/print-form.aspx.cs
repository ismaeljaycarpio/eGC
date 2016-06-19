﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using eGC.DAL;

namespace eGC.tran
{
    public partial class print_form : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        UserAccountsDataContext dbUser = new UserAccountsDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(Request.QueryString["gcId"] == null)
                {
                    Response.Redirect("~/fo/frontoffice.aspx");
                }

                string gcId = Request.QueryString["gcId"];
                generateReport(gcId);
            }
        }

        protected void generateReport(string gcId)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/tran/gc-report.rdlc");

            //querty tran
            var tran = (from gctran in db.GCTransactions
                       join guest in db.Guests
                       on gctran.GuestId equals guest.Id
                       where gctran.GCNumber == gcId
                       select new 
                       {
                           GuestId = guest.GuestId,
                           FullName = guest.LastName + ", " + guest.FirstName + " " + guest.MiddleName,
                           CompanyName = (from gu in db.Guests where guest.CompanyId == gu.Id select gu).FirstOrDefault().CompanyName,
                           ContactNo = guest.ContactNumber,
                           Email = guest.Email,
                           GCNumber = gctran.GCNumber,
                           ExpirationDate = gctran.ExpirationDate,
                           GCType = gctran.GCType,
                           RecommendingApproval = gctran.RecommendingApproval,
                           Remarks = gctran.Reason,
                           DateCancelled = gctran.CancelledDate,
                           ReasonForCancellation = gctran.CancellationReason,
                           GCStatus = gctran.StatusGC,
                           ApprovedBy = gctran.ApprovedBy,
                           DateIssued = gctran.DateIssued,
                           RequestedBy = gctran.RequestedBy,
                           Room = gctran.Room.Room1,
                           Includes = gctran.WithBreakfast,
                           HeadCount = gctran.HeadCount,
                           Dining = gctran.Dining.Name,
                           DiningType = gctran.DiningType.DiningType1,
                           Checkin = gctran.CheckinDate,
                           Checkout = gctran.CheckoutDate
                       }).FirstOrDefault();

            string approvedBy = String.Empty;
            string expirationDate = String.Empty;
            string dateCancelled = String.Empty;
            string dateIssued = String.Empty;
            string checkin = String.Empty;
            string checkout = String.Empty;
            
            if(tran.ApprovedBy != null)
            {
                var approver = (from user in dbUser.UserProfiles
                               where tran.ApprovedBy == user.UserId
                               select new
                               {
                                   ApproverName = user.FirstName + " " + user.MiddleName + " " + user.LastName
                               }).FirstOrDefault();

                if(approver != null)
                {
                    approvedBy = approver.ApproverName;
                }       
            }

            if(tran.ExpirationDate != null)
            {
                expirationDate = tran.ExpirationDate.Value.ToShortDateString();
            }

            if(tran.DateCancelled != null)
            {
                dateCancelled = tran.DateCancelled.Value.ToShortDateString();
            }

            if(tran.DateIssued != null)
            {
                dateIssued = tran.DateIssued.Value.ToShortDateString();
            }

            if(tran.Checkin != null)
            {
                checkin = tran.Checkin.Value.ToShortDateString();
            }

            if(tran.Checkout != null)
            {
                checkout = tran.Checkout.Value.ToShortDateString();
            }

            //fill param
            ReportParameter[] param = new ReportParameter[23];

            param[0] = new ReportParameter("GuestId", tran.GuestId);
            param[1] = new ReportParameter("CompanyName", tran.CompanyName);
            param[2] = new ReportParameter("FullName", tran.FullName);
            param[3] = new ReportParameter("ContactNo", tran.ContactNo);
            param[4] = new ReportParameter("GCType", tran.GCType);
            param[5] = new ReportParameter("Remarks", tran.Remarks);
            param[6] = new ReportParameter("DateIssued", dateIssued);
            param[7] = new ReportParameter("GCNumber", tran.GCNumber);
            param[8] = new ReportParameter("Email", tran.Email);
            param[9] = new ReportParameter("RecommendingApproval", tran.RecommendingApproval);
            param[10] = new ReportParameter("ExpirationDate", expirationDate);
            param[11] = new ReportParameter("GCStatus", tran.GCStatus);
            param[12] = new ReportParameter("DateCancelled", dateCancelled);
            param[13] = new ReportParameter("ReasonForCancellation", tran.ReasonForCancellation);
            param[14] = new ReportParameter("ApprovedBy", approvedBy);
            param[15] = new ReportParameter("RequestedBy", tran.RequestedBy);
            param[16] = new ReportParameter("Room", tran.Room);
            param[17] = new ReportParameter("Includes", tran.Includes.ToString());
            param[18] = new ReportParameter("HeadCount", tran.HeadCount.ToString());
            param[19] = new ReportParameter("Dining", tran.Dining);
            param[20] = new ReportParameter("DiningType", tran.DiningType);
            param[21] = new ReportParameter("Checkin", checkin);
            param[22] = new ReportParameter("Checkout", checkout);

            //put param to report
            ReportViewer1.LocalReport.SetParameters(param);

            //put source to report
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.Refresh();
        }
    }
}