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
    public class gimnasio_crm_2_etapasplantillaData
    {
        public List<gimnasio_crm_2_etapasplantillaDTO> CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla(gimnasio_crm_2_etapasplantillaDTO request)
        {
            List<gimnasio_crm_2_etapasplantillaDTO> lista = new List<gimnasio_crm_2_etapasplantillaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_gimnasio_crm_2_etapasplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = request.CodigoEmbudoVenta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new gimnasio_crm_2_etapasplantillaDTO();

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
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoEtapa")))
                                {
                                    itemDTO.CodigoEtapa = (oIDataReader[oIDataReader.GetOrdinal("CodigoEtapa")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NombreEtapa")))
                                {
                                    itemDTO.NombreEtapa = (oIDataReader[oIDataReader.GetOrdinal("NombreEtapa")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("OrdenEtapa")))
                                {
                                    itemDTO.OrdenEtapa = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("OrdenEtapa")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProbabilidadNegocio")))
                                {
                                    itemDTO.ProbabilidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ProbabilidadNegocio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NegocioEstancandose")))
                                {
                                    itemDTO.NegocioEstancandose = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("NegocioEstancandose")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiasAvisoInactividad")))
                                {
                                    itemDTO.DiasAvisoInactividad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiasAvisoInactividad")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Estado")))
                                {
                                    itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                }
                           
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("UsuarioCreacion")))
                                {
                                    itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaCreacion")))
                                {
                                    itemDTO.FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]);
                                }


                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        public gimnasio_crm_2_etapasplantillaDTO CentroEntrenamiento_uspBuscar_gimnasio_crm_2_etapasplantilla(gimnasio_crm_2_etapasplantillaDTO request)
        {
            gimnasio_crm_2_etapasplantillaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscar_gimnasio_crm_2_etapasplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = request.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEtapa", System.Data.SqlDbType.VarChar)).Value = request.CodigoEtapa;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new gimnasio_crm_2_etapasplantillaDTO()
                                {
                                   CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                   CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                   CodigoEmbudoVenta = (oIDataReader[oIDataReader.GetOrdinal("CodigoEmbudoVenta")].ToString()),
                                   CodigoEtapa = (oIDataReader[oIDataReader.GetOrdinal("CodigoEtapa")].ToString()),
                                   NombreEtapa = (oIDataReader[oIDataReader.GetOrdinal("NombreEtapa")].ToString()),
                                   OrdenEtapa = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("OrdenEtapa")]),
                                   ProbabilidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ProbabilidadNegocio")]),
                                   NegocioEstancandose = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("NegocioEstancandose")]),
                                   DiasAvisoInactividad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiasAvisoInactividad")]),
                                   Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                   UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                   FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void CentroEntrenamiento_uspRegistrar_gimnasio_crm_2_etapasplantilla(gimnasio_crm_2_etapasplantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrar_gimnasio_crm_2_etapasplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@NombreEtapa", System.Data.SqlDbType.VarChar,100)).Value = item.NombreEtapa;
                    cmd.Parameters.Add(new SqlParameter("@OrdenEtapa", System.Data.SqlDbType.Int)).Value = item.OrdenEtapa;
                    cmd.Parameters.Add(new SqlParameter("@ProbabilidadNegocio", System.Data.SqlDbType.Int)).Value = item.ProbabilidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@NegocioEstancandose", System.Data.SqlDbType.Bit)).Value = item.NegocioEstancandose;
                    cmd.Parameters.Add(new SqlParameter("@DiasAvisoInactividad", System.Data.SqlDbType.Int)).Value = item.DiasAvisoInactividad;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void CentroEntrenamiento_uspActualizar_gimnasio_crm_2_etapasplantilla(gimnasio_crm_2_etapasplantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_gimnasio_crm_2_etapasplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEtapa", System.Data.SqlDbType.VarChar)).Value = item.CodigoEtapa;
                    cmd.Parameters.Add(new SqlParameter("@NombreEtapa", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreEtapa;
                    cmd.Parameters.Add(new SqlParameter("@OrdenEtapa", System.Data.SqlDbType.Int)).Value = item.OrdenEtapa;
                    cmd.Parameters.Add(new SqlParameter("@ProbabilidadNegocio", System.Data.SqlDbType.Int)).Value = item.ProbabilidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@NegocioEstancandose", System.Data.SqlDbType.Bit)).Value = item.NegocioEstancandose;
                    cmd.Parameters.Add(new SqlParameter("@DiasAvisoInactividad", System.Data.SqlDbType.Int)).Value = item.DiasAvisoInactividad;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void CentroEntrenamiento_uspEliminar_gimnasio_crm_2_etapasplantilla(gimnasio_crm_2_etapasplantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminar_gimnasio_crm_2_etapasplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEtapa", System.Data.SqlDbType.VarChar)).Value = item.CodigoEtapa;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }

            }
        }

    }
}
