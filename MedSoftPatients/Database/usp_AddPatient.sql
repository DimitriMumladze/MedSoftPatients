USE MedSoftPatients;
GO

CREATE OR ALTER PROCEDURE dbo.usp_AddPatient
    @FullName NVARCHAR(200),
    @Dob      DATE,
    @GenderID INT,
    @Phone    VARCHAR(9)    = NULL,
    @Address  NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO dbo.Patients (FullName, Dob, GenderID, Phone, Address)
        VALUES (@FullName, @Dob, @GenderID, @Phone, @Address);

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END