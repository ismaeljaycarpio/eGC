﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eGC.DAL;

namespace eGC.guest
{
    public partial class editguest : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string guestId = Request.QueryString["guestid"];
                if (guestId == String.Empty)
                {
                    Response.Redirect("~/guest/default.aspx");
                }
                else
                {
                    //load ddl
                    bindDropdown();

                    //chk id if valid
                    var gu = (from g in db.Guests
                            where g.Id.Equals(guestId)
                            select g).ToList();

                    if(gu.Count < 1)
                    {
                        Response.Redirect("~/guest/default.aspx");
                    }
                    else
                    {
                        //load guest
                        var guest = gu.FirstOrDefault();

                        if (!File.Exists(Server.MapPath("~/ProfilePic/") + guest.GuestId + "_Profile.png"))
                        {
                            imgProfile.ImageUrl = "~/ProfilePic/noImage.png";
                        }
                        else
                        {
                            imgProfile.ImageUrl = "~/ProfilePic/" + guest.GuestId + "_Profile.png?t=" + DateTime.Now.ToString();
                        }

                        if (!File.Exists(Server.MapPath("~/IDPic/") + guest.GuestId + "_IDPic.png"))
                        {
                            IDPic.ImageUrl = "~/IDPic/noImage.png";
                        }
                        else
                        {
                            IDPic.ImageUrl = "~/IDPic/" + guest.GuestId + "_IDPic.png?t=" + DateTime.Now.ToString();
                        }

                        txtGuestId.Text = guest.GuestId;
                        txtFirstName.Text = guest.FirstName;
                        txtMiddleName.Text = guest.MiddleName;
                        txtLastName.Text = guest.LastName;
                        ddlCompanyName.SelectedValue = guest.CompanyId.ToString();
                        txtContactNo.Text = guest.ContactNumber;
                        txtEmail.Text = guest.Email;
                        txtIdNumber.Text = guest.ValidIDNumber;
                        txtContactPerson.Text = guest.EmergencyContactPerson;
                        txtContactPersonNumber.Text = guest.EmergencyContactNumber;
                        txtContactPersonAddress.Text = guest.EmergencyContactAddress;

                        checkAccess();
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                //upload pics
                if (FileUpload1.HasFile)
                {
                    string fileName = FileUpload1.FileName;
                    Bitmap originalBMP = new Bitmap(FileUpload1.FileContent);

                    // Calculate the new image dimensions
                    decimal origWidth = originalBMP.Width;
                    decimal origHeight = originalBMP.Height;
                    decimal sngRatio = origWidth / origHeight;
                    int newWidth = 200;
                    decimal newHeight_temp = newWidth / sngRatio;
                    int newHeight = Convert.ToInt32(newHeight_temp);

                    Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
                    Graphics oGraphics = Graphics.FromImage(newBMP);

                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                    newBMP.Save(Server.MapPath("~/ProfilePic/") + txtGuestId.Text + "_Profile.png");

                    originalBMP.Dispose();
                    newBMP.Dispose();
                    oGraphics.Dispose();

                    FileUpload1.FileContent.Dispose();
                    FileUpload1.Dispose();
                }

                if (FileUpload2.HasFile)
                {
                    string fileName = FileUpload2.FileName;
                    Bitmap originalBMP = new Bitmap(FileUpload2.FileContent);

                    // Calculate the new image dimensions
                    decimal origWidth = originalBMP.Width;
                    decimal origHeight = originalBMP.Height;
                    decimal sngRatio = origWidth / origHeight;
                    int newWidth = 200;
                    decimal newHeight_temp = newWidth / sngRatio;
                    int newHeight = Convert.ToInt32(newHeight_temp);

                    Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
                    Graphics oGraphics = Graphics.FromImage(newBMP);

                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                    newBMP.Save(Server.MapPath("~/IDPic/") + txtGuestId.Text + "_IDPic.png");

                    originalBMP.Dispose();
                    newBMP.Dispose();
                    oGraphics.Dispose();

                    FileUpload2.FileContent.Dispose();
                    FileUpload2.Dispose();
                }

                var g = (from gu in db.Guests
                         where gu.Id.Equals(Request.QueryString["guestid"])
                         select gu).FirstOrDefault();

                g.GuestId = txtGuestId.Text;
                g.FirstName = txtFirstName.Text;
                g.MiddleName = txtMiddleName.Text;
                g.LastName = txtLastName.Text;
                g.CompanyId = Convert.ToInt32(ddlCompanyName.SelectedValue);
                g.ContactNumber = txtContactNo.Text;
                g.Email = txtEmail.Text;
                g.ValidIDNumber = txtIdNumber.Text;
                g.PicFilePath = txtGuestId.Text + "_Profile.png";
                g.IDFilePath = txtGuestId.Text + "_IDPic.png";
                g.EmergencyContactPerson = txtContactPerson.Text;
                g.EmergencyContactNumber = txtContactPersonNumber.Text;
                g.EmergencyContactAddress = txtContactPersonAddress.Text;
                g.CreatedBy = User.Identity.Name;

                db.SubmitChanges();

                //audit trail
                DBLogger.Log("Update", "Updated Individual Guest", g.GuestId);

                pnlSuccess.Visible = true;

                //load images
                if (!File.Exists(Server.MapPath("~/ProfilePic/") + g.GuestId + "_Profile.png"))
                {
                    imgProfile.ImageUrl = "~/ProfilePic/noImage.png";
                }
                else
                {
                    imgProfile.ImageUrl = "~/ProfilePic/" + g.GuestId + "_Profile.png?t=" + DateTime.Now.ToString();
                }

                if (!File.Exists(Server.MapPath("~/IDPic/") + g.GuestId + "_IDPic.png"))
                {
                    IDPic.ImageUrl = "~/IDPic/noImage.png";
                }
                else
                {
                    IDPic.ImageUrl = "~/IDPic/" + g.GuestId + "_IDPic.png?t=" + DateTime.Now.ToString();
                }
            }
        }

        private void bindDropdown()
        {
            var q = (from g in db.Guests
                     where g.IsCompany == true
                     select new
                     {
                         Id = g.Id,
                         CompanyName = g.CompanyName
                     }).ToList();

            ddlCompanyName.DataSource = q;
            ddlCompanyName.DataTextField = "CompanyName";
            ddlCompanyName.DataValueField = "Id";
            ddlCompanyName.DataBind();
            ddlCompanyName.Items.Insert(0, new ListItem("-- Select Company --", "0"));
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/guest/default.aspx");
        }

        private void checkAccess()
        {
            if(!User.IsInRole("can-create-gc") && !User.IsInRole("Admin-GC"))
            {
                FileUpload1.Enabled = false;
                FileUpload2.Enabled = false;
                txtGuestId.Enabled = false;
                txtFirstName.Enabled = false;
                txtMiddleName.Enabled = false;
                txtLastName.Enabled = false;
                ddlCompanyName.Enabled = false;
                txtContactNo.Enabled = false;
                txtEmail.Enabled = false;
                txtIdNumber.Enabled = false;
                txtContactPerson.Enabled = false;
                txtContactPersonNumber.Enabled = false;
                txtContactPersonAddress.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }
    }
}