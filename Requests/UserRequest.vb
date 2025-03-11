Imports System.Data
Imports MySql.Data.MySqlClient

Public Class UserRequest 
    ' Hàm kiểm tra email có tồn tại trong DB hay không
    Private Function IsEmailExists(email As String, Optional userId As Integer = 0) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM Users WHERE email = @Email"
        If userId > 0 Then
            ' Nếu đang edit, loại trừ chính user đó
            query &= " AND id <> @UserId"
        End If

        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Email", email)
            If userId > 0 Then cmd.Parameters.AddWithValue("@UserId", userId)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0 ' Trả về True nếu email đã tồn tại
        End Using
    End Function

    Public Function Validate(user As UserModel, Optional userId As Integer = 0) As List(Of String)
        Dim arrError As New List(Of String)
        
        ' Dim phonePattern As String = "^(0[35789])[0-9]{8}$"
        ' If Not System.Text.RegularExpressions.Regex.IsMatch(name, phonePattern) Then
        '     arrError.Add("Số điện thoại không hợp lệ! Số phải có 10 chữ số và bắt đầu bằng 03, 05, 07, 08, 09.")
        ' End If

        If String.IsNullOrWhiteSpace(user.name) Then
            arrError.Add("Tên không được để trống!")
        End If
        
        Dim emailPattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        If Not System.Text.RegularExpressions.Regex.IsMatch(user.email, emailPattern) Then
            arrError.Add("Email không hợp lệ! Email phải có dạng: user@domain.com.")
        End If
        
        ' Kiểm tra email unique
        If IsEmailExists(user.email, userId) Then
            arrError.Add("Email đã tồn tại! Vui lòng chọn email khác.")
        End If
        
        Return arrError
    End Function
End Class

