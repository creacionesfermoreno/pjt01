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
    public class CentroEntrenamiento_Presencial_DisciplinaSalaData
    {
             
        public List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO> CentroEntrenamiento_uspListarDisciplinaSala_Presencial(CentroEntrenamiento_Presencial_DisciplinaSalaDTO oFiltro)
        {
            List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO> lista = new List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarDisciplinaSala_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSala;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CentroEntrenamiento_Presencial_DisciplinaSalaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSala")]),
                                    CodigoDisciplinaSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDisciplinaSala")]),
                                    Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString(),
                                    Capacidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]),
                                    Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    Orden = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Orden")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public int CentroEntrenamiento_uspRegistrarDisciplinaSala_Presencial(CentroEntrenamiento_Presencial_DisciplinaSalaDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarDisciplinaSala_Presencial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDisciplinaSala", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(item.Disciplina))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Disciplina", System.Data.SqlDbType.VarChar, 200)).Value = item.Disciplina;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Capacidad", System.Data.SqlDbType.Int)).Value = item.Capacidad;

                    if (!string.IsNullOrEmpty(item.Color))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar, 100)).Value = item.Color;
                    }

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@CodigoDisciplinaSala"].Value);
                }

            }
            return resultado;
        }



    }
}
