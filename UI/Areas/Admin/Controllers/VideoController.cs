using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {
        VideoBLL bll = new VideoBLL();
        // GET: Admin/Video
        public ActionResult VideoList()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            dtolist = bll.GetVideos();
            string[] Data = { "Video List", "Videos", "Video", "VideoList" };
            ViewData["Data"] = Data;
            return View(dtolist);
        }
        public ActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();
            string[] Data = { "Add Video", "Videos", "Video", "VideoList" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoDTO model)
        {
            //<iframe width="560" height="315" src="https://www.youtube.com/embed/9V4bRSmf988" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/";
                mergelink += path;
                model.VideoPath = string.Format(@"<iframe width=""300"" height=""200"" src=""{0}"" frameborder=""0"" allowfullscreen ></iframe >", mergelink);
                if(bll.AddVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
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
            string[] Data = { "Add Video", "Videos", "Video", "VideoList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public ActionResult UpdateVideo(int ID)
        {
            VideoDTO dto = bll.GetVideoWithID(ID);
            string[] Data = { "Update Video", "Videos", "Video", "VideoList" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/";
                mergelink += path;
                model.VideoPath = string.Format(@"< iframe width=""300"" height=""200"" src=""{0}"" frameborder=""0"" allowfullscreen ></ iframe >", mergelink);
                if (bll.UpdateVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
                ViewBag.ProcessSate = General.Messages.EmptyArea;
            string[] Data = { "Update Video", "Videos", "Video", "VideoList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public JsonResult DeleteVideo(int ID)
        {
            bll.DeleteVideo(ID);
            string[] Data = { "Video List", "Videos", "Video", "VideoList" };
            ViewData["Data"] = Data;
            return Json("");
        }
    }
}