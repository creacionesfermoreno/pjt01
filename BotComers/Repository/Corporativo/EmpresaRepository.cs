using BotComers.Helpers;
using BotComers.ViewModels;
using E_BusinessLayer;
using E_BusinessLayer.Corporativo;
using E_DataModel;
using E_DataModel.Common;
using E_DataModel.Corporativo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;

namespace BotComers.Repository.Corporativo
{
    public class EmpresaRepository : IDisposable
    {
        public List<EmpresaViewModelGrid> ecommerce_uspListarEmpresas_Paginacion(int PageNumber)
        {
            List<EmpresaViewModelGrid> lista = null;

            ReqFilterEmpresaDTO oReq = new ReqFilterEmpresaDTO()
            {
                FilterCase = filterCaseEmpresa.ecommerce_uspListarEmpresas_Paginacion,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListEmpresaDTO oResp = null;

            using (EmpresaLogic oEmpresaLogic = new EmpresaLogic())
            {
                oResp = oEmpresaLogic.EmpresaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<EmpresaViewModelGrid>();
                foreach (EmpresaDTO item in oResp.List)
                {
                    lista.Add(new EmpresaViewModelGrid()
                    {
                        CodigoUnidadNegocio = Convert.ToInt32(item.CodigoUnidadNegocio),
                        CodigoSede = Convert.ToInt32(item.CodigoSede),
                        DesTipoDocumentoEmpresa = item.DesTipoDocumentoEmpresa,
                        NroDocumentoEmpresa = item.NroDocumentoEmpresa,
                        RazonSocialEmpresa = item.RazonSocialEmpresa,
                        NombreComercialEmpresa = item.NombreComercialEmpresa,
                        TelefonoEmpresa = item.TelefonoEmpresa,
                        CorreoEmpresa = item.CorreoEmpresa,
                        SubDominio = item.SubDominio,
                        LogoTipo = item.LogoTipo,
                        DesEstado = item.DesEstado
                    });
                }
            }

            return lista;

        }

        public EmpresaViewEditModel ecommerce_uspBuscarEmpresas(int CodigoUnidadNegocio, int CodigoSede)
        {
            EmpresaViewEditModel oItemViewModel = null;

            EmpresaDTO oEmpresaDTO = new EmpresaDTO();
            oEmpresaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oEmpresaDTO.CodigoSede = CodigoSede;

            ReqFilterEmpresaDTO oReq = new ReqFilterEmpresaDTO()
            {
                FilterCase = filterCaseEmpresa.ecommerce_uspBuscarEmpresas,
                Item = oEmpresaDTO,
                User = "admin"
            };
            RespItemEmpresaDTO oResp = null;
            using (EmpresaLogic oEmpresaLogic = new EmpresaLogic())
            {
                oResp = oEmpresaLogic.EmpresaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new EmpresaViewEditModel();
                oItemViewModel.CodigoUnidadNegocio = oResp.Item.CodigoUnidadNegocio;
                oItemViewModel.CodigoSede = oResp.Item.CodigoSede;
                oItemViewModel.NombreDuenio = oResp.Item.NombreDuenio;
                oItemViewModel.ApellidosDuenio = oResp.Item.ApellidosDuenio;
                oItemViewModel.CorreoDuenio = oResp.Item.CorreoDuenio;
                oItemViewModel.CodigoPais = oResp.Item.CodigoPais;
                oItemViewModel.CelularDuenio = oResp.Item.CelularDuenio;
                oItemViewModel.TipoDocumentoEmpresa = oResp.Item.TipoDocumentoEmpresa;
                oItemViewModel.NroDocumentoEmpresa = oResp.Item.NroDocumentoEmpresa;
                oItemViewModel.RazonSocialEmpresa = oResp.Item.RazonSocialEmpresa;
                oItemViewModel.DireccionEmpresa = oResp.Item.DireccionEmpresa;
                oItemViewModel.NombreComercialEmpresa = oResp.Item.NombreComercialEmpresa;
                oItemViewModel.TelefonoEmpresa = oResp.Item.TelefonoEmpresa;
                oItemViewModel.FechaAniversarioEmpresa = oResp.Item.FechaAniversarioEmpresa;
                oItemViewModel.CorreoEmpresa = oResp.Item.CorreoEmpresa;
                oItemViewModel.SubDominio = oResp.Item.SubDominio;
                oItemViewModel.LogoTipo = oResp.Item.LogoTipo;
                oItemViewModel.Estado = oResp.Item.Estado;
                oItemViewModel.UsuarioCreacion = oResp.Item.UsuarioCreacion;
                oItemViewModel.IdEmpresa = oResp.Item.IdEmpresa;
            }

            return oItemViewModel;

        }

        public List<EmpresaViewEditModel> ecommerce_uspObtenerEmpresaPorDominio(string SubDominio)
        {
            List<EmpresaViewEditModel> oItemViewModel = null;

            EmpresaDTO oEmpresaDTO = new EmpresaDTO();
            oEmpresaDTO.SubDominio = SubDominio;

            ReqFilterEmpresaDTO oReq = new ReqFilterEmpresaDTO()
            {
                FilterCase = filterCaseEmpresa.ecommerce_uspObtenerEmpresaPorDominio,
                Item = oEmpresaDTO,
                User = "appsfit",
                Paging = new Paging()
                {
                    All = true,
                    PageRecords = 5,
                    PageNumber = 1
                }
            };
            RespListEmpresaDTO oResp = new RespListEmpresaDTO();
            using (EmpresaLogic oEmpresaLogic = new EmpresaLogic())
            {
                oResp = oEmpresaLogic.EmpresaGetList(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new List<EmpresaViewEditModel>();

                foreach (EmpresaDTO item in oResp.List)
                {
                    oItemViewModel.Add(new EmpresaViewEditModel()
                    {
                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                        CodigoSede = item.CodigoSede,
                        SubDominio = item.SubDominio,
                        NombreComercialEmpresa = item.NombreComercialEmpresa,
                        LogoTipo = item.LogoTipo,
                        ColorEmpresa = item.ColorEmpresa,
                        Estado = item.Estado
                    });
                }

            }

            return oItemViewModel;
        }

        public List<MaestroViewModel> ecommerce_uspListarMaestro(string Filter)
        {
            List<MaestroViewModel> lista = null;
            List<MaestroDTO> listMaestroDTO = new List<MaestroDTO>();

            using (MaestroLogic oMaestroLogic = new MaestroLogic())
            {
                listMaestroDTO = oMaestroLogic.ecommerce_uspListarMaestro(new MaestroDTO() { Filter = Filter });
            }

            lista = new List<MaestroViewModel>();
            foreach (MaestroDTO item in listMaestroDTO)
            {
                lista.Add(new MaestroViewModel()
                {
                    Codigo = item.Codigo,
                    Descripcion = item.Descripcion,
                    valor = item.valor,
                    urlImagen = item.urlImagen
                });
            }

            return lista;

        }

        public string ecommerce_uspRegistrarEmpresa(EmpresaViewInsertModel oItem)
        {
            string mensaje = string.Empty;
            string ruta = string.Empty;

            if (oItem.Accion != "")
            {
                var file = oItem.ImageFileLogo;

                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var extention = Path.GetExtension(file.FileName);
                    var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);

                    var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                    var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file.FileName, file.ContentType, file.InputStream });

                    ruta = UploadImgageAzure.UploadFilesAzure(obj, (oItem.IdEmpresa + extention), "empresas");
                }
            }

