Public Class Gender
    Public Property GenderId As Integer
    Public Property GenderName As String

    Public Overrides Function ToString() As String
        Return GenderName
    End Function

End Class
