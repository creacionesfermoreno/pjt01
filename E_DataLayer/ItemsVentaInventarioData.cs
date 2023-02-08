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
    public class ItemsVentaInventarioData
    {
        public void ecommerce_uspRegistrarItemsVentaInventario(ItemsVentaInventarioDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarItemsVentaInventario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVentaInventario", System.Data.SqlDbType.Int)).Value = item.CodigoItemsVentaInventario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadMedida", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadMedida;
                    cmd.Parameters.Add(new SqlParameter("@CostoUnidad", System.Data.SqlDbType.Decimal)).Value = item.CostoUnidad;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@CantidadInicial", System.Data.SqlDbType.Decimal)).Value = item.CantidadInicial;
                    cmd.Parameters.Add(new SqlParameter("@CantidadMinima", System.Data.SqlDbType.Decimal)).Value = item.CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@CantidadMaxima", System.Data.SqlDbType.Decimal)).Value = item.CantidadMaxima;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void ecommerce_uspActualizarItemsVentaInventario(ItemsVentaInventarioDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarItemsVentaInventario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVentaInventario", System.Data.SqlDbType.Int)).Value = item.CodigoItemsVentaInventario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadMedida", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadMedida;
                    cmd.Parameters.Add(new SqlParameter("@CostoUnidad", System.Data.SqlDbType.Decimal)).Value = item.CostoUnidad;
                    cmd.Parameters.Add(new SqlParameter("@CantidadInicial", System.Data.SqlDbType.Decimal)).Value = item.CantidadInicial;
                    cmd.Parameters.Add(new SqlParameter("@CantidadMinima", System.Data.SqlDbType.Decimal)).Value = item.CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@CantidadMaxima", System.Data.SqlDbType.Decimal)).Value = item.CantidadMaxima;
                   
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public List<ItemsVentaInventarioDTO> ecommerce_uspListarItemsVentaInventario(ItemsVentaInventarioDTO oFiltro)
        {
            List<ItemsVentaInventarioDTO> lista = new List<ItemsVentaInventarioDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarItemsVentaInventario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoItemVenta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaInventarioDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    CodigoItemsVentaInventario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemsVentaInventario")]),
                                    CostoUnidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoUnidad")].ToString()),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    DesAlmacen = oIDataReader[oIDataReader.GetOrdinal("DesAlmacen")].ToString(),
                                    CantidadInicial = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadInicial")]),
                                    CantidadMinima = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMinima")]),
                                    CantidadMaxima = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMaxima")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm tt")
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ItemsVentaInventarioDTO> ecommerce_uspListarMovimientoItemVentaPorItemVenta_Paginaciones(ItemsVentaInventarioDTO oFiltro, Paging paging)
        {
            List<ItemsVentaInventarioDTO> lista = new List<ItemsVentaInventarioDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarMovimientoItemVentaPorItemVenta_Paginaciones", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoItemVenta;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaInventarioDTO()
                                {

                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    CantidadActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadActual")]),
                                    CantidadAjuste = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadAjusteTotal")]),
                                    CantidadFinal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadFinal")]),
                                    CostoUnidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoUnitarioTotal")]),
                                    CodigoTipoAjuste = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoAjuste")]),
                                    DesTipoAjuste = oIDataReader[oIDataReader.GetOrdinal("DesTipoAjuste")].ToString(),
                                    ColorTipoAjuste = oIDataReader[oIDataReader.GetOrdinal("ColorTipoAjuste")].ToString()                                  
                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

    }
}
