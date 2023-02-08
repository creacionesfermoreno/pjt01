using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_DataLayer.CentroEntrenamiento
{
    public class CentroEntrenamiento_GaleriaFitnessData
    {

        public List<CentroEntrenamiento_GaleriaFitnessDTO> CentroEntrenamiento_uspListarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessDTO request)
        {
            List<CentroEntrenamiento_GaleriaFitnessDTO> lista = new List<CentroEntrenamiento_GaleriaFitnessDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarGaleriaFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Privacidad", System.Data.SqlDbType.Int, 100)).Value = request.Privacidad;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int, 100)).Value = request.Tipo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_GaleriaFitnessDTO();                                
                                itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                itemDTO.Codigo = oIDataReader[oIDataReader.GetOrdinal("Codigo")].ToString();
                                itemDTO.Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]);
                                itemDTO.Privacidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Privacidad")]);
                                itemDTO.UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString();
                                itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                itemDTO.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);
                                
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public string CentroEntrenamiento_uspRegistrarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarGaleriaFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.VarChar, 200)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Privacidad", System.Data.SqlDbType.Int)).Value = item.Privacidad;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlImagen;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    
                    cmd.ExecuteNonQuery();
                    resultado = cmd.Parameters["@Codigo"].Value.ToString();
                }

            }
            return resultado;
        }

        public void CentroEntrenamiento_uspActualizarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessDTO item)
        {            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarGaleriaFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.VarChar, 200)).Value = item.Codigo;
                    //cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.Tipo;
                    //cmd.Parameters.Add(new SqlParameter("@Privacidad", System.Data.SqlDbType.Int)).Value = item.Privacidad;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlImagen;
                    //cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    //cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                   
                }

            }
        }

        public void CentroEntrenamiento_uspEliminarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspEliminarGaleriaFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.VarChar, 200)).Value = item.Codigo;
                   
                    cmd.ExecuteNonQuery();

                }

            }
        }

        
    }
}
