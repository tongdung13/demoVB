Imports System
Imports MySql.Data.MySqlClient

Module Program
    Dim connectionString As String = "Server=localhost;Database=testdb;User Id=root;Password=10Tongdung@10;"

    Sub Main()
        Dim choice As Integer

        Do
            Console.WriteLine("1. Insert")
            Console.WriteLine("2. Read")
            Console.WriteLine("3. Update")
            Console.WriteLine("4. Delete")
            Console.WriteLine("5. Search")
            Console.WriteLine("6. Exit")
            Console.Write("Choose an option: ")
            choice = Convert.ToInt32(Console.ReadLine())

            Select Case choice
                Case 1
                    InsertData()
                Case 2
                    ReadData()
                Case 3
                    UpdateData()
                Case 4
                    DeleteData()
                Case 5
                    SearchData()
                Case 6
                    Exit Do
                Case Else
                    Console.WriteLine("Invalid option!")
            End Select
        Loop
    End Sub

    Sub InsertData()
        Try
            OpenConnection()
            If Not IsConnectionOpen() Then
                Console.WriteLine("Không thể kết nối đến cơ sở dữ liệu.")
                Return
            End If

            Dim columns As String() = {"name", "email", "created_at", "updated_at"}
            Dim columnList As String = String.Join(", ", columns)
            Dim valueLists As String = String.Join(", ", columns.Select(Function(c) "@" & c))
            Dim query As String = $"INSERT INTO users ({columnList}) VALUES ({valueLists})"
            Dim now As DateTime = DateTime.Now()
            Using cmd As New MySqlCommand(query, conn)
                Console.Write("Enter Name: ")
                cmd.Parameters.AddWithValue("@name", Console.ReadLine())
                Console.Write("Enter Email: ")
                cmd.Parameters.AddWithValue("@email", Console.ReadLine())
                cmd.Parameters.AddWithValue("@created_at", now)
                cmd.Parameters.AddWithValue("@updated_at", now)
                cmd.ExecuteNonQuery()
                Console.WriteLine("Data inserted successfully!")
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        Finally
            CloseConnection()
        End Try
    End Sub

    Sub ReadData()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM users"
            Using cmd As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Console.WriteLine($"ID: {reader("id")}, Name: {reader("name")}, Email: {reader("email")}")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Sub UpdateData()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "UPDATE users SET name = @name, email = @email WHERE id = @id"
            Using cmd As New MySqlCommand(query, conn)
                Dim now As Datetime = Datetime.now()
                Console.Write("Enter ID to update: ")
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Console.ReadLine()))
                Console.Write("Enter New Name: ")
                cmd.Parameters.AddWithValue("@name", Console.ReadLine())
                Console.Write("Enter New Email: ")
                cmd.Parameters.AddWithValue("@email", Console.ReadLine())
                cmd.Parameters.AddWithValue("@updated_at", now)
                cmd.ExecuteNonQuery()
                Console.WriteLine("Data updated successfully!")
            End Using
        End Using
    End Sub

    Sub DeleteData()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "DELETE FROM users WHERE id = @id"
            Using cmd As New MySqlCommand(query, conn)
                Console.Write("Enter ID to delete: ")
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Console.ReadLine()))
                cmd.ExecuteNonQuery()
                Console.WriteLine("Data deleted successfully!")
            End Using
        End Using
    End Sub

    Sub SearchData()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM Users"
            Console.Write("Enter to search: ")
            Dim searchTerm As String = Console.ReadLine()
            Dim conditions As New List(Of String)
            conditions.Add($"name LIKE '%{searchTerm}%'")
            conditions.Add($"email LIKE '%{searchTerm}%'")
            query += " WHERE " + String.Join(" OR ", conditions)
            Using cmd As New MySqlCommand(query, conn)
                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    cmd.Parameters.AddWithValue("@name", "%" & searchTerm & "%")
                    cmd.Parameters.AddWithValue("@email", "%" & searchTerm & "%")
                End If
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Console.WriteLine($"ID: {reader("id")}, Name: {reader("name")}, Email: {reader("email")}")
                    End While
                End Using
            End Using
        End Using
    End Sub
End Module
