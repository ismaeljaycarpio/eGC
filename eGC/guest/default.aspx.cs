using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.guest
{
    public partial class _default : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {
                bindGridview();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGridview();
        }

        protected void gvGuests_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvGuests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGuests.PageIndex = e.NewPageIndex;
            bindGridview();
        }

        protected void gvGuests_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvGuests_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void bindGridview()
        {
            var q = from g in db.Guests
                    where g.GuestId.Equals(txtSearch.Text) ||
                    g.FirstName.Equals(txtSearch.Text) ||
                    g.MiddleName.Equals(txtSearch.Text) ||
                    g.LastName.Equals(txtSearch.Text)
                    select g;

            gvGuests.DataSource = q.ToList();
            gvGuests.DataBind();
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }

            set
            {
                ViewState["directionState"] = value;
            }
        }
    }
}