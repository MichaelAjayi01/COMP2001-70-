-- Update procedure for CW2_USER_PROFILE
CREATE PROCEDURE UpdateUserProfile
    @User_ID INT,
    @First_Name NVARCHAR(50),
    @Last_Name NVARCHAR(50),
    @Email NVARCHAR(100), -- Add parameter for Email
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
    UPDATE CW2_USER_PROFILE
    SET
        First_Name = @First_Name,
        Last_Name = @Last_Name,
        Email = @Email, -- Update the Email column
        About = @About,
        Location = @Location,
        Units = @Units,
        Calorie_Counter_Info = @Calorie_Counter_Info,
        Height = @Height,
        Weight = @Weight,
        Birthday = @Birthday,
        Set_Password = @Set_Password,
        Profile_Picture = @Profile_Picture
    WHERE
        User_ID = @User_ID;

    INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
    VALUES (@User_ID, 'UPDATE', GETDATE(), 'User profile updated.');
END;
