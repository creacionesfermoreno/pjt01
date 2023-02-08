using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class MenuData
	{
		public List<MenuDTO> Listar()
		{
		    List<MenuDTO> lista = new List<MenuDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MenuDTO()
                                {
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Url = oIDataReader[oIDataReader.GetOrdinal("Url")].ToString(),
                                    Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;          
		}

        public List<MenuDTO> ListarSEG_PermisosPorCodigoMenuSuperior(MenuDTO oitem)
		{
            List<MenuDTO> lista = new List<MenuDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ListarSEG_PermisosPorCodigoMenuSuperior", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = oitem.CodigoMenuSuperior;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MenuDTO()
                                {
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Url = oIDataReader[oIDataReader.GetOrdinal("Url")].ToString(),
                                    Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
            
		}
        
		public MenuDTO BuscarPorCodigoMenu(MenuDTO oitem)
		{
			MenuDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGBuscarMenuPorCodigoMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = oitem.CodigoMenu;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new MenuDTO()
                                {
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Url = oIDataReader[oIDataReader.GetOrdinal("Url")].ToString(),
                                    Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
		}
		
		public int Registrar(MenuDTO item)
		{
		    int ? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGRegistrarMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoMenu", campoRetorno).Direction = System.Data.ParameterDirection.Output;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = item.CodigoMenuSuperior;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Url", System.Data.SqlDbType.VarChar)).Value = item.Url;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.VarChar, 50)).Value = item.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@Orden", System.Data.SqlDbType.Int)).Value = item.Orden;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,50)).Value = item.UsuarioCreacion;
                  
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoMenu"].Value);
                }
            }
         
		  return Convert.ToInt32(campoRetorno);
		}

		public void Actualizar(MenuDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGActualizarMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenuSuperior", System.Data.SqlDbType.Int)).Value = item.CodigoMenuSuperior;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Url", System.Data.SqlDbType.VarChar)).Value = item.Url;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.VarChar, 50)).Value = item.Tipo;

                    cmd.Parameters.Add(new SqlParameter("@Orden", System.Data.SqlDbType.Int)).Value = item.Orden;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }
            }
		 }

		public void Eliminar(MenuDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGEliminarMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = item.CodigoMenu;
                   
                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
