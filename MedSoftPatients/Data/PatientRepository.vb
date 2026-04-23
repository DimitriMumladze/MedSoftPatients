Imports System.Data
Imports System.Data.SqlClient

Public Class PatientRepository
    Public Shared Function GetAll() As List(Of Patient)
        Dim result As New List(Of Patient)

        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand("dbo.usp_GetPatients", conn)
                cmd.CommandType = CommandType.StoredProcedure

                conn.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        result.Add(MapFromReader(reader))
                    End While
                End Using
            End Using
        End Using

        Return result
    End Function

    Public Shared Function Add(patient As Patient) As Integer
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand("dbo.usp_AddPatient", conn)
                cmd.CommandType = CommandType.StoredProcedure

                AddParameters(cmd, patient, includeId:=False)

                conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Shared Sub Update(patient As Patient)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand("dbo.usp_UpdatePatient", conn)
                cmd.CommandType = CommandType.StoredProcedure

                AddParameters(cmd, patient, includeId:=True)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub Delete(id As Integer)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand("dbo.usp_DeletePatient", conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Function MapFromReader(reader As SqlDataReader) As Patient
        Dim p As New Patient() With {
            .ID = reader.GetInt32(reader.GetOrdinal("ID")),
            .FullName = reader.GetString(reader.GetOrdinal("FullName")),
            .Dob = reader.GetDateTime(reader.GetOrdinal("Dob")),
            .GenderID = reader.GetInt32(reader.GetOrdinal("GenderID")),
            .GenderName = reader.GetString(reader.GetOrdinal("GenderName"))
        }

        ' Phone და Address nullable-ია DB-ში
        Dim phoneIdx As Integer = reader.GetOrdinal("Phone")
        If Not reader.IsDBNull(phoneIdx) Then
            p.Phone = reader.GetString(phoneIdx)
        End If

        Dim addressIdx As Integer = reader.GetOrdinal("Address")
        If Not reader.IsDBNull(addressIdx) Then
            p.Address = reader.GetString(addressIdx)
        End If

        Return p
    End Function

    Private Shared Sub AddParameters(cmd As SqlCommand, patient As Patient, includeId As Boolean)
        If includeId Then
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = patient.ID
        End If

        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 200).Value = patient.FullName
        cmd.Parameters.Add("@Dob", SqlDbType.Date).Value = patient.Dob
        cmd.Parameters.Add("@GenderID", SqlDbType.Int).Value = patient.GenderID
        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 9).Value = ToDbValue(patient.Phone)
        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = ToDbValue(patient.Address)
    End Sub

    Private Shared Function ToDbValue(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If
        Return value
    End Function

End Class