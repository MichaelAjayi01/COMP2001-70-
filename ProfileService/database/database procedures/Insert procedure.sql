-- Insert procedure for CW2_USER_PROFILE
CREATE PROCEDURE InsertUserProfile
    @First_Name NVARCHAR(50),
    @Last_Name NVARCHAR(50),
    @About NVARCHAR(MAX),
    @Location NVARCHAR(100),
    @Units NVARCHAR(20),
    @Calorie_Counter_Info NVARCHAR(100),
    @Height FLOAT,
    @Weight FLOAT,
    @Birthday DATE,
    @Set_Password NVARCHAR(50),
    @Profile_Picture VARBINARY(MAX)
AS
BEGIN
    INSERT INTO CW2_USER_PROFILE (First_Name, Last_Name, About, Location, Units, Calorie_Counter_Info, Height, Weight, Birthday, Set_Password, Profile_Picture)
    VALUES (@First_Name, @Last_Name, @About, @Location, @Units, @Calorie_Counter_Info, @Height, @Weight, @Birthday, @Set_Password, @Profile_Picture);

    INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
    VALUES (SCOPE_IDENTITY(), 'INSERT', GETDATE(), 'User profile added.');
END;
