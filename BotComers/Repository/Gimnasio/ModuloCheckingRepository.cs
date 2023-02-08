using BotComers.Helpers;
using E_BusinessLayer.CentroEntrenamiento;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.UI.WebControls;
using System.Xml;

namespace BotComers.Repository.Gimnasio
{
    public class ModuloCheckingRepository : IDisposable
    {
        //no controller
        public int uspEnviarSocioANuevo(int CodigoSocio, string Vendedor, int CodSede, int CodigoUnidadNegocio, string tk, string latitud, string longitud, string UsuarioCreacion)
        {


            int mensaje = 0;
            List<ClientesDTO> list = new List<ClientesDTO>();
            list.Add(new ClientesDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,
                UsuarioCreacion = UsuarioCreacion,
                CodigoSocio = CodigoSocio,
                Vendedor = Vendedor,
                CodigoSede = CodSede,
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                Operation = Operation.uspEnviarSocioANuevo
            });
            ReqClientesDTO oReq = new ReqClientesDTO()
            {
                List = list,
                User = "Admin"
            };
            RespClientesDTO oResp = null;
            using (ClientesLogic oProspectosLogic = new ClientesLogic())
            {
                oResp = oProspectosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return mensaje;
        }
        //no controller
        public List<UsuarioDTO> ListarAsesoresComercialesEnviarANuevo(int CodSede, int CodigoUnidadNegocio)
        {


            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;

            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuario_AgendaComercial,
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public List<ClientesDTO> ListaSocios(string valor, int flag, int CodSede, int CodigoUnidadNegocio)
        {


            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.Genero = "";
            oClientesDTO.Nombres = valor;
            oClientesDTO.flag = flag;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.UserAsesorVenta = "";
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;

            if (valor != string.Empty)
            {
                ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
                {
                    Item = oClientesDTO,
                    User = "Admin",
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageRecords = 0
                    }
                };

                RespListClientesDTO oResp = null;

                using (ClientesLogic oSociosLogic = new ClientesLogic())
                {
                    oResp = oSociosLogic.ClientesGetList(oReq);
                }

                if (oResp.Success)
                {
                    lista = new List<ClientesDTO>();
                    lista = oResp.List;
                }
            }

            return lista;

        }

        //SI SE USA ESTA TABLA ES ESTANDAR

        public List<PerfilMenuDTO> ListarPerfilMenu(int CodigoUnidadNegocio, string Perfil, string User)
        {


            List<PerfilMenuDTO> oListPerfilMenuDTO = null;
            UsuarioDTO item = new UsuarioDTO();
            PerfilMenuDTO oPerfilMenuDTO = new PerfilMenuDTO();
            oPerfilMenuDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPerfilMenuDTO.CodigoPerfil = Convert.ToInt32(Perfil);
            ReqFilterPerfilMenuDTO oReq = new ReqFilterPerfilMenuDTO()
            {
                Item = oPerfilMenuDTO,
                User = User,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListPerfilMenuDTO oResp = null;

            using (PerfilMenuLogic oPerfilMenuLogic = new PerfilMenuLogic())
            {
                oResp = oPerfilMenuLogic.PerfilMenuGetList(oReq);
            }

            if (oResp.Success)
            {
                oListPerfilMenuDTO = oResp.List;
            }

            return oListPerfilMenuDTO;
        }

        //SI SE USA
        protected void btnGuardarFotoSocio_Click(object sender, EventArgs e)
        {
            //

            //string User = HttpContext.Current.Request.QueryString["User"].ToString();
            //int CodigoUnidadNegocio = Convert.ToInt32(HttpContext.Current.Request.QueryString["un"]);
            //string CarpSocios = HttpContext.Current.Request.QueryString["Carp"];
            //string ruta; //= string.Empty;
            //if (FileUpload1.HasFile)
            //{
            //    //ruta = "../Imagenes/Socios/" + CarpSocios + "/" + hdCodigo.Value + FileUpload1.FileName;
            //    HttpPostedFile file = FileUpload1.PostedFile;
            //    ruta = UploadImgageAzure.UploadFilesAzure(file, hdCodigo.Value + FileUpload1.FileName, CarpSocios);

            //    //FileUpload1.SaveAs(Server.MapPath(ruta));
            //}
            //else
            //{
            //    ruta = hdURLImagenSubir.Value;

            //    if (ruta.Contains("PerfilHombre.png"))
            //    {
            //        ruta = "../Imagenes/fitness/PerfilHombre.png";

            //    }
            //    if (ruta.Contains("PerfilMujer.png"))
            //    {
            //        ruta = "../Imagenes/fitness/PerfilMujer.png";
            //    }
            //    else
            //    {
            //        ruta = hdURLImagenSubir.Value;
            //    }
            //}

            //List<ClientesDTO> list = new List<ClientesDTO>();

            //list.Add(new ClientesDTO()
            //{
            //    CodigoUnidadNegocio = CodigoUnidadNegocio,
            //    CodigoSocio = hdCodigo.Value == String.Empty ? 0 : Convert.ToInt32(hdCodigo.Value),
            //    ImagenUrl = ruta,
            //    Operation = Operation.UpdateFoto
            //});

            //ReqClientesDTO oReq = new ReqClientesDTO()
            //{
            //    List = list,
            //    User = User
            //};

            //RespClientesDTO oResp = null;
            //using (ClientesLogic oProductoLogic = new ClientesLogic())
            //{
            //    oResp = oProductoLogic.ExecuteTransac(oReq);
            //}

            //if (oResp.Success)
            //{

            //}

        }

        //SI SE USA

        public int ValidarBuscarDiasHorarioPaquete(int CodigoUnidadNegocio, int CodigoPaquete)
        {
            int existe = 0;

            using (PlanesLogic oSociosLogic = new PlanesLogic())
            {
                existe = oSociosLogic.ValidarBuscarDiasHorarioPaquete(CodigoUnidadNegocio, CodigoPaquete);
            }

            return existe;
        }
        //SI SE USA

        public ContratoDTO ValidarIngresoDiaPaquete(int CodigoUnidadNegocio, int CodigoSede, int CodigoMenbresia, string User)
        {


            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoMenbresia = CodigoMenbresia;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.ValidarIngresoDiaPaquete,
                Item = oContratoDTO,
                User = User
            };
            RespItemContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
            }

            return oContratoDTO;

        }
        //SI SE USA        
        public ClientesDTO BuscarInformacionSociosPorCodigo(int CodigoUnidadNegocio, int CodigoSede, int codigo, string User)
        {


            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoSocio = codigo;
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodigoSede;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.BuscarInfoPorCodSocio,
                Item = oClientesDTO,
                User = User
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            return oClientesDTO;
        }

