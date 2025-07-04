-- Get all employees with their positions and departments
SELECT 
    e.FirstName,
    e.LastName,
    e.Email,
    e.DateOfBirth,
    e.HireDate,
    e.Salary,
    p.Name AS Position,
    p.Department
FROM Employees e
INNER JOIN Positions p ON e.PositionId = p.Id
ORDER BY e.LastName, e.FirstName;

-- Get employees with salary greater than 10000
SELECT 
    e.FirstName,
    e.LastName,
    e.Email,
    e.Salary,
    p.Name AS Position,
    p.Department
FROM Employees e
INNER JOIN Positions p ON e.PositionId = p.Id
WHERE e.Salary > 10000
ORDER BY e.Salary DESC;

-- Delete employees older than 70 years
DELETE FROM Employees
WHERE DateOfBirth <= CURRENT_DATE - INTERVAL '70 years';

-- Update salary to 15000 for employees with lower salary
UPDATE Employees
SET Salary = 15000
WHERE Salary < 15000;

-- Additional useful queries

-- Get employees grouped by department with average salary
SELECT 
    p.Department,
    COUNT(*) as EmployeeCount,
    AVG(e.Salary) as AverageSalary,
    MIN(e.Salary) as MinSalary,
    MAX(e.Salary) as MaxSalary
FROM Employees e
INNER JOIN Positions p ON e.PositionId = p.Id
GROUP BY p.Department
ORDER BY p.Department;

-- Get employees hired in the last 90 days
SELECT 
    e.FirstName,
    e.LastName,
    e.Email,
    e.HireDate,
    p.Name AS Position
FROM Employees e
INNER JOIN Positions p ON e.PositionId = p.Id
WHERE e.HireDate >= CURRENT_DATE - INTERVAL '90 days'
ORDER BY e.HireDate DESC;

-- Get positions with employee count
SELECT 
    p.Name AS Position,
    p.Department,
    p.BaseSalary,
    COUNT(e.Id) as EmployeeCount
FROM Positions p
LEFT JOIN Employees e ON e.PositionId = p.Id
GROUP BY p.Id, p.Name, p.Department, p.BaseSalary
ORDER BY p.Department, p.Name; 