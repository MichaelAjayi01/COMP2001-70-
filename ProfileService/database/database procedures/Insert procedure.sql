CREATE PROCEDURE InsertUserProfile
    @First_Name NVARCHAR(50),
    @Last_Name NVARCHAR(50),
    @Email NVARCHAR(100),
    @About NVARCHAR(MAX),
    @Location NVARCHAR(100),
    @Units NVARCHAR(20),
    @Calorie_Counter_Info NVARCHAR(100),
    @Height FLOAT,
    @Weight FLOAT,
    @Birthday DATE,
    @Set_Password NVARCHAR(50),
    @Profile_Picture VARBINARY(MAX),
    @Trail_Name NVARCHAR(100),
    @List_of_Trails NVARCHAR(MAX),
    @PasswordSalt NVARCHAR(128)
AS
BEGIN
    -- Insert into CW2_USER_PROFILE
    INSERT INTO CW2_USER_PROFILE (First_Name, Last_Name, Email, About, Location, Units, Calorie_Counter_Info, Height, Weight, Birthday, Set_Password, Profile_Picture, PasswordSalt)
    VALUES (@First_Name, @Last_Name, @Email, @About, @Location, @Units, @Calorie_Counter_Info, @Height, @Weight, @Birthday, @Set_Password, @Profile_Picture, @PasswordSalt);

    -- Insert into CW2_Trails
    DECLARE @Trail_ID INT;

    INSERT INTO CW2_Trails (Trail_Name, List_of_Trails)
    VALUES (@Trail_Name, @List_of_Trails);

    SET @Trail_ID = SCOPE_IDENTITY();

    -- Insert into CW2_Audit_Log
    INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
    VALUES (@Trail_ID, 'INSERT', GETDATE(), 'User profile added.');

END;
