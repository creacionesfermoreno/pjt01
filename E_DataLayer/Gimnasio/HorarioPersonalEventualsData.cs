
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class HorarioPersonalEventualsData
	{
       
        public List<HorarioPersonalEventualsDTO> Listar(HorarioPersonalEventualsDTO oHorarioPersonalEventualsDTO)
        {
            List<HorarioPersonalEventualsDTO> lista = new List<HorarioPersonalEventualsDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioPersonalEventuals", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHorarioPersonalEventualsDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oHorarioPersonalEventualsDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HorarioPersonalEventualsDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoHorario")]),
                                    CodigoPersonal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPersonal")]),
                                    CodigoEspecialidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEspecialidad")]),
                                    Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString(),
                                    PagoXhora = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PagoXhora")]),
                                    Dia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Dia")]),
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]),
                                    NroCupos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroCupos")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString()
                                });

                            }
                        }

                    }
                }
            }
          
            return lista;
        }
        
        public void Registrar(HorarioPersonalEventualsDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarHorarioPersonalEventuals", conn))
                {                                                                                                
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorario", System.Data.SqlDbType.Int)).Value = item.CodigoHorario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.Int)).Value = item.CodigoPersonal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEspecialidad", System.Data.SqlDbType.Int)).Value = item.CodigoEspecialidad;
                    cmd.Parameters.Add(new SqlParameter("@Disciplina", System.Data.SqlDbType.VarChar,100)).Value = item.Disciplina;

                    cmd.Parameters.Add(new SqlParameter("@PagoXhora", System.Data.SqlDbType.Decimal)).Value = item.PagoXhora;
                    cmd.Parameters.Add(new SqlParameter("@Dia", System.Data.SqlDbType.Int)).Value = item.Dia;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = item.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@NroCupos", System.Data.SqlDbType.Int)).Value = item.NroCupos;

                    cmd.ExecuteNonQuery();
                }
            }
		}
        
	}
}
