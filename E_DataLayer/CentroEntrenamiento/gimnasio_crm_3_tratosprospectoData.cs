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
    public class gimnasio_crm_3_tratosprospectoData
    {

        public List<gimnasio_crm_3_tratosprospectoDTO> CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto(gimnasio_crm_3_tratosprospectoDTO request)
        {
            List<gimnasio_crm_3_tratosprospectoDTO> lista = new List<gimnasio_crm_3_tratosprospectoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_gimnasio_crm_3_tratosprospecto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = request.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEstadoEtapa", System.Data.SqlDbType.Int)).Value = request.CodigoEstadoEtapa;
                    cmd.Parameters.Add(new SqlParameter("@Vendedor", System.Data.SqlDbType.VarChar)).Value = request.Vendedor;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar,50)).Value = request.Nombres;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new gimnasio_crm_3_tratosprospectoDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoUnidadNegocio")))
                                {
                                    itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSede")))
                                {
                                    itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoEmbudoVenta")))
                                {
                                    itemDTO.CodigoEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("CodigoEmbudoVenta")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesEmbudoVenta")))
                                {
                                    itemDTO.DesEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("DesEmbudoVenta")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoEtapa")))
                                {
                                    itemDTO.CodigoEtapa = (oIDataReader[oIDataReader.GetOrdinal("CodigoEtapa")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NombreEtapa")))
                                {
                                    itemDTO.NombreEtapa = (oIDataReader[oIDataReader.GetOrdinal("NombreEtapa")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoTratoProspecto")))
                                {
                                    itemDTO.CodigoTratoProspecto = (oIDataReader[oIDataReader.GetOrdinal("CodigoTratoProspecto")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NombreTrato")))
                                {
                                    itemDTO.NombreTrato = (oIDataReader[oIDataReader.GetOrdinal("NombreTrato")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoEstadoEtapa")))
                                {
                                    itemDTO.CodigoEstadoEtapa = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEstadoEtapa")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesEstadoEtapa")))
                                {
                                    itemDTO.DesEstadoEtapa = (oIDataReader[oIDataReader.GetOrdinal("DesEstadoEtapa")].ToString());
                                }
                                
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaPrevistaCierre")))
                                {
                                    itemDTO.FechaPrevistaCierre = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPrevistaCierre")]);
                                } 
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaPrevistaCierre")))
                                {
                                    itemDTO.DesFechaPrevistaCierre = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPrevistaCierre")]).ToString("dd/MM/yyy");
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoMoneda")))
                                {
                                    itemDTO.CodigoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMoneda")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Valor")))
                                {
                                    itemDTO.Valor = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Valor")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoOrigenProspecto")))
                                {
                                    itemDTO.CodigoOrigenProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoOrigenProspecto")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesOrigenProspecto")))
                                {
                                    itemDTO.DesOrigenProspecto = (oIDataReader[oIDataReader.GetOrdinal("DesOrigenProspecto")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ColorOrigenProspecto")))
                                {
                                    itemDTO.ColorOrigenProspecto = (oIDataReader[oIDataReader.GetOrdinal("ColorOrigenProspecto")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProspecto")))
                                {
                                    itemDTO.CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Nombres")))
                                {
                                    itemDTO.Nombres = (oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Apellidos")))
                                {
                                    itemDTO.Apellidos = (oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Celular")))
                                {
                                    itemDTO.Celular = (oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Vendedor")))
                                {
                                    itemDTO.Vendedor = (oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString());
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("UsuarioCreacion")))
                                {
                                    itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaCreacion")))
                                {
                                    itemDTO.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("UsuarioEdicion")))
                                {
                                    itemDTO.UsuarioEdicion = oIDataReader[oIDataReader.GetOrdinal("UsuarioEdicion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaEdicion")))
                                {
                                    itemDTO.FechaEdicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEdicion")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ColorEstadoActividad")))
                                {
                                    itemDTO.ColorEstadoActividad = (oIDataReader[oIDataReader.GetOrdinal("ColorEstadoActividad")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("IconoEstadoActividad")))
                                {
                                    itemDTO.IconoEstadoActividad = (oIDataReader[oIDataReader.GetOrdinal("IconoEstadoActividad")].ToString());
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesObjetivo")))
                                {
                                    itemDTO.DesObjetivo = (oIDataReader[oIDataReader.GetOrdinal("DesObjetivo")].ToString());
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesComoConocioGym")))
                                {
                                    itemDTO.DesComoConocioGym = (oIDataReader[oIDataReader.GetOrdinal("DesComoConocioGym")].ToString());
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        public List<gimnasio_crm_3_tratosprospectoDTO> CentroEntrenamiento_uspListar_gimnasio_crm_4_etapahistorial(gimnasio_crm_3_tratosprospectoDTO request)
        {
            List<gimnasio_crm_3_tratosprospectoDTO> lista = new List<gimnasio_crm_3_tratosprospectoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_gimnasio_crm_4_etapahistorial", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = request.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTratoProspecto", System.Data.SqlDbType.VarChar)).Value = request.CodigoTratoProspecto;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new gimnasio_crm_3_tratosprospectoDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoUnidadNegocio")))
                                {
                                    itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSede")))
                                {
                                    itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoEmbudoVenta")))
                                {
                                    itemDTO.CodigoEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("CodigoEmbudoVenta")].ToString());
                                }
                                //if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesEmbudoVenta")))
                                //{
                                //    itemDTO.DesEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("DesEmbudoVenta")].ToString());
                                //}
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoEtapa")))
                                {
                                    itemDTO.CodigoEtapa = (oIDataReader[oIDataReader.GetOrdinal("CodigoEtapa")].ToString());
                                }
                                //if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NombreEtapa")))
                                //{
                                //    itemDTO.NombreEtapa = (oIDataReader[oIDataReader.GetOrdinal("NombreEtapa")].ToString());
                                //}
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoTratoProspecto")))
                                {
                                    itemDTO.CodigoTratoProspecto = (oIDataReader[oIDataReader.GetOrdinal("CodigoTratoProspecto")].ToString());
                                }
                                //if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NombreTrato")))
                                //{
                                //    itemDTO.NombreTrato = (oIDataReader[oIDataReader.GetOrdinal("NombreTrato")].ToString());
                                //}
                               
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NombreEtapa")))
                                {
                                    itemDTO.NombreEtapa = (oIDataReader[oIDataReader.GetOrdinal("NombreEtapa")].ToString());
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("UsuarioCreacion")))
                                {
                                    itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaCreacion")))
                                {
                                    itemDTO.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaCreacion")))
                                {
                                    itemDTO.DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy hh:mm tt");
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        public gimnasio_crm_3_tratosprospectoDTO CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto(gimnasio_crm_3_tratosprospectoDTO request)
        {
            gimnasio_crm_3_tratosprospectoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscar_gimnasio_crm_3_tratosprospecto", conn)) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = request.CodigoEmbudoVenta;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoTratoProspecto", System.Data.SqlDbType.VarChar)).Value = request.CodigoTratoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigenProspecto", System.Data.SqlDbType.Int, 10)).Value = request.CodigoOrigenProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int, 10)).Value = request.CodigoProspecto;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new gimnasio_crm_3_tratosprospectoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("CodigoEmbudoVenta")].ToString()),
                                    DesEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("DesEmbudoVenta")].ToString()),
                                    CodigoEtapa = (oIDataReader[oIDataReader.GetOrdinal("CodigoEtapa")].ToString()),
                                    NombreEtapa = (oIDataReader[oIDataReader.GetOrdinal("NombreEtapa")].ToString()),
                                    CodigoTratoProspecto = (oIDataReader[oIDataReader.GetOrdinal("CodigoTratoProspecto")].ToString()),
                                    NombreTrato = (oIDataReader[oIDataReader.GetOrdinal("NombreTrato")].ToString()),
                                    CodigoEstadoEtapa = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEstadoEtapa")]),
                                    DesEstadoEtapa = (oIDataReader[oIDataReader.GetOrdinal("DesEstadoEtapa")].ToString()),
                                    FechaPrevistaCierre = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPrevistaCierre")]),
                                    CodigoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMoneda")]),
                                    Valor = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Valor")]),
                                    CodigoOrigenProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoOrigenProspecto")]),
                                    CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]),
                                    Nombres = (oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString()),
                                    Apellidos = (oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString()),
                                    Celular = (oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString()),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                    //UsuarioEdicion = oIDataReader[oIDataReader.GetOrdinal("UsuarioEdicion")].ToString(),
                                    //FechaEdicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEdicion")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public gimnasio_crm_3_tratosprospectoDTO CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto(gimnasio_crm_3_tratosprospectoDTO request)
        {
            gimnasio_crm_3_tratosprospectoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscar_gimnasio_crm_3_tratosprospecto_abierto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = request.CodigoEmbudoVenta;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoTratoProspecto", System.Data.SqlDbType.VarChar)).Value = request.CodigoTratoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigenProspecto", System.Data.SqlDbType.Int, 10)).Value = request.CodigoOrigenProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int, 10)).Value = request.CodigoProspecto;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new gimnasio_crm_3_tratosprospectoDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("CodigoEmbudoVenta")].ToString()),
                                    DesEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("DesEmbudoVenta")].ToString()),
                                    CodigoEtapa = (oIDataReader[oIDataReader.GetOrdinal("CodigoEtapa")].ToString()),
                                    NombreEtapa = (oIDataReader[oIDataReader.GetOrdinal("NombreEtapa")].ToString()),
                                    CodigoTratoProspecto = (oIDataReader[oIDataReader.GetOrdinal("CodigoTratoProspecto")].ToString()),
                                    NombreTrato = (oIDataReader[oIDataReader.GetOrdinal("NombreTrato")].ToString()),
                                    CodigoEstadoEtapa = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEstadoEtapa")]),
                                    DesEstadoEtapa = (oIDataReader[oIDataReader.GetOrdinal("DesEstadoEtapa")].ToString()),
                                    FechaPrevistaCierre = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPrevistaCierre")]),
                                    CodigoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMoneda")]),
                                    Valor = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Valor")]),
                                    CodigoOrigenProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoOrigenProspecto")]),
                                    CodigoProspecto = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoProspecto")]),
                                    Nombres = (oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString()),
                                    Apellidos = (oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString()),
                                    Celular = (oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString()),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                    //UsuarioEdicion = oIDataReader[oIDataReader.GetOrdinal("UsuarioEdicion")].ToString(),
                                    //FechaEdicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEdicion")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto(gimnasio_crm_3_tratosprospectoDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrar_gimnasio_crm_3_tratosprospecto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEtapa", System.Data.SqlDbType.VarChar)).Value = item.CodigoEtapa;
                    cmd.Parameters.Add(new SqlParameter("@NombreTrato", System.Data.SqlDbType.VarChar)).Value = item.NombreTrato;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEstadoEtapa", System.Data.SqlDbType.Int)).Value = item.CodigoEstadoEtapa;
                    cmd.Parameters.Add(new SqlParameter("@FechaPrevistaCierre", System.Data.SqlDbType.DateTime)).Value = item.FechaPrevistaCierre;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMoneda", System.Data.SqlDbType.Int)).Value = item.CodigoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@Valor", System.Data.SqlDbType.Decimal)).Value = item.Valor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigenProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoOrigenProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.Vendedor;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void uspActualizar_gimnasio_crm_3_tratosprospecto(gimnasio_crm_3_tratosprospectoDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_gimnasio_crm_3_tratosprospecto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTratoProspecto", System.Data.SqlDbType.VarChar)).Value = item.CodigoTratoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEtapa", System.Data.SqlDbType.VarChar)).Value = item.CodigoEtapa;
                    cmd.Parameters.Add(new SqlParameter("@NombreTrato", System.Data.SqlDbType.VarChar)).Value = item.NombreTrato;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEstadoEtapa", System.Data.SqlDbType.Int)).Value = item.CodigoEstadoEtapa;
                    cmd.Parameters.Add(new SqlParameter("@FechaPrevistaCierre", System.Data.SqlDbType.DateTime)).Value = item.FechaPrevistaCierre;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMoneda", System.Data.SqlDbType.Int)).Value = item.CodigoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@Valor", System.Data.SqlDbType.Decimal)).Value = item.Valor;
                    cmd.Parameters.Add(new SqlParameter("@Nota", System.Data.SqlDbType.VarChar,100)).Value = item.Nota;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigenProspecto", System.Data.SqlDbType.Int)).Value = item.CodigoOrigenProspecto;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void uspActualizar_gimnasio_crm_3_tratosprospectoEstado(gimnasio_crm_3_tratosprospectoDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_gimnasio_crm_3_tratosprospectoEstado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTratoProspecto", System.Data.SqlDbType.VarChar)).Value = item.CodigoTratoProspecto;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoEstadoEtapa", System.Data.SqlDbType.Int)).Value = item.CodigoEstadoEtapa;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void uspActualizar_gimnasio_crm_3_tratosprospectoEtapa(gimnasio_crm_3_tratosprospectoDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_gimnasio_crm_3_tratosprospectoEtapa", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTratoProspecto", System.Data.SqlDbType.VarChar)).Value = item.CodigoTratoProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEtapa", System.Data.SqlDbType.VarChar)).Value = item.CodigoEtapa;                    
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public int uspValidarExisteTratoAbiertoEmbudo_gimnasio_crm_3_tratosprospecto(gimnasio_crm_3_tratosprospectoDTO request)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarExisteTratoAbiertoEmbudo_gimnasio_crm_3_tratosprospecto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = request.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEtapa", System.Data.SqlDbType.VarChar)).Value = request.CodigoEtapa;              
                    cmd.Parameters.Add(new SqlParameter("@CodigoOrigenProspecto", System.Data.SqlDbType.Int, 10)).Value = request.CodigoOrigenProspecto;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProspecto", System.Data.SqlDbType.VarChar)).Value = request.CodigoProspecto;
                    cmd.Parameters.AddWithValue("@Existe", campoRetorno).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

    }
}
