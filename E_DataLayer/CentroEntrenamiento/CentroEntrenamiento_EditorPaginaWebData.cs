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
    public class CentroEntrenamiento_EditorPaginaWebData
    {

        public CentroEntrenamiento_EditorPaginaWebDTO CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            CentroEntrenamiento_EditorPaginaWebDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_EditorPaginaWebDTO();
                                itemDTO.CodigoPagina = oIDataReader[oIDataReader.GetOrdinal("CodigoPagina")].ToString();
                                itemDTO.logoPagina = oIDataReader[oIDataReader.GetOrdinal("logoPagina")].ToString();
                                itemDTO.LogoCorporativo = oIDataReader[oIDataReader.GetOrdinal("LogoCorporativo")].ToString();
                                itemDTO.NombreComercial = oIDataReader[oIDataReader.GetOrdinal("NombreComercial")].ToString();
                                itemDTO.ColorPrincipalPagina = oIDataReader[oIDataReader.GetOrdinal("ColorPrincipalPagina")].ToString();
                                itemDTO.BannerReserva_Titulo       = oIDataReader[oIDataReader.GetOrdinal("BannerReserva_Titulo")].ToString();
                                itemDTO.BannerReserva_Descripcion  = oIDataReader[oIDataReader.GetOrdinal("BannerReserva_Descripcion")].ToString();
                                itemDTO.BannerReserva_Descripcion2 = oIDataReader[oIDataReader.GetOrdinal("BannerReserva_Descripcion2")].ToString();
                                itemDTO.BannerReserva_FondoImagen = oIDataReader[oIDataReader.GetOrdinal("BannerReserva_FondoImagen")].ToString();
                                itemDTO.BannerReserva_Estado =  Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("BannerReserva_Estado")]);
                                itemDTO.BannerCentro_Titulo      = oIDataReader[oIDataReader.GetOrdinal("BannerCentro_Titulo")].ToString();
                                itemDTO.BannerCentro_Descripcion = oIDataReader[oIDataReader.GetOrdinal("BannerCentro_Descripcion")].ToString();
                                itemDTO.BannerCentro_Beneficio1  = oIDataReader[oIDataReader.GetOrdinal("BannerCentro_Beneficio1")].ToString();
                                itemDTO.BannerCentro_Beneficio2  = oIDataReader[oIDataReader.GetOrdinal("BannerCentro_Beneficio2")].ToString();
                                itemDTO.BannerCentro_Beneficio3  = oIDataReader[oIDataReader.GetOrdinal("BannerCentro_Beneficio3")].ToString();
                                itemDTO.BannerCentro_FondoImagen = oIDataReader[oIDataReader.GetOrdinal("BannerCentro_FondoImagen")].ToString();
                                itemDTO.BannerCentro_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("BannerCentro_Estado")]);
                                itemDTO.SesionFormulario_Titulo = oIDataReader[oIDataReader.GetOrdinal("SesionFormulario_Titulo")].ToString();
                                itemDTO.SesionFormulario_Descripcion = oIDataReader[oIDataReader.GetOrdinal("SesionFormulario_Descripcion")].ToString();
                                itemDTO.SesionFormulario_Tituloformulario = oIDataReader[oIDataReader.GetOrdinal("SesionFormulario_Tituloformulario")].ToString();
                                itemDTO.SesionFormulario_Imagen = oIDataReader[oIDataReader.GetOrdinal("SesionFormulario_Imagen")].ToString();
                                itemDTO.SesionFormulario_CodigoPais = oIDataReader[oIDataReader.GetOrdinal("Pais")].ToString();
                                itemDTO.SesionFormulario_Whatsapp = oIDataReader[oIDataReader.GetOrdinal("SesionFormulario_Whatsapp")].ToString();
                                itemDTO.SesionFormulario_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SesionFormulario_Estado")]);
                                itemDTO.SesionTrainner_Titulo      = oIDataReader[oIDataReader.GetOrdinal("SesionTrainner_Titulo")].ToString();
                                itemDTO.SesionTrainner_Descripcion = oIDataReader[oIDataReader.GetOrdinal("SesionTrainner_Descripcion")].ToString();
                                itemDTO.SesionTrainner_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SesionTrainner_Estado")]);
                                itemDTO.SesionServicio_Titulo       = oIDataReader[oIDataReader.GetOrdinal("SesionServicio_Titulo")].ToString();
                                itemDTO.SesionServicio_Descripcion = oIDataReader[oIDataReader.GetOrdinal("SesionServicio_Descripcion")].ToString();
                                itemDTO.SesionServicio_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SesionServicio_Estado")]);
                                itemDTO.SesionPlan_Titulo        = oIDataReader[oIDataReader.GetOrdinal("SesionPlan_Titulo")].ToString();
                                itemDTO.SesionPlan_Descripcion = oIDataReader[oIDataReader.GetOrdinal("SesionPlan_Descripcion")].ToString();
                                itemDTO.SesionPlan_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SesionPlan_Estado")]);
                                itemDTO.SesionVideo_Titulo = oIDataReader[oIDataReader.GetOrdinal("SesionVideo_Titulo")].ToString();
                                itemDTO.SesionVideo_Descripcion    = oIDataReader[oIDataReader.GetOrdinal("SesionVideo_Descripcion")].ToString();
                                itemDTO.SesionVideo_Linkvideo = oIDataReader[oIDataReader.GetOrdinal("SesionVideo_Linkvideo")].ToString();
                                itemDTO.SesionVideo_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SesionVideo_Estado")]);
                                itemDTO.SesionFreepass_Titulo = oIDataReader[oIDataReader.GetOrdinal("SesionFreepass_Titulo")].ToString();
                                itemDTO.SesionFreepass_Descripcion = oIDataReader[oIDataReader.GetOrdinal("SesionFreepass_Descripcion")].ToString();
                                itemDTO.SesionFreepass_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SesionFreepass_Estado")]);
                                itemDTO.SesionInformacion_Direccion1   = oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_Direccion1")].ToString();
                                itemDTO.SesionInformacion_Direccion2   = oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_Direccion2")].ToString();
                                itemDTO.SesionInformacion_Contactanos1 = oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_Contactanos1")].ToString();
                                itemDTO.SesionInformacion_Contactanos2 = oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_Contactanos2")].ToString();
                                itemDTO.SesionInformacion_Horario1     = oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_Horario1")].ToString();
                                itemDTO.SesionInformacion_Horario2     = oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_Horario2")].ToString();
                                itemDTO.SesionInformacion_LatitudMapa  = oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_LatitudMapa")].ToString();
                                itemDTO.SesionInformacion_LongitudMapa = oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_LongitudMapa")].ToString();
                                itemDTO.SesionInformacion_Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("SesionInformacion_Estado")]);

                                itemDTO.ReservasNormativa = oIDataReader[oIDataReader.GetOrdinal("ReservasNormativa")].ToString();
                                itemDTO.ReservasNotas = oIDataReader[oIDataReader.GetOrdinal("ReservasNotas")].ToString();
                                itemDTO.ReservasMinutosCancelar = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("ReservasMinutosCancelar")]);
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public CentroEntrenamiento_EditorPaginaWebDTO CentroEntrenamiento_uspBuscarLogoCorporativo(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            CentroEntrenamiento_EditorPaginaWebDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarLogoCorporativo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@User", System.Data.SqlDbType.VarChar, 100)).Value = request.UsuarioCreacion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_EditorPaginaWebDTO();                                
                                itemDTO.logoPagina = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString();
                                itemDTO.NombreComercial = oIDataReader[oIDataReader.GetOrdinal("NombreComercial")].ToString();
                                itemDTO.SubDominio = oIDataReader[oIDataReader.GetOrdinal("SubDominio")].ToString();
                                itemDTO.CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]);
                                itemDTO.DesPlanEmpresa = oIDataReader[oIDataReader.GetOrdinal("DesPlan")].ToString();
                                itemDTO.EstadoEmpresa = oIDataReader[oIDataReader.GetOrdinal("DesEstadoGym")].ToString();
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }


        public string CentroEntrenamiento_uspActualizarEdicionPaginaWeb(CentroEntrenamiento_EditorPaginaWebDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarEdicionPaginaWeb", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPagina", System.Data.SqlDbType.VarChar, 200)).Value = item.CodigoPagina;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    //cmd.Parameters.Add(new SqlParameter("@logoPagina", System.Data.SqlDbType.VarChar, 200)).Value = item.logoPagina;
                    cmd.Parameters.Add(new SqlParameter("@ColorPrincipalPagina", System.Data.SqlDbType.VarChar, 100)).Value = item.ColorPrincipalPagina;

                    cmd.Parameters.Add(new SqlParameter("@BannerReserva_Titulo", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerReserva_Titulo;
                    cmd.Parameters.Add(new SqlParameter("@BannerReserva_Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerReserva_Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@BannerReserva_Descripcion2", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerReserva_Descripcion2;
                    //cmd.Parameters.Add(new SqlParameter("@BannerReserva_FondoImagen", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerReserva_FondoImagen;
                    cmd.Parameters.Add(new SqlParameter("@BannerReserva_Estado", System.Data.SqlDbType.Bit)).Value = item.BannerReserva_Estado;

                    cmd.Parameters.Add(new SqlParameter("@BannerCentro_Titulo", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerCentro_Titulo; 
                    cmd.Parameters.Add(new SqlParameter("@BannerCentro_Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerCentro_Descripcion; 
                    cmd.Parameters.Add(new SqlParameter("@BannerCentro_Beneficio1", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerCentro_Beneficio1; 
                    cmd.Parameters.Add(new SqlParameter("@BannerCentro_Beneficio2", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerCentro_Beneficio2; 
                    cmd.Parameters.Add(new SqlParameter("@BannerCentro_Beneficio3", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerCentro_Beneficio3; 
                    //cmd.Parameters.Add(new SqlParameter("@BannerCentro_FondoImagen", System.Data.SqlDbType.VarChar, 200)).Value = item.BannerCentro_FondoImagen;
                    cmd.Parameters.Add(new SqlParameter("@BannerCentro_Estado", System.Data.SqlDbType.Bit)).Value = item.BannerCentro_Estado;

                    cmd.Parameters.Add(new SqlParameter("@SesionFormulario_Titulo", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionFormulario_Titulo;        
                    cmd.Parameters.Add(new SqlParameter("@SesionFormulario_Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionFormulario_Descripcion; 
                    cmd.Parameters.Add(new SqlParameter("@SesionFormulario_Tituloformulario", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionFormulario_Tituloformulario; 
                    cmd.Parameters.Add(new SqlParameter("@SesionFormulario_Whatsapp", System.Data.SqlDbType.VarChar, 100)).Value = item.SesionFormulario_Whatsapp;
                    cmd.Parameters.Add(new SqlParameter("@SesionFormulario_Estado", System.Data.SqlDbType.Bit)).Value = item.SesionFormulario_Estado;
                    
                    cmd.Parameters.Add(new SqlParameter("@SesionTrainner_Titulo", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionTrainner_Titulo;    
                    cmd.Parameters.Add(new SqlParameter("@SesionTrainner_Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionTrainner_Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@SesionTrainner_Estado", System.Data.SqlDbType.Bit)).Value = item.SesionTrainner_Estado;

                    cmd.Parameters.Add(new SqlParameter("@SesionServicio_Titulo", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionServicio_Titulo;     
                    cmd.Parameters.Add(new SqlParameter("@SesionServicio_Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionServicio_Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@SesionServicio_Estado", System.Data.SqlDbType.Bit)).Value = item.SesionServicio_Estado;

                    cmd.Parameters.Add(new SqlParameter("@SesionPlan_Titulo", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionPlan_Titulo;     
                    cmd.Parameters.Add(new SqlParameter("@SesionPlan_Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionPlan_Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@SesionPlan_Estado", System.Data.SqlDbType.Bit)).Value = item.SesionPlan_Estado;

                    cmd.Parameters.Add(new SqlParameter("@SesionVideo_Titulo", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionVideo_Titulo;     
                    cmd.Parameters.Add(new SqlParameter("@SesionVideo_Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionVideo_Descripcion;     
                    cmd.Parameters.Add(new SqlParameter("@SesionVideo_Linkvideo", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionVideo_Linkvideo;
                    cmd.Parameters.Add(new SqlParameter("@SesionVideo_Estado", System.Data.SqlDbType.Bit)).Value = item.SesionVideo_Estado;

                    cmd.Parameters.Add(new SqlParameter("@SesionFreepass_Titulo", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionFreepass_Titulo;      
                    cmd.Parameters.Add(new SqlParameter("@SesionFreepass_Descripcion", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionFreepass_Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@SesionFreepass_Estado", System.Data.SqlDbType.Bit)).Value = item.SesionFreepass_Estado;

                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_Direccion1", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionInformacion_Direccion1;       
                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_Direccion2", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionInformacion_Direccion2;     
                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_Contactanos1", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionInformacion_Contactanos1;     
                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_Contactanos2", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionInformacion_Contactanos2;     
                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_Horario1", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionInformacion_Horario1;     
                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_Horario2", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionInformacion_Horario2;    
                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_LatitudMapa", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionInformacion_LatitudMapa;     
                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_LongitudMapa", System.Data.SqlDbType.VarChar, 200)).Value = item.SesionInformacion_LongitudMapa;
                    cmd.Parameters.Add(new SqlParameter("@SesionInformacion_Estado", System.Data.SqlDbType.Bit)).Value = item.SesionInformacion_Estado;

                    cmd.ExecuteNonQuery();

                }

            }
            return resultado;
        }

        public string CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina(CentroEntrenamiento_EditorPaginaWebDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    
                    cmd.Parameters.Add(new SqlParameter("@ColorPrincipalPagina", System.Data.SqlDbType.VarChar, 100)).Value = item.ColorPrincipalPagina;
                    cmd.Parameters.Add(new SqlParameter("@ReservasNormativa", System.Data.SqlDbType.VarChar, 200)).Value = item.ReservasNormativa;
                    cmd.Parameters.Add(new SqlParameter("@ReservasNotas", System.Data.SqlDbType.VarChar, 200)).Value = item.ReservasNotas;
                    cmd.Parameters.Add(new SqlParameter("@ReservasMinutosCancelar", System.Data.SqlDbType.Int)).Value = item.ReservasMinutosCancelar;

                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }



        public string CentroEntrenamiento_uspActualizarEdicionPaginaWeb_Foto(CentroEntrenamiento_EditorPaginaWebDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarEdicionPaginaWeb_Foto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPagina", System.Data.SqlDbType.VarChar, 200)).Value = item.CodigoPagina;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int)).Value = item.TipoFoto;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar,200)).Value = item.UrlImagen;
                    
                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }


    }
}
