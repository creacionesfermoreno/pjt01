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
    public class ItemsVentaData
    {
        public List<ItemsVentaDTO> ecommerce_uspBuscadorItemsVentaInventariable(ItemsVentaDTO oFiltro)
        {
            List<ItemsVentaDTO> lista = new List<ItemsVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscadorItemsVentaInventariable", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@filterNombre", System.Data.SqlDbType.VarChar,100)).Value = oFiltro.Nombre;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")].ToString()),
                                    PrecioTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioTotal")].ToString()),
                                    CodigoTipoImpuesto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoImpuesto")]),
                                    CodigoUnidadMedida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadMedida")]),
                                    CodigoTipoItem = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoItem")]),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    ItemInventariable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ItemInventariable")]),
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    CodigoCategoriaItem = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoriaItem")]),
                                    CodigoProductoSUNAT = oIDataReader[oIDataReader.GetOrdinal("CodigoProductoSUNAT")].ToString(),
                                    CodigoCuentaContable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCuentaContable")]),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    CodigoImagen = oIDataReader[oIDataReader.GetOrdinal("CodigoImagen")].ToString(),
                                    d_CantidadActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadActual")]),
                                    d_CostoUnidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoUnidad")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ItemsVentaDTO> ecommerce_uspListarItemsVenta_Paginacion(ItemsVentaDTO oFiltro, Paging paging)
        {
            List<ItemsVentaDTO> lista = new List<ItemsVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarItemsVenta_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    if (oFiltro.Nombre == string.Empty || oFiltro.Nombre == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oFiltro.Nombre;
                    }

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")].ToString()),
                                    PrecioTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioTotal")].ToString()),
                                    CodigoTipoImpuesto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoImpuesto")]),
                                    CodigoUnidadMedida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadMedida")]),
                                    CodigoTipoItem = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoItem")]),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    ItemInventariable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ItemInventariable")]),
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    CodigoCategoriaItem = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoriaItem")]),
                                    CodigoProductoSUNAT = oIDataReader[oIDataReader.GetOrdinal("CodigoProductoSUNAT")].ToString(),
                                    CodigoCuentaContable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCuentaContable")]),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()

                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ItemsVentaDTO> ecommerce_uspListarItemsVenta_PorCategoriaPaginacion(ItemsVentaDTO oFiltro, Paging paging)
        {
            List<ItemsVentaDTO> lista = new List<ItemsVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarItemsVenta_PorCategoriaPaginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoImagen", System.Data.SqlDbType.VarChar)).Value = oFiltro.CodigoImagen;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")].ToString()),                                   
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),                                    
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    CodigoImagen = oIDataReader[oIDataReader.GetOrdinal("CodigoImagen")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ItemsVentaDTO> ecommerce_uspListarValorInventario_Paginaciones(ItemsVentaDTO oFiltro, Paging paging)
        {
            List<ItemsVentaDTO> lista = new List<ItemsVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarValorInventario_Paginaciones", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar,100)).Value = oFiltro.Nombre == null ? string.Empty : oFiltro.Nombre;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")].ToString()),
                                    d_CantidadActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadActual")].ToString()),
                                    d_CostoPromedio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoPromedio")].ToString()),
                                    d_CostoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoTotal")].ToString()),
                                    CodigoUnidadMedida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadMedida")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<ItemsVentaDTO> ecommerce_uspListarValorInventario_PuntoVenta(ItemsVentaDTO oFiltro)
        {
            List<ItemsVentaDTO> lista = new List<ItemsVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarValorInventario_PuntoVenta", conn))
                {
                    
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoAlmacen;
                    if (oFiltro.Nombre == string.Empty || oFiltro.Nombre == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oFiltro.Nombre;
                    }
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    CodigoImagen = oIDataReader[oIDataReader.GetOrdinal("CodigoImagen")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")].ToString()),
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    d_CantidadActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadActual")].ToString()),                                   
                                    CodigoUnidadMedida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadMedida")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    VisualizarTiendaVirtual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("VisualizarTiendaVirtual")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //*********************************************** API *******************************************

        public List<ItemsVentaDTO> ListProductByCategory(ItemsVentaDTO oFiltro)
        {
            List<ItemsVentaDTO> lista = new List<ItemsVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarValorInventario_PuntoVentaApp", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoMenu;
                    if (oFiltro.Nombre == string.Empty || oFiltro.Nombre == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oFiltro.Nombre;
                    }

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ItemsVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    CodigoImagen = oIDataReader[oIDataReader.GetOrdinal("CodigoImagen")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")].ToString()),
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    d_CantidadActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadActual")].ToString()),
                                    CodigoUnidadMedida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadMedida")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    VisualizarTiendaVirtual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("VisualizarTiendaVirtual")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        //*********************************************** API *******************************************
        public ItemsVentaDTO ecommerce_uspBuscarItemsVentas(ItemsVentaDTO oFiltro)
        {
            ItemsVentaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarItemsVentas", conn))
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
                                itemDTO = new ItemsVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    PrecioTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioTotal")]),
                                    CodigoTipoImpuesto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoImpuesto")]),
                                    CodigoUnidadMedida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadMedida")]),
                                    CodigoTipoItem = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoItem")]),
                                    CodigoAlmacen = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAlmacen")]),
                                    ItemInventariable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ItemInventariable")]),
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    CodigoCategoriaItem = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoriaItem")]),
                                    CodigoProductoSUNAT = oIDataReader[oIDataReader.GetOrdinal("CodigoProductoSUNAT")].ToString(),
                                    CodigoCuentaContable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCuentaContable")]),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    VisualizarTiendaVirtual = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("VisualizarTiendaVirtual")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    CodigoImagen = oIDataReader[oIDataReader.GetOrdinal("CodigoImagen")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public ItemsVentaDTO ecommerce_uspBuscarItemsVentasTienda(ItemsVentaDTO oFiltro)
        {
            ItemsVentaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarItemsVentasTienda", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoImagen", System.Data.SqlDbType.VarChar)).Value = oFiltro.CodigoImagen;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ItemsVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoItemVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoItemVenta")]),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    PrecioTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioTotal")]),
                                    
                                    ItemInventariable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ItemInventariable")]),
                                    Referencia = oIDataReader[oIDataReader.GetOrdinal("Referencia")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    CodigoCategoriaItem = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ItemInventariable")]),

                                    
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    CodigoImagen = oIDataReader[oIDataReader.GetOrdinal("CodigoImagen")].ToString(),
                                    d_CantidadActual = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CantidadActual")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public int ecommerce_uspRegistrarItemsVenta(ItemsVentaDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarItemsVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;
                    
                    if (!string.IsNullOrEmpty(item.Nombre))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombre;
                    }
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@PrecioTotal", System.Data.SqlDbType.Decimal)).Value = item.PrecioTotal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoImpuesto", System.Data.SqlDbType.Int)).Value = item.CodigoTipoImpuesto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadMedida", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadMedida;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoItem", System.Data.SqlDbType.Int)).Value = item.CodigoTipoItem;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@ItemInventariable", System.Data.SqlDbType.Int)).Value = item.ItemInventariable;
                    cmd.Parameters.Add(new SqlParameter("@Referencia", System.Data.SqlDbType.VarChar, 100)).Value = item.Referencia;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoriaItem", System.Data.SqlDbType.Int)).Value = item.CodigoCategoriaItem;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProductoSUNAT", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoProductoSUNAT;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCuentaContable", System.Data.SqlDbType.Int)).Value = item.CodigoCuentaContable;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar,1000)).Value = item.UrlImagen;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@VisualizarTiendaVirtual", System.Data.SqlDbType.Int)).Value = item.VisualizarTiendaVirtual;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoItemVenta"].Value);
                }

            }
            return resultado;
        }

        public void ecommerce_uspActualizarItemsVenta(ItemsVentaDTO item)
        {
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarItemsVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemVenta;

                    if (!string.IsNullOrEmpty(item.Nombre))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombre;
                    }
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@PrecioTotal", System.Data.SqlDbType.Decimal)).Value = item.PrecioTotal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoImpuesto", System.Data.SqlDbType.Int)).Value = item.CodigoTipoImpuesto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadMedida", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadMedida;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoItem", System.Data.SqlDbType.Int)).Value = item.CodigoTipoItem;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", System.Data.SqlDbType.Int)).Value = item.CodigoAlmacen;
                    cmd.Parameters.Add(new SqlParameter("@ItemInventariable", System.Data.SqlDbType.Int)).Value = item.ItemInventariable;
                    cmd.Parameters.Add(new SqlParameter("@Referencia", System.Data.SqlDbType.VarChar, 100)).Value = item.Referencia;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoriaItem", System.Data.SqlDbType.Int)).Value = item.CodigoCategoriaItem;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProductoSUNAT", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoProductoSUNAT;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCuentaContable", System.Data.SqlDbType.Int)).Value = item.CodigoCuentaContable;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 1000)).Value = item.UrlImagen;
                    cmd.Parameters.Add(new SqlParameter("@VisualizarTiendaVirtual", System.Data.SqlDbType.Int)).Value = item.VisualizarTiendaVirtual;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }
        
        public void ecommerce_uspActualizarItemsVentaFoto(ItemsVentaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarItemsVentaFoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoImagen", System.Data.SqlDbType.VarChar, 1000)).Value = item.CodigoImagen;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 1000)).Value = item.UrlImagen;
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public void ecommerce_uspEliminarItemsVenta(ItemsVentaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspEliminarItemsVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemVenta;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }

    }
}
