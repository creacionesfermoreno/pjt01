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
    public class ItemsVentaAjusteInventarioDetalleData
    {
        public void ecommerce_uspRegistrarItemsVentaAjusteInventarioDetalle(ItemsVentaAjusteInventarioDetalleDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarItemsVentaAjusteInventarioDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVentaAjusteInventario", System.Data.SqlDbType.Int)).Value = item.CodigoItemsVentaAjusteInventario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVentaAjusteInventarioDetalle", System.Data.SqlDbType.Int)).Value = item.CodigoItemsVentaAjusteInventarioDetalle;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;

                    cmd.Parameters.Add(new SqlParameter("@CantidadActual", System.Data.SqlDbType.Decimal)).Value = item.CantidadActual;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoAjuste", System.Data.SqlDbType.Int)).Value = item.CodigoTipoAjuste;
                    cmd.Parameters.Add(new SqlParameter("@CantidadAjuste", System.Data.SqlDbType.Decimal)).Value = item.CantidadAjuste;
                    cmd.Parameters.Add(new SqlParameter("@CantidadFinal", System.Data.SqlDbType.Decimal)).Value = item.CantidadFinal;
                    cmd.Parameters.Add(new SqlParameter("@CostoUnidad", System.Data.SqlDbType.Decimal)).Value = item.CostoUnidad;
                    cmd.Parameters.Add(new SqlParameter("@TotalAjuste", System.Data.SqlDbType.Decimal)).Value = item.TotalAjuste;                    
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public List<ItemsVentaAjusteInventarioDetalleDTO> ecommerce_uspListarItemsVentaAjusteInventarioDetalle(ItemsVentaAjusteInventarioDetalleDTO oFiltro)
        {
            List<ItemsVentaAjusteInventarioDetalleDTO> lista = new List<ItemsVentaAjusteInventarioDetalleDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarItemsVentaAjusteInventarioDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVentaAjusteInventario", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoItemsVentaAjusteInventario;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaAjusteInventarioDetalleDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoItemsVentaAjusteInventario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemsVentaAjusteInventario")]),
                                    CodigoItemsVentaAjusteInventarioDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemsVentaAjusteInventarioDetalle")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    NombreItemVenta = oIDataReader[oIDataReader.GetOrdinal("NombreItemVenta")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    CantidadActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadActual")].ToString()),
                                    CodigoTipoAjuste = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoAjuste")]),
                                    DesTipoAjuste = oIDataReader[oIDataReader.GetOrdinal("DesTipoAjuste")].ToString(),
                                    CantidadAjuste = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadAjuste")].ToString()),
                                    CantidadFinal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadFinal")].ToString()),
                                    CostoUnidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoUnidad")].ToString()),
                                    TotalAjuste = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalAjuste")].ToString()),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")].ToString())
                                    
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
