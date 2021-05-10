using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
//using MySqlConnector;

namespace Mr_Doctor.Models
{
    public partial class AppointmentModelEntyties
    {
        private int idAppointment, idDoctor, idCustomer;
        private string description, timeAppointment, state;
        private DateTime dateAppointment, appointmentCreated;
   
        public int IdAppointment { get => idAppointment; set => idAppointment = value; }
        public int IdDoctor { get => idDoctor; set => idDoctor = value; }
        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string Description { get => description; set => description = value; }
        public string TimeAppointment { get => timeAppointment; set => timeAppointment = value; }
        public DateTime DateAppointment { get => dateAppointment; set => dateAppointment = value; }
        public string State { get => state; set => state = value; }
        public DateTime AppointmentCreated { get => appointmentCreated; set => appointmentCreated = value; }
    }
    public static partial class AppointmentModel
    {

        public static int InsertNewAppointment(AppointmentModelEntyties appointmentModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "INSERT INTO appointment(id_customer,id_doctor,description,date_appointment,time_appointment,appointment_created,state) values(@id_customer, @id_doctor, @description, @date_appointment,@time_appointment,  @appointment_created, @state)";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);

                //pasamos los parametros
                cmd.Parameters.AddWithValue("@id_customer", appointmentModel.IdCustomer);
                cmd.Parameters.AddWithValue("@id_doctor", appointmentModel.IdDoctor);
                cmd.Parameters.AddWithValue("@description", appointmentModel.Description);
                cmd.Parameters.AddWithValue("@time_appointment", appointmentModel.TimeAppointment);
                cmd.Parameters.AddWithValue("@state", appointmentModel.State);
                cmd.Parameters.AddWithValue("@appointment_created", appointmentModel.AppointmentCreated);
                //le cambiamos el formato a la fecha para que mysql no lance error
                //string dateFormat = appointmentModel.DateAppointment.ToString("yyyyMMdd");
                cmd.Parameters.AddWithValue("@date_appointment", appointmentModel.DateAppointment);


               
                myReturn = cmd.ExecuteNonQuery();
    }
            catch (Exception e)
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
            
        }
        public static List<AppointmentModelEntyties> ShowAppointment()
        {
            MySqlDataReader readerRow;

            MySqlConnection Connection = null;

            Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
            Connection.Open();

            string sqlShow = "SELECT * FROM appointment";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            readerRow = cmd.ExecuteReader();

            List<AppointmentModelEntyties> listing = new List<AppointmentModelEntyties>();

            while (readerRow.Read())
            {
                listing.Add(new AppointmentModelEntyties
                {
                    IdDoctor = readerRow.GetInt32("id_doctor"),
                    IdCustomer = readerRow.GetInt32("id_customer"),
                    IdAppointment = readerRow.GetInt32("id_appointment"),
                    Description = readerRow.GetString("description"),
                    TimeAppointment = readerRow.GetString("time_appointment"),
                    State = readerRow.GetString("state"),
                    DateAppointment = readerRow.GetDateTime("date_appointment"),
                    AppointmentCreated = readerRow.GetDateTime("appointment_created")

                });
            }
            Connection.Close();
            readerRow.Close();
            return listing;

        }
        public static int DeleteAppointment(int id)
        {
            MySqlDataReader readerRow;
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();

                string sqlDelete = "Delete from appointment WHERE id_appointment=@id";
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

        public static int UpdateAppointment(AppointmentModelEntyties appointmentModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "update appointment SET id_customer=@id_customer,id_doctor=@id_doctor,description=@description,date_appointment=@date_appointment,time_appointment=@time_appointment,appointment_created=@appointment_created,state=@state where id_appointment = @id_appointment";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@id_appointment", appointmentModel.IdAppointment);
                cmd.Parameters.AddWithValue("@id_customer", appointmentModel.IdCustomer);
                cmd.Parameters.AddWithValue("@id_doctor", appointmentModel.IdDoctor);
                cmd.Parameters.AddWithValue("@description", appointmentModel.Description);
                cmd.Parameters.AddWithValue("@time_appointment", appointmentModel.TimeAppointment);
                cmd.Parameters.AddWithValue("@state", appointmentModel.State);
                cmd.Parameters.AddWithValue("@date_appointment", appointmentModel.DateAppointment);
                cmd.Parameters.AddWithValue("@appointment_created", appointmentModel.AppointmentCreated);
                myReturn = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                myReturn = -1;
            }
            return myReturn;
        }
    }
}
