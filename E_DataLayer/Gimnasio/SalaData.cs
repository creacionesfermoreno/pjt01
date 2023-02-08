using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace E_DataLayer.Gimnasio
{
    public class SalaData
    {
        public List<SalaDTO> Listar(SalaDTO oSalaDTO)
        {
            List<SalaDTO> lista = new List<SalaDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSala", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oSalaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oSalaDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new SalaDTO() { };
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSala")))
                                {
                                    itemDTO.Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSala")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Descripcion")))
                                {
                                    itemDTO.Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroSala")))
                                {
                                    itemDTO.NroSala = (oIDataReader[oIDataReader.GetOrdinal("NroSala")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CantidadClases")))
                                {
                                    itemDTO.CantidadClases = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClases")]);
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            return lista;
        }

        public SalaDTO BuscarPorCodigoSala(SalaDTO oSala)
        {
            SalaDTO itemDTO = null;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSalaHorarioPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;//oSalaHorario.CodigoUnidadNegocio, oSalaHorario.CodigoSede, oSalaHorario.CodigoSalaHorario
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oSala.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oSala.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int, 10)).Value = oSala.Codigo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new SalaDTO()
                                {

                                };
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSala")))
                                {
                                    itemDTO.Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSala")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Descripcion")))
                                {
                                    itemDTO.Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroSala")))
                                {
                                    itemDTO.NroSala = (oIDataReader[oIDataReader.GetOrdinal("NroSala")].ToString());
                                }
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public int? Registrar(SalaDTO item)
        {
            int? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarSala", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.AddWithValue("@CodigoSala", "0").Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@NroSala", System.Data.SqlDbType.VarChar, 50)).Value = item.NroSala;
                    cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar, 50)).Value = item.Color;
                    cmd.Parameters.Add(new SqlParameter("@Estilo", System.Data.SqlDbType.VarChar, 50)).Value = item.Estilo;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoSala"].Value);
                }
            }

            return campoRetorno;
        }

        public void Actualizar(SalaDTO item)
        {

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSala", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@NroSala", System.Data.SqlDbType.Int, 10)).Value = item.NroSala??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar, 50)).Value = item.Color??String.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Estilo", System.Data.SqlDbType.VarChar, 50)).Value = item.Estilo??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion ?? item.UsuarioEdicion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(SalaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarSala", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int, 10)).Value = item.Codigo;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
