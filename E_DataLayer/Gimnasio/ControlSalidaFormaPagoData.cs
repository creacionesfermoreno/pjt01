using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class ControlSalidaFormaPagoData
    {
        public List<ControlSalidaFormaPagoDTO> uspListarControlSalidaFormaPago(ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO)
        {
            List<ControlSalidaFormaPagoDTO> lista = new List<ControlSalidaFormaPagoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarControlSalidaFormaPago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oControlSalidaFormaPagoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIngreso", System.Data.SqlDbType.Int)).Value = oControlSalidaFormaPagoDTO.CodigoIngreso;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ControlSalidaFormaPagoDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    TipoMoneda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")]),
                                    Monto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Monto")]),
                                    TipoCambio = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TipoCambio")]),
                                    FormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("FormaPago")]),
                                    SubFormaPago = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("SubFormaPago")]),
                                    NroBoucher = oIDataReader[oIDataReader.GetOrdinal("NroBoucher")].ToString(),
                                    UrlBoucher = oIDataReader[oIDataReader.GetOrdinal("UrlBoucher")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public void Registrar(ControlSalidaFormaPagoDTO item)
        {
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarControlSalidaFormaPago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIngreso", System.Data.SqlDbType.Int)).Value = item.CodigoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@TipoMoneda", System.Data.SqlDbType.VarChar, 200)).Value = item.TipoMoneda;
                    cmd.Parameters.Add(new SqlParameter("@Monto", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = item.Monto;


                    cmd.Parameters.Add(new SqlParameter("@TipoCambio", System.Data.SqlDbType.Decimal)).Value = item.TipoCambio;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = item.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@SubFormaPago", System.Data.SqlDbType.Int)).Value = item.SubFormaPago;
                    cmd.Parameters.Add(new SqlParameter("@NroBoucher", System.Data.SqlDbType.VarChar, 100)).Value = item.NroBoucher;
                    cmd.Parameters.Add(new SqlParameter("@UrlBoucher", System.Data.SqlDbType.VarChar, 100)).Value = item.UrlBoucher;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //***************************************************** FOR API ****************************
        public void RegistrarAPP(ControlSalidaFormaPagoDTO item)
        {
            var sscsb = new SqlConnectionStringBuilder(Helper.Conexion());
            sscsb.ConnectTimeout = 180;
            using (var conn = new SqlConnection(sscsb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarControlSalidaFormaPagoApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar)).Value = item.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = 0;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIngreso", System.Data.SqlDbType.Int)).Value = item.CodigoIngreso;
                    cmd.Parameters.Add(new SqlParameter("@Monto", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = item.Monto;
                    cmd.Parameters.Add(new SqlParameter("@FormaPago", System.Data.SqlDbType.Int)).Value = item.FormaPago;
                    cmd.Parameters.Add(new SqlParameter("@NroBoucher", System.Data.SqlDbType.VarChar, 100)).Value = item.NroBoucher;
                    cmd.Parameters.Add(new SqlParameter("@UrlBoucher", System.Data.SqlDbType.VarChar, 100)).Value = item.UrlBoucher;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
