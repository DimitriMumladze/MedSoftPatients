# MedSoft.GE — პაციენტების მართვის სისტემა (N1)

კლინიკის მართვის საინფორმაციო სისტემის პირველი ეტაპი — WinForms აპლიკაცია პაციენტების CRUD ოპერაციებისთვის.

## ტექნოლოგიები

- **VB.NET** (.NET Framework 4.8)
- **WinForms** — UI
- **Microsoft SQL Server** — მონაცემთა ბაზა
- **Stored Procedures** — ყველა DB ოპერაცია (inline SQL არ გამოიყენება)
- **ADO.NET** (`System.Data.SqlClient`) — DB-თან დაკავშირება

## არქიტექტურა

სამშრიანი (3-Layer) არქიტექტურა — თითოეული შრე გამოყოფილია საკუთარ ფოლდერში და აქვს მხოლოდ ერთი პასუხისმგებლობა.

```
┌─────────────────────────────────────┐
│  Presentation Layer (Forms/)        │  ← UI, user events, validation
│  - FrmMain, FrmPatientEdit          │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│  Data Access Layer (Data/)          │  ← DB ოპერაციები Repository pattern-ით
│  - DbConnectionFactory              │
│  - PatientRepository                │
│  - GenderRepository                 │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│  Database (Database/)               │  ← Schema + Stored Procedures
│  - dbo.Patients, dbo.Gender         │
│  - 5 Stored Procedures              │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│  Models (Models/)                   │  ← Entity classes (POCO)
│  - Patient, Gender                  │
└─────────────────────────────────────┘
```

### შრეების აღწერა

**1. Models** — მონაცემთა ობიექტები. მხოლოდ properties, ლოგიკის გარეშე.
- `Patient` — ID, FullName, Dob, GenderID, GenderName, Phone, Address
- `Gender` — GenderId, GenderName

**2. Data** — DB-თან კომუნიკაცია. UI არაფერი იცის SQL-ის შესახებ.
- `DbConnectionFactory` — connection-ის შექმნის ცენტრალიზებული წერტილი (Connection string `App.config`-დან)
- `PatientRepository` — `GetAll`, `Add`, `Update`, `Delete`
- `GenderRepository` — `GetAll`

**3. Forms** — UI. იძახებს მხოლოდ Repository-ებს.
- `FrmMain` — ძირითადი ფანჯარა ცხრილით და 3 ღილაკით
- `FrmPatientEdit` — დამატება/რედაქტირების modal ფორმა (ორი რეჟიმი)

**4. Database** — SQL სკრიპტები (Schema + 5 Stored Procedures).

## რა გაკეთდა

### ძირითადი ფორმა (FrmMain)
- `DataGridView`-ში ჩამონათვალი: ID, პაციენტის გვარი სახელი, დაბ. თარიღი, სქესი, მობ. ნომერი, მისამართი
- 3 ღილაკი: ➕ დამატება, ✏️ რედაქტირება, ✖️ წაშლა
- ტელეფონი ფორმატდება `598-130-150` სახით (`CellFormatting` event)
- თარიღი `dd.MM.yyyy` ფორმატით
- Row-ზე double-click = რედაქტირება (UX shortcut)

### დამატება/რედაქტირების ფორმა (FrmPatientEdit)
- ველები: სახელი, გვარი, დაბ. თარიღი (`DateTimePicker`), სქესი (`ComboBox`), მობ. ნომერი, მისამართი (multiline)
- ორი რეჟიმი ერთ ფორმაში (constructor overloading):
  - `New()` — Add რეჟიმი (ცარიელი ფორმა)
  - `New(patient As Patient)` — Edit რეჟიმი (შევსებული ფორმა)
- Load დროს: Gender-ების ჩატვირთვა DB-დან, `dtpDob.MaxDate = Today`

### ვალიდაცია
- **სავალდებულო ველები**: სახელი, გვარი, სქესი (თარიღი — implicit, `DateTimePicker`-ს ყოველთვის აქვს value)
- **ტელეფონი** (თუ შევსებულია): უნდა იწყებოდეს "5"-ით და იყოს ზუსტად 9 ციფრი
- ვალიდაციის შეცდომისას — `MessageBox` + focus ცუდ ველზე

