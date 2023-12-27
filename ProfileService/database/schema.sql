-- Creating CW2_User_Profile table
CREATE TABLE CW2_USER_PROFILE (
    User_ID INT IDENTITY(1,1) PRIMARY KEY, --
    First_Name NVARCHAR(50),
    Last_Name NVARCHAR(50),
    About NVARCHAR(MAX),
    Location NVARCHAR(100),
    Units NVARCHAR(20),
    Calorie_Counter_Info NVARCHAR(100),
    Height FLOAT,
    Weight FLOAT,
    Birthday DATE,
    Set_Password NVARCHAR(50),
    Profile_Picture VARBINARY(MAX)
);

-- Creating CW2_Trails table
CREATE TABLE CW2_Trails (--
    Trail_ID INT IDENTITY(1,1) PRIMARY KEY,
    Trail_Name NVARCHAR(100),
    List_of_Trails NVARCHAR(MAX)
);

-- Creating CW2_UserProfile_CompletedTrails_JT(Junction Table)
CREATE TABLE CW2_UserProfile_CompletedTrails_JT ( --
    User_ID INT,
    Trail_ID INT,
    PRIMARY KEY (User_ID, Trail_ID),
    FOREIGN KEY (User_ID) REFERENCES CW2_USER_PROFILE(User_ID),
    FOREIGN KEY (Trail_ID) REFERENCES CW2_Trails(Trail_ID)
);

-- Creating CW2_COMPLETED_TRAILS table
CREATE TABLE CW2_COMPLETED_TRAILS (--
    User_ID INT,
    Trail_ID INT,
    Completed_Trail_Count INT DEFAULT 0,
    PRIMARY KEY (User_ID, Trail_ID),
    FOREIGN KEY (User_ID, Trail_ID) REFERENCES CW2_UserProfile_CompletedTrails_JT(User_ID, Trail_ID)
);
-- Creating CW2_Audit_Log table
CREATE TABLE CW2_Audit_Log (
    Audit_ID INT IDENTITY(1,1) PRIMARY KEY,
    User_ID INT, -- If you want to track which user performed the operation
    Operation_Type NVARCHAR(50) NOT NULL,
    Operation_DateTime DATETIME NOT NULL,
    Operation_Details NVARCHAR(MAX),
    FOREIGN KEY (User_ID) REFERENCES CW2_USER_PROFILE(User_ID) -- If you want to associate the operation with a specific user
);

