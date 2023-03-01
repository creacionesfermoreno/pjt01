using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Common;
using E_DataModel.Gimnasio;

namespace E_DataLayer.Gimnasio
{
    public class EmailCampaingData
    {

        //list campaing emil - pagination

        public List<EmailCampaingDTO> ListPagination(EmailCampaingDTO item, Paging paging, out uint NumeroRegistros)
        {
            List<EmailCampaingDTO> lista = new List<EmailCampaingDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_CorreoCampanias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;


                    SqlParameter TotalRows = cmd.Parameters.Add("@NumeroRegistros", SqlDbType.Int);
                    TotalRows.Direction = ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EmailCampaingDTO()
                                {
                                    CodigoCorreoCampania = oIDataReader[oIDataReader.GetOrdinal("CodigoCorreoCampania")].ToString(),
                                    NombreCorreoCampania = oIDataReader[oIDataReader.GetOrdinal("NombreCorreoCampania")].ToString(),
                                    UrlDestinatarios = oIDataReader[oIDataReader.GetOrdinal("UrlDestinatarios")].ToString(),
                                    Content_html = oIDataReader[oIDataReader.GetOrdinal("Content_html")].ToString(),
                                    SendCorreo = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SendCorreo")]),
                                    EstadoCorreoCampania = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoCorreoCampania")]),
                                    DescFechaCreacion = item.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                });
                            }
                        }

                    }
                    NumeroRegistros = Convert.ToUInt32(TotalRows.Value);
                }
            }
            return lista;
        }


        //search campaing email
        public EmailCampaingDTO Search(EmailCampaingDTO item)
        {
            EmailCampaingDTO itemDTO = new EmailCampaingDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscar_CorreoCampanias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoCorreoCampania;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new EmailCampaingDTO()
                                {
                                    CodigoCorreoCampania = oIDataReader[oIDataReader.GetOrdinal("CodigoCorreoCampania")].ToString(),
                                    NombreCorreoCampania = oIDataReader[oIDataReader.GetOrdinal("NombreCorreoCampania")].ToString(),
                                    UrlDestinatarios = oIDataReader[oIDataReader.GetOrdinal("UrlDestinatarios")].ToString(),
                                    Content_html = oIDataReader[oIDataReader.GetOrdinal("Content_html")].ToString(),
                                    SendCorreo = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SendCorreo")]),
                                    EstadoCorreoCampania = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoCorreoCampania")]),
                                    DescFechaCreacion = item.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }


        //register campaing email
        public string Register(EmailCampaingDTO item)
        {
            string id = "";
            try
            {
                using (var conn = new SqlConnection(Helper.Conexion()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("uspRegistrar_CorreoCampanias", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                        cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                        cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar,100)).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@NombreCorreoCampania", System.Data.SqlDbType.VarChar,100)).Value = item.NombreCorreoCampania;
                        cmd.Parameters.Add(new SqlParameter("@UrlDestinatarios", System.Data.SqlDbType.VarChar,200)).Value = item.UrlDestinatarios;
                        cmd.Parameters.Add(new SqlParameter("@SendCorreo", System.Data.SqlDbType.Bit)).Value = item.SendCorreo;
                        cmd.Parameters.Add(new SqlParameter("@Content_html", System.Data.SqlDbType.VarChar,1000)).Value = item.Content_html;
                        if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                        {
                            cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        id = cmd.Parameters["@CodigoCorreoCampania"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                id = "";
            }
            return id;
        }


        //update campaing email
        public void Update(EmailCampaingDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_CorreoCampanias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoCorreoCampania;
                    cmd.Parameters.Add(new SqlParameter("@NombreCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.NombreCorreoCampania;
                    cmd.Parameters.Add(new SqlParameter("@UrlDestinatarios", System.Data.SqlDbType.VarChar)).Value = item.UrlDestinatarios;
                    cmd.Parameters.Add(new SqlParameter("@Content_html", System.Data.SqlDbType.VarChar)).Value = item.Content_html;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }


        //update send campaing email
        public void UpdateSend(EmailCampaingDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizar_CorreoCampaniasEstadoSendCorreo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoCorreoCampania;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        //delete campaing email
        public void Destroy(EmailCampaingDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminar_CorreoCampanias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoCorreoCampania;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }




        //********************************************************* FILES ******************************************************//

        public int RegisterFile(EmailCampaingDTO item)
        {
            int id = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrar_CorreoCampaniasArchivosAdjuntos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampaniaArchivosAdjuntos", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoCorreoCampania;
                    cmd.Parameters.Add(new SqlParameter("@UrlArchivosAdjunto", System.Data.SqlDbType.VarChar)).Value = item.UrlArchivosAdjunto;
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt16(cmd.Parameters["@CodigoCorreoCampaniaArchivosAdjuntos"].Value);
                }
                conn.Close();
            }
            return id;
        }

        public List<EmailCampaingDTO> ListFile(EmailCampaingDTO item)
        {
            List<EmailCampaingDTO> lista = new List<EmailCampaingDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_CorreoCampaniasArchivosAdjuntos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoCorreoCampania;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EmailCampaingDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoCorreoCampaniaArchivosAdjuntos = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCorreoCampaniaArchivosAdjuntos")]),
                                    CodigoCorreoCampania = oIDataReader[oIDataReader.GetOrdinal("CodigoCorreoCampania")].ToString(),
                                    UrlArchivosAdjunto = oIDataReader[oIDataReader.GetOrdinal("UrlArchivosAdjunto")].ToString(),
                                });
                            }
                        }

                    }
                }
                conn.Close();
            }
            return lista;
        }


        public void DestroyFile(EmailCampaingDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminar_CorreoCampaniasArchivosAdjuntos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoCorreoCampania;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampaniaArchivosAdjuntos", System.Data.SqlDbType.Int)).Value = item.CodigoCorreoCampaniaArchivosAdjuntos;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        //********************************************************* FILES ******************************************************//




        //********************************************************* DETAIL CAMPAING ******************************************************//

        public string RegisterDetail(EmailCampaingDTO item)
        {
            string id = "";
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrar_CorreoCampaniasDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampaniaDetalle", System.Data.SqlDbType.VarChar,100)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoCorreoCampania;
                    cmd.Parameters.Add(new SqlParameter("@Destinatario", System.Data.SqlDbType.VarChar,200)).Value = item.Destinatario;
                    cmd.Parameters.Add(new SqlParameter("@EstadoCorreoCampania", System.Data.SqlDbType.Bit)).Value = item.EstadoCorreoCampania;
                    cmd.ExecuteNonQuery();
                    id = Convert.ToString(cmd.Parameters["@CodigoCorreoCampaniaDetalle"].Value);
                }
                conn.Close();
            }
            return id;
        }


        public List<EmailCampaingDTO> ListPaginationDetail(EmailCampaingDTO item, Paging paging, out uint TotalRecord)
        {
            List<EmailCampaingDTO> lista = new List<EmailCampaingDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_CorreoCampaniasDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCorreoCampania", System.Data.SqlDbType.VarChar)).Value = item.CodigoCorreoCampania;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                  

                    SqlParameter TotalRows = cmd.Parameters.Add("@NumeroRegistros", SqlDbType.Int);
                    TotalRows.Direction = ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EmailCampaingDTO()
                                {
                                    CodigoCorreoCampania = oIDataReader[oIDataReader.GetOrdinal("CodigoCorreoCampania")].ToString(),
                                    CodigoCorreoCampaniaDetalle = oIDataReader[oIDataReader.GetOrdinal("CodigoCorreoCampaniaDetalle")].ToString(),
                                    Destinatario = oIDataReader[oIDataReader.GetOrdinal("Destinatario")].ToString(),
                                    // SendCorreo = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SendCorreo")]),
                                    EstadoCorreoCampania = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("EstadoCorreoCampania")]),
                                    DescFechaCreacion = item.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])),
                                   // UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                });
                            }
                        }

                    }
                    TotalRecord = Convert.ToUInt32(TotalRows.Value);
                }
            }
            return lista;
        }



        //********************************************************* DETAIL CAMPAING ******************************************************//




    }
}