### წაშლა
- Confirmation dialog "გსურთ მონიშნული ჩანაწერის წაშლა?" Yes/No
- Default button = **No** (უსაფრთხოება — Enter-ზე შემთხვევით არ წაიშლება)

### Database
- 2 ცხრილი: `dbo.Patients`, `dbo.Gender` (FK constraint-ით)
- 5 Stored Procedure: `usp_GetPatients`, `usp_GetGenders`, `usp_AddPatient`, `usp_UpdatePatient`, `usp_DeletePatient`
- Seed data-ში 2 სქესი: მამრობითი, მდედრობითი

## მნიშვნელოვანი დეტალები

### 1. Stored Procedures ყველგან
**inline SQL არ გამოიყენება**. ყველა DB ოპერაცია გადის Stored Procedure-ზე parameterized way-ით — **SQL Injection-სგან დაცვა** by design.

### 2. Repository Pattern
UI კოდი პირდაპირ `SqlConnection`-ს არ იცნობს. `FrmMain`/`FrmPatientEdit` მხოლოდ `PatientRepository.GetAll()`, `PatientRepository.Add()` და ა.შ. იძახებს. თუ ხვალ DB შეიცვლება — UI-ს ხელი არ ხლებია.

### 3. `Using` ყველა `SqlConnection`-ზე
ყოველი DB connection/command `Using`-ში არის — **resource leak-ის თავიდან აცილება**, თუ exception მოხდება.

### 4. Connection String `App.config`-ში
`App.config` **git-ში არ ატვირთულია** (`.gitignore`-ში) — connection string სენსიტიურია. პროექტში არის `App.config.example` — ვინც კლონს, მან უნდა დაიმატოს თავისი `App.config` ამ template-ის მიხედვით.

### 5. ქართული ტექსტი
`Sylfaen` font-ი დაყენებული Form-ებზე — child controls მემკვიდრეობით იღებენ. ქართული ტექსტი სწორად იკითხება ყველა Label-ში, MessageBox-ში და გრიდში.

### 6. Modal Dialog + DialogResult
`FrmPatientEdit` იხსნება `ShowDialog()`-ით (modal). `DialogResult.OK` → FrmMain აახლებს ცხრილს, `Cancel` → არაფერი ხდება. ცხრილი ზედმეტად არ იტვირთება.

### 7. `TryCast` Safe-ობა
`dgvPatients.CurrentRow.DataBoundItem` → `TryCast(..., Patient)` — თუ row არჩეული არაა, `Nothing` ბრუნდება, `NullReferenceException` არ ხდება.

### 8. Nullable DB ველები
`Phone` და `Address` DB-ში `NULL`-ია. Repository-ში `IsDBNull` check-ი გვაქვს წაკითხვისას და `DBNull.Value`-ად გარდაიქმნება ცარიელი string-ი შენახვისას.

## პროექტის სტრუქტურა

```
MedSoftPatients/
├── Database/
│   ├── 01_Schema.sql           ← ცხრილების შექმნა
│   ├── 02_SeedData.sql         ← საწყისი მონაცემები (Gender)
│   ├── usp_GetPatients.sql
│   ├── usp_GetGenders.sql
│   ├── usp_AddPatient.sql
│   ├── usp_UpdatePatient.sql
│   └── usp_DeletePatient.sql
├── Models/
│   ├── Patient.vb
│   └── Gender.vb
├── Data/
│   ├── DbConnectionFactory.vb
│   ├── PatientRepository.vb
│   └── GenderRepository.vb
├── Forms/
│   ├── FrmMain.vb (+ Designer + resx)
│   └── FrmPatientEdit.vb (+ Designer + resx)
├── App.config.example          ← template (App.config .gitignore-ში)
└── MedSoftPatients.vbproj
```

## Setup ინსტრუქცია

1. **SQL Server**-ზე შექმენი DB სახელად `MedSoftPatients`
2. გაუშვი `Database/` ფოლდერში არსებული სკრიპტები ამ რიგით:
   - `01_Schema.sql`
   - `02_SeedData.sql`
   - 5 `usp_*.sql` ფაილი
3. დააკოპირე `App.config.example` → `App.config` და შეცვალე connection string შენი SQL Server instance-ისთვის
4. გახსენი Visual Studio-ში და გაუშვი (F5)
