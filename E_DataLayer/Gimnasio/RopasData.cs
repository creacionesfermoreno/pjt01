
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class RopasData
	{
     
        public List<RopasDTO> uspListarRopasPorFiltro_Paginacion(RopasDTO oRopasDTO, Paging paging)
        {
            List<RopasDTO> lista = new List<RopasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarRopasPorFiltro_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oRopasDTO.Busqueda;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new RopasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoProducto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = oIDataReader[oIDataReader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = oIDataReader[oIDataReader.GetOrdinal("CodigoBarras")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
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

        public RopasDTO uspListarRopasPorFiltro_NumeroRegistros(RopasDTO oRopasDTO)
        {
            RopasDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarRopasPorFiltro_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 200)).Value = oRopasDTO.Busqueda;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new RopasDTO()
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

        public List<RopasDTO> uspListarRopasCompras(RopasDTO oRopasDTO)
        {
            List<RopasDTO> lista = new List<RopasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarRopasCompras", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoSede;
                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lista.Add(new RopasDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(reader[reader.GetOrdinal("CodigoProducto")]),
                                    PrecioVenta = Convert.ToDecimal(reader[reader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(reader[reader.GetOrdinal("Cantidad")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString()                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<RopasDTO> uspListarRopasComprasFiltro(RopasDTO oRopasDTO)
        {
            List<RopasDTO> lista = new List<RopasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarRopasComprasFiltro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar)).Value = oRopasDTO.Descripcion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new RopasDTO()
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

        public List<RopasDTO> uspListarRopasVentas(RopasDTO oRopasDTO)
        {
            List<RopasDTO> lista = new List<RopasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarRopasVentas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoSede;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new RopasDTO()
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
        
        public List<RopasDTO> uspListarRopas(RopasDTO oRopasDTO)
        {
            List<RopasDTO> lista = new List<RopasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarRopas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oRopasDTO.CodigoSede;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lista.Add(new RopasDTO()
                                {
                                    CodigoProducto = Convert.ToInt32(reader[reader.GetOrdinal("CodigoProducto")]),
                                    PrecioVenta = Convert.ToDecimal(reader[reader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(reader[reader.GetOrdinal("Cantidad")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = reader[reader.GetOrdinal("Detalle")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }
        
        public RopasDTO BuscarPorCodigoRopas(RopasDTO oRopas)
		{
            RopasDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarRopasPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oRopas.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oRopas.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = oRopas.CodigoProducto;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new RopasDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    CodigoProducto = Convert.ToInt32(reader[reader.GetOrdinal("CodigoProducto")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString(),
                                    Detalle = reader[reader.GetOrdinal("Detalle")].ToString(),
                                    CodigoBarras = reader[reader.GetOrdinal("CodigoBarras")].ToString(),
                                    ImagenUrl = reader[reader.GetOrdinal("imagenUrl")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]),
                                    PrecioCompra = Convert.ToDecimal(reader[reader.GetOrdinal("PrecioCompra")]),
                                    PrecioVenta = Convert.ToDecimal(reader[reader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(reader[reader.GetOrdinal("Cantidad")]),
                                    CantidadMinima = Convert.ToInt32(reader[reader.GetOrdinal("CantidadMinima")])
                                };
                            }
                        }
                    }
                }

            }
            return itemDTO;         
		}
		
		public void Registrar(RopasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarRopas", conn))
                {
		
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Detalle", System.Data.SqlDbType.VarChar, 200)).Value = item.Detalle;

                    cmd.Parameters.Add(new SqlParameter("@CodigoBarras", System.Data.SqlDbType.VarChar, 200)).Value = item.CodigoBarras;
                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
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
        
		public void Actualizar(RopasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarRopas", conn))
                {                                        
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Detalle", System.Data.SqlDbType.VarChar, 200)).Value = item.Detalle;

                    cmd.Parameters.Add(new SqlParameter("@CodigoBarras", System.Data.SqlDbType.VarChar, 200)).Value = item.CodigoBarras;
                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
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

        public void ActualizarPrecioVentaCantidadRopas(RopasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPrecioVentaCantidadRopas", conn))
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

        public void Eliminar(RopasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarRopas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProducto", System.Data.SqlDbType.Int)).Value = item.CodigoProducto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 200)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int, 200)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}

	}
}
