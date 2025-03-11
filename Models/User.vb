Imports System.Data
' Imports System.Windows.Forms

Public Class UserModel
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property CreatedAt As DateTime
    Public Property UpdatedAt As DateTime

    ' Constructor lấy dữ liệu từ Console
    Public Sub New()
        Console.Write("Enter Name: ")
        Me.Name = Console.ReadLine()

        Console.Write("Enter Email: ")
        Me.Email = Console.ReadLine()

        ' Console.Write("Nhập số điện thoại: ")
        ' Me.Phone = Console.ReadLine()

        Me.CreatedAt = DateTime.Now
        Me.UpdatedAt = DateTime.Now
    End Sub

    ' Hàm trả về danh sách cột (thuộc tính) của UserModel
    Public Shared Function GetColumns(Optional isUpdate As Boolean = False) As String()
        Dim columns As String() = {"name", "email", "created_at", "updated_at"}
        
        ' Nếu là update thì bỏ created_at
        If isUpdate Then
            columns = columns.Where(Function(c) c <> "created_at").ToArray()
        End If
        
        Return columns
    End Function

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


