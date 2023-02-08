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
    public class gimnasio_crm_1_embudosventaplantillaData
    {

        public List<gimnasio_crm_1_embudosventaplantillaDTO> CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO request)
        {
            List<gimnasio_crm_1_embudosventaplantillaDTO> lista = new List<gimnasio_crm_1_embudosventaplantillaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_gimnasio_crm_1_embudosventaplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new gimnasio_crm_1_embudosventaplantillaDTO();

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
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Nombre")))
                                {
                                    itemDTO.Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Descripcion")))
                                {
                                    itemDTO.Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString();
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

        public gimnasio_crm_1_embudosventaplantillaDTO CentroEntrenamiento_uspBuscar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO request)
        {
            gimnasio_crm_1_embudosventaplantillaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscar_gimnasio_crm_1_embudosventaplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = request.CodigoEmbudoVenta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new gimnasio_crm_1_embudosventaplantillaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoEmbudoVenta = oIDataReader[oIDataReader.GetOrdinal("CodigoEmbudoVenta")].ToString(),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombre")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                 
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void CentroEntrenamiento_uspRegistrar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrar_gimnasio_crm_1_embudosventaplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 250)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    
                    cmd.ExecuteNonQuery();
                }

            }            
        }

        public void CentroEntrenamiento_uspActualizar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_gimnasio_crm_1_embudosventaplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 250)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void CentroEntrenamiento_uspEliminar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminar_gimnasio_crm_1_embudosventaplantilla", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEmbudoVenta", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoEmbudoVenta;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }

            }
        }


    }

}
