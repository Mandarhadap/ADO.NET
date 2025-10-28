using Microsoft.Data.SqlClient;


using var connection = new SqlConnection("Data Source=iitdac.met.edu;Database=Shop2;USer Id=Dac2;Password=Dac2@1234;Trust Server Certificate = True");
connection.Open();


while (true)
{
    Console.WriteLine("\n--- EMP Management System Menu ---");
    Console.WriteLine("1. Show All Employees");
    Console.WriteLine("2. Add New Employee");
    Console.WriteLine("3. Exit");
    Console.Write("Enter your choice (1-3): ");
    string choice = Console.ReadLine();

    
    switch (choice)
    {
        case "1":
            ShowAllEmployees(connection);
            break;
        case "2":
            AddNewEmployee(connection);
            break;
        case "3":
            Console.WriteLine("Exiting program.");
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}


void ShowAllEmployees(SqlConnection conn)
{
    Console.WriteLine("\n--- All Employees ---");
    
    using var command = conn.CreateCommand();
    command.CommandText = "SELECT EMPNO, ENAME, DEPTNO, SAL, COMM FROM Rno26_Emp";
    
    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDecimal(3), reader.GetDecimal(4));
    }
}


void AddNewEmployee(SqlConnection conn)
{
    try
    {
        Console.WriteLine("\n--- Add New Employee ---");

        Console.Write("Enter Employee Number: ");
        int empNo = int.Parse(Console.ReadLine());

        Console.Write("Enter Employee Name: ");
        string ename = Console.ReadLine();

        Console.Write("Enter Department Number: ");
        int deptNo = int.Parse(Console.ReadLine());

        Console.Write("Enter Salary: ");
        decimal sal = decimal.Parse(Console.ReadLine());

        Console.Write("Enter Commission: ");
        decimal comm = decimal.Parse(Console.ReadLine());

        
        using var command = conn.CreateCommand();
        command.CommandText = "INSERT INTO Rno26_Emp (EMPNO, ENAME, DEPTNO, SAL, COMM) VALUES (@EmpNo, @Ename, @DeptNo, @Sal, @Comm)";
        
        command.Parameters.AddWithValue("@EmpNo", empNo);
        command.Parameters.AddWithValue("@Ename", ename);
        command.Parameters.AddWithValue("@DeptNo", deptNo);
        command.Parameters.AddWithValue("@Sal", sal);
        command.Parameters.AddWithValue("@Comm", comm);

        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine($"\nSuccessfully inserted {rowsAffected} row.");
    }
    catch (Exception ex)
    {
    
        Console.WriteLine($"\nError adding employee: {ex.Message}");
    }
}