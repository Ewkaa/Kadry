using System;
using System.Collections.Generic;
using System.Data.SqlClient;

//blad w rzutowaniu sex varchar na int
namespace Kadry.Models
{
    public class SQLConnection
    {
        // private string connectionString = "Data Source=DESKTOP-VR8CI70;Initial Catalog=PortfolioWebAppV2;Integrated Security=True;Pooling=False";
        //string connectionString = @"Server=.\DESKTOP-VR8CI70;Database=Kadry;Integrated Security=True;";
        private readonly string connectionString =
            "Data Source=LAPTOP-DOID3AST\\SQLEXPRESS;Initial Catalog=Kadry;Integrated Security=True;Pooling=False";

        private readonly SqlConnection _connection; // wartosc tylko do odczytu w zakresie klasy

        public SQLConnection()
        {
            _connection = new SqlConnection(connectionString); // Konstruktor to jedyne miejsce gdzie mozemy nadac wartosc polu z właściwością "readonly" (chyba)
            _connection.Open();

        }

        ~SQLConnection()
        {
            _connection.Close();
        }

        private SqlCommand Execute(string query) //wykonuje query , aby nie było trzeba dodawać ścieżki 
        {
            return new SqlCommand(query, _connection); //_c oznacza, że zmienna jest prywatna
        }

