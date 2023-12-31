CREATE PROCEDURE DeleteUserProfile
    @User_ID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Declare table variables to store the deleted IDs
        DECLARE @DeletedTrails TABLE (DeletedTrailID INT);

        -- Check if the user profile exists
        IF EXISTS (SELECT 1 FROM CW2_USER_PROFILE WHERE User_ID = @User_ID)
        BEGIN
            -- Delete from CW2_UserProfile_CompletedTrails_JT and store the deleted trail IDs
            DELETE FROM CW2_UserProfile_CompletedTrails_JT
            OUTPUT DELETED.Trail_ID INTO @DeletedTrails
            WHERE User_ID = @User_ID;

            -- Delete from CW2_Completed_Trails using the stored deleted trail IDs
            DELETE FROM CW2_COMPLETED_TRAILS
            WHERE Completed_Trail_ID IN (SELECT DeletedTrailID FROM @DeletedTrails);

            -- Delete from CW2_Trails using the stored deleted trail IDs
            DELETE FROM CW2_Trails
            WHERE Trail_ID IN (SELECT DeletedTrailID FROM @DeletedTrails);

            -- Archive the user profile before deletion
            INSERT INTO CW2_Archived_Users (
                User_ID, 
                First_Name, 
                Last_Name, 
                Email, 
                About, 
                Location, 
                Units, 
                Calorie_Counter_Info, 
                Height, 
                Weight, 
                Birthday, 
                Set_Password, 
                Profile_Picture, 
                ArchiveDateTime
            )
            SELECT 
                User_ID, 
                First_Name, 
                Last_Name, 
                Email, 
                About, 
                Location, 
                Units, 
                Calorie_Counter_Info, 
                Height, 
                Weight, 
                Birthday, 
                Set_Password, 
                Profile_Picture, 
                GETDATE() -- Use current datetime for ArchiveDateTime
            FROM CW2_USER_PROFILE
            WHERE User_ID = @User_ID;

            -- Log the archiving in the audit table
            INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
            VALUES (@User_ID, 'ARCHIVE', GETDATE(), 'User profile archived.');

            -- Delete from CW2_USER_PROFILE
            DELETE FROM CW2_USER_PROFILE 
            WHERE User_ID = @User_ID;

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
