using BotComers.Helpers;
using BotComers.Models;
using BotComers.Repository;
using BotComers.ViewModels;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using Microsoft.Ajax.Utilities;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BotComers.Controllers
{
    public class emailcampaingController : Controller
    {
        // GET: emailcampaing
        public ActionResult Index()
        {
            return View();
        }


        //register email campaing
        [ValidateInput(false)]
        public ActionResult RegisterEmailCampaing(string name, string subject = "campaña demo", string content = "", int mode = 0)
        {
            ResponseModel response = new ResponseModel();

            List<Object> errors = new List<Object>();

            if (string.IsNullOrEmpty(content) || content == " ")
            {
                errors.Add(new { name = "Contenido  requerido" });
            }
            if (string.IsNullOrEmpty(name))
            {
                errors.Add(new { name = "Nombre requerido" });
            }

            HttpFileCollectionBase files = Request.Files;

            ClienteViewModel ExeclFileUp = new ClienteViewModel();
            List<HttpPostedFileBase> FilesEmail = new List<HttpPostedFileBase>();
            List<ClienteViewModel> FilesEmailUp = new List<ClienteViewModel>();
            for (int i = 0; i < files.AllKeys.Length; i++)
            {
                if (files.AllKeys[i] == "excel")
                {
                    var filetemp = (HttpPostedFileWrapper)files[i];
                    ExeclFileUp.ImageFile = filetemp;
                }
                else
                {
                    HttpPostedFileBase file = files[i];
                    FilesEmail.Add(file);
                    //for upload
                    var temp = (HttpPostedFileWrapper)files[i];
                    FilesEmailUp.Add(new ClienteViewModel() { ImageFile = temp });
                }
            }

            if (ExeclFileUp.ImageFile == null)
            {
                errors.Add(new { name = "Destinatario requerido" });
            }



            string path = Server.MapPath("~/Content/assets/filetemps/");
            HomeRepository hrepo = new HomeRepository();

            //upload addresss
            var upload = hrepo.uploadFileRepo(ExeclFileUp, "emailscampaing");
            if (!upload.Success)
            {
                errors.Add(new { name = upload.Message1 });
            }

            if (errors.Count > 0)
            {
                response.Success = false;
                response.Message1 = content;
                response.Message2 = name;
                response.Date = errors;
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            if (mode == 0)
            {
                //mode=0, send now

                Random random = new Random();
                List<FileSendModel> filesArray = new List<FileSendModel>();
                for (int i = 0; i < FilesEmail.Count; i++)
                {
                    HttpPostedFileBase file = FilesEmail[i];
                    string extension = Path.GetExtension(file.FileName);
                    string nameFile = random.Next() + extension;
                    file.SaveAs(path + nameFile);
                    filesArray.Add(new FileSendModel() { Name = nameFile, Origin = file.FileName });
                }
                //----------------------------- set people excel -----------------------------------
                //save temp excel
                var client = new WebClient();
                String url = upload?.Message1;
                var fullPath = Path.GetTempFileName();

                client.DownloadFile(url, fullPath);
                SLDocument sl = new SLDocument(fullPath);

                //clear tempFile
                var fileP = new FileInfo(fullPath);
                fileP.Delete();

                List<FilePeople> dataPeople = new List<FilePeople>();

                //reading
                int iRow = 2;
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    string email = sl.GetCellValueAsString(iRow, 1);
                    bool valid = Commun.IsValidEmail(email);
                    if (valid) { dataPeople.Add(new FilePeople() { Email = email, Name = "---", Valid = true }); }
                    else { dataPeople.Add(new FilePeople() { Email = email, Name = "---", Valid = false }); }
                    iRow++;
                }
                //----------------------------- set people excel -----------------------------------

                int bccTotal = int.Parse(ConfigurationManager.AppSettings["BCCTOTAL"]);
                if (dataPeople.Count > bccTotal)
                {
                    var newList = HomeHelper.SplitList(dataPeople, bccTotal);
                    for (int i = 0; i < newList.Count; i++)
                    {

                        response = hrepo.SendEmailMassiveRepo(newList[i], subject, content, filesArray, path);
                    }
                }
                else
                {
                    //send
                    response = hrepo.SendEmailMassiveRepo(dataPeople, subject, content, filesArray, path);

                }

                //remove files
                foreach (FileSendModel a in filesArray)
                {
                    Commun.removeFile(path + a.Name);
                }

                if (!response.Success)
                {

                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                //register email campaign
                EmailCampaingRepository eCampaignRepo = new EmailCampaingRepository();
                EmailCampaingDTO model = new EmailCampaingDTO();
                model.NombreCorreoCampania = name;
                model.UrlDestinatarios = upload.Message1;
                model.SendCorreo = true;
                model.Content_html = content;
                model.Action = "created";

                var register = eCampaignRepo.RegisterECampainRepo(model);
                if (register != null)
                {
                    //register detail -- emails
                    foreach (FilePeople item in dataPeople)
                    {
                        var regDetail = eCampaignRepo.RegisterECampainDetailRepo(CodigoCorreoCampania: register, Destinatario: item.Email, EstadoCorreoCampania: item.Valid);
                    }
                    var totalValids = dataPeople.Where(itemx => itemx.Valid == true);
                    response.Message1 = $"Enviado correntamente a {totalValids.Count()} correos";
                    response.Success = true;
                }

                else
                {
                    response.Message1 = "Error en el proceso";
                    response.Success = false;
                }


            }
            else
            {
                //mode=1, save


                List<string> dataUrls = new List<string>();
                foreach (ClienteViewModel filem in FilesEmailUp)
                {
                    var uploadFiles = hrepo.uploadFileRepo(filem, "emailscampaing");
                    response.Message1 += uploadFiles.Message1;
                    dataUrls.Add(uploadFiles.Message1);

                }

                //register email campaign
                EmailCampaingRepository eCampaignRepo = new EmailCampaingRepository();
                EmailCampaingDTO model = new EmailCampaingDTO();
                model.NombreCorreoCampania = name;
                model.UrlDestinatarios = upload.Message1;
                model.SendCorreo = false;
                model.Content_html = content;
                model.Action = "created";

                var register = eCampaignRepo.RegisterECampainRepo(model);

                if (register != null)
                {
                    //register files
                    for (int i = 0; i < dataUrls.Count; i++)
                    {
                        var regDetail = eCampaignRepo.RegisterECampainFilesRepo(CodigoCorreoCampania: register, url: dataUrls[i]);
                        response.Message2 += regDetail;
                    }

                    response.Message1 = $"Campaña registrado correctamente";
                    response.Success = true;
                }

                else
                {
                    response.Message1 = "Error en el proceso";
                    response.Success = false;
                }

            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }


        //list email campaign
        public ActionResult ListCampaign(int PageNumber = 1)
        {
            List<EmailCampaingDTO> lista = new List<EmailCampaingDTO>();

            EmailCampaingDTO oModel = new EmailCampaingDTO();
            oModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oModel.CodigoSede = Commun.CodigoSede;

            ReqFilterEmailCampaingDTO oReq = new ReqFilterEmailCampaingDTO()

            {
                FilterCase = FilterEmailCampaing.ListPagination,
                Item = oModel,
                User = Commun.Usuario,

                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListEmailCampaingDTO oResp = null;
            EmailCampaingLogic oLogic = new EmailCampaingLogic();

            oResp = oLogic.GetList(oReq);


            return Json(oResp, JsonRequestBehavior.AllowGet);
        }


        //destroy email campaign
        public ActionResult DestroyECamp(string code)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<EmailCampaingDTO> list = new List<EmailCampaingDTO>();

            bool validadorParametros = true;

            if (code == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un codigo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }
            list.Add(new EmailCampaingDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoCorreoCampania = code,
                Operation = Operation.Delete,
            }); ;
            ReqEmailCampaingDTO oReq = new ReqEmailCampaingDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespEmailCampaingDTO oResp = null;
            EmailCampaingLogic logic = new EmailCampaingLogic();
            oResp = logic.ExecuteTransac(oReq);
            if (oResp.Success)
            {
                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Status = 0;
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }


        //show item email campaign
        public ActionResult ShowECamp(string code)
        {
            EmailCampaingDTO oDTO = new EmailCampaingDTO();
            try
            {
                oDTO.CodigoSede = Commun.CodigoSede;
                oDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oDTO.CodigoCorreoCampania = code;
                ReqFilterEmailCampaingDTO oReq = new ReqFilterEmailCampaingDTO()
                {
                    FilterCase = FilterEmailCampaing.Search,
                    Item = oDTO,
                    User = Commun.Usuario,
                };
                RespItemEmailCampaingDTO oResp = null;
                EmailCampaingLogic oLogic = new EmailCampaingLogic();

                oResp = oLogic.GetItem(oReq);

                if (oResp.Success)
                {
                    oDTO = oResp.Item;

                }
            }
            catch (Exception) { }
            return Json(oDTO, JsonRequestBehavior.AllowGet);
        }


        //send email campaign
        public ActionResult SendECamp(string code = "")
        {
            ResponseModel response = new ResponseModel();

            EmailCampaingRepository repository = new EmailCampaingRepository();
            var data = repository.getItemECampRepo(code);
            if (string.IsNullOrEmpty(data.Content_html))
            {
                response.Success = false;
            }
            else
            {
                string path = Server.MapPath("~/Content/assets/filetemps/");
                HomeRepository hrepo = new HomeRepository();

                //----------------------------- set people excel -----------------------------------
                //save temp excel
                var client = new WebClient();
                String url = data?.UrlDestinatarios;
                var fullPath = Path.GetTempFileName();
                client.DownloadFile(url, fullPath);
                SLDocument sl = new SLDocument(fullPath);

                //clear tempFile
                var fileP = new FileInfo(fullPath);
                fileP.Delete();

                List<FilePeople> dataPeople = new List<FilePeople>();

                //reading
                int iRow = 2;
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                {
                    string email = sl.GetCellValueAsString(iRow, 1);
                    bool valid = Commun.IsValidEmail(email);
                    if (valid) { dataPeople.Add(new FilePeople() { Email = email, Name = "---", Valid = true }); }
                    else { dataPeople.Add(new FilePeople() { Email = email, Name = "---", Valid = false }); }
                    iRow++;
                }
                //----------------------------- set people excel -----------------------------------

                //dowload files
                List<FileSendModel> filesArrayx = new List<FileSendModel>();

                var filesDb = repository.filesByECamp(code);
                foreach (EmailCampaingDTO item in filesDb)
                {
                    
                    Random random = new Random();
                    string uriFile = item.UrlArchivosAdjunto;
                    WebClient webClient = new WebClient();


                    string extension = System.IO.Path.GetExtension(uriFile);
                    string name = random.Next().ToString() + extension;
                    webClient.DownloadFile(uriFile, path + $"{name}");

                    byte[] bytes = System.IO.File.ReadAllBytes(path + $"{name}");

                    HttpPostedFileBase fil = (HttpPostedFileBase)new MemoryPostedFile(bytes, path + $"{name}", name);
                    filesArrayx.Add(new FileSendModel() { Name = name, Origin = fil.FileName });
                }

                int bccTotal = int.Parse(ConfigurationManager.AppSettings["BCCTOTAL"]);
                if (dataPeople.Count > bccTotal)
                {
                    var newList = HomeHelper.SplitList(dataPeople, bccTotal);
                    for (int i = 0; i < newList.Count; i++)
                    {
                        response = hrepo.SendEmailMassiveRepo(newList[i], "", data.Content_html, filesArrayx, path);
                    }
                }
                else
                {
                    //send
                    response = hrepo.SendEmailMassiveRepo(dataPeople, "", data.Content_html, filesArrayx, path);
                }

                //remove files
                foreach (FileSendModel a in filesArrayx)
                {
                    Commun.removeFile(path + a.Name);
                }


                if (!response.Success)
                {

                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                //update senCorreo
                EmailCampaingRepository ecrepo = new EmailCampaingRepository();
                var upSendc = ecrepo.UpdateSendCorreoECampainRepo(code);
                if (upSendc)
                {
                    //register detail -- emails
                    foreach (FilePeople item in dataPeople)
                    {
                        var regDetail = ecrepo.RegisterECampainDetailRepo(CodigoCorreoCampania: code, Destinatario: item.Email, EstadoCorreoCampania: item.Valid);
                    }
                    var totalValids = dataPeople.Where(itemx => itemx.Valid == true);
                    response.Message1 = $"Enviado correntamente a {totalValids.Count()} correos";
                    response.Success = true;
                }

                else
                {
                    response.Message1 = "Error en el proceso";
                    response.Success = false;
                }

            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }




        //get files by email campaign
        public ActionResult AllFiles(string code)
        {
            List<EmailCampaingDTO> lista = new List<EmailCampaingDTO>();

            EmailCampaingDTO oDto = new EmailCampaingDTO();
            oDto.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oDto.CodigoSede = Commun.CodigoSede;
            oDto.CodigoCorreoCampania = code;

            ReqFilterEmailCampaingDTO oReq = new ReqFilterEmailCampaingDTO()

            {
                FilterCase = FilterEmailCampaing.ListFiles,
                Item = oDto,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListEmailCampaingDTO oResp = null;

            EmailCampaingLogic oLogic = new EmailCampaingLogic();
            oResp = oLogic.GetList(oReq);

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        //************************************ DETAILS *********************************************

        public ActionResult ListCampaignDetails(string code, int PageNumber = 1)
        {
            List<EmailCampaingDTO> lista = new List<EmailCampaingDTO>();

            EmailCampaingDTO oModel = new EmailCampaingDTO();
            oModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oModel.CodigoSede = Commun.CodigoSede;
            oModel.CodigoCorreoCampania = code;

            ReqFilterEmailCampaingDTO oReq = new ReqFilterEmailCampaingDTO()

            {
                FilterCase = FilterEmailCampaing.ListPaginationDetail,
                Item = oModel,
                User = Commun.Usuario,

                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListEmailCampaingDTO oResp = null;
            EmailCampaingLogic oLogic = new EmailCampaingLogic();
            oResp = oLogic.GetList(oReq);
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }



        //************************************ DETAILS *********************************************

    }

    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] FileBytes;
        private string FilePath;

        public MemoryPostedFile(byte[] fileBytes, string path, string fileName = null)
        {
            this.FilePath = path;
            this.FileBytes = fileBytes;
            this._FileName = fileName;
            this._Stream = new MemoryStream(fileBytes);
        }

        public override int ContentLength { get { return FileBytes.Length; } }
        public override String FileName { get { return _FileName; } }
        private String _FileName;
        public override Stream InputStream
        {
            get
            {
                if (_Stream == null)
                {
                    _Stream = new FileStream(_FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                return _Stream;
            }
        }
        private Stream _Stream;
        public override void SaveAs(string filename)
        {
            System.IO.File.WriteAllBytes(filename, System.IO.File.ReadAllBytes(FilePath));
        }
    }

}