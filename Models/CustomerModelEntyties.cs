using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
//using MySqlConnector;

namespace Mr_Doctor.Models
{
    public partial class CustomerModelEntyties
    {
        private int idCustomer;
        private string name, lastName, motherLastName, dni, phone, address, civilStatus, gender;
        private int ars;
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
        public int Ars { get => ars; set => ars = value; }
        public DateTime Date { get => date; set => date = value; }


    }
    public static partial class CustomerModel
    {

        public static int InsertNewCustomer(CustomerModelEntyties customerModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "INSERT INTO customer(name,last_name,mother_last_name,dni,phone,address,civil_status,gender,ars,date) values(@name, @last_name, @mother_last_name, @dni, @phone, @address, @civil_status, @gender, @ars,@date)";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@name", customerModel.Name);
                cmd.Parameters.AddWithValue("@last_name", customerModel.LastName);
                cmd.Parameters.AddWithValue("@mother_last_name", customerModel.MotherLastName);
                cmd.Parameters.AddWithValue("@dni", customerModel.Dni);
                cmd.Parameters.AddWithValue("@phone", customerModel.Phone);
                cmd.Parameters.AddWithValue("@address", customerModel.Address);
                cmd.Parameters.AddWithValue("@civil_status", customerModel.CivilStatus);
                cmd.Parameters.AddWithValue("@gender", customerModel.Gender);
                cmd.Parameters.AddWithValue("@ars", customerModel.Ars);
                cmd.Parameters.AddWithValue("@date", customerModel.Date);
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
        public static List<CustomerModelEntyties> ShowCustomer()
        {

            MySqlDataReader readerRow;

            MySqlConnection Connection = null;

            Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
            Connection.Open();

            string sqlShow = "SELECT * FROM customer";

            MySqlCommand cmd = new MySqlCommand(sqlShow, Connection);
            readerRow = cmd.ExecuteReader();

            List<CustomerModelEntyties> listing = new List<CustomerModelEntyties>();

            while (readerRow.Read())
            {
                listing.Add(new CustomerModelEntyties
                {
                    IdCustomer = readerRow.GetInt32("id_customer"),
                    Name = readerRow.GetString("name"),
                    LastName = readerRow.GetString("last_name"),
                    MotherLastName = readerRow.GetString("mother_last_name"),
                    Dni = readerRow.GetString("dni"),
                    Phone = readerRow.GetString("phone"),
                    Address = readerRow.GetString("address"),
                    CivilStatus = readerRow.GetString("civil_status"),
                    Gender = readerRow.GetString("gender"),
                    Ars = readerRow.GetInt32("ars"),
                    Date = readerRow.GetDateTime("date"),

                });
            }
            Connection.Close();
            readerRow.Close();
            return listing;



        }
        public static int DeleteCustomer(int id)
        {
            MySqlDataReader readerRow;
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();

                string sqlDelete = "Delete from customer WHERE id_customer=@id";
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
    
        /* public static List<CustomerModelEntyties> EditCustomer(int id) {
             MySqlDataReader readerRow;
             MySqlConnection Connection = null;

                 Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                 Connection.Open();
                 string sqlOneRegister = "SELECT * from customer WHERE id_customer = @id";

                 MySqlCommand cmd = new MySqlCommand(sqlOneRegister,Connection);

                 cmd.Parameters.AddWithValue("@id", id);
                 readerRow = cmd.ExecuteReader();

                 List<CustomerModelEntyties> listing = new List<CustomerModelEntyties>();

                 while (readerRow.Read())
                 {
                     listing.Add(new CustomerModelEntyties
                     {
                         IdCustomer = readerRow.GetInt32("id_customer"),
                         Name = readerRow.GetString("name"),
                         LastName = readerRow.GetString("last_name"),
                         MotherLastName = readerRow.GetString("mother_last_name"),
                         Dni = readerRow.GetString("dni"),
                         Phone = readerRow.GetString("phone"),
                         Address = readerRow.GetString("address"),
                         CivilStatus = readerRow.GetString("civil_status"),
                         Gender = readerRow.GetString("gender"),
                         Ars = readerRow.GetInt32("ars"),

                     });
                 }
                 Connection.Close();
                 readerRow.Close();
                 return listing;

             }*/
        public static int UpdateCustomer(CustomerModelEntyties customerModel)
        {
            int myReturn;
            MySqlConnection Connection = null;
            try
            {
                Connection = new MySqlConnection("Server=localhost;Uid=root;Password=;Database=mrdoctor;Port=3306");
                Connection.Open();
                string sqlInsert = "update customer SET name=@name,last_name=@last_name,mother_last_name=@mother_last_name," +
                    "dni=@dni,phone=@phone,address=@address,civil_status=@civil_status,gender=@gender,ars=@ars where id_customer = @id_customer";

                MySqlCommand cmd = new MySqlCommand(sqlInsert, Connection);
                //pasamos los parametros
                cmd.Parameters.AddWithValue("@id_customer", customerModel.IdCustomer);
                cmd.Parameters.AddWithValue("@name", customerModel.Name);
                cmd.Parameters.AddWithValue("@last_name", customerModel.LastName);
                cmd.Parameters.AddWithValue("@mother_last_name", customerModel.MotherLastName);
                cmd.Parameters.AddWithValue("@dni", customerModel.Dni);
                cmd.Parameters.AddWithValue("@phone", customerModel.Phone);
                cmd.Parameters.AddWithValue("@address", customerModel.Address);
                cmd.Parameters.AddWithValue("@civil_status", customerModel.CivilStatus);
                cmd.Parameters.AddWithValue("@gender", customerModel.Gender);
                cmd.Parameters.AddWithValue("@ars", customerModel.Ars);
                myReturn = cmd.ExecuteNonQuery();
            }
            catch (Exception ass)
            {
                string error = ass.Message;
                myReturn = -1;
            }
            return myReturn;
        }
    }
}
