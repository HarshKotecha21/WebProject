﻿using BLL;
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
    public class UserController : BaseController
    {
        UserBLL bll = new UserBLL();
        // GET: Admin/User

        public ActionResult UserList()
        {
            List<UserDTO> model = new List<UserDTO>();
            model = bll.GetUsers();
            string[] Data = { "User List", "Users", "User", "UserList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public ActionResult AddUser()
        {
            UserDTO model = new UserDTO();
            string[] Data = { "Add User", "Users", "User", "UserList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddUser(UserDTO model)
        {
            if (model.UserImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                string filename = "";
                HttpPostedFileBase postedfile = model.UserImage;
                Bitmap UserImage = new Bitmap(postedfile.InputStream);
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedfile.FileName);
                if(ext ==".jpg" || ext==".jpeg" || ext==".png" || ext==".gif")
                {
                    string unqiueNumber = Guid.NewGuid().ToString();
                    filename = unqiueNumber + postedfile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImage/"+filename));
                    model.Imagepath = filename;
                    bll.AddUser(model);
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new UserDTO();
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
            string[] Data = { "Add User", "Users", "User", "UserList" };
            ViewData["Data"] = Data;
            return View(model);   
        }
        public ActionResult UpdateUser(int ID)
        {
            UserDTO dto = new UserDTO();
            dto = bll.GetUserWithID(ID);
            string[] Data = { "User Update", "Users", "User", "UserList2" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        public ActionResult UpdateUser(UserDTO model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else 
            {
                if (model.UserImage != null)
                {
                    string filename = "";
                    HttpPostedFileBase postedfile = model.UserImage;
                    Bitmap UserImage = new Bitmap(postedfile.InputStream);
                    Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string unqiueNumber = Guid.NewGuid().ToString();
                        filename = unqiueNumber + postedfile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImage/" + filename));
                        model.Imagepath = filename;
                    }
                    
                }
                string oldImagePAth = bll.UpdateUser(model);
                if(model.UserImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePAth)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePAth));
                    }
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
            }
            string[] Data = { "User Update", "Users", "User", "UserList2" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public JsonResult DeleteUser(int ID)
        {
            string imagepath = bll.DeleteUser(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImage/" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImage/" + imagepath));
            }
            string[] Data = { "User List", "Users", "User", "UserList" };
            ViewData["Data"] = Data;
            return Json("");
        }
    }
}