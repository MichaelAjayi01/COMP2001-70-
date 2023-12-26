-- Fixed a lot of discrepancies between the diagram and SQL

-- Creating CW2_User_Profile table
CREATE TABLE CW2_USER_PROFILE (
    User_ID INT IDENTITY(1,1) PRIMARY KEY,
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
CREATE TABLE CW2_Trails (
    Trail_ID INT IDENTITY(1,1) PRIMARY KEY,
    Trail_Name NVARCHAR(100),
    List_of_Trails NVARCHAR(MAX)
);

-- Creating CW2 completed trails table
CREATE TABLE CW2_COMPLETED_TRAILS (
    User_ID INT,
    Trail_ID INT,
    Completed_Trail_Count INT DEFAULT 0,
    PRIMARY KEY (User_ID, Trail_ID),
    FOREIGN KEY(User_ID) REFERENCES CW2_UserProfile_CompletedTrials_JT(User_ID),
    FOREIGN KEY(Trail_ID) REFERENCES CW2_Trails(Trail_ID)
);


-- Creating CW2_Stats table
CREATE TABLE CW2_Stats (
    Stats_ID INT IDENTITY(1,1) PRIMARY KEY,
    User_ID INT,
    FOREIGN KEY (User_ID) REFERENCES CW2_USER_PROFILE(User_ID)
);

-- Creating CW2_Trail_Review_JT(Junction Table)
CREATE TABLE CW2_Trail_Review_JT (
    Trail_Review_ID INT IDENTITY(1,1) PRIMARY KEY,
    Trail_ID INT,
    Review_ID INT,
    FOREIGN KEY(Trail_ID) REFERENCES CW2_Trails(Trail_ID)
);

-- Creating CW2_Trail_Reviews
CREATE TABLE CW2_Reviews (
    Review_ID INT IDENTITY(1,1) PRIMARY KEY,
    Trail_Review_ID INT,
    User_ID INT,
    FOREIGN KEY (Trail_Review_ID) REFERENCES CW2_COMPLETED_TRAILS_JT(Trail_Review_ID),
    FOREIGN KEY (User_ID) REFERENCES CW2_User_Profile(User_ID)
);

-- Creating CW2_Custom_Routes_Table
CREATE TABLE CW2_Custom_RouteS (
    Custom_Route_ID INT IDENTITY(1,1) PRIMARY KEY,
    User_ID INT,
    FOREIGN KEY (User_ID) REFERENCES CW2_USER_PROFILE
);

-- Creating CW2_UserProfile_CompletedTrials_JT(Junction Table)
CREATE TABLE CW2_UserProfile_CompletedTrials_JT (
    User_ID INT,
    Trail_ID INT,
    FOREIGN KEY (Trail_ID) REFERENCES CW2_COMPLETED_TRAILS(Trail_ID),
    FOREIGN KEY(User_ID) REFERENCES CW2_User_Profile(User_ID)
);
