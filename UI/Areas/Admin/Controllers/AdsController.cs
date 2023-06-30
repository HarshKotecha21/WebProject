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
    public class AdsController : BaseController
    {
        AdsBLL bll = new AdsBLL();
        // GET: Admin/Ads
        public ActionResult AdsList()
        {
            List<AdsDTO> AdsList = new List<AdsDTO>();
            AdsList = bll.GetAds();
            string[] Data = { "Ads List", "Adss", "Ads", "AdsList" };
            ViewData["Data"] = Data;
            return View(AdsList);
        }
        public ActionResult AddAds()
        {
            AdsDTO dto = new AdsDTO();
            string[] Data = { "Add Ads", "Adss", "Ads", "AdsList" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        public ActionResult AddAds(AdsDTO model)
        {
            if (model.AdsImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                string filename = "";
                HttpPostedFileBase postedfile = model.AdsImage;
                Bitmap UserImage = new Bitmap(postedfile.InputStream);
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedfile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string unqiueNumber = Guid.NewGuid().ToString();
                    filename = unqiueNumber + postedfile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + filename));
                    model.ImagePath = filename;
                    bll.AddAds(model);
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new AdsDTO();
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
            string[] Data = { "Add Ads", "Adss", "Ads", "AdsList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public ActionResult UpdateAds(int ID)
        {
            AdsDTO dto = bll.GetAdsWithID(ID);
            string[] Data = { "Update Ads", "Adss", "Ads", "AdsList" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        public ActionResult UpdateAds(AdsDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else if (model.AdsImage != null)
            {
                string filename = "";
                HttpPostedFileBase postedfile = model.AdsImage;
                Bitmap UserImage = new Bitmap(postedfile.InputStream);
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedfile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string unqiueNumber = Guid.NewGuid().ToString();
                    filename = unqiueNumber + postedfile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + filename));
                    model.ImagePath = filename;
                }
                string oldImagePath = bll.UpdateAds(model);
                if(model.AdsImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + oldImagePath)))
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + oldImagePath));
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            string[] Data = { "Update Ads", "Adss", "Ads", "AdsList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public ActionResult DeleteAds(int ID)
        {
            string imagepath = bll.DeleteAds(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + imagepath));
            }
            string[] Data = { "Ads List", "Adss", "Ads", "AdsList" };
            ViewData["Data"] = Data;
            return Json("");
        }
    }
}