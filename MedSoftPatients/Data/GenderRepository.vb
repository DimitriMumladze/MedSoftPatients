Imports System.Data
Imports System.Data.SqlClient

Public Class GenderRepository
    Public Shared Function GetAll() As List(Of Gender)
        Dim result As New List(Of Gender)

        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand("dbo.usp_GetGenders", conn)
                cmd.CommandType = CommandType.StoredProcedure

                conn.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim g As New Gender() With {
                            .GenderId = reader.GetInt32(reader.GetOrdinal("GenderID")),
                            .GenderName = reader.GetString(reader.GetOrdinal("GenderName"))
                        }
                        result.Add(g)
                    End While
                End Using
            End Using
        End Using

        Return result
    End Function

End Class