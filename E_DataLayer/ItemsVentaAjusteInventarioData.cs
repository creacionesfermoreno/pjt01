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
    public class ItemsVentaAjusteInventarioData
    {

        public List<ItemsVentaAjusteInventarioDTO> ecommerce_uspListarItemsVentaAjusteInventario_Paginacion(ItemsVentaAjusteInventarioDTO oFiltro, Paging paging)
        {
            List<ItemsVentaAjusteInventarioDTO> lista = new List<ItemsVentaAjusteInventarioDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarItemsVentaAjusteInventario_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVentaAjusteInventario", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoItemsVentaAjusteInventario;
                    cmd.Parameters.Add(new SqlParameter("@DesAlmacen", System.Data.SqlDbType.VarChar, 100)).Value = oFiltro.DesAlmacen;
                    if (oFiltro.b_FechaAjusteInicio != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaAjusteInicio", System.Data.SqlDbType.DateTime)).Value = oFiltro.b_FechaAjusteInicio;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaAjusteInicio", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    }

                    if (oFiltro.b_FechaAjusteFin != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaAjusteFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.b_FechaAjusteFin;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaAjusteFin", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    }

                    
                    cmd.Parameters.Add(new SqlParameter("@Observaciones", System.Data.SqlDbType.VarChar, 300)).Value = oFiltro.Observaciones;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaAjusteInventarioDTO()
                                {
                                    CodigoItemsVentaAjusteInventario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemsVentaAjusteInventario")]),
                                    FechaAjuste = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaAjuste")].ToString()),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    DesAlmacen = oIDataReader[oIDataReader.GetOrdinal("DesAlmacen")].ToString(),
                                    TotalAjuste = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalAjuste")].ToString()),
                                    Observaciones = oIDataReader[oIDataReader.GetOrdinal("Observaciones")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public int ecommerce_uspRegistrarItemsVentaAjusteInventario(ItemsVentaAjusteInventarioDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarItemsVentaAjusteInventario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemsVentaAjusteInventario", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@FechaAjuste", System.Data.SqlDbType.DateTime)).Value = item.FechaAjuste;
                    cmd.Parameters.Add(new SqlParameter("@Observaciones", System.Data.SqlDbType.VarChar, 100)).Value = item.Observaciones;
                    cmd.Parameters.Add(new SqlParameter("@TotalAjuste", System.Data.SqlDbType.Decimal)).Value = item.TotalAjuste;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoItemsVentaAjusteInventario"].Value);
                }

            }
            return resultado;
        }


    }
}