            List<EmpresaDTO> list = new List<EmpresaDTO>();

            list.Add(new EmpresaDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                NombreDuenio = oItem.NombreDuenio,
                ApellidosDuenio = oItem.ApellidosDuenio,
                CorreoDuenio = oItem.CorreoDuenio,
                CodigoPais = oItem.CodigoPais,
                CelularDuenio = oItem.CelularDuenio,
                TipoDocumentoEmpresa = oItem.TipoDocumentoEmpresa,
                NroDocumentoEmpresa = oItem.NroDocumentoEmpresa,
                RazonSocialEmpresa = oItem.RazonSocialEmpresa,
                DireccionEmpresa = oItem.DireccionEmpresa,
                NombreComercialEmpresa = oItem.NombreComercialEmpresa,
                TelefonoEmpresa = oItem.TelefonoEmpresa,

                FechaAniversarioEmpresa = DateTime.Now,
                CorreoEmpresa = oItem.CorreoEmpresa,
                SubDominio = oItem.SubDominio,
                LogoTipo = ruta,
                ColorEmpresa = oItem.ColorEmpresa,
                Estado = oItem.Estado,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,
            });



            ReqEmpresaDTO oReq = new ReqEmpresaDTO()
            {
                List = list,
                User = "admin"
            };
            RespEmpresaDTO oResp = null;
            using (EmpresaLogic oEmpresaLogic = new EmpresaLogic())
            {
                oResp = oEmpresaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";

            }

            return mensaje;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}