USE [Students_DB]
GO
/****** Object:  StoredProcedure [dbo].[InsertStudent_SP]    Script Date: 2025/03/12 14:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[InsertStudent_SP]
@StudentID int,
@FirstName nvarchar(50),
@Surname nvarchar(50),
@Age float,
@Email nvarchar(50),
@PhoneNumber nvarchar(10),
@Gender nvarchar(10)
as
begin
Insert into StudentData(Student_ID, FirstName, Surname, Age, Email, PhoneNumber, Gender)
values(@StudentID, @FirstName, @Surname, @Age, @Email, @PhoneNumber, @Gender)
end