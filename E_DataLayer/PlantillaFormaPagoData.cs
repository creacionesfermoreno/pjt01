using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using E_DataModel;

namespace E_DataLayer
{
    public class PlantillaFormaPagoData
    {
        public List<PlantillaFormaPagoDTO> ecommerce_uspListarAdminFormaPago(PlantillaFormaPagoDTO oFiltro)
        {
            List<PlantillaFormaPagoDTO> lista = new List<PlantillaFormaPagoDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarAdminFormaPago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlantillaFormaPagoDTO()
                                {
                                    CodigoPlantillaFormaPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPlantillaFormaPago")].ToString(),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        public List<PlantillaFormaPagoDTO> listPasarelaTypes(PlantillaFormaPagoDTO oFiltro)
        {
            List<PlantillaFormaPagoDTO> lista = new List<PlantillaFormaPagoDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarPlantillaFormaPago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PlantillaFormaPagoDTO()
                                {
                                    CodigoPlantillaFormaPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPlantillaFormaPago")].ToString(),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public void ecommerce_uspRegistrarFormaPago_MercadoPago(PlantillaFormaPagoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarFormaPago_MercadoPago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                   
                    if (!string.IsNullOrEmpty(item.MercadoPago_Publickey))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Publickey", System.Data.SqlDbType.VarChar, 200)).Value = item.MercadoPago_Publickey;
                    }
                    if (!string.IsNullOrEmpty(item.MercadoPago_Accesstoken))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Accesstoken", System.Data.SqlDbType.VarChar, 200)).Value = item.MercadoPago_Accesstoken;
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.MercadoPago_Estado;
                    
                    cmd.ExecuteNonQuery();
                    //ID = cmd.Parameters["@po_CodigoProducto"].Value.ToString();
                }

            }
        }

        public PlantillaFormaPagoDTO ecommerce_uspBuscarFormaPago_MercadoPago(PlantillaFormaPagoDTO oFiltro)
        {
            PlantillaFormaPagoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarFormaPago_MercadoPago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new PlantillaFormaPagoDTO()
                                {
                                    MercadoPago_Publickey = oIDataReader[oIDataReader.GetOrdinal("Publickey")].ToString(),
                                    MercadoPago_Accesstoken = oIDataReader[oIDataReader.GetOrdinal("Accesstoken")].ToString(),
                                    MercadoPago_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void ecommerce_uspRegistrarFormaPago_Yape(PlantillaFormaPagoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarFormaPago_Yape", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    if (!string.IsNullOrEmpty(item.Yape_NroCelular))
                    {
                        cmd.Parameters.Add(new SqlParameter("@NroCelular", System.Data.SqlDbType.VarChar, 100)).Value = item.Yape_NroCelular;
                    }
                    if (!string.IsNullOrEmpty(item.Yape_CodigoQR))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoQR", System.Data.SqlDbType.VarChar, 500)).Value = item.Yape_CodigoQR;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Yape_Estado;

                    cmd.ExecuteNonQuery();                    
                }

            }
        }

        public PlantillaFormaPagoDTO ecommerce_uspBuscarFormaPago_Yape(PlantillaFormaPagoDTO oFiltro)
        {
            PlantillaFormaPagoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarFormaPago_Yape", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new PlantillaFormaPagoDTO()
                                {
                                    Yape_NroCelular = oIDataReader[oIDataReader.GetOrdinal("NroCelular")].ToString(),
                                    Yape_CodigoQR = oIDataReader[oIDataReader.GetOrdinal("CodigoQR")].ToString(),
                                    Yape_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void ecommerce_uspRegistrarFormaPago_ContraEntrega(PlantillaFormaPagoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarFormaPago_ContraEntrega", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    if (!string.IsNullOrEmpty(item.ContraEntrega_InstruccionesCorreo))
                    {
                        cmd.Parameters.Add(new SqlParameter("@InstruccionesCorreo", System.Data.SqlDbType.VarChar, 500)).Value = item.ContraEntrega_InstruccionesCorreo;
                    }
                    if (!string.IsNullOrEmpty(item.ContraEntrega_InstruccionesCheckout))
                    {
                        cmd.Parameters.Add(new SqlParameter("@InstruccionesCheckout", System.Data.SqlDbType.VarChar, 500)).Value = item.ContraEntrega_InstruccionesCheckout;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.ContraEntrega_Estado;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public PlantillaFormaPagoDTO ecommerce_uspBuscarFormaPago_ContraEntrega(PlantillaFormaPagoDTO oFiltro)
        {
            PlantillaFormaPagoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarFormaPago_ContraEntrega", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new PlantillaFormaPagoDTO()
                                {
                                    ContraEntrega_InstruccionesCorreo = oIDataReader[oIDataReader.GetOrdinal("InstruccionesCorreo")].ToString(),
                                    ContraEntrega_InstruccionesCheckout = oIDataReader[oIDataReader.GetOrdinal("InstruccionesCheckout")].ToString(),
                                    ContraEntrega_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
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
