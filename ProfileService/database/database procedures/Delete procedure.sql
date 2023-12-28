-- Delete procedure for CW2_USER_PROFILE
CREATE PROCEDURE DeleteUserProfile
    @User_ID INT
AS
BEGIN
    -- Begin a transaction
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Declare a table variable to store the deleted user ID
        DECLARE @DeletedUsers TABLE (DeletedUserID INT);

        -- Check if the user profile exists
        IF EXISTS (SELECT 1 FROM CW2_USER_PROFILE WHERE User_ID = @User_ID)
        BEGIN
            -- Delete from CW2_USER_PROFILE and store the deleted user ID
            DELETE FROM CW2_USER_PROFILE 
            OUTPUT DELETED.User_ID INTO @DeletedUsers
            WHERE User_ID = @User_ID;

            -- Insert into CW2_Audit_Log using the stored deleted user ID
            INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
            SELECT DeletedUserID, 'DELETE', GETDATE(), 'User profile deleted.' FROM @DeletedUsers;

            -- Commit the transaction
            COMMIT;
        END
        ELSE
        BEGIN
            -- Rollback the transaction if the user profile does not exist
            ROLLBACK;

            -- Insert into CW2_Audit_Log for non-existing user profile
            INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
            VALUES (@User_ID, 'DELETE', GETDATE(), 'Attempted deletion failed');
        END
    END TRY
    BEGIN CATCH
        -- An error occurred, rollback the transaction
        ROLLBACK;

        -- Log the error or take appropriate action
        INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
        VALUES (@User_ID, 'DELETE', GETDATE(), 'Error during user profile deletion');

        -- Rethrow the error for further handling
        THROW;
    END CATCH
END;
