
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class UsuariosIngresosData
	{
       
        public UsuariosIngresosDTO uspValidarAccesoSistema(UsuariosIngresosDTO oItem)
        {
            UsuariosIngresosDTO itemDTO = null;
          
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarAccesoSistema", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar,100)).Value = oItem.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIngreso", System.Data.SqlDbType.Int)).Value = oItem.CodigoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@Latitud", System.Data.SqlDbType.VarChar,100)).Value = oItem.Latitud;
                    cmd.Parameters.Add(new SqlParameter("@Longitud", System.Data.SqlDbType.VarChar,100)).Value = oItem.Longitud;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new UsuariosIngresosDTO()
                                {
                                    CodigoValidacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString()                                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

        public int Registrar(UsuariosIngresosDTO item)
		{
		    int ? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGRegistrarUsuariosIngresos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.AddWithValue("@CodigoIngreso", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario", System.Data.SqlDbType.Int)).Value = item.CodigoUsuario;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreCompleto;

                    cmd.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.VarChar, 50)).Value = item.Password;
                    cmd.Parameters.Add(new SqlParameter("@Latitud", System.Data.SqlDbType.VarChar, 200)).Value = item.Latitud;
                    cmd.Parameters.Add(new SqlParameter("@Longitud", System.Data.SqlDbType.VarChar, 200)).Value = item.Longitud;
                    cmd.Parameters.Add(new SqlParameter("@IPPublica", System.Data.SqlDbType.VarChar, 200)).Value = item.IPPublica;
                    cmd.Parameters.Add(new SqlParameter("@IPPrivada", System.Data.SqlDbType.VarChar, 200)).Value = item.IPPrivada;

                    cmd.Parameters.Add(new SqlParameter("@NombrePC", System.Data.SqlDbType.VarChar, 200)).Value = item.NombrePC;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoIngreso"].Value);
                }
            }

		    return Convert.ToInt32(campoRetorno);
		}

        public int uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo(UsuariosIngresosDTO item)
        {
            int? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;                   
                    cmd.Parameters.Add(new SqlParameter("@flag", System.Data.SqlDbType.Int)).Value = item.flag;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    
                    cmd.ExecuteNonQuery();                  
                }
            }

            return Convert.ToInt32(campoRetorno);
        }

        public int uspObtenerValidacionOperaciones(UsuariosIngresosDTO item)
		{
		    int ? campoRetorno = 999999999;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspObtenerValidacionOperaciones", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.Parameters.Add(new SqlParameter("@Operacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Operacion;

                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.DescripcionTabla;
                    cmd.Parameters.AddWithValue("@CodigoOperacion", 0).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoOperacion"].Value);
                }
            }

		    return Convert.ToInt32(campoRetorno);
		}
        
	}
}
