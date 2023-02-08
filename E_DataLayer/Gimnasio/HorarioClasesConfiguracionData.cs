using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace E_DataLayer.Gimnasio
{
    public class HorarioClasesConfiguracionData
    {
        public List<HorarioClasesConfiguracionDTO> Listar(HorarioClasesConfiguracionDTO oHorarioClasesConfiguracionDTO)
        {
            List<HorarioClasesConfiguracionDTO> lista = new List<HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoSede;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new HorarioClasesConfiguracionDTO();
                              
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSalaHorario")))
                                {
                                    itemDTO.CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNombre")))
                                {
                                    itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NombreProfecionalFitness")))
                                {
                                    itemDTO.NombreProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("NombreProfecionalFitness")].ToString());
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            
            return lista;            
        }

        public List<HorarioClasesConfiguracionDTO> ListarCalendario(HorarioClasesConfiguracionDTO oHorarioClasesConfiguracionDTO)
        {
            List<HorarioClasesConfiguracionDTO> lista = new List<HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesConfiguracionCalendario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClasesConfiguracionDTO.CodigoSala;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalNombre")))
                                {
                                    itemDTO.NombreProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalNombre")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalDNI")))
                                {
                                    itemDTO.DNIProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("ProfesionalDNI")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalPhoto")))
                                {
                                    itemDTO.PhotoProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("ProfesionalPhoto")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public List<HorarioClasesConfiguracionDTO> uspListarHorarioClasesConfiguracionCalendario_ExportarExcel(HorarioClasesConfiguracionDTO oHorarioClasesConfiguracionDTO)
        {
            List<HorarioClasesConfiguracionDTO> lista = new List<HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesConfiguracionCalendario_ExportarExcel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoSede;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalNombre")))
                                {
                                    itemDTO.NombreProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalNombre")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Celular")))
                                {
                                    itemDTO.Celular = (oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Correo")))
                                {
                                    itemDTO.Correo = (oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Direccion")))
                                {
                                    itemDTO.Direccion = (oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaNacimiento")))
                                {
                                    itemDTO.FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalDNI")))
                                {
                                    itemDTO.DNIProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("ProfesionalDNI")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesDia")))
                                {
                                    itemDTO.DesDia = oIDataReader[oIDataReader.GetOrdinal("DesDia")].ToString();
                                }


                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }


        public List<HorarioClasesConfiguracionDTO> ListarPorCodigoProfesional(HorarioClasesConfiguracionDTO oHorarioClasesConfiguracionDTO)
        {
            List<HorarioClasesConfiguracionDTO> lista = new List<HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesConfiguracionPorProfesional", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClasesConfiguracionDTO.CodigoProfesional;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new HorarioClasesConfiguracionDTO();
                                
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSalaHorario")))
                                {
                                    itemDTO.CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNombre")))
                                {
                                    itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = (oIDataReader[oIDataReader.GetOrdinal("Color")].ToString());
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;            
        }

        public HorarioClasesConfiguracionDTO BuscarPorCodigoHorarioClasesConfiguracion(HorarioClasesConfiguracionDTO oHorarioClasesConfiguracion)
        {
            HorarioClasesConfiguracionDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarHorarioClasesConfiguracionPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracion.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracion.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClasesConfiguracion.CodigoHorarioClasesConfiguracion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new HorarioClasesConfiguracionDTO();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSalaHorario")))
                                {
                                    itemDTO.CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNombre")))
                                {
                                    itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]);
                                }


                                itemDTO.ProfesionalFitness = new ProfesionalFitnessDTO() { };

                                itemDTO.ProfesionalFitness.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                itemDTO.ProfesionalFitness.NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreProfecionalFitness")].ToString();
                                itemDTO.ProfesionalFitness.Nombres = oIDataReader[oIDataReader.GetOrdinal("NombresProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Apellidos = oIDataReader[oIDataReader.GetOrdinal("ApellidosProfesor")].ToString();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("TipoDocumentoProfesor")))
                                {
                                    itemDTO.ProfesionalFitness.TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumentoProfesor")]);
                                }

                                itemDTO.ProfesionalFitness.DNI = oIDataReader[oIDataReader.GetOrdinal("DNIProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Celular = oIDataReader[oIDataReader.GetOrdinal("CelularProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Correo = oIDataReader[oIDataReader.GetOrdinal("CorreoProfesor")].ToString();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaNacimientoProfesor")))
                                {
                                    itemDTO.ProfesionalFitness.FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimientoProfesor")]);
                                }

                                itemDTO.ProfesionalFitness.Genero = oIDataReader[oIDataReader.GetOrdinal("GeneroProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Direccion = oIDataReader[oIDataReader.GetOrdinal("DireccionProfesor")].ToString();
                                itemDTO.ProfesionalFitness.ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("PhotoProfecionalFitness")].ToString();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CostoPorHora")))
                                {
                                    itemDTO.ProfesionalFitness.CostoPorHora = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoPorHora")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DescuentoPorMinuto")))
                                {
                                    itemDTO.ProfesionalFitness.DstoPorMinuto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DescuentoPorMinuto")]);
                                }

                                itemDTO.CostoPorClase = itemDTO.ProfesionalFitness.CostoPorHora;
                                itemDTO.DescuentoPorMinuto = itemDTO.ProfesionalFitness.DstoPorMinuto;
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public List<HorarioClasesConfiguracionDTO> ListarConfiguracionPorDia(HorarioClasesConfiguracionDTO oHorarioClasesConfiguracionDTO)
        {
            List<HorarioClasesConfiguracionDTO> lista = new List<HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesConfiguracionPorDiaActual", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesConfiguracionDTO.CodigoSede;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new HorarioClasesConfiguracionDTO();
                              
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSalaHorario")))
                                {
                                    itemDTO.CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalNombre")))
                                {
                                    itemDTO.NombreProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalNombre")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalDNI")))
                                {
                                    itemDTO.DNIProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalDNI")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalPhoto")))
                                {
                                    itemDTO.PhotoProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalPhoto")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = (oIDataReader[oIDataReader.GetOrdinal("Color")].ToString());
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            return lista;
        }

        public void Registrar(HorarioClasesConfiguracionDTO item)
        {

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarHorarioClasesConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalaHorario", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSalaHorario;

                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@CapacidadPermitida", System.Data.SqlDbType.Int, 10)).Value = item.CapacidadPermitida;
                    cmd.Parameters.Add(new SqlParameter("@CostoPorClase", System.Data.SqlDbType.Decimal, 15)).Value = item.CostoPorClase;
                    cmd.Parameters.Add(new SqlParameter("@DescuentoPorMinuto", System.Data.SqlDbType.Decimal, 15)).Value = item.DescuentoPorMinuto;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int, 10)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@DiaNombre", System.Data.SqlDbType.VarChar, 50)).Value = item.DiaNombre;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = item.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(HorarioClasesConfiguracionDTO item)
        {

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarHorarioClasesConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalaHorario", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSalaHorario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@CapacidadPermitida", System.Data.SqlDbType.Int, 10)).Value = item.CapacidadPermitida;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int, 10)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@DiaNombre", System.Data.SqlDbType.VarChar, 50)).Value = item.DiaNombre;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        public void Eliminar(HorarioClasesConfiguracionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarHorarioClasesConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int uspValidadorHorarioClasesConfiguracion(HorarioClasesConfiguracionDTO item)
        {
            int? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarHorarioClasesConfiguraion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalaHorario", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSalaHorario;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int, 10)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.AddWithValue("@Existe", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }
            
            return Convert.ToInt32(campoRetorno);
        }
    }
}
