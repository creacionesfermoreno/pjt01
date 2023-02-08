using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace E_DataLayer.Gimnasio
{
    public class SalaHorarioData
    {
        public List<SalaHorarioDTO> Listar(SalaHorarioDTO oSalaHorarioDTO)
        {
            List<SalaHorarioDTO> lista = new List<SalaHorarioDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSalaHorario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oSalaHorarioDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oSalaHorarioDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodNroSala", System.Data.SqlDbType.Int, 10)).Value = oSalaHorarioDTO.CodNroSala;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new SalaHorarioDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSalaHorario")))
                                {
                                    itemDTO.CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Capacidad")))
                                {
                                    itemDTO.Capacidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]);
                                }
                                
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color  = (oIDataReader[oIDataReader.GetOrdinal("Color")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("WidthPanel")))
                                {
                                    itemDTO.StyleWidth = (oIDataReader[oIDataReader.GetOrdinal("WidthPanel")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HeightPanel")))
                                {
                                    itemDTO.StyleHeight = (oIDataReader[oIDataReader.GetOrdinal("HeightPanel")].ToString());
                                }
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            return lista;
        }

        public SalaHorarioDTO BuscarPorCodigoSalaHorario(SalaHorarioDTO oSalaHorario)
        {
            SalaHorarioDTO itemDTO = null;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSalaHorarioPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oSalaHorario.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oSalaHorario.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalaHorario", System.Data.SqlDbType.Int, 10)).Value = oSalaHorario.CodigoSalaHorario;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new SalaHorarioDTO()
                                {

                                };
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSalaHorario")))
                                {
                                    itemDTO.CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Capacidad")))
                                {
                                    itemDTO.Capacidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]);
                                }
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public int? Registrar(SalaHorarioDTO item)
        {
            int? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarSalaHorario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.AddWithValue("@CodigoSalaHorario", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Disciplina", System.Data.SqlDbType.VarChar, 100)).Value = item.Disciplina;
                    cmd.Parameters.Add(new SqlParameter("@Capacidad", System.Data.SqlDbType.Int, 10)).Value = item.Capacidad;
                    cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar, 50)).Value = item.Color;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@NroSala", System.Data.SqlDbType.Int)).Value = item.CodNroSala;
                    cmd.ExecuteReader();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoSalaHorario"].Value);
                }
            }

            return campoRetorno;
        }

        public void Actualizar(SalaHorarioDTO item)
        {

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSalaHorario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;//oSalaHorario.CodigoUnidadNegocio, oSalaHorario.CodigoSede, oSalaHorario.CodigoSalaHorario
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalaHorario", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSalaHorario;

                    cmd.Parameters.Add(new SqlParameter("@Disciplina", System.Data.SqlDbType.VarChar, 100)).Value = item.Disciplina ?? String.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Capacidad", System.Data.SqlDbType.Int, 10)).Value = item.Capacidad ?? 0;
                    cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar, 50)).Value = item.Color ?? string.Empty;

                    cmd.Parameters.Add(new SqlParameter("@StyleWidth", System.Data.SqlDbType.VarChar, 50)).Value = item.StyleWidth ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@StyleHeight", System.Data.SqlDbType.VarChar, 50)).Value = item.StyleHeight ?? string.Empty;

                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion ?? item.UsuarioEdicion;
                    cmd.ExecuteReader();
                }
            }
        }

        public void Eliminar(SalaHorarioDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarSalaHorario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;//oSalaHorario.CodigoUnidadNegocio, oSalaHorario.CodigoSede, oSalaHorario.CodigoSalaHorario
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalaHorario", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSalaHorario;
                    cmd.ExecuteReader();
                }
            }
        }

    }
}
