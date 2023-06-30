using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        ContactBLL bll = new ContactBLL();
        public ActionResult UnreadMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = bll.GetUnreadMessages();
            string[] Data = { "Unread Messages", "Messages", "Message", "MessageList1" };
            ViewData["Data"] = Data;
            return View(list);
        }
        public ActionResult AllMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = bll.GetAllMessages();
            string[] Data = { "All Messages", "Messages", "Message", "MessageList" };
            ViewData["Data"] = Data;
            return View(list);
        }
        public ActionResult ReadMessages(int ID)
        {
            bll.ReadMessage(ID);
            return RedirectToAction("UnreadMessages");
        }
        public ActionResult ReadMessages2(int ID)
        {
            bll.ReadMessage(ID);
            return RedirectToAction("AllMessages");
        }
        public JsonResult DeleteMessage(int ID)
        {
            bll.DeleteMessage(ID);
            return Json("");
        }
    }
}