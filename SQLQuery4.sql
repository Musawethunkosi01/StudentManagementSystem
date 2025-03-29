--Update stored procedure

create proc UpdateStudent_SP
@StudentID int,
@FirstName nvarchar(50),
@Surname nvarchar(50),
@Age float,
@Email nvarchar(50),
@PhoneNumber nvarchar(10),
@Gender nvarchar(10)
as
begin
Update StudentData set Student_ID = @StudentID, FirstName = @FirstName, Surname = @Surname, Age = @Age, Email = @Email, PhoneNumber = @PhoneNumber, Gender = @Gender where Student_ID = @StudentID
end

--Delete store procudeure
create proc DeleteStudent_SP
@StudentID int
as
begin
Delete StudentData where Student_ID = @StudentID
end

--Load specific record
create proc LoadStudent_SP
@StudentID int
as
begin
Select * From  StudentData where Student_ID = @StudentID
end