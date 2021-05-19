using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
//using MySqlConnector;

namespace Mr_Doctor.Models
{
    public partial class SecretaryModelEntyties
    {
        private int idSecretary;
        private string name, lastName, motherLastName, dni, phone, address, civilStatus, gender;
        private DateTime date;

        public int IdSecretary { get => idSecretary; set => idSecretary = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MotherLastName { get => motherLastName; set => motherLastName = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string CivilStatus { get => civilStatus; set => civilStatus = value; }
        public string Gender { get => gender; set => gender = value; }
        public DateTime Date { get => date; set => date = value; }


    }
    public static partial class SecretaryModel
    {

        public static int InsertNewSecretary(SecretaryModelEntyties secretaryModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "INSERT INTO secretary(name,last_name,mother_last_name,dni,phone,address,civil_status,gender,date) values(@name, @last_name, @mother_last_name, @dni, @phone, @address, @civil_status, @gender,@date)";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@name", secretaryModel.Name);
                cmd.Parameters.AddWithValue("@last_name", secretaryModel.LastName);
                cmd.Parameters.AddWithValue("@mother_last_name", secretaryModel.MotherLastName);
                cmd.Parameters.AddWithValue("@dni", secretaryModel.Dni);
                cmd.Parameters.AddWithValue("@phone", secretaryModel.Phone);
                cmd.Parameters.AddWithValue("@address", secretaryModel.Address);
                cmd.Parameters.AddWithValue("@civil_status", secretaryModel.CivilStatus);
                cmd.Parameters.AddWithValue("@gender", secretaryModel.Gender);
                cmd.Parameters.AddWithValue("@date", secretaryModel.Date);
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
        public static List<SecretaryModelEntyties> ShowSecretary()
        {
            MySqlDataReader readerRow;

            MySqlConnection Connection = null;

            Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
            Connection.Open();

            string sqlShow = "SELECT * FROM secretary";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            readerRow = cmd.ExecuteReader();

            List<SecretaryModelEntyties> listing = new List<SecretaryModelEntyties>();

            while (readerRow.Read())
            {
                listing.Add(new SecretaryModelEntyties
                {
                    IdSecretary = readerRow.GetInt32("id_secretary"),
                    Name = readerRow.GetString("name"),
                    LastName = readerRow.GetString("last_name"),
                    MotherLastName = readerRow.GetString("mother_last_name"),
                    Dni = readerRow.GetString("dni"),
                    Phone = readerRow.GetString("phone"),
                    Address = readerRow.GetString("address"),
                    CivilStatus = readerRow.GetString("civil_status"),
                    Gender = readerRow.GetString("gender"),
                    Date = readerRow.GetDateTime("date"),

                });
            }
            Connection.Close();
            readerRow.Close();
            return listing;



        }
        public static int DeleteSecretary(int id)
        {
            MySqlDataReader readerRow;
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();

                string sqlDelete = "Delete from secretary WHERE id_secretary=@id";
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

        public static int UpdateSecretart(SecretaryModelEntyties secretaryModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "update secretary SET name=@name,last_name=@last_name,mother_last_name=@mother_last_name," +
                    "dni=@dni,phone=@phone,address=@address,civil_status=@civil_status,gender=@gender where id_secretary = @id_secretary";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@id_secretary", secretaryModel.IdSecretary);
                cmd.Parameters.AddWithValue("@name", secretaryModel.Name);
                cmd.Parameters.AddWithValue("@last_name", secretaryModel.LastName);
                cmd.Parameters.AddWithValue("@mother_last_name", secretaryModel.MotherLastName);
                cmd.Parameters.AddWithValue("@dni", secretaryModel.Dni);
                cmd.Parameters.AddWithValue("@phone", secretaryModel.Phone);
                cmd.Parameters.AddWithValue("@address", secretaryModel.Address);
                cmd.Parameters.AddWithValue("@civil_status", secretaryModel.CivilStatus);
                cmd.Parameters.AddWithValue("@gender", secretaryModel.Gender);
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
