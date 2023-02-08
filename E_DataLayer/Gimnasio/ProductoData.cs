using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class ProductoData
    {

        public List<ProductoDTO> Listar(ProductoDTO oitem)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProducto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oitem.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oitem.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString(),
                                    CodigoMarca = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMarca")]),
                                    DesMarca = oIDataReader[oIDataReader.GetOrdinal("DesMarca")].ToString(),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = oIDataReader[oIDataReader.GetOrdinal("CodigoBarras")].ToString(),
                                    Modelo = oIDataReader[oIDataReader.GetOrdinal("Modelo")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    imagenURL = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    CantidadMinima = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMinima")]),
                                    DescripcionEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> uspListarProductosPorFiltro_Paginacion(ProductoDTO oitem, Paging paging)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductosPorFiltro_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oitem.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oitem.Busqueda;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString(),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = oIDataReader[oIDataReader.GetOrdinal("CodigoBarras")].ToString(),
                                    Modelo = oIDataReader[oIDataReader.GetOrdinal("Modelo")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    imagenURL = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    CantidadMinima = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMinima")]),
                                    DescripcionEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> ListarTodo(ProductoDTO oitem)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTodoProducto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@filtro", System.Data.SqlDbType.VarChar, 200)).Value = oitem.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString(),
                                    CodigoMarca = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMarca")]),
                                    DesMarca = oIDataReader[oIDataReader.GetOrdinal("DesMarca")].ToString(),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = oIDataReader[oIDataReader.GetOrdinal("CodigoBarras")].ToString(),
                                    Modelo = oIDataReader[oIDataReader.GetOrdinal("Modelo")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    imagenURL = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    DescripcionEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> uspListarProductoPorCategoria(ProductoDTO oitem)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductoPorCategoria", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oitem.CodigoCategoria;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDetalle")]),
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

        public List<ProductoDTO> uspListarProductoPorCategoriaCompra(ProductoDTO oitem)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductoPorCategoriaCompra", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oitem.CodigoCategoria;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> uspListarProductoPorCategoriaVenta(ProductoDTO oProductoDTO)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductoPorCategoriaVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoCategoria;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> ListarPorNombre(ProductoDTO oProductoDTO)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductoPorNombre", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = oProductoDTO.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.codigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDetalle")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Modelo = oIDataReader[oIDataReader.GetOrdinal("Modelo")].ToString(),
                                    tipoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    fechaCompra = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCompra")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    AsesorComercial = oIDataReader[oIDataReader.GetOrdinal("AsesorComercial")].ToString(),
                                    TipoIngreso = oIDataReader[oIDataReader.GetOrdinal("TipoIngreso")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> uspListarProductoBuscadorPorNombre(ProductoDTO oProductoDTO)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductoBuscadorPorNombre", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.codigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar)).Value = oProductoDTO.Descripcion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    CodigoDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    Color = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]) == 20 || Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]) == 30 ? "rgb(255, 0, 0)" : "rgb(0, 0, 0)"
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> uspListarDeudasSuplementoRopaDelSocio(ProductoDTO oProductoDTO)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDeudasSuplementoRopaDelSocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.codigoSocio;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    CodigoDetalle = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                    Color = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]) == 20 || Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]) == 30 ? "rgb(255, 0, 0)" : "rgb(0, 0, 0)"
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> uspListarProductoPorCategoriaCompraFiltro(ProductoDTO oProductoDTO)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductoPorCategoriaCompraFiltro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar)).Value = oProductoDTO.Descripcion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public ProductoDTO BuscarPorCodigo(ProductoDTO oProductoDTO)
        {
            ProductoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarProductoPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoProducto;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProductoDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(reader[reader.GetOrdinal("CodigoCategoria")]),
                                    CodigoMarca = Convert.ToInt32(reader[reader.GetOrdinal("CodigoMarca")]),
                                    CodigoProducto = Convert.ToInt32(reader[reader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = reader[reader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = reader[reader.GetOrdinal("CodigoBarras")].ToString(),
                                    Modelo = reader[reader.GetOrdinal("Modelo")].ToString(),
                                    PrecioCompra = Convert.ToDecimal(reader[reader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(reader[reader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(reader[reader.GetOrdinal("Cantidad")]),
                                    CantidadMinima = Convert.ToInt32(reader[reader.GetOrdinal("CantidadMinima")]),
                                    imagenURL = reader[reader.GetOrdinal("imagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]),
                                    DescripcionEstado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]) ? "Activo" : "Inactivo"
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ProductoDTO uspListarProductosPorFiltro_NumeroRegistros(ProductoDTO oProductoDTO)
        {
            ProductoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductosPorFiltro_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oProductoDTO.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oProductoDTO.Busqueda;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProductoDTO()
                                {
                                    CantTotal = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
                return itemDTO;
            }
        }



        public List<ProductoDTO> uspListarKardexProductos_Paginacion(ProductoDTO oitem)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarKardexProductos_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Buscador ", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesProducto")].ToString(),
                                    ImporteIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporteIngreso")]),
                                    ImporteSalida = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporteSalida")]),
                                    CantidadIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCantidadIngreso")]),
                                    CantidadSalida = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCantidadSalida")]),
                                    StockActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("StockActual")]),
                                    EstadoStockActual = oIDataReader[oIDataReader.GetOrdinal("DesEstadoStock")].ToString(),
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> uspListarKardexSuplementos_Paginacion(ProductoDTO oitem)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarKardexSuplementos_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Buscador ", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesProducto")].ToString(),
                                    ImporteIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporteIngreso")]),
                                    ImporteSalida = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporteSalida")]),
                                    CantidadIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCantidadIngreso")]),
                                    CantidadSalida = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCantidadSalida")]),
                                    StockActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("StockActual")]),
                                    EstadoStockActual = oIDataReader[oIDataReader.GetOrdinal("DesEstadoStock")].ToString(),
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        public List<ProductoDTO> uspListarKardexRopas_Paginacion(ProductoDTO oitem)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarKardexRopas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@Buscador ", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesProducto")].ToString(),
                                    ImporteIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporteIngreso")]),
                                    ImporteSalida = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporteSalida")]),
                                    CantidadIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCantidadIngreso")]),
                                    CantidadSalida = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCantidadSalida")]),
                                    StockActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("StockActual")]),
                                    EstadoStockActual = oIDataReader[oIDataReader.GetOrdinal("DesEstadoStock")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        public ProductoDTO uspListarHistorialCompraProductos_NumeroRegistros(ProductoDTO oitem)
        {
            ProductoDTO ItemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHistorialCompraProductos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                ItemDTO = new ProductoDTO
                                {
                                    CantidadRegistro = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    TotalImporte = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporte")])
                                };
                                
                            }
                        }
                    }
                }
            }
            return ItemDTO;
        }
        public List<ProductoDTO> uspListarHistorialCompraProductos_Paginacion(ProductoDTO oitem, Paging paging)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using(var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using(var cmd = new SqlCommand("uspListarHistorialCompraProductos_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    FechaCompra = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCompra")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesProducto")].ToString(),
                                    CantidadIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadIngreso")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    GananciaUnitaria = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaUnitaria")]),
                                    GananciaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaTotal")]),
                                    Importe = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Importe")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }

        public List<ProductoDTO> uspListarHistorialCompraSuplementos_Paginacion(ProductoDTO oitem, Paging paging)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using(var cmd = new SqlCommand("uspListarHistorialCompraSuplementos_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using(SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesProducto")].ToString(),
                                    CantidadIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadIngreso")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    GananciaUnitaria = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaUnitaria")]),
                                    GananciaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaTotal")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()
                                });
                            }
                        }
                    }
                }

            }
            return lista;
        }

        public ProductoDTO uspListarHistorialCompraSuplementos_NumeroRegistros(ProductoDTO oitem)
        {
            ProductoDTO ItemDTO = null;
            using(var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using(var cmd = new SqlCommand("uspListarHistorialCompraSuplementos_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    using(SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                ItemDTO = new ProductoDTO
                                {
                                    CantidadRegistro = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    TotalImporte = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporte")])
                                };
                            }
                        }
                    }
                }
            }
            return ItemDTO;
        }

        public List<ProductoDTO> uspListarHistorialCompraRopas_Paginacion(ProductoDTO oitem, Paging paging)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHistorialCompraRopas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("DesProducto")].ToString(),
                                    CantidadIngreso = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadIngreso")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    GananciaUnitaria = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaUnitaria")]),
                                    GananciaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("GananciaTotal")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()
                                });
                            }
                        }
                    }
                }

            }
            return lista;
        }

        public ProductoDTO uspListarHistorialCompraRopas_NumeroRegistros(ProductoDTO oitem)
        {
            ProductoDTO ItemDTO = null;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHistorialCompraRopas_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oitem.Busqueda;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                ItemDTO = new ProductoDTO
                                {
                                    CantidadRegistro = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")]),
                                    TotalImporte = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalImporte")])
                                };
                            }
                        }
                    }
                }
            }
            return ItemDTO;
        }

        public void Registrar(ProductoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarProducto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                 
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria ", System.Data.SqlDbType.Int)).Value = item.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMarca", System.Data.SqlDbType.Int)).Value = item.CodigoMarca;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar,200)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Detalle", System.Data.SqlDbType.VarChar,200)).Value = item.Detalle;

                    cmd.Parameters.Add(new SqlParameter("@CodigoBarras", System.Data.SqlDbType.VarChar,15)).Value = item.CodigoBarras;
                    cmd.Parameters.Add(new SqlParameter("@Modelo", System.Data.SqlDbType.VarChar, 100)).Value = item.Modelo;
                    //Parameters.Add(new SqlParameter("@logo", System.Data.SqlDbType.Image)).Value = item.logo;
                    cmd.Parameters.Add(new SqlParameter("@ImageUrl", System.Data.SqlDbType.VarChar,200)).Value = string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@PrecioCompra", System.Data.SqlDbType.Decimal)).Value = item.PrecioCompra;

                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Int)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@CantidadMinima", System.Data.SqlDbType.Int)).Value = item.CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                   
                    cmd.ExecuteNonQuery();
                }
            }	  
		}

		public void Actualizar(ProductoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarProducto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria ", System.Data.SqlDbType.Int)).Value = item.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMarca", System.Data.SqlDbType.Int)).Value = item.CodigoMarca;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Detalle", System.Data.SqlDbType.VarChar, 200)).Value = item.Detalle;

                    cmd.Parameters.Add(new SqlParameter("@CodigoBarras", System.Data.SqlDbType.VarChar, 15)).Value = item.CodigoBarras;
                    cmd.Parameters.Add(new SqlParameter("@Modelo", System.Data.SqlDbType.VarChar, 100)).Value = item.Modelo;
                    cmd.Parameters.Add(new SqlParameter("@logo", System.Data.SqlDbType.Image)).Value = item.logo;
                    cmd.Parameters.Add(new SqlParameter("@ImageUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.imagenURL;
                    cmd.Parameters.Add(new SqlParameter("@PrecioCompra", System.Data.SqlDbType.Decimal)).Value = item.PrecioCompra;

                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Int)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@CantidadMinima", System.Data.SqlDbType.Int)).Value = item.CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }            
		}

        public void ActualizarPrecioVentaCantidad(ProductoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarProductoPrecioCantidad", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Int)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;

                    cmd.Parameters.Add(new SqlParameter("@flag", System.Data.SqlDbType.Int)).Value = item.flag;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

		public void Eliminar(ProductoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarProducto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = item.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}

	}
}
