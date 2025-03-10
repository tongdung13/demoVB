Imports System.Data
' Imports System.Windows.Forms

Public Class UserModel
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String

    ' ' Constructor lấy dữ liệu từ form
    ' Public Sub New(form As Form)
    '     Me.Name = GetTextBoxValue(form, "txtName")
    '     Me.Email = GetTextBoxValue(form, "txtEmail")
    '     Me.Phone = GetTextBoxValue(form, "txtPhone")
    ' End Sub

    ' ' Hàm lấy giá trị từ TextBox trong Form
    ' Private Function GetTextBoxValue(form As Form, fieldName As String) As String
    '     Dim control As Control = form.Controls(fieldName)
    '     If control Is Nothing OrElse Not (TypeOf control Is TextBox) Then
    '         Return "" ' Tránh lỗi nếu TextBox không tồn tại
    '     End If
    '     Return DirectCast(control, TextBox).Text
    ' End Function

    ' ' Trả về danh sách các cột trong UserModel
    ' Public Shared Function GetColumns() As String()
    '     Return GetType(UserModel).GetProperties().Select(Function(p) p.Name.ToLower()).ToArray()
    ' End Function
    
End Class


