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
    public class PostController : BaseController
    {
        PostBLL bll = new PostBLL();
        // GET: Admin/Post
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult PostList()
        {
            CountDTO countdto = new CountDTO();
            countdto = bll.GetAllCounts();
            ViewData["AllCounts"] = countdto;
            string[] Data = { "Post List","Posts","Post","PostList" };
            ViewData["Data"] = Data;
            List<PostDTO> postlist = new List<PostDTO>();
            postlist = bll.GetPosts();
            return View(postlist);
        }
        public ActionResult AddPost()
        {
            PostDTO model = new PostDTO();
            model.Categories = CategoryBLL.GetCategoriesForDropDown();
            string[] Data = { "Add Post", "Posts", "Post", "PostList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDTO model)
        {
            if (model.PostImage[0] == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                foreach (var item in model.PostImage)
                {
                    Bitmap image = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropDown();
                        return View(model);
                    }
                }
                List<PostImageDTO> imagelist = new List<PostImageDTO>();
                foreach (var postedFile in model.PostImage)
                {
                    Bitmap image = new Bitmap(postedFile.InputStream);
                    Bitmap resizeimage = new Bitmap(image, 750, 422);
                    string filename = "";
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFile.FileName;
                    resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = filename;
                    imagelist.Add(dto);
                }
                model.PostImages = imagelist;
                if (bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new PostDTO();
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
            model.Categories = CategoryBLL.GetCategoriesForDropDown();
            string[] Data = { "Add Post", "Posts", "Post", "PostList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public ActionResult UpdatePost(int ID)
        {
            PostDTO model = new PostDTO();
            model = bll.GetPostWithID(ID);
            model.Categories = CategoryBLL.GetCategoriesForDropDown();
            model.isUpdate = true;
            string[] Data = { "Update Post", "Posts", "Post", "PostList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(PostDTO model)
        {
            IEnumerable<SelectListItem> selectlist = CategoryBLL.GetCategoriesForDropDown();
            if (ModelState.IsValid)
            {
                if (model.PostImage[0]!= null)
                { 
                foreach (var item in model.PostImage)
                {
                    Bitmap image = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropDown();
                        return View(model);
                    }
                }
                List<PostImageDTO> imagelist = new List<PostImageDTO>();
                foreach (var postedFile in model.PostImage)
                {
                    Bitmap image = new Bitmap(postedFile.InputStream);
                    Bitmap resizeimage = new Bitmap(image, 750, 422);
                    string filename = "";
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFile.FileName;
                    resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = filename;
                    imagelist.Add(dto);
                }
                model.PostImages = imagelist;
                }
                if (bll.UpdatePost(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;


                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
            model = bll.GetPostWithID(model.ID);
            model.Categories = selectlist;
            model.isUpdate=true;
            string[] Data = { "Update Post", "Posts", "Post", "PostList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public JsonResult DeletePostImage(int ID)
        {
            string imagepath = bll.DeletePostImage(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagepath));
            }
            string[] Data = { "Post List", "Posts", "Post", "PostList" };
            ViewData["Data"] = Data;
            return Json("");
        }
        public JsonResult DeletePost(int ID)
        {
            List<PostImageDTO> imagelist = bll.DeletePost(ID);
            foreach(var item in imagelist)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath));
                }
            }
            string[] Data = { "Post List", "Posts", "Post", "PostList" };
            ViewData["Data"] = Data;
            return Json("");
        }
        public JsonResult GetCounts()
        {
            CountDTO dto = new CountDTO();
            dto = bll.GetCounts();
            string[] Data = { "Post List", "Posts", "Post", "PostList" };
            ViewData["Data"] = Data;
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
    }
}