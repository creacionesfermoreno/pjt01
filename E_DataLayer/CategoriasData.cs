using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using E_DataModel;
using E_DataModel.Corporativo;
using E_DataModel.Common;

namespace E_DataLayer
{
    public class CategoriasData
    {
        public List<CategoriasDTO> ecommerce_uspListarCategorias_Edit(CategoriasDTO oFiltro)
        {
            List<CategoriasDTO> lista = new List<CategoriasDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarCategorias_Edit", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoMenuSuperior;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CategoriasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    UrlUbicacion = oIDataReader[oIDataReader.GetOrdinal("UrlUbicacion")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    CodigoImagenPortada = oIDataReader[oIDataReader.GetOrdinal("CodigoImagenPortada")].ToString(),
                                    Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
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

        //************************************** API *****************************************

        public List<CategoriasDTO> CategoryListApi(CategoriasDTO oFiltro)
        {
            List<CategoriasDTO> lista = new List<CategoriasDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarCategoriasApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CategoriasDTO()
                                {
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    UrlUbicacion = oIDataReader[oIDataReader.GetOrdinal("UrlUbicacion")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    CodigoImagenPortada = oIDataReader[oIDataReader.GetOrdinal("CodigoImagenPortada")].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }


        //************************************** API *****************************************


        public CategoriasDTO ecommerce_uspBuscarCategorias(CategoriasDTO oFiltro)
        {
            CategoriasDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarCategorias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoMenuSuperior;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CategoriasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    UrlUbicacion = oIDataReader[oIDataReader.GetOrdinal("UrlUbicacion")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public CategoriasDTO ecommerce_uspBuscarCategoriasTiendaVirtual(CategoriasDTO oFiltro)
        {
            CategoriasDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarCategoriasTiendaVirtual", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoImagenPortada", System.Data.SqlDbType.VarChar,100)).Value = oFiltro.CodigoImagenPortada;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CategoriasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    UrlUbicacion = oIDataReader[oIDataReader.GetOrdinal("UrlUbicacion")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void ecommerce_uspRegistrarCategorias(CategoriasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarCategorias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = item.CodigoMenuSuperior;
                    
                    if (!string.IsNullOrEmpty(item.Descripcion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.Descripcion;
                    }
                    cmd.Parameters.Add(new SqlParameter("@UrlUbicacion", System.Data.SqlDbType.VarChar, 500)).Value = item.UrlUbicacion;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 500)).Value = item.UrlImagen;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.VarChar, 500)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Orden", System.Data.SqlDbType.Int)).Value = item.Orden;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }                    
                    cmd.ExecuteNonQuery();
                    //ID = cmd.Parameters["@po_CodigoProducto"].Value.ToString();
                }

            }
        }

        public void ecommerce_uspActualizarCategorias(CategoriasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarCategorias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = item.CodigoMenuSuperior;

                    if (!string.IsNullOrEmpty(item.Descripcion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.Descripcion;
                    }
                    cmd.Parameters.Add(new SqlParameter("@UrlUbicacion", System.Data.SqlDbType.VarChar, 500)).Value = item.UrlUbicacion;
                    cmd.Parameters.Add(new SqlParameter("@Orden", System.Data.SqlDbType.Int)).Value = item.Orden;
                    
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void ecommerce_uspActualizarCatalogoPortadaFoto(CategoriasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarCatalogoPortadaFoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@CodigoImagen", System.Data.SqlDbType.VarChar)).Value = item.CodigoImagenPortada;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar)).Value = item.UrlImagen;
                    
                    cmd.ExecuteNonQuery();
                }

            }
        }
        
        public void ecommerce_uspEliminarCategorias(CategoriasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspEliminarCategorias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = item.CodigoMenuSuperior;

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
