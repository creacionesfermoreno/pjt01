using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace E_DataLayer.Gimnasio
{
    public class VentasData
    {
        public VentasDTO uspValidarNroComprobante(VentasDTO oItem)
        {
            int? campoRetorno = 0;
            VentasDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarNroComprobante", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoComprobante", System.Data.SqlDbType.Int)).Value = oItem.CodigoTipoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@SubTipoComprobante", System.Data.SqlDbType.Int)).Value = oItem.CodigoSubTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@nro", System.Data.SqlDbType.Int)).Value = oItem.NroComprobante;
                    cmd.Parameters.AddWithValue("@Existe", campoRetorno).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                        itemDTO = new VentasDTO()
                        {
                            ValidadorSerie = Convert.ToInt32(campoRetorno)
                        };

                    }
                }
            }
            return itemDTO;
        }

        //si se usa
        public List<VentasDTO> BuscarInformacionDetalleDeVentaPorCodigo(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarInformacionDetalleDeVentaPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDetalleIngreso", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoVenta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    DescripcionProducto = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    ImporteProducto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }



        public List<VentasDTO> VentaDiarioPorCodigo(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarInformacionGeneralVentaDiarioPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NroComprobante", System.Data.SqlDbType.VarChar)).Value = oVentasDTO.NroComprobante;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    NombreEmpresa = reader[reader.GetOrdinal("NombreEmpresa")].ToString(),
                                    DireccionEmpresa = reader[reader.GetOrdinal("DireccionEmpresa")].ToString(),
                                    DistritoEmpresa = reader[reader.GetOrdinal("DistritoEmpresa")].ToString(),

                                    RucEmpresa = reader[reader.GetOrdinal("RucEmpresa")].ToString(),
                                    TelefonoEmpresa = reader[reader.GetOrdinal("TelefonoEmpresa")].ToString(),
                                    CelularEmpresa = reader[reader.GetOrdinal("CelularEmpresa")].ToString(),
                                    Frase = reader[reader.GetOrdinal("Frase")].ToString(),

                                    CorreoEmpresa = reader[reader.GetOrdinal("CorreoEmpresa")].ToString(),

                                    NombreCliente = reader[reader.GetOrdinal("NombreCliente")].ToString(),
                                    RUC_DNI = reader[reader.GetOrdinal("RucDni")].ToString(),

                                    DistritoCliente = reader[reader.GetOrdinal("DistritoCliente")].ToString(),
                                    DireccionCliente = reader[reader.GetOrdinal("DireccionCliente")].ToString(),
                                    DireccionDistritoCliente = reader[reader.GetOrdinal("DireccionCliente")].ToString() != "" ? (reader[reader.GetOrdinal("DireccionCliente")].ToString() != "" ? (reader[reader.GetOrdinal("DireccionCliente")].ToString() + " - " + reader[reader.GetOrdinal("DistritoCliente")].ToString()) : reader[reader.GetOrdinal("DireccionCliente")].ToString()) : "",
                                    NroComprobante = reader[reader.GetOrdinal("NroComprobante")].ToString(),
                                    TotalNeto = Convert.ToDecimal(reader[reader.GetOrdinal("TotalNeto")]),

                                    DescripcionProducto = reader[reader.GetOrdinal("Descripcion")].ToString(),
                                    Cantidad = Convert.ToInt32(reader[reader.GetOrdinal("Cantidad")]),
                                    DescFormaPago = reader[reader.GetOrdinal("FormaPago")].ToString(),
                                    UsuarioCreacion = reader[reader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescHoraVenta = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")]).ToString("HH:mm tt"),
                                    DescFechaVenta = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),


                                    Dia = DateTime.Now.Day,
                                    Mes = DateTime.Now.Month,
                                    Anio = DateTime.Now.Year,
                                    TipoComprobante = reader[reader.GetOrdinal("TipoComprobante")].ToString(),
                                    Correlativo_Comprobante = reader[reader.GetOrdinal("Correlativo_Comprobante")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        //si se usa
        public VentasDTO BuscarInformacionGeneralVentaPorCodigo(VentasDTO oVentasDTO)
        {
            VentasDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarInformacionGeneralVentaPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.VarChar, 100)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIngreso", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoVenta;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDTO()
                                {
                                    NombreEmpresa = reader[reader.GetOrdinal("NombreEmpresa")].ToString(),
                                    RucEmpresa = reader[reader.GetOrdinal("RucEmpresa")].ToString(),
                                    TelefonoEmpresa = reader[reader.GetOrdinal("TelefonoEmpresa")].ToString(),
                                    CelularEmpresa = reader[reader.GetOrdinal("CelularEmpresa")].ToString(),
                                    DireccionEmpresa = reader[reader.GetOrdinal("DireccionEmpresa")].ToString(),
                                    DistritoEmpresa = reader[reader.GetOrdinal("DistritoEmpresa")].ToString(),
                                    CorreoEmpresa = reader[reader.GetOrdinal("CorreoEmpresa")].ToString(),

                                    NombreCliente = reader[reader.GetOrdinal("NombreCliente")].ToString(),
                                    RUC_DNI = reader[reader.GetOrdinal("RucDni")].ToString(),
                                    DistritoCliente = reader[reader.GetOrdinal("DistritoCliente")].ToString(),
                                    DireccionCliente = reader[reader.GetOrdinal("DireccionCliente")].ToString(),
                                    DireccionDistritoCliente = reader[reader.GetOrdinal("DireccionCliente")].ToString() != "" ? (reader[reader.GetOrdinal("DireccionCliente")].ToString() != "" ? (reader[reader.GetOrdinal("DireccionCliente")].ToString() + " - " + reader[reader.GetOrdinal("DistritoCliente")].ToString()) : reader[reader.GetOrdinal("DireccionCliente")].ToString()) : "",

                                    NroComprobante = reader[reader.GetOrdinal("NroComprobante")].ToString(),

                                    SubTotal = Convert.ToDecimal(reader[reader.GetOrdinal("SubTotal")]),
                                    IGV = Convert.ToDecimal(reader[reader.GetOrdinal("IGV")]),
                                    TotalNeto = Convert.ToDecimal(reader[reader.GetOrdinal("TotalNeto")]),
                                    CostoPlan = Convert.ToDecimal(reader[reader.GetOrdinal("Costo")]),

                                    Debe = Convert.ToDecimal(reader[reader.GetOrdinal("Debe")]),
                                    DescFormaPago = reader[reader.GetOrdinal("FormaPago")].ToString(),
                                    UsuarioCreacion = reader[reader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescHoraVenta = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")]).ToString("HH:mm tt"),
                                    DescFechaVenta = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),


                                    Frase = reader[reader.GetOrdinal("Frase")].ToString(),
                                    Tipo_Conf_Comprobante = Convert.ToInt32(reader[reader.GetOrdinal("Tipo_Conf_Comprobante")]),
                                    Dia = DateTime.Now.Day,
                                    Mes = DateTime.Now.Month,
                                    Anio = DateTime.Now.Year,
                                    TipoComprobante = reader[reader.GetOrdinal("TipoComprobante")].ToString(),
                                    Correlativo_Comprobante = reader[reader.GetOrdinal("Correlativo_Comprobante")].ToString()
                                };
                            }
                        }
                    }
                }
            }

            //using (ERPProcesosDataContext dc = new ERPProcesosDataContext(Helper.Conexion()))
            //{
            //    var query = dc.uspBuscarInformacionGeneralVentaPorCodigo(oVentasDTO.CodigoUnidadNegocio,oVentasDTO.CodigoSede, oVentasDTO.CodigoVenta);
            //    foreach (var item in query)
            //    {
            //        itemDTO = new VentasDTO()
            //        {
            //            NombreEmpresa = item.NombreEmpresa,
            //            DireccionEmpresa = item.DireccionEmpresa,
            //            DistritoEmpresa = item.DistritoEmpresa,
            //            DescFechaVenta = item.FechaHoraVenta,
            //            NombreCliente = item.NombreCliente,
            //            RUC_DNI = item.RucDni,
            //            DireccionCliente = item.DireccionCliente,
            //            NroComprobante = item.NroComprobante,
            //            SubTotal = Convert.ToDecimal(item.SubTotal),
            //            IGV = Convert.ToDecimal(item.IGV),
            //            TotalNeto = Convert.ToDecimal(item.TotalNeto),
            //            DescFormaPago = item.FormaPago,
            //            UsuarioCreacion = item.UsuarioCreacion,
            //            RucEmpresa = item.RucEmpresa,
            //            TelefonoEmpresa = item.TelefonoEmpresa,
            //            DistritoCliente = item.DistritoCliente,
            //            CorreoEmpresa = item.CorreoEmpresa,
            //            DireccionDistritoCliente = item.DireccionCliente != "" ? (item.DistritoCliente != "" ? (item.DireccionCliente + " - " + item.DistritoCliente) : item.DireccionCliente) : "",
            //            Frase = item.Frase,
            //            Tipo_Conf_Comprobante = item.Tipo_Conf_Comprobante,
            //            Dia = DateTime.Now.Day,
            //            Mes = DateTime.Now.Month,
            //            Anio = DateTime.Now.Year,
            //            TipoComprobante = item.TipoComprobante,
            //            Correlativo_Comprobante = item.Correlativo_Comprobante
            //        };
            //    }
            //}

            return itemDTO;
        }

        //si se usa
        public List<VentasDTO> uspListarControlSalidaPorFechaAnular_Paginacion(VentasDTO oVentasDTO, Paging paging)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlSalidaPorFechaAnular_Paginacion", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    CodigoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    RazonSocial_Sr = oIDataReader[oIDataReader.GetOrdinal("RazonSocial_Sr")].ToString(),
                                    RUC_DNI = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]),
                                    Hora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("hh:mm:ss tt"),
                                    CodigoTipoComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]),

                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    NombreComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]) == 1 ? "Factura" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]) == 2 ? "Boleta" : "Otros"),
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    SubTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")]),
                                    IGV = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IGV")]),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalNeto")]),
                                    FormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]),
                                    TipoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")]),
                                    tipoCambio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TipoCambio")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Anulado",
                                    flagEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "" : "none",
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }
        //si se usa
        public VentasDTO uspListarControlSalidaPorFechaAnular_NumeroRegistros(VentasDTO oItem)
        {
            VentasDTO itemDTO = null;
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlSalidaPorFechaAnular_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new VentasDTO()
                                {
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        //si se usa
        public List<VentasDTO> uspListarVentasRapidasAnular_Paginacion(VentasDTO oVentasDTO, Paging paging)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarVentasRapidasAnular_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    CodigoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    RazonSocial_Sr = oIDataReader[oIDataReader.GetOrdinal("RazonSocial_Sr")].ToString(),
                                    RUC_DNI = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]),
                                    Hora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyy hh:mm:ss tt"),
                                    CodigoTipoComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]),

                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    NombreComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]) == 1 ? "Factura" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]) == 2 ? "Boleta" : "Otros"),
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    SubTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")]),
                                    IGV = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IGV")]),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalNeto")]),
                                    FormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]),
                                    TipoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")]),
                                    tipoCambio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TipoCambio")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Anulado",
                                    flagEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "" : "none",
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        //si se usa
        public VentasDTO uspListarVentasRapidasAnular_NumeroRegistros(VentasDTO oItem)
        {
            VentasDTO itemDTO = null;
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarVentasRapidasAnular_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new VentasDTO()
                                {
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        //si se usa
        public List<VentasDTO> uspListarDeudasSuplementosClientes(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDeudasSuplementosClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    CodigoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    RazonSocial_Sr = oIDataReader[oIDataReader.GetOrdinal("RazonSocial_Sr")].ToString(),
                                    RUC_DNI = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]),
                                    Hora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    CodigoTipoComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    NombreComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]) == 1 ? "Factura" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]) == 2 ? "Boleta" : "Otros"),
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    SubTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")]),
                                    IGV = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IGV")]),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalNeto")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    FormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]),
                                    TipoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")]),
                                    tipoCambio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TipoCambio")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Anulado",
                                    flagEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "" : "none",
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<VentasDTO> uspListarDeudasRopasClientes(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDeudasRopasClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    CodigoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    RazonSocial_Sr = oIDataReader[oIDataReader.GetOrdinal("RazonSocial_Sr")].ToString(),
                                    RUC_DNI = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]),
                                    Hora = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    CodigoTipoComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    NombreComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]) == 1 ? "Factura" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]) == 2 ? "Boleta" : "Otros"),
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    SubTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")]),
                                    IGV = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IGV")]),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalNeto")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    FormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]),
                                    TipoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")]),
                                    tipoCambio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TipoCambio")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Anulado",
                                    flagEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "" : "none",
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }


        //si se usa
        public List<VentasDTO> ListarControlSalidaFacturacionMensual(VentasDTO oControlSalida)
        {
            List<VentasDTO> controlSalidaList = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ListarCierreVentasPorMes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oControlSalida.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oControlSalida.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Anio", System.Data.SqlDbType.VarChar, 50)).Value = oControlSalida.FechaVenta.Year;
                    cmd.Parameters.Add(new SqlParameter("@Mes", System.Data.SqlDbType.VarChar, 50)).Value = oControlSalida.FechaVenta.Month;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new VentasDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaVenta")))
                                {
                                    itemDTO.FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroDocumento")))
                                {
                                    itemDTO.RUC_DNI = oIDataReader[oIDataReader.GetOrdinal("NroDocumento")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("RazonSocial")))
                                {
                                    itemDTO.RazonSocial_Sr = (oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("TipoDocumento")))
                                {
                                    itemDTO.CodigoSubTipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroComprobante")))
                                {
                                    itemDTO.NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Moneda")))
                                {
                                    itemDTO.TipoMoneda = oIDataReader[oIDataReader.GetOrdinal("Moneda")].ToString() == "SOLES" ? 1 : 0;
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("SubTotal")))
                                {
                                    itemDTO.SubTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("IGV")))
                                {
                                    itemDTO.IGV = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IGV")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Total")))
                                {
                                    itemDTO.TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]);
                                }
                                controlSalidaList.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return controlSalidaList;
        }
        //si se usa
        public int Registrar(VentasDTO item)
        {
            int? campoRetorno = 0;
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarControlSalida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.AddWithValue("@CodigoIngreso", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@RazonSocial_Sr", System.Data.SqlDbType.VarChar, 200)).Value = item.RazonSocial_Sr;
                    cmd.Parameters.Add(new SqlParameter("@RUC_DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.RUC_DNI;

                    cmd.Parameters.Add(new SqlParameter("@Direccion ", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@FechaVenta", System.Data.SqlDbType.DateTime)).Value = item.FechaVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoTipoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSubTipoDocumento", System.Data.SqlDbType.Int)).Value = item.CodigoSubTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@NroComprobante", System.Data.SqlDbType.VarChar, 100)).Value = item.NroComprobante;

                    cmd.Parameters.Add(new SqlParameter("@NroTarjeta", System.Data.SqlDbType.VarChar, 100)).Value = item.NroTarjeta;
                    cmd.Parameters.Add(new SqlParameter("@SubTotal", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = item.SubTotal;
                    cmd.Parameters.Add(new SqlParameter("@IGV", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = item.IGV;
                    cmd.Parameters.Add(new SqlParameter("@TotalNeto", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = item.TotalNeto;

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;

                    cmd.Parameters.Add(new SqlParameter("@Comentario", System.Data.SqlDbType.VarChar, 100)).Value = item.Comentario;
                    cmd.Parameters.Add(new SqlParameter("@TipoMoneda", System.Data.SqlDbType.Int)).Value = item.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = item.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoCambio", System.Data.SqlDbType.Decimal)).Value = item.tipoCambio;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;


                    cmd.Parameters.Add(new SqlParameter("@TotalNetoDolares", System.Data.SqlDbType.Decimal)).Value = item.TotalDolares;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoIngreso"].Value);
                }
            }

            return Convert.ToInt32(campoRetorno);
        }

        //************************************************************ FOR API ******************************


        public int RegistrarAPP(VentasDTO item)
        {
            int? campoRetorno = 0;
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarControlSalidaApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoIngreso", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar)).Value = item.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@RazonSocial_Sr", System.Data.SqlDbType.VarChar, 200)).Value = item.RazonSocial_Sr;
                    cmd.Parameters.Add(new SqlParameter("@RUC_DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.RUC_DNI;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoTipoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSubTipoDocumento", System.Data.SqlDbType.Int)).Value = item.CodigoSubTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@NroComprobante", System.Data.SqlDbType.VarChar, 100)).Value = item.NroComprobante;
                    cmd.Parameters.Add(new SqlParameter("@TotalNeto", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = item.TotalNeto;
                    cmd.Parameters.Add(new SqlParameter("@Comentario", System.Data.SqlDbType.VarChar, 100)).Value = item.Comentario;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = item.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@NroTarjeta", System.Data.SqlDbType.VarChar)).Value = item.NroTarjeta;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoIngreso"].Value);
                }
            }

            return Convert.ToInt32(campoRetorno);
        }

        //************************************************************ FOR API ******************************
        //si se usa
        public void ActualizarEstado(VentasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarControlSalidaEstado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede ", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoControlSalida ", System.Data.SqlDbType.Int)).Value = item.CodigoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@UserAuditoria ", System.Data.SqlDbType.VarChar, 100)).Value = string.Empty;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion ", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }

        }

        //si se usa
        public List<VentasDTO> uspEstadisticaVentasPorEvolucionTicketPromedio_Ventas(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaVentasPorEvolucionTicketPromedio_Ventas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar)).Value = oVentasDTO.AsesorComercial;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    Anio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Anio")]),
                                    MesDescripcion = convertirMesTexto(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        //si se usa
        public List<VentasDTO> uspListarEstadistica_VentasDiarios(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEstadistica_VentasDiarios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioAsistencia", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinAsistencia", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    MesDescripcion = Convert.ToString(oIDataReader[oIDataReader.GetOrdinal("Descripcion")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TotalCantidad")]),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporte")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }


        public List<VentasDTO> uspEstadisticaVentasPorTiempoMembresia_Ventas(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaVentasPorTiempoMembresia_Ventas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar)).Value = oVentasDTO.AsesorComercial ?? String.Empty;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    DescripcionProducto = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public List<VentasDTO> uspEstadisticaMatriculadosPorNombrePlan(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaMatriculadosPorNombrePlan", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar)).Value = oVentasDTO.AsesorComercial;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]),
                                    DescripcionProducto = oIDataReader[oIDataReader.GetOrdinal("DesPlan")].ToString(),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Venta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }


        public List<VentasDTO> uspEstadisticaVentasPorDiaSemana_Ventas(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaVentasPorDiaSemana_Ventas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar)).Value = oVentasDTO.AsesorComercial;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    Dia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaSemana")]),
                                    DescripcionProducto = oIDataReader[oIDataReader.GetOrdinal("DesSemana")].ToString(),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }


        public List<VentasDTO> uspEstadisticaVentasPorDia_Ventas(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaVentasPorDia_Ventas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar)).Value = oVentasDTO.AsesorComercial;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    Dia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Dia")]),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public List<VentasDTO> uspEstadisticaVentasPorHoras_Ventas(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaVentasPorHoras_Ventas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar)).Value = oVentasDTO.AsesorComercial;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    HoraDia = oIDataReader[oIDataReader.GetOrdinal("HoraDia")].ToString(),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public List<VentasDTO> uspEstadisticaVentasPorFormaPago_Ventas(VentasDTO oVentasDTO)
        {
            List<VentasDTO> lista = new List<VentasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEstadisticaVentasPorFormaPago_Ventas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oVentasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oVentasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar)).Value = oVentasDTO.AsesorComercial;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDTO()
                                {
                                    HoraDia = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")])
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }


        public string convertirMesTexto(int mes)
        {
            string mesTexto = "";
            switch (mes)
            {
                case 1:
                    mesTexto = "Enero";
                    break;
                case 2:
                    mesTexto = "Febrero";
                    break;
                case 3:
                    mesTexto = "Marzo";
                    break;
                case 4:
                    mesTexto = "Abril";
                    break;
                case 5:
                    mesTexto = "Mayo";
                    break;
                case 6:
                    mesTexto = "Junio";
                    break;
                case 7:
                    mesTexto = "Julio";
                    break;
                case 8:
                    mesTexto = "Agosto";
                    break;
                case 9:
                    mesTexto = "Setiembre";
                    break;
                case 10:
                    mesTexto = "Octubre";
                    break;
                case 11:
                    mesTexto = "Noviembre";
                    break;
                case 12:
                    mesTexto = "Diciembre";
                    break;
            }

            return mesTexto;
        }

    }
}
