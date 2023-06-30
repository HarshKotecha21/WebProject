using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        // GET: Admin/Comment
        PostBLL bll = new PostBLL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UnapprovedComments()
        {
            List<CommentDTO> commentlist = new List<CommentDTO>();
            commentlist = bll.GetComments();
            string[] Data = { "Unapproved Comments", "Unapps", "Unapp", "UnappList" };
            ViewData["Data"] = Data;
            return View(commentlist);
        }
        public ActionResult AllComments()
        {
            List<CommentDTO> commentlist = bll.GetAllComments();
            string[] Data = { "All Comments", "Unapps", "Unapp", "UnappList1" };
            ViewData["Data"] = Data;
            return View(commentlist);
        }
        public ActionResult ApproveComment(int ID)
        {
            bll.ApproveComment(ID);
            return RedirectToAction("UnapprovedComments", "Comment");
        }
        public ActionResult ApproveComment2(int ID)
        {
            bll.ApproveComment(ID);
            return RedirectToAction("AllComments", "Comment");
        }
        public JsonResult DeleteComment(int ID)
        {
            bll.DeleteComment(ID);
            return Json("");
        }
    }
}