using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : BaseController
    {
        // GET: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }
        MetaBLL bll = new MetaBLL();
        public ActionResult AddMeta() 
        {
            MetaDTO dto = new MetaDTO();
            string[] Data = { "Add Meta", "SiteSettings", "SiteSetting", "MetaList" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        public ActionResult AddMeta(MetaDTO model) 
        {
            if(ModelState.IsValid)
            {
                if (bll.AddMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            MetaDTO newmodel = new MetaDTO();
            string[] Data = { "Add Meta", "SiteSettings", "SiteSetting", "MetaList" };
            ViewData["Data"] = Data;
            return View(newmodel);
        }
        public ActionResult MetaList()
        {
            List<MetaDTO> model = new List<MetaDTO>();
            model = bll.GetMetaData();
            string[] Data = { "Meta List", "SiteSettings","SiteSetting", "MetaList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public ActionResult UpdateMeta(int ID)
        {
            MetaDTO model = new MetaDTO();
            model = bll.GetMetaWithID(ID);
            string[] Data = { "Update Meta", "SiteSettings", "SiteSetting", "MetaList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.processState = General.Messages.EmptyArea;
            }
            string[] Data = { "Meta List", "SiteSettings", "SiteSetting", "MetaList" };
            ViewData["Data"] = Data;
            return View(model);
            
        }
        public JsonResult DeleteMeta(int ID)
        {
            bll.DeleteMeta(ID);
            string[] Data = { "Meta List", "SiteSettings", "SiteSetting", "MetaList" };
            ViewData["Data"] = Data;
            return Json("");
        }
    }
}