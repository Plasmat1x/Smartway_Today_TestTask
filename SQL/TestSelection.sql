USE stt_db;

SELECT * FROM Employees
INNER JOIN Departments ON DepartmentId = Departments.Id
WHERE CompanyId = 1 AND Departments.Name = 'c1d1';
GO