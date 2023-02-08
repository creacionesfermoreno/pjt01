using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel;
using E_DataModel.Common;

namespace E_DataLayer
{
    public class ComprobanteDetalleData
    {

        public List<ComprobanteDetalleDTO> CentroEntrenamiento_uspListarComprobanteDetalleParaAnular(ComprobanteDetalleDTO oFiltro)
        {
            List<ComprobanteDetalleDTO> lista = new List<ComprobanteDetalleDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarComprobanteDetalleParaAnular", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoComprobante;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprobanteDetalleDTO()
                                {
                                    CodigoComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoComprobante")]),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")].ToString()),                                   
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    CodigoItemsVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemsVenta")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),                                 
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public List<ComprobanteDetalleDTO> CentroEntrenamiento_uspListarDeudasCliente(ComprobanteDetalleDTO oFiltro)
        {
            List<ComprobanteDetalleDTO> lista = new List<ComprobanteDetalleDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarDeudasCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;                    
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.Identificacion;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprobanteDetalleDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoComprobante")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoComprobante")]),
                                    CodigoComprobanteDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoComprobanteDetalle")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    NombresCliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    Identificacion = oIDataReader[oIDataReader.GetOrdinal("Identificacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")].ToString()),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),
                                    CodigoItemsVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemsVenta")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),                                  
                                    Correlativo = oIDataReader[oIDataReader.GetOrdinal("Correlativo")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString()                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        
        public List<ComprobanteDetalleDTO> CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(ComprobanteDetalleDTO oFiltro, Paging paging)
        {
            List<ComprobanteDetalleDTO> lista = new List<ComprobanteDetalleDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oFiltro.request_FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.request_Fin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.request_Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.request_Counter;   
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oFiltro.request_Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oFiltro.request_Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oFiltro.request_FormaPago;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprobanteDetalleDTO()
                                {
                                    CodigoComprobante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoComprobante")]),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    NombresCliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    Identificacion = oIDataReader[oIDataReader.GetOrdinal("Identificacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")].ToString()),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Precio")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    DesFormaPago =  oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    Correlativo = oIDataReader[oIDataReader.GetOrdinal("Correlativo")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString(),
                                    DesSubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ComprobanteDetalleDTO CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros(ComprobanteDetalleDTO oFiltro)
        {
            ComprobanteDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oFiltro.request_FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.request_Fin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.request_Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.request_Counter;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oFiltro.request_Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oFiltro.request_Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oFiltro.request_FormaPago;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ComprobanteDetalleDTO()
                                {
                                    CantidadTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadTotal")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public int ecommerce_uspRegistrarComprobanteDetalle(ComprobanteDetalleDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarComprobanteDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobanteDetalle", System.Data.SqlDbType.Int)).Direction =  ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = item.CodigoMenuSuperior;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemsVenta;
                    cmd.Parameters.Add(new SqlParameter("@Referencia", System.Data.SqlDbType.VarChar, 100)).Value = item.Referencia;
                    cmd.Parameters.Add(new SqlParameter("@Precio", System.Data.SqlDbType.Decimal)).Value = item.Precio;
                    cmd.Parameters.Add(new SqlParameter("@Descuento", System.Data.SqlDbType.Decimal)).Value = item.Descuento;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoImpuesto", System.Data.SqlDbType.Int)).Value = item.CodigoTipoImpuesto;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Decimal)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = item.Total;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@FechaCreacion", System.Data.SqlDbType.DateTime)).Value = item.FechaCreacion;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoComprobanteDetalle"].Value);
                }

            }
            return resultado;
        }

        public int ecommerce_uspRegistrarComprobanteDetalleTiendaVirtual(ComprobanteDetalleDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarComprobanteDetalleTiendaVirtual", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobanteDetalle", System.Data.SqlDbType.Int)).Value = item.CodigoComprobanteDetalle;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemsVenta;
                    cmd.Parameters.Add(new SqlParameter("@Referencia", System.Data.SqlDbType.VarChar, 100)).Value = item.Referencia;
                    cmd.Parameters.Add(new SqlParameter("@Precio", System.Data.SqlDbType.Decimal)).Value = item.Precio;
                    cmd.Parameters.Add(new SqlParameter("@Descuento", System.Data.SqlDbType.Decimal)).Value = item.Descuento;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoImpuesto", System.Data.SqlDbType.Int)).Value = item.CodigoTipoImpuesto;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Decimal)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = item.Total;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoImagen", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoImagen;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();

                }

            }
            return resultado;
        }

    }
}
