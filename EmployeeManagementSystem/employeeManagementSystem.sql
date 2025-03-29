CREATE TABLE Employee (
    empId INT IDENTITY(1,1) PRIMARY KEY,
    empName VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone VARCHAR(15),
    role VARCHAR(50)
);

CREATE TABLE Department (
    deptId INT IDENTITY(1,1) PRIMARY KEY,
    deptName VARCHAR(100) NOT NULL
);

CREATE TABLE Manager (
    managerId INT,
    empId INT,
    PRIMARY KEY (managerId, empId),
    FOREIGN KEY (managerId) REFERENCES Employee(empId),
    FOREIGN KEY (empId) REFERENCES Employee(empId)
);

CREATE TABLE EmployeeDepartment (
    empId INT,
    deptId INT,
    PRIMARY KEY (empId, deptId),
    FOREIGN KEY (empId) REFERENCES Employee(empId),
    FOREIGN KEY (deptId) REFERENCES Department(deptId)
);
