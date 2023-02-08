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
    public class CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData
    {

        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oFiltro)
        {
            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> lista = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = oFiltro.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@FechaCreacion", System.Data.SqlDbType.DateTime)).Value = oFiltro.FechaCreacion;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
                                {                 
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]),                                    
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")])                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oFiltro)
        {
            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> lista = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = oFiltro.DiaNumero;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
                                {                                    
                                    DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]),
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> CentroEntrenamiento_uspListarPresencial_ConfiguracionSalaFitness(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oFiltro)
        {
            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> lista = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_ConfiguracionSalaFitness", conn))
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
                                lista.Add(new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoConfiguracionSalaFitness = oIDataReader[oIDataReader.GetOrdinal("CodigoConfiguracionSalaFitness")].ToString(),
                                    DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]),
                                    DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString(),
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]),
                                    Tiempo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tiempo")]),
                                    Minutos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Minutos")]),
                                    CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]),
                                    NroHorarios = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroHorarios")]),
                                    AforoxHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("AforoxHorario")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public string CentroEntrenamiento_uspRegistrarPresencial_ConfiguracionSalaFitness(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarPresencial_ConfiguracionSalaFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@CodigoConfiguracionSalaFitness", System.Data.SqlDbType.VarChar, 50)).Direction = ParameterDirection.Output;
  
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@DiaNombre", System.Data.SqlDbType.VarChar, 10)).Value = item.DiaNombre;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = item.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@Minutos", System.Data.SqlDbType.Int)).Value = item.Minutos;
                    cmd.Parameters.Add(new SqlParameter("@Tiempo", System.Data.SqlDbType.Int)).Value = item.Tiempo;
                    cmd.Parameters.Add(new SqlParameter("@CapacidadPermitida", System.Data.SqlDbType.Int)).Value = item.CapacidadPermitida;

                    //cmd.Parameters.Add(new SqlParameter("@NroHorarios", System.Data.SqlDbType.Int)).Value = item.NroHorarios;
                    //cmd.Parameters.Add(new SqlParameter("@AforoxHorario", System.Data.SqlDbType.Int)).Value = item.AforoxHorario;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = cmd.Parameters["@CodigoConfiguracionSalaFitness"].Value.ToString();
                }

            }
            return resultado;
        }

        public string CentroEntrenamiento_uspActualizarPresencial_ConfiguracionSalaFitness(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarPresencial_ConfiguracionSalaFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoConfiguracionSalaFitness", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoConfiguracionSalaFitness;

                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@DiaNombre", System.Data.SqlDbType.VarChar, 10)).Value = item.DiaNombre;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = item.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@Minutos", System.Data.SqlDbType.Int)).Value = item.Minutos;
                    cmd.Parameters.Add(new SqlParameter("@CapacidadPermitida", System.Data.SqlDbType.Int)).Value = item.CapacidadPermitida;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                   
                }

            }
            return resultado;
        }

       
      
        public int CentroEntrenamiento_uspEliminarPresencial_SalaMaquinas_HorarioTemporal(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO item)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspEliminarPresencial_SalaMaquinas_HorarioTemporal", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@validador", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@validador"].Value);
                }

            }
            return resultado;
        }

        //BUSCAR CONFIGURACION
        public CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;                   
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = request.CodigoHorarioClasesConfiguracion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoUnidadNegocio")))
                                {
                                    itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSede")))
                                {
                                    itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Estado")))
                                {
                                    itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                }
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        //CAMBIAR AFORO CONFIGURACION
        public string CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CapacidadPermitida", System.Data.SqlDbType.Int)).Value = item.CapacidadPermitida;

                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }

        //DESACTIVAR CONFIGURACION
        public string CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;                    

                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }

        //ACTIVAR CONFIGURACION
        public string CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;

                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }



    }
}
