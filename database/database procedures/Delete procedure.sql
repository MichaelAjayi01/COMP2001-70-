-- Delete procedure for CW2_USER_PROFILE
CREATE PROCEDURE DeleteUserProfile
    @User_ID INT
AS
BEGIN
    DELETE FROM CW2_USER_PROFILE WHERE User_ID = @User_ID;

    INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
    VALUES (@User_ID, 'DELETE', GETDATE(), 'User profile deleted.');
END;
