using E_DataModel.Gimnasio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace E_DataLayer.Gimnasio
{
    public class WhatsappConfigData
    {

        public List<WhatsappConfigDTO> List(WhatsappConfigDTO oFiltro)
        {
            List<WhatsappConfigDTO> lista = new List<WhatsappConfigDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_WhatsappConfiguracion", conn))
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
                                lista.Add(new WhatsappConfigDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoWhatsappConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoWhatsappConfiguracion")].ToString(),
                                    IdPhone = oIDataReader[oIDataReader.GetOrdinal("IdPhone")].ToString(),
                                    IdAccount = oIDataReader[oIDataReader.GetOrdinal("IdAccount")].ToString(),
                                    Token = oIDataReader[oIDataReader.GetOrdinal("Token")].ToString(),
                                    SDK = oIDataReader[oIDataReader.GetOrdinal("SDK")].ToString(),
                                    NumberPhone = oIDataReader[oIDataReader.GetOrdinal("NumberPhone")].ToString(),
                                    DesDateCreate = oFiltro.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                });
                            }
                        }

                    }
                }
            }


            return lista;
        }

        public WhatsappConfigDTO SearchByCode(WhatsappConfigDTO oWhatsappConfigDTO)
        {
            WhatsappConfigDTO itemDTO = new WhatsappConfigDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscar_WhatsappConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oWhatsappConfigDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oWhatsappConfigDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappConfiguracion", System.Data.SqlDbType.VarChar)).Value = oWhatsappConfigDTO.CodigoWhatsappConfiguracion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new WhatsappConfigDTO()
                                {
                                  //  CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                  //  CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                   // CodigoWhatsappConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoWhatsappConfiguracion")].ToString(),
                                    IdPhone = oIDataReader[oIDataReader.GetOrdinal("IdPhone")].ToString(),
                                    IdAccount = oIDataReader[oIDataReader.GetOrdinal("IdAccount")].ToString(),
                                    IdentificadorApp = oIDataReader[oIDataReader.GetOrdinal("IdentificadorApp")].ToString(),
                                    Token = oIDataReader[oIDataReader.GetOrdinal("Token")].ToString(),
                                    SDK = oIDataReader[oIDataReader.GetOrdinal("SDK")].ToString(),
                                    NumberPhone = oIDataReader[oIDataReader.GetOrdinal("NumberPhone")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    DesDateCreate = oWhatsappConfigDTO.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void Register(WhatsappConfigDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrar_WhatsappConfiguracion", conn))
                {                  
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappConfiguracion", System.Data.SqlDbType.Int)).Value = item.CodigoWhatsappConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@IdentificadorApp", System.Data.SqlDbType.VarChar)).Value = item.IdentificadorApp;
                    cmd.Parameters.Add(new SqlParameter("@IdPhone", System.Data.SqlDbType.VarChar)).Value = item.IdPhone;
                    cmd.Parameters.Add(new SqlParameter("@IdAccount", System.Data.SqlDbType.VarChar)).Value = item.IdAccount;
                    cmd.Parameters.Add(new SqlParameter("@Token", System.Data.SqlDbType.VarChar)).Value = item.Token;
                    cmd.Parameters.Add(new SqlParameter("@SDK", System.Data.SqlDbType.VarChar)).Value = item.SDK;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@NumberPhone", System.Data.SqlDbType.VarChar)).Value = item.NumberPhone;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(WhatsappConfigDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_WhatsappConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappConfiguracion", System.Data.SqlDbType.VarChar)).Value = item.CodigoWhatsappConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@IdPhone", System.Data.SqlDbType.VarChar)).Value = item.IdPhone;
                    cmd.Parameters.Add(new SqlParameter("@IdAccount", System.Data.SqlDbType.VarChar)).Value = item.IdAccount;
                    cmd.Parameters.Add(new SqlParameter("@Token", System.Data.SqlDbType.VarChar)).Value = item.Token;
                    cmd.Parameters.Add(new SqlParameter("@SDK", System.Data.SqlDbType.VarChar)).Value = item.SDK;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@NumberPhone", System.Data.SqlDbType.VarChar)).Value = item.NumberPhone;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Destroy(WhatsappConfigDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminar_WhatsappConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappConfiguracion", System.Data.SqlDbType.VarChar)).Value = item.CodigoWhatsappConfiguracion;
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //*********************************************** START CAMPAÑA*************************

        public List<WhatsappConfigDTO> ListCampaign(WhatsappConfigDTO oFiltro)
        {
            List<WhatsappConfigDTO> lista = new List<WhatsappConfigDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_WhatsappCampanias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappConfiguracion", System.Data.SqlDbType.VarChar)).Value = oFiltro.CodigoWhatsappConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oFiltro.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oFiltro.FechaFin;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                WhatsappConfigDTO m = new WhatsappConfigDTO();
                                m.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                m.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                m.CodigoWhatsappCampania = oIDataReader[oIDataReader.GetOrdinal("CodigoWhatsappCampania")].ToString();
                                m.CodigoWhatsappConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoWhatsappConfiguracion")].ToString();
                                m.NombreWhatsappCampania = oIDataReader[oIDataReader.GetOrdinal("NombreWhatsappCampania")].ToString();
                                m.UrlDestinatarios = oIDataReader[oIDataReader.GetOrdinal("UrlDestinatarios")].ToString();
                                m.IdTemplate = oIDataReader[oIDataReader.GetOrdinal("IdTemplate")].ToString();
                                m.NameTemplate = oIDataReader[oIDataReader.GetOrdinal("NameTemplate")].ToString();
                                m.Languaje = oIDataReader[oIDataReader.GetOrdinal("Lenguage")].ToString();
                                m.TypeHeader = oIDataReader[oIDataReader.GetOrdinal("TypeHeader")].ToString();
                                m.ParametersHeader = oIDataReader[oIDataReader.GetOrdinal("ParametersHeader")].ToString();
                                m.ParametersBody = oIDataReader[oIDataReader.GetOrdinal("ParametersBody")].ToString();
                                m.EstadoWhatsappCampania = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoWhatsappCampania")]);
                                m.DescFechaCreacion = oFiltro.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]));
                                m.DateGlobalization = oFiltro.DateParseText(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]));
                                m.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();
                                m.DesColaDestino = oIDataReader[oIDataReader.GetOrdinal("DesColaDestino")].ToString();


                                m.Total = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Total")]);
                                m.TotalEnviado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TotalEnviado")]);
                                m.TotalError = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TotalError")]);

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraProgramado")))
                                {
                                    m.DescFechaHoraProgramado = oFiltro.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraProgramado")]));
                                }
                                else
                                {
                                    m.DescFechaHoraProgramado = "";
                                }

                                lista.Add(m);
                            }
                        }

                    }
                }
            }


            return lista;
        }

        public WhatsappConfigDTO SearchByCodeCampaign(WhatsappConfigDTO oWhatsappConfigDTO)
        {
            WhatsappConfigDTO itemDTO = new WhatsappConfigDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscar_WhatsappCampanias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oWhatsappConfigDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oWhatsappConfigDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = oWhatsappConfigDTO.CodigoWhatsappCampania;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                itemDTO.ColaDestino = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ColaDestino")]);
                                itemDTO.TiempoRespuesta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TiempoRespuesta")]);

                                itemDTO.CodigoWhatsappCampania = oIDataReader[oIDataReader.GetOrdinal("CodigoWhatsappCampania")].ToString();
                                itemDTO.CodigoWhatsappConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoWhatsappConfiguracion")].ToString();
                                itemDTO.NombreWhatsappCampania = oIDataReader[oIDataReader.GetOrdinal("NombreWhatsappCampania")].ToString();
                                itemDTO.UrlDestinatarios = oIDataReader[oIDataReader.GetOrdinal("UrlDestinatarios")].ToString();
                                itemDTO.IdTemplate = oIDataReader[oIDataReader.GetOrdinal("IdTemplate")].ToString();
                                itemDTO.NameTemplate = oIDataReader[oIDataReader.GetOrdinal("NameTemplate")].ToString();
                                itemDTO.Languaje = oIDataReader[oIDataReader.GetOrdinal("Lenguage")].ToString();
                                itemDTO.TypeHeader = oIDataReader[oIDataReader.GetOrdinal("TypeHeader")].ToString();
                                itemDTO.ParametersHeader = oIDataReader[oIDataReader.GetOrdinal("ParametersHeader")].ToString();
                                itemDTO.ParametersBody = oIDataReader[oIDataReader.GetOrdinal("ParametersBody")].ToString();
                                itemDTO.EstadoWhatsappCampania = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoWhatsappCampania")]);
                                itemDTO.DescFechaCreacion = oWhatsappConfigDTO.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]));
                                itemDTO.UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraProgramado")))
                                {
                                    itemDTO.DescFechaHoraProgramado = oWhatsappConfigDTO.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraProgramado")]));
                                }
                                else
                                {
                                    itemDTO.DescFechaHoraProgramado = "";
                                }
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public string RegisterCampaign(WhatsappConfigDTO item)
        {
            SqlParameter parametro = new SqlParameter("@CodigoWhatsappCampania", System.Data.SqlDbType.VarChar, 50);
            parametro.Value = "";
            parametro.Direction = System.Data.ParameterDirection.Output;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrar_WhatsappCampanias", conn))
                {  
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NombreWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = item.NombreWhatsappCampania;
                    cmd.Parameters.Add(new SqlParameter("@UrlDestinatarios", System.Data.SqlDbType.VarChar)).Value = item.UrlDestinatarios;
                    cmd.Parameters.Add(new SqlParameter("@ColaDestino", System.Data.SqlDbType.Int)).Value = item.ColaDestino;
                    cmd.Parameters.Add(new SqlParameter("@TiempoRespuesta", System.Data.SqlDbType.Int)).Value = item.TiempoRespuesta;
                    cmd.Parameters.Add(new SqlParameter("@IdTemplate", System.Data.SqlDbType.VarChar)).Value = item.IdTemplate;
                    cmd.Parameters.Add(new SqlParameter("@Lenguage", System.Data.SqlDbType.VarChar)).Value = item.Languaje;
                    cmd.Parameters.Add(new SqlParameter("@NameTemplate", System.Data.SqlDbType.VarChar)).Value = item.NameTemplate;
                    cmd.Parameters.Add(new SqlParameter("@TypeHeader", System.Data.SqlDbType.VarChar)).Value = item.TypeHeader;
                    cmd.Parameters.Add(new SqlParameter("@ParametersHeader", System.Data.SqlDbType.VarChar)).Value = item.ParametersHeader;
                    cmd.Parameters.Add(new SqlParameter("@ParametersBody", System.Data.SqlDbType.VarChar)).Value = item.ParametersBody;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraProgramado", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraProgramado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappConfiguracion", System.Data.SqlDbType.VarChar)).Value = item.CodigoWhatsappConfiguracion;
                    cmd.Parameters.Add(parametro);
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    //code = Convert.ToString(cmd.Parameters["@CodigoWhatsappCampania"].Value);
                }
            }
            return parametro.Value.ToString();
        }

        public void UpdateCampaign(WhatsappConfigDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_WhatsappCampanias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoWhatsappCampania;
                    cmd.Parameters.Add(new SqlParameter("@NombreWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = item.NombreWhatsappCampania;
                    cmd.Parameters.Add(new SqlParameter("@UrlDestinatarios", System.Data.SqlDbType.VarChar)).Value = item.UrlDestinatarios;
                    cmd.Parameters.Add(new SqlParameter("@ParametersHeader", System.Data.SqlDbType.VarChar)).Value = item.ParametersHeader;
                    cmd.Parameters.Add(new SqlParameter("@ParametersBody", System.Data.SqlDbType.VarChar)).Value = item.ParametersBody;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraProgramado", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraProgramado;
                    //if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    //{
                    //    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    //}
                    cmd.ExecuteNonQuery();
                }
            }
        }
          public void UpdateCampaignStatu(WhatsappConfigDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_WhatsappCampaniasEstado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoWhatsappCampania;
                    cmd.Parameters.Add(new SqlParameter("@EstadoWhatsappCampania", System.Data.SqlDbType.Bit)).Value = item.EstadoWhatsappCampania;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DestroyCampaign(WhatsappConfigDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminar_WhatsappCampanias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoWhatsappCampania;
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //**********detail campaign**********

        //list
        public List<WhatsappConfigDTO> ListCampaignDetail(WhatsappConfigDTO oFiltro)
        {
            List<WhatsappConfigDTO> lista = new List<WhatsappConfigDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_WhatsappCampaniasDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = oFiltro.CodigoWhatsappCampania;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new WhatsappConfigDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoWhatsappCampania = oIDataReader[oIDataReader.GetOrdinal("CodigoWhatsappCampania")].ToString(),
                                    CodigoWhatsappCampaniaDetalle = oIDataReader[oIDataReader.GetOrdinal("CodigoWhatsappCampaniaDetalle")].ToString(),

                                    Destinatario = oIDataReader[oIDataReader.GetOrdinal("Destinatario")].ToString(),
                                    Phone = oIDataReader[oIDataReader.GetOrdinal("Phone")].ToString(),
                                    EstadoWhatsappCampania = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoWhatsappCampania")].ToString()),
                                    DescFechaCreacion = oFiltro.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),


                                });
                            }
                        }

                    }
                }
            }


            return lista;
        }


        //register
        public void RegisterCampaignDetail(WhatsappConfigDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrar_WhatsappCampaniasDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoWhatsappCampania;
                    cmd.Parameters.Add(new SqlParameter("@CodigoWhatsappCampaniaDetalle", System.Data.SqlDbType.VarChar)).Value = item.CodigoWhatsappCampaniaDetalle;
                    cmd.Parameters.Add(new SqlParameter("@Destinatario", System.Data.SqlDbType.VarChar)).Value = item.Destinatario;
                    cmd.Parameters.Add(new SqlParameter("@Phone", System.Data.SqlDbType.VarChar)).Value = item.Phone;
                    cmd.Parameters.Add(new SqlParameter("@EstadoWhatsappCampania", System.Data.SqlDbType.VarChar)).Value = item.EstadoWhatsappCampania;
                    //if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    //{
                    //    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    //}
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //*********************************************** END CAMPAÑA*************************

    }
}
