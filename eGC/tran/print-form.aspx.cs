using System;
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
            DataTable dtRooms = new DataTable();
            dtRooms.TableName = "Rooms";
            DataTable dtDinings = new DataTable();
            dtDinings.TableName = "Dinings";

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/tran/gc-report.rdlc");

            //var rooms = (from room in db.Rooms
            //            join gcroom in db.GCRooms
            //            on room.Id equals gcroom.RoomId
            //            join tr in db.GCTransactions
            //            on gcroom.GCTransactionId equals tr.Id
            //            where tr.GCNumber == gcId
            //            select new
            //            {
            //                Id = gcroom.Id,
            //                Type = room.Type,
            //                Room = room.Room1,
            //                WithBreakfast = gcroom.WithBreakfast,
            //                HowManyPerson = gcroom.HowManyPerson
            //            }).ToList();

            //var dinings = (from dining in db.Dinings
            //              join gcdining in db.GCRooms
            //              on dining.Id equals gcdining.DiningId
            //              join tr in db.GCTransactions
            //              on gcdining.GCTransactionId equals tr.Id
            //              where tr.GCNumber == gcId
            //              select new
            //              {
            //                  Id = gcdining.Id,
            //                  Name = dining.Name,
            //                  DiningType = gcdining.DiningType,
            //                  HeadCount = gcdining.HowManyDiningPerson
            //              }).ToList();

            //if(rooms.Count > 0)
            //{
            //    dtRooms = rooms.ToDataTable().AsEnumerable().CopyToDataTable();
            //}
            
            //if(dinings.Count > 0)
            //{
            //    dtDinings = dinings.ToDataTable().AsEnumerable().CopyToDataTable();
            //}
                   
            //first param: name of the dataset
            ReportDataSource rdsRooms = new ReportDataSource("Rooms", dtRooms);
            ReportDataSource rdsDinings = new ReportDataSource("Dinings", dtDinings);

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
                           Reason = gctran.GCType,
                           //AccountNo = gctran.AccountNo,
                           RecommendingApproval = gctran.RecommendingApproval,
                           //Remarks = gctran.Remarks,
                           DateCancelled = gctran.CancelledDate,
                           ReasonForCancellation = gctran.CancellationReason,
                           GCStatus = gctran.StatusGC,
                           ApprovedBy = gctran.ApprovedBy
                       }).FirstOrDefault();

            string approvedBy = String.Empty;
            string expirationDate = String.Empty;
            string dateCancelled = String.Empty;


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

            //fill param
            ReportParameter[] param = new ReportParameter[15];

            param[0] = new ReportParameter("GuestId", tran.GuestId);
            param[1] = new ReportParameter("FullName", tran.FullName);
            param[2] = new ReportParameter("CompanyName", tran.CompanyName);
            param[3] = new ReportParameter("ContactNo", tran.ContactNo);
            param[4] = new ReportParameter("Email", tran.Email);
            param[5] = new ReportParameter("GCNumber", tran.GCNumber);
            param[6] = new ReportParameter("ExpirationDate", expirationDate);
            param[7] = new ReportParameter("Reason", tran.Reason);
            //param[8] = new ReportParameter("AccountNo", tran.AccountNo);
            param[9] = new ReportParameter("RecommendingApproval", tran.RecommendingApproval);
            //param[10] = new ReportParameter("Remarks", tran.Remarks);
            param[11] = new ReportParameter("DateCancelled", dateCancelled);
            param[12] = new ReportParameter("ReasonForCancellation", tran.ReasonForCancellation);
            param[13] = new ReportParameter("GCStatus", tran.GCStatus);
            param[14] = new ReportParameter("ApprovedBy", approvedBy);

            //put param to report
            ReportViewer1.LocalReport.SetParameters(param);

            //put source to report
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rdsRooms);
            ReportViewer1.LocalReport.DataSources.Add(rdsDinings);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}