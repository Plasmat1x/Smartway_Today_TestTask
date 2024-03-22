USE stt_db;

INSERT INTO Departments(Name, Phone) VALUES
('c1d1', '888555'),
('c1d2', '888554'),
('c1d3', '888553'),
('c2d1', '888');

INSERT INTO Employees(Name, Surname, Phone, PassportType, PassportNumber, CompanyId, DepartmentId) VALUES
('n1', 'sn1', '88005553535','pass','8888888888', 1, 1),
('n2', 'sn2','88005553535','pass','8888888888', 1, 2),
('n3', 'sn3','88005553535','pass','8888888888', 2, 1);
GO