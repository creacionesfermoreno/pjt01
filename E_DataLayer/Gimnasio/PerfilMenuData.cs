using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class PerfilMenuData
	{

        public List<PerfilMenuDTO> Listar(PerfilMenuDTO item)
		{
			List<PerfilMenuDTO> lista = new List<PerfilMenuDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarPerfilMenu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PerfilMenuDTO()
                                {
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    ControlTotal = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("ControlTotal")]),
                                    Escritura = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Escritura")]),
                                    Lectura = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Lectura")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Url = oIDataReader[oIDataReader.GetOrdinal("Url")].ToString(),
                                    Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")])                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}


        public List<PerfilMenuDTO> SEGListarPerfilMenuPermisos(PerfilMenuDTO item)
		{
			 List<PerfilMenuDTO> lista = new List<PerfilMenuDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarPerfilMenuPermisos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PerfilMenuDTO()
                                {
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    CodigoMenu = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenu")]),
                                    CodigoMenuSuperior = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMenuSuperior")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    deschecked = Convert.ToString(Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "checked" : "")                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}


        public void SEGRegistrarPerfilMenuPermisos(PerfilMenuDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGRegistrarPerfilMenuPermisos", conn))
                {                                                                                 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenu", System.Data.SqlDbType.Int)).Value = item.CodigoMenu;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
            
		}


       public void SEGEliminarPerfilMenuPermisos(PerfilMenuDTO item)
	   {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGEliminarPerfilMenuPermisos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;
                   
                    cmd.ExecuteNonQuery();
                }
            }
		}


	}
}
