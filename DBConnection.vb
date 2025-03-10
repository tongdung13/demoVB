Imports MySql.Data.MySqlClient

' Module DBConnection
'     Dim conn As String = "Server=localhost;Database=testdb;User Id=root;Password=10Tongdung@10;"

'     Public Sub OpenConnection()
'         Try
'             If conn Is Nothing Then
'                 conn = New MySqlConnection("Server=localhost;Database=testdb;User Id=root;Password=10Tongdung@10;")
'             End If

'             If conn.State = ConnectionState.Closed Then
'                 conn.Open()
'             End If
'         Catch ex As Exception
'             MessageBox.Show("Lỗi kết nối MySQL: " & ex.Message)
'         End Try
'     End Sub

    
'     Public Sub CloseConnection()
'         Try
'             If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
'                 conn.Close()
'             End If
'         Catch ex As Exception
'             MessageBox.Show("Lỗi đóng kết nối: " & ex.Message)
'         End Try
'     End Sub

' End Module
