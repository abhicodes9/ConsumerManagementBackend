

CREATE PROCEDURE CreateCustomer
    @FirstName NVARCHAR(250),
    @LastName NVARCHAR(250),
    @Email NVARCHAR(100),
    @DateofJoining DATE,
    @CreatedDate DATETIME,
    @LastUpdatedDate DATETIME
AS
BEGIN
    INSERT INTO Customers(FirstName, LastName, Email, DateofJoining, CreatedDate, LastUpdatedDate)
    VALUES (@FirstName, @LastName, @Email, @DateofJoining, @CreatedDate, @LastUpdatedDate);

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END


CREATE PROCEDURE DeleteCustomer
    @CustomerId INT
AS
BEGIN
    DELETE FROM Customers WHERE CustomerId = @CustomerId;
END


CREATE PROCEDURE UpdateCustomer
    @CustomerId INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @DateofJoining DATE,
    @LastUpdatedDate DATETIME
AS
BEGIN
    UPDATE Customers
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        DateofJoining = @DateofJoining,
        LastUpdatedDate = @LastUpdatedDate
    WHERE CustomerId = @CustomerId;
END




CREATE PROCEDURE GetCustomerById
    @CustomerId INT
AS
BEGIN
    SELECT CustomerId, FirstName, LastName, Email, DateofJoining, CreatedDate, LastUpdatedDate
    FROM Customers
    WHERE CustomerId = @CustomerId;
END

CREATE PROCEDURE GetAllCustomers
AS
BEGIN
    SELECT CustomerId, FirstName, LastName, Email, DateofJoining, CreatedDate, LastUpdatedDate
    FROM Customers
    ORDER BY LastUpdatedDate DESC;
END



CREATE PROCEDURE CheckExistingEmail
    @Email NVARCHAR(100)
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = COUNT(*) FROM Customers WHERE Email = @Email;
    SELECT CASE WHEN @Count > 0 THEN 1 ELSE 0 END AS EmailExists;
END
