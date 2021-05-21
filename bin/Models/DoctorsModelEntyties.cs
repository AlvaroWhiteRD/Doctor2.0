using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
//using MySqlConnector;

namespace Mr_Doctor.Models
{
    public partial class DoctorsModelEntyties
    {
        public int IdDoctor { get; set; }
        public int IdAppointment { get; set; }
        public string MedicalNotes { get; set; }
        public string PrescribedMedical { get; set; }
        public string AppointmentProcess { get; set; }
    }
    public static partial class DoctorsModel
    {

        public static int InsertNewDoctor(DoctorModelEntyties doctorModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "INSERT INTO doctor(name,last_name,mother_last_name,dni,phone,address,civil_status,gender,title,execuatur,specialty,date) values(@name, @last_name, @mother_last_name, @dni, @phone, @address, @civil_status, @gender, @title,@execuatur,@specialty,@date)";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@name", doctorModel.Name);
                cmd.Parameters.AddWithValue("@last_name", doctorModel.LastName);
                cmd.Parameters.AddWithValue("@mother_last_name", doctorModel.MotherLastName);
                cmd.Parameters.AddWithValue("@dni", doctorModel.Dni);
                cmd.Parameters.AddWithValue("@phone", doctorModel.Phone);
                cmd.Parameters.AddWithValue("@address", doctorModel.Address);
                cmd.Parameters.AddWithValue("@civil_status", doctorModel.CivilStatus);
                cmd.Parameters.AddWithValue("@gender", doctorModel.Gender);
                cmd.Parameters.AddWithValue("@title", doctorModel.Title);
                cmd.Parameters.AddWithValue("@execuatur", doctorModel.Execuatur);
                cmd.Parameters.AddWithValue("@specialty", doctorModel.Specialty);
                cmd.Parameters.AddWithValue("@date", doctorModel.Date);
                myReturn = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                myReturn = -1;
            }
            finally
            {
                try
                {
                    Connection.Close();
                }
                catch (Exception)
                {
                }


                // Connection.Dispose();
            }




            return myReturn;
            //using var cmd = Connection.CreateCommand();
            //cmd.CommandText = @"INSERT INTO `BlogPost` (`Title`, `Content`) VALUES (@title, @content);";
            //INSERT INTO `mrdoctor`.`customer` (`name`, `last_name`, `mother_last_name`, `dni`, `phone`, `address`, `civilStatus`, `gender`, `ars`) VALUES ('nombre', 'paterno', 'materno', '123456789', '809829849', 'vive en su casa, pienso yo', 'soltero', 'hombre', '1');

        }
       /* public static List<DoctorsModelEntyties> ShowDoctor()
        {
           /* MySqlDataReader readerRow;

            MySqlConnection Connection = null;

            Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
            Connection.Open();

            string sqlShow = "SELECT * FROM doctor";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            readerRow = cmd.ExecuteReader();

            List<DoctorsModelEntyties> listing = new List<DoctorsModelEntyties>();

            while (readerRow.Read())
            {
                listing.Add(new DoctorsModelEntyties
                {
                    IdDoctor = readerRow.GetInt32("id_doctor"),
                    Name = readerRow.GetString("name"),
                    LastName = readerRow.GetString("last_name"),
                    MotherLastName = readerRow.GetString("mother_last_name"),
                    Dni = readerRow.GetString("dni"),
                    Phone = readerRow.GetString("phone"),
                    Address = readerRow.GetString("address"),
                    CivilStatus = readerRow.GetString("civil_status"),
                    Gender = readerRow.GetString("gender"),
                    Title = readerRow.GetString("title"),
                    Specialty = readerRow.GetString("specialty"),
                    Execuatur = readerRow.GetString("execuatur"),
                    Date = readerRow.GetDateTime("date"),

                });
            }
            Connection.Close();
            readerRow.Close();
            return listing;



        }*/
        public static int DeleteDoctor(int id)
        {
            MySqlDataReader readerRow;
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();

                string sqlDelete = "Delete from doctor WHERE id_doctor=@id";
                MySqlCommand cmd = new MySqlCommand(sqlDelete, Connection);
                cmd.Parameters.AddWithValue("@id", id);
                readerRow = cmd.ExecuteReader();
                myReturn = 1;
            }
            catch (Exception)
            {
                myReturn = -1;
            }
            return myReturn;

        }

        public static int UpdateAppointmentForDoctor(DoctorsModelEntyties appointmentForDoctor)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "update appointment SET " +
                                        "id_appointment=@id_appointment,id_doctor_info=@id_doctor_info," +
                                        "medical_notes=@medical_notes,prescribed_medical=@prescribed_medical," +
                                        "state=@state where id_appointment = @id_appointment";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@id_appointment", appointmentForDoctor.IdAppointment);
                cmd.Parameters.AddWithValue("@id_doctor_info", appointmentForDoctor.IdDoctor);
                cmd.Parameters.AddWithValue("@medical_notes", appointmentForDoctor.MedicalNotes);
                cmd.Parameters.AddWithValue("@prescribed_medical", appointmentForDoctor.PrescribedMedical);
                cmd.Parameters.AddWithValue("@state", appointmentForDoctor.AppointmentProcess);
                myReturn = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                myReturn = -1;
            }
            return myReturn;
        }
    }
}
