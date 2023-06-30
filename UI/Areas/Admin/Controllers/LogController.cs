using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class LogController : BaseController
    {
        // GET: Admin/Log
        public ActionResult Index()
        {
            return View();
        }
        LogBLL bll = new LogBLL();
        public ActionResult LogList() 
        {
            List<LogDTO> list = new List<LogDTO>();
            list = bll.GetLogs();
            string[] Data = { "Log List", "Logs", "Log", "LogList" };
            ViewData["Data"] = Data;
            return View(list);
        }
    }
}