using BotComers.Helpers;
using BotComers.Repository.CentroEntrenamiento;
using BotComers.ViewModels.CentroEntrenamiento;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class reservapresencialController : Controller
    {

        public ActionResult Configuracion()
        {
            return View();
        }

        public ActionResult ConfiguracionM()
        {
            return View();
        }

        //SALA FITNESS O MAQUINAS

        public ActionResult uspListarPresencial_ConfiguracionSalaFitness(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_ConfiguracionSalaFitness(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspRegistrarPresencial_ConfiguracionSalaFitness(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspRegistrarPresencial_ConfiguracionSalaFitness(request), JsonRequestBehavior.AllowGet);
            }
        }

        //LISTAR LOS HORARIOS DE LA SALA DE MAQUINAS QUE HAN SIDO RESERVADAS DESDE HOY PARA ADELANTE
        public ActionResult uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE(int CodigoSala)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoSala = CodigoSala;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspEliminarPresencial_SalaMaquinas_HorarioTemporal(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspEliminarPresencial_SalaMaquinas_HorarioTemporal(request), JsonRequestBehavior.AllowGet);
            }
        }


        //SALA
        public ActionResult uspListarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarSala_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspRegistrarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspRegistrarSala_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspEditarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspEditarSala_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspEliminarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspEliminarSala_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        //SALA DISCIPLINA
        public ActionResult uspListarDisciplinaSala_Presencial(CentroEntrenamiento_Presencial_DisciplinaSalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_DisciplinaSalaRepository oRepository = new CentroEntrenamiento_Presencial_DisciplinaSalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarDisciplinaSala_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspRegistrarDisciplinaSala_Presencial(CentroEntrenamiento_Presencial_DisciplinaSalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_DisciplinaSalaRepository oRepository = new CentroEntrenamiento_Presencial_DisciplinaSalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspRegistrarDisciplinaSala_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        //PROFESOR

        public ActionResult uspRegistrarProfesor(CentroEntrenamiento_ProfesorDTO request)
        {
            using (CentroEntrenamiento_ProfesorRepository oRepository = new CentroEntrenamiento_ProfesorRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspRegistrarProfesor(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspBuscarProfesorPorNombres(CentroEntrenamiento_ProfesorDTO request)
        {
            using (CentroEntrenamiento_ProfesorRepository oRepository = new CentroEntrenamiento_ProfesorRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspBuscarProfesorPorNombres(request), JsonRequestBehavior.AllowGet);
            }
        }

        //CALENDARIO
        public ActionResult uspListarPresencial_HorarioClasesConfiguracionCalendario(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspRegistrarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspRegistrarPresencial_HorarioClasesConfiguracion(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspDesactivarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspDesactivarPresencial_HorarioClasesConfiguracion(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS(request), JsonRequestBehavior.AllowGet);
            }
        }

        //BUSCAR CONFIGURACION
        public ActionResult CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion(request), JsonRequestBehavior.AllowGet);
            }
        }

        //ACTIVAR CONFIGURACION
        public ActionResult CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar(request), JsonRequestBehavior.AllowGet);
            }
        }

        //DESACTIVAR CONFIGURACION
        public ActionResult CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar(request), JsonRequestBehavior.AllowGet);
            }
        }

        //CAMBIAR AFORO CONFIGURACION
        public ActionResult CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerLogoColorEmpresa()
        {
            using (CentroEntrenamiento_EditorPaginaWebRepository oRepositoryPG = new CentroEntrenamiento_EditorPaginaWebRepository())
            {
                CentroEntrenamiento_EditorPaginaWebDTO requestPG = new CentroEntrenamiento_EditorPaginaWebDTO();
                requestPG.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                requestPG.CodigoSede = Commun.CodigoSede;
                requestPG.UsuarioCreacion = "appsfit";
                requestPG = oRepositoryPG.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(requestPG);

                return Json(requestPG, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GuardarConfiguracion_logo(CentroEntrenamiento_ConfiguracionViewModel request)
        {

            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;

            var file2 = request.file;
            string ruta;
            request.UrlUmagen = string.Empty;
            if (file2 != null)
            {
                var fileName = Path.GetFileName(file2.FileName);
                var extention = Path.GetExtension(file2.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                ruta = UploadImgageAzure.UploadFilesAzure(obj, (request.CodigoUnidadNegocio.ToString() + request.CodigoSede.ToString() + extention), "empresas");
                request.UrlUmagen = ruta;
            }


            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = request.CodigoSede;
            oConfiguracionDTO.Logo = request.UrlUmagen;

            oConfiguracionDTO.Operation = Operation.uspActualizarConfiguracionLogo_adFitness;
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
            lista.Add(oConfiguracionDTO);

            ReqConfiguracionDTO oReq = new ReqConfiguracionDTO()
            {
                List = lista,
                User = "appsfit"
            };
            RespConfiguracionDTO oResp = null;

            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ExecuteTransac(oReq);

            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            using (CentroEntrenamiento_EditorPaginaWebRepository oRepository = new CentroEntrenamiento_EditorPaginaWebRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina(request), JsonRequestBehavior.AllowGet);
            }
        }

    }
}