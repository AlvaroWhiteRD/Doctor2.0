using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
//using MySqlConnector;

namespace Mr_Doctor.Models
{
    public partial class DoctorModelEntyties
    {
        private int idDoctor;
        private string name, lastName, motherLastName, dni, phone, address, civilStatus, gender;
        private string title, execuatur, specialty;
        private DateTime date;

        public int IdDoctor { get => idDoctor; set => idDoctor = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MotherLastName { get => motherLastName; set => motherLastName = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string CivilStatus { get => civilStatus; set => civilStatus = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Title { get => title; set => title = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Execuatur { get => execuatur; set => execuatur = value; }
        public string Specialty { get => specialty; set => specialty = value; }
    }
    public static partial class DoctorModel
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
            catch (Exception erro)
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
        public static List<DoctorModelEntyties> ShowDoctor()
        {
            MySqlDataReader readerRow;

            MySqlConnection Connection = null;

            Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
            Connection.Open();

            string sqlShow = "SELECT * FROM doctor";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            readerRow = cmd.ExecuteReader();

            List<DoctorModelEntyties> listing = new List<DoctorModelEntyties>();

            while (readerRow.Read())
            {
                listing.Add(new DoctorModelEntyties
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



        }
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
                return myReturn = 1;
            }
            catch (Exception)
            {
                return myReturn = -1;
            }

        }

        public static int UpdateDoctor(DoctorModelEntyties doctorModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "update doctor SET name=@name,last_name=@last_name,mother_last_name=@mother_last_name," +
                    "dni=@dni,phone=@phone,address=@address,civil_status=@civil_status,gender=@gender,title=@title,execuatur=@execuatur,specialty=@specialty where id_doctor = @id_doctor";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@id_doctor", doctorModel.IdDoctor);
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
                myReturn = cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                myReturn = -1;
            }
            return myReturn;
        }
    }
}
