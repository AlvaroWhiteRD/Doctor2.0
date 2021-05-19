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
        private string doctorName, doctorLastName, customerName, customerLastName;
   
        public int IdAppointment { get => idAppointment; set => idAppointment = value; }
        public int IdDoctor { get => idDoctor; set => idDoctor = value; }
        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string Description { get => description; set => description = value; }
        public string TimeAppointment { get => timeAppointment; set => timeAppointment = value; }
        public DateTime DateAppointment { get => dateAppointment; set => dateAppointment = value; }
        public string State { get => state; set => state = value; }
        public DateTime AppointmentCreated { get => appointmentCreated; set => appointmentCreated = value; }
        public string DoctorName { get => doctorName; set => doctorName = value; }
        public string DoctorLastName { get => doctorLastName; set => doctorLastName = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string CustomerLastName { get => customerLastName; set => customerLastName = value; }
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
            //realizamos un select con inner join para traer el nombre y apellido del paciente y el doctro
            string sqlShow = "Select id_appointment,description,date_appointment,time_appointment,state,doctor.name, doctor.last_name,customer.name, customer.last_name " +
                                 "From appointment " +
                                    "INNER JOIN doctor ON appointment.id_doctor = doctor.id_doctor " +
                                    "INNER JOIN customer ON appointment.id_customer = customer.id_customer " +
                                    "ORDER BY date_appointment";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            readerRow = cmd.ExecuteReader();
            //creamos la lista que almacenara el resultado de la busqueda en la base de datos
            List<AppointmentModelEntyties> listing = new List<AppointmentModelEntyties>();
            var prueba = readerRow.Read();

            while (readerRow.Read())
            {
                listing.Add(new AppointmentModelEntyties
                {//0 id cita // 1 descripcion // 2date apoitment // 3 la hora // 4 state
                    //5 nomb doc 6 app doct 7 nombre cleinte appe 8
                    IdAppointment = readerRow.GetInt32("id_appointment"),
                    Description = readerRow.GetString("description"),
                    TimeAppointment = readerRow.GetString("time_appointment"),
                    State = readerRow.GetString("state"),
                    DateAppointment = readerRow.GetDateTime("date_appointment"),
                    DoctorName = readerRow.GetString(5),
                    DoctorLastName = readerRow.GetString(6),
                    CustomerName = readerRow.GetString(7),
                    CustomerLastName = readerRow.GetString(8),


                });
            }
            Connection.Close();
            readerRow.Close();
            return listing;

        }

        public static List<AppointmentModelEntyties> ShowAppointmentForDoctor( int idDoctor )
        {
            MySqlDataReader readerRow;

            MySqlConnection Connection = null;

            Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
            Connection.Open();
            //realizamos un select con inner join para traer el nombre y apellido del paciente
            string sqlShow = "Select id_appointment,description,date_appointment,time_appointment,state,customer.name, customer.last_name " +
                                 "From appointment " +
                                    "INNER JOIN doctor ON appointment.id_doctor = doctor.id_doctor " +
                                    "INNER JOIN customer ON appointment.id_customer = customer.id_customer" +
                                    " WHERE appointment.id_doctor = @idDoctor and state = 'pendiente' " +
                                    "ORDER BY date_appointment";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            cmd.Parameters.AddWithValue("@idDoctor", idDoctor);
            readerRow = cmd.ExecuteReader();

            //creamos la lista que almacenara el resultado de la busqueda en la base de datos
            List<AppointmentModelEntyties> listing = new List<AppointmentModelEntyties>();
            var prueba = readerRow.Read();

            while (readerRow.Read())
            {
                listing.Add(new AppointmentModelEntyties
                {//0 id cita // 1 descripcion // 2date apoitment // 3 la hora // 4 state
                    //5 nomb doc 6 app doct 7 nombre cleinte appe 8
                    IdAppointment = readerRow.GetInt32("id_appointment"),
                    Description = readerRow.GetString("description"),
                    TimeAppointment = readerRow.GetString("time_appointment"),
                    State = readerRow.GetString("state"),
                    DateAppointment = readerRow.GetDateTime("date_appointment"),
                    CustomerName = readerRow.GetString("name"),
                    CustomerLastName = readerRow.GetString("last_name")

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
