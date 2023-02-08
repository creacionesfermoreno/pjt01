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
    public class ComprobanteData
    {
        public List<ComprobanteDTO> ecommerce_uspListarComprobanteParaAnular(ComprobanteDTO oFiltro, Paging paging)
        {
            List<ComprobanteDTO> lista = new List<ComprobanteDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarComprobanteParaAnular", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaEmisionInicio", System.Data.SqlDbType.DateTime)).Value = oFiltro.b_FechaEmisionInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaEmisionFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.b_FechaEmisionFin;
                                   
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprobanteDTO()
                                {
                                    CodigoUnidadNegocio    = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede             = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoComprobante      = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoComprobante")]),
                                    CodigoTipoComprobante  = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]),
                                    TipoMoneda             = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")]),
                                    CodigoAlmacen          = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    Correlativo            = oIDataReader[oIDataReader.GetOrdinal("Correlativo")].ToString(),
                                    CodigoCliente          = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    NombresCliente         = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    CodigoVendedor         = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoVendedor")]),
                                    FechaEmision           = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEmision")]),
                                    CodigoPlazoPago        = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPlazoPago")]),
                                    FechaVencimiento       = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
                                    ColorFechaVencimiento  = oIDataReader[oIDataReader.GetOrdinal("ColorFechaVencimiento")].ToString(),
                                    TerminosCondiciones    = oIDataReader[oIDataReader.GetOrdinal("TerminosCondiciones")].ToString(),
                                    Notas                  = oIDataReader[oIDataReader.GetOrdinal("Notas")].ToString(),
                                    Comentarios            = oIDataReader[oIDataReader.GetOrdinal("Comentarios")].ToString(),
                                    SubTotal               = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")].ToString()),
                                    Descuento              = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Descuento")].ToString()),
                                    SubTotal2              = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal2")].ToString()),
                                    IGV                    = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IGV")].ToString()),
                                    Total                  = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")].ToString()),
                                    TotalCobrado           = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCobrado")].ToString()),
                                    TotalPorCobrar         = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalPorCobrar")].ToString()),
                                    Estado                 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado              = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString(),
                                    ColorEstado            = oIDataReader[oIDataReader.GetOrdinal("ColorEstado")].ToString(),
                                   // DesEstadoEntrega = oIDataReader[oIDataReader.GetOrdinal("DesEstadoEntrega")].ToString(),
                                    UsuarioCreacion        = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion          = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ComprobanteDTO> ecommerce_uspListarComprobante(ComprobanteDTO oFiltro, Paging paging,out uint NumeroRegistros)
        {
            List<ComprobanteDTO> lista = new List<ComprobanteDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarComprobante", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    if (oFiltro.CodigoComprobante == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoComprobante;
                    }
                    if (oFiltro.CodigoEstadoEntrega == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoEstadoEntrega", System.Data.SqlDbType.Int)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoEstadoEntrega", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoEstadoEntrega;
                    }
                    if (oFiltro.CodigoCliente ==null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoCliente;
                    }
                    if (oFiltro.b_FechaEmisionInicio == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaEmisionInicio", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaEmisionInicio", System.Data.SqlDbType.DateTime)).Value = oFiltro.b_FechaEmisionInicio;
                    }
                    if (oFiltro.b_FechaEmisionFin == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaEmisionFin", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaEmisionFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.b_FechaEmisionFin;
                    }

                    if (oFiltro.Estado == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoEstado", System.Data.SqlDbType.Int)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoEstado", System.Data.SqlDbType.DateTime)).Value = oFiltro.Estado;
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    SqlParameter outputParam_NumeroRegistros = cmd.Parameters.Add("@NumeroRegistros", SqlDbType.Int);
                    outputParam_NumeroRegistros.Direction = ParameterDirection.Output;
                    //cmd.Parameters.AddWithValue("@NumeroRegistros", NumeroRegistros).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprobanteDTO()
                                {
                                    CodigoUnidadNegocio    = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede             = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoComprobante      = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoComprobante")]),
                                    CodigoTipoComprobante  = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoComprobante")]),
                                    TipoMoneda             = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")]),
                                    CodigoAlmacen          = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    Correlativo            = oIDataReader[oIDataReader.GetOrdinal("Correlativo")].ToString(),
                                    CodigoCliente          = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCliente")]),
                                    NombresCliente         = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    CodigoVendedor         = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoVendedor")]),
                                    FechaEmision           = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEmision")]),
                                    FechaEmision_Texto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEmision")]).ToString("dd/MM/yyyy hh:mm tt"),
                                    CodigoPlazoPago        = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPlazoPago")]),
                                    FechaVencimiento       = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimiento")]),
                                    ColorFechaVencimiento  = oIDataReader[oIDataReader.GetOrdinal("ColorFechaVencimiento")].ToString(),
                                    TerminosCondiciones    = oIDataReader[oIDataReader.GetOrdinal("TerminosCondiciones")].ToString(),
                                    Notas                  = oIDataReader[oIDataReader.GetOrdinal("Notas")].ToString(),
                                    Comentarios            = oIDataReader[oIDataReader.GetOrdinal("Comentarios")].ToString(),
                                    SubTotal               = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal")].ToString()),
                                    Descuento              = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Descuento")].ToString()),
                                    SubTotal2              = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SubTotal2")].ToString()),
                                    IGV                    = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("IGV")].ToString()),
                                    Total                  = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")].ToString()),
                                    TotalCobrado           = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCobrado")].ToString()),
                                    TotalPorCobrar         = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalPorCobrar")].ToString()),
                                    Estado                 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesEstado              = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString(),
                                    ColorEstado            = oIDataReader[oIDataReader.GetOrdinal("ColorEstado")].ToString(),
                                    DesEstadoEntrega = oIDataReader[oIDataReader.GetOrdinal("DesEstadoEntrega")].ToString(),
                                    UsuarioCreacion        = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion          = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    UrlPDF = oIDataReader[oIDataReader.GetOrdinal("UrlPDF")].ToString()
                                });
                            }
                        }

                    }

                    NumeroRegistros = Convert.ToUInt32(outputParam_NumeroRegistros.Value);
                }
            }
            return lista;
        }

        public int ecommerce_uspRegistrarComprobante(ComprobanteDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarComprobante", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoTipoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@TipoMoneda", System.Data.SqlDbType.Int)).Value = item.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@Correlativo", System.Data.SqlDbType.VarChar, 500)).Value = item.Correlativo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoVendedor", System.Data.SqlDbType.Int)).Value = item.CodigoVendedor;
                    cmd.Parameters.Add(new SqlParameter("@FechaEmision", System.Data.SqlDbType.DateTime)).Value = item.FechaEmision;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlazoPago", System.Data.SqlDbType.Int)).Value = item.CodigoPlazoPago;
                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;
                    cmd.Parameters.Add(new SqlParameter("@TerminosCondiciones", System.Data.SqlDbType.VarChar, 500)).Value = item.TerminosCondiciones;
                    cmd.Parameters.Add(new SqlParameter("@Notas", System.Data.SqlDbType.VarChar, 500)).Value = item.Notas;
                    cmd.Parameters.Add(new SqlParameter("@Comentarios", System.Data.SqlDbType.VarChar, 500)).Value = item.Comentarios;
                    cmd.Parameters.Add(new SqlParameter("@SubTotal", System.Data.SqlDbType.Decimal)).Value = item.SubTotal;
                    cmd.Parameters.Add(new SqlParameter("@Descuento", System.Data.SqlDbType.Decimal)).Value = item.Descuento;
                    cmd.Parameters.Add(new SqlParameter("@SubTotal2", System.Data.SqlDbType.Decimal)).Value = item.SubTotal2;
                    cmd.Parameters.Add(new SqlParameter("@Igv", System.Data.SqlDbType.Decimal)).Value = item.IGV;
                    cmd.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = item.Total;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UrlPDF", System.Data.SqlDbType.VarChar,150)).Value = item.UrlPDF;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoComprobante"].Value);
                }

            }
            return resultado;
        }


        public int ecommerce_uspValidar_Registrar_Clientes(ComprobanteDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspValidar_Registrar_Clientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar, 100)).Value = item.NroIdentificacion;
                    
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoCliente"].Value);
                }

            }
            return resultado;
        }

        public int ecommerce_uspRegistrarComprobanteTiendaVirtual(ComprobanteDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarComprobanteTiendaVirtual", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoTipoComprobante;
                    cmd.Parameters.Add(new SqlParameter("@TipoMoneda", System.Data.SqlDbType.Int)).Value = item.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@Correlativo", System.Data.SqlDbType.VarChar, 500)).Value = item.Correlativo;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoCliente", System.Data.SqlDbType.Int)).Value = item.CodigoCliente;
                    cmd.Parameters.Add(new SqlParameter("@CodigoVendedor", System.Data.SqlDbType.Int)).Value = item.CodigoVendedor;
                    //cmd.Parameters.Add(new SqlParameter("@FechaEmision", System.Data.SqlDbType.DateTime)).Value = item.FechaEmision;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlazoPago", System.Data.SqlDbType.Int)).Value = item.CodigoPlazoPago;
                    //cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaVencimiento;
                    cmd.Parameters.Add(new SqlParameter("@TerminosCondiciones", System.Data.SqlDbType.VarChar, 500)).Value = item.TerminosCondiciones;
                    cmd.Parameters.Add(new SqlParameter("@Notas", System.Data.SqlDbType.VarChar, 500)).Value = item.Notas;
                    cmd.Parameters.Add(new SqlParameter("@Comentarios", System.Data.SqlDbType.VarChar, 500)).Value = item.Comentarios;
                    cmd.Parameters.Add(new SqlParameter("@SubTotal", System.Data.SqlDbType.Decimal)).Value = item.SubTotal;
                    cmd.Parameters.Add(new SqlParameter("@Descuento", System.Data.SqlDbType.Decimal)).Value = item.Descuento;
                    cmd.Parameters.Add(new SqlParameter("@SubTotal2", System.Data.SqlDbType.Decimal)).Value = item.SubTotal2;
                    cmd.Parameters.Add(new SqlParameter("@Envio", System.Data.SqlDbType.Decimal)).Value = item.Envio;
                    cmd.Parameters.Add(new SqlParameter("@SubTotal3", System.Data.SqlDbType.Decimal)).Value = item.SubTotal3;
                    cmd.Parameters.Add(new SqlParameter("@Igv", System.Data.SqlDbType.Decimal)).Value = item.IGV;
                    cmd.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = item.Total;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDireccion", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoDireccion;
                    if (item.CodigoCupon == string.Empty)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoCupon", System.Data.SqlDbType.VarChar, 100)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoCupon", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoCupon;
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoComprobante"].Value);
                }

            }
            return resultado;
        }


        public int CentroEntrenamiento_uspEliminarComprobante(ComprobanteDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspEliminarComprobante", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoComprobante", System.Data.SqlDbType.Int)).Value = item.CodigoComprobante;

                    cmd.ExecuteNonQuery();
                    //resultado = Convert.ToInt32(cmd.Parameters["@CodigoComprobante"].Value);
                }

            }
            return resultado;
        }


    }
}