        public List<ContractType> ContractTypeList()
        {
            List<ContractType> list = new List<ContractType>();

            string query = "SELECT * FROM Contract_type";
            SqlDataReader reader = Execute(query).ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    var contractType = new ContractType()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Contract_name"].ToString()
                    };
                    list.Add(contractType);
                }
            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
            }

            return list;
        }

        public List<Workplace> WorkplaceList()
        {
            List<Workplace> list = new List<Workplace>();

            string query = "SELECT * FROM Workplace";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    var workplace = new Workplace()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Workplace"].ToString()
                    };
                    list.Add(workplace);
                }
            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
            }

            return list;
        }

        public List<Sex> SexList()
        {
            List<Sex> list = new List<Sex>();

            string query = "SELECT * FROM Sex";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    var sex = new Sex()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString()
                    };
                    list.Add(sex);
                }
            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
            }

            return list;
        }

        public List<Employeer> GetAllEmployeers()
        {

            List<Employeer> listEmployeers = new List<Employeer>();
            string query = "SELECT * FROM Employeer";

            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {

                while (reader.Read())
                {
                    var employer = new Employeer()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Firstname = reader["First_name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        MiddleName = reader["Middle_name"].ToString(),
                        Pesel = reader["Pesel"].ToString(),
                        Birthday = DateTime.Parse(reader["Birth_date"].ToString()),
                        ContractDate = DateTime.Parse(reader["Contract_date"].ToString()),
                        ContractEndDate = DateTime.Parse(reader["Contract_end_date"].ToString()),

                    };
                    listEmployeers.Add(employer);
                }
            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
            }

            return listEmployeers;
        }

        public Employeer GetEmployer(int id)
        {
            Employeer employer = null;
            string query = "SELECT * FROM Employeer WHERE id = " + id.ToString();
            //SELECT * FROM Employeer WHERE id=1
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    employer = new Employeer()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Firstname = reader["First_name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        MiddleName = reader["Middle_name"].ToString(),
                        Pesel = reader["Pesel"].ToString(),
                        Birthday = DateTime.Parse(reader["Birth_date"].ToString()),
                        ContractDate = DateTime.Parse(reader["Contract_date"].ToString()),
                        ContractEndDate = DateTime.Parse(reader["Contract_end_date"].ToString()),

                    };

                    var idContractType = int.Parse(reader["id_Contract_type"].ToString());
                    var idWorkplace = int.Parse(reader["id_Workplace"].ToString());
                    var idHoliday = int.Parse(reader["id_Holiday"].ToString());
                    var idMedical = int.Parse(reader["id_Medical"].ToString());
                    var idSalary = int.Parse(reader["id_Salary"].ToString());
                    var idHours = int.Parse(reader["id_Hours"].ToString());
                    var idLogin = int.Parse(reader["id_Login"].ToString());
                    var idSex = int.Parse(reader["id_Sex"].ToString());

                    reader.Close();

                    employer.ContractType = GetContractType(idContractType);
                    employer.Workplace = GetWorkplace(idWorkplace);
                    employer.Holiday = GetHoliday(idHoliday);
                    employer.Medical = GetMedical(idMedical);
                    employer.Salary = GetSalary(idSalary);
                    employer.Hours = GetHours(idHours);
                    employer.Login = GetLogin(idLogin);
                    employer.Sex = GetSex(idSex);

                }

            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
            }

            return employer;
        }

        public ContractType GetContractType(int id)
        {
            ContractType contract = null;
            string query = "SELECT * FROM Contract_type WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    contract = new ContractType()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["Contract_name"].ToString()
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return contract;

        }

        public Workplace GetWorkplace(int id)
        {
            Workplace workplace = null;
            string query = "SELECT * FROM Workplace WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    workplace = new Workplace()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["Workplace"].ToString()
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return workplace;
        }

        public Sex GetSex(int id)
        {
            Sex sex = null;
            string query = "SELECT * FROM Sex WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    sex = new Sex()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["Name"].ToString()
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return sex;
        }
        public Holiday GetHoliday(int id)
        {
            Holiday holiday = null;
            string query = "SELECT * FROM Holiday WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    holiday = new Holiday()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        HolidayAnuses = int.Parse(reader["Holiday_anuses"].ToString()),
                        RemainingHoliday = int.Parse(reader["Remaining_holiday"].ToString()),
                        Year = int.Parse(reader["Year"].ToString())
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return holiday;

        }

        public Medical GetMedical(int id)
        {
            Medical medical = null;
            string query = "SELECT * FROM Medical WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    medical = new Medical()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        ExpirationTime = DateTime.Parse(reader["Expiration_date"].ToString()),
                        Number = int.Parse(reader["Number"].ToString())
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return medical;

        }

        public Salary GetSalary(int id)
        {
            Salary salary = null;
            string query = "SELECT TOP 1 * FROM dbo.Salary  WHERE id = " + id.ToString() + "ORDER BY ID DESC ";
            SqlDataReader reader = Execute(query).ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    salary = new Salary()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Base = Decimal.Parse(reader["Base"].ToString()),
                        Bonus = Decimal.Parse(reader["Bonus"].ToString()),
                        Overtime = Decimal.Parse(reader["Overtime"].ToString()),
                        Total = Decimal.Parse(reader["Total"].ToString())
                    };

                }
            }
            finally
            {
                reader.Close();
            }


            return salary;

        }
        public Login GetLogin(int id)
        {
            Login login = null;
            string query = "SELECT TOP 1 * FROM dbo.login  WHERE id = " + id.ToString() + "ORDER BY ID DESC ";
            SqlDataReader reader = Execute(query).ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    login = new Login()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Username = reader["login"].ToString(),
                        Password = reader["Password"].ToString(),
                        Admin = Convert.ToBoolean(Int32.Parse(reader["admin"].ToString()))


                    };

                }
            }
            finally
            {
                reader.Close();
            }


            return login;

        }


        public Hours GetHours(int id)
        {
            Hours hours = null;
            string query = "SELECT TOP 1 * FROM dbo.Hours  WHERE id = " + id.ToString() + "ORDER BY ID DESC ";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    hours = new Hours()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        HoursWorked = Decimal.Parse(reader["Hours_worked"].ToString()),
                        QuantityOvertime = Decimal.Parse(reader["Quantity_overtime"].ToString())
                    };
                }

            }
            finally
            {
                reader.Close();
            }


            return hours;

        }

        public bool IsPasswordCorrect(string username, string password)
        {
            bool isPasswordCorrect = false;
            string query = "SELECT * FROM LOGIN WHERE login = '" + username + "'";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    isPasswordCorrect = (reader["Password"].ToString() == password);

                }

            }
            finally
            {
                reader.Close();
            }

            return isPasswordCorrect;

        }

        public bool IsAdmin(int id)
        {
            bool isAdmin = false;
            string query = "SELECT admin FROM LOGIN WHERE id = " + id;
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    isAdmin = Convert.ToBoolean(Int32.Parse(reader["Admin"].ToString()));
                }

            }
            finally
            {
                reader.Close();
            }

            return isAdmin;
        }

        public int GetUserId(int idLogin)
        {
            int result = -1;
            string query = "SELECT ID FROM EMPLOYEER WHERE ID_login  = " + idLogin.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())

                {
                    result = int.Parse(reader["id"].ToString());

                }

            }
            finally
            {
                reader.Close();
            }

            return result;
        }

        public int GetLoginId(string username)
        {
            int result = -1;
            string query = "SELECT ID FROM Login WHERE  Login= '" + username + "'";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())

                {
                    result = int.Parse(reader["id"].ToString());


                }

            }
            finally
            {
                reader.Close();
            }

            return result;

        }
        private int GetLastId(string tableName)
        {
            int returnId = -1;
            string query = "SELECT TOP(1) [id] FROM [" + tableName + "] ORDER BY [Id] DESC";
            SqlDataReader reader2 = Execute(query).ExecuteReader();
            try
            {
                while (reader2.Read())
                {
                    returnId = int.Parse(reader2["id"].ToString());
                }
            }
            finally
            {
                reader2.Close();
            }

            return returnId;
        }
        public int CreateOrUpdateWorkplace(Workplace workplace)
        {
            int returnId = -1;

            string query = "INSERT INTO Workplace ([Workplace]) VALUES ('" + workplace.Name + "')";

            Workplace workpl = GetWorkplace(workplace.Id);
            if (workpl != null)
            {
                query = " UPDATE Workplace SET [Workplace] = (' " + workplace.Name + "') WHERE id= " + workpl.Id;
                returnId = workpl.Id;
            }
            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }


            return GetLastId("Workplace");
        }



        public int CreateOrUpdateMedical(Medical medical)
        {
            int returnId = -1;

            string query = "INSERT INTO Medical ([Expiration_date], [Number]) VALUES ('" + medical.ExpirationTime.ToShortDateString() + "'," +
                           medical.Number + ")";


            Medical med = GetMedical(medical.Id);
            if (med != null)
            {
                query = " UPDATE Medical SET [Expiration_date] = '" + medical.ExpirationTime.ToString("yyyy-MM-DD") +
                        "',[Number] = " + medical.Number + "WHERE id=" + medical.Id;
                returnId = medical.Id;
            }

            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }

            return GetLastId("Medical");
        }

        public int CreateOrUpdateHoliday(Holiday holiday)
        {
            int returnId = -1;

            string query = "INSERT INTO Holiday ([Holiday_anuses],[Remaining_holiday],[Year]) VALUES (" + holiday.HolidayAnuses + "," + holiday.RemainingHoliday + "," + holiday.Year + ")";


            Holiday hol = GetHoliday(holiday.Id);
            if (hol != null)
            {
                query = " UPDATE Holiday SET [Holiday_anuses] = '" + holiday.HolidayAnuses +
                        ", [Remaining_holiday]= " + holiday.RemainingHoliday + ",[Year]=" + holiday.Year +
                        "WHERE id=" + holiday.Id;

                returnId = holiday.Id;

            }

            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }

            return GetLastId("Holiday");
        }

        public int CreateOrUpdateSalary(Salary salary)
        {
            int returnId = -1;

            string query = "INSERT INTO Salary ([Base],[Bonus],[Overtime],[Total]) VALUES (" + salary.Base + "," + salary.Bonus.GetValueOrDefault() + "," + salary.Overtime.GetValueOrDefault() + "," + salary.Total + ")";


            Salary sal = GetSalary(salary.Id);
            if (sal != null)
            {
                query = " UPDATE Salary SET [Base] = '" + salary.Base + ", [Bonus]= " + salary.Bonus + ",[Overtime]=" + salary.Overtime + ",[Total]" + salary.Total + "WHERE id=" + salary.Id;
                returnId = salary.Id;
            }

            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }

            return GetLastId("Salary");
        }

        public int CreateOrUpdateHours(Hours hours)
        {
            int returnId = -1;

            string query = "INSERT INTO Hours ([Hours_worked],[Quantity_overtime]) VALUES (" + hours.HoursWorked + "," + hours.QuantityOvertime + ")";


            Hours hou = GetHours(hours.Id);
            if (hou != null)
            {
                query = " UPDATE Hours SET [Hours_worked] = " + hours.HoursWorked + ", [Quantity_overtime]= " + hours.QuantityOvertime + "WHERE id=" + hours.Id;
                returnId = hours.Id;
            }

            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }
            return GetLastId("Hours");
        }

        public int CreateOrUpdateContract(ContractType contract)
        {
            int returnId = -1;

            string query = "INSERT INTO Contract_type VALUES ('" + contract.Name + "')";


            ContractType con = GetContractType(contract.Id);
            if (con != null)
            {
                query = " UPDATE Contract_type SET [Contract_name] = '" + contract.Name + "' WHERE id=" + contract.Id;
                returnId = contract.Id;

            }

            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }
            return GetLastId("Contract_type");
        }
        public int CreateOrUpdateSex(Sex sex)
        {
            int returnId = -1;

            string query = "INSERT INTO Sex VALUES ('" + sex.Name + "')";


            Sex se = GetSex(sex.Id);
            if (se != null)
            {
                query = " UPDATE Sex SET [Name] = '" + sex.Name + "' WHERE id=" + sex.Id;
                returnId = sex.Id;

            }

            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }

            return GetLastId("Sex");
        }

        public int CreateLogin(Login login)
        {
            int returnId = -1;

            string query = "INSERT INTO login VALUES ('" + login.Username + "','" + login.Password + "'," +
                           (login.Admin ? "1" : "0") + ")";


            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }

            return GetLastId("login");
        }



        public int CreateEmployer(Employeer employer)
        {
            int returnId = -1;

            string query =
                "INSERT INTO [dbo].[Employeer]" +
                "([Surname]" +
                ",[First_name]" +
                ",[Middle_name]" +
                ",[Pesel]" +
                ",[Birth_date]" +
                ",[id_Sex]" +
                ",[id_login]" +
                ",[id_Contract_type]" +
                ",[id_Hours]" +
                ",[id_Holiday]" +
                ",[id_Salary]" +
                ",[id_Medical]" +
                ",[id_Workplace]" +
                ",[Contract_date]" +
                ",[Contract_end_date])" +
                "VALUES(" +
                "'" + employer.Surname + "', " +
                "'" + employer.Firstname + "', " +
                "'" + employer.MiddleName + "', " +
                "'" + employer.Pesel + "', " +
                "'" + employer.Birthday.ToShortDateString() + "', " +
                CreateLogin(employer.Login) + ", " +

                 CreateOrUpdateContract(employer.ContractType).ToString() + ", " +
                 CreateOrUpdateHours(employer.Hours).ToString() + ", " +
                 CreateOrUpdateHoliday(employer.Holiday).ToString() + ", " +
                 CreateOrUpdateSalary(employer.Salary).ToString() + ", " +
                 CreateOrUpdateMedical(employer.Medical).ToString() + ", " +
                 CreateOrUpdateWorkplace(employer.Workplace).ToString() + "," +
                 CreateOrUpdateSex(employer.Sex).ToString() + "," +
                "'" + employer.ContractDate.ToShortDateString() + "', " +
                "'" + employer.ContractEndDate.ToShortDateString() + "')";

            try
            {
                Execute(query).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return returnId;
            }

            return GetLastId("Employeer");
        }

        public void RemoveEmployeer(int id)
        {   //Holiday, Hours,login,Medical, Salary

            var emplo = GetEmployer(id);
            string query = "Delete FROM Holiday where id=" + emplo.Holiday.Id;
            Execute(query).ExecuteNonQuery();
            query = "Delete FROM Hours where id=" + emplo.Hours.Id;
            Execute(query).ExecuteNonQuery();
            query = "Delete FROM login where id=" + emplo.Login.Id;
            Execute(query).ExecuteNonQuery();
            query = "Delete FROM Medical where id=" + emplo.Medical.Id;
            Execute(query).ExecuteNonQuery();
            query = "Delete FROM Salary where id=" + emplo.Salary.Id;
            Execute(query).ExecuteNonQuery();
            query = "Delete FROM Sex where id=" + emplo.Sex.Id;
            Execute(query).ExecuteNonQuery();
            query = "Delete FROM Employeer where id=" + id;
            Execute(query).ExecuteNonQuery();

        }

    }


}
