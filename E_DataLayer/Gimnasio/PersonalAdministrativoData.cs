using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace E_DataLayer.Gimnasio
{

    public class PersonalAdministrativoData
    {
      
        public List<PersonalAdministrativoDTO> Listar(PersonalAdministrativoDTO oPersonalAdministrativo)
        {
            List<PersonalAdministrativoDTO> lista = new List<PersonalAdministrativoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPersonalAdministrativo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oPersonalAdministrativo.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oPersonalAdministrativo.CodigoSede;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                PersonalAdministrativoDTO personalAdmin = new PersonalAdministrativoDTO()
                                {
                                    CodigoPersonalAdministrativo = oIDataReader[oIDataReader.GetOrdinal("CodigoPersonalAdministrativo")].ToString(),
                                    NumeroDocumento = oIDataReader[oIDataReader.GetOrdinal("NumeroDocumento")].ToString(),
                                    TipoNumeroDocumentoTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoNumeroDocumentoTm")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    ApellidoPaterno = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()),
                                    Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    CodigoCargo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCargo")]),
                                    EstadoCivilTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoCivilTm")]),
                                    SexoTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("SexoTm")]),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    DescripcionCargo = oIDataReader[oIDataReader.GetOrdinal("DescripcionCargo")].ToString(),
                                    SueldoBase = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SueldoBase")]),
                                    MontoDescuento = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalDescuento")]),
                                    DesVigencia = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Vigencia")]) ? "Activo" : "Inactivo"
                                };
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Vigencia")))
                                {
                                    personalAdmin.Vigencia = oIDataReader[oIDataReader.GetOrdinal("Vigencia")].ToString();
                                }
                                
                                lista.Add(personalAdmin);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        public List<PersonalAdministrativoDTO> ListarPorFiltros(PersonalAdministrativoDTO oPersonalAdministrativo)
        {
            List<PersonalAdministrativoDTO> lista = new List<PersonalAdministrativoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPersonalAdministrativoPorFiltros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oPersonalAdministrativo.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oPersonalAdministrativo.CodigoSede;
                    
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAdministrativo.NumeroDocumento;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 500)).Value = oPersonalAdministrativo.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 500)).Value = oPersonalAdministrativo.ApellidoPaterno;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                PersonalAdministrativoDTO personalAdmin = new PersonalAdministrativoDTO()
                                {
                                    CodigoPersonalAdministrativo = oIDataReader[oIDataReader.GetOrdinal("CodigoPersonalAdministrativo")].ToString(),
                                    NumeroDocumento = oIDataReader[oIDataReader.GetOrdinal("NumeroDocumento")].ToString(),
                                    TipoNumeroDocumentoTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoNumeroDocumentoTm")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    ApellidoPaterno = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()),
                                    Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    CodigoCargo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCargo")]),
                                    EstadoCivilTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoCivilTm")]),
                                    SexoTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("SexoTm")]),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString(),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    DescripcionCargo = oIDataReader[oIDataReader.GetOrdinal("DescripcionCargo")].ToString(),

                                    SueldoBase = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SueldoBase")]),
                                    MontoDescuento = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalDescuento")]),
                                    DesVigencia = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Vigencia")]) ? "Activo" : "Inactivo"
                                };

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Vigencia")))
                                {
                                    personalAdmin.Vigencia = oIDataReader[oIDataReader.GetOrdinal("Vigencia")].ToString();
                                }
                                lista.Add(personalAdmin);
                            }
                        }
                    }
                }
            }

            return lista;
        }

        public PersonalAdministrativoDTO uspBuscarPersonalAdministrativoGeneralPorNumeroDocumento(PersonalAdministrativoDTO oPersonalAdministrativo)
        {
            PersonalAdministrativoDTO personalAdmin = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarPersonalAdministrativoGeneralPorNumeroDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                  
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAdministrativo.NumeroDocumento;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                personalAdmin = new PersonalAdministrativoDTO()
                                {
                                    CodigoPersonalAdministrativo = oIDataReader[oIDataReader.GetOrdinal("CodigoPersonalAdministrativo")].ToString(),
                                    NumeroDocumento = oIDataReader[oIDataReader.GetOrdinal("NumeroDocumento")].ToString(),
                                    TipoNumeroDocumentoTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoNumeroDocumentoTm")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    ApellidoPaterno = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()),
                                    Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    EstadoCivilTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoCivilTm")]),
                                    SexoTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("SexoTm")]),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                };
                            }
                        }
                    }
                }
            }

            return personalAdmin;
        }

        public PersonalAdministrativoDTO uspBuscarPersonalAsistenciaConfiguracionPorDNI(PersonalAdministrativoDTO oPersonalAdministrativo)
        {
            PersonalAdministrativoDTO personalAdmin = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarPersonalAdministrativoAsistenciaConfiguracionPorNumeroDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oPersonalAdministrativo.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oPersonalAdministrativo.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAdministrativo.NumeroDocumento;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                   {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                personalAdmin = new PersonalAdministrativoDTO()
                                {
                                    CodigoPersonalAdministrativo = oIDataReader[oIDataReader.GetOrdinal("CodigoPersonalAdministrativo")].ToString(),
                                    NumeroDocumento = oIDataReader[oIDataReader.GetOrdinal("NumeroDocumento")].ToString(),
                                    TipoNumeroDocumentoTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoNumeroDocumentoTm")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    ApellidoPaterno = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimiento")].ToString()),
                                    Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    CodigoCargo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCargo")]),
                                    EstadoCivilTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoCivilTm")]),
                                    SexoTm = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("SexoTm")]),
                                    UrlImagen = oIDataReader[oIDataReader.GetOrdinal("UrlImagen")].ToString(),
                                    DescripcionCargo = oIDataReader[oIDataReader.GetOrdinal("DescripcionCargo")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Direccion = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),

                                    SueldoBase = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Sueldo")]),
                                    MontoDescuento = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DescuentoXMinuto")]),
                                    Vigencia = oIDataReader[oIDataReader.GetOrdinal("Vigencia")].ToString(),
                                    AsistenciaConfiguracion = new PersonalAsistenciaConfiguracionDTO() { }
                                };
                                
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoPersonalAsistenciaConfiguracion")))
                                {
                                    personalAdmin.AsistenciaConfiguracion.CodigoPersonalAsistenciaConfiguracion = Convert.ToString(oIDataReader[oIDataReader.GetOrdinal("CodigoPersonalAsistenciaConfiguracion")].ToString());
                                }

                                personalAdmin.AsistenciaConfiguracion.CodigoCargo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCargo")]);
                             
                                    personalAdmin.AsistenciaConfiguracion.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Lunes_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Lunes_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Lunes_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Lunes_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Lunes_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Lunes_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Martes_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Martes_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Martes_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Martes_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Martes_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Martes_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Miercoles_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Miercoles_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Miercoles_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Miercoles_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Miercoles_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Miercoles_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Jueves_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Jueves_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Jueves_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Jueves_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Jueves_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Jueves_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Viernes_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Viernes_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Viernes_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Viernes_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Viernes_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Viernes_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Sabado_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Sabado_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Sabado_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Sabado_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Sabado_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Sabado_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Domingo_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Domingo_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Domingo_Turno1")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Domingo_Turno1")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Domingo_Turno1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Domingo_Turno1")].ToString());

                                //HORARIO 2
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Lunes_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Lunes_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Lunes_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Lunes_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Lunes_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Lunes_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Martes_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Martes_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Martes_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Martes_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Martes_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Martes_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Miercoles_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Miercoles_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Miercoles_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Miercoles_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Miercoles_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Miercoles_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Jueves_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Jueves_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Jueves_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Jueves_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Jueves_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Jueves_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Viernes_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Viernes_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Viernes_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Viernes_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Viernes_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Viernes_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Sabado_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Sabado_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Sabado_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Sabado_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Sabado_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Sabado_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraIngreso_Domingo_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraIngreso_Domingo_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraIngreso_Domingo_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraSalida_Domingo_Turno2")))
                                    personalAdmin.AsistenciaConfiguracion.HoraSalida_Domingo_Turno2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraSalida_Domingo_Turno2")].ToString());

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Sueldo")))
                                    personalAdmin.AsistenciaConfiguracion.Sueldo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Sueldo")]);

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("MinutosTolerancia")))
                                    personalAdmin.AsistenciaConfiguracion.MinutosTolerancia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MinutosTolerancia")]);

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DescuentoXMinuto")))
                                    personalAdmin.AsistenciaConfiguracion.DescuentoXMinuto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DescuentoXMinuto")]);

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("MinutosRefrigerio")))
                                    personalAdmin.AsistenciaConfiguracion.MinutosRefrigerio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("MinutosRefrigerio")]);
                                
                            }
                        }
                    }
                }
            }

            return personalAdmin;
        }

        public string Registrar(PersonalAdministrativoDTO item)
        {
            string Result = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPersonalAdministrativo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    SqlParameter parametro = new SqlParameter("@CodigoPersonalAdministrativo",System.Data.SqlDbType.VarChar,50);
                    parametro.Value = "";
                    parametro.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(parametro);

                    cmd.Parameters.Add(new SqlParameter("@TipoNumeroDocumentoTm", System.Data.SqlDbType.Int)).Value = item.TipoNumeroDocumentoTm;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = item.NumeroDocumento;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 200)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar, 200)).Value = item.ApellidoPaterno;
                    //cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar, 200)).Value = item.ApellidoMaterno;
                    cmd.Parameters.Add(new SqlParameter("@SexoTm", System.Data.SqlDbType.Int)).Value = item.SexoTm;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;
                    cmd.Parameters.Add(new SqlParameter("@EstadoCivilTm", System.Data.SqlDbType.Int)).Value = item.EstadoCivilTm;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlImagen ?? string.Empty;                   
                    cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar, 200)).Value = item.Email ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 50)).Value = item.Celular ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 50)).Value = item.Direccion ?? string.Empty;

                    cmd.ExecuteNonQuery();
                    Result = cmd.Parameters["@CodigoPersonalAdministrativo"].Value.ToString();
                }
            }
            return Result;
        }

      
        public int Actualizar(PersonalAdministrativoDTO item)
        {
            int Result = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPersonalAdministrativo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAdministrativo", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAdministrativo;
                    cmd.Parameters.Add(new SqlParameter("@TipoNumeroDocumentoTm", System.Data.SqlDbType.Int)).Value = item.TipoNumeroDocumentoTm;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = item.NumeroDocumento;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 200)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar, 200)).Value = item.ApellidoPaterno;
                    //cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar, 200)).Value = item.ApellidoMaterno;
                    cmd.Parameters.Add(new SqlParameter("@SexoTm", System.Data.SqlDbType.Int)).Value = item.SexoTm;
                    if (item.FechaNacimiento.Year < 1910)
                        cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;

                    cmd.Parameters.Add(new SqlParameter("@EstadoCivilTm", System.Data.SqlDbType.Int)).Value = item.EstadoCivilTm;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlImagen ?? string.Empty;

                    if (item.Email == null)
                        cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar, 200)).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar, 200)).Value = item.Email;

                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 50)).Value = item.Celular??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 500)).Value = item.Direccion??string.Empty;
                    
                    Result = cmd.ExecuteNonQuery();
                }
            }

            return Result;
        }

        public int CesarPersonalAdministrativo(PersonalAdministrativoDTO item)
        {
            int Result = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarCesarPersonalAdministrativo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAdministrativo", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAdministrativo;

                    //cmd.Parameters.Add(new SqlParameter("@MotivoCeseTm", System.Data.SqlDbType.Int)).Value = item.MotivoCeseTm;
                    //cmd.Parameters.Add(new SqlParameter("@FechaCese", System.Data.SqlDbType.DateTime)).Value = item.FechaCese;
                    //cmd.Parameters.Add(new SqlParameter("@ObservacionCese", System.Data.SqlDbType.VarChar, 500)).Value = item.ObservacionCese??string.Empty;                    
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    Result = cmd.ExecuteNonQuery();
                }
            }

            return Result;
        }

        public int ActivarPersonalAdministrativo(PersonalAdministrativoDTO item)
        {
            int Result = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarActivarPersonalAdministrativo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAdministrativo", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAdministrativo;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    Result = cmd.ExecuteNonQuery();
                }
            }
            return Result;
        }

        public int ActualizarFoto(PersonalAdministrativoDTO item)
        {
            int Result = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPersonalAdministrativoFoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAdministrativo", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAdministrativo;
                    cmd.Parameters.Add(new SqlParameter("@UrlImagen", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlImagen ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion??item.UsuarioEdicion;
                    Result = cmd.ExecuteNonQuery();
                }
            }

            return Result;
        }

        public string ValidarPersonalAdministrativoPorNumeroDocumento(PersonalAdministrativoDTO item)
        {
            int Result = 0;
            string CodigoPersonal = string.Empty;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarPersonalAdministrativoGeneral", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = item.NumeroDocumento;
                    cmd.Parameters.AddWithValue("@CodigoPersonal", "").Direction = System.Data.ParameterDirection.Output;
                    Result = cmd.ExecuteNonQuery();
                    CodigoPersonal = (cmd.Parameters["@CodigoPersonal"].Value.ToString());
                }
            }

            return CodigoPersonal;
        }
    }
}
