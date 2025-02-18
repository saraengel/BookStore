using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Entities
{
    public enum Language
    {
        Hebrew = 1,
        Arabic = 2,
        English = 3,
        Russian = 4
    }

    public enum UserType
    {
        Site = 1,
        Api = 2
    }

    public enum Status
    {
        Succeeded = 1,
        NotSucceeded = 2,
        Unauthorized = 3
    }

    public enum Severity
    {
        Debug,
        Info,
        Warning,
        Error,
        FatalError
    }

    public enum LogType
    {
        Default,
        DBQuery,
        Performances,
        MicrosoftLogs,
        EventLog
    }

    public enum AppCacheOption
    {
        Configuration = 2
    }



    public enum PermissionValue
    {
        Edit = 1,
        Readonly = 2,
        NotAllowed = 3,
        Allowed = 4
    }
    public enum Permissions
    {
        GeneralSettingsManagement,
        ManageSystemTables,
        ChangeUserPassword
    }

    public enum PermissionType
    {
        Boolean = 1,
        Edit = 2,
        Values = 3
    }


    public enum LogicError
    {
    }

    public enum LockEntityType
    {
        Product
    }

    public enum SecurityStatus
    {
        Success = 1,
        SecurityError = 2,
        Failed = 3,
        WaitForNextStep = 4,
    }

    public enum SecurityResultCode
    {
        InvalidLogin = 1,
        VerifyCodeSent = 4,
        WrongVerifyCode = 5,
        VerifyCodeSuccess = 10,
        SendMailOrSMSFailed = 14,
        InvalidVerificationsMax = 15,
        NoUserMailOrPhone = 16,
        TooManyCodeValidationVhecks = 17,
        ShouldAnotherVerification = 18,
        ShouldChangePassword = 19
    }

    public enum LoginError
    {
        InvalidLogin = 1,
        InvalidCaptcha,
        BlockedIp,
        ForgotPasswrodWithWrongMail,
        ForgotPasswrodWithWrongPhone
    }
}