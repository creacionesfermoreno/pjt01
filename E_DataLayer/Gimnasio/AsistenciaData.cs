using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using E_DataModel.Gimnasio;
using E_DataModel.Common;

namespace E_DataLayer.Gimnasio
{
	public class AsistenciaData
	{
        //si se usa
        public List<AsistenciaDTO> uspListar_Socios_Inasistencias_Paginacion(AsistenciaDTO oAsistenciaDTO, Paging paging)
        {
            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_Socios_Inasistencias_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@DiasAtras", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.DiasAtras;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar)).Value = oAsistenciaDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAsistenciaDTO.Nombres;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AsistenciaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    MontoTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    strFechaIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaIngreso")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    DesCalificacion =  oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        public AsistenciaDTO uspListar_Socios_Inasistencias_NumeroRegistro(AsistenciaDTO oAsistenciaDTO)
        {
            AsistenciaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_Socios_Inasistencias_NumeroRegistro", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@DiasAtras", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.DiasAtras;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar)).Value = oAsistenciaDTO.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAsistenciaDTO.Nombres;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AsistenciaDTO()
                                {
                                    CantHombres = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadHombres")]),
                                    CantMujeres = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMujeres")]),
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])                                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
        //si se usa
        public AsistenciaDTO uspListarAsistenciaTodosFiltro_NumeroRegistros(AsistenciaDTO oAsistenciaDTO)
        {
            AsistenciaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAsistenciaTodosFiltro_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@TipoPersona", System.Data.SqlDbType.VarChar,1)).Value = oAsistenciaDTO.TipoPersona;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAsistenciaDTO.FechaIngreso;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAsistenciaDTO.FechaFinalizo;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = oAsistenciaDTO.HoraInicioAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = oAsistenciaDTO.HoraFinAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar,100)).Value = oAsistenciaDTO.Nombres;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AsistenciaDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadTotal")]),
                                    CantHombres = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadHombres")]),
                                    CantMujeres = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMujeres")]),                                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
        //si se usa
        public List<AsistenciaDTO> ListarAsistenciaTodosFiltro_Paginacion(AsistenciaDTO oAsistenciaDTO, Paging paging)
        {
            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAsistenciaTodosFiltro_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@TipoPersona", System.Data.SqlDbType.VarChar)).Value = oAsistenciaDTO.TipoPersona;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAsistenciaDTO.FechaIngreso;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAsistenciaDTO.FechaFinalizo;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = oAsistenciaDTO.HoraInicioAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = oAsistenciaDTO.HoraFinAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAsistenciaDTO.Nombres;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AsistenciaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                  
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    Costo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Costo")]),
                                    
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]).ToString("dd/MM/yyyy"),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]).ToString("dd/MM/yyyy"),
                                    Vendedor = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString(),
                                    desTiempoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesPaquete")].ToString(),
                                    strFechaIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaIngreso")]).ToString("dd/MM/yyyy HH:mm tt"),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    DesTipoIngreso = oIDataReader[oIDataReader.GetOrdinal("DesTipoIngreso")].ToString(),
                                    DesTipoPaquete = oIDataReader[oIDataReader.GetOrdinal("DesTipoPaquete")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            
            return lista;
        }
        //si se usa
        public List<AsistenciaDTO> uspListarDetalleAsistenciaSocio_Paginacion(AsistenciaDTO oAsistenciaDTO, Paging paging)
        {
            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleAsistenciaSocio_Paginacion", conn))
                {
                
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoMembresia;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AsistenciaDTO()
                                {
                                    //fila = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Fila")]),
                                    CodigoAsistencia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAsistencia")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresiaReal")]),
                                    CodigoPersona = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPersona")]),
                                    FechaCreacion = (DateTime)oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")],
                                    DiaSemana = ObtenerDiaSemana(oIDataReader[oIDataReader.GetOrdinal("DiaSemana")].ToString()),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    flagVistaImagenAsistioReserva = oIDataReader[oIDataReader.GetOrdinal("flagVistaImagenAsistioReserva")].ToString(),
                                    CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        //si se usa
        private string ObtenerDiaSemana(string dia) {

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
        //si se usa
        public AsistenciaDTO uspListarDetalleAsistenciaSocio_NumeroRegistros(AsistenciaDTO oAsistenciaDTO)
        {
            AsistenciaDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleAsistenciaSocio_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoMembresia;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AsistenciaDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
        //si se usa
        public AsistenciaDTO BuscarAsistenciaEfectiva(AsistenciaDTO oAsistenciaDTO)
		{
            AsistenciaDTO itemDTO = new AsistenciaDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarAsistenciaEfectiva", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoUnidadNegocio;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AsistenciaDTO()
                                {
                                    totalDias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("diaTotal")]),
                                    CantDiasTomaFrezzeng = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantTomaFreezing")]),
                                    DiasEfectivo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiasEfectivoIngreso")]),
                                    DiasAsistidos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiasAsistidos")]),
                                    desPorAsistido = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DesPorAsistido")]),
                                    DiasFaltantes = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiasFaltantes")]),
                                    desPorfaltante = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("desPorFaltante")]),
                                    desNomEstado = oIDataReader[oIDataReader.GetOrdinal("desNomEstado")].ToString(),
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
            
		}
        //si se usa
        public void Registrar(AsistenciaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAsistencia", System.Data.SqlDbType.Int)).Value = item.CodigoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersona", System.Data.SqlDbType.Int)).Value = item.CodigoPersona;
                    cmd.Parameters.Add(new SqlParameter("@TipoPersona", System.Data.SqlDbType.VarChar,1)).Value = item.TipoPersona;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresiaReal", System.Data.SqlDbType.Int)).Value = item.CodigoMembresiaReal;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
	
		}
        //si se usa
        public void EliminarAsistencia(AsistenciaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAsistencia", System.Data.SqlDbType.Int)).Value = item.CodigoAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresiaReal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoInicioSesion;
                    if (item.CodigoHorarioClasesConfiguracionAsistencias != string.Empty)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionAsistencias", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoHorarioClasesConfiguracionAsistencias;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionAsistencias", System.Data.SqlDbType.VarChar, 100)).Value = "CodigoHorarioClasesConfiguracionAsistencias";
                    }
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}


	}
}
