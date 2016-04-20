using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eGC.guest
{
    public partial class createguest : System.Web.UI.Page
    {
        GiftCheckDataContext db = new GiftCheckDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindDropdown();
            }
        }

        protected void btnGenerateId_Click(object sender, EventArgs e)
        {
            txtGuestId.Text = "AZA-2600-" + DateTime.Now.Year.ToString() + "-";
            int maxId = db.Guests.DefaultIfEmpty().Max(r => r == null ? 0 : r.Id);
            txtGuestId.Text += maxId.ToString();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if(Page.IsValid)
            {
                //upload pics
                if(FileUpload1.HasFile)
                {
                    string fileName = FileUpload1.FileName;
                    Bitmap originalBMP = new Bitmap(FileUpload1.FileContent);

                    // Calculate the new image dimensions
                    int origWidth = originalBMP.Width;
                    int origHeight = originalBMP.Height;
                    int sngRatio = origWidth / origHeight;
                    int newWidth = 200;
                    int newHeight = newWidth / sngRatio;

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
                    int origWidth = originalBMP.Width;
                    int origHeight = originalBMP.Height;
                    int sngRatio = origWidth / origHeight;
                    int newWidth = 200;
                    int newHeight = newWidth / sngRatio;

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

                Guest g = new Guest();
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
                g.CreateDate = DateTime.Now;
                g.CreatedBy = User.Identity.Name;
                g.IsCompany = false;

                db.Guests.InsertOnSubmit(g);
                db.SubmitChanges();

                pnlSuccess.Visible = true;
                clearButtons();
            }
        }

        private void clearButtons()
        {
            txtGuestId.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtMiddleName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtContactNo.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtIdNumber.Text = String.Empty;
            txtContactPerson.Text = String.Empty;
            txtContactPersonNumber.Text = String.Empty;
            txtContactPersonAddress.Text = String.Empty;
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
            ddlCompanyName.Items.Insert(0, new ListItem("--Select Company--", "0"));
        }
    }
}