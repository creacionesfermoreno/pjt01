using BotComers.Repository;
using BotComers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //upload files

        public ActionResult uploadFile()
        {
            ResponseModel response = new ResponseModel();

            HomeRepository repo = new HomeRepository();
            HttpFileCollectionBase files = Request.Files;

            if (files.Count > 0)
            {
                ClienteViewModel fi = new ClienteViewModel();
                fi.ImageFile = (HttpPostedFileWrapper)files[0];

                var upload = repo.uploadFileRepo(fi, "notisimages");
                if (!upload.Success)
                {
                    response.Success = false;
                    response.Message1 = "No se pudo subir la imagen";
                }
                else
                {
                    response.Success = true;
                    response.Message1 = upload.Message1;
                }
            }

            return Json(response,JsonRequestBehavior.AllowGet);
        }


    }
}