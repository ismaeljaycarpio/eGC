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

            var rooms = (from room in db.Rooms
                        join gcroom in db.GCRooms
                        on room.Id equals gcroom.RoomId
                        join tr in db.GCTransactions
                        on gcroom.GCTransactionId equals tr.Id
                        where tr.GCNumber == gcId
                        select new
                        {
                            Id = gcroom.Id,
                            Type = room.Type,
                            Room = room.Room1,
                            WithBreakfast = gcroom.WithBreakfast,
                            HowManyPerson = gcroom.HowManyPerson
                        }).ToList();

            var dinings = (from dining in db.Dinings
                          join gcdining in db.GCRooms
                          on dining.Id equals gcdining.DiningId
                          join tr in db.GCTransactions
                          on gcdining.GCTransactionId equals tr.Id
                          where tr.GCNumber == gcId
                          select new
                          {
                              Id = gcdining.Id,
                              Name = dining.Name,
                              DiningType = gcdining.DiningType,
                              HeadCount = gcdining.HowManyDiningPerson
                          }).ToList();

            dtRooms = rooms.ToDataTable().AsEnumerable().CopyToDataTable();
            dtDinings = dinings.ToDataTable().AsEnumerable().CopyToDataTable();
            
            //first param: name of the dataset
            ReportDataSource rdsRooms = new ReportDataSource("Rooms", dtRooms);
            ReportDataSource rdsDinings = new ReportDataSource("Dinings", dtDinings);

            //put source to report
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rdsRooms);
            ReportViewer1.LocalReport.DataSources.Add(rdsDinings);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}