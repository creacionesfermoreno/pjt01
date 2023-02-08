
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class SuplementosData
	{
	
		public List<SuplementosDTO> uspListarSuplementosPorFiltro_Paginacion(SuplementosDTO oItem, Paging paging)
		{
			 List<SuplementosDTO> lista = new List<SuplementosDTO>();
         
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementosPorFiltro_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oItem.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oItem.Busqueda;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SuplementosDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSuplemento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = oIDataReader[oIDataReader.GetOrdinal("CodigoBarras")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    CantidadMinima = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMinima")])                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;          
		}

        public List<SuplementosDTO> uspListarSuplementos(SuplementosDTO oItem)
        {
            List<SuplementosDTO> lista = new List<SuplementosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@filtro", System.Data.SqlDbType.VarChar,200)).Value = oItem.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SuplementosDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    CodigoSuplemento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = oIDataReader[oIDataReader.GetOrdinal("CodigoBarras")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString()                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<SuplementosDTO> uspListarSuplementosPorCategoria(SuplementosDTO oItem)
        {
            List<SuplementosDTO> lista = new List<SuplementosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementosPorCategoria", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oItem.CodigoCategoria;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SuplementosDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    CodigoSuplemento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString()                                 
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

       public List<SuplementosDTO> uspListarSuplementosComprasPorCategoria(SuplementosDTO oItem)
        {
            List<SuplementosDTO> lista = new List<SuplementosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementosComprasPorCategoria", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oItem.CodigoCategoria;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SuplementosDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    CodigoSuplemento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()                                   
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<SuplementosDTO> uspListarSuplementosComprasPorCategoriaFiltro(SuplementosDTO oSuplementosDTO)
        {
            List<SuplementosDTO> lista = new List<SuplementosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementosComprasPorCategoriaFiltro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSuplementosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSuplementosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar)).Value = oSuplementosDTO.Descripcion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SuplementosDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    CodigoSuplemento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            
            return lista;
        }

        public List<SuplementosDTO> uspListarSuplementosVentasPorCategoria(SuplementosDTO oSuplementosDTO)
        {
            List<SuplementosDTO> lista = new List<SuplementosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementosVentasPorCategoria", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSuplementosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSuplementosDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.VarChar)).Value = oSuplementosDTO.CodigoCategoria;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SuplementosDTO()
                                {
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    CodigoSuplemento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        
        public SuplementosDTO BuscarPorCodigoSuplementos(SuplementosDTO oItem)
		{
			SuplementosDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSuplementosPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSuplemento", System.Data.SqlDbType.Int)).Value = oItem.CodigoSuplemento;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new SuplementosDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSuplemento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSuplemento")]),
                                    CodigoCategoria = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCategoria")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = oIDataReader[oIDataReader.GetOrdinal("CodigoBarras")].ToString(),
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    PrecioCompra = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    CantidadMinima = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMinima")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()                                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;          
		}


        public SuplementosDTO uspListarSuplementosPorFiltro_NumeroRegistros(SuplementosDTO oItem)
        {         
            SuplementosDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSuplementosPorFiltro_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = oItem.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar,200)).Value = oItem.Busqueda;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new SuplementosDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])                                  
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

      
        public void Registrar(SuplementosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarSuplementos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                                                                                                        
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSuplemento", System.Data.SqlDbType.Int)).Value = item.CodigoSuplemento;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = item.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.Descripcion;
                                                                  
                    cmd.Parameters.Add(new SqlParameter("@Detalle", System.Data.SqlDbType.VarChar,200)).Value = item.Detalle;
                    cmd.Parameters.Add(new SqlParameter("@CodigoBarras", System.Data.SqlDbType.VarChar,20)).Value = item.CodigoBarras;
                    cmd.Parameters.Add(new SqlParameter("@imagenUrl", System.Data.SqlDbType.VarChar,200)).Value = item.imagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@PrecioCompra", System.Data.SqlDbType.Decimal)).Value = item.PrecioCompra;
                                                                           
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Int)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@CantidadMinima", System.Data.SqlDbType.Int)).Value = item.CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }		
		}

		
		public void Actualizar(SuplementosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSuplementos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSuplemento", System.Data.SqlDbType.Int)).Value = item.CodigoSuplemento;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = item.CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.Descripcion;

                    cmd.Parameters.Add(new SqlParameter("@Detalle", System.Data.SqlDbType.VarChar, 200)).Value = item.Detalle;
                    cmd.Parameters.Add(new SqlParameter("@CodigoBarras", System.Data.SqlDbType.VarChar, 20)).Value = item.CodigoBarras;
                    cmd.Parameters.Add(new SqlParameter("@imagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.imagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@PrecioCompra", System.Data.SqlDbType.Decimal)).Value = item.PrecioCompra;

                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Int)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@CantidadMinima", System.Data.SqlDbType.Int)).Value = item.CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }            
		}

        public void ActualizarPrecioVentaCantidadSuplementos(SuplementosDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPrecioVentaCantidadSuplementos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSuplemento", System.Data.SqlDbType.Int)).Value = item.CodigoSuplemento;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Int)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", System.Data.SqlDbType.Decimal)).Value = item.PrecioVenta;

                    cmd.Parameters.Add(new SqlParameter("@flag", System.Data.SqlDbType.Int)).Value = item.flag;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                  
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public void Eliminar(SuplementosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarSuplementos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSuplemento", System.Data.SqlDbType.Int)).Value = item.CodigoSuplemento;                                  
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
