using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dapper;
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
    }

    public static partial class LoginModel
    {

        public static List<LoginModelEntyties> Login(LoginModelEntyties user)
        {
            //string sql = "Select * FROM login where user_name='" + user.UserName + "' and password='"+user.Password+"'";
            string sql = "Select * FROM login where user_name=?userName? and password=?password?";

            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306"))
                {
                return connection.Query<LoginModelEntyties>(sql, new { userName = user.UserName, password=user.Password }).ToList();

                // return connection.Query<LoginModelEntyties>("Select * FROM login where user_name='" + user.UserName + 
                //"and password=" + user.Password + "'").ToList();
                //new { userName = user.UserName });
            }

        }

    }
}
