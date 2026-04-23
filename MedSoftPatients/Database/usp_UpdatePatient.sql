USE MedSoftPatients;
GO

CREATE OR ALTER PROCEDURE dbo.usp_UpdatePatient
    @ID       INT,
    @FullName NVARCHAR(200),
    @Dob      DATE,
    @GenderID INT,
    @Phone    VARCHAR(9)    = NULL,
    @Address  NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE dbo.Patients
        SET 
            FullName = @FullName,
            Dob      = @Dob,
            GenderID = @GenderID,
            Phone    = @Phone,
            Address  = @Address
        WHERE ID = @ID;

        SELECT @@ROWCOUNT AS RowsAffected;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END