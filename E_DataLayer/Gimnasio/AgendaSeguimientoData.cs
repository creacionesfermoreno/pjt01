using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class AgendaSeguimientoData
	{
		
        public List<AgendaSeguimientoDTO> Listar(AgendaSeguimientoDTO oAgendaSeguimientoDTO)
		{
			List<AgendaSeguimientoDTO> lista = new List<AgendaSeguimientoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaSeguimiento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaSeguimientoDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    fechaTexto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    fechaActividadTexto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    strFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString() ,
                                    imgTipoActividad = oIDataReader[oIDataReader.GetOrdinal("imgTipoActividad")].ToString(),
                                    desActividad = oIDataReader[oIDataReader.GetOrdinal("desActividad")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}

        public List<AgendaSeguimientoDTO> uspListarVerSeguimientoAgendaSocios(AgendaSeguimientoDTO oAgendaSeguimientoDTO)
		{
            List<AgendaSeguimientoDTO> lista = new List<AgendaSeguimientoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarVerSeguimientoAgendaSocios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaSeguimientoDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    AsuntoCompleto = oIDataReader[oIDataReader.GetOrdinal("AsuntoCompleto")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    fechaTexto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    strFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DesTipo = oIDataReader[oIDataReader.GetOrdinal("DesTipo")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}

         public List<AgendaSeguimientoDTO> uspListarInformeCantidadCitasVendedores(AgendaSeguimientoDTO oAgendaSeguimientoDTO)
        {
            List<AgendaSeguimientoDTO> lista = new List<AgendaSeguimientoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarInformeCantidadCitasVendedores", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodSede;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaInicio", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@FiltroFechaFin", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaHasta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaSeguimientoDTO()
                                {
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    CantCitasVendedores = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadTotal")])                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<AgendaSeguimientoDTO> uspListarGridAgendaGeneral_ExportarExcel(AgendaSeguimientoDTO oAgendaSeguimientoDTO, Paging paging)
        {
            List<AgendaSeguimientoDTO> lista = new List<AgendaSeguimientoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarGridAgendaGeneral_ExportarExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.Buscador;
                    cmd.Parameters.Add(new SqlParameter("@fechaDesde", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@fechaHasta", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaHasta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoTipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TiempoPaquete", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.TipoCliente;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaSeguimientoDTO()
                                {
                                    CodigoTipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DescTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DescTipo")].ToString().Substring(0, 5),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    DesPaquete = oIDataReader[oIDataReader.GetOrdinal("desPaquete")].ToString(),
                                    DesTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("desTiempoPaquete")].ToString(),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    ColorAgenda = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    HoraInicioAgenda = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    DesTipoCliente = oIDataReader[oIDataReader.GetOrdinal("desTipoCliente")].ToString(),
                                    DesFechaHoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    HoraFinAgenda = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]),
                                    AsuntoAgenda = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    DescEstadoAgenda = oIDataReader[oIDataReader.GetOrdinal("DescEstado")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DesFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    EstadoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    imagenActualizar = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "../Imagenes/innovatec/denegar.png" : "../Imagenes/innovatec/checkActualizar.png",
                                    BotonVerAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]) == 1 ? "<button onclick='VerInformes(" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]) + ")' type='button' class='btn btn-primary btn-sm' style='color: #FFFFFF;background-color: rgb(0, 140, 205);border-color: #010101;'>Ver Agenda</button>" : "",
                                    CantidadCitas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadCitas")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public List<AgendaSeguimientoDTO> uspListarGridAgendaGeneral_Paginacion(AgendaSeguimientoDTO oAgendaSeguimientoDTO,Paging paging)
        {
            List<AgendaSeguimientoDTO> lista = new List<AgendaSeguimientoDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarGridAgendaGeneral_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.Buscador;
                    cmd.Parameters.Add(new SqlParameter("@fechaDesde", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@fechaHasta", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaHasta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoTipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodSede;
                    cmd.Parameters.Add(new SqlParameter("@TiempoPaquete", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.TipoCliente;
                    cmd.Parameters.Add(new SqlParameter("@TipoActividad", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.TipoActividad;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.Add("@NumeroRegistros", System.Data.SqlDbType.Int);
                    cmd.Parameters["@NumeroRegistros"].Direction = System.Data.ParameterDirection.Output;

                    //cmd.Parameters.Add("@Proyectado", System.Data.SqlDbType.Decimal);
                    //cmd.Parameters["@Proyectado"].Direction = System.Data.ParameterDirection.Output;

                    //cmd.Parameters.Add("@DiaCitasCaida", System.Data.SqlDbType.Int);
                    //cmd.Parameters["@DiaCitasCaida"].Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                AgendaSeguimientoDTO bucle = new AgendaSeguimientoDTO();

                                bucle.CodigoTipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]);
                                bucle.Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]);
                                bucle.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                bucle.Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString();
                                bucle.Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString();
                                bucle.DescTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DescTipo")].ToString().Substring(0, 3);
                                bucle.Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString();
                                bucle.Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString();
                                bucle.EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString();
                                bucle.Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString();
                                bucle.DesPaquete = oIDataReader[oIDataReader.GetOrdinal("desPaquete")].ToString();
                                bucle.DesTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("desTiempoPaquete")].ToString();
                                bucle.Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]);
                                bucle.Vendedor = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                bucle.ColorAgenda = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                bucle.HoraInicioAgenda = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]);
                                bucle.DesTipoCliente = oIDataReader[oIDataReader.GetOrdinal("desTipoCliente")].ToString();
                                bucle.DesFechaHoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]).ToString("dd/MM/yyyy HH:mm:ss tt");
                                bucle.HoraFinAgenda = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]);
                                bucle.AsuntoAgenda = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString();
                                bucle.DescEstadoAgenda = oIDataReader[oIDataReader.GetOrdinal("DescEstado")].ToString();
                                bucle.Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>";
                                bucle.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                bucle.DiasFaltantesCaida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiasFaltantesCaida")]);

                                bucle.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);
                                bucle.DesFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm:ss tt");
                                bucle.EstadoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                bucle.DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString();
                                bucle.imagenActualizar = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "../Imagenes/innovatec/denegar.png" : "../Imagenes/innovatec/checkActualizar.png";
                                bucle.BotonVerAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]) == 1 ? "<button onclick='VerInformes(" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]) + ")' type='button' class='btn btn-primary btn-sm' style='color: #FFFFFF;background-color: rgb(0, 140, 205);border-color: #010101;'>Ver Agenda</button>" : "";
                                bucle.CantidadCitas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadCitas")]);
                                bucle.imgTipoActividad = oIDataReader[oIDataReader.GetOrdinal("imgTipoActividad")].ToString();
                                lista.Add(bucle);

                            }
                           
                        }

                    }

                    ////NumeroRegistros,ref decimal Proyectado,ref int DiaCitasCaida
                    //NumeroRegistros = Convert.ToInt32(cmd.Parameters["@NumeroRegistros"].Value);
                    //Proyectado = Convert.ToDecimal(cmd.Parameters["@Proyectado"].Value);
                    //DiaCitasCaida = Convert.ToInt32(cmd.Parameters["@DiaCitasCaida"].Value);

                }
            }
            return lista;
        }
		
        public AgendaSeguimientoDTO uspListarGridAgendaGeneral_NumeroRegistros(AgendaSeguimientoDTO oAgendaSeguimientoDTO)
		{
            AgendaSeguimientoDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarGridAgendaGeneral_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.Buscador;
                    cmd.Parameters.Add(new SqlParameter("@fechaDesde", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@fechaHasta", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaHasta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoTipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodSede;
                    cmd.Parameters.Add(new SqlParameter("@TiempoPaquete", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.TipoCliente;
                    cmd.Parameters.Add(new SqlParameter("@TipoActividad", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.TipoActividad;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new AgendaSeguimientoDTO()
                                {
                                    CantTotal = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")]),
                                    MontoTotal = Convert.ToDecimal(reader[reader.GetOrdinal("TotalMonto")]),
                                    DiasCitaCaida = Convert.ToInt32(reader[reader.GetOrdinal("CitasCaida")])                                    
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}

        public List<AgendaSeguimientoDTO> uspListarGridAgendaGeneralAuditoria_Paginacion(AgendaSeguimientoDTO oAgendaSeguimientoDTO, Paging paging)
        {
            List<AgendaSeguimientoDTO> lista = new List<AgendaSeguimientoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarGridAgendaGeneralAuditoria_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.Buscador;
                    cmd.Parameters.Add(new SqlParameter("@fechaDesde", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@fechaHasta", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaHasta;                    
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.UsuarioCreacion;
                   
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                AgendaSeguimientoDTO bucle = new AgendaSeguimientoDTO();

                                bucle.CodigoTipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]);
                                bucle.Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]);
                                bucle.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                bucle.Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString();
                                bucle.Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString();
                                bucle.DescTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DescTipo")].ToString().Substring(0, 3);
                                bucle.Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString();
                                bucle.Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString();
                                bucle.EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString();
                                bucle.Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString();
                                bucle.DesPaquete = oIDataReader[oIDataReader.GetOrdinal("desPaquete")].ToString();
                                bucle.DesTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("desTiempoPaquete")].ToString();
                                bucle.Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]);
                                bucle.Vendedor = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                bucle.ColorAgenda = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                bucle.HoraInicioAgenda = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]);
                                bucle.DesTipoCliente = oIDataReader[oIDataReader.GetOrdinal("desTipoCliente")].ToString();
                                bucle.DesFechaHoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]).ToString("dd/MM/yyyy HH:mm:ss tt");
                                bucle.HoraFinAgenda = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]);
                                bucle.AsuntoAgenda = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString();
                               // bucle.DescEstadoAgenda = oIDataReader[oIDataReader.GetOrdinal("DescEstado")].ToString();
                               // bucle.Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>";
                                bucle.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                bucle.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);
                                bucle.DesFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm:ss tt");
                                bucle.EstadoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                bucle.DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString();
                                //bucle.imagenActualizar = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "../Imagenes/innovatec/denegar.png" : "../Imagenes/innovatec/checkActualizar.png";
                                //bucle.BotonVerAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]) == 1 ? "<button onclick='VerInformes(" + Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]) + ")' type='button' class='btn btn-primary btn-sm' style='color: #FFFFFF;background-color: rgb(0, 140, 205);border-color: #010101;'>Ver Agenda</button>" : "";                                
                                bucle.imgTipoActividad = oIDataReader[oIDataReader.GetOrdinal("imgTipoActividad")].ToString();
                                bucle.DescTipoActividad = oIDataReader[oIDataReader.GetOrdinal("DescTipoActividad")].ToString();

                                lista.Add(bucle);

                            }

                        }

                    }
                }
            }
            return lista;
        }

        public AgendaSeguimientoDTO uspListarGridAgendaGeneralAuditoria_NumeroRegistros(AgendaSeguimientoDTO oAgendaSeguimientoDTO)
        {
            AgendaSeguimientoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarGridAgendaGeneralAuditoria_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.Buscador;
                    cmd.Parameters.Add(new SqlParameter("@fechaDesde", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@fechaHasta", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaHasta;                    
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreador", System.Data.SqlDbType.VarChar)).Value = oAgendaSeguimientoDTO.UsuarioCreacion;
                   
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new AgendaSeguimientoDTO()
                                {
                                    CantTotal = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])                                  
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
      

        public List<AgendaSeguimientoDTO> uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor(AgendaSeguimientoDTO oAgendaSeguimientoDTO)
        {
            List<AgendaSeguimientoDTO> lista = new List<AgendaSeguimientoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaSeguimientoDTO.CodSede;
                    cmd.Parameters.Add(new SqlParameter("@fechaDesde", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@fechaHasta", System.Data.SqlDbType.DateTime)).Value = oAgendaSeguimientoDTO.FechaHasta;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                AgendaSeguimientoDTO bucle = new AgendaSeguimientoDTO();

                                bucle.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("1")))
                                {
                                    bucle.actividad1 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("1")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("2")))
                                {
                                    bucle.actividad2 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("2")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("3")))
                                {
                                    bucle.actividad3 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("3")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("4")))
                                {
                                    bucle.actividad4 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("4")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("5")))
                                {
                                    bucle.actividad5 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("5")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("6")))
                                {
                                    bucle.actividad6 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("6")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("7")))
                                {
                                    bucle.actividad7 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("7")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("8")))
                                {
                                    bucle.actividad8 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("8")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("9")))
                                {
                                    bucle.actividad9 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("9")]);
                                }


                                lista.Add(bucle);

                            }

                        }

                    }
                }
            }
            return lista;
        }


        public void RegistrarAgendaSeguimientoTodos(AgendaSeguimientoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarAgendaSeguimientoTodos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                                                                                   
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar, 15)).Value = item.Color;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;

                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = item.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@Asunto", System.Data.SqlDbType.VarChar, 200)).Value = item.Asunto;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = item.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.Parameters.Add(new SqlParameter("@TipoActividad", System.Data.SqlDbType.Int)).Value = item.TipoActividad;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UspReagendarAgendaSeguimientoTodosCaidos(AgendaSeguimientoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspReagendarAgendaSeguimientoTodosCaidos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                                                                      
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar, 15)).Value = item.Color;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;

                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = item.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@Asunto", System.Data.SqlDbType.VarChar, 200)).Value = item.Asunto;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar, 100)).Value = item.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.Parameters.Add(new SqlParameter("@TipoActividad", System.Data.SqlDbType.Int)).Value = item.TipoActividad;

                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        public int uspValidarExisteCitaAgendaGeneral(int CodigoSocio, int CodigoTipoAgenda, int CodSede, int CodigoUnidadNegocio)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarExisteCitaAgendaGeneral", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoAgenda", System.Data.SqlDbType.Int)).Value = CodigoTipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodSede", System.Data.SqlDbType.Int)).Value = CodSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 15)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.AddWithValue("@Existe", campoRetorno).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }

            return Convert.ToInt32(campoRetorno);
        }
        
        public int uspCerrarCitaClienteAgenda(int CodigoCita,int CodigoCliente, int Tipo, string User, int CodSede, int CodigoUnidadNegocio)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspCerrarCitaClienteAgenda", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoCita", System.Data.SqlDbType.Int)).Value = CodigoCita;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = Tipo;                    
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar,100)).Value = User??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@CodSede", System.Data.SqlDbType.Int)).Value = CodSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    
                    cmd.Parameters.AddWithValue("@Cant", campoRetorno).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Cant"].Value);
                }
            }

            return Convert.ToInt32(campoRetorno);
        }


        public int uspValidarCitaAgendarDesdeCliente(int CodigoSocio, string Vendedor, int CodigoTipoAgenda, int CodSede, int CodigoUnidadNegocio)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarCitaAgendarDesdeCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.Int)).Value = Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoAgenda", System.Data.SqlDbType.Int)).Value = CodigoTipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodSede", System.Data.SqlDbType.VarChar, 100)).Value = CodSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    
                    cmd.Parameters.AddWithValue("@Cant", campoRetorno).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Cant"].Value);
                }
            }

            return Convert.ToInt32(campoRetorno);            
        }


	}
}
