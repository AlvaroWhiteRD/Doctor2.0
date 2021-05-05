using System;
using MySqlConnector;

namespace Mr_Doctor.Models
{
    public partial class CustomerModelEntyties
    {
        private int idCustomer;
        private string name, lastName, motherLastName, dni, phone, address, civilStatus, gender;
        private bool ars;
        private DateTime date;

        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MotherLastName { get => motherLastName; set => motherLastName = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string CivilStatus { get => civilStatus; set => civilStatus = value; }
        public string Gender { get => gender; set => gender = value; }
        public bool Ars { get => ars; set => ars = value; }
        public DateTime Date { get => date; set => date = value; }


    }
    public static partial class CustomerModel
    {

        public static int InsertNewCustomer(CustomerModelEntyties customerModel)
        {
            //llamamos la cadena de conexion
            MySqlConnection Connection = new MySqlConnection("DBConnection");

            string sqlUpdate = "INSERT INTO customer (@name, @last_name, @mother_last_name@, @dni, @phone, @address, @civilStatus@, @gender, @ars,@date)";

            MySqlCommand cmd = new MySqlCommand(sqlUpdate, Connection);
            //pasamos los parametros
            cmd.Parameters.AddWithValue("@name", customerModel.Name);
            cmd.Parameters.AddWithValue("@last_name", customerModel.LastName);
            cmd.Parameters.AddWithValue("@mother_last_name", customerModel.MotherLastName);
            cmd.Parameters.AddWithValue("@dni", customerModel.Dni);
            cmd.Parameters.AddWithValue("@phone", customerModel.Phone);
            cmd.Parameters.AddWithValue("@address", customerModel.Address);
            cmd.Parameters.AddWithValue("@civilStatus", customerModel.CivilStatus);
            cmd.Parameters.AddWithValue("@gender", customerModel.Gender);
            cmd.Parameters.AddWithValue("@ars", customerModel.Ars);
            cmd.Parameters.AddWithValue("@date", customerModel.Date);

            int myReturn;
            myReturn = cmd.ExecuteNonQuery();
            Connection.Clone();
            return myReturn;
            //using var cmd = Connection.CreateCommand();
            //cmd.CommandText = @"INSERT INTO `BlogPost` (`Title`, `Content`) VALUES (@title, @content);";
            //INSERT INTO `mrdoctor`.`customer` (`name`, `last_name`, `mother_last_name`, `dni`, `phone`, `address`, `civilStatus`, `gender`, `ars`) VALUES ('nombre', 'paterno', 'materno', '123456789', '809829849', 'vive en su casa, pienso yo', 'soltero', 'hombre', '1');

        }
    }
}
