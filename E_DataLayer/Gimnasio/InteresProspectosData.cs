
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class InteresProspectosData
	{
		public List<InteresProspectosDTO> uspListarInteresProspectos(InteresProspectosDTO oitem)
		{
			List<InteresProspectosDTO> lista = new List<InteresProspectosDTO>();
        
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarInteresProspectos", conn))
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
                                lista.Add(new InteresProspectosDTO()
                                {
                                    CodigoInteres = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInteres")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()                                
                                });
                            }
                        }

                    }
                }
            }
            return lista;         
		}
        
        public void Registrar(InteresProspectosDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarInteresProspectos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInteres", System.Data.SqlDbType.Int)).Value = item.CodigoInteres;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar,100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }         
        }
        
        public void Eliminar(InteresProspectosDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarInteresProspectos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInteres", System.Data.SqlDbType.Int)).Value = item.CodigoInteres;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;                    
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
