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
    public class CentroEntrenamiento_Presencial_HorarioClasesAsistenciasData
    {

        public string CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO item)
        {
            int? flagExisteClase = 0;
            int? CantidadNroIngresos = 0;
            string Mensaje = string.Empty;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionTiempoReal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracionTiempoReal;                   
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@NroCupo", System.Data.SqlDbType.Int)).Value = item.NroCupo;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }

                    cmd.Parameters.Add("@flagExisteClase", SqlDbType.Int);
                    cmd.Parameters["@flagExisteClase"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@NroIngresosActual", SqlDbType.Int);
                    cmd.Parameters["@NroIngresosActual"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar,200);
                    cmd.Parameters["@Mensaje"].Direction = ParameterDirection.Output;

                    //cmd.Parameters.AddWithValue("@NroIngresosActual", CantidadNroIngresos).Direction = System.Data.ParameterDirection.Output;
                    //cmd.Parameters.AddWithValue("@Mensaje", Mensaje).Direction = System.Data.ParameterDirection.Output;


                    cmd.ExecuteNonQuery();

                    flagExisteClase = Convert.ToInt32(cmd.Parameters["@flagExisteClase"].Value);
                    CantidadNroIngresos = Convert.ToInt32(cmd.Parameters["@NroIngresosActual"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }

            }
            return CantidadNroIngresos + "|" + Mensaje + "|" + flagExisteClase;
        }


        public string CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar,50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionTiempoReal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracionTiempoReal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionAsistencias", System.Data.SqlDbType.VarChar, 50)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@NroCupo", System.Data.SqlDbType.Int)).Value = item.NroCupo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioReservacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioReservacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaReservacion", System.Data.SqlDbType.DateTime)).Value = item.FechaReservacion;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = item.DiaNumero;
                    
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = cmd.Parameters["@CodigoHorarioClasesConfiguracionAsistencias"].Value.ToString();
                }

            }
            return resultado;
        }

        public void CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionTiempoReal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracionTiempoReal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionAsistencias", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracionAsistencias;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }
        
        public string CentroEntrenamiento_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO item)
        {
            int? CantidadNroIngresos = 0;
            string Mensaje = "";
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    ////cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionTiempoReal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracionTiempoReal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionAsistencias", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracionAsistencias;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresia;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }

                    cmd.Parameters.Add("@NroIngresosActual", SqlDbType.Int);
                    cmd.Parameters["@NroIngresosActual"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 200);
                    cmd.Parameters["@Mensaje"].Direction = ParameterDirection.Output;

                    //cmd.Parameters.AddWithValue("@NroIngresosActual", CantidadNroIngresos).Direction = System.Data.ParameterDirection.Output;
                    //cmd.Parameters.AddWithValue("@Mensaje", Mensaje).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    CantidadNroIngresos = Convert.ToInt32(cmd.Parameters["@NroIngresosActual"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }

            }

            return CantidadNroIngresos + "|" + Mensaje;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionTiempoReal", System.Data.SqlDbType.VarChar,100)).Value = request.CodigoHorarioClasesConfiguracionTiempoReal;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracionTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionTiempoReal")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSocio")))
                                {
                                    itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoMembresia")))
                                {
                                    itemDTO.CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoPaquete")))
                                {
                                    itemDTO.CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraReserva")))
                                {
                                    itemDTO.FechaHoraReserva = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraReserva")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Nombres")))
                                {
                                    itemDTO.Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Apellidos")))
                                {
                                    itemDTO.Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Celular")))
                                {
                                    itemDTO.Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ImagenUrl")))
                                {
                                    itemDTO.ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DNI")))
                                {
                                    itemDTO.DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("PlanMembresia")))
                                {
                                    itemDTO.PlanMembresia = oIDataReader[oIDataReader.GetOrdinal("PlanMembresia")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaInicio")))
                                {
                                    itemDTO.FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaFin")))
                                {
                                    itemDTO.FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]);
                                }
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = request.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = request.CodigoMembresia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO();


                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoUnidadNegocio")))
                                {
                                    itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSede")))
                                {
                                    itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracionTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString();
                                }
                                
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoDisciplinaSala")))
                                {
                                    itemDTO.CodigoDisciplinaSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDisciplinaSala")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.FechaHoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.FechaHoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSocio")))
                                {
                                    itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoMembresia")))
                                {
                                    itemDTO.CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaSemana")))
                                {
                                    itemDTO.DiaSemana = ObtenerDiaSemana(oIDataReader[oIDataReader.GetOrdinal("DiaSemana")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraReserva")))
                                {
                                    itemDTO.FechaHoraReserva = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraReserva")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("UsuarioReservacion")))
                                {
                                    itemDTO.UsuarioReservacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioReservacion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("flagAsistio")))
                                {
                                    itemDTO.flagAsistio = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("flagAsistio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraAsistio")))
                                {
                                    itemDTO.FechaHoraAsistio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraAsistio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Estado")))
                                {
                                    itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("flagVistaBotonMarcarAsistencia")))
                                {
                                    itemDTO.flagVistaBotonMarcarAsistencia = oIDataReader[oIDataReader.GetOrdinal("flagVistaBotonMarcarAsistencia")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("flagVistaImagenAsistio")))
                                {
                                    itemDTO.flagVistaImagenAsistio = oIDataReader[oIDataReader.GetOrdinal("flagVistaImagenAsistio")].ToString();
                                } 
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesflagAsistio")))
                                {
                                    itemDTO.DesflagAsistio = oIDataReader[oIDataReader.GetOrdinal("DesflagAsistio")].ToString();
                                }
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        private string ObtenerDiaSemana(string dia)
        {

            string semana = "";

            if (dia == "2")
            {
                semana = "Lunes";
            }
            else if (dia == "3")
            {
                semana = "Martes";
            }
            else if (dia == "4")
            {
                semana = "Miercoles";
            }
            else if (dia == "5")
            {
                semana = "Jueves";
            }
            else if (dia == "6")
            {
                semana = "Viernes";
            }
            else if (dia == "7")
            {
                semana = "Sabado";
            }
            else if (dia == "8")
            {
                semana = "Domingo";
            }
            else if (dia == "1")
            {
                semana = "Domingo";
            }

            return semana;
        }
    }
}
