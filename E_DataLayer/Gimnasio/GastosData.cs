using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class GastosData
	{
        
        public List<GastosDTO> uspReporteEgresoRangoFechas(GastosDTO oGastosDTO)
        {
            List<GastosDTO> lista = new List<GastosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteEgresoRangoFechas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@fechaInicio", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaFin;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new GastosDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),                                                                    
                                    MontoEgreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoEgreso")])                                 
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public List<GastosDTO> Listar(GastosDTO oGastosDTO)
		{
            List<GastosDTO> lista = new List<GastosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEgresosPorDia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@FechaEgresos", System.Data.SqlDbType.Date)).Value = oGastosDTO.FechaHora;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new GastosDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    FechaHora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHora")]),
                                    Responsable = oIDataReader[oIDataReader.GetOrdinal("DescripcionTipoResponsable")].ToString(),
                                    TipoEgresoDescripcion = oIDataReader[oIDataReader.GetOrdinal("DescripcionTipoEgreso")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    MontoEgreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoEgreso")]),
                                    FirmaAutorizacionDesc = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("FirmaAutorizacion")]) ? "Si" : "No",
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    NumeroRecibo = oIDataReader[oIDataReader.GetOrdinal("NroRecibo")].ToString(),
                                    DescTipoMoneda = oIDataReader[oIDataReader.GetOrdinal("DescTipoMoneda")].ToString()                                   
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}

        public List<GastosDTO> ListarDetalleEgresosCaja(GastosDTO oGastosDTO)
		{
            List<GastosDTO> lista = new List<GastosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleEgresosCaja", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAbrirCaja", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoAbrirCaja;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new GastosDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    MontoEgreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoEgreso")]),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    TipoEgresoDescripcion = oIDataReader[oIDataReader.GetOrdinal("desTipoEgreso")].ToString()                                   
                                });
                            }
                        }

                    }
                }
            }
            return lista;
		}
        
        public List<GastosDTO> uspReporteEgresoRangoFechas_Paginacion(GastosDTO oGastosDTO, Paging paging)
        {
            List<GastosDTO> lista = new List<GastosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteEgresoRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Responsable", System.Data.SqlDbType.VarChar,100)).Value = oGastosDTO.Responsable;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oGastosDTO.Tipo;
                  
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oGastosDTO.Turno;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = oGastosDTO.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@TipoEgreso", System.Data.SqlDbType.Int)).Value = oGastosDTO.TipoEgreso;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new GastosDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    RZProveedor = oIDataReader[oIDataReader.GetOrdinal("RZProveedor")].ToString(),
                                    RUCProveedor = oIDataReader[oIDataReader.GetOrdinal("RUCProveedor")].ToString(),
                                    Observaciones = oIDataReader[oIDataReader.GetOrdinal("Observaciones")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    FechaHora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHora")]),
                                    TipoEgresoDescripcion = oIDataReader[oIDataReader.GetOrdinal("DescripcionEgresos")].ToString(),
                                    MontoEgreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoEgreso")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescTipoMoneda = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    NumeroRecibo = oIDataReader[oIDataReader.GetOrdinal("NroRecibo")].ToString(),
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<GastosDTO> uspReporteEgresoRangoFechas_ExportarExcel(GastosDTO oGastosDTO, Paging paging)
        {
            List<GastosDTO> lista = new List<GastosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteEgresoRangoFechas_ExportarExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Responsable", System.Data.SqlDbType.VarChar, 100)).Value = oGastosDTO.Responsable;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oGastosDTO.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oGastosDTO.Turno;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = oGastosDTO.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@TipoEgreso", System.Data.SqlDbType.Int)).Value = oGastosDTO.TipoEgreso;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new GastosDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    FechaHora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHora")]),
                                    TipoEgresoDescripcion = oIDataReader[oIDataReader.GetOrdinal("DescripcionEgresos")].ToString(),
                                    MontoEgreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoEgreso")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescTipoMoneda = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    NumeroRecibo = oIDataReader[oIDataReader.GetOrdinal("NroRecibo")].ToString(),

                                    RUCProveedor = oIDataReader[oIDataReader.GetOrdinal("RUCProveedor")].ToString(),
                                    RZProveedor = oIDataReader[oIDataReader.GetOrdinal("RZProveedor")].ToString(),
                                    SubTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")]),
                                    Igv = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Igv")]),
                                    OtrosTributos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("OtrosTributos")]),
                                    DesMedioPago = oIDataReader[oIDataReader.GetOrdinal("DesMedioPago")].ToString(),
                                    NroOperacion = oIDataReader[oIDataReader.GetOrdinal("NroOperacion")].ToString(),
                                    Observaciones = oIDataReader[oIDataReader.GetOrdinal("Observaciones")].ToString(),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public GastosDTO uspReporteEgresoRangoFechas_NumeroRegistros(GastosDTO oGastosDTO)
        {
            GastosDTO itemDTO = new GastosDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteEgresoRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Responsable", System.Data.SqlDbType.VarChar, 100)).Value = oGastosDTO.Responsable;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oGastosDTO.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oGastosDTO.Turno;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = oGastosDTO.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@TipoEgreso", System.Data.SqlDbType.Int)).Value = oGastosDTO.TipoEgreso;
                    cmd.Parameters.AddWithValue("@NumeroRegistros",0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new GastosDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    Igv = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Igv")]),
                                    SubTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")]),
                                    TotalGasto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalGasto")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }
        
        public List<GastosDTO> uspReporteEgresoRangoFechas_PaginacionExcel(GastosDTO oGastosDTO, Paging paging)
        {
            List<GastosDTO> lista = new List<GastosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteEgresoRangoFechas_PaginacionExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Responsable", System.Data.SqlDbType.VarChar, 100)).Value = oGastosDTO.Responsable;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oGastosDTO.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oGastosDTO.Turno;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = oGastosDTO.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@TipoEgreso", System.Data.SqlDbType.Int)).Value = oGastosDTO.TipoEgreso;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new GastosDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    FechaHora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHora")]),
                                    TipoEgresoDescripcion = oIDataReader[oIDataReader.GetOrdinal("DescripcionEgresos")].ToString(),
                                    MontoEgreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoEgreso")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescTipoMoneda = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    NumeroRecibo = oIDataReader[oIDataReader.GetOrdinal("NroRecibo")].ToString(),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        /*
         * Author: Angel Rojas
         * Date: 18/01/2021 16:51
         * 
         * */
        public List<GastosDTO> ListarEgresosTotal(GastosDTO oGastosDTO)
        {
            List<GastosDTO> lista = new List<GastosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("uspListarEgresosTotal", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oGastosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@fechaInicio", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", System.Data.SqlDbType.DateTime)).Value = oGastosDTO.FechaFin;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new GastosDTO()
                                {
                                    CodigoEgreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("codigoGasto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("descripcion")].ToString(),
                                    MontoEgreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("montoGasto")]),
                                });
                            }
                        }
                    }
                }
            }

            return lista;
        }
        
        public GastosDTO BuscarPorCodigoEgresos(GastosDTO oEgresos)
		{
            GastosDTO itemDTO = new GastosDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarEgresosPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oEgresos.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oEgresos.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oEgresos.Codigo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new GastosDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    FechaHora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHora")]),
                                    TipoEgreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoEgreso")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    MontoEgreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoEgreso")]),
                                    FirmaAutorizacion = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("FirmaAutorizacion")]),
                                    NumeroRecibo = oIDataReader[oIDataReader.GetOrdinal("NroRecibo")].ToString(),
                                    Responsable = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString(),
                                    TipoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")])                                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}
		
		public void Registrar(GastosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarEgresos", conn))
                {                                                                 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Responsable", System.Data.SqlDbType.VarChar,100)).Value = item.Responsable;
                    cmd.Parameters.Add(new SqlParameter("@TipoEgreso", System.Data.SqlDbType.Int)).Value = item.TipoEgreso;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@MontoEgreso", System.Data.SqlDbType.Decimal)).Value = item.MontoEgreso;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@NroRecibo", System.Data.SqlDbType.VarChar,100)).Value = item.NumeroRecibo;
                    cmd.Parameters.Add(new SqlParameter("@TipoMoneda", System.Data.SqlDbType.Int)).Value = item.TipoMoneda;

                    cmd.Parameters.Add(new SqlParameter("@RUCProveedor", System.Data.SqlDbType.VarChar, 50)).Value = item.RUCProveedor;
                    cmd.Parameters.Add(new SqlParameter("@RZProveedor", System.Data.SqlDbType.VarChar, 50)).Value = item.RZProveedor;
                    cmd.Parameters.Add(new SqlParameter("@SubTotal", System.Data.SqlDbType.Decimal)).Value = item.SubTotal;
                    cmd.Parameters.Add(new SqlParameter("@Igv", System.Data.SqlDbType.Decimal)).Value = item.Igv;
                    cmd.Parameters.Add(new SqlParameter("@OtrosTributos", System.Data.SqlDbType.Decimal)).Value = item.OtrosTributos;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMedioPago", System.Data.SqlDbType.Int)).Value = item.CodigoMedioPago;
                    cmd.Parameters.Add(new SqlParameter("@NroOperacion", System.Data.SqlDbType.VarChar, 20)).Value = item.NroOperacion;
                    cmd.Parameters.Add(new SqlParameter("@Observaciones", System.Data.SqlDbType.VarChar, 70)).Value = item.Observaciones;
                    cmd.Parameters.Add(new SqlParameter("@FechaGasto", System.Data.SqlDbType.DateTime)).Value = item.FechaCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
		}

		public void Actualizar(GastosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarEgresos", conn))
                {                                                                                           
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@FechaHora", System.Data.SqlDbType.DateTime)).Value = item.FechaHora;
                    cmd.Parameters.Add(new SqlParameter("@Responsable", System.Data.SqlDbType.Int)).Value = item.TipoResponsable;
                    cmd.Parameters.Add(new SqlParameter("@TipoEgreso", System.Data.SqlDbType.Int)).Value = item.TipoEgreso;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;

                    cmd.Parameters.Add(new SqlParameter("@MontoEgreso", System.Data.SqlDbType.Decimal)).Value = item.MontoEgreso;
                    cmd.Parameters.Add(new SqlParameter("@FirmaAutorizacion", System.Data.SqlDbType.Bit)).Value = item.FirmaAutorizacion;
                    cmd.Parameters.Add(new SqlParameter("@NroRecibo", System.Data.SqlDbType.VarChar, 100)).Value = item.NumeroRecibo;
                    cmd.Parameters.Add(new SqlParameter("@TipoMoneda", System.Data.SqlDbType.Int)).Value = item.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}

		public void Eliminar(GastosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarEgresos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;

                    cmd.ExecuteNonQuery();
                }
            }            
		}
	}
}
