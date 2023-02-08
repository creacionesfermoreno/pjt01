using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class VentasDetalleData
    {

        public List<VentasDetalleDTO> Listar(VentasDetalleDTO oItem)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlDetalleSalida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = oItem.CodigoSalida;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoSalidaDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalidaDetalle")]),
                                    CodigoSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalida")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<VentasDetalleDTO> uspListarDetalleVentasSuplementos(VentasDetalleDTO oItem)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleVentasSuplementos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = oItem.CodigoSalida;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoSalidaDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalidaDetalle")]),
                                    CodigoSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalida")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> uspListarDetalleVentasRopas(VentasDetalleDTO oItem)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleVentasRopas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = oItem.CodigoSalida;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoSalidaDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalidaDetalle")]),
                                    CodigoSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalida")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> ListarControlDetalle_PorCodigoPagoMembresia(VentasDetalleDTO oItem)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlDetalleSalida_PorCodigoProducto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = oItem.CodigoProducto;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoSalidaDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalidaDetalle")]),
                                    CodigoSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalida")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion(VentasDetalleDTO oItem, Paging paging, out int NumeroRegistros)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    //cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    SqlParameter outputParam_NumeroRegistros = cmd.Parameters.Add("@NumeroRegistros", System.Data.SqlDbType.Int);
                    outputParam_NumeroRegistros.Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    TipoIngresoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngresoMembresia")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString()
                                });
                            }
                        }

                    }

                    NumeroRegistros = Convert.ToInt32(outputParam_NumeroRegistros.Value);
                }
            }
            return lista;
        }


        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasRangoFechas_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasMembresiasRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),
                                    TipoIngresoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngresoMembresia")].ToString(),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    codigoDetalle_Ingreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasNutricionRangoFechas_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasNutricionRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),

                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    SubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString(),
                                    TipoIngresoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngresoMembresia")].ToString(),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    codigoDetalle_Ingreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasPersonalizadoRangoFechas_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasPersonalizadoRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),
                                    SubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString(),
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    TipoIngresoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngresoMembresia")].ToString(),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    codigoDetalle_Ingreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasProductosRangoFechas_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasProductosRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    //Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    //TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    //Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasSuplementosTotalesRangoFechas_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasSuplementosTotalesRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;

                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Counter;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Responsable = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString(),
                                    SubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString(),
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasRopasTotalesRangoFechas_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasRopasTotalesRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;

                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;

                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Responsable = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString(),
                                    SubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString(),
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString()

                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> uspListarSuplementosPagosPorFechaAnular_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementosPagosPorFechaAnular_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.Fecha;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPago")]),
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("debe")]),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Responsable = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString(),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Anulado",
                                    flagEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "" : "none",
                                    CodigoSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalida")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //si se usa
        public List<VentasDetalleDTO> uspListarRopasPagosPorFechaAnular_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarRopasPagosPorFechaAnular_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.Fecha;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPago")]),
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("debe")]),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Responsable = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString(),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Anulado",
                                    flagEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "" : "none",
                                    CodigoSalida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalida")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasCafeteriaRangoFechas_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasCafeteriaRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public List<VentasDetalleDTO> uspReporteVentasServiciosRangoFechas_Paginacion(VentasDetalleDTO oItem, Paging paging)
        {
            int? count = 0;
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasServiciosRangoFechas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    //Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    //TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    //Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public VentasDetalleDTO uspReporteVentasRangoFechas_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasMembresiasRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        //si se usa
        public VentasDetalleDTO uspReporteVentasNutricionRangoFechas_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasNutricionRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        //si se usa
        public VentasDetalleDTO uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        //si se usa
        public VentasDetalleDTO uspReporteVentasProductosRangoFechas_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasProductosRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        //si se usa
        public VentasDetalleDTO uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;

                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Counter;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        //si se usa
        public VentasDetalleDTO uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;

                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Counter;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        //si se usa
        public VentasDetalleDTO uspListarSuplementosPagosPorFechaAnular_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementosPagosPorFechaAnular_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.Fecha;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        //si se usa
        public VentasDetalleDTO uspListarRopasPagosPorFechaAnular_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarRopasPagosPorFechaAnular_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.Fecha;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        //si se usa
        public VentasDetalleDTO uspReporteVentasCafeteriaRangoFechas_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasCafeteriaRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        //si se usa
        public VentasDetalleDTO uspReporteVentasServiciosRangoFechas_NumeroRegistros(VentasDetalleDTO oItem)
        {
            VentasDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasServiciosRangoFechas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new VentasDetalleDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("NumeroRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        #region Metodos Excel

        public List<VentasDetalleDTO> uspReporteVentasMembresiasRangoFechas_PaginacionExcel(VentasDetalleDTO oItem, Paging paging)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasMembresiasRangoFechas_PaginacionExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),
                                    DNI_RUC = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    DesTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTiempoPaquete")].ToString(),
                                    SubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString(),
                                    TipoIngresoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngresoMembresia")].ToString(),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString(),
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasServiciosRangoFechas_PaginacionExcel(VentasDetalleDTO oItem, Paging paging)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasServiciosRangoFechas_PaginacionExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),
                                    DNI_RUC = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    SubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasProductosRangoFechas_PaginacionExcel(VentasDetalleDTO oItem, Paging paging)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasProductosRangoFechas_PaginacionExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),
                                    DNI_RUC = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    SubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasSuplementosTotalesRangoFechas_PaginacionExcel(VentasDetalleDTO oItem, Paging paging)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasSuplementosTotalesRangoFechas_PaginacionExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;

                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;

                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();

                                oVentasDetalleDTO.CodigoCliente = oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                oVentasDetalleDTO.Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString();
                                oVentasDetalleDTO.FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt");
                                oVentasDetalleDTO.Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]);
                                oVentasDetalleDTO.PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]);
                                oVentasDetalleDTO.Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
                                oVentasDetalleDTO.Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]);
                                oVentasDetalleDTO.Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]);
                                oVentasDetalleDTO.Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("debe")]);
                                oVentasDetalleDTO.DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString();
                                oVentasDetalleDTO.NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString();
                                oVentasDetalleDTO.Responsable = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString();
                                oVentasDetalleDTO.DNI_RUC = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString();
                                oVentasDetalleDTO.TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString();

                                lista.Add(oVentasDetalleDTO);
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasNutricionRangoFechas_PaginacionExcel(VentasDetalleDTO oItem, Paging paging)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasNutricionRangoFechas_PaginacionExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),
                                    DNI_RUC = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    DesTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTiempoPaquete")].ToString(),
                                    TipoIngresoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngresoMembresia")].ToString(),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasPersonalizadoRangoFechas_PaginacionExcel(VentasDetalleDTO oItem, Paging paging)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasPersonalizadoRangoFechas_PaginacionExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Couter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Mes = obtenerNombreMes(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Mes")])),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    TotalPagando = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedores")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    Facebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString(),
                                    ImgFacebook = oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() == "" ? "" : "<a href=" + oIDataReader[oIDataReader.GetOrdinal("Facebook")].ToString() + " target='_blank'><image src='../Imagenes/Facebook.png' style='width:30px;height:30px;cursor:pointer;'></image></a>",
                                    NroTarjeta = oIDataReader[oIDataReader.GetOrdinal("NroTarjeta")].ToString(),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    Counter = oIDataReader[oIDataReader.GetOrdinal("Counter")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorVentas")].ToString(),
                                    DNI_RUC = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    DesTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTiempoPaquete")].ToString(),
                                    TipoIngresoMembresia = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngresoMembresia")].ToString(),
                                    NroContrato = oIDataReader[oIDataReader.GetOrdinal("NroContrato")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasRopasTotalesRangoFechas_PaginacionExcel(VentasDetalleDTO oItem, Paging paging)
        {
            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();
            int? count = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspReporteVentasRopasTotalesRangoFechas_PaginacionExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oItem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oItem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oItem.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = oItem.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Turno", System.Data.SqlDbType.Int)).Value = oItem.Turno;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = oItem.FormaPago;
                    //cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.VarChar, 50)).Value = oItem.TipoIngresoMembresia;
                    //cmd.Parameters.Add(new SqlParameter("@TipoCliente", System.Data.SqlDbType.Int)).Value = oItem.TipoCliente;

                    cmd.Parameters.Add(new SqlParameter("@Counter", System.Data.SqlDbType.VarChar, 50)).Value = oItem.Counter;
                    //cmd.Parameters.Add(new SqlParameter("@AsesorVentas", System.Data.SqlDbType.VarChar, 50)).Value = oItem.AsesorComercial;
                    //cmd.Parameters.Add(new SqlParameter("@TiempoMembresia", System.Data.SqlDbType.Int)).Value = oItem.CodigoTiempoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", count).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new VentasDetalleDTO()
                                {
                                    TipoComprobante = oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")].ToString(),
                                    Cliente = oIDataReader[oIDataReader.GetOrdinal("Cliente")].ToString(),
                                    FechaVenta = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVenta")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioUnitario = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioUnitario")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    DNI_RUC = oIDataReader[oIDataReader.GetOrdinal("RUC_DNI")].ToString(),
                                    Precio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    Debe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Debe")]),
                                    DescFormaPago = oIDataReader[oIDataReader.GetOrdinal("FormaPago")].ToString(),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("Responsable")].ToString(),

                                    CodigoCliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")])


                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        #endregion

        public string obtenerNombreMes(int nro)
        {

            string n = string.Empty;
            switch (nro)
            {
                case 1:
                    {
                        n = "Enero";
                    }
                    break;
                case 2:
                    {
                        n = "Febrero";
                    }
                    break;
                case 3:
                    {
                        n = "Marzo";
                    }
                    break;
                case 4:
                    {
                        n = "Abril";
                    }
                    break;
                case 5:
                    {
                        n = "Mayo";
                    }
                    break;
                case 6:
                    {
                        n = "Junio";
                    }
                    break;
                case 7:
                    {
                        n = "Julio";
                    }
                    break;
                case 8:
                    {
                        n = "Agosto";
                    }
                    break;
                case 9:
                    {
                        n = "Setiembre";
                    }
                    break;
                case 10:
                    {
                        n = "Octubre";
                    }
                    break;
                case 11:
                    {
                        n = "Noviembre";
                    }
                    break;
                case 12:
                    {
                        n = "Diciembre";
                    }
                    break;
                default:
                    break;
            }

            return n;

        }

        public void Registrar(VentasDetalleDTO item)
        {
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarControlDetalleSalida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = item.CodigoSalida;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalidaDetalle", System.Data.SqlDbType.Int)).Value = item.CodigoSalidaDetalle;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Int)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", System.Data.SqlDbType.Decimal)).Value = item.PrecioUnitario;

                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Importe", System.Data.SqlDbType.Decimal)).Value = item.Importe;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@FechaVenta", System.Data.SqlDbType.DateTime)).Value = item.FechaCreacion;

                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar, 100)).Value = item.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngresoMembre", System.Data.SqlDbType.VarChar, 50)).Value = string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //*********************************************** API ***********************************
        public void RegistrarAPP(VentasDetalleDTO item)
        {
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarControlDetalleSalidaApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar)).Value = item.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalidaDetalle", System.Data.SqlDbType.Int)).Value = 0;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = item.CodigoSalida;
                    cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", System.Data.SqlDbType.Decimal)).Value = item.PrecioUnitario;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Importe", System.Data.SqlDbType.Decimal)).Value = item.Importe;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarAsesorControlDetalleSalida(VentasDetalleDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarAsesorControlDetalleSalida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.CodigoSalidaDetalle;
                    cmd.Parameters.Add(new SqlParameter("@Asesorventas", System.Data.SqlDbType.VarChar, 300)).Value = item.AsesorComercial;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarAsesorComercial(VentasDetalleDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarAsesorComercial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalidaDetalle", System.Data.SqlDbType.Int)).Value = item.CodigoSalidaDetalle;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar, 50)).Value = item.AsesorComercial;

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
