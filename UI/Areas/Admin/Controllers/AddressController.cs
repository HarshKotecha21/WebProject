using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : BaseController
    {
        // GET: Admin/Address
        AddressBLL bll = new AddressBLL();
        public ActionResult AddressList()
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddress();
            string[] Data = { "Address List", "Addresss", "Address", "AddressList" };
            ViewData["Data"] = Data;
            return View(list);
        }
        public ActionResult AddAddress()
        {
            AddressDTO dto = new AddressDTO();
            string[] Data = { "Add Address", "Addresss", "Address", "AddressList" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new AddressDTO();
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            string[] Data = { "Add Address", "Addresss", "Address", "AddressList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public ActionResult UpdateAddress(int ID)
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddress();
            AddressDTO dto = list.First(x=>x.ID == ID);
            string[] Data = { "Update Address", "Addresss", "Address", "AddressList" };
            ViewData["Data"] = Data;
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO model)
        {
            if(ModelState.IsValid)
            {
                if(bll.UpdateAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
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
            string[] Data = { "Address List", "Addresss", "Address", "AddressList" };
            ViewData["Data"] = Data;
            return View(model);
        }
        public JsonResult DeleteAddress(int ID)
        {
            bll.DeleteAddress(ID);
            string[] Data = { "Address List", "Addresss", "Address", "AddressList" };
            ViewData["Data"] = Data;
            return Json("");
        }
    }
}