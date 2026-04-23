USE MedSoftPatients;
GO

CREATE OR ALTER PROCEDURE dbo.usp_DeletePatient
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE FROM dbo.Patients
        WHERE ID = @ID;

        SELECT @@ROWCOUNT AS RowsAffected;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END