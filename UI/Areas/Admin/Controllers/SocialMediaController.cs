using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class SocialMediaController : BaseController
    {
        SocialMediaBLL bll = new SocialMediaBLL();
        // GET: Admin/SocialMedia
        public ActionResult AddSocialMedia()
        {
            SocialMediaDTO model = new SocialMediaDTO();
            string[] Data = { "Social Media List", "SocialMedias", "SocialMedia", "SocialMediaList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO model)
        {
            if (model.SocialImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;

            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.SocialImage;
                Bitmap SocialMedia = new Bitmap(postedFile.InputStream);
                string ext = Path.GetExtension(postedFile.FileName);
                string filename = "";
                if (ext == ".jpg" || ext ==".jpeg" || ext==".png" || ext==".gif")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedFile.FileName;
                    SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/"+filename));
                    model.ImagePath = filename;
                    if(bll.AddSocialMedia(model))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.GeneralError;
                    }
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.ExtensionError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            string[] Data = { "Social Media List", "SocialMedias", "SocialMedia", "SocialMediaList" };
            ViewData["Data"] = Data;
            return View(model);
        }
    
        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            dtolist = bll.GetSocialMedia();
            string[] Data = { "Social Media List", "SocialMedias", "SocialMedia", "SocialMediaList" };
            ViewData["Data"] = Data;

            return View(dtolist);
        }
        public ActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDTO dto = bll.GetSocialMediaWithID(ID);
            string[] Data = { "Social Media List", "SocialMedias", "SocialMedia", "SocialMediaList" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
           if(!ModelState.IsValid)
           {
                ViewBag.ProcessState = General.Messages.EmptyArea;
           }
           else
           {
                if (model.SocialImage != null)
                {
                    HttpPostedFileBase postedFile = model.SocialImage;
                    Bitmap SocialMedia = new Bitmap(postedFile.InputStream);
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedFile.FileName;
                        SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                        model.ImagePath = filename;
                    }
                }
                string OldImagePath = bll.UpdateSocialMedia(model);
                if(model.SocialImage!=null)
                {
                    if(System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" +OldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + OldImagePath));
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
           }
            string[] Data = { "Social Media List", "SocialMedias", "SocialMedia", "SocialMediaList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public JsonResult DeleteSocialMedia(int ID)
        {
            string imagepath = bll.DeleteSocialMedia(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + imagepath));
            }
            string[] Data = { "Social Media List", "SocialMedias", "SocialMedia", "SocialMediaList" };
            ViewData["Data"] = Data;
            return Json("");
        }
    }
}