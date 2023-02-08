using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class EnvioCorreoBienvenidaData
	{
		
		public EnvioCorreoBienvenidaDTO BuscarPorCodigoEnvioCorreoBienvenida(EnvioCorreoBienvenidaDTO oEnvioCorreoBienvenida)
		{
            EnvioCorreoBienvenidaDTO itemDTO = new EnvioCorreoBienvenidaDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarEnvioCorreoBienvenidaPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oEnvioCorreoBienvenida.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oEnvioCorreoBienvenida.CodigoSede;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new EnvioCorreoBienvenidaDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CorreoSaliente = oIDataReader[oIDataReader.GetOrdinal("CorreoSaliente")].ToString(),
                                    Contrasenia = oIDataReader[oIDataReader.GetOrdinal("Contrasenia")].ToString(),
                                    Titulo = oIDataReader[oIDataReader.GetOrdinal("Titulo")].ToString(),
                                    Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    CorreoSaliente2 = oIDataReader[oIDataReader.GetOrdinal("CorreoSaliente2")].ToString(),
                                    Contrasenia2 = oIDataReader[oIDataReader.GetOrdinal("Contrasenia2")].ToString()
                                  };
                            }
                        }
                    }
                }
            }

            return itemDTO;
		}
        
		public void Registrar(EnvioCorreoBienvenidaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarEnvioCorreoBienvenida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CorreoSaliente", System.Data.SqlDbType.VarChar, 150)).Value = item.CorreoSaliente;
                    cmd.Parameters.Add(new SqlParameter("@Contrasenia", System.Data.SqlDbType.VarChar,200)).Value = item.Contrasenia;
                    cmd.Parameters.Add(new SqlParameter("@Titulo", System.Data.SqlDbType.VarChar,100)).Value = item.Titulo;
                    cmd.Parameters.Add(new SqlParameter("@Asunto", System.Data.SqlDbType.VarChar,300)).Value = item.Asunto;
                    cmd.Parameters.Add(new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar)).Value = item.Mensaje;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CorreoSaliente2", System.Data.SqlDbType.VarChar,150)).Value = item.CorreoSaliente2;
                    cmd.Parameters.Add(new SqlParameter("@Contrasenia2", System.Data.SqlDbType.VarChar,200)).Value = item.Contrasenia2;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
		}

		public void Actualizar(EnvioCorreoBienvenidaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarEnvioCorreoBienvenida", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CorreoSaliente", System.Data.SqlDbType.VarChar, 150)).Value = item.CorreoSaliente;
                    cmd.Parameters.Add(new SqlParameter("@Contrasenia", System.Data.SqlDbType.VarChar, 200)).Value = item.Contrasenia;
                    cmd.Parameters.Add(new SqlParameter("@Titulo", System.Data.SqlDbType.VarChar, 100)).Value = item.Titulo;
                    cmd.Parameters.Add(new SqlParameter("@Asunto", System.Data.SqlDbType.VarChar, 300)).Value = item.Asunto;
                    cmd.Parameters.Add(new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar)).Value = item.Mensaje;
                    cmd.Parameters.Add(new SqlParameter("@CorreoSaliente2", System.Data.SqlDbType.VarChar, 150)).Value = item.CorreoSaliente2;
                    cmd.Parameters.Add(new SqlParameter("@Contrasenia2", System.Data.SqlDbType.VarChar, 200)).Value = item.Contrasenia2;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
		}

		
	}
}
