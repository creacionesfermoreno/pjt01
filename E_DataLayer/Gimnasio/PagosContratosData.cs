
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class PagosContratoData
    {



        public List<PagosContratoDTO> uspListarPagoMembresia_Anulados(PagosContratoDTO oitem)
        {
            List<PagosContratoDTO> lista = new List<PagosContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPagoMembresia_Anulados", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oitem.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oitem.FechaFin;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PagosContratoDTO()
                                {
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    DesPlan = oIDataReader[oIDataReader.GetOrdinal("DesPlan")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    CodigoPagoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPagoMembresia")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    Monto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Monto")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    FechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]),
                                    DesFormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 1 ? "Efectivo" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 2 ? "T. Debito" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 3 ? "T. Credito" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 4 ? "Deposito" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 5 ? "web" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 6 ? "Cuponera" : "Ninguno"))))),
                                    nroTarjeta = oIDataReader[oIDataReader.GetOrdinal("nroTarjeta")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DesEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : "Anulado",
                                    ColorEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "rgb(0 0 0 1)" : "rgb(255 2 0)",
                                    desFechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]).ToString("dd/MM/yyy"),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Comentario = oIDataReader[oIDataReader.GetOrdinal("Comentario")].ToString(),
                                    UsuarioEdicion = oIDataReader[oIDataReader.GetOrdinal("UsuarioEdicion")].ToString(),
                                    FechaEdicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEdicion")])

                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<PagosContratoDTO> Listar(PagosContratoDTO oitem)
        {
            List<PagosContratoDTO> lista = new List<PagosContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPagoMembresia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = oitem.CodigoMembresia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PagosContratoDTO()
                                {
                                    CodigoPagoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPagoMembresia")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    Monto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Monto")]),
                                    NroComprobante = oIDataReader[oIDataReader.GetOrdinal("NroComprobante")].ToString(),
                                    FechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]),
                                    DesFormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 1 ? "Efectivo" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 2 ? "T. Debito" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 3 ? "T. Credito" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 4 ? "Deposito" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 5 ? "web" : (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]) == 6 ? "Cuponera" : "Ninguno"))))),
                                    nroTarjeta = oIDataReader[oIDataReader.GetOrdinal("nroTarjeta")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    DesEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "Activo" : "Anulado",
                                    ColorEstado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 1 ? "rgb(0 0 0 1)" : "rgb(255 2 0)",
                                    desFechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]).ToString("dd/MM/yyy"),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<PagosContratoDTO> ListarPagosFormaPago(int CodigoUnidadNegocio, int CodigoSede, int CodigoSalida)
        {
            List<PagosContratoDTO> lista = new List<PagosContratoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPagosFormaPago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalida", System.Data.SqlDbType.Int)).Value = CodigoSalida;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PagosContratoDTO()
                                {
                                    CodigoControlSalidaFormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    FechaNewPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("Fechaventa")]),
                                    desFechaPago = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("Fechaventa")]).ToString("dd/MM/yyyy"), // hh:mm:ss tt
                                    Monto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Monto")]),
                                    DesFormaPago = oIDataReader[oIDataReader.GetOrdinal("desFormaPago")].ToString(),
                                    desTarjeta = oIDataReader[oIDataReader.GetOrdinal("desTarjeta")].ToString(),
                                    nroBoucher = oIDataReader[oIDataReader.GetOrdinal("NroBoucher")].ToString(),
                                    desComprobante = oIDataReader[oIDataReader.GetOrdinal("desComprobante")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("Vendedor")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public int Registrar(PagosContratoDTO item)
        {
            int? campoRetorno = 0;
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPagoMembresia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.AddWithValue("@CodigoPagoMembresia", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@Monto", System.Data.SqlDbType.Decimal)).Value = item.Monto;

                    cmd.Parameters.Add(new SqlParameter("@NroComprobante", System.Data.SqlDbType.VarChar, 50)).Value = item.NroComprobante;
                    cmd.Parameters.Add(new SqlParameter("@FechaPago", System.Data.SqlDbType.DateTime)).Value = item.FechaPago;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = item.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@nroTarjeta", System.Data.SqlDbType.VarChar, 100)).Value = item.nroTarjeta;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoPagoMembresia"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }


        //*********************************************** API *******************************************
        public int RegistrarAPP(PagosContratoDTO item)
        {
            int? campoRetorno = 0;
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPagoMembresiaApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoPagoMembresia", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar)).Value = item.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@Monto", System.Data.SqlDbType.Decimal)).Value = item.Monto;
                    cmd.Parameters.Add(new SqlParameter("@NroComprobante", System.Data.SqlDbType.VarChar, 50)).Value = item.NroComprobante;
                    cmd.Parameters.Add(new SqlParameter("@FechaPago", System.Data.SqlDbType.DateTime)).Value = item.FechaPago;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = item.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@nroTarjeta", System.Data.SqlDbType.VarChar, 100)).Value = item.nroTarjeta;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoPagoMembresia"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

        //*********************************************** API *******************************************

        public void ActualizarEstado(PagosContratoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPagoMembresiaEstado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPagoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoPagoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(PagosContratoDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarPagosMenbresias", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPagoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoPagoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@Comentario", System.Data.SqlDbType.VarChar, 100)).Value = item.Comentario;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
