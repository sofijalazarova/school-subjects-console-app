# School Subjects System - Console App

A simple console application to manage school subjects developed in C#.

## How to start this application

### 1. Clone this repository
```bash
git clone https://github.com/sofijalazarova/school-subjects-console-app.git
cd school-subjects-console-app
```
### 2. Configure the `.env` file
In the root directory of the project, create a `.env` file with the following content:
```env
DB_CONNECTION_STRING=Host=localhost;Port=5432;Username=postgres;Password=YOUR_PASSWORD;Database=schoolsystemDb
```
### 3. Create the database and tables
Connect to PostgreSQL (via `psql`, `pgAdmin`, or another tool) and run the following SQL statements:
```sql
CREATE TABLE Subjects (
    Id SERIAL PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT NOT NULL,
    WeeklyClasses INTEGER NOT NULL
);

CREATE TABLE Literature (
    Id SERIAL PRIMARY KEY,
    SubjectId INTEGER NOT NULL,
    Title TEXT NOT NULL,
    Author TEXT NOT NULL,
    ReleaseYear INTEGER NOT NULL,
    Content TEXT NOT NULL,
    CONSTRAINT fk_subject FOREIGN KEY (SubjectId)
        REFERENCES Subjects(Id)
        ON DELETE CASCADE
);

```
### 4. Run the application
Run the app from the root directory using:
```bash
dotnet run
