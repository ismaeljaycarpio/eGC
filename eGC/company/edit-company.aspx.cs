﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.company
{
    public partial class edit_company : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                string companyId = Request.QueryString["companyId"];
                if (companyId == String.Empty)
                {
                    Response.Redirect("~/company/company-profile.aspx");
                }
                else
                {

                    //chk id if valid
                    var gu = (from g in db.Guests
                              where g.GuestId.Equals(companyId)
                              select g).ToList();

                    if (gu.Count < 1)
                    {
                        Response.Redirect("~/company/company-profile.aspx");
                    }
                    else
                    {

                        var guest = gu.FirstOrDefault();

                        txtCompanyId.Text = guest.GuestId;
                        txtCompanyName.Text = guest.CompanyName;
                        txtContactNo.Text = guest.ContactNumber;
                        txtEmail.Text = guest.Email;
                        txtIdNumber.Text = guest.ValidIDNumber;
                        txtContactPerson.Text = guest.EmergencyContactPerson;
                        txtContactPersonNumber.Text = guest.EmergencyContactNumber;
                        txtContactPersonAddress.Text = guest.EmergencyContactAddress;
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if(Page.IsValid)
            {
                var g = (from gu in db.Guests
                         where gu.GuestId.Equals(Request.QueryString["companyId"])
                         select gu).FirstOrDefault();

                g.GuestId = txtCompanyId.Text;
                g.CompanyName = txtCompanyName.Text;
                g.ContactNumber = txtContactNo.Text;
                g.Email = txtEmail.Text;
                g.ValidIDNumber = txtIdNumber.Text;
                g.EmergencyContactPerson = txtContactPerson.Text;
                g.EmergencyContactNumber = txtContactPersonNumber.Text;
                g.EmergencyContactAddress = txtContactPersonAddress.Text;
                g.CreatedBy = User.Identity.Name;

                db.SubmitChanges();

                pnlSuccess.Visible = true;
            }
        }
    }
}