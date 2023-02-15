using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_DataLayer
{
    public class PasarelaEmpresaData
    {

        public List<PasarelaEmpresaDTO> List(PasarelaEmpresaDTO oFiltro)
        {
            List<PasarelaEmpresaDTO> lista = new List<PasarelaEmpresaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarPasarelaPagoEmpresa", conn))
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
                                lista.Add(new PasarelaEmpresaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoPlantillaFormaPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPlantillaFormaPago")].ToString(),
                                    Valor1 = oIDataReader[oIDataReader.GetOrdinal("Valor1")].ToString(),
                                    Valor2 = oIDataReader[oIDataReader.GetOrdinal("Valor2")].ToString(),
                                    Valor3 = oIDataReader[oIDataReader.GetOrdinal("Valor3")].ToString(),
                                    DesFormaPago = oIDataReader[oIDataReader.GetOrdinal("DesFormaPago")].ToString(),
                                    UrlImagenFormaPago = oIDataReader[oIDataReader.GetOrdinal("UrlImagenFormaPago")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesFechaCreacion = oFiltro.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }

        public PasarelaEmpresaDTO SearchByCode(PasarelaEmpresaDTO oItem)
        {
            PasarelaEmpresaDTO itemDTO = new PasarelaEmpresaDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarPasarelaPagoEmpresa", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlantillaFormaPago", System.Data.SqlDbType.VarChar)).Value = oItem.CodigoPlantillaFormaPago;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new PasarelaEmpresaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoPlantillaFormaPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPlantillaFormaPago")].ToString(),
                                    Valor1 = oIDataReader[oIDataReader.GetOrdinal("Valor1")].ToString(),
                                    Valor2 = oIDataReader[oIDataReader.GetOrdinal("Valor2")].ToString(),
                                    Valor3 = oIDataReader[oIDataReader.GetOrdinal("Valor3")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesFechaCreacion = oItem.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        //active
        //*********************************************************** API ***********************************************

        public PasarelaEmpresaDTO ListPasarelaEMActive(PasarelaEmpresaDTO oItem)
        {
            PasarelaEmpresaDTO itemDTO = new PasarelaEmpresaDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarPasarelaPagoEmpresaActivoAppi", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar)).Value = oItem.DefaultKeyEmpresa;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new PasarelaEmpresaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoPlantillaFormaPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPlantillaFormaPago")].ToString(),
                                    Valor1 = oIDataReader[oIDataReader.GetOrdinal("Valor1")].ToString(),
                                    Valor2 = oIDataReader[oIDataReader.GetOrdinal("Valor2")].ToString(),
                                    Valor3 = oIDataReader[oIDataReader.GetOrdinal("Valor3")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesFechaCreacion = oItem.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DesFormaPago = oIDataReader[oIDataReader.GetOrdinal("DesFormaPago")].ToString(),
                                    UrlImagenFormaPago = oIDataReader[oIDataReader.GetOrdinal("UrlImagenFormaPago")].ToString(),
                                    MonedaSistema = oIDataReader[oIDataReader.GetOrdinal("MonedaSistema")].ToString(),

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        //get pasareal by code
        public PasarelaEmpresaDTO GetItemPasarelaByCode(PasarelaEmpresaDTO oItem)
        {
            PasarelaEmpresaDTO itemDTO = new PasarelaEmpresaDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarPasarelaPagoEmpresaAppi", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar)).Value = oItem.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlantillaFormaPago", System.Data.SqlDbType.VarChar)).Value = oItem.CodigoPlantillaFormaPago;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new PasarelaEmpresaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoPlantillaFormaPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPlantillaFormaPago")].ToString(),
                                    Valor1 = oIDataReader[oIDataReader.GetOrdinal("Valor1")].ToString(),
                                    Valor2 = oIDataReader[oIDataReader.GetOrdinal("Valor2")].ToString(),
                                    Valor3 = oIDataReader[oIDataReader.GetOrdinal("Valor3")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    DesFormaPago = oIDataReader[oIDataReader.GetOrdinal("DesFormaPago")].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }


        //list pasarela account by gym
        public List<PasarelaEmpresaDTO> ListPasarelaByEM(PasarelaEmpresaDTO oFiltro)
        {
            List<PasarelaEmpresaDTO> lista = new List<PasarelaEmpresaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarPasarelaPagoEmpresaActivoAppi", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar)).Value = oFiltro.DefaultKeyEmpresa;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PasarelaEmpresaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoPlantillaFormaPago = oIDataReader[oIDataReader.GetOrdinal("CodigoPlantillaFormaPago")].ToString(),
                                   
                                    DesFormaPago = oIDataReader[oIDataReader.GetOrdinal("DesFormaPago")].ToString(),
                                    UrlImagenFormaPago = oIDataReader[oIDataReader.GetOrdinal("UrlImagenFormaPago")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesFechaCreacion = oFiltro.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }


        //*********************************************************** END API ***********************************************
        public void Register(PasarelaEmpresaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarPasarelaPagoEmpresa", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlantillaFormaPago", System.Data.SqlDbType.VarChar)).Value = item.CodigoPlantillaFormaPago;
                    cmd.Parameters.Add(new SqlParameter("@Valor1", System.Data.SqlDbType.VarChar)).Value = item.Valor1;
                    cmd.Parameters.Add(new SqlParameter("@Valor2", System.Data.SqlDbType.VarChar)).Value = item.Valor2;
                    cmd.Parameters.Add(new SqlParameter("@Valor3", System.Data.SqlDbType.VarChar)).Value = item.Valor3;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(PasarelaEmpresaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarPasarelaPagoEmpresa", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlantillaFormaPago", System.Data.SqlDbType.VarChar)).Value = item.CodigoPlantillaFormaPago;
                    cmd.Parameters.Add(new SqlParameter("@Valor1", System.Data.SqlDbType.VarChar)).Value = item.Valor1;
                    cmd.Parameters.Add(new SqlParameter("@Valor2", System.Data.SqlDbType.VarChar)).Value = item.Valor2;
                    cmd.Parameters.Add(new SqlParameter("@Valor3", System.Data.SqlDbType.VarChar)).Value = item.Valor3;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Destroy(PasarelaEmpresaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspEliminarPasarelaPagoEmpresa", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPlantillaFormaPago", System.Data.SqlDbType.VarChar)).Value = item.CodigoPlantillaFormaPago;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
