using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class MetasData
	{

        public List<MetasDTO> uspListarMetricas_ConversionLeads_Totales(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMetricas_ConversionLeads_Totales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),

                                    ConversionLeads_CantidadWalking = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Walking")]),
                                    ConversionLeads_CantidadRenovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Renovaciones")]),
                                    ConversionLeads_CantidadReinscripciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Inactivos")]),
                                    ConversionLeads_CantidadProspeccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Prospeccion")]),
                                    ConversionLeads_CantidadDigital = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Digital")]),
                                    ConversionLeads_CantidadLlamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("LlamadaE")]),

                                    ConversionLeads_ActividadCitas_walking = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCitas_walking")]),
                                    ConversionLeads_ActividadReunion_walking = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_walking")]),
                                    ConversionLeads_ActividadCita_Renovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCita_Renovaciones")]),
                                    ConversionLeads_ActividadReunion_Renovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_Renovaciones")]),
                                    ConversionLeads_ActividadCita_Reinscripciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCita_Reinscripciones")]),
                                    ConversionLeads_ActividadReunion_Reinscripciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_Reinscripciones")]),
                                    ConversionLeads_ActividadCitas_prospeccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCitas_prospeccion")]),
                                    ConversionLeads_ActividadReunion_prospeccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_prospeccion")]),
                                    ConversionLeads_ActividadCita_digital = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCita_digital")]),
                                    ConversionLeads_ActividadReunion_digital = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_digital")]),
                                    ConversionLeads_ActividadCita_llamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCita_llamadaE")]),
                                    ConversionLeads_ActividadReunion_llamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_llamadaE")]),

                                    ConversionLeads_VentaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaTotal")]),
                                    ConversionLeads_CantidadClientesVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta")]),
                                    ConversionLeads_CantidadClientesVenta_walking = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_walking")]),
                                    ConversionLeads_CantidadClientesVenta_renovacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_renovacion")]),
                                    ConversionLeads_CantidadClientesVenta_reinscripcion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_reinscripcion")]),
                                    ConversionLeads_CantidadClientesVenta_prospeccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_prospeccion")]),
                                    ConversionLeads_CantidadClientesVenta_digital = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_digital")]),
                                    ConversionLeads_CantidadClientesVenta_llamadaentrante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_llamadaentrante")])

                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public List<MetasDTO> uspListarMetasDetalle_ConversionLeads_Totales(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMetasDetalle_ConversionLeads_Totales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oitem.CodigoMeta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEntidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMeta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMeta")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),

                                    ConversionLeads_CantidadWalking = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Walking")]),
                                    ConversionLeads_CantidadRenovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Renovaciones")]),
                                    ConversionLeads_CantidadReinscripciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Inactivos")]),
                                    ConversionLeads_CantidadProspeccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Prospeccion")]),
                                    ConversionLeads_CantidadDigital = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Digital")]),
                                    ConversionLeads_CantidadLlamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("LlamadaE")]),

                                    ConversionLeads_ActividadCitas_walking = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCitas_walking")]),
                                    ConversionLeads_ActividadReunion_walking = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_walking")]),
                                    ConversionLeads_ActividadCita_Renovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCita_Renovaciones")]),
                                    ConversionLeads_ActividadReunion_Renovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_Renovaciones")]),
                                    ConversionLeads_ActividadCita_Reinscripciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCita_Reinscripciones")]),
                                    ConversionLeads_ActividadReunion_Reinscripciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_Reinscripciones")]),
                                    ConversionLeads_ActividadCitas_prospeccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCitas_prospeccion")]),
                                    ConversionLeads_ActividadReunion_prospeccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_prospeccion")]),
                                    ConversionLeads_ActividadCita_digital = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCita_digital")]),
                                    ConversionLeads_ActividadReunion_digital = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_digital")]),
                                    ConversionLeads_ActividadCita_llamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadCita_llamadaE")]),
                                    ConversionLeads_ActividadReunion_llamadaE = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ActividadReunion_llamadaE")]),

                                    ConversionLeads_VentaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaTotal")]),
                                    ConversionLeads_CantidadClientesVenta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta")]),
                                    ConversionLeads_CantidadClientesVenta_walking = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_walking")]),
                                    ConversionLeads_CantidadClientesVenta_renovacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_renovacion")]),
                                    ConversionLeads_CantidadClientesVenta_reinscripcion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_reinscripcion")]),
                                    ConversionLeads_CantidadClientesVenta_prospeccion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_prospeccion")]),
                                    ConversionLeads_CantidadClientesVenta_digital = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_digital")]),
                                    ConversionLeads_CantidadClientesVenta_llamadaentrante = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta_llamadaentrante")])

                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<MetasDTO> uspListarEfectivadadCitasVendedores(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarEfectivadadCitasVendedores", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),

                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    VentaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaTotal")]),
                                    CantidadVentasNuevas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("VentaNuevasCantidad")]),
                                    CantidadMenbresiasRenovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMenbresiasRenovacion")]),
                                    CantidadMenbresiasReinscripcion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMenbresiasReinscripcion")]),
                                    CantidadMenbresiasAmplacionRenovacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMenbresiasAmpliacionRenovacion")]),
                                    CantidadMenbresiasAmpliacionReinscripcion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMenbresiasAmpliacionReinscripcion")]),
                                    CantidadVentaMenbresiasAnuales = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadVentaMenbresiasAnuales")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<MetasDTO> ListarVendedores(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarVendedoresYMetas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    imagenUrl = oIDataReader[oIDataReader.GetOrdinal("imagenUrl")].ToString(),
                                    CodigoUsuario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUsuario")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    VentaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Venta")])                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }
        
        public List<MetasDTO> uspListarHistorialMetas(MetasDTO oitem, Paging paging)
		{
			List<MetasDTO> lista = new List<MetasDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHistorialMetas", conn))
                {                                                                             
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oitem.CodigoMeta;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEntidadNegocio")]),
                                  
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMeta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMeta")]),
                                    Meta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Meta")]),
                                    Bono = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Bono")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    CantidadVendedores = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadVendedores")]),
                                    VentaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Venta")])                                   
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}

        public List<MetasDTO> uspListarMetasDetalle_EstadisticaVenta(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMetasDetalle_EstadisticaVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oitem.CodigoMeta;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEntidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMeta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMeta")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    Meta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Meta")]),
                                    B_TicketPromedio_MontoMinimo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("B_TicketPromedio_MontoMinimo")]),
                                    CantidadClientesVendidos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVendidos")]),
                                    VentaMenbresiasAnuales = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaMenbresiasAnuales")]),
                                    CantidadVentaMenbresiasAnuales = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadVentaMenbresiasAnuales")]),
                                    CantidadMenbresiasRenovaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMenbresiasRenovacion")]),
                                    CantidadMenbresiasRenovacionPorVendedorRepartido = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMenbresiasRenovacionPorVendedorRepartido")]),
                                    VentaMenbresiasRenovaciones = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaMenbresiasRenovacion")]),
                                    VentaRenovacionesPorcentaje = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaRenovacionesPorcentaje")]),
                                    CantidadMenbresiasReinscripcion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMenbresiasReinscripcion")]),
                                    IngresoCantidadCitasReinscripcion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("IngresoCantidadCitasReinscripcion")]),
                                    PorcentajeEfectividadReinscripcion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorcentajeEfectividadReinscripcion")]),
                                    VentaMenbresiasReinscripcion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaMenbresiasReinscripcion")]),
                                    VentaNuevasPorcentaje = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaNuevasPorcentaje")]),
                                    CantidadVentasNuevas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("VentaNuevasCantidad")]),
                                    CantidadClientesNuevos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesNuevos")]),
                                    CantidadMenbresiasAmplacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMenbresiasAmpliacion")]),
                                    VentaMenbresiasAmplacion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaMenbresiasAmpliacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        
        public List<MetasDTO> uspListarMetasDetalle_CuadroComisiones(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMetasDetalle_CuadroComisiones", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oitem.CodigoMeta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    Pago_ComisionSoles = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ComisionSoles")]),
                                    Pago_BonoGrupalPorcentaje = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorcentajeBonoGrupal")]),
                                    Pago_BonoGrupalSoles = oIDataReader[oIDataReader.GetOrdinal("BonoGrupalSoles")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoGrupalSoles")]),
                                    Pago_BonoTicketPromedioSoles = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoTicketPromedio")]),
                                    Pago_BonoAnualesSoles = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoMembresiaAnual")]),
                                    Pago_BonoEfectividadNuevos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoEfectividadNuevos")]),
                                    Pago_BonoSemanal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoSemanal")]),
                                    Pago_BonoRenovacion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoRenovaciones")]),
                                    Pago_BonoReinscripcion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoReinscripcion")]),
                                    Pago_Bono10PorcientoAdicional = oIDataReader[oIDataReader.GetOrdinal("BonoCada10porcientoAdicional")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoCada10porcientoAdicional")]),
                                    Pago_ComisionBonoAdicional = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ComisionBonoAdicional")]),
                                    Pago_BonoAmpliacion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("BonoCadaNumeroAmpliaciones")]),
                                    Pago_TotalCobrar = oIDataReader[oIDataReader.GetOrdinal("TotalCobrar")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalCobrar")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<MetasDTO> uspListarMetasDetalle_VentasAvance(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMetasDetalle_VentasAvance", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oitem.CodigoMeta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEntidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMeta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMeta")]),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    CantidadMetaPlan = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadPlan")]),
                                    CantidadPlanesVendido = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadClientesVenta")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    Meta = oIDataReader[oIDataReader.GetOrdinal("Meta")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Meta")]),
                                    MetaMinima = oIDataReader[oIDataReader.GetOrdinal("MetaMinima")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MetaMinima")]),
                                    MetaSemanal = oIDataReader[oIDataReader.GetOrdinal("MetaSemanal")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MetaSemanal")]),
                                    VentaTotal = oIDataReader[oIDataReader.GetOrdinal("VentaTotal")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaTotal")]),
                                    DiferenciaFaltante = oIDataReader[oIDataReader.GetOrdinal("DiferenciaFaltante")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DiferenciaFaltante")]),
                                    VentaDiaria = oIDataReader[oIDataReader.GetOrdinal("VentaHoy")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("VentaHoy")]),
                                    NecesidadDiaria = oIDataReader[oIDataReader.GetOrdinal("NecesidadDiaria")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("NecesidadDiaria")]),
                                    PorAcumulado = oIDataReader[oIDataReader.GetOrdinal("PorAcumulado")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorAcumulado")]),
                                    PorFaltante = oIDataReader[oIDataReader.GetOrdinal("PorFaltante")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PorFaltante")]),
                                    Proyeccion = oIDataReader[oIDataReader.GetOrdinal("Proyeccion")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Proyeccion")]),
                                    MetaMinimaPorc = oIDataReader[oIDataReader.GetOrdinal("MetaMinimaPorc")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MetaMinimaPorc")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }


        public List<MetasDTO> uspListarProductividad_AreaComercial(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProductividad_AreaComercial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oitem.CodigoMeta;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEntidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMeta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMeta")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),

                                    Productividad_Nuevos = oIDataReader[oIDataReader.GetOrdinal("Nuevos")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Nuevos")]),
                                    Productividad_Invitados = oIDataReader[oIDataReader.GetOrdinal("Invitados")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Invitados")]),
                                    Productividad_Referidos = oIDataReader[oIDataReader.GetOrdinal("Referidos")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Referidos")]),
                                    Productividad_Llamada = oIDataReader[oIDataReader.GetOrdinal("Llamada")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Llamada")]),
                                    Productividad_Web = oIDataReader[oIDataReader.GetOrdinal("Web")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Web")]),

                                    Productividad_TotalProspectos = oIDataReader[oIDataReader.GetOrdinal("TotalProspectos")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TotalProspectos")]),
                                    Productividad_TotalInscritos = oIDataReader[oIDataReader.GetOrdinal("TotalInscritos")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TotalInscritos")]),

                                    Productividad_Efectividad = oIDataReader[oIDataReader.GetOrdinal("Efectividad")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Efectividad")]),
                                    Productividad_MontoTotal = oIDataReader[oIDataReader.GetOrdinal("MontoTotal")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoTotal")]),
                                    Productividad_TPromedio = oIDataReader[oIDataReader.GetOrdinal("TPromedio")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TPromedio")]),

                                    Productividad_CantidadCitas = oIDataReader[oIDataReader.GetOrdinal("CantidadCitas")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadCitas")]),
                                    Productividad_EfectividadCitas = oIDataReader[oIDataReader.GetOrdinal("EfectividadCitas")] == null ? 0 : Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EfectividadCitas")]),
                                    Productividad_TPromedioCitas = oIDataReader[oIDataReader.GetOrdinal("TPromedioCitas")] == null ? 0 : Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TPromedioCitas")])


                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }


        public MetasDTO uspBuscarMetaVendedorPorCodigo(MetasDTO oMetas)
        {
            MetasDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarMetaVendedorPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = oMetas.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oMetas.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oMetas.CodigoMeta;
                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoEntidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    CodigoMeta = Convert.ToInt32(reader[reader.GetOrdinal("CodigoMeta")]),
                                    Meta = Convert.ToDecimal(reader[reader.GetOrdinal("Meta")]),
                                    FechaInicio = Convert.ToDateTime(reader[reader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(reader[reader.GetOrdinal("FechaFin")]),
                                    CantidadVendedores = Convert.ToInt32(reader[reader.GetOrdinal("CantidadVendedores")]),
                                    CodigoSupervisorVenta = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSupervisorVenta")]),
                                    Bono = Convert.ToDecimal(reader[reader.GetOrdinal("Bono")]),
                                    B_TicketPromedio_MontoMinimo = Convert.ToDecimal(reader[reader.GetOrdinal("B_TicketPromedio_MontoMinimo")]),
                                    B_TicketPromedio_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_TicketPromedio_Bono")]),
                                    B_Nuevos_PorcentajeMinimo = Convert.ToInt32(reader[reader.GetOrdinal("B_Nuevos_PorcentajeMinimo")]),
                                    B_Nuevos_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_Nuevos_Bono")]),
                                    B_Reinscripciones_MontoMinimo = Convert.ToDecimal(reader[reader.GetOrdinal("B_Reinscripciones_MontoMinimo")]),
                                    B_Reinscripciones_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_Reinscripciones_Bono")]),
                                    B_Renovaciones_PorcentajeMinimo = Convert.ToInt32(reader[reader.GetOrdinal("B_Renovaciones_PorcentajeMinimo")]),
                                    B_Renovaciones_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_Renovaciones_Bono")]),
                                    B_ContratosAnuales_CantidadMinima = Convert.ToInt32(reader[reader.GetOrdinal("B_ContratosAnuales_CantidadMinima")]),
                                    
                                    B_ContratosAnuales_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_ContratosAnuales_Bono")]),
                                    B_VentaSemanal_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_VentaSemanal_Bono")]),
                                    B_VentaAdicionalMeta10porciento_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_VentaAdicionalMeta10porciento_Bono")]),
                                    B_AmpliacionContrato_Cantidad = Convert.ToInt32(reader[reader.GetOrdinal("B_AmpliacionContrato_Cantidad")]),
                                    B_AmpliacionContrato_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_AmpliacionContrato_Bono")]),
                                    
                                    MetaSemanal = Convert.ToDecimal(reader[reader.GetOrdinal("MetaSemanal")]),
                                    Comision1a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision1a")]),
                                    Comision1b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision1b")]),
                                    Comision1porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision1porc")]),

                                    Comision2a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision2a")]),
                                    Comision2b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision2b")]),
                                    Comision2porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision2porc")]),

                                    Comision3a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision3a")]),
                                    Comision3b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision3b")]),
                                    Comision3porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision3porc")]),

                                    Comision4a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision4a")]),
                                    Comision4b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision4b")]),
                                    Comision4porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision4porc")]),

                                    Comision5a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision5a")]),
                                    Comision5b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision5b")]),
                                    Comision5porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision5porc")]),

                                    Comision6a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision6a")]),
                                    Comision6b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision6b")]),
                                    Comision6porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision6porc")]),
                                    UsuarioCreacion = reader[reader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    PorcenBonoAdicional1 = Convert.ToDecimal(reader[reader.GetOrdinal("PorcenBonoAdicional1")]),
                                    Monto_BonoAdicional1 = Convert.ToDecimal(reader[reader.GetOrdinal("Monto_BonoAdicional1")]),
                                    PorcenBonoAdicional2 = Convert.ToDecimal(reader[reader.GetOrdinal("PorcenBonoAdicional2")]),
                                    Monto_BonoAdicional2 = Convert.ToDecimal(reader[reader.GetOrdinal("Monto_BonoAdicional2")]),
                                    PorcenBonoAdicional3 = Convert.ToDecimal(reader[reader.GetOrdinal("PorcenBonoAdicional3")]),
                                    Monto_BonoAdicional3 = Convert.ToDecimal(reader[reader.GetOrdinal("Monto_BonoAdicional3")]),
                                    PorcenBonoAdicional4 = Convert.ToDecimal(reader[reader.GetOrdinal("PorcenBonoAdicional4")]),
                                    Monto_BonoAdicional4 = Convert.ToDecimal(reader[reader.GetOrdinal("Monto_BonoAdicional4")]),
                                    PorcenBonoAdicional5 = Convert.ToDecimal(reader[reader.GetOrdinal("PorcenBonoAdicional5")]),
                                    Monto_BonoAdicional5 = Convert.ToDecimal(reader[reader.GetOrdinal("Monto_BonoAdicional5")]),
                                    PorcenBonoAdicional6 = Convert.ToDecimal(reader[reader.GetOrdinal("PorcenBonoAdicional6")]),
                                    Monto_BonoAdicional6 = Convert.ToDecimal(reader[reader.GetOrdinal("Monto_BonoAdicional6")]),

                                    FechaSemanal1a = Convert.ToDateTime(reader[reader.GetOrdinal("FechaSemanal1a")]),
                                    FechaSemanal1b = Convert.ToDateTime(reader[reader.GetOrdinal("FechaSemanal1b")]),
                                    CuotaSemanalBono1 = Convert.ToDecimal(reader[reader.GetOrdinal("CuotaSemanalBono1")]),
                                    FechaSemanal2a = Convert.ToDateTime(reader[reader.GetOrdinal("FechaSemanal2a")]),
                                    FechaSemanal2b = Convert.ToDateTime(reader[reader.GetOrdinal("FechaSemanal2b")]),
                                    CuotaSemanalBono2 = Convert.ToDecimal(reader[reader.GetOrdinal("CuotaSemanalBono2")]),
                                    FechaSemanal3a = Convert.ToDateTime(reader[reader.GetOrdinal("FechaSemanal3a")]),
                                    FechaSemanal3b = Convert.ToDateTime(reader[reader.GetOrdinal("FechaSemanal3b")]),
                                    CuotaSemanalBono3 = Convert.ToDecimal(reader[reader.GetOrdinal("CuotaSemanalBono3")]),
                                    FechaSemanal4a = Convert.ToDateTime(reader[reader.GetOrdinal("FechaSemanal4a")]),
                                    FechaSemanal4b = Convert.ToDateTime(reader[reader.GetOrdinal("FechaSemanal4b")]),
                                    CuotaSemanalBono4 = Convert.ToDecimal(reader[reader.GetOrdinal("CuotaSemanalBono4")]),
                                    MetaMinimaPorc = Convert.ToDecimal(reader[reader.GetOrdinal("MetaMinimaPorc")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

        public MetasDTO uspBuscarMetaVendedorPorMesActual(MetasDTO oMetas)
        {
            MetasDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarMetaVendedorPorMesActual", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = oMetas.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oMetas.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oMetas.CodigoMeta;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new MetasDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoEntidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    CodigoMeta = Convert.ToInt32(reader[reader.GetOrdinal("CodigoMeta")]),
                                    Meta = Convert.ToDecimal(reader[reader.GetOrdinal("Meta")]),
                                    VentaTotal = Convert.ToDecimal(reader[reader.GetOrdinal("VentaTotal")]),
                                    FechaInicio = Convert.ToDateTime(reader[reader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(reader[reader.GetOrdinal("FechaFin")]),
                                    CantidadVendedores = Convert.ToInt32(reader[reader.GetOrdinal("CantidadVendedores")]),
                                    CodigoSupervisorVenta = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSupervisorVenta")]),
                                    Bono = Convert.ToDecimal(reader[reader.GetOrdinal("Bono")]),
                                    B_TicketPromedio_MontoMinimo = Convert.ToDecimal(reader[reader.GetOrdinal("B_TicketPromedio_MontoMinimo")]),
                                    B_TicketPromedio_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_TicketPromedio_Bono")]),
                                    B_Nuevos_PorcentajeMinimo = Convert.ToInt32(reader[reader.GetOrdinal("B_Nuevos_PorcentajeMinimo")]),
                                    B_Nuevos_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_Nuevos_Bono")]),
                                    B_Reinscripciones_MontoMinimo = Convert.ToDecimal(reader[reader.GetOrdinal("B_Reinscripciones_MontoMinimo")]),
                                    B_Reinscripciones_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_Reinscripciones_Bono")]),
                                    B_Renovaciones_PorcentajeMinimo = Convert.ToInt32(reader[reader.GetOrdinal("B_Renovaciones_PorcentajeMinimo")]),
                                    B_Renovaciones_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_Renovaciones_Bono")]),
                                    B_ContratosAnuales_CantidadMinima = Convert.ToInt32(reader[reader.GetOrdinal("B_ContratosAnuales_CantidadMinima")]),
                                    B_ContratosAnuales_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_ContratosAnuales_Bono")]),
                                    B_VentaSemanal_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_VentaSemanal_Bono")]),
                                    B_VentaAdicionalMeta10porciento_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_VentaAdicionalMeta10porciento_Bono")]),
                                    B_AmpliacionContrato_Cantidad = Convert.ToInt32(reader[reader.GetOrdinal("B_AmpliacionContrato_Cantidad")]),
                                    B_AmpliacionContrato_Bono = Convert.ToDecimal(reader[reader.GetOrdinal("B_AmpliacionContrato_Bono")]),

                                    MetaSemanal = Convert.ToDecimal(reader[reader.GetOrdinal("MetaSemanal")]),
                                    Comision1a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision1a")]),
                                    Comision1b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision1b")]),
                                    Comision1porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision1porc")]),

                                    Comision2a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision2a")]),
                                    Comision2b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision2b")]),
                                    Comision2porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision2porc")]),

                                    Comision3a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision3a")]),
                                    Comision3b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision3b")]),
                                    Comision3porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision3porc")]),

                                    Comision4a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision4a")]),
                                    Comision4b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision4b")]),
                                    Comision4porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision4porc")]),

                                    Comision5a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision5a")]),
                                    Comision5b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision5b")]),
                                    Comision5porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision5porc")]),

                                    Comision6a = Convert.ToDecimal(reader[reader.GetOrdinal("Comision6a")]),
                                    Comision6b = Convert.ToDecimal(reader[reader.GetOrdinal("Comision6b")]),
                                    Comision6porc = Convert.ToDecimal(reader[reader.GetOrdinal("Comision6porc")]),

                                    CantidadRepartidosMes = Convert.ToInt32(reader[reader.GetOrdinal("RepartidosMes")]),
                                    CantidadRepartidosPorVendedorMes = Convert.ToInt32(reader[reader.GetOrdinal("RepartidosMes")]) / Convert.ToInt32(reader[reader.GetOrdinal("CantidadVendedores")]),
                                    UsuarioCreacion = reader[reader.GetOrdinal("UsuarioCreacion")].ToString()
                                    
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }




        public string obtenerNombreMes(int nro)
        {
            string n = string.Empty;
            switch (nro)
            {
                case 1:
                    {
                        n = "Enero";
                    }
                    break;
                case 2:
                    {
                        n = "Febrero";
                    }
                    break;
                case 3:
                    {
                        n = "Marzo";
                    }
                    break;
                case 4:
                    {
                        n = "Abril";
                    }
                    break;
                case 5:
                    {
                        n = "Mayo";
                    }
                    break;
                case 6:
                    {
                        n = "Junio";
                    }
                    break;
                case 7:
                    {
                        n = "Julio";
                    }
                    break;
                case 8:
                    {
                        n = "Agosto";
                    }
                    break;
                case 9:
                    {
                        n = "Setiembre";
                    }
                    break;
                case 10:
                    {
                        n = "Octubre";
                    }
                    break;
                case 11:
                    {
                        n = "Noviembre";
                    }
                    break;
                case 12:
                    {
                        n = "Diciembre";
                    }
                    break;
                default:
                    break;
            }

            return n;

        }
        
		public int Registrar(MetasDTO item)
		{
		    int ? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarMetas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.AddWithValue("@CodigoMeta", campoRetorno).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Meta", System.Data.SqlDbType.Decimal)).Value = item.Meta;
                    cmd.Parameters.Add(new SqlParameter("@Bono", System.Data.SqlDbType.Decimal)).Value = item.Bono;

                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CantidadVendedores", System.Data.SqlDbType.Int)).Value = item.CantidadVendedores;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSupervisorVenta", System.Data.SqlDbType.Int)).Value = item.CodigoSupervisorVenta;
                    cmd.Parameters.Add(new SqlParameter("@B_TicketPromedio_MontoMinimo", System.Data.SqlDbType.Decimal)).Value = item.B_TicketPromedio_MontoMinimo;

                    cmd.Parameters.Add(new SqlParameter("@B_TicketPromedio_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_TicketPromedio_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_Nuevos_PorcentajeMinimo", System.Data.SqlDbType.Int)).Value = item.B_Nuevos_PorcentajeMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Nuevos_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Nuevos_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_Reinscripciones_MontoMinimo", System.Data.SqlDbType.Decimal)).Value = item.B_Reinscripciones_MontoMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Reinscripciones_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Reinscripciones_Bono;

                    cmd.Parameters.Add(new SqlParameter("@B_Renovaciones_PorcentajeMinimo", System.Data.SqlDbType.Int)).Value = item.B_Renovaciones_PorcentajeMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Renovaciones_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Renovaciones_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_ContratosAnuales_CantidadMinima", System.Data.SqlDbType.Int)).Value = item.B_ContratosAnuales_CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@B_ContratosAnuales_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_ContratosAnuales_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_VentaSemanal_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_VentaSemanal_Bono;

                    cmd.Parameters.Add(new SqlParameter("@B_VentaAdicionalMeta10porciento_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_VentaAdicionalMeta10porciento_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_AmpliacionContrato_Cantidad", System.Data.SqlDbType.Int)).Value = item.B_AmpliacionContrato_Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@B_AmpliacionContrato_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_AmpliacionContrato_Bono;
                    cmd.Parameters.Add(new SqlParameter("@MetaSemanal", System.Data.SqlDbType.Decimal)).Value = item.MetaSemanal;
                    cmd.Parameters.Add(new SqlParameter("@Comision1a", System.Data.SqlDbType.Decimal)).Value = item.Comision1a;
                    
                    cmd.Parameters.Add(new SqlParameter("@Comision1b", System.Data.SqlDbType.Decimal)).Value = item.Comision1b;
                    cmd.Parameters.Add(new SqlParameter("@Comision1porc", System.Data.SqlDbType.Decimal)).Value = item.Comision1porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision2a", System.Data.SqlDbType.Decimal)).Value = item.Comision2a;
                    cmd.Parameters.Add(new SqlParameter("@Comision2b", System.Data.SqlDbType.Decimal)).Value = item.Comision2b;
                    cmd.Parameters.Add(new SqlParameter("@Comision2porc", System.Data.SqlDbType.Decimal)).Value = item.Comision2porc;

                    cmd.Parameters.Add(new SqlParameter("@Comision3a", System.Data.SqlDbType.Decimal)).Value = item.Comision3a;
                    cmd.Parameters.Add(new SqlParameter("@Comision3b", System.Data.SqlDbType.Decimal)).Value = item.Comision3b;
                    cmd.Parameters.Add(new SqlParameter("@Comision3porc", System.Data.SqlDbType.Decimal)).Value = item.Comision3porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision4a", System.Data.SqlDbType.Decimal)).Value = item.Comision4a;
                    cmd.Parameters.Add(new SqlParameter("@Comision4b", System.Data.SqlDbType.Decimal)).Value = item.Comision4b;

                    cmd.Parameters.Add(new SqlParameter("@Comision4porc", System.Data.SqlDbType.Decimal)).Value = item.Comision4porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision5a", System.Data.SqlDbType.Decimal)).Value = item.Comision5a;
                    cmd.Parameters.Add(new SqlParameter("@Comision5b", System.Data.SqlDbType.Decimal)).Value = item.Comision5b;
                    cmd.Parameters.Add(new SqlParameter("@Comision5porc", System.Data.SqlDbType.Decimal)).Value = item.Comision5porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision6a", System.Data.SqlDbType.Decimal)).Value = item.Comision6a;

                    cmd.Parameters.Add(new SqlParameter("@Comision6b", System.Data.SqlDbType.Decimal)).Value = item.Comision6b;
                    cmd.Parameters.Add(new SqlParameter("@Comision6porc", System.Data.SqlDbType.Decimal)).Value = item.Comision6porc;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional1", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional1;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional1", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional1;

                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional2", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional2;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional2", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional2;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional3", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional3;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional3", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional3;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional4", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional4;

                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional4", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional4;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional5", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional5;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional5", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional5;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional6", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional6;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional6", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional6;

                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal1a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal1a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal1b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal1b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono1", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono1;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal2a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal2a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal2b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal2b;

                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono2", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono2;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal3a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal3a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal3b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal3b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono3", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono3;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal4a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal4a;

                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal4b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal4b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono4", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono4;
                    cmd.Parameters.Add(new SqlParameter("@MetaMinimaPorc", System.Data.SqlDbType.Decimal)).Value = item.MetaMinimaPorc;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoMeta"].Value);
                }
            }

		  return Convert.ToInt32(campoRetorno);
		}

        public int uspRegistrarMetaInicioMes(MetasDTO item)
		{
		   int ? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarMetaInicioMes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.AddWithValue("@CodigoMeta", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Meta", System.Data.SqlDbType.Decimal)).Value = item.Meta;
                    cmd.Parameters.Add(new SqlParameter("@Bono", System.Data.SqlDbType.Decimal)).Value = item.Bono;

                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CantidadVendedores", System.Data.SqlDbType.Int)).Value = item.CantidadVendedores;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSupervisorVenta", System.Data.SqlDbType.Int)).Value = item.CodigoSupervisorVenta;
                    cmd.Parameters.Add(new SqlParameter("@B_TicketPromedio_MontoMinimo", System.Data.SqlDbType.Decimal)).Value = item.B_TicketPromedio_MontoMinimo;

                    cmd.Parameters.Add(new SqlParameter("@B_TicketPromedio_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_TicketPromedio_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_Nuevos_PorcentajeMinimo", System.Data.SqlDbType.Int)).Value = item.B_Nuevos_PorcentajeMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Nuevos_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Nuevos_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_Reinscripciones_MontoMinimo", System.Data.SqlDbType.Decimal)).Value = item.B_Reinscripciones_MontoMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Reinscripciones_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Reinscripciones_Bono;

                    cmd.Parameters.Add(new SqlParameter("@B_Renovaciones_PorcentajeMinimo", System.Data.SqlDbType.Int)).Value = item.B_Renovaciones_PorcentajeMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Renovaciones_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Renovaciones_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_ContratosAnuales_CantidadMinima", System.Data.SqlDbType.Int)).Value = item.B_ContratosAnuales_CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@B_ContratosAnuales_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_ContratosAnuales_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_VentaSemanal_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_VentaSemanal_Bono;

                    cmd.Parameters.Add(new SqlParameter("@B_VentaAdicionalMeta10porciento_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_VentaAdicionalMeta10porciento_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_AmpliacionContrato_Cantidad", System.Data.SqlDbType.Int)).Value = item.B_AmpliacionContrato_Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@B_AmpliacionContrato_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_AmpliacionContrato_Bono;
                    cmd.Parameters.Add(new SqlParameter("@MetaSemanal", System.Data.SqlDbType.Decimal)).Value = item.MetaSemanal;
                    cmd.Parameters.Add(new SqlParameter("@Comision1a", System.Data.SqlDbType.Decimal)).Value = item.Comision1a;
                    
                    cmd.Parameters.Add(new SqlParameter("@Comision1b", System.Data.SqlDbType.Decimal)).Value = item.Comision1b;
                    cmd.Parameters.Add(new SqlParameter("@Comision1porc", System.Data.SqlDbType.Decimal)).Value = item.Comision1porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision2a", System.Data.SqlDbType.Decimal)).Value = item.Comision2a;
                    cmd.Parameters.Add(new SqlParameter("@Comision2b", System.Data.SqlDbType.Decimal)).Value = item.Comision2b;
                    cmd.Parameters.Add(new SqlParameter("@Comision2porc", System.Data.SqlDbType.Decimal)).Value = item.Comision2porc;

                    cmd.Parameters.Add(new SqlParameter("@Comision3a", System.Data.SqlDbType.Decimal)).Value = item.Comision3a;
                    cmd.Parameters.Add(new SqlParameter("@Comision3b", System.Data.SqlDbType.Decimal)).Value = item.Comision3b;
                    cmd.Parameters.Add(new SqlParameter("@Comision3porc", System.Data.SqlDbType.Decimal)).Value = item.Comision3porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision4a", System.Data.SqlDbType.Decimal)).Value = item.Comision4a;
                    cmd.Parameters.Add(new SqlParameter("@Comision4b", System.Data.SqlDbType.Decimal)).Value = item.Comision4b;

                    cmd.Parameters.Add(new SqlParameter("@Comision4porc", System.Data.SqlDbType.Decimal)).Value = item.Comision4porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision5a", System.Data.SqlDbType.Decimal)).Value = item.Comision5a;
                    cmd.Parameters.Add(new SqlParameter("@Comision5b", System.Data.SqlDbType.Decimal)).Value = item.Comision5b;
                    cmd.Parameters.Add(new SqlParameter("@Comision5porc", System.Data.SqlDbType.Decimal)).Value = item.Comision5porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision6a", System.Data.SqlDbType.Decimal)).Value = item.Comision6a;

                    cmd.Parameters.Add(new SqlParameter("@Comision6b", System.Data.SqlDbType.Decimal)).Value = item.Comision6b;
                    cmd.Parameters.Add(new SqlParameter("@Comision6porc", System.Data.SqlDbType.Decimal)).Value = item.Comision6porc;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional1", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional1;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional1", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional1;

                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional2", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional2;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional2", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional2;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional3", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional3;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional3", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional3;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional4", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional4;

                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional4", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional4;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional5", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional5;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional5", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional5;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional6", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional6;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional6", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional6;

                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal1a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal1a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal1b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal1b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono1", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono1;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal2a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal2a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal2b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal2b;

                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono2", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono2;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal3a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal3a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal3b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal3b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono3", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono3;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal4a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal4a;

                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal4b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal4b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono4", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono4;
                    cmd.Parameters.Add(new SqlParameter("@MetaMinimaPorc", System.Data.SqlDbType.Decimal)).Value = item.MetaMinimaPorc;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoMeta"].Value);
                }
            }
          
		  return Convert.ToInt32(campoRetorno);
		}

		public void Actualizar(MetasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarMetas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = item.CodigoMeta;
                    cmd.Parameters.Add(new SqlParameter("@Meta", System.Data.SqlDbType.Decimal)).Value = item.Meta;
                    cmd.Parameters.Add(new SqlParameter("@Bono", System.Data.SqlDbType.Decimal)).Value = item.Bono;

                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CantidadVendedores", System.Data.SqlDbType.Int)).Value = item.CantidadVendedores;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSupervisorVenta", System.Data.SqlDbType.Int)).Value = item.CodigoSupervisorVenta;
                    cmd.Parameters.Add(new SqlParameter("@B_TicketPromedio_MontoMinimo", System.Data.SqlDbType.Decimal)).Value = item.B_TicketPromedio_MontoMinimo;

                    cmd.Parameters.Add(new SqlParameter("@B_TicketPromedio_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_TicketPromedio_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_Nuevos_PorcentajeMinimo", System.Data.SqlDbType.Int)).Value = item.B_Nuevos_PorcentajeMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Nuevos_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Nuevos_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_Reinscripciones_MontoMinimo", System.Data.SqlDbType.Decimal)).Value = item.B_Reinscripciones_MontoMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Reinscripciones_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Reinscripciones_Bono;

                    cmd.Parameters.Add(new SqlParameter("@B_Renovaciones_PorcentajeMinimo", System.Data.SqlDbType.Int)).Value = item.B_Renovaciones_PorcentajeMinimo;
                    cmd.Parameters.Add(new SqlParameter("@B_Renovaciones_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_Renovaciones_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_ContratosAnuales_CantidadMinima", System.Data.SqlDbType.Int)).Value = item.B_ContratosAnuales_CantidadMinima;
                    cmd.Parameters.Add(new SqlParameter("@B_ContratosAnuales_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_ContratosAnuales_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_VentaSemanal_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_VentaSemanal_Bono;

                    cmd.Parameters.Add(new SqlParameter("@B_VentaAdicionalMeta10porciento_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_VentaAdicionalMeta10porciento_Bono;
                    cmd.Parameters.Add(new SqlParameter("@B_AmpliacionContrato_Cantidad", System.Data.SqlDbType.Int)).Value = item.B_AmpliacionContrato_Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@B_AmpliacionContrato_Bono", System.Data.SqlDbType.Decimal)).Value = item.B_AmpliacionContrato_Bono;
                    cmd.Parameters.Add(new SqlParameter("@MetaSemanal", System.Data.SqlDbType.Decimal)).Value = item.MetaSemanal;
                    cmd.Parameters.Add(new SqlParameter("@Comision1a", System.Data.SqlDbType.Decimal)).Value = item.Comision1a;

                    cmd.Parameters.Add(new SqlParameter("@Comision1b", System.Data.SqlDbType.Decimal)).Value = item.Comision1b;
                    cmd.Parameters.Add(new SqlParameter("@Comision1porc", System.Data.SqlDbType.Decimal)).Value = item.Comision1porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision2a", System.Data.SqlDbType.Decimal)).Value = item.Comision2a;
                    cmd.Parameters.Add(new SqlParameter("@Comision2b", System.Data.SqlDbType.Decimal)).Value = item.Comision2b;
                    cmd.Parameters.Add(new SqlParameter("@Comision2porc", System.Data.SqlDbType.Decimal)).Value = item.Comision2porc;

                    cmd.Parameters.Add(new SqlParameter("@Comision3a", System.Data.SqlDbType.Decimal)).Value = item.Comision3a;
                    cmd.Parameters.Add(new SqlParameter("@Comision3b", System.Data.SqlDbType.Decimal)).Value = item.Comision3b;
                    cmd.Parameters.Add(new SqlParameter("@Comision3porc", System.Data.SqlDbType.Decimal)).Value = item.Comision3porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision4a", System.Data.SqlDbType.Decimal)).Value = item.Comision4a;
                    cmd.Parameters.Add(new SqlParameter("@Comision4b", System.Data.SqlDbType.Decimal)).Value = item.Comision4b;

                    cmd.Parameters.Add(new SqlParameter("@Comision4porc", System.Data.SqlDbType.Decimal)).Value = item.Comision4porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision5a", System.Data.SqlDbType.Decimal)).Value = item.Comision5a;
                    cmd.Parameters.Add(new SqlParameter("@Comision5b", System.Data.SqlDbType.Decimal)).Value = item.Comision5b;
                    cmd.Parameters.Add(new SqlParameter("@Comision5porc", System.Data.SqlDbType.Decimal)).Value = item.Comision5porc;
                    cmd.Parameters.Add(new SqlParameter("@Comision6a", System.Data.SqlDbType.Decimal)).Value = item.Comision6a;

                    cmd.Parameters.Add(new SqlParameter("@Comision6b", System.Data.SqlDbType.Decimal)).Value = item.Comision6b;
                    cmd.Parameters.Add(new SqlParameter("@Comision6porc", System.Data.SqlDbType.Decimal)).Value = item.Comision6porc;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional1", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional1;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional1", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional1;

                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional2", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional2;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional2", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional2;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional3", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional3;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional3", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional3;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional4", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional4;

                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional4", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional4;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional5", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional5;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional5", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional5;
                    cmd.Parameters.Add(new SqlParameter("@PorcenBonoAdicional6", System.Data.SqlDbType.Decimal)).Value = item.PorcenBonoAdicional6;
                    cmd.Parameters.Add(new SqlParameter("@Monto_BonoAdicional6", System.Data.SqlDbType.Decimal)).Value = item.Monto_BonoAdicional6;

                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal1a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal1a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal1b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal1b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono1", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono1;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal2a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal2a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal2b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal2b;

                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono2", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono2;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal3a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal3a;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal3b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal3b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono3", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono3;
                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal4a", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal4a;

                    cmd.Parameters.Add(new SqlParameter("@FechaSemanal4b", System.Data.SqlDbType.DateTime)).Value = item.FechaSemanal4b;
                    cmd.Parameters.Add(new SqlParameter("@CuotaSemanalBono4", System.Data.SqlDbType.Decimal)).Value = item.CuotaSemanalBono4;
                    cmd.Parameters.Add(new SqlParameter("@MetaMinimaPorc", System.Data.SqlDbType.Decimal)).Value = item.MetaMinimaPorc;

                    cmd.ExecuteNonQuery();
                }
            }            
		}

		public void Eliminar(MetasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarMetaVendedor", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = item.CodigoMeta;
                    
                    cmd.ExecuteNonQuery();
                }
            }            
		}

        public int uspValidarPrimerDiaMesConfiguracionMetas(int CodigoUnidadNegocio, int CodigoSede)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarPrimerDiaMesConfiguracionMetas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = CodigoSede;
                    cmd.Parameters.AddWithValue("@Existe", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }           
            return Convert.ToInt32(campoRetorno);
        }

        public void uspActualizarEstado_RepartirClientes(MetasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarEstado_RepartirClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                   cmd.ExecuteNonQuery();
                }
            }
		}

        public void uspAsignarClienteInactivosSinCitaAVendedores(MetasDTO item, bool flagRepartirEquitativamenteSegunMeta)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspAsignarClienteInactivosSinCitaAVendedores", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    //cmd.Parameters.Add(new SqlParameter("@flagRepartirEquitativamenteSegunMeta", System.Data.SqlDbType.Bit)).Value = item.flagRepartirEquitativamenteSegunMeta;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void uspBuscarProspectosSinCitaVendedoresInactivosYRepartirVendedoresActivos(MetasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                ///ESTA SP BUSCA LOS PROSPECTOS SIN ACTIVIDAD DE LOS VENDEDORES INACTIVOS Y LOS REPARTE A LOS VENDEDORES ACTIVOS
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarProspectosSinCitaVendedoresInactivosYRepartirVendedoresActivos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void uspBuscarProspectosSinCitaVendedoresActivosYRepartir(MetasDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                ///ESTA SP BUSCA LOS PROSPECTOS SIN ACTIVIDAD DE LOS VENDEDORES ACTIVOS Y LOS REPARTE.
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarProspectosSinCitaVendedoresActivosYRepartir", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<MetasDTO> uspListarVerificadorCodigosComerciales(MetasDTO oitem)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarVerificadorCodigosComerciales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    Verificador_AsesorDeVentas = oIDataReader[oIDataReader.GetOrdinal("AsesorDeVentas")].ToString(),
                                    Verificador_Nuevo_Cant = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("_1")]),
                                    Verificador_Renov_Cant = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("_2")]),
                                    Verificador_Reins_Cant = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("_3")]),
                                    Verificador_Ampli_Cant = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("_4")]),
                                    Verificador_Anual_Cant = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("_5")])                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;          
        }
        //este procedimiento almacenado no existe
        public List<MetasDTO> uspListarVerificadorInformacionSociosComerciales_paginacion(MetasDTO oMetasDTO, Paging paging)
        {
            List<MetasDTO> lista = new List<MetasDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarVerificadorInformacionSociosComerciales_paginacion", conn))
                {              
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oMetasDTO.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oMetasDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oMetasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oMetasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar,100)).Value = oMetasDTO.AsesorDeVentas;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = oMetasDTO.Verificador_TipoIngreso;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDTO()
                                {
                                    Verificador_TipoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoIngreso")]),
                                    Verificador_NombreTipoIngreso = oIDataReader[oIDataReader.GetOrdinal("NombreTipoIngreso")].ToString(),
                                    Verificador_CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Verificador_NombresCliente = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Verificador_ApellidoCliente = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Verificador_DNICliente = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString(),
                                    Verificador_PaqueteCliente = oIDataReader[oIDataReader.GetOrdinal("Paquete")].ToString(),
                                    Verificador_FechaFinCliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    Verificador_FechaInicioCliente = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    Verificador_AsesorDeVentas = oIDataReader[oIDataReader.GetOrdinal("AsesorDeVentas")].ToString(),
                                    Verificador_CodigoSeguimiento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSeguimiento")]),
                                    Verificador_Seguimiento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Seguimiento")])
                                });

                            }
                        }

                    }
                }
            }
            return lista;
            
        }
        //este procedimiento almacenado no existe
        public MetasDTO uspListarVerificadorInformacionSociosComerciales_NumeroRegistros(MetasDTO oitem)
        {           
            MetasDTO itemDTO = null;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarVerificadorInformacionSociosComerciales_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@AsesorComercial", System.Data.SqlDbType.VarChar,100)).Value = oitem.AsesorDeVentas;
                    cmd.Parameters.Add(new SqlParameter("@TipoIngreso", System.Data.SqlDbType.Int)).Value = oitem.Verificador_TipoIngreso;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new MetasDTO()
                                {
                                    Verificador_CantidadTotalFilas = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])                                    
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }
        
    }
}
