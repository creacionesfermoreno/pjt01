
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class PersonalAsistenciaNotaData
    {
        public List<PersonalAsistenciaNotaDTO> Listar(PersonalAsistenciaNotaDTO oPersonalAsistenciaNotaDTO, Paging paging, ref uint recordCount)
        {
            List<PersonalAsistenciaNotaDTO> lista = new List<PersonalAsistenciaNotaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPersonalAsistenciaNota", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaNotaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaNotaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAsistenciaNotaDTO.CodigoPersonalAsistencia;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PersonalAsistenciaNotaDTO item = new PersonalAsistenciaNotaDTO();
                                item.CodigoPersonalAsistenciaNota = reader[reader.GetOrdinal("CodigoPersonalAsistenciaNota")].ToString();
                                item.Nota = reader[reader.GetOrdinal("Nota")].ToString();
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaRegistro")))
                                {
                                    item.FechaRegistro = Convert.ToDateTime(reader[reader.GetOrdinal("FechaRegistro")]);
                                }
                                lista.Add(item);
                            }
                        }
                    }
                }
            }
            return lista;
        }

        public PersonalAsistenciaNotaDTO BuscarPorCodigoPersonalAsistenciaNota(PersonalAsistenciaNotaDTO oPersonalAsistenciaNota)
        {
            PersonalAsistenciaNotaDTO itemDTO = null;
            
            return itemDTO;
        }

        public void Registrar(PersonalAsistenciaNotaDTO item)
        {
            string  campoRetorno = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPersonalAsistenciaNota", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAsistencia;
                    cmd.Parameters.AddWithValue("@CodigoPersonalAsistenciaNota", "").Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Notas", System.Data.SqlDbType.VarChar,500)).Value = item.Nota;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.ExecuteNonQuery();
                    campoRetorno = cmd.Parameters["@CodigoPersonalAsistenciaNota"].Value.ToString();
                }
            }
        }

        public void Actualizar(PersonalAsistenciaNotaDTO item)
        {
        }

        public void Eliminar(PersonalAsistenciaNotaDTO item)
        {
        }
    }
}
