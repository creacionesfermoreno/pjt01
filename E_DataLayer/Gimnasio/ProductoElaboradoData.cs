using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class ProductoElaboradoData
    {

        public List<ProductoElaboradoDTO> Listar(ProductoElaboradoDTO oitem)
        {
            List<ProductoElaboradoDTO> lista = new List<ProductoElaboradoDTO>();
          
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductoElaborado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = oitem.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoElaboradoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    DescripcionCategoria = oIDataReader[oIDataReader.GetOrdinal("DescripcionCategoria")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")])                                   
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<ProductoElaboradoDTO> uspListarProductoElaboradoPorFiltro_Paginacion(ProductoElaboradoDTO oitem, Paging paging)
        {
            List<ProductoElaboradoDTO> lista = new List<ProductoElaboradoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductoElaborado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar,100)).Value = oitem.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoElaboradoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }
        
        public List<ProductoElaboradoDTO> uspListarDiario(ProductoElaboradoDTO oitem)
        {
            List<ProductoElaboradoDTO> lista = new List<ProductoElaboradoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDiario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoElaboradoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Modelo = oIDataReader[oIDataReader.GetOrdinal("Modelo")].ToString(),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCompra")])                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }
        
        public ProductoElaboradoDTO BuscarProductoElaboradoPorCodigo(ProductoElaboradoDTO oItem)
        {
            ProductoElaboradoDTO itemDTO = null;
          
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarProductoElaboradoPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = oItem.CodigoProducto;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProductoElaboradoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(reader[reader.GetOrdinal("CodigoProducto")]),
                                    CodigoCategoria = Convert.ToInt32(reader[reader.GetOrdinal("CodigoCategoria")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(reader[reader.GetOrdinal("PrecioVenta")])                                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;         
        }
        
        public ProductoElaboradoDTO uspListarProductoElaboradoPorFiltro_NumeroRegistros(ProductoElaboradoDTO oItem)
        {         
            ProductoElaboradoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductosPorFiltro_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oItem.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar,100)).Value = oItem.Busqueda;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProductoElaboradoDTO()
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

        public void Registrar(ProductoElaboradoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarProductoElaborado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto ", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = item.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar,100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(ProductoElaboradoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarProductoElaborado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto ", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = item.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(ProductoElaboradoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarProductoElaborado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
            
        }

    }
}
