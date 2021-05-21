using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
//using MySqlConnector;

namespace Mr_Doctor.Models
{
    public partial class UserManagerModelEntyties
    {
        public int IdLogin { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int IdRol { get; set; }

        public int IdSecretary { get; set; }

        public string NameSecretary { get; set; }

        public string LastNameSecretary { get; set; }

        public int IdDoctor { get; set; }

        public string NameDoctor { get; set; }

        public string LastNameDoctor { get; set; }


    }
    public static partial class UserManagerModel
    {

        public static int InsertNewUser(UserManagerModelEntyties userModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "INSERT INTO login(user_name,password,id_rol,id_secretary,id_doctor) values(@userName, @password, @idRol, @idSecretary, @idDoctor)";
                var passwordEncript = Doctor2.Controllers.EncryptPassword.GetSHA256(userModel.Password);

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@userName", userModel.UserName);
                cmd.Parameters.AddWithValue("@password", passwordEncript);
                cmd.Parameters.AddWithValue("@idRol", userModel.IdRol);
                cmd.Parameters.AddWithValue("@idSecretary", userModel.IdSecretary);
                cmd.Parameters.AddWithValue("@idDoctor", userModel.IdDoctor);
                myReturn = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                myReturn = -1;
            }
            finally
            {

                Connection.Close();
            }




            return myReturn;
            //using var cmd = Connection.CreateCommand();
            //cmd.CommandText = @"INSERT INTO `BlogPost` (`Title`, `Content`) VALUES (@title, @content);";
            //INSERT INTO `mrdoctor`.`customer` (`name`, `last_name`, `mother_last_name`, `dni`, `phone`, `address`, `civilStatus`, `gender`, `ars`) VALUES ('nombre', 'paterno', 'materno', '123456789', '809829849', 'vive en su casa, pienso yo', 'soltero', 'hombre', '1');

        }
        public static List<UserManagerModelEntyties> ShowUserDoctor()
        {
            MySqlDataReader readerRow;

            MySqlConnection Connection = null;

            Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
            Connection.Open();

            string sqlShow = "SELECT id_login,user_name,id_rol,login.id_doctor, doctor.name, doctor.last_name " +
                "From login " +
                "INNER JOIN doctor ON login.id_doctor = doctor.id_doctor ORDER BY id_login";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            readerRow = cmd.ExecuteReader();

            List<UserManagerModelEntyties> listing = new List<UserManagerModelEntyties>();

            while (readerRow.Read())
            {
                listing.Add(new UserManagerModelEntyties
                {
                    IdDoctor = readerRow.GetInt32("id_doctor"),
                    UserName = readerRow.GetString("user_name"),
                    IdRol = readerRow.GetInt32("id_rol"),
                    IdLogin = readerRow.GetInt32("id_login"),
                    NameDoctor = readerRow.GetString("name"),
                    LastNameDoctor = readerRow.GetString("last_name"),
           
                });
            }
            Connection.Close();
            readerRow.Close();
            return listing;



        }

        public static List<UserManagerModelEntyties> ShowUserSecretary()
        {
            MySqlDataReader readerRow;

            MySqlConnection Connection = null;

            Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
            Connection.Open();

            string sqlShow = "SELECT id_login,user_name,id_rol,login.id_secretary, secretary.name, secretary.last_name " +
                "From login " +
                "INNER JOIN secretary ON login.id_secretary = secretary.id_secretary " +
                "ORDER BY id_login";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            readerRow = cmd.ExecuteReader();

            List<UserManagerModelEntyties> listing = new List<UserManagerModelEntyties>();

            while (readerRow.Read())
            {
                listing.Add(new UserManagerModelEntyties
                {
                    IdSecretary = readerRow.GetInt32("id_secretary"),
                    UserName = readerRow.GetString("user_name"),
                    IdRol = readerRow.GetInt32("id_rol"),
                    IdLogin = readerRow.GetInt32("id_login"),
                    NameSecretary = readerRow.GetString("name"),
                    LastNameSecretary = readerRow.GetString("last_name"),

                });
            }
            Connection.Close();
            readerRow.Close();
            return listing;



        }
        public static int DeleteUser(int id)
        {
            MySqlDataReader readerRow;
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();

                string sqlDelete = "Delete from login WHERE id_login=@id";
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

        public static int UpdateUser(UserManagerModelEntyties entyties)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "update login SET user_name=@userName," +
                    "id_rol=@idRol,id_secretary=@idSecretary,id_doctor=@idDoctor" +
                    " where id_login=@idLogin";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@userName", entyties.UserName);
                cmd.Parameters.AddWithValue("@idRol", entyties.IdRol);
                cmd.Parameters.AddWithValue("@idSecretary", entyties.IdSecretary);
                cmd.Parameters.AddWithValue("@idDoctor", entyties.IdDoctor);
                cmd.Parameters.AddWithValue("@idLogin", entyties.IdLogin);
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

