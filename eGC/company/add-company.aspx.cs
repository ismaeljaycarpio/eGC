using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eGC.DAL;

namespace eGC.company
{
    public partial class add_company : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                txtCompanyId.Focus();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if(Page.IsValid)
            {
                Guest g = new Guest();
                g.GuestId = txtCompanyId.Text;
                g.CompanyName = txtCompanyName.Text;
                g.ContactNumber = txtContactNo.Text;
                g.Email = txtEmail.Text;
                g.ValidIDNumber = txtIdNumber.Text;
                g.EmergencyContactPerson = txtContactPerson.Text;
                g.EmergencyContactNumber = txtContactPersonNumber.Text;
                g.EmergencyContactAddress = txtContactPersonAddress.Text;
                g.CreateDate = DateTime.Now;
                g.CreatedBy = User.Identity.Name;
                g.IsCompany = true;

                db.Guests.InsertOnSubmit(g);
                db.SubmitChanges();

                g.CompanyId = g.Id;
                db.SubmitChanges();

                //audit trail
                DBLogger.Log("Create", "Created Company", g.GuestId);

                Response.Redirect("~/company/company-profile.aspx");

                //pnlSuccess.Visible = true;
                //clearButtons();
            }
        }

        private void clearButtons()
        {
            txtCompanyId.Text = String.Empty;
            txtCompanyName.Text = String.Empty;
            txtContactNo.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtIdNumber.Text = String.Empty;
            txtContactPerson.Text = String.Empty;
            txtContactPersonNumber.Text = String.Empty;
            txtContactPersonAddress.Text = String.Empty;
        }
    }
}