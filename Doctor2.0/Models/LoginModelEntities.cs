using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dapper;
using Doctor2.Controllers;
using MySql.Data.MySqlClient;
//using MySqlConnector;

namespace Mr_Doctor.Models
{
    public partial class LoginModelEntyties
    {
        public int IdLogin { get; set; }
        [Required(ErrorMessage ="Escriba su nombre de usuario")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Escriba su nombre su contraseña")]
        public string Password { get; set; }
        public int IdRol { get; set; }
        public int IdDoctor { get; set; }
    }

    public static partial class LoginModel
    {

        public static List<LoginModelEntyties> Login(LoginModelEntyties user)
        {
            //string sql = "Select * FROM login where user_name='" + user.UserName + "' and password='"+user.Password+"'";
            string sql = "Select * FROM login where user_name=@userName and password=@password";

            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306"))
            {
                var passwordEncript = EncryptPassword.GetSHA256(user.Password);
                //var login =  connection.Query<LoginModelEntyties>(sql, new { userName = user.UserName, password=user.Password }).ToList();
                MySqlCommand login = new MySqlCommand(sql, connection);
                login.Parameters.AddWithValue("@userName", user.UserName);
                login.Parameters.AddWithValue("@password", passwordEncript);

                connection.Open();
                MySqlDataReader reader = login.ExecuteReader();
                // siempre retorana solo una fila

                LoginModelEntyties obj = new LoginModelEntyties();
                reader.Read();

                //if (reader.HasRows)
                //{
                    if (reader != null)
                    {
                        obj.IdRol = Convert.ToInt32(reader["id_rol"]);
                        obj.UserName = Convert.ToString(reader["user_name"]);
                        obj.IdDoctor = Convert.ToInt32(reader["id_doctor"]);
                    }
                //}
                List<LoginModelEntyties> list = new List<LoginModelEntyties>();

                list.Add(obj);

                return list;
                // return connection.Query<LoginModelEntyties>("Select * FROM login where user_name='" + user.UserName + 
                //"and password=" + user.Password + "'").ToList();
                //new { userName = user.UserName });
            }

        }

    }
}
