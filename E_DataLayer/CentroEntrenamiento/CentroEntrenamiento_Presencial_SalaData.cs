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
    public class CentroEntrenamiento_Presencial_SalaData
    {
        public List<CentroEntrenamiento_Presencial_SalaDTO> CentroEntrenamiento_uspListarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO oFiltro)
        {
            List<CentroEntrenamiento_Presencial_SalaDTO> lista = new List<CentroEntrenamiento_Presencial_SalaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarSala_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
        
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CentroEntrenamiento_Presencial_SalaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSala")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    NroSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroSala")]),
                                    Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<CentroEntrenamiento_Presencial_SalaDTO> CentroEntrenamiento_uspListarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO oFiltro)
        {
            List<CentroEntrenamiento_Presencial_SalaDTO> lista = new List<CentroEntrenamiento_Presencial_SalaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarSalaMaquinas_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CentroEntrenamiento_Presencial_SalaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    TipoSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoSala")]),
                                    CodigoSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSala")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    NroSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroSala")]),
                                    Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public int CentroEntrenamiento_uspRegistrarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarSala_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(item.Descripcion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    }
                   
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoSala"].Value);
                }

            }
            return resultado;
        }
        
        public int CentroEntrenamiento_uspRegistrarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarSalaMaquinas_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(item.Descripcion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    }

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoSala"].Value);
                }

            }
            return resultado;
        }


        public int CentroEntrenamiento_uspEditarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspEditarSala_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.CodigoSala;
                    //cmd.Parameters.Add(new SqlParameter("@ValidacionClases", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(item.Descripcion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    }
                    
                    cmd.ExecuteNonQuery();
                    //resultado = Convert.ToInt32(cmd.Parameters["@ValidacionClases"].Value);
                }

            }
            return resultado;
        }

        public int CentroEntrenamiento_uspEliminarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspEliminarSala_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@ValidacionClases", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@ValidacionClases"].Value);
                }

            }
            return resultado;

        }

        public int CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@ValidacionClases", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@ValidacionClases"].Value);
                }

            }
            return resultado;

        }


    }
}
