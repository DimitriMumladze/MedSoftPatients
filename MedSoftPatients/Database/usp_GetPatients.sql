USE MedSoftPatients;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetPatients
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.ID,
        p.FullName,
        p.Dob,
        p.GenderID,
        g.GenderName,
        p.Phone,
        p.Address
    FROM dbo.Patients AS p
    INNER JOIN dbo.Gender AS g 
        ON p.GenderID = g.GenderID
    ORDER BY p.ID;
END