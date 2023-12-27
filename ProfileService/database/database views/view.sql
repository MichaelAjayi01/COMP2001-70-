-- Creating a modified view with information from CW2_USER_PROFILE, CW2_COMPLETED_TRAILS, and CW2_Trails
CREATE VIEW CW2_User_Profile_View AS
SELECT
    UP.User_ID,
    UP.First_Name,
    UP.Last_Name,
    UP.About,
    UP.Location,
    UP.Units,
    UP.Calorie_Counter_Info,
    UP.Height,
    UP.Weight,
    UP.Birthday,
    UP.Set_Password,
    UP.Profile_Picture,
    CTC.User_Trail_ID AS Completed_Trail_ID,
    CTC.Completed_Trail_Count,
    T.Trail_Name,
    T.List_of_Trails
FROM
    CW2_USER_PROFILE UP
LEFT JOIN
    CW2_UserProfile_CompletedTrails_JT CT ON UP.User_ID = CT.User_ID
LEFT JOIN
    CW2_COMPLETED_TRAILS CTC ON CT.User_Trail_ID = CTC.User_Trail_ID
LEFT JOIN
    CW2_Trails T ON CT.Trail_ID = T.Trail_ID;
