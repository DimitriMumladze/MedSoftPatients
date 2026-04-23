USE MedSoftPatients;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetGenders
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        GenderID,
        GenderName
    FROM dbo.Gender
    ORDER BY GenderID;
END