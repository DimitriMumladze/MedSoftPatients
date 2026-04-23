Imports System.Configuration
Imports System.Data.SqlClient

Public Class DbConnectionFactory
    Private Shared ReadOnly ConnectionString As String =
        ConfigurationManager.ConnectionStrings("MedSoftDb").ConnectionString

    Public Shared Function CreateConnection() As SqlConnection
        Return New SqlConnection(ConnectionString)
    End Function

End Class