-- Creating an Audit Log Trigger for CW2_USER_PROFILE table

CREATE TRIGGER tr_CW2_UserProfile_AuditLog
ON CW2_USER_PROFILE
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OperationType NVARCHAR(50);
    DECLARE @OperationDetails NVARCHAR(MAX);

    -- Check the type of operation
    IF EXISTS (SELECT * FROM INSERTED)
    BEGIN
        SET @OperationType = 'INSERT';
        SET @OperationDetails = 'New user added.';
    END
    ELSE IF EXISTS (SELECT * FROM DELETED)
    BEGIN
        SET @OperationType = 'DELETE';
        SET @OperationDetails = 'User deleted.';
    END
    ELSE
    BEGIN
        SET @OperationType = 'UPDATE';
        SET @OperationDetails = 'User details updated.';
    END

    -- Insert into the Audit Log table
    INSERT INTO CW2_Audit_Log (User_ID, Operation_Type, Operation_DateTime, Operation_Details)
    SELECT
        ISNULL(i.User_ID, d.User_ID),
        @OperationType,
        GETDATE(),
        @OperationDetails
    FROM
        INSERTED i
    FULL OUTER JOIN
        DELETED d ON i.User_ID = d.User_ID;
END;
