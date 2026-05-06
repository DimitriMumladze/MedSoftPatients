USE MedSoftPatients;
GO

INSERT INTO dbo.Patients (PersonalNumber, FullName, Dob, GenderID, Phone, Address)
SELECT '01001012345', N'გიორგი მაისურაძე',     '1985-04-12', 1, '599123456', N'თბილისი, ვაჟა-ფშაველას გამზ. 12'
WHERE NOT EXISTS (SELECT 1 FROM dbo.Patients WHERE PersonalNumber = '01001012345');

INSERT INTO dbo.Patients (PersonalNumber, FullName, Dob, GenderID, Phone, Address)
SELECT '01001023456', N'ნინო ბერიძე',           '1992-07-25', 2, '577234567', N'ბათუმი, რუსთაველის ქ. 45'
WHERE NOT EXISTS (SELECT 1 FROM dbo.Patients WHERE PersonalNumber = '01001023456');

INSERT INTO dbo.Patients (PersonalNumber, FullName, Dob, GenderID, Phone, Address)
SELECT '01001034567', N'დავით კვარაცხელია',     '1978-11-03', 1, '595345678', NULL
WHERE NOT EXISTS (SELECT 1 FROM dbo.Patients WHERE PersonalNumber = '01001034567');

INSERT INTO dbo.Patients (PersonalNumber, FullName, Dob, GenderID, Phone, Address)
SELECT '01001045678', N'ანა ლომიძე',             '2000-02-19', 2, NULL,        N'ქუთაისი, თამარ მეფის ქ. 8'
WHERE NOT EXISTS (SELECT 1 FROM dbo.Patients WHERE PersonalNumber = '01001045678');

INSERT INTO dbo.Patients (PersonalNumber, FullName, Dob, GenderID, Phone, Address)
SELECT '01001056789', N'ლევან ჯავახიშვილი',     '1965-09-30', 1, '551456789', N'თბილისი, აღმაშენებლის გამზ. 102'
WHERE NOT EXISTS (SELECT 1 FROM dbo.Patients WHERE PersonalNumber = '01001056789');
GO

-- შემოწმება
SELECT ID, PersonalNumber, FullName, Dob, GenderID, Phone, Address
FROM dbo.Patients
ORDER BY ID;