        public ClientesDTO BuscarInformacionSociosPorCodigoFiltro(int CodigoUnidadNegocio, int CodigoSede, string Filtro, string User)
        {


            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.Filtro = Filtro;
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodigoSede;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.BuscarInfoPorCodSocioFiltro,
                Item = oClientesDTO,
                User = User
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            if (oClientesDTO == null)
            {
                oClientesDTO = new ClientesDTO();
                oClientesDTO.CodigoSocio = 0;
            }

            return oClientesDTO;
        }
        public ClientesDTO CentroEntrenamiento_uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo(int CodigoUnidadNegocio, int CodigoSede, string Correo, string User)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.Correo = Correo;
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodigoSede;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.CentroEntrenamiento_uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo,
                Item = oClientesDTO,
                User = User
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;

            }

            return oClientesDTO;
        }

        public List<PagosContratoDTO> ListarHistorialPagos(int CodigoUnidadNegocio, int CodigoSede, int codMembresia)
        {
            List<PagosContratoDTO> lista = null;
            PagosContratoDTO oPagoMembresiaDTO = new PagosContratoDTO();
            oPagoMembresiaDTO.CodigoMembresia = codMembresia;
            oPagoMembresiaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPagoMembresiaDTO.CodigoSede = CodigoSede;

            ReqFilterPagosContratoDTO oReq = new ReqFilterPagosContratoDTO()
            {
                FilterCase = filterCasePagosContrato.ListarPagosFormaPago,
                User = "Admin",
                Item = oPagoMembresiaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListPagosContratoDTO oResp = null;
            using (PagosContratoLogic oPagoMembresiaLogic = new PagosContratoLogic())
            {
                oResp = oPagoMembresiaLogic.PagosContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.OrderByDescending(x => x.FechaNewPago).ToList();
            }
            return lista;

        }

        public List<UsuarioDTO> listardllAsesoresVentas(int CodigoUnidadNegocio, int CodSede)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;

            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.ListarAsesoresVentasAcuentaVentas,
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.ToList();
            }

            return lista;
        }

        public int EliminarAsistencia(string CodigoHorarioClasesConfiguracionAsistencias, int CodigoUnidadNegocio, int CodigoAsistencia, int CodigoMenbresia, int CodSede, string User, string tk, string latitud, string longitud)
        {
            int mensaje = 0;
            List<AsistenciaDTO> list = new List<AsistenciaDTO>();
            list.Add(new AsistenciaDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoSede = CodSede,
                CodigoAsistencia = CodigoAsistencia,
                CodigoMembresiaReal = CodigoMenbresia,
                CodigoHorarioClasesConfiguracionAsistencias = CodigoHorarioClasesConfiguracionAsistencias,
                Operation = Operation.DeleteAsistencia,
                UsuarioCreacion = User
            });
            ReqAsistenciaDTO oReq = new ReqAsistenciaDTO()
            {
                List = list,
                User = "Admin"
            };
            RespAsistenciaDTO oResp = null;
            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;

        }

        public void EliminarAsistenciaInvitado(int CodigoUnidadNegocio, int CodigoSede, int CodigoAsistenciaI, int CodigoInvitado)
        {
            string mensaje = string.Empty;
            List<AsistenciaInvitadosDTO> list = new List<AsistenciaInvitadosDTO>();
            list.Add(new AsistenciaInvitadosDTO()
            {
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoSede = CodigoSede,
                CodigoAsistenciaI = CodigoAsistenciaI,
                CodigoInvitado = CodigoInvitado,
                Operation = Operation.Delete
            });
            ReqAsistenciaInvitadosDTO oReq = new ReqAsistenciaInvitadosDTO()
            {
                List = list,
                User = "Admin"
            };
            RespAsistenciaInvitadosDTO oResp = null;
            using (AsistenciaInvitadosLogic oAsistenciaInvitadosLogic = new AsistenciaInvitadosLogic())
            {
                oResp = oAsistenciaInvitadosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {

            }

        }

        public TipoAgendaClienteDTO uspBuscarTipoAgendaClientePorCodigo(int Codigo)
        {


            TipoAgendaClienteDTO oTipoAgendaClienteDTO = new TipoAgendaClienteDTO();
            oTipoAgendaClienteDTO.Codigo = Codigo;

            ReqFilterTipoAgendaClienteDTO oReq = new ReqFilterTipoAgendaClienteDTO()
            {
                FilterCase = filterCaseTipoAgendaCliente.Filter_uspBuscarTipoAgendaClientePorCodigo,
                Item = oTipoAgendaClienteDTO,
                User = "Admin"
            };

            RespItemTipoAgendaClienteDTO oResp = null;
            using (TipoAgendaClienteLogic oTipoAgendaClienteLogic = new TipoAgendaClienteLogic())
            {
                oResp = oTipoAgendaClienteLogic.TipoAgendaClienteGetItem(oReq);
            }

            if (oResp.Success)
            {
                oTipoAgendaClienteDTO = oResp.Item;
            }

            return oTipoAgendaClienteDTO;
        }

        public List<ContratoDTO> ListarMembresias(int CodigoUnidadNegocio, int CodigoSede, int codSocio)
        {
            List<ContratoDTO> lista = null;

            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoSocio = codSocio;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.ListarMembresiasSocios,
                User = "Admin",
                Item = oContratoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.Take(6).ToList();
            }

            return lista;

        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio(int CodigoUnidadNegocio, int CodigoSede, int codSocio, int CodigoMembresia)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> lista = null;

            CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oParametros = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO();
            oParametros.CodigoSocio = codSocio;
            oParametros.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oParametros.CodigoSede = CodigoSede;
            oParametros.CodigoMembresia = CodigoMembresia;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias.CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio,
                User = "appsfit",
                Item = oParametros,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oLogic.CentroEntrenamiento_Presencial_HorarioClasesAsistenciasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }


        public List<ContratoDTO> CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo(int CodigoUnidadNegocio, int CodigoSede, string correo)
        {
            List<ContratoDTO> lista = null;

            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.Correo = correo;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo,
                User = "Admin",
                Item = oContratoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.Take(6).ToList();
            }

            return lista;

        }

        public List<HistorialCongelamientoDTO> ListarHitorialFreezingPorMenbresia(int CodigoUnidadNegocio, int CodigoSede, int codigo)
        {


            List<HistorialCongelamientoDTO> lista = null;
            HistorialCongelamientoDTO oHistorialCongelamientoDTO = new HistorialCongelamientoDTO();
            oHistorialCongelamientoDTO.CodigoMembresia = codigo;
            oHistorialCongelamientoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oHistorialCongelamientoDTO.CodigoSede = CodigoSede;

            ReqFilterHistorialCongelamientoDTO oReq = new ReqFilterHistorialCongelamientoDTO()
            {
                Item = oHistorialCongelamientoDTO,
                FilterCase = filterCaseHistorialCongelamiento.ListarHitorialFreezingPorMenbresia,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListHistorialCongelamientoDTO oResp = null;

            using (HistorialCongelamientoLogic oHistorialCongelamientoLogic = new HistorialCongelamientoLogic())
            {
                oResp = oHistorialCongelamientoLogic.HistorialCongelamientoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public List<ContratoMensajeDTO> ListarMensajesMenbresia(int CodigoUnidadNegocio, int codigoMenbresia)
        {


            List<ContratoMensajeDTO> lista = null;
            ContratoMensajeDTO oContratoMensajeDTO = new ContratoMensajeDTO();
            oContratoMensajeDTO.CodigoMenbresia = codigoMenbresia;
            oContratoMensajeDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;

            ReqFilterContratoMensajeDTO oReq = new ReqFilterContratoMensajeDTO()
            {
                Item = oContratoMensajeDTO,
                FilterCase = filterCaseContratoMensaje.ListarMensajesMenbresia,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListContratoMensajeDTO oResp = null;

            using (ContratoMensajeLogic oMensajesMenbresiaLogic = new ContratoMensajeLogic())
            {
                oResp = oMensajesMenbresiaLogic.ContratoMensajeGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public string GuardarConfiguracionFreezing(int CodigoUnidadNegocio, int CodSede, int flag, int nro)
        {


            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oConfiguracionDTO.Codigo = CodSede;
            oConfiguracionDTO.DescontarFreezingDisponiblesFlag = flag;
            oConfiguracionDTO.DescontarFreezingDisponiblesNumero = nro;
            oConfiguracionDTO.UsuarioEdicion = "Admin";
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.UpdateConfiguracionFreezing;

            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
            lista.Add(oConfiguracionDTO);

            ReqConfiguracionDTO oReq = new ReqConfiguracionDTO()
            {
                List = lista,
                User = "Admin"
            };
            RespConfiguracionDTO oResp = null;

            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ExecuteTransac(oReq);

            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;

        }

        public int GuardarMembresiaCongelamiento(int CodigoUnidadNegocio, int codigo, DateTime fechaInicio, DateTime fechaFin, int FrezenDisponibles, int NroDiasCongelar, DateTime fechaFreziing, DateTime fechaDesFreziing, int CodSede, int CodigoSocio, int NroDias, string NroSolicitud, string Motivo, string User)
        {
            string mensaje = string.Empty;
            int codigoMembresia = 0;
            int EstadoMembresia = 0;

            if (fechaFreziing > DateTime.Now)
            {
                EstadoMembresia = 1; //estado activo y futura congelacion programada y pasara a estado 0
            }
            else
            {
                EstadoMembresia = 0; //estado congelado
            }


            DateTime fechaCongelacionProgramada = DateTime.Now;
            fechaCongelacionProgramada = fechaCongelacionProgramada.AddDays(-1);

            DateTime fechaDesCongelacion;

            fechaCongelacionProgramada = fechaFreziing;

            if (fechaFreziing == null)
            {
                fechaDesCongelacion = fechaDesFreziing;// DateTime.Now.AddDays(NroDiasCongelar);
            }
            else
            {
                fechaDesCongelacion = fechaDesFreziing;// Convert.ToDateTime(fechaFreziing).AddDays(NroDiasCongelar);
            }

            List<ContratoDTO> list = new List<ContratoDTO>();

            list.Add(new ContratoDTO()
            {
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoMenbresia = codigo,
                FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day),
                FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day),
                FrezenDisponibles = FrezenDisponibles,
                FechaCongelacionProgramada = fechaCongelacionProgramada,
                FechaDesCongelacion = fechaDesCongelacion,
                CodigoSede = CodSede,
                Estado = EstadoMembresia,
                CodigoSocio = CodigoSocio,
                NroDias = NroDias,
                NroSolicitud = NroSolicitud,
                Motivo = Motivo,
                UsuarioCreacion = User,
                Operation = Operation.UpdateMembresiaFreezing
            });

            ReqContratoDTO oReq = new ReqContratoDTO()
            {
                List = list,
                User = User
            };

            RespContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "";
                codigoMembresia = oResp.MessageList[0].Codigo;
            }

            return codigoMembresia;
        }

        public int GuardarMensaje(int CodigoUnidadNegocio, int CodigoSocio, int codigoMembresia, string Mensaje, string User, string tk, string latitud, string longitud, int CodSede)
        {

            int Codigo = 0;
            List<ContratoMensajeDTO> list = new List<ContratoMensajeDTO>();
            list.Add(new ContratoMensajeDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,

                CodigoUnidadNegocio = CodigoUnidadNegocio,
                Codigo = 0,
                CodigoSocio = CodigoSocio,
                CodigoMenbresia = codigoMembresia,
                CodigoSede = CodSede,
                Ocurrencia = Mensaje,
                UsuarioCreacion = User,
                Operation = E_DataModel.Common.Operation.Create,
            });

            ReqContratoMensajeDTO oReq = new ReqContratoMensajeDTO()
            {
                List = list,
                User = "Admin"
            };
            RespContratoMensajeDTO oResp = null;
            using (ContratoMensajeLogic oMensajesMenbresiaLogic = new ContratoMensajeLogic())
            {
                oResp = oMensajesMenbresiaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Codigo;
        }

        //no se usa        
        public PlanesDTO BuscarCantidadCupoPaquetesPorCodigo(int CodigoUnidadNegocio, int CodigoSede, int codigo)
        {
            PlanesDTO oPaquetesDTO = new PlanesDTO();
            oPaquetesDTO.CodigoPaquete = codigo;
            oPaquetesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPaquetesDTO.CodigoSede = CodigoSede;

            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                FilterCase = filterCasePlanes.BuscarCantidadCupoPaquetesPorCodigo,
                Item = oPaquetesDTO,
                User = "Admin"
            };
            RespItemPlanesDTO oResp = null;
            using (PlanesLogic oCargoLogic = new PlanesLogic())
            {
                oResp = oCargoLogic.PlanesGetItem(oReq);
            }
            if (oResp.Success)
            {
                oPaquetesDTO = oResp.Item;
            }

            return oPaquetesDTO;

        }

        // no se usa
        public ConfiguracionDTO buscarConfiguracionCorreoBienvenida(int CodigoUnidadNegocio, int CodSede)
        {

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoSede = CodSede;
            oConfiguracionDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = "Admin",
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.filter_uspBuscarConfCorreoBienvenidaPorCodigo
            };

            RespItemConfiguracionDTO oResp = null;
            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);

            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }

            return oConfiguracionDTO;
        }

        //no se usa        
        public List<HorarioPaqueteDetalleDTO> uspListarHorasCurso(int CodigoUnidadNegocio, int CodigoPaquete, int dia)
        {
            List<HorarioPaqueteDetalleDTO> lista = null;
            HorarioPaqueteDetalleDTO oHorarioPaqueteDetalleDTO = new HorarioPaqueteDetalleDTO();
            oHorarioPaqueteDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oHorarioPaqueteDetalleDTO.CodigoPaquete = CodigoPaquete;
            oHorarioPaqueteDetalleDTO.Dia = dia;
            ReqFilterHorarioPaqueteDetalleDTO oReq = new ReqFilterHorarioPaqueteDetalleDTO()
            {
                FilterCase = filterCaseHorarioPaqueteDetalle.uspListarHorasCurso,
                User = "Admin",
                Item = oHorarioPaqueteDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListHorarioPaqueteDetalleDTO oResp = null;
            using (HorarioPaqueteDetalleLogic oHorarioPaqueteDetalleLogic = new HorarioPaqueteDetalleLogic())
            {
                oResp = oHorarioPaqueteDetalleLogic.HorarioPaqueteDetalleGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public List<ProductoDTO> ListaProductos(int CodigoUnidadNegocio, string filtro, int codigoSocio, int CodSede)
        {
            List<ProductoDTO> lista = null;
            ProductoDTO oProductoDTO = new ProductoDTO();
            oProductoDTO.Descripcion = filtro;
            oProductoDTO.codigoSocio = codigoSocio;
            oProductoDTO.CodigoSede = CodSede;
            oProductoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            if (filtro != string.Empty)
            {
                ReqFilterProductoDTO oReq = new ReqFilterProductoDTO()
                {
                    Item = oProductoDTO,
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageRecords = 0
                    },
                    User = "Admin",
                    FilterCase = E_DataModel.Common.filterCaseProducto.ListaPorNombre
                };
                RespListProductoDTO oResp = null;
                using (ProductoLogic oProductoLogic = new ProductoLogic())
                {
                    oResp = oProductoLogic.ProductoGetList(oReq);
                }
                if (oResp.Success)
                {
                    lista = oResp.List;
                }
            }

            return lista;
        }
        //SI SE USA ESTA TABLA ES ESTANDAR

        public List<TipoDocumentoDTO> ListaTipoDocumentos()
        {


            List<TipoDocumentoDTO> lista = null;

            ReqFilterTipoDocumentoDTO oReq = new ReqFilterTipoDocumentoDTO()
            {
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListTipoDocumentoDTO oResp = null;

            using (TipoDocumentoLogic oTipoDocumentoLogic = new TipoDocumentoLogic())
            {
                oResp = oTipoDocumentoLogic.TipoDocumentoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<TipoDocumentoDTO>();
                lista = oResp.List.Where(x => x.codigo != 4).ToList();
            }

            return lista;
        }

        public List<PlanesDTO> uspListarTotalesPaquetesPorMes(int CodigoUnidadNegocio, int codSede, string fecha, int TipoPaquete)
        {


            List<PlanesDTO> lista = null;
            PlanesDTO oPaquetesDTO = new PlanesDTO();
            int anio = Convert.ToInt32(fecha.Split(' ')[1]);

            int mes = 0;

            if (fecha.Split(' ')[0].Trim() == "Enero")
            {
                mes = 1;
            }
            if (fecha.Split(' ')[0].Trim() == "Febrero")
            {
                mes = 2;
            }
            if (fecha.Split(' ')[0].Trim() == "Marzo")
            {
                mes = 3;
            }
            if (fecha.Split(' ')[0].Trim() == "Abril")
            {
                mes = 4;
            }
            if (fecha.Split(' ')[0].Trim() == "Mayo")
            {
                mes = 5;
            }
            if (fecha.Split(' ')[0].Trim() == "Junio")
            {
                mes = 6;
            }
            if (fecha.Split(' ')[0].Trim() == "Julio")
            {
                mes = 7;
            }
            if (fecha.Split(' ')[0].Trim() == "Agosto")
            {
                mes = 8;
            }
            if (fecha.Split(' ')[0].Trim() == "Septiembre")
            {
                mes = 9;
            }
            if (fecha.Split(' ')[0].Trim() == "Octubre")
            {
                mes = 10;
            }
            if (fecha.Split(' ')[0].Trim() == "Noviembre")
            {
                mes = 11;
            }
            if (fecha.Split(' ')[0].Trim() == "Diciembre")
            {
                mes = 12;
            }

            oPaquetesDTO.Anio = anio;
            oPaquetesDTO.Mes = mes;
            oPaquetesDTO.Dia = 0;
            oPaquetesDTO.CodigoSede = codSede;
            oPaquetesDTO.CodigoTipoPaquete = TipoPaquete;
            oPaquetesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;

            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                FilterCase = filterCasePlanes.uspListarTotalesPaquetesPorMes,
                User = "Admin",
                Item = oPaquetesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListPlanesDTO oResp = null;

            using (PlanesLogic oPaquetesLogic = new PlanesLogic())
            {
                oResp = oPaquetesLogic.PlanesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public string ActualizarNroIngresoMembresia(int codigoMembresia, int codigoSede, int codigoSocio, int CodigoUnidadNegocio, string UsuarioCreacion)
        {
            string Mensaje = "";
            ContratoDTO oContratoDTO = new ContratoDTO();

            oContratoDTO.CodigoSocio = codigoSocio;
            oContratoDTO.CodigoMenbresia = codigoMembresia;
            oContratoDTO.UsuarioCreacion = UsuarioCreacion;
            oContratoDTO.CodigoSede = codigoSede;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.NroIngreso = 0;
            oContratoDTO.Operation = Operation.UpdateMembresiaNroIngresos;

            List<ContratoDTO> list = new List<ContratoDTO>();
            list.Add(oContratoDTO);

            ReqContratoDTO oReq = new ReqContratoDTO()
            {
                User = "Admin",
                List = list
            };

            RespContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {
                Mensaje = (oResp.MessageList[0].Detalle).Split('/')[0];

            }
            return Mensaje;
        }

        public string uspActualizarAsistenciaInvitadoPorCodigoInvitado(int CodigoInvitado, int CodSede, int CodigoUnidadNegocio)
        {

            string mensaje = string.Empty;
            AsistenciaInvitadosDTO oAsistenciaInvitadosDTO = new AsistenciaInvitadosDTO();
            oAsistenciaInvitadosDTO.CodigoInvitado = CodigoInvitado;
            oAsistenciaInvitadosDTO.CodigoSede = CodSede;
            oAsistenciaInvitadosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaInvitadosDTO.Operation = Operation.uspActualizarAsistenciaInvitadoPorCodigoInvitado;
            oAsistenciaInvitadosDTO.UsuarioCreacion = "Admin";

            List<AsistenciaInvitadosDTO> list = new List<AsistenciaInvitadosDTO>();
            list.Add(oAsistenciaInvitadosDTO);

            ReqAsistenciaInvitadosDTO oReq = new ReqAsistenciaInvitadosDTO()
            {
                User = "Admin",
                List = list
            };

            RespAsistenciaInvitadosDTO oResp = null;

            using (AsistenciaInvitadosLogic oAsistenciaInvitadosLogic = new AsistenciaInvitadosLogic())
            {
                oResp = oAsistenciaInvitadosLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return mensaje;
        }

        public string GuardarAsistencia(int CodigoPersona, string TipoPersona, int CodigoPaquete, int CodigoMenbresia, string User, int CodSede, int CodigoUnidadNegocio)
        {


            string mensaje = string.Empty;
            List<AsistenciaDTO> list = new List<AsistenciaDTO>();
            list.Add(new AsistenciaDTO()
            {
                CodigoAsistencia = 0,
                CodigoPersona = CodigoPersona,
                CodigoPaquete = CodigoPaquete,
                CodigoMembresiaReal = CodigoMenbresia,
                TipoPersona = TipoPersona,
                UsuarioCreacion = User,
                UsuarioEdicion = User,
                CodigoSede = CodSede,
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                Operation = Operation.Create
            });
            ReqAsistenciaDTO oReq = new ReqAsistenciaDTO()
            {
                List = list,
                User = User
            };
            RespAsistenciaDTO oResp = null;
            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return mensaje;
        }
        //no controller
        public string GuardarAsistenciaInvitados(int CodigoInvitado, int CodigoSede, string User, int CodigoUnidadNegocio)
        {


            string mensaje = string.Empty;
            List<AsistenciaInvitadosDTO> list = new List<AsistenciaInvitadosDTO>();
            list.Add(new AsistenciaInvitadosDTO()
            {
                CodigoAsistenciaI = 0,
                CodigoInvitado = CodigoInvitado,
                CodigoSede = CodigoSede,
                UsuarioCreacion = User,
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                Operation = Operation.Create
            });
            ReqAsistenciaInvitadosDTO oReq = new ReqAsistenciaInvitadosDTO()
            {
                List = list,
                User = User
            };
            RespAsistenciaInvitadosDTO oResp = null;
            using (AsistenciaInvitadosLogic oAsistenciaInvitadosLogic = new AsistenciaInvitadosLogic())
            {
                oResp = oAsistenciaInvitadosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return mensaje;
        }

        public AsistenciaDTO BuscarAsistenciaEfectiva(int CodigoMenbresia, int CodigoSocio, string User, int CodSede, int CodigoUnidadNegocio)
        {



            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.CodigoMembresia = CodigoMenbresia;
            oAsistenciaDTO.CodigoSocio = CodigoSocio;
            oAsistenciaDTO.CodigoSede = CodSede;
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.BuscarAsistenciaEfectiva,
                Item = oAsistenciaDTO,
                User = User
            };
            RespItemAsistenciaDTO oResp = null;
            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAsistenciaDTO = oResp.Item;
            }
            return oAsistenciaDTO;
        }

        public List<PedidosDTO> ListarPedidosDelSocio(int CodigoUnidadNegocio, int CodigoSede, int codSocio)
        {
            //
            List<PedidosDTO> lista = null;
            PedidosDTO oPedidosDTO = new PedidosDTO();
            oPedidosDTO.CodigoSocio = codSocio;
            oPedidosDTO.CodigoSede = CodigoSede;
            oPedidosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterPedidosDTO oReq = new ReqFilterPedidosDTO()
            {
                User = "Admin",
                Item = oPedidosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListPedidosDTO oResp = null;
            using (PedidosLogic oPedidosLogic = new PedidosLogic())
            {
                oResp = oPedidosLogic.PedidosGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public List<AsistenciaDTO> uspListarDetalleAsistenciaSocio_Paginacion(int CodigoUnidadNegocio, int CodSede, int CodigoMembresia, int PageNumber)
        {


            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();

            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = CodSede;
            oAsistenciaDTO.CodigoMembresia = CodigoMembresia;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.uspListarDetalleAsistenciaSocio_Paginacion,
                Item = oAsistenciaDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }

        public AsistenciaDTO uspListarDetalleAsistenciaSocio_NumeroRegistros(int CodigoUnidadNegocio, int CodSede, int CodigoMenbresia)
        {


            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.CodigoMembresia = CodigoMenbresia;
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = CodSede;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.uspListarDetalleAsistenciaSocio_NumeroRegistros,
                Item = oAsistenciaDTO,
                User = "admin"
            };
            RespItemAsistenciaDTO oResp = null;
            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAsistenciaDTO = oResp.Item;
                oAsistenciaDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarDetalleAsistenciaSocio_Paginacion"]);
            }
            return oAsistenciaDTO;
        }


        public List<AsistenciaDTO> uspListarDetalleAsistenciaSocio_Paginacion_TODOS(int CodigoUnidadNegocio, int CodSede, int CodigoMembresia, int PageNumber)
        {
            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();

            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = CodSede;
            oAsistenciaDTO.CodigoMembresia = CodigoMembresia;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.uspListarDetalleAsistenciaSocio_Paginacion,
                Item = oAsistenciaDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 200
                }
            };

            RespListAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }

        public AsistenciaDTO uspListarDetalleAsistenciaSocio_NumeroRegistros_TODOS(int CodigoUnidadNegocio, int CodSede, int CodigoMenbresia)
        {


            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.CodigoMembresia = CodigoMenbresia;
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = CodSede;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.uspListarDetalleAsistenciaSocio_NumeroRegistros,
                Item = oAsistenciaDTO,
                User = "admin"
            };
            RespItemAsistenciaDTO oResp = null;
            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAsistenciaDTO = oResp.Item;
                oAsistenciaDTO.TamanioPagina = 200;
            }
            return oAsistenciaDTO;
        }


        //NO CONTROLLER
        public List<AsistenciaInvitadosDTO> uspListarDetalleAsistenciaInvitados_Paginacion(int CodigoUnidadNegocio, int CodInvitado, int PageNumber)
        {

            List<AsistenciaInvitadosDTO> lista = new List<AsistenciaInvitadosDTO>();

            AsistenciaInvitadosDTO oAsistenciaInvitadosDTO = new AsistenciaInvitadosDTO();
            oAsistenciaInvitadosDTO.CodigoInvitado = CodInvitado;
            oAsistenciaInvitadosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterAsistenciaInvitadosDTO oReq = new ReqFilterAsistenciaInvitadosDTO()
            {
                FilterCase = filterCaseAsistenciaInvitados.uspListarDetalleAsistenciaInvitados_Paginacion,
                Item = oAsistenciaInvitadosDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListAsistenciaInvitadosDTO oResp = null;

            using (AsistenciaInvitadosLogic oAsistenciaInvitadosLogic = new AsistenciaInvitadosLogic())
            {
                oResp = oAsistenciaInvitadosLogic.AsistenciaInvitadosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }
        //NO CONTROLLER
        public AsistenciaInvitadosDTO uspListarDetalleAsistenciaInvitados_NumeroRegistros(int CodigoUnidadNegocio, int CodigoInvitado)
        {

            AsistenciaInvitadosDTO oAsistenciaInvitadosDTO = new AsistenciaInvitadosDTO();
            oAsistenciaInvitadosDTO.CodigoInvitado = CodigoInvitado;
            oAsistenciaInvitadosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterAsistenciaInvitadosDTO oReq = new ReqFilterAsistenciaInvitadosDTO()
            {
                FilterCase = filterCaseAsistenciaInvitados.uspListarDetalleAsistenciaInvitados_NumeroRegistros,
                Item = oAsistenciaInvitadosDTO,
                User = "admin"
            };
            RespItemAsistenciaInvitadosDTO oResp = null;
            using (AsistenciaInvitadosLogic oAsistenciaInvitadosLogic = new AsistenciaInvitadosLogic())
            {
                oResp = oAsistenciaInvitadosLogic.AsistenciaInvitadosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAsistenciaInvitadosDTO = oResp.Item;
                oAsistenciaInvitadosDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarDetalleAsistenciaInvitados_Paginacion"]);
            }
            return oAsistenciaInvitadosDTO;
        }
        //NO CONTROLLER
        public List<AsistenciaDTO> ListarAsistenciaTodosFiltro(int CodigoUnidadNegocio, string fechaInicio, string fechaFin, string Hi, string Hf, int CodSede, int PageNumber)
        {

            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);
            if (fechaInicio == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fechaInicio.Split('/')[2]), Convert.ToInt32(fechaInicio.Split('/')[1]), Convert.ToInt32(fechaInicio.Split('/')[0]), HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]), HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
            }


            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();

            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.TipoPersona = "S";
            oAsistenciaDTO.FechaIngreso = fechaConsulta;
            oAsistenciaDTO.FechaFinalizo = fechaConsultaFin;
            oAsistenciaDTO.HoraInicioAsistencia = HoraInicio;
            oAsistenciaDTO.HoraFinAsistencia = HoraFin;
            oAsistenciaDTO.CodigoSede = CodSede;
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.ListarAsistenciaTodosFiltro_Paginacion,
                Item = oAsistenciaDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
                if (lista.Count > 0)
                {
                    lista[0].TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarAsistenciaTodosFiltro_Paginacion"]);
                }
            }

            return lista;

        }
        //NO CONTROLLER
        public List<AsistenciaDTO> uspListar_Socios_Inasistencias(int CodigoUnidadNegocio, int CodSede, string NumeroDiasAtras, string Vendedor, int PageNumber)
        {


            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();
            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            int DiasAtras;
            if (NumeroDiasAtras == "")
            {
                DiasAtras = 1;
            }
            else
            {
                try
                {
                    DiasAtras = Convert.ToInt32(NumeroDiasAtras);
                }
                catch
                {
                    DiasAtras = 1;
                }
            }
            oAsistenciaDTO.DiasAtras = DiasAtras;
            oAsistenciaDTO.CodigoSede = CodSede;
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaDTO.Vendedor = Vendedor;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                Item = oAsistenciaDTO,
                User = "ADMIN",
                FilterCase = filterCaseAsistencia.uspListar_Socios_Inasistencias_Paginacion,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }
            return lista;
        }
        //NO CONTROLLER
        public AsistenciaDTO uspListar_Socios_Inasistencias_NumeroRegistro(int CodigoUnidadNegocio, int CodSede, string NumeroDiasAtras, string Vendedor)
        {


            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            int DiasAtras;
            if (NumeroDiasAtras == "")
            {
                DiasAtras = 1;
            }
            else
            {
                try
                {
                    DiasAtras = Convert.ToInt32(NumeroDiasAtras);
                }
                catch
                {
                    DiasAtras = 1;
                }
            }
            oAsistenciaDTO.DiasAtras = DiasAtras;
            oAsistenciaDTO.CodigoSede = CodSede;
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaDTO.Vendedor = Vendedor;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                Item = oAsistenciaDTO,
                User = "ADMIN",
                FilterCase = filterCaseAsistencia.uspListar_Socios_Inasistencias_NumeroRegistro

            };

            RespItemAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetItem(oReq);
            }

            if (oResp.Success)
            {
                oAsistenciaDTO = oResp.Item;
                oAsistenciaDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListar_Socios_Inasistencias_NumeroRegistro"]);

            }
            return oAsistenciaDTO;
        }
        //NO CONTROLLER

        public List<ContratoDTO> ListarMembresiasSociosAcuenta(int CodigoUnidadNegocio, string Vendedor, string fecha, string fechaFin, int CodSede, int PageNumber)
        {


            DateTime fechaConsulta;
            DateTime fechaConsultaFin;
            if (fecha == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fecha.Split('/')[2]), Convert.ToInt32(fecha.Split('/')[1]), Convert.ToInt32(fecha.Split('/')[0]));
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]));
            }

            List<ContratoDTO> lista = new List<ContratoDTO>();
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.UsuarioCreacion = Vendedor;
            oContratoDTO.FechaInicio = fechaConsulta;
            oContratoDTO.FechaFin = fechaConsultaFin;
            oContratoDTO.CodigoSede = CodSede;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMembresiasSociosAcuenta_Paginacion,
                Item = oContratoDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }
            return lista;
        }
        //NO CONTROLLER
        public ContratoDTO uspListarMembresiasSociosAcuenta_NumeroRegistro(int CodigoUnidadNegocio, string Vendedor, string fecha, string fechaFin, int CodSede)
        {


            DateTime fechaConsulta;
            DateTime fechaConsultaFin;
            if (fecha == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fecha.Split('/')[2]), Convert.ToInt32(fecha.Split('/')[1]), Convert.ToInt32(fecha.Split('/')[0]));
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]));
            }

            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.UsuarioCreacion = Vendedor;
            oContratoDTO.FechaInicio = fechaConsulta;
            oContratoDTO.FechaFin = fechaConsultaFin;
            oContratoDTO.CodigoSede = CodSede;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMembresiasSociosAcuenta_NumeroRegistro,
                Item = oContratoDTO,
                User = "ADMIN"

            };

            RespItemContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }

            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
                oContratoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarMembresiasSociosAcuenta_Paginacion"]);

            }
            return oContratoDTO;
        }
        //NO CONTROLLER
        public List<ClientesDTO> uspVerMasClientesComprometidosPagosCuotas_Paginacion(int CodigoUnidadNegocio, int Tipo, int CodSede, string Vendedor, int PageNumber)
        {


            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Tipo = Tipo;
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.Vendedor = Vendedor;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspVerMasClientesComprometidosPagosCuotas_Paginacion,
                Item = oClientesDTO,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }

            return lista;

        }

        //NO CONTROLLER
        public ClientesDTO uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros(int CodigoUnidadNegocio, int Tipo, string Vendedor, int CodSede)
        {


            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Tipo = Tipo;
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.Vendedor = Vendedor;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros,
                Item = oClientesDTO,
                User = "admin"
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros"]);
            }

            return oClientesDTO;

        }
        //NO CONTROLLER
        public InvitadosDTO BuscarInformacionInvitadosPorCodigoFiltro(int CodigoUnidadNegocio, int CodSede, string Filtro, string User)
        {


            InvitadosDTO oInvitadosDTO = new InvitadosDTO();
            oInvitadosDTO.DNI = Filtro;
            oInvitadosDTO.CodigoSede = CodSede;
            oInvitadosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterInvitadosDTO oReq = new ReqFilterInvitadosDTO()
            {
                FilterCase = filterCaseInvitados.uspBuscarInfoPorCodInvitadoFiltro,
                Item = oInvitadosDTO,
                User = User
            };
            RespItemInvitadosDTO oResp = null;
            using (InvitadosLogic oInvitadosLogic = new InvitadosLogic())
            {
                oResp = oInvitadosLogic.InvitadosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oInvitadosDTO = oResp.Item;
            }

            return oInvitadosDTO;
        }
        //NO CONTROLLER
        public List<InvitadosDTO> uspListarInvitadosBusqueda(int CodigoUnidadNegocio, string valor, int CodSede)
        {


            List<InvitadosDTO> lista = null;
            InvitadosDTO oInvitadosDTO = new InvitadosDTO();
            oInvitadosDTO.Nombres = valor;
            oInvitadosDTO.CodigoSede = CodSede;
            oInvitadosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            if (valor != string.Empty)
            {
                ReqFilterInvitadosDTO oReq = new ReqFilterInvitadosDTO()
                {
                    FilterCase = filterCaseInvitados.uspListarInvitadosBusqueda,
                    Item = oInvitadosDTO,
                    User = "Admin",
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageRecords = 0
                    }
                };

                RespListInvitadosDTO oResp = null;

                using (InvitadosLogic oInvitadosLogic = new InvitadosLogic())
                {
                    oResp = oInvitadosLogic.InvitadosGetList(oReq);
                }

                if (oResp.Success)
                {
                    lista = new List<InvitadosDTO>();
                    lista = oResp.List;
                }
            }

            return lista;

        }
        //NO CONTROLLER
        public List<AsistenciaInvitadosDTO> ListarAsistenciaTodosFiltroInvitados(int CodigoUnidadNegocio, string fechaInicio, string fechaFin, string Hi, string Hf, int CodSede, int PageNumber)
        {


            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);
            if (fechaInicio == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fechaInicio.Split('/')[2]), Convert.ToInt32(fechaInicio.Split('/')[1]), Convert.ToInt32(fechaInicio.Split('/')[0]), HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]), HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
            }


            List<AsistenciaInvitadosDTO> lista = new List<AsistenciaInvitadosDTO>();

            AsistenciaInvitadosDTO oAsistenciaDTO = new AsistenciaInvitadosDTO();
            oAsistenciaDTO.FechaIngreso = fechaConsulta;
            oAsistenciaDTO.FechaFinalizo = fechaConsultaFin;
            oAsistenciaDTO.HoraInicioAsistencia = HoraInicio;
            oAsistenciaDTO.HoraFinAsistencia = HoraFin;
            oAsistenciaDTO.CodigoSede = CodSede;
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;

            ReqFilterAsistenciaInvitadosDTO oReq = new ReqFilterAsistenciaInvitadosDTO()
            {
                FilterCase = filterCaseAsistenciaInvitados.ListarAsistenciaTodosFiltroInvitados_Paginacion,
                Item = oAsistenciaDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListAsistenciaInvitadosDTO oResp = null;

            using (AsistenciaInvitadosLogic oAsistenciaInvitadosLogic = new AsistenciaInvitadosLogic())
            {
                oResp = oAsistenciaInvitadosLogic.AsistenciaInvitadosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }
        //NO CONTROLLER
        public AsistenciaInvitadosDTO uspListarAsistenciaTodosFiltroInvitados_NumeroRegistros(int CodigoUnidadNegocio, string fechaInicio, string fechaFin, string Hi, string Hf, int CodSede)
        {


            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);
            if (fechaInicio == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fechaInicio.Split('/')[2]), Convert.ToInt32(fechaInicio.Split('/')[1]), Convert.ToInt32(fechaInicio.Split('/')[0]), HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]), HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
            }

            AsistenciaInvitadosDTO oAsistenciaInvitadosDTO = new AsistenciaInvitadosDTO();
            oAsistenciaInvitadosDTO.FechaIngreso = fechaConsulta;
            oAsistenciaInvitadosDTO.FechaFinalizo = fechaConsultaFin;
            oAsistenciaInvitadosDTO.CodigoSede = CodSede;
            oAsistenciaInvitadosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterAsistenciaInvitadosDTO oReq = new ReqFilterAsistenciaInvitadosDTO()
            {
                FilterCase = filterCaseAsistenciaInvitados.uspListarAsistenciaTodosFiltroInvitados_NumeroRegistros,
                Item = oAsistenciaInvitadosDTO,
                User = "admin"
            };
            RespItemAsistenciaInvitadosDTO oResp = null;
            using (AsistenciaInvitadosLogic oAsistenciaInvitadosLogic = new AsistenciaInvitadosLogic())
            {
                oResp = oAsistenciaInvitadosLogic.AsistenciaInvitadosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAsistenciaInvitadosDTO = oResp.Item;
                oAsistenciaInvitadosDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarAsistenciaTodosInvitados_Paginacion"]);
            }
            return oAsistenciaInvitadosDTO;
        }

        //NO CONTROLLER
        public List<AgendaSeguimientoDTO> ListarVerSeguimientoAgenda(int CodigoUnidadNegocio, int codSocio)
        {


            List<AgendaSeguimientoDTO> lista = null;
            AgendaSeguimientoDTO oAgendaSeguimientoDTO = new AgendaSeguimientoDTO();
            oAgendaSeguimientoDTO.CodigoSocio = codSocio;
            oAgendaSeguimientoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterAgendaSeguimientoDTO oReq = new ReqFilterAgendaSeguimientoDTO()
            {
                FilterCase = filterCaseAgendaSeguimiento.ListarVerSeguimientoAgendaSocios,
                User = "Admin",
                Item = oAgendaSeguimientoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaSeguimientoLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaSeguimientoLogic.AgendaSeguimientoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }
        //NO CONTROLLER
        public List<ClientesDTO> uspListarClientesPorTodos(int CodigoUnidadNegocio, int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, int CodigoSede, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos, int PageNumber)
        {



            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;


            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorTodos,
                Item = oClientesDTO,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }

            return lista;

        }

        //NO CONTROLLER
        public ClientesDTO uspListarClientesPorTodos_NumeroRegistros(int CodigoUnidadNegocio, int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, int CodigoSede, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos)
        {



            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 30, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorTodos_NumeroRegistros,
                Item = oClientesDTO,
                User = "admin"
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_Todos"]);
            }

            return oClientesDTO;

        }

        //NO CONTROLLER
        public List<ClientesDTO> uspListarClientesActivos(int CodigoUnidadNegocio, int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, int CodigoSede, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {



            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesActivos,
                Item = oClientesDTO,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }

            return lista;

        }
        //NO CONTROLLER
        public ClientesDTO uspListarClientesActivos_NumeroRegistros(int CodigoUnidadNegocio, int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, int CodigoSede, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin)
        {



            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            // oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesActivos_NumeroRegistros,
                Item = oClientesDTO,
                User = "admin"
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
            }

            return oClientesDTO;

        }

        //NO CONTROLLER
        public List<ClientesDTO> uspListarClientesInactivos(int CodigoUnidadNegocio, int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, int CodigoSede, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos, int PageNumber)
        {



            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesInactivos,
                Item = oClientesDTO,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }

            return lista;

        }

        //NO CONTROLLER
        public ClientesDTO uspListarClientesInactivos_NumeroRegistros(int CodigoUnidadNegocio, int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, int CodigoSede, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos)
        {



            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 30, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesInactivos_NumeroRegistros,
                Item = oClientesDTO,
                User = "admin"
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
            }

            return oClientesDTO;

        }


        //NO CONTROLLER
        public List<ClientesDTO> uspListarClientesPorVencer(int CodigoUnidadNegocio, int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, int CodigoSede, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos, int PageNumber)
        {


            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorVencer,
                Item = oClientesDTO,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }

            return lista;

        }
        //NO CONTROLLER
        public ClientesDTO uspListarClientesPorVencer_NumeroRegistros(int CodigoUnidadNegocio, int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, int CodigoSede, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos)
        {



            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 30, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorVencer_NumeroRegistros,
                Item = oClientesDTO,
                User = "admin"
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
            }

            return oClientesDTO;

        }


        //NO CONTROLLER
        //preguntar si se va quitar esta parte
        public List<ContratoDTO> ListarMembresiasImprimir(int codSocio)
        {


            List<ContratoDTO> lista = null;
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoSocio = codSocio;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                User = "Admin",
                Item = oContratoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List.Take(30).ToList();

                for (int i = 0; i < oResp.List.Count; i++)
                {
                    //  lista[i].ListaDetalleObservacionAdicional = ListarObservacionAdicionalImprimir(codSocio, oResp.List[i].CodigoMenbresia);
                }

                for (int i = 0; i < oResp.List.Count; i++)
                {
                    //  lista[i].ListarDetallecalificacionDesempenioAlumnoImprimir = ListarcalificacionDesempenioAlumnoImprimir(codSocio, oResp.List[i].CodigoMenbresia);
                }


            }
            return lista;
        }
        //NO CONTROLLER
        public List<UsuarioDTO> ListarAsesoresComerciales(int CodSede, int CodigoUnidadNegocio)
        {


            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuario_AgendaComercial,
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }
        //NO CONTROLLER
        public int GuardarAgendaSeguimientoCliente(int Codigo, int CodigoSocio, int Tipo, string Asunto, string HoraInicio, string Color, string User, int Estado, int CodigoPaquete, int CodSede, int CodigoUnidadNegocio, string tk, string latitud, string longitud, string UsuarioCreacion)
        {


            int mensaje = 0;
            string[] sHi = HoraInicio.Split('|');
            DateTime hInicio = new DateTime(Convert.ToInt32(sHi[0]), Convert.ToInt32(sHi[1]), Convert.ToInt32(sHi[2]), Convert.ToInt32(sHi[3]), Convert.ToInt32(sHi[4]), Convert.ToInt32(sHi[5]));
            DateTime hFin = new DateTime();
            hFin = hInicio.AddHours(1);
            List<AgendaSeguimientoDTO> list = new List<AgendaSeguimientoDTO>();
            list.Add(new AgendaSeguimientoDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,

                CodigoSocio = CodigoSocio,
                Codigo = Codigo,
                Tipo = Tipo,
                HoraInicio = hInicio,
                HoraFin = hFin,
                Asunto = Asunto,
                Color = Color,
                Estado = Estado,
                CodigoPaquete = CodigoPaquete,
                Vendedor = User,
                UsuarioCreacion = UsuarioCreacion,
                CodigoSede = CodSede,
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                Operation = Operation.Create_UspAgendaSeguimientoTodos
            });
            ReqAgendaSeguimientoDTO oReq = new ReqAgendaSeguimientoDTO()
            {
                List = list,
                User = User
            };
            RespAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return mensaje;
        }
        //NO CONTROLLER
        public int uspValidarCitaAgendarDesdeCliente(int CodigoSocio, string Vendedor, int CodigoTipoAgenda, int CodSede, int CodigoUnidadNegocio)
        {
            int existe = 0;
            using (AgendaSeguimientoLogic oSociosLogic = new AgendaSeguimientoLogic())
            {
                existe = oSociosLogic.uspValidarCitaAgendarDesdeCliente(CodigoSocio, Vendedor, CodigoTipoAgenda, CodSede, CodigoUnidadNegocio);
            }

            return existe;
        }

        public int EliminarFreezing(int CodigoUnidadNegocio, int Codigo, int CodigoMenbresia, int CodigoSocio, string User, int CodSede)
        {

            int mensaje = 0;
            List<HistorialCongelamientoDTO> list = new List<HistorialCongelamientoDTO>();
            list.Add(new HistorialCongelamientoDTO()
            {
                UsuarioCreacion = User,
                CodigoSede = CodSede,
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                Codigo = Codigo,
                CodigoMembresia = CodigoMenbresia,
                CodigoSocio = CodigoSocio,
                Operation = Operation.Delete
            });
            ReqHistorialCongelamientoDTO oReq = new ReqHistorialCongelamientoDTO()
            {
                List = list,
                User = "Admin"
            };
            RespHistorialCongelamientoDTO oResp = null;
            using (HistorialCongelamientoLogic oHistorialCongelamientoLogic = new HistorialCongelamientoLogic())
            {
                oResp = oHistorialCongelamientoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return mensaje;

        }

        public List<PagosSuplementosDTO> uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int PageNumber)
        {


            List<PagosSuplementosDTO> lista = null;
            PagosSuplementosDTO oPagosSuplementosDTO = new PagosSuplementosDTO();
            oPagosSuplementosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPagosSuplementosDTO.CodigoSede = CodigoSede;
            oPagosSuplementosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oPagosSuplementosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oPagosSuplementosDTO.UsuarioCreacion = AsesorComercial;

            ReqFilterPagosSuplementosDTO oReq = new ReqFilterPagosSuplementosDTO()
            {
                FilterCase = filterCasePagosSuplementos.uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion,
                User = "Admin",
                Item = oPagosSuplementosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListPagosSuplementosDTO oResp = null;

            using (PagosSuplementosLogic oPagosSuplementosLogic = new PagosSuplementosLogic())
            {
                oResp = oPagosSuplementosLogic.PagosSuplementosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public PagosSuplementosDTO uspListarACuentaSuplementosPorFechaRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial)
        {


            PagosSuplementosDTO oPagosSuplementosDTO = new PagosSuplementosDTO();
            oPagosSuplementosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPagosSuplementosDTO.CodigoSede = CodigoSede;
            oPagosSuplementosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oPagosSuplementosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oPagosSuplementosDTO.UsuarioCreacion = AsesorComercial;

            ReqFilterPagosSuplementosDTO oReq = new ReqFilterPagosSuplementosDTO()
            {
                FilterCase = filterCasePagosSuplementos.uspListarACuentaSuplementosPorFechaRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oPagosSuplementosDTO

            };

            RespItemPagosSuplementosDTO oResp = null;

            using (PagosSuplementosLogic oPagosSuplementosLogic = new PagosSuplementosLogic())
            {
                oResp = oPagosSuplementosLogic.PagosSuplementosGetItem(oReq);
            }

            if (oResp.Success)
            {
                oPagosSuplementosDTO = oResp.Item;
                oPagosSuplementosDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion"]);
            }

            return oPagosSuplementosDTO;
        }

        public UsuariosIngresosDTO uspValidarAccesoSistema(int CodigoUnidadNegocio, int CodigoSede, string Usuario, string tk, string latitud, string longitud)
        {


            UsuariosIngresosDTO oUsuariosIngresosDTO = new UsuariosIngresosDTO();
            oUsuariosIngresosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oUsuariosIngresosDTO.CodigoSede = CodigoSede;
            oUsuariosIngresosDTO.UsuarioCreacion = Usuario;
            oUsuariosIngresosDTO.CodigoIngreso = Convert.ToInt32(tk.Split('|')[1]);
            oUsuariosIngresosDTO.Latitud = latitud;
            oUsuariosIngresosDTO.Longitud = longitud;

            ReqFilterUsuariosIngresosDTO oReq = new ReqFilterUsuariosIngresosDTO()
            {
                FilterCase = filterCaseUsuariosIngresos.uspValidarAccesoSistema,
                Item = oUsuariosIngresosDTO,
                User = "Admin"
            };
            RespItemUsuariosIngresosDTO oResp = null;
            using (UsuariosIngresosLogic oUsuariosIngresosLogic = new UsuariosIngresosLogic())
            {
                oResp = oUsuariosIngresosLogic.UsuariosIngresosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oUsuariosIngresosDTO = oResp.Item;
            }
            return oUsuariosIngresosDTO;
        }


        public List<ContratoCuotaDTO> uspListarClientesMenbresiasCuotas(int CodigoUnidadNegocio, int CodigoSede, int CodigoMenbresia)
        {
            List<ContratoCuotaDTO> lista = null;
            ContratoCuotaDTO oContratoCuotaDTO = new ContratoCuotaDTO();
            oContratoCuotaDTO.CodigoMembresia = CodigoMenbresia;
            oContratoCuotaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoCuotaDTO.CodigoSede = CodigoSede;

            ReqFilterContratoCuotaDTO oReq = new ReqFilterContratoCuotaDTO()
            {
                FilterCase = filterCaseContratoCuota.uspListarClientesMenbresiasCuotas,
                User = "Admin",
                Item = oContratoCuotaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListContratoCuotaDTO oResp = null;

            using (ContratoCuotaLogic oMembresiasCuotaLogic = new ContratoCuotaLogic())
            {
                oResp = oMembresiasCuotaLogic.ContratoCuotaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public List<UsuarioDTO> ListarAsesorVentas(int CodigoUnidadNegocio, int CodSede)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuario_HacerContrato,
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
                lista.Insert(0, new UsuarioDTO() { Codigo = 0, NombreCompleto = "Ninguno" });
            }
            return lista;
        }

        public List<TiempoMembresiaDTO> ListaTiempoMembresia(string nombre)
        {
            nombre = nombre == "undefined" ? string.Empty : nombre;
            List<TiempoMembresiaDTO> lista = null;
            ReqFilterTiempoMembresiaDTO oReq = new ReqFilterTiempoMembresiaDTO()
            {
                Item = new TiempoMembresiaDTO() { Descripcion = nombre },
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListTiempoMembresiaDTO oResp = null;
            using (TiempoMembresiaLogic oTiempoMembresiaLogic = new TiempoMembresiaLogic())
            {
                oResp = oTiempoMembresiaLogic.TiempoMembresiaGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public string ObtenerInformacionFin(int CodigoUnidadNegocio, int CodigoSede, int codigoMenbresia)
        {
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoMenbresia = codigoMenbresia;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.ObtenerTiempoFin,
                Item = oContratoDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };
            RespItemContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
            }
            return oContratoDTO.Mensaje;
        }

        public ContratoDTO VerInformacionMenbresias(int CodigoUnidadNegocio, int CodigoSede, int codigoMenbresia)
        {
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoMenbresia = codigoMenbresia;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.porCodigoMembresia,
                Item = oContratoDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };

            RespItemContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
            }
            return oContratoDTO;
        }

        public List<UsuarioDTO> SEGListarUsuarioResponsableSuplementos(int CodigoUnidadNegocio, int CodSede, string filtro)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oUsuarioDTO.filtro = filtro;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuarioResponsableSuplementos,
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public ConfiguracionDTO BuscarConfiguracion(int CodigoUnidadNegocio, int CodSede)
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.Codigo = CodSede;
            oConfiguracionDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = "Admin",
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.BuscarPorCodigo
            };
            RespItemConfiguracionDTO oResp = null;
            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);
            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }
            return oConfiguracionDTO;
        }

        public List<UsuarioDTO> ListarUsuarioVendedor(int CodigoUnidadNegocio, int CodSede)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.Where(x => x.CodigoPerfil == 4 && x.Estado == true).ToList();
            }
            return lista;
        }

        public ContratoDTO BuscarMembresia(int CodigoUnidadNegocio, int CodigoSede, int codigoMenbresia)
        {
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoMenbresia = codigoMenbresia;
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.porCodigo,
                Item = oContratoDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };

            RespItemContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
            }
            return oContratoDTO;
        }

        public ContratoFolioDTO ListarContratoMembresia(int CodigoUnidadNegocio, int CodigoSede, int codigoMenbresia)
        {
            ContratoFolioDTO oContratoMembresiaDTO = new ContratoFolioDTO();
            oContratoMembresiaDTO.codigo_Membresia = codigoMenbresia;
            oContratoMembresiaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoMembresiaDTO.CodigoSede = CodigoSede;
            ReqFilterContratoFolioDTO oReq = new ReqFilterContratoFolioDTO()
            {
                FilterCase = filterCaseContratoFolioDTO.porCodigo,
                Item = oContratoMembresiaDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };
            RespItemContratoFolioDTO oResp = null;
            using (ContratoFolioLogic oContratoMembresiaLogic = new ContratoFolioLogic())
            {
                oResp = oContratoMembresiaLogic.ContratoFolioGetItem(oReq);
            }
            if (oResp.Success)
            {
                oContratoMembresiaDTO = oResp.Item;
            }
            return oContratoMembresiaDTO;
        }
        //NO CONTROLLER
        public List<ClientesDTO> uspNotificacionCumpleaniosSocios_Paginacion(int CodigoUnidadNegocio, int CodSede, int flag, int PageNumber)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.flag = flag;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspNotificacionCumpleaniosSocios_Paginacion,
                User = "Admin",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }
        //NO CONTROLLER
        public ClientesDTO uspNotificacionCumpleaniosSocios_NumeroRegistros(int CodigoUnidadNegocio, int CodSede, int flag)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.flag = flag;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspNotificacionCumpleaniosSocios_NumeroRegistros,
                Item = oClientesDTO,
                User = "admin"
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarGridCumpleanios_NumeroRegistros"]);
            }
            return oClientesDTO;
        }

        public List<SubTipoDocumentoDTO> ListarSubTipoDocumentosPorTipoDocumento(int CodigoUnidadNegocio, int CodTipoDocumento, int CodSede)
        {
            List<SubTipoDocumentoDTO> lista = null;
            SubTipoDocumentoDTO oSubTipoDocumentoDTO = new SubTipoDocumentoDTO();
            oSubTipoDocumentoDTO.CodigoTipoDocumento = CodTipoDocumento;
            oSubTipoDocumentoDTO.CodigoSede = CodSede;
            oSubTipoDocumentoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterSubTipoDocumentoDTO oReq = new ReqFilterSubTipoDocumentoDTO()
            {
                User = "Admin",
                FilterCase = E_DataModel.Common.filterCaseSubTipoDocumento.ListarPorTipoDocumento,
                Item = oSubTipoDocumentoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListSubTipoDocumentoDTO oResp = null;
            using (SubTipoDocumentoLogic oSubTipoDocumentoLogic = new SubTipoDocumentoLogic())
            {
                oResp = oSubTipoDocumentoLogic.SubTipoDocumentoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<SubTipoDocumentoDTO>();
                lista = oResp.List;
            }
            return lista;
        }

        public string ObtenerSerieGenarado(int CodigoUnidadNegocio, int tipoDocumento, int subTipoDocumento, int CodSede)
        {
            string nro = string.Empty;
            if (tipoDocumento != 0)
            {
                SeriesDTO oSeriesDTO = new SeriesDTO();
                oSeriesDTO.flag = tipoDocumento;
                oSeriesDTO.subFlag = subTipoDocumento;
                oSeriesDTO.longitudSerie = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["longitudSerie"]);
                oSeriesDTO.CodigoSede = CodSede;
                oSeriesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
                ReqFilterSeriesDTO oReq = new ReqFilterSeriesDTO()
                {
                    FilterCase = E_DataModel.Common.filterCaseSeries.BuscarGenerarCorrelativo,
                    Item = oSeriesDTO,
                    User = "Admin"
                };
                RespItemSeriesDTO oResp = null;
                using (SeriesLogic oSeriesLogic = new SeriesLogic())
                {
                    oResp = oSeriesLogic.SeriesGetItem(oReq);
                }
                if (oResp.Success)
                {
                    oSeriesDTO = oResp.Item;
                    nro = oResp.Item.NroCorrelativoActual;
                }
            }
            return nro;
        }

        public string GuardarSalida(int CodigoUnidadNegocio, int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
                                         string RUC_DNI, string Direccion, DateTime FechaVenta,
                                         int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                         string NroTarjeta, int TipoMoneda, int FormaPago,
                                         decimal SubTotal, decimal IGV, decimal TotalNeto,
                                         decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                         string User, int CodSede, decimal TotalDolares, string tk, string latitud, string longitud, string listaCuotas)
        {

            string mensaje = "";

            //DETALLE SALIDA Y SALIDA
            List<VentasDetalleDTO> Detalle = new List<VentasDetalleDTO>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(listaDetalle);
            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");

            List<printProductos> listaProductosEmprimir = new List<printProductos>();

            foreach (XmlElement nodo in detalle)
            {
                VentasDetalleDTO oitem = new VentasDetalleDTO();
                printProductos oitemPrint = new printProductos();
                oitem.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oitem.CodigoSalidaDetalle = 0;
                oitem.CodigoProducto = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Tipo = 2;
                oitem.Cantidad = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
                oitemPrint.cantidad = nodo.ChildNodes[2].InnerText; //cantidad
                oitem.Descripcion = nodo.ChildNodes[3].InnerText;
                oitemPrint.producto = nodo.ChildNodes[3].InnerText.Split('-')[0]; //producto
                oitem.codigoDetalle_Ingreso = Convert.ToInt32(nodo.ChildNodes[6].InnerText);
                oitem.CodigoPedido = Convert.ToInt32(nodo.ChildNodes[7].InnerText);
                oitem.AsesorComercial = nodo.ChildNodes[8].InnerText;
                oitem.TipoIngresoMembre = nodo.ChildNodes[9].InnerText;
                string pruebaPrecioU = nodo.ChildNodes[4].InnerText;//.Replace(".", ",");
                string pruebaImporte = nodo.ChildNodes[5].InnerText;//.Replace(".", ",");
                decimal newImporte = Convert.ToDecimal(pruebaImporte);
                oitem.PrecioUnitario = decimal.Parse(pruebaPrecioU);
                oitem.Importe = decimal.Parse(pruebaImporte);
                oitem.CodigoSede = CodSede;
                oitemPrint.precio = Convert.ToString(newImporte);
                Detalle.Add(oitem);
                listaProductosEmprimir.Add(oitemPrint);
            }

            //FORMA DE PAGO
            List<ControlSalidaFormaPagoDTO> FPDetalle = new List<ControlSalidaFormaPagoDTO>();
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.LoadXml(listaFormaPago);
            XmlNodeList detallesFP = xmlDoc2.GetElementsByTagName("ds");
            XmlNodeList detalleFP = ((XmlElement)detallesFP[0]).GetElementsByTagName("d");
            foreach (XmlElement nodo in detalleFP)
            {
                ControlSalidaFormaPagoDTO oItemFP = new ControlSalidaFormaPagoDTO();
                oItemFP.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oItemFP.Codigo = 0;
                oItemFP.TipoMoneda = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                string FP_Monto = nodo.ChildNodes[1].InnerText;//.Replace(".", ",");
                oItemFP.Monto = decimal.Parse(FP_Monto);
                string FP_TipoCambio = nodo.ChildNodes[2].InnerText;//.Replace(".", ",");
                oItemFP.TipoCambio = decimal.Parse(FP_TipoCambio);

                oItemFP.FormaPago = Convert.ToInt32(nodo.ChildNodes[3].InnerText);
                oItemFP.SubFormaPago = Convert.ToInt32(nodo.ChildNodes[4].InnerText);
                oItemFP.NroBoucher = nodo.ChildNodes[5].InnerText.ToString();
                oItemFP.UrlBoucher = "";
                FPDetalle.Add(oItemFP);
            }

            //// Cuotas
            //List<ContratoCuotaDTO> FPCuotas = new List<ContratoCuotaDTO>();
            //XmlDocument xmlDoc3 = new XmlDocument();
            //xmlDoc3.LoadXml(listaCuotas);
            //XmlNodeList detallesCuotas = xmlDoc3.GetElementsByTagName("ds");
            //XmlNodeList detalleCuotas = ((XmlElement)detallesCuotas[0]).GetElementsByTagName("d");
            //foreach (XmlElement nodo in detalleCuotas)
            //{
            //    ContratoCuotaDTO oItemCuotas = new ContratoCuotaDTO();
            //    oItemCuotas.CodigoUnidadNegocio = CodigoUnidadNegocio;
            //    oItemCuotas.CodigoSede = CodSede;
            //    oItemCuotas.CodigoCuota = 0;
            //    oItemCuotas.Fecha = Convert.ToDateTime(nodo.ChildNodes[0].InnerText.ToString());
            //    string FP_Monto = nodo.ChildNodes[1].InnerText.Replace(".", ",");
            //    oItemCuotas.Monto = decimal.Parse(FP_Monto);
            //    oItemCuotas.CodigoMembresia = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
            //    FPCuotas.Add(oItemCuotas);
            //}

            List<VentasDTO> lista = new List<VentasDTO>();
            lista.Add(new VentasDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoIngreso = codigoSalida,
                CodigoSocio = CodigoSocio,
                RazonSocial_Sr = RazonSocial_Sr,
                RUC_DNI = RUC_DNI,
                Direccion = Direccion,
                FechaVenta = FechaVenta,
                CodigoTipoComprobante = CodigoTipoComprobante,
                CodigoSubTipoDocumento = CodigoSubTipoComprobante,
                NroComprobante = NroComprobante,
                NroTarjeta = NroTarjeta,
                TipoMoneda = TipoMoneda,
                FormaPago = FormaPago,
                SubTotal = SubTotal,
                IGV = IGV,
                TotalNeto = TotalNeto,
                tipoCambio = tipoCambio,
                UsuarioCreacion = User,
                User = User,
                Operation = E_DataModel.Common.Operation.Create,
                ListaDetalle = Detalle,
                ListaFormaPago = FPDetalle,
                ListaCuotas = null,
                Comentario = "",
                Estado = true,
                CodigoSede = CodSede,
                TotalDolares = TotalDolares,
               

                SerieComprobante = NroComprobante.Split('-')[0],

            });
            ReqVentasDTO oReq = new ReqVentasDTO()
            {
                User = User,
                List = lista
            };
            RespVentasDTO oResp = null;
            bool generarComprobante = false;
            ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
            ConfiguracionDTO = BuscarConfiguracion(CodigoUnidadNegocio, CodSede);
            generarComprobante = ConfiguracionDTO.GenerarComprobante;
            if (CodigoTipoComprobante == 3)
            {
                ConfiguracionDTO.TieneFacturacionElectronica = false;
            }

            var rptaFact = new Respuesta();
            if (ConfiguracionDTO.TieneFacturacionElectronica)
            {
                using (Helpers.NubefactService facturacion = new Helpers.NubefactService())
                {
                    rptaFact = facturacion.EjecutarWebService(lista[0], ConfiguracionDTO);
                }
                if (string.IsNullOrEmpty(rptaFact.errors))
                {
                    oReq.List[0].NroComprobante = string.Format("{0}-{1}", rptaFact.serie, rptaFact.numero);
                }
                using (VentasLogic oControlSalidaLogic = new VentasLogic())
                {
                    oResp = oControlSalidaLogic.ExecuteTransac(oReq);
                }

                if (oResp.Success)
                {
                    mensaje = (oResp.MessageList[0].Codigo).ToString();
                    string data_base64_pdf = "";
                    if (!string.IsNullOrEmpty(rptaFact.enlace_del_pdf))
                    {
                        using (var client = new WebClient())
                        {
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            var bytes = client.DownloadData(rptaFact.enlace_del_pdf);
                            data_base64_pdf = Convert.ToBase64String(bytes);
                        }
                    }

                    return (mensaje + "|" + "2" + "|" + data_base64_pdf ?? string.Empty);
                }
                //else { 

                //}
                if (generarComprobante)
                {
                    return mensaje + "|" + "1";
                }
                else
                {
                    return mensaje + "|" + "0";
                }
            }
            else
            {
                using (VentasLogic oControlSalidaLogic = new VentasLogic())
                {
                    lista[0].GenerarSerie = false;
                    if (ConfiguracionDTO.GenerarSerie == true)
                    {
                        lista[0].GenerarSerie= true;
                    }
                    oResp = oControlSalidaLogic.ExecuteTransac(oReq);
                }

                if (oResp.Success)
                {
                    mensaje = (oResp.MessageList[0].Codigo).ToString();
                }
                if (generarComprobante)
                {
                    return mensaje + "|" + "1";
                }
                else
                {
                    return mensaje + "|" + "0";
                }

            }
        }


        public string GuardarPagoFiado(int CodigoUnidadNegocio, int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
                                        string RUC_DNI, string Direccion, DateTime FechaVenta,
                                        int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                        string NroTarjeta, int TipoMoneda, int FormaPago,
                                        decimal SubTotal, decimal IGV, decimal TotalNeto,
                                        decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                        string User, int CodSede, decimal TotalDolares, string tk, string latitud, string longitud, string listaCuotas)
        {

            string mensaje = "";

            //DETALLE SALIDA Y SALIDA
            List<VentasDetalleDTO> Detalle = new List<VentasDetalleDTO>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(listaDetalle);
            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");

            List<printProductos> listaProductosEmprimir = new List<printProductos>();

            foreach (XmlElement nodo in detalle)
            {
                VentasDetalleDTO oitem = new VentasDetalleDTO();
                printProductos oitemPrint = new printProductos();
                oitem.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oitem.CodigoSalidaDetalle = 0;
                oitem.CodigoProducto = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Tipo = Convert.ToInt32(nodo.ChildNodes[1].InnerText);
                oitem.Cantidad = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
                oitemPrint.cantidad = nodo.ChildNodes[2].InnerText; //cantidad
                oitem.Descripcion = nodo.ChildNodes[3].InnerText;
                oitemPrint.producto = nodo.ChildNodes[3].InnerText.Split('-')[0]; //producto
                oitem.CodigoPedido = Convert.ToInt32(nodo.ChildNodes[7].InnerText);
                oitem.AsesorComercial = nodo.ChildNodes[6].InnerText;
                string pruebaPrecioU = nodo.ChildNodes[4].InnerText;//Replace(".", ",");
                string pruebaImporte = nodo.ChildNodes[5].InnerText;//Replace(".", ",");
                string pruebaAcuenta = nodo.ChildNodes[8].InnerText; //.Replace(".", ",");
                string pruebaDebe = nodo.ChildNodes[9].InnerText;//.Replace(".", ",");
                decimal newImporte = Convert.ToDecimal(pruebaImporte);
                oitem.PrecioUnitario = decimal.Parse(pruebaPrecioU);
                oitem.Importe = decimal.Parse(pruebaImporte);
                oitem.Acuenta = decimal.Parse(pruebaAcuenta);
                oitem.Debe = decimal.Parse(pruebaDebe);
                oitem.CodigoSede = CodSede;
                oitemPrint.precio = Convert.ToString(newImporte);
                Detalle.Add(oitem);
                listaProductosEmprimir.Add(oitemPrint);
            }

            //FORMA DE PAGO
            List<ControlSalidaFormaPagoDTO> FPDetalle = new List<ControlSalidaFormaPagoDTO>();
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.LoadXml(listaFormaPago);
            XmlNodeList detallesFP = xmlDoc2.GetElementsByTagName("ds");
            XmlNodeList detalleFP = ((XmlElement)detallesFP[0]).GetElementsByTagName("d");
            foreach (XmlElement nodo in detalleFP)
            {
                ControlSalidaFormaPagoDTO oItemFP = new ControlSalidaFormaPagoDTO();
                oItemFP.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oItemFP.Codigo = 0;
                oItemFP.TipoMoneda = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                string FP_Monto = nodo.ChildNodes[1].InnerText;//.Replace(".", ",");
                oItemFP.Monto = decimal.Parse(FP_Monto);
                string FP_TipoCambio = nodo.ChildNodes[2].InnerText;//.Replace(".", ",");
                oItemFP.TipoCambio = decimal.Parse(FP_TipoCambio);

                oItemFP.FormaPago = Convert.ToInt32(nodo.ChildNodes[3].InnerText);
                oItemFP.SubFormaPago = Convert.ToInt32(nodo.ChildNodes[4].InnerText);
                oItemFP.NroBoucher = nodo.ChildNodes[5].InnerText.ToString();
                oItemFP.UrlBoucher = "";
                FPDetalle.Add(oItemFP);
            }

            List<VentasDTO> lista = new List<VentasDTO>();
            lista.Add(new VentasDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoIngreso = codigoSalida,
                CodigoSocio = CodigoSocio,
                RazonSocial_Sr = RazonSocial_Sr,
                RUC_DNI = RUC_DNI,
                Direccion = Direccion,
                FechaVenta = FechaVenta,
                CodigoTipoComprobante = CodigoTipoComprobante,
                CodigoSubTipoDocumento = CodigoSubTipoComprobante,
                NroComprobante = NroComprobante,
                NroTarjeta = NroTarjeta,
                TipoMoneda = TipoMoneda,
                FormaPago = FormaPago,
                SubTotal = SubTotal,
                IGV = IGV,
                TotalNeto = TotalNeto,
                tipoCambio = tipoCambio,
                UsuarioCreacion = User,
                User = User,
                Operation = E_DataModel.Common.Operation.CreatePagarFiados,
                ListaDetalle = Detalle,
                ListaFormaPago = FPDetalle,
                ListaCuotas = null,
                Comentario = "",
                Estado = true,
                CodigoSede = CodSede,
                TotalDolares = TotalDolares
            });
            ReqVentasDTO oReq = new ReqVentasDTO()
            {
                User = User,
                List = lista
            };
            RespVentasDTO oResp = null;
            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {
                mensaje = (oResp.MessageList[0].Codigo).ToString();
            }
            return mensaje;
        }


        public string uspActualizarMenbresiasFechaInicio(int CodigoUnidadNegocio, int CodigoSede, int codigoMembresia, string User)
        {
            string mensaje = "0";
            List<ContratoDTO> list = new List<ContratoDTO>();
            list.Add(new ContratoDTO()
            {
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoSede = CodigoSede,
                CodigoMenbresia = codigoMembresia,
                Operation = Operation.uspActualizarMenbresiasFechaInicio,
                UsuarioCreacion = User,
                UsuarioEdicion = User
            });
            ReqContratoDTO oReq = new ReqContratoDTO()
            {
                List = list,
                User = "Admin"
            };
            RespContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = (oResp.MessageList[0].Codigo).ToString();
            }

            return mensaje;
        }


        //NO CONTROLLER
        public int BuscarInformacionPrimerSocioActivo(int CodigoUnidadNegocio, int CodigoSede, string User)
        {


            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodigoSede;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.BuscarCodigoDelPrimerSocio,
                Item = oClientesDTO,
                User = User
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            return oResp.Item == null ? 0 : oResp.Item.CodigoSocio;
        }
        //NO CONTROLLER
        public List<ProductoDTO> uspListarDeudasSuplementoRopaDelSocio(int CodigoUnidadNegocio, int CodigoSede, int CodigoSocio)
        {
            List<ProductoDTO> lista = null;
            ProductoDTO oProductoDTO = new ProductoDTO();
            oProductoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oProductoDTO.CodigoSede = CodigoSede;
            oProductoDTO.codigoSocio = CodigoSocio;

            ReqFilterProductoDTO oReq = new ReqFilterProductoDTO()
            {
                FilterCase = filterCaseProducto.uspListarDeudasSuplementoRopaDelSocio,
                User = "ADMIN",
                Item = oProductoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListProductoDTO oResp = null;
            using (ProductoLogic oProductoLogic = new ProductoLogic())
            {
                oResp = oProductoLogic.ProductoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }
        //NO CONTROLLER
        public string GuardarVentaDeudasSuplementosRopa(int CodigoUnidadNegocio, int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
                                         string RUC_DNI, string Direccion, DateTime FechaVenta,
                                         int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                         string NroTarjeta, int TipoMoneda, int FormaPago,
                                         decimal SubTotal, decimal IGV, decimal TotalNeto,
                                         decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                         string Vendedor, int CodSede, decimal TotalDolares, decimal TotalAporte, int SubFormaPago, string tk, string latitud, string longitud, string UsuarioCreacion)
        {
            string mensaje = "";

            //DETALLE SALIDA Y SALIDA
            List<VentasDetalleDTO> Detalle = new List<VentasDetalleDTO>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(listaDetalle);

            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");

            List<printProductos> listaProductosEmprimir = new List<printProductos>();

            foreach (XmlElement nodo in detalle)
            {
                VentasDetalleDTO oitem = new VentasDetalleDTO();
                printProductos oitemPrint = new printProductos();
                oitem.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oitem.CodigoSede = CodSede;
                oitem.CodigoSalidaDetalle = 0;
                oitem.CodigoProducto = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Tipo = Convert.ToInt32(nodo.ChildNodes[1].InnerText);
                oitem.Cantidad = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
                oitemPrint.cantidad = nodo.ChildNodes[2].InnerText; //cantidad
                oitem.Descripcion = nodo.ChildNodes[3].InnerText;
                oitemPrint.producto = nodo.ChildNodes[3].InnerText.Split('-')[0]; //producto
                oitem.codigoDetalle_Ingreso = Convert.ToInt32(nodo.ChildNodes[6].InnerText);


                string pruebaPrecioU = nodo.ChildNodes[4].InnerText.Replace(".", ",");
                string pruebaImporte = nodo.ChildNodes[5].InnerText.Replace(".", ",");
                string pruebaAcuenta = nodo.ChildNodes[7].InnerText.Replace(".", ",");

                decimal newImporte = Convert.ToDecimal(pruebaImporte);
                decimal newAcuenta = Convert.ToDecimal(pruebaAcuenta);

                oitem.PrecioUnitario = decimal.Parse(pruebaPrecioU);
                oitem.Importe = decimal.Parse(pruebaImporte);
                oitem.Acuenta = Convert.ToDecimal(newAcuenta);
                oitemPrint.precio = Convert.ToString(newImporte);
                Detalle.Add(oitem);
                listaProductosEmprimir.Add(oitemPrint);
            }

            //FORMA DE PAGO
            List<ControlSalidaFormaPagoDTO> FPDetalle = new List<ControlSalidaFormaPagoDTO>();

            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.LoadXml(listaFormaPago);

            XmlNodeList detallesFP = xmlDoc2.GetElementsByTagName("ds");
            XmlNodeList detalleFP = ((XmlElement)detallesFP[0]).GetElementsByTagName("d");

            foreach (XmlElement nodo in detalleFP)
            {
                ControlSalidaFormaPagoDTO oItemFP = new ControlSalidaFormaPagoDTO();
                oItemFP.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oItemFP.Codigo = 0;
                oItemFP.TipoMoneda = Convert.ToInt32(nodo.ChildNodes[0].InnerText);

                string FP_Monto = nodo.ChildNodes[1].InnerText.Replace(".", ",");
                oItemFP.Monto = decimal.Parse(FP_Monto);

                string FP_TipoCambio = nodo.ChildNodes[2].InnerText.Replace(".", ",");
                oItemFP.TipoCambio = decimal.Parse(FP_TipoCambio);

                oItemFP.FormaPago = Convert.ToInt32(nodo.ChildNodes[3].InnerText);
                oItemFP.SubFormaPago = Convert.ToInt32(nodo.ChildNodes[4].InnerText);
                oItemFP.NroBoucher = nodo.ChildNodes[5].InnerText.ToString();
                oItemFP.UrlBoucher = "";

                FPDetalle.Add(oItemFP);
            }

            List<VentasDTO> lista = new List<VentasDTO>();
            lista.Add(new VentasDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,

                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoIngreso = codigoSalida,
                CodigoSocio = CodigoSocio,
                RazonSocial_Sr = RazonSocial_Sr,
                RUC_DNI = RUC_DNI,
                Direccion = Direccion,
                FechaVenta = Convert.ToDateTime(FechaVenta.ToString("yyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss")),
                CodigoTipoComprobante = CodigoTipoComprobante,
                CodigoSubTipoDocumento = CodigoSubTipoComprobante,
                NroComprobante = NroComprobante,
                NroTarjeta = NroTarjeta,
                TipoMoneda = TipoMoneda,
                FormaPago = FormaPago,
                SubFormaPago = SubFormaPago,
                SubTotal = SubTotal,
                IGV = IGV,
                TotalNeto = TotalNeto,
                tipoCambio = tipoCambio,
                UsuarioCreacion = Vendedor,
                User = UsuarioCreacion,
                Operation = E_DataModel.Common.Operation.Create,
                ListaDetalle = Detalle,
                ListaFormaPago = FPDetalle,
                Comentario = "",
                Estado = true,
                CodigoSede = CodSede,
                TotalDolares = TotalDolares,
                TotalAporte = TotalAporte,
                AsesorComercial = Vendedor
            });

            ReqVentasDTO oReq = new ReqVentasDTO()
            {
                User = UsuarioCreacion,
                List = lista
            };
            RespVentasDTO oResp = null;

            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.ExecuteTransac(oReq);
            }

            bool generarComprobante = false;

            if (oResp.Success)
            {
                mensaje = (oResp.MessageList[0].Codigo).ToString();

                //Verificar Configuracion de imprimir
                ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
                ConfiguracionDTO = BuscarConfiguracion(CodigoUnidadNegocio, CodSede);
                generarComprobante = ConfiguracionDTO.GenerarComprobante;

            }

            if (generarComprobante)
            {
                return mensaje + "|" + "1";
            }
            else
            {
                return mensaje + "|" + "0";
            }

        }


        //NO CONTROLLER
        public List<ProductoElaboradoDTO> uspListarDiarios(int CodigoUnidadNegocio, int CodSede)
        {
            List<ProductoElaboradoDTO> lista = null;
            ProductoElaboradoDTO oProductoElaboradoDTO = new ProductoElaboradoDTO();
            oProductoElaboradoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oProductoElaboradoDTO.CodigoSede = CodSede;
            ReqFilterProductoElaboradoDTO oReq = new ReqFilterProductoElaboradoDTO()
            {
                FilterCase = filterCaseProductoElaborado.uspListarDiario,
                User = "ADMIN",
                Item = oProductoElaboradoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListProductoElaboradoDTO oResp = null;
            using (ProductoElaboradoLogic oProductoElaboradoLogic = new ProductoElaboradoLogic())
            {
                oResp = oProductoElaboradoLogic.ProductoElaboradoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }
        //NO CONTROLLER
        public string GuardarVentaDiario(int CodigoUnidadNegocio, int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
                                         string RUC_DNI, string Direccion, DateTime FechaVenta,
                                         int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                         string NroTarjeta, int TipoMoneda, int FormaPago,
                                         decimal SubTotal, decimal IGV, decimal TotalNeto,
                                         decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                         string Vendedor, int CodSede, decimal TotalDolares, decimal TotalAporte, int SubFormaPago, string tk, string latitud, string longitud, string UsuarioCreacion)
        {
            string mensaje = "";

            //DETALLE SALIDA Y SALIDA
            List<VentasDetalleDTO> Detalle = new List<VentasDetalleDTO>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(listaDetalle);

            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");
            List<printProductos> listaProductosEmprimir = new List<printProductos>();

            foreach (XmlElement nodo in detalle)
            {
                VentasDetalleDTO oitem = new VentasDetalleDTO();
                printProductos oitemPrint = new printProductos();
                oitem.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oitem.CodigoSede = CodSede;
                oitem.CodigoSalidaDetalle = 0;
                oitem.CodigoProducto = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Tipo = Convert.ToInt32(nodo.ChildNodes[1].InnerText);
                oitem.Cantidad = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
                oitemPrint.cantidad = nodo.ChildNodes[2].InnerText; //cantidad
                oitem.Descripcion = nodo.ChildNodes[3].InnerText;
                oitemPrint.producto = nodo.ChildNodes[3].InnerText.Split('-')[0]; //producto
                oitem.codigoDetalle_Ingreso = Convert.ToInt32(nodo.ChildNodes[6].InnerText);

                //string pruebaPrecioU = nodo.ChildNodes[4].InnerText.Replace(".", ",");
                //string pruebaImporte = nodo.ChildNodes[5].InnerText.Replace(".", ",");
                //string pruebaAcuenta = nodo.ChildNodes[7].InnerText.Replace(".", ",");
                string pruebaPrecioU = nodo.ChildNodes[4].InnerText;
                string pruebaImporte = nodo.ChildNodes[5].InnerText;
                string pruebaAcuenta = nodo.ChildNodes[7].InnerText;

                decimal newImporte = decimal.Parse(pruebaImporte);
                decimal newAcuenta = decimal.Parse(pruebaAcuenta);

                oitem.PrecioUnitario = decimal.Parse(pruebaPrecioU);
                oitem.Importe = decimal.Parse(pruebaImporte);
                oitem.Acuenta = newAcuenta;
                oitemPrint.precio = Convert.ToString(newImporte);
                Detalle.Add(oitem);
                listaProductosEmprimir.Add(oitemPrint);
            }

            //FORMA DE PAGO
            List<ControlSalidaFormaPagoDTO> FPDetalle = new List<ControlSalidaFormaPagoDTO>();

            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.LoadXml(listaFormaPago);

            XmlNodeList detallesFP = xmlDoc2.GetElementsByTagName("ds");
            XmlNodeList detalleFP = ((XmlElement)detallesFP[0]).GetElementsByTagName("d");

            foreach (XmlElement nodo in detalleFP)
            {
                ControlSalidaFormaPagoDTO oItemFP = new ControlSalidaFormaPagoDTO();
                oItemFP.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oItemFP.Codigo = 0;
                oItemFP.TipoMoneda = Convert.ToInt32(nodo.ChildNodes[0].InnerText);

                string FP_Monto = nodo.ChildNodes[1].InnerText;//.Replace(".", ",");
                oItemFP.Monto = decimal.Parse(FP_Monto);

                string FP_TipoCambio = nodo.ChildNodes[2].InnerText.Replace(".", ",");
                oItemFP.TipoCambio = decimal.Parse(FP_TipoCambio);

                oItemFP.FormaPago = Convert.ToInt32(nodo.ChildNodes[3].InnerText);
                oItemFP.SubFormaPago = Convert.ToInt32(nodo.ChildNodes[4].InnerText);
                oItemFP.NroBoucher = nodo.ChildNodes[5].InnerText.ToString();
                oItemFP.UrlBoucher = "";

                FPDetalle.Add(oItemFP);
            }

            ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
            ConfiguracionDTO = BuscarConfiguracion(CodigoUnidadNegocio, CodSede);

            if (CodigoTipoComprobante == 3)
            {
                ConfiguracionDTO.TieneFacturacionElectronica = false;
            }

            List<VentasDTO> lista = new List<VentasDTO>();
            lista.Add(new VentasDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,

                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoIngreso = codigoSalida,
                CodigoSocio = CodigoSocio,
                RazonSocial_Sr = RazonSocial_Sr,
                RUC_DNI = RUC_DNI,
                Direccion = Direccion,
                FechaVenta = Convert.ToDateTime(FechaVenta.ToString("yyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss")),
                CodigoTipoComprobante = CodigoTipoComprobante,
                CodigoSubTipoDocumento = CodigoSubTipoComprobante,
                NroComprobante = NroComprobante,
                NroTarjeta = NroTarjeta,
                TipoMoneda = TipoMoneda,
                FormaPago = FormaPago,
                SubFormaPago = SubFormaPago,
                SubTotal = SubTotal,
                IGV = IGV,
                TotalNeto = TotalNeto,
                tipoCambio = tipoCambio,
                UsuarioCreacion = Vendedor,
                User = UsuarioCreacion,
                Operation = E_DataModel.Common.Operation.Create,
                ListaDetalle = Detalle,
                ListaFormaPago = FPDetalle,
                Comentario = "",
                Estado = true,
                CodigoSede = CodSede,
                TotalDolares = TotalDolares,
                TotalAporte = TotalAporte,
                AsesorComercial = Vendedor,
                GenerarSerie = ConfiguracionDTO.GenerarSerie,
                TieneFacturacionElectronica = ConfiguracionDTO.TieneFacturacionElectronica
            });
            ReqVentasDTO oReq = new ReqVentasDTO()
            {
                User = UsuarioCreacion,
                List = lista
            };
            RespVentasDTO oResp = null;

            var rptaFact = new Respuesta();
            if (ConfiguracionDTO.TieneFacturacionElectronica)
            {
                using (Helpers.NubefactService facturacion = new Helpers.NubefactService())
                {
                    lista[0].SerieComprobante = lista[0].NroComprobante.Split('-')[0].ToString();
                    rptaFact = facturacion.EjecutarWebService(lista[0], ConfiguracionDTO);
                }
                string data_base64_pdf = "";
                if (string.IsNullOrEmpty(rptaFact.errors))
                {
                    oReq.List[0].NroComprobante = string.Format("{0}-{1}", rptaFact.serie, rptaFact.numero);
                    using (VentasLogic oControlSalidaLogic = new VentasLogic())
                    {
                        oReq.List[0].NroComprobante = string.Format("{0}-{1}", rptaFact.serie, rptaFact.numero);
                        oResp = oControlSalidaLogic.ExecuteTransac(oReq);
                    }
                    if (!string.IsNullOrEmpty(rptaFact.enlace_del_pdf))
                    {
                        using (var client = new WebClient())
                        {
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            var bytes = client.DownloadData(rptaFact.enlace_del_pdf);
                            data_base64_pdf = Convert.ToBase64String(bytes);
                        }
                    }
                }
                else
                {
                    oResp.Success = false;
                    oResp.MessageList = new List<Mensaje>();
                    oResp.MessageList.Add(new Mensaje()
                    {
                        Codigo = 100,
                        Detalle = rptaFact.errors
                    });
                }

                if (oResp.Success)
                {
                    mensaje = (oResp.MessageList[0].Codigo).ToString();
                }

                if (ConfiguracionDTO.GenerarComprobante)
                {
                    return mensaje + "|" + "2" + "|" + data_base64_pdf;
                }
                else
                {
                    return mensaje + "|" + "0";
                }
            }
            else
            {
                using (VentasLogic oControlSalidaLogic = new VentasLogic())
                {
                    oResp = oControlSalidaLogic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    mensaje = (oResp.MessageList[0].Codigo).ToString();
                }

                if (ConfiguracionDTO.GenerarComprobante)
                {
                    return mensaje + "|" + "1";
                }
                else
                {
                    return mensaje + "|" + "0";
                }
            }



        }


        public ConfiguracionDTO uspSeguridadObtenerUnidadNegocio(int CodigoUnidadNegocio, int CodigoSede)
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = CodigoSede;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = "Admin",
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.uspSeguridadObtenerUnidadNegocio
            };

            RespItemConfiguracionDTO oResp = null;
            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);

            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }
            return oConfiguracionDTO;
        }

        public List<SuplementosDTO> uspListarSuplementosVentasPorCategoria(int CodigoUnidadNegocio, int CodSede, int CodigoCategoria)
        {
            List<SuplementosDTO> lista = null;
            SuplementosDTO oSuplementosDTO = new SuplementosDTO();
            oSuplementosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oSuplementosDTO.CodigoSede = CodSede;
            oSuplementosDTO.CodigoCategoria = CodigoCategoria;
            ReqFilterSuplementosDTO oReq = new ReqFilterSuplementosDTO()
            {
                FilterCase = filterCaseSuplementos.uspListarSuplementosVentasPorCategoria,
                User = "ADMIN",
                Item = oSuplementosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListSuplementosDTO oResp = null;
            using (SuplementosLogic oSuplementosLogic = new SuplementosLogic())
            {
                oResp = oSuplementosLogic.SuplementosGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public List<ProductoDTO> uspListarProductoPorCategoriaVenta(int CodigoUnidadNegocio, int CodSede, int CodigoCategoria)
        {
            List<ProductoDTO> lista = null;
            ProductoDTO oProductoDTO = new ProductoDTO();
            oProductoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oProductoDTO.CodigoSede = CodSede;
            oProductoDTO.CodigoCategoria = CodigoCategoria;
            ReqFilterProductoDTO oReq = new ReqFilterProductoDTO()
            {
                FilterCase = filterCaseProducto.uspListarProductoPorCategoriaVenta,
                User = "ADMIN",
                Item = oProductoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListProductoDTO oResp = null;
            using (ProductoLogic oProductoLogic = new ProductoLogic())
            {
                oResp = oProductoLogic.ProductoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public List<RopasDTO> uspListarRopasVentas(int CodigoUnidadNegocio, int CodSede)
        {
            List<RopasDTO> lista = null;
            RopasDTO oRopasDTO = new RopasDTO();
            oRopasDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oRopasDTO.CodigoSede = CodSede;
            ReqFilterRopasDTO oReq = new ReqFilterRopasDTO()
            {
                FilterCase = filterCaseRopas.uspListarRopasVentas,
                User = "ADMIN",
                Item = oRopasDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListRopasDTO oResp = null;
            using (RopasLogic oRopasLogic = new RopasLogic())
            {
                oResp = oRopasLogic.RopasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public string GuardarVentaSuplementos(int CodigoUnidadNegocio, int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
                                         string RUC_DNI, string Direccion, DateTime FechaVenta,
                                         int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                         string NroTarjeta, int TipoMoneda, int FormaPago,
                                         decimal SubTotal, decimal IGV, decimal TotalNeto,
                                         decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                         string Vendedor, int CodSede, decimal TotalDolares, decimal TotalAporte, int SubFormaPago, string tk, string latitud, string longitud, string UsuarioCreacion)
        {
            string mensaje = "";

            //DETALLE SALIDA Y SALIDA
            List<VentasDetalleDTO> Detalle = new List<VentasDetalleDTO>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(listaDetalle);

            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");

            List<printProductos> listaProductosEmprimir = new List<printProductos>();

            foreach (XmlElement nodo in detalle)
            {
                VentasDetalleDTO oitem = new VentasDetalleDTO();
                printProductos oitemPrint = new printProductos();
                oitem.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oitem.CodigoSede = CodSede;
                oitem.CodigoSalidaDetalle = 0;
                oitem.CodigoProducto = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Tipo = Convert.ToInt32(nodo.ChildNodes[1].InnerText);
                oitem.Cantidad = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
                oitemPrint.cantidad = nodo.ChildNodes[2].InnerText; //cantidad
                oitem.Descripcion = nodo.ChildNodes[3].InnerText;
                oitemPrint.producto = nodo.ChildNodes[3].InnerText.Split('-')[0]; //producto
                oitem.codigoDetalle_Ingreso = Convert.ToInt32(nodo.ChildNodes[6].InnerText);


                string pruebaPrecioU = nodo.ChildNodes[4].InnerText.Replace(".", ",");
                string pruebaImporte = nodo.ChildNodes[5].InnerText.Replace(".", ",");
                string pruebaAcuenta = nodo.ChildNodes[7].InnerText.Replace(".", ",");

                decimal newImporte = Convert.ToDecimal(pruebaImporte);
                decimal newAcuenta = Convert.ToDecimal(pruebaAcuenta);

                oitem.PrecioUnitario = decimal.Parse(pruebaPrecioU);
                oitem.Importe = decimal.Parse(pruebaImporte);
                oitem.Acuenta = Convert.ToDecimal(newAcuenta);
                oitemPrint.precio = Convert.ToString(newImporte);
                Detalle.Add(oitem);
                listaProductosEmprimir.Add(oitemPrint);
            }

            //FORMA DE PAGO
            List<ControlSalidaFormaPagoDTO> FPDetalle = new List<ControlSalidaFormaPagoDTO>();

            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.LoadXml(listaFormaPago);

            XmlNodeList detallesFP = xmlDoc2.GetElementsByTagName("ds");
            XmlNodeList detalleFP = ((XmlElement)detallesFP[0]).GetElementsByTagName("d");

            foreach (XmlElement nodo in detalleFP)
            {
                ControlSalidaFormaPagoDTO oItemFP = new ControlSalidaFormaPagoDTO();
                oItemFP.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oItemFP.Codigo = 0;
                oItemFP.TipoMoneda = Convert.ToInt32(nodo.ChildNodes[0].InnerText);

                string FP_Monto = nodo.ChildNodes[1].InnerText.Replace(".", ",");
                oItemFP.Monto = decimal.Parse(FP_Monto);

                string FP_TipoCambio = nodo.ChildNodes[2].InnerText.Replace(".", ",");
                oItemFP.TipoCambio = decimal.Parse(FP_TipoCambio);

                oItemFP.FormaPago = Convert.ToInt32(nodo.ChildNodes[3].InnerText);
                oItemFP.SubFormaPago = Convert.ToInt32(nodo.ChildNodes[4].InnerText);
                oItemFP.NroBoucher = nodo.ChildNodes[5].InnerText.ToString();
                oItemFP.UrlBoucher = "";

                FPDetalle.Add(oItemFP);
            }

            List<VentasDTO> lista = new List<VentasDTO>();
            lista.Add(new VentasDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,

                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoIngreso = codigoSalida,
                CodigoSocio = CodigoSocio,
                RazonSocial_Sr = RazonSocial_Sr,
                RUC_DNI = RUC_DNI,
                Direccion = Direccion,
                FechaVenta = Convert.ToDateTime(FechaVenta.ToString("yyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss")),
                CodigoTipoComprobante = CodigoTipoComprobante,
                CodigoSubTipoDocumento = CodigoSubTipoComprobante,
                NroComprobante = NroComprobante,
                NroTarjeta = NroTarjeta,
                TipoMoneda = TipoMoneda,
                FormaPago = FormaPago,
                SubFormaPago = SubFormaPago,
                SubTotal = SubTotal,
                IGV = IGV,
                TotalNeto = TotalNeto,
                tipoCambio = tipoCambio,
                UsuarioCreacion = Vendedor,
                User = UsuarioCreacion,
                Operation = E_DataModel.Common.Operation.Create,
                ListaDetalle = Detalle,
                ListaFormaPago = FPDetalle,
                Comentario = "",
                Estado = true,
                CodigoSede = CodSede,
                TotalDolares = TotalDolares,
                TotalAporte = TotalAporte,
                AsesorComercial = Vendedor
            });

            ReqVentasDTO oReq = new ReqVentasDTO()
            {
                User = UsuarioCreacion,
                List = lista
            };
            RespVentasDTO oResp = null;

            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.ExecuteTransac(oReq);
            }

            bool generarComprobante = false;

            if (oResp.Success)
            {
                mensaje = (oResp.MessageList[0].Codigo).ToString();

                //Verificar Configuracion de imprimir
                ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
                ConfiguracionDTO = BuscarConfiguracion(CodigoUnidadNegocio, CodSede);
                generarComprobante = ConfiguracionDTO.GenerarComprobante;

            }

            if (generarComprobante)
            {
                return mensaje + "|" + "1";
            }
            else
            {
                return mensaje + "|" + "0";
            }

        }



        public List<ProductoDTO> uspListarProductoBuscadorPorNombre(int CodigoUnidadNegocio, int CodigoSede, int CodigoSocio, string Descripcion)
        {
            List<ProductoDTO> lista = null;
            ProductoDTO oProductoDTO = new ProductoDTO();
            oProductoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oProductoDTO.CodigoSede = CodigoSede;
            oProductoDTO.codigoSocio = CodigoSocio;
            oProductoDTO.Descripcion = Descripcion;
            ReqFilterProductoDTO oReq = new ReqFilterProductoDTO()
            {
                FilterCase = filterCaseProducto.uspListarProductoBuscadorPorNombre,
                User = "ADMIN",
                Item = oProductoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListProductoDTO oResp = null;
            using (ProductoLogic oProductoLogic = new ProductoLogic())
            {
                oResp = oProductoLogic.ProductoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }


        public string GuardarFiadosSuplementos(int CodigoUnidadNegocio, int codigoSalida, int CodigoSocio,
                                           DateTime FechaVenta, string listaDetalle, string listaFormaPago, string Vendedor, int CodSede,
                                           string tk, string latitud, string longitud, string UsuarioCreacion)
        {
            string mensaje = "";

            //DETALLE SALIDA Y SALIDA
            List<VentasDetalleDTO> Detalle = new List<VentasDetalleDTO>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(listaDetalle);

            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");

            List<printProductos> listaProductosEmprimir = new List<printProductos>();

            foreach (XmlElement nodo in detalle)
            {
                VentasDetalleDTO oitem = new VentasDetalleDTO();
                printProductos oitemPrint = new printProductos();
                oitem.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oitem.CodigoSede = CodSede;
                oitem.CodigoSalidaDetalle = 0;
                oitem.CodigoProducto = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Tipo = Convert.ToInt32(nodo.ChildNodes[1].InnerText);
                oitem.Cantidad = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
                oitemPrint.cantidad = nodo.ChildNodes[2].InnerText; //cantidad
                oitem.Descripcion = nodo.ChildNodes[3].InnerText;
                oitemPrint.producto = nodo.ChildNodes[3].InnerText.Split('-')[0]; //producto
                oitem.codigoDetalle_Ingreso = Convert.ToInt32(nodo.ChildNodes[6].InnerText);


                string pruebaPrecioU = nodo.ChildNodes[4].InnerText.Replace(".", ",");
                string pruebaImporte = nodo.ChildNodes[5].InnerText.Replace(".", ",");
                string pruebaAcuenta = nodo.ChildNodes[7].InnerText.Replace(".", ",");

                decimal newImporte = Convert.ToDecimal(pruebaImporte);
                decimal newAcuenta = Convert.ToDecimal(pruebaAcuenta);

                oitem.PrecioUnitario = decimal.Parse(pruebaPrecioU);
                oitem.Importe = decimal.Parse(pruebaImporte);
                oitem.Acuenta = Convert.ToDecimal(newAcuenta);
                oitemPrint.precio = Convert.ToString(newImporte);
                Detalle.Add(oitem);
                listaProductosEmprimir.Add(oitemPrint);
            }

            //FORMA DE PAGO
            List<ControlSalidaFormaPagoDTO> FPDetalle = new List<ControlSalidaFormaPagoDTO>();
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.LoadXml(listaFormaPago);
            XmlNodeList detallesFP = xmlDoc2.GetElementsByTagName("ds");
            XmlNodeList detalleFP = ((XmlElement)detallesFP[0]).GetElementsByTagName("d");
            foreach (XmlElement nodo in detalleFP)
            {
                ControlSalidaFormaPagoDTO oItemFP = new ControlSalidaFormaPagoDTO();
                oItemFP.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oItemFP.Codigo = 0;
                oItemFP.TipoMoneda = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                string FP_Monto = nodo.ChildNodes[1].InnerText.Replace(".", ",");
                oItemFP.Monto = decimal.Parse(FP_Monto);
                string FP_TipoCambio = nodo.ChildNodes[2].InnerText.Replace(".", ",");
                oItemFP.TipoCambio = decimal.Parse(FP_TipoCambio);

                oItemFP.FormaPago = Convert.ToInt32(nodo.ChildNodes[3].InnerText);
                oItemFP.SubFormaPago = Convert.ToInt32(nodo.ChildNodes[4].InnerText);
                oItemFP.NroBoucher = nodo.ChildNodes[5].InnerText.ToString();
                oItemFP.UrlBoucher = "";
                FPDetalle.Add(oItemFP);
            }

            List<VentasDTO> lista = new List<VentasDTO>();
            lista.Add(new VentasDTO()
            {
                TK_ID = Convert.ToInt32(tk.Split('|')[1]),
                TK_Latitude = latitud,
                TK_Longitude = longitud,

                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoIngreso = codigoSalida,
                CodigoSocio = CodigoSocio,
                RazonSocial_Sr = "",
                RUC_DNI = "",
                Direccion = "",
                FechaVenta = FechaVenta,
                CodigoTipoComprobante = 1,
                CodigoSubTipoDocumento = 1,
                NroComprobante = "",
                NroTarjeta = "",
                TipoMoneda = 1,
                FormaPago = 1,
                SubTotal = 0,
                IGV = 0,
                TotalNeto = 0,
                tipoCambio = 1,
                UsuarioCreacion = Vendedor,
                User = UsuarioCreacion,
                Operation = E_DataModel.Common.Operation.CreateFiados,
                ListaDetalle = Detalle,
                ListaFormaPago = FPDetalle,
                Comentario = "",
                Estado = true,
                CodigoSede = CodSede,
                TotalDolares = 0
            });

            ReqVentasDTO oReq = new ReqVentasDTO()
            {
                User = UsuarioCreacion,
                List = lista
            };
            RespVentasDTO oResp = null;

            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.ExecuteTransac(oReq);
            }

            bool generarComprobante = false;

            if (oResp.Success)
            {
                mensaje = (oResp.MessageList[0].Codigo).ToString();
            }

            if (generarComprobante)
            {
                return mensaje + "|" + "1";
            }
            else
            {
                return mensaje + "|" + "0";
            }

        }




        public VentasDTO BuscarInformacionGeneralVentaPorCodigo(int CodigoUnidadNegocio, int CodigoSede, int codigo)
        {
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoVenta = codigo;
            oVentasDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = CodigoSede;
            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.BuscarInfoGeneralVentaPorCodigo,
                Item = oVentasDTO,
                User = "appsfit"
            };
            RespItemVentasDTO oResp = null;
            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.VentasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oVentasDTO = oResp.Item;
            }
            return oVentasDTO;
        }


        public List<HorarioPaqueteDTO> uspListarAsignarDiasHorarioPaquete(int CodigoUnidadNegocio, int CodigoPaquete)
        {
            List<HorarioPaqueteDTO> lista = null;
            ReqFilterHorarioPaqueteDTO oReq = new ReqFilterHorarioPaqueteDTO()
            {
                Item = new HorarioPaqueteDTO()
                {
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoPaquete = CodigoPaquete
                },
                FilterCase = filterCaseHorarioPaquete.uspListarDiasHorarioPaquete_visualizar,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListHorarioPaqueteDTO oResp = null;
            using (HorarioPaqueteLogic oHorarioPaqueteLogic = new HorarioPaqueteLogic())
            {
                oResp = oHorarioPaqueteLogic.HorarioPaqueteGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public string uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo(int CodigoUnidadNegocio, int CodigoSede, int flag, string User)
        {
            string mensaje = "0";
            List<UsuariosIngresosDTO> list = new List<UsuariosIngresosDTO>();
            list.Add(new UsuariosIngresosDTO()
            {
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoSede = CodigoSede,
                flag = flag,
                Operation = Operation.uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo,
                UsuarioCreacion = User
            });
            ReqUsuariosIngresosDTO oReq = new ReqUsuariosIngresosDTO()
            {
                List = list,
                User = "Admin"
            };
            RespUsuariosIngresosDTO oResp = null;
            using (UsuariosIngresosLogic oMenbresiasLogic = new UsuariosIngresosLogic())
            {
                oResp = oMenbresiasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = (oResp.MessageList[0].Codigo).ToString();
            }

            return mensaje;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

