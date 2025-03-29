using System;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem
{
    public class Program
    {
        private static string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ZensarTrDb;Integrated Security=True;Encrypt=True";
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Create an Employee");
                Console.WriteLine("2. Create a Department");
                Console.WriteLine("3. Create a Manager");
                Console.WriteLine("4. List Departments of an Employee");
                Console.WriteLine("5. List Managers of an Employee");
                Console.WriteLine("6. List Employees under a Manager");
                Console.WriteLine("7. List Details of an Employee");
                Console.WriteLine("8. Add Department to Employee/Manager");
                Console.WriteLine("9. Add Manager to an Employee/Manager");
                Console.WriteLine("10. Remove Manager from Employee");
                Console.WriteLine("11. Remove Department from Employee/Manager");
                Console.WriteLine("12. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateEmployee();
                        break;
                    case "2":
                        CreateDepartment();
                        break;
                    case "3":
                        CreateManager();
                        break;
                    case "4":
                        ListDepartmentsOfEmployee();
                        break;
                    case "5":
                        ListManagersOfEmployee();
                        break;
                    case "6":
                        ListEmployeesUnderManager();
                        break;
                    case "7":
                        ListDetailsOfEmployee();
                        break;
                    case "8":
                        AddDepartmentToEmployeeOrManager();
                        break;
                    case "9":
                        AddManagerToEmployeeOrManager();
                        break;
                    case "10":
                        RemoveManagerFromEmployee();
                        break;
                    case "11":
                        RemoveDepartmentFromEmployeeOrManager();
                        break;
                    case "12":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }

        private static void CreateEmployee()
        {
            Console.Clear();
            Console.WriteLine("Create Employee");
            Console.WriteLine("---------------");
            Console.Write("Enter Employee Name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Email: ");
            var email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            var phone = Console.ReadLine();
            Console.Write("Enter Role: ");
            var role = Console.ReadLine();
            Console.Write("Enter this employee's Manager ID (or leave blank if none): ");
            var managerIdInput = Console.ReadLine();
            int? managerId = string.IsNullOrEmpty(managerIdInput) ? (int?)null : int.Parse(managerIdInput);
            Console.Write("Enter Department ID (or leave blank if none): ");
            var deptIdInput = Console.ReadLine();
            int? deptId = string.IsNullOrEmpty(deptIdInput) ? (int?)null : int.Parse(deptIdInput);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = new SqlCommand("INSERT INTO Employee (empName, email, phone, role) OUTPUT INSERTED.empId VALUES (@name, @Email, @Phone, @Role)", connection, transaction);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Role", role);

                    var empId = (int)command.ExecuteScalar();

                    if (managerId.HasValue)
                    {
                        command = new SqlCommand("INSERT INTO Manager (managerId, empId) VALUES (@ManagerId, @EmpId)", connection, transaction);
                        command.Parameters.AddWithValue("@ManagerId", managerId.Value);
                        command.Parameters.AddWithValue("@EmpId", empId);
                        command.ExecuteNonQuery();
                    }

                    if (deptId.HasValue)
                    {
                        command = new SqlCommand("INSERT INTO EmployeeDepartment (empId, deptId) VALUES (@EmpId, @DeptId)", connection, transaction);
                        command.Parameters.AddWithValue("@EmpId", empId);
                        command.Parameters.AddWithValue("@DeptId", deptId.Value);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine("\nEmployee created successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void CreateDepartment()
        {
            Console.Clear();
            Console.WriteLine("Create Department");
            Console.WriteLine("-----------------");
            Console.Write("Enter Department Name: ");
            var name = Console.ReadLine();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Department (deptName) VALUES (@Name)", connection);
                command.Parameters.AddWithValue("@Name", name);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\nDepartment created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void CreateManager()
        {
            Console.Clear();
            Console.WriteLine("Create Manager");
            Console.WriteLine("--------------");
            Console.Write("Enter Manager Name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Email: ");
            var email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            var phone = Console.ReadLine();
            Console.Write("Enter Role: ");
            var role = Console.ReadLine();
            Console.Write("Enter this manager's Manager ID (or leave blank if none): ");
            var managerIdInput = Console.ReadLine();
            int? managerId = string.IsNullOrEmpty(managerIdInput) ? (int?)null : int.Parse(managerIdInput);
            Console.Write("Enter Department ID (or leave blank if none): ");
            var deptIdInput = Console.ReadLine();
            int? deptId = string.IsNullOrEmpty(deptIdInput) ? (int?)null : int.Parse(deptIdInput);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = new SqlCommand("INSERT INTO Employee (empName, email, phone, role) OUTPUT INSERTED.empId VALUES (@Name, @Email, @Phone, @Role)", connection, transaction);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Role", role);

                    var empId = (int)command.ExecuteScalar();

                    if (managerId.HasValue)
                    {
                        command = new SqlCommand("INSERT INTO Manager (managerId, empId) VALUES (@ManagerId, @EmpId)", connection, transaction);
                        command.Parameters.AddWithValue("@ManagerId", managerId.Value);
                        command.Parameters.AddWithValue("@EmpId", empId);
                        command.ExecuteNonQuery();
                    }

                    if (deptId.HasValue)
                    {
                        command = new SqlCommand("INSERT INTO EmployeeDepartment (empId, deptId) VALUES (@EmpId, @DeptId)", connection, transaction);
                        command.Parameters.AddWithValue("@EmpId", empId);
                        command.Parameters.AddWithValue("@DeptId", deptId.Value);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine("\nManager created successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void ListDepartmentsOfEmployee()
        {
            Console.Clear();
            Console.WriteLine("List Departments of an Employee");
            Console.WriteLine("-------------------------------");
            Console.Write("Enter Employee ID: ");
            var empId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT d.deptName FROM Department d JOIN EmployeeDepartment ed ON d.deptId = ed.deptId WHERE ed.empId = @EmpId", connection);
                command.Parameters.AddWithValue("@EmpId", empId);

                try
                {
                    var reader = command.ExecuteReader();
                    Console.WriteLine("\nDepartments:");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["deptName"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void ListManagersOfEmployee()
        {
            Console.Clear();
            Console.WriteLine("List Managers of an Employee");
            Console.WriteLine("----------------------------");
            Console.Write("Enter Employee ID: ");
            var empId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT e.empName FROM Employee e JOIN Manager m ON e.empId = m.managerId WHERE m.empId = @EmpId", connection);
                command.Parameters.AddWithValue("@EmpId", empId);

                try
                {
                    var reader = command.ExecuteReader();
                    Console.WriteLine("\nManagers:");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["empName"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void ListEmployeesUnderManager()
        {
            Console.Clear();
            Console.WriteLine("List Employees under a Manager");
            Console.WriteLine("------------------------------");
            Console.Write("Enter Manager ID: ");
            var managerId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT e.empName FROM Employee e JOIN Manager m ON e.empId = m.empId WHERE m.managerId = @ManagerId", connection);
                command.Parameters.AddWithValue("@ManagerId", managerId);

                try
                {
                    var reader = command.ExecuteReader();
                    Console.WriteLine("\nEmployees:");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["empName"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void ListDetailsOfEmployee()
        {
            Console.Clear();
            Console.WriteLine("List Details of an Employee");
            Console.WriteLine("---------------------------");
            Console.Write("Enter Employee ID: ");
            var empId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT e.empName, e.email, e.phone, e.role, d.deptName, m.managerId FROM Employee e LEFT JOIN EmployeeDepartment ed ON e.empId = ed.empId LEFT JOIN Department d ON ed.deptId = d.deptId LEFT JOIN Manager m ON e.empId = m.empId WHERE e.empId = @EmpId", connection);
                command.Parameters.AddWithValue("@EmpId", empId);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine($"\nName: {reader["empName"]}");
                        Console.WriteLine($"Email: {reader["email"]}");
                        Console.WriteLine($"Phone: {reader["phone"]}");
                        Console.WriteLine($"Role: {reader["role"]}");
                        Console.WriteLine($"Department: {reader["deptName"]}");
                        Console.WriteLine($"Manager ID: {reader["managerId"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void AddDepartmentToEmployeeOrManager()
        {
            Console.Clear();
            Console.WriteLine("Add Employee/Manager to a Department");
            Console.WriteLine("------------------------------------");
            Console.Write("Enter Employee/Manager ID: ");
            var empId = int.Parse(Console.ReadLine());
            Console.Write("Enter Department ID: ");
            var deptId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO EmployeeDepartment (empId, deptId) VALUES (@EmpId, @DeptId)", connection);
                command.Parameters.AddWithValue("@EmpId", empId);
                command.Parameters.AddWithValue("@DeptId", deptId);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\nEmployee/Manager added to department successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void AddManagerToEmployeeOrManager()
        {
            Console.Clear();
            Console.WriteLine("Add Manager to an Employee/Manager");
            Console.WriteLine("----------------------------------");
            Console.Write("Enter Employee ID: ");
            var empId = int.Parse(Console.ReadLine());
            Console.Write("Enter Manager ID: ");
            var managerId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Manager (managerId, empId) VALUES (@ManagerId, @EmpId)", connection);
                command.Parameters.AddWithValue("@ManagerId", managerId);
                command.Parameters.AddWithValue("@EmpId", empId);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\nManager added to employee/manager successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void RemoveManagerFromEmployee()
        {
            Console.Clear();
            Console.WriteLine("Remove Manager from Employee");
            Console.WriteLine("----------------------------");
            Console.Write("Enter Employee ID: ");
            var empId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Manager WHERE empId = @EmpId", connection);
                command.Parameters.AddWithValue("@EmpId", empId);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\nManager removed from employee successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }

        private static void RemoveDepartmentFromEmployeeOrManager()
        {
            Console.Clear();
            Console.WriteLine("Remove Department from Employee/Manager");
            Console.WriteLine("---------------------------------------");
            Console.Write("Enter Employee ID: ");
            var empId = int.Parse(Console.ReadLine());
            Console.Write("Enter Department ID: ");
            var deptId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM EmployeeDepartment WHERE empId = @EmpId AND deptId = @DeptId", connection);
                command.Parameters.AddWithValue("@EmpId", empId);
                command.Parameters.AddWithValue("@DeptId", deptId);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\nDepartment removed from employee/manager successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }
            }
        }
    }
}

