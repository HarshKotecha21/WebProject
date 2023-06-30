using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        LayoutBLL layoutbll = new LayoutBLL();
        GeneralBLL bll = new GeneralBLL();
        ContactBLL Contactbll = new ContactBLL();
        PostBLL postbll = new PostBLL();
        public ActionResult Index()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetAllPosts();
            return View(dto);
        }
        public ActionResult CategoryPostList(string categoryname)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetCategoryPostList(categoryname);
            return View(dto);
        }
        public ActionResult PostDetail(int ID)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetPostDetailPageitemsWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public ActionResult PostDetail(GeneralDTO model)
        {
            if (model.Name != null && model.Email != null && model.Message != null)
            {
                if (postbll.AddComment(model))
                {
                    ViewData["CommentState"] = "Success";
                    ModelState.Clear();
                }
                else
                    ViewData["CommentState"] = "Error";
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            model = bll.GetPostDetailPageitemsWithID(model.PostID);
            return View(model);
        }
        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetContactPageItems();
            return View(dto);
        }
        [Route("ContactUs")]
        [HttpPost]
        public ActionResult ContactUs(GeneralDTO model)
        {
            if(model.Name!=null && model.Subject!=null && model.Email != null && model.Message != null)
            {
                if (Contactbll.AddContact(model))
                    ViewData["commentState"] = "Success";
                else
                    ViewData["commentState"] = "Error";
            }
            else
            {
                ViewData["commentState"] = "Error";
            }
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetContactPageItems();
            return View(dto);
        }
        [Route("search")]
        [HttpPost]  
        public ActionResult Search(GeneralDTO model)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetSearchPosts(model.SearchText);
            return View(dto);
        }

    }
}