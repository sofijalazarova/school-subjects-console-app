# School Subjects System - Console App

A simple console application to manage school subjects developed in C#.

## How to start this application

### 1. Clone this repository
```bash
git clone https://github.com/sofijalazarova/school-subjects-console-app.git
cd school-subjects-console-app
```
### 2. Configure the `.env` file
In the root directory of the project, create a `.env` file with the following content or make a copy of the .env.example file:
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
### 4. Populate the Database with Sample Data
```sql
INSERT INTO public.subjects VALUES 
  (1, 'Mathematics', 'Fundamentals of algebra, geometry, and calculus.', 4),
  (2, 'Computer Science', 'Introduction to programming, algorithms, and data structures.', 3),
  (3, 'Physics', 'Basic concepts of motion, force, and energy.', 3);

INSERT INTO public.literature VALUES 
  (1, 1, 'Basic Mathematics for Beginners', 'Emily Carter', 2018,
   'Mathematics is the study of numbers, shapes, patterns, and logical reasoning. This book serves as a foundational guide for learners who are beginning their journey into math. It starts with basic arithmetic operations such as addition, subtraction, multiplication, and division, followed by an exploration of fractions, decimals, and percentages. Students will also be introduced to geometric concepts like points, lines, angles, triangles, and circles. Algebraic thinking is developed through simple equations and the use of variables. Data handling topics such as bar graphs, tables, and basic probability are included to enhance analytical skills. With clear explanations, step-by-step examples, and plenty of exercises, this book aims to build critical thinking and practical problem-solving abilities essential for academic and everyday life.'),
  
  (2, 2, 'Introduction to Programming with C#', 'Michael Varela', 2019,
   'This book introduces the fundamental concepts of computer programming using the C# programming language. It is designed for beginners with little or no prior experience in coding. Topics include variables, data types, operators, conditional statements, loops, and arrays. Students will learn how to write clean, readable code and understand how a program executes step by step. The book also covers methods, object-oriented programming concepts such as classes, objects, inheritance, and encapsulation. Real-world examples and hands-on coding challenges are provided in each chapter to reinforce learning. The goal is to help readers develop a strong programming mindset and prepare them for advanced software development topics.'),
  
  (3, 3, 'Foundations of Physics: Motion and Energy', 'Dr. Laura Kim', 2017,
   'Physics helps us understand how the universe works by exploring matter, energy, and the forces that interact with them. This book introduces students to fundamental physics concepts including motion, velocity, acceleration, and Newton’s laws of motion. It explains the relationship between force, mass, and acceleration, and introduces simple machines such as levers and pulleys. Energy topics include kinetic and potential energy, conservation of energy, and basic thermodynamics. Diagrams, real-world examples, and experiments help illustrate abstract concepts. Each chapter includes conceptual questions and problem-solving exercises to promote deep understanding. This book lays a strong foundation for students pursuing further studies in science and engineering.');


-- Fix sequences for SERIAL columns
SELECT setval(pg_get_serial_sequence('subjects', 'id'), (SELECT MAX(id) FROM subjects));
SELECT setval(pg_get_serial_sequence('literature', 'id'), (SELECT MAX(id) FROM literature));
```



### 5. Text Summarization with Ollama
This project includes a service that allows text summarization using a locally running Ollama LLM server.
#### Prerequisites
1.  **Install Ollama (https://ollama.com/download)**
```bash
curl -fsSL https://ollama.com/install.sh | sh
```
3.  **Run this command in the terminal**
```bash
ollama run llama3.2:1b
```

### 6. Run the application
Run the app from the root directory using:
```bash
dotnet run
```

