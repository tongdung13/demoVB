Imports MySql.Data.MySqlClient
Imports System.Data

Module DBConnection
    Public conn As MySqlConnection = New MySqlConnection("server=localhost;port=3306;database=testdb;uid=root;password=10Tongdung@10;")

    Public Sub OpenConnection()
        Try
            If conn Is Nothing Then
                conn = New MySqlConnection("server=localhost;port=3306;database=testdb;uid=root;password=10Tongdung@10;")
            End If

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            Console.WriteLine("Lỗi kết nối MySQL: " & ex.Message)
        End Try
    End Sub

    
    Public Sub CloseConnection()
        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch ex As Exception
            Console.WriteLine("Lỗi đóng kết nối: " & ex.Message)
        End Try
    End Sub

    Public Function IsConnectionOpen() As Boolean
        Return conn IsNot Nothing AndAlso conn.State = ConnectionState.Open
    End Function
End Module
