
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class ProfesionalFitnessData
    {
   
        public List<ProfesionalFitnessDTO> Listar(ProfesionalFitnessDTO oProfesionalFitnessDTO, Paging paging, ref uint recordCount)
        {
            List<ProfesionalFitnessDTO> lista = new List<ProfesionalFitnessDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarProfesionalFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProfesionalFitnessDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProfesionalFitnessDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = oProfesionalFitnessDTO.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 50)).Value = oProfesionalFitnessDTO.DNI??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", System.Data.SqlDbType.VarChar, 500)).Value = oProfesionalFitnessDTO.NombreCompleto??string.Empty;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                             
                                var itemDTO = new ProfesionalFitnessDTO()
                                {
                                    CodigoProfesional = reader[reader.GetOrdinal("CodigoProfesional")].ToString(),
                                    CodigoTipoProfesional = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTipoProfesional")]),
                                    Nombres = reader[reader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = reader[reader.GetOrdinal("Apellidos")].ToString(),
                                    TipoDocumento = Convert.ToInt32(reader[reader.GetOrdinal("TipoDocumento")]),
                                    DNI = reader[reader.GetOrdinal("DNI")].ToString(),
                                    Telefono = reader[reader.GetOrdinal("Telefono")].ToString(),
                                    Celular = reader[reader.GetOrdinal("Celular")].ToString(),
                                    Correo = reader[reader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]),
                                    ImagenUrl = reader[reader.GetOrdinal("ImagenUrl")].ToString(),
                                    Genero = reader[reader.GetOrdinal("Genero")].ToString(),
                                    EstadoCivil = Convert.ToInt32(reader[reader.GetOrdinal("EstadoCivil")]),
                                    Facebook = reader[reader.GetOrdinal("Facebook")].ToString(),
                                    Ubicaciones = reader[reader.GetOrdinal("Ubigeo")].ToString(),
                                    Direccion = reader[reader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = reader[reader.GetOrdinal("Distrito")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]),
                                    CostoPorHora = Convert.ToDecimal(reader[reader.GetOrdinal("CostoPorHora")]),
                                    DstoPorMinuto = Convert.ToDecimal(reader[reader.GetOrdinal("DstoPorMinuto")])
                                };

                                itemDTO.NombreCompleto = string.Format("{0} {1}", itemDTO.Nombres, itemDTO.Apellidos);
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }
        
        public ProfesionalFitnessDTO uspBuscarProfesionalFitnessPorCodigo(ProfesionalFitnessDTO oProfesionalFitness)
        {
            ProfesionalFitnessDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarProfesionalFitnessPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar,100)).Value = oProfesionalFitness.CodigoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar,100)).Value = oProfesionalFitness.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar,100)).Value = oProfesionalFitness.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = oProfesionalFitness.DNI;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new ProfesionalFitnessDTO()
                                {
                                    CodigoProfesional = reader[reader.GetOrdinal("CodigoProfesional")].ToString(),
                                    CodigoTipoProfesional = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTipoProfesional")]),
                                    Nombres = reader[reader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = reader[reader.GetOrdinal("Apellidos")].ToString(),
                                    TipoDocumento = Convert.ToInt32(reader[reader.GetOrdinal("TipoDocumento")]),
                                    DNI = reader[reader.GetOrdinal("DNI")].ToString(),
                                    Telefono = reader[reader.GetOrdinal("Telefono")].ToString(),
                                    Celular = reader[reader.GetOrdinal("Celular")].ToString(),
                                    Correo = reader[reader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]),
                                    ImagenUrl = reader[reader.GetOrdinal("ImagenUrl")].ToString(),
                                    Genero = reader[reader.GetOrdinal("Genero")].ToString(),
                                    EstadoCivil = Convert.ToInt32(reader[reader.GetOrdinal("EstadoCivil")]),
                                    Facebook = reader[reader.GetOrdinal("Facebook")].ToString(),
                                    Ubicaciones = reader[reader.GetOrdinal("Ubigeo")].ToString(),
                                    Direccion = reader[reader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = reader[reader.GetOrdinal("Distrito")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")])                             
                                };
                            }
                        }
                    }
                }
                
            }
            
            return itemDTO;
        }
     
        public ProfesionalFitnessDTO uspBuscarProfesionalFitnessPorDNI(ProfesionalFitnessDTO oProfesionalFitness)
        {
            ProfesionalFitnessDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarProfesionalFitnessPorDNI", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProfesionalFitness.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProfesionalFitness.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = oProfesionalFitness.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 50)).Value = oProfesionalFitness.DNI;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {                              
                                itemDTO = new ProfesionalFitnessDTO()
                                {
                                    CodigoProfesional = reader[reader.GetOrdinal("CodigoProfesional")].ToString(),
                                   // CodigoTipoProfesional = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTipoProfesional")]),
                                    Nombres = reader[reader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = reader[reader.GetOrdinal("Apellidos")].ToString(),
                                    TipoDocumento = Convert.ToInt32(reader[reader.GetOrdinal("TipoDocumento")]),
                                    DNI = reader[reader.GetOrdinal("DNI")].ToString(),
                                    Telefono = reader[reader.GetOrdinal("Telefono")].ToString(),
                                    Celular = reader[reader.GetOrdinal("Celular")].ToString(),
                                    Correo = reader[reader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]),
                                    ImagenUrl = reader[reader.GetOrdinal("ImagenUrl")].ToString(),
                                    Genero = reader[reader.GetOrdinal("Genero")].ToString(),
                                  //  EstadoCivil = Convert.ToInt32(reader[reader.GetOrdinal("EstadoCivil")]),
                                    Facebook = reader[reader.GetOrdinal("Facebook")].ToString(),
                                    Ubicaciones = reader[reader.GetOrdinal("Ubigeo")].ToString(),
                                    Direccion = reader[reader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = reader[reader.GetOrdinal("Distrito")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]),
                                    CostoPorHora = Convert.ToDecimal(reader[reader.GetOrdinal("CostoPorHora")]),
                                    DstoPorMinuto = Convert.ToDecimal(reader[reader.GetOrdinal("DstoPorMinuto")])
                                };
                            }
                        }
                    }
                }
            }
            
            return itemDTO;
        }

        public ProfesionalFitnessDTO uspBuscarProfesionalFitnessPorNombres(ProfesionalFitnessDTO oProfesionalFitness)
        {
            ProfesionalFitnessDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarProfesionalFitnessPorNombres", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oProfesionalFitness.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oProfesionalFitness.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = oProfesionalFitness.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 50)).Value = oProfesionalFitness.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = oProfesionalFitness.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = oProfesionalFitness.Apellidos;

                    using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (reader.HasRows)
                        {

                            //item.DNIProfesor = reader.GetValue(reader.GetOrdinal(""))
                            while (reader.Read())
                            {
                                //ProfesionalFitnessDTO item = new ProfesionalFitnessDTO();
                                //item.CostoPorHora = Convert.ToDecimal(reader[reader.GetOrdinal("CostoPorHora")]);
                                //item.Disciplina = reader[reader.GetOrdinal("Disciplina")].ToString();
                                //item.NumeroDocumento = reader[reader.GetOrdinal("DNIProfesional")].ToString();
                                //item.FechaHoraInicioClase = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraInicio")]);
                                //item.FechaHoraMarcacion = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso")].ToString());
                                //item.NombreProfesionalFitness = reader[reader.GetOrdinal("Profesor")].ToString();
                                //item.Tardanza = Convert.ToInt32(reader[reader.GetOrdinal("Tardanza")].ToString());
                                //item.TotalAlumnos = Convert.ToInt32(reader[reader.GetOrdinal("TotalAlumnos")].ToString());

                                itemDTO = new ProfesionalFitnessDTO()
                                {
                                    CodigoProfesional = reader[reader.GetOrdinal("CodigoProfesional")].ToString(),
                                    CodigoTipoProfesional = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTipoProfesional")]),
                                    Nombres = reader[reader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = reader[reader.GetOrdinal("Apellidos")].ToString(),
                                    TipoDocumento = Convert.ToInt32(reader[reader.GetOrdinal("TipoDocumento")]),
                                    DNI = reader[reader.GetOrdinal("DNI")].ToString(),
                                    Telefono = reader[reader.GetOrdinal("Telefono")].ToString(),
                                    Celular = reader[reader.GetOrdinal("Celular")].ToString(),
                                    Correo = reader[reader.GetOrdinal("Correo")].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader[reader.GetOrdinal("FechaNacimiento")]),
                                    ImagenUrl = reader[reader.GetOrdinal("ImagenUrl")].ToString(),
                                    Genero = reader[reader.GetOrdinal("Genero")].ToString(),
                                    EstadoCivil = Convert.ToInt32(reader[reader.GetOrdinal("EstadoCivil")]),
                                    Facebook = reader[reader.GetOrdinal("Facebook")].ToString(),
                                    Ubicaciones = reader[reader.GetOrdinal("Ubigeo")].ToString(),
                                    Direccion = reader[reader.GetOrdinal("Direccion")].ToString(),
                                    Distrito = reader[reader.GetOrdinal("Distrito")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")]),
                                    CostoPorHora = Convert.ToDecimal(reader[reader.GetOrdinal("CostoPorHora")]),
                                    DstoPorMinuto = Convert.ToDecimal(reader[reader.GetOrdinal("DstoPorMinuto")])
                                };
                            }
                        }
                    }
                }
            }
            
            return itemDTO;
        }
        
        public string Registrar(ProfesionalFitnessDTO item)
        {
            string campoRetorno = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarProfesionalFitness", conn))
                {    
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional",System.Data.SqlDbType.VarChar,50)).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoProfesional", System.Data.SqlDbType.Int)).Value = item.CodigoTipoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;

                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar,20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;

                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar,200)).Value = item.ImagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar,1)).Value = item.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EstadoCivil", System.Data.SqlDbType.Int)).Value = item.EstadoCivil;
                    cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar,100)).Value = item.Facebook;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 50)).Value = item.Ubicaciones;

                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar,100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar,100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToString(cmd.Parameters["@CodigoProfesional"].Value);
                }
            }
            return Convert.ToString(campoRetorno);
        }
        
        public void Actualizar(ProfesionalFitnessDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarProfesionalFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar,50)).Value = item.CodigoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoProfesional", System.Data.SqlDbType.Int)).Value = item.CodigoTipoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar, 100)).Value = item.Nombres;
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", System.Data.SqlDbType.VarChar, 100)).Value = item.Apellidos;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;

                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 20)).Value = item.DNI;
                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Celular", System.Data.SqlDbType.VarChar, 20)).Value = item.Celular;
                    cmd.Parameters.Add(new SqlParameter("@Correo", System.Data.SqlDbType.VarChar, 100)).Value = item.Correo;
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", System.Data.SqlDbType.DateTime)).Value = item.FechaNacimiento;

                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Genero", System.Data.SqlDbType.VarChar, 1)).Value = item.Genero;
                    cmd.Parameters.Add(new SqlParameter("@EstadoCivil", System.Data.SqlDbType.Int)).Value = item.EstadoCivil;
                    cmd.Parameters.Add(new SqlParameter("@Facebook", System.Data.SqlDbType.VarChar, 100)).Value = item.Facebook;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 50)).Value = item.Ubicaciones;

                    cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 100)).Value = item.Direccion;
                    cmd.Parameters.Add(new SqlParameter("@Distrito", System.Data.SqlDbType.VarChar, 100)).Value = item.Distrito;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarFoto(ProfesionalFitnessDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarFotoProfesionalFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@ImagenUrl", System.Data.SqlDbType.VarChar, 200)).Value = item.ImagenUrl;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioEdicion;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public int uspValidadorDNIProfesionalFitness(string DNI)
        {
            int? campoRetorno = 0;
          
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidadorDNIProfesionalFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DNI", System.Data.SqlDbType.VarChar, 10)).Value = DNI;
                    cmd.Parameters.AddWithValue("@Existe", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                               
                            }
                        }
                    }

                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);
        }

        public string RegistrarActualizarProfesionalFitnessPago(ProfesionalFitnessDTO.ProfesionalFitnessPagos request)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspProfesionalFitnessPagoRegistrar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = request.CodigoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@CostoPorHora", System.Data.SqlDbType.Decimal)).Value = request.CostoPorHora;
                    cmd.Parameters.Add(new SqlParameter("@DstoPorMinuto", System.Data.SqlDbType.Decimal)).Value = request.DstoPorMinuto;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = request.UsuarioCreacion;
                    cmd.ExecuteNonQuery();
                }
            }
            return resultado;
        }
    }
}
