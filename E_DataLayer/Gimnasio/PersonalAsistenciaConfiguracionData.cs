
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class PersonalAsistenciaConfiguracionData
    {

        public List<PersonalAsistenciaConfiguracionDTO> Listar(PersonalAsistenciaConfiguracionDTO oPersonalAsistenciaConfiguracionDTO, Paging paging, ref uint recordCount)
        {
            List<PersonalAsistenciaConfiguracionDTO> lista = new List<PersonalAsistenciaConfiguracionDTO>();          
            return lista;
        }

        public PersonalAsistenciaConfiguracionDTO BuscarPorCodigoPersonalAsistenciaConfiguracion(PersonalAsistenciaConfiguracionDTO oPersonalAsistenciaConfiguracion)
        {
            PersonalAsistenciaConfiguracionDTO itemDTO = null;            
            return itemDTO;
        }

        public void Registrar(PersonalAsistenciaConfiguracionDTO item)
        {
            string campoRetorno = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPersonalAsistenciaConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCargo", System.Data.SqlDbType.Int)).Value = item.CodigoCargo;
                    cmd.Parameters.AddWithValue("@CodigoPersonalAsistenciaConfiguracion", "").Direction = System.Data.ParameterDirection.Output;
                    
                    if (item.HoraIngreso_Lunes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Lunes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Lunes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Lunes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Lunes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Lunes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Lunes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Lunes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Martes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Martes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Martes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Martes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Martes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Martes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Martes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Martes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;
                    
                    if (item.HoraIngreso_Miercoles_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Miercoles_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Miercoles_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Miercoles_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Miercoles_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Miercoles_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Miercoles_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Miercoles_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Jueves_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Jueves_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Jueves_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Jueves_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Jueves_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Jueves_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Jueves_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Jueves_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Viernes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Viernes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Viernes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Viernes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Viernes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Viernes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Viernes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Viernes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Sabado_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Sabado_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Sabado_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Sabado_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Sabado_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Sabado_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Sabado_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Sabado_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Domingo_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Domingo_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Domingo_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Domingo_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Domingo_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Domingo_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Domingo_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Domingo_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    //--turno 2

                    if (item.HoraIngreso_Lunes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Lunes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Lunes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Lunes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Lunes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Lunes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Lunes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Lunes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Martes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Martes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Martes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Martes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Martes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Martes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Martes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Martes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Miercoles_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Miercoles_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Miercoles_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Miercoles_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Miercoles_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Miercoles_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Miercoles_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Miercoles_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Jueves_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Jueves_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Jueves_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Jueves_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Jueves_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Jueves_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Jueves_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Jueves_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Viernes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Viernes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Viernes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Viernes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Viernes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Viernes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Viernes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Viernes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Sabado_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Sabado_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Sabado_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Sabado_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Sabado_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Sabado_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Sabado_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Sabado_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Domingo_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Domingo_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Domingo_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Domingo_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Domingo_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Domingo_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Domingo_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Domingo_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    
                    cmd.Parameters.Add(new SqlParameter("@Sueldo", System.Data.SqlDbType.Decimal)).Value = item.Sueldo;
                    cmd.Parameters.Add(new SqlParameter("@MinutosRefrigerio", System.Data.SqlDbType.Int)).Value = item.MinutosRefrigerio;
                    cmd.Parameters.Add(new SqlParameter("@MinutosTolerancia", System.Data.SqlDbType.Int)).Value = item.MinutosTolerancia;
                    cmd.Parameters.Add(new SqlParameter("@DescuentoXMinuto", System.Data.SqlDbType.Decimal)).Value = item.DescuentoXMinuto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,50)).Value = item.UsuarioCreacion??item.UsuarioEdicion;
                    
                    cmd.ExecuteNonQuery();
                    campoRetorno = cmd.Parameters["@CodigoPersonalAsistenciaConfiguracion"].Value.ToString();
                }
            }
        }

        public void Actualizar(PersonalAsistenciaConfiguracionDTO item)
        {
            string campoRetorno = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPersonalAsistenciaConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonal;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCargo", System.Data.SqlDbType.Int)).Value = item.CodigoCargo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistenciaConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAsistenciaConfiguracion;

                    if (item.HoraIngreso_Lunes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Lunes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Lunes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Lunes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Lunes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Lunes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Lunes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Lunes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Martes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Martes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Martes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Martes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Martes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Martes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Martes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Martes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Miercoles_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Miercoles_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Miercoles_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Miercoles_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Miercoles_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Miercoles_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Miercoles_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Miercoles_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Jueves_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Jueves_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Jueves_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Jueves_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Jueves_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Jueves_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Jueves_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Jueves_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Viernes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Viernes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Viernes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Viernes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Viernes_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Viernes_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Viernes_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Viernes_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Sabado_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Sabado_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Sabado_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Sabado_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Sabado_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Sabado_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Sabado_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Sabado_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Domingo_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Domingo_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Domingo_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Domingo_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Domingo_Turno1.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Domingo_Turno1", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Domingo_Turno1.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Domingo_Turno1", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    //--turno 2

                    if (item.HoraIngreso_Lunes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Lunes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Lunes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Lunes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Lunes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Lunes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Lunes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Lunes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Martes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Martes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Martes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Martes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Martes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Martes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Martes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Martes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Miercoles_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Miercoles_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Miercoles_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Miercoles_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Miercoles_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Miercoles_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Miercoles_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Miercoles_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Jueves_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Jueves_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Jueves_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Jueves_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Jueves_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Jueves_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Jueves_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Jueves_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Viernes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Viernes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Viernes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Viernes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Viernes_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Viernes_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Viernes_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Viernes_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Sabado_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Sabado_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Sabado_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Sabado_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Sabado_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Sabado_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Sabado_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Sabado_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraIngreso_Domingo_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Domingo_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraIngreso_Domingo_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraIngreso_Domingo_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;

                    if (item.HoraSalida_Domingo_Turno2.HasValue)
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Domingo_Turno2", System.Data.SqlDbType.Time)).Value = item.HoraSalida_Domingo_Turno2.Value.TimeOfDay;
                    else
                        cmd.Parameters.Add(new SqlParameter("@HoraSalida_Domingo_Turno2", System.Data.SqlDbType.Time)).Value = DBNull.Value;
                    

                    cmd.Parameters.Add(new SqlParameter("@Sueldo", System.Data.SqlDbType.Decimal)).Value = item.Sueldo;
                    cmd.Parameters.Add(new SqlParameter("@MinutosRefrigerio", System.Data.SqlDbType.Int)).Value = item.MinutosRefrigerio;
                    cmd.Parameters.Add(new SqlParameter("@MinutosTolerancia", System.Data.SqlDbType.Int)).Value = item.MinutosTolerancia;
                    cmd.Parameters.Add(new SqlParameter("@DescuentoXMinuto", System.Data.SqlDbType.Decimal)).Value = item.DescuentoXMinuto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion ?? item.UsuarioEdicion;

                    int result = cmd.ExecuteNonQuery();
                    
                }
            }
            
        }

        public void Eliminar(PersonalAsistenciaConfiguracionDTO item)
        {
            
        }
    }
}
