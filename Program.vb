Imports System
Imports MySql.Data.MySqlClient

Module Program
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

            Dim columns As String() = UserModel.GetColumns()
            Dim columnList As String = String.Join(", ", columns)
            Dim valueLists As String = String.Join(", ", columns.Select(Function(c) "@" & c))
            Dim query As String = $"INSERT INTO users ({columnList}) VALUES ({valueLists})"
            Dim now As DateTime = DateTime.Now()
            Using cmd As New MySqlCommand(query, conn)
                Dim user As New UserModel()
                Dim validator As New UserRequest()
                Dim errors As List(Of String) = validator.Validate(user)
                If errors.Count > 0 Then
                    Console.WriteLine("Errors:")
                    For Each mesErr In errors 
                        Console.WriteLine(mesErr)
                    Next
                    Exit Sub
                End If

                cmd.Parameters.AddWithValue("@name", user.name)
                cmd.Parameters.AddWithValue("@email", user.email)
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
        Try
            OpenConnection()
            If Not IsConnectionOpen() Then
                Console.WriteLine("Không thể kết nối đến cơ sở dữ liệu.")
                Return
            End If
            Dim query As String = "SELECT * FROM users"
            Using cmd As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Console.WriteLine($"ID: {reader("id")}, Name: {reader("name")}, Email: {reader("email")}")
                    End While
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        Finally
            CloseConnection()
        End Try
    End Sub

    Sub UpdateData()
        OpenConnection()
        If Not IsConnectionOpen() Then
            Console.WriteLine("Không thể kết nối đến cơ sở dữ liệu.")
            Return
        End If
        Try
            Dim columns As String() = UserModel.GetColumns(True)
            Dim columnList As String = String.Join(", ", columns.Select(Function(c) c & " = @" & c))
            Dim query As String = "UPDATE users SET " & columnList & " WHERE id = @id"
            Using cmd As New MySqlCommand(query, conn)
                Dim now As Datetime = Datetime.now()
                Console.Write("Enter ID to update: ")
                Dim userId = Convert.ToInt32(Console.ReadLine())
                Dim user As New UserModel()
                Dim validator As New UserRequest()
                Dim errors As List(Of String) = validator.Validate(user, userId)
                If errors.Count > 0 Then
                    Console.WriteLine("Errors:")
                    For Each mesErr In errors 
                        Console.WriteLine(mesErr)
                    Next
                    Exit Sub
                End If
                cmd.Parameters.AddWithValue("@id", userId)
                cmd.Parameters.AddWithValue("@name", user.name)
                cmd.Parameters.AddWithValue("@email", user.email)
                cmd.Parameters.AddWithValue("@updated_at", now)
                cmd.ExecuteNonQuery()
                Console.WriteLine("Data updated successfully!")
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        Finally
            CloseConnection()
        End Try
    End Sub

    Sub DeleteData()
        OpenConnection()
        If Not IsConnectionOpen() Then
            Console.WriteLine("Không thể kết nối đến cơ sở dữ liệu.")
            Return
        End If
        Try
            Dim query As String = "DELETE FROM users WHERE id = @id"
            Using cmd As New MySqlCommand(query, conn)
                Console.Write("Enter ID to delete: ")
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Console.ReadLine()))
                cmd.ExecuteNonQuery()
                Console.WriteLine("Data deleted successfully!")
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        Finally
            CloseConnection()
        End Try
    End Sub

    Sub SearchData()
        OpenConnection()
        If Not IsConnectionOpen() Then
            Console.WriteLine("Không thể kết nối đến cơ sở dữ liệu.")
            Return
        End If
        Try
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
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        Finally
            CloseConnection()
        End Try
    End Sub
End Module
