USE MedSoftPatients;
GO

CREATE TABLE dbo.Gender (
    GenderID    INT IDENTITY(1,1) PRIMARY KEY,
    GenderName  NVARCHAR(30) NOT NULL
);

CREATE TABLE dbo.Patients (
    ID          INT IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(200) NOT NULL,
    Dob         DATE          NOT NULL,
    GenderID    INT           NOT NULL,
    Phone       VARCHAR(9)    NULL,
    Address     NVARCHAR(500) NULL,

    CONSTRAINT FK_Patients_Gender 
        FOREIGN KEY (GenderID) REFERENCES dbo.Gender(GenderID)
);