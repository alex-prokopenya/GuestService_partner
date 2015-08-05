namespace GuestService.Resources
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    using Sm.System.Mvc.Language;
    using System.Collections.Generic;
    using System.Collections;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    public class AccountStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        private static Dictionary<string, Dictionary<string, string>> strings = new Dictionary<string, Dictionary<string, string>>();

        public static string Get(string key)
        {
            var str = UrlLanguage.CurrentLanguage;

            if (strings.Count == 0)
            {
                strings[str] = new Dictionary<string, string>();

                //загрузить строки из xml
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "AccountStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return AccountStrings.ResourceManager.GetString(key, AccountStrings.resourceCulture);
        }

        internal AccountStrings()
        {
        }

        public static string AccountLogin_EmailNotConfirmed
        {
            get
            {
                return ResourceManager.GetString("AccountLogin_EmailNotConfirmed", resourceCulture);
            }
        }

        public static string AccountLogin_InvalidCredentails
        {
            get
            {
                return ResourceManager.GetString("AccountLogin_InvalidCredentails", resourceCulture);
            }
        }

        public static string AccountRecovery_CannotRecovery
        {
            get
            {
                return ResourceManager.GetString("AccountRecovery_CannotRecovery", resourceCulture);
            }
        }

        public static string AccountResetPassword_CannotReset
        {
            get
            {
                return ResourceManager.GetString("AccountResetPassword_CannotReset", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        public static string ErrorDuplicateEmail
        {
            get
            {
                return ResourceManager.GetString("ErrorDuplicateEmail", resourceCulture);
            }
        }

        public static string ErrorDuplicateUserName
        {
            get
            {
                return ResourceManager.GetString("ErrorDuplicateUserName", resourceCulture);
            }
        }

        public static string ErrorInvalidAnswer
        {
            get
            {
                return ResourceManager.GetString("ErrorInvalidAnswer", resourceCulture);
            }
        }

        public static string ErrorInvalidEmail
        {
            get
            {
                return ResourceManager.GetString("ErrorInvalidEmail", resourceCulture);
            }
        }

        public static string ErrorInvalidPassword
        {
            get
            {
                return ResourceManager.GetString("ErrorInvalidPassword", resourceCulture);
            }
        }

        public static string ErrorInvalidQuestion
        {
            get
            {
                return ResourceManager.GetString("ErrorInvalidQuestion", resourceCulture);
            }
        }

        public static string ErrorInvalidUserName
        {
            get
            {
                return ResourceManager.GetString("ErrorInvalidUserName", resourceCulture);
            }
        }

        public static string ErrorProviderError
        {
            get
            {
                return ResourceManager.GetString("ErrorProviderError", resourceCulture);
            }
        }

        public static string ErrorUnknownError
        {
            get
            {
                return ResourceManager.GetString("ErrorUnknownError", resourceCulture);
            }
        }

        public static string ErrorUserRejected
        {
            get
            {
                return ResourceManager.GetString("ErrorUserRejected", resourceCulture);
            }
        }

        public static string LocalPasswordModel_C_ConfirmPassword
        {
            get
            {
                return ResourceManager.GetString("LocalPasswordModel_C_ConfirmPassword", resourceCulture);
            }
        }

        public static string LocalPasswordModel_L_Password
        {
            get
            {
                return ResourceManager.GetString("LocalPasswordModel_L_Password", resourceCulture);
            }
        }

        public static string Login_AlreadyUser
        {
            get
            {
                return ResourceManager.GetString("Login_AlreadyUser", resourceCulture);
            }
        }

        public static string Login_Email
        {
            get
            {
                return ResourceManager.GetString("Login_Email", resourceCulture);
            }
        }

        public static string Login_Forget_1
        {
            get
            {
                return ResourceManager.GetString("Login_Forget_1", resourceCulture);
            }
        }

        public static string Login_Forget_2
        {
            get
            {
                return ResourceManager.GetString("Login_Forget_2", resourceCulture);
            }
        }

        public static string Login_LoginButton
        {
            get
            {
                return ResourceManager.GetString("Login_LoginButton", resourceCulture);
            }
        }

        public static string Login_LogoutButton
        {
            get
            {
                return ResourceManager.GetString("Login_LogoutButton", resourceCulture);
            }
        }

        public static string Login_Password
        {
            get
            {
                return ResourceManager.GetString("Login_Password", resourceCulture);
            }
        }

        public static string Login_Ph_Email
        {
            get
            {
                return ResourceManager.GetString("Login_Ph_Email", resourceCulture);
            }
        }

        public static string Login_Ph_Password
        {
            get
            {
                return ResourceManager.GetString("Login_Ph_Password", resourceCulture);
            }
        }

        public static string Login_RememberMe
        {
            get
            {
                return ResourceManager.GetString("Login_RememberMe", resourceCulture);
            }
        }

        public static string Login_Social
        {
            get
            {
                return ResourceManager.GetString("Login_Social", resourceCulture);
            }
        }

        public static string LoginModel_C_ConfirmPassword
        {
            get
            {
                return ResourceManager.GetString("LoginModel_C_ConfirmPassword", resourceCulture);
            }
        }

        public static string LoginModel_L_Password
        {
            get
            {
                return ResourceManager.GetString("LoginModel_L_Password", resourceCulture);
            }
        }

        public static string LoginModel_R_Mail
        {
            get
            {
                return ResourceManager.GetString("LoginModel_R_Mail", resourceCulture);
            }
        }

        public static string LoginModel_R_Password
        {
            get
            {
                return ResourceManager.GetString("LoginModel_R_Password", resourceCulture);
            }
        }

        public static string LoginModel_R_UserName
        {
            get
            {
                return ResourceManager.GetString("LoginModel_R_UserName", resourceCulture);
            }
        }

        public static string LoginText_1
        {
            get
            {
                return ResourceManager.GetString("LoginText_1", resourceCulture);
            }
        }

        public static string LoginText_2
        {
            get
            {
                return ResourceManager.GetString("LoginText_2", resourceCulture);
            }
        }

        public static string LoginTitle
        {
            get
            {
                return ResourceManager.GetString("LoginTitle", resourceCulture);
            }
        }

        public static string RecoveryModel_R_UserName
        {
            get
            {
                return ResourceManager.GetString("RecoveryModel_R_UserName", resourceCulture);
            }
        }

        public static string Register_As_1
        {
            get
            {
                return ResourceManager.GetString("Register_As_1", resourceCulture);
            }
        }

        public static string Register_As_2
        {
            get
            {
                return ResourceManager.GetString("Register_As_2", resourceCulture);
            }
        }

        public static string Register_Back
        {
            get
            {
                return ResourceManager.GetString("Register_Back", resourceCulture);
            }
        }

        public static string Register_ChangePassword
        {
            get
            {
                return ResourceManager.GetString("Register_ChangePassword", resourceCulture);
            }
        }

        public static string Register_ConfirmEmail
        {
            get
            {
                return ResourceManager.GetString("Register_ConfirmEmail", resourceCulture);
            }
        }

        public static string Register_ConfirmPassword
        {
            get
            {
                return ResourceManager.GetString("Register_ConfirmPassword", resourceCulture);
            }
        }

        public static string Register_Email
        {
            get
            {
                return ResourceManager.GetString("Register_Email", resourceCulture);
            }
        }

        public static string Register_EmailSuccess
        {
            get
            {
                return ResourceManager.GetString("Register_EmailSuccess", resourceCulture);
            }
        }

        public static string Register_EmailUnsuccess
        {
            get
            {
                return ResourceManager.GetString("Register_EmailUnsuccess", resourceCulture);
            }
        }

        public static string Register_ErrorSocial
        {
            get
            {
                return ResourceManager.GetString("Register_ErrorSocial", resourceCulture);
            }
        }

        public static string Register_ErrorSocialS
        {
            get
            {
                return ResourceManager.GetString("Register_ErrorSocialS", resourceCulture);
            }
        }

        public static string Register_MainFormLink
        {
            get
            {
                return ResourceManager.GetString("Register_MainFormLink", resourceCulture);
            }
        }

        public static string Register_Password
        {
            get
            {
                return ResourceManager.GetString("Register_Password", resourceCulture);
            }
        }

        public static string Register_Ph_ConfirmPassword
        {
            get
            {
                return ResourceManager.GetString("Register_Ph_ConfirmPassword", resourceCulture);
            }
        }

        public static string Register_Ph_Email
        {
            get
            {
                return ResourceManager.GetString("Register_Ph_Email", resourceCulture);
            }
        }

        public static string Register_Ph_Password
        {
            get
            {
                return ResourceManager.GetString("Register_Ph_Password", resourceCulture);
            }
        }

        public static string Register_RegisterButton
        {
            get
            {
                return ResourceManager.GetString("Register_RegisterButton", resourceCulture);
            }
        }

        public static string Register_ToMyAccount
        {
            get
            {
                return ResourceManager.GetString("Register_ToMyAccount", resourceCulture);
            }
        }

        public static string RegisterEmailNotConfirmed
        {
            get
            {
                return ResourceManager.GetString("RegisterEmailNotConfirmed", resourceCulture);
            }
        }

        public static string RegisterEmailNotConfirmedNote
        {
            get
            {
                return ResourceManager.GetString("RegisterEmailNotConfirmedNote", resourceCulture);
            }
        }

        public static string RegisterEmailNote
        {
            get
            {
                return ResourceManager.GetString("RegisterEmailNote", resourceCulture);
            }
        }

        public static string RegisterText
        {
            get
            {
                return ResourceManager.GetString("RegisterText", resourceCulture);
            }
        }

        public static string RegisterTitle
        {
            get
            {
                return ResourceManager.GetString("RegisterTitle", resourceCulture);
            }
        }

        public static string ResetPassword_CannotReset
        {
            get
            {
                return ResourceManager.GetString("ResetPassword_CannotReset", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("GuestService.Resources.AccountStrings", typeof(AccountStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string Restore_Btn
        {
            get
            {
                return ResourceManager.GetString("Restore_Btn", resourceCulture);
            }
        }

        public static string Restore_ConfirmPassword
        {
            get
            {
                return ResourceManager.GetString("Restore_ConfirmPassword", resourceCulture);
            }
        }

        public static string Restore_Email_Register
        {
            get
            {
                return ResourceManager.GetString("Restore_Email_Register", resourceCulture);
            }
        }

        public static string Restore_MaimFormLink
        {
            get
            {
                return ResourceManager.GetString("Restore_MaimFormLink", resourceCulture);
            }
        }

        public static string Restore_Password
        {
            get
            {
                return ResourceManager.GetString("Restore_Password", resourceCulture);
            }
        }

        public static string Restore_Ph_ConfirmPassword
        {
            get
            {
                return ResourceManager.GetString("Restore_Ph_ConfirmPassword", resourceCulture);
            }
        }

        public static string Restore_Ph_Password
        {
            get
            {
                return ResourceManager.GetString("Restore_Ph_Password", resourceCulture);
            }
        }

        public static string Restore_SetPasswordButton
        {
            get
            {
                return ResourceManager.GetString("Restore_SetPasswordButton", resourceCulture);
            }
        }

        public static string RestoreChangedOK
        {
            get
            {
                return ResourceManager.GetString("RestoreChangedOK", resourceCulture);
            }
        }

        public static string RestorePasswordChangedError
        {
            get
            {
                return ResourceManager.GetString("RestorePasswordChangedError", resourceCulture);
            }
        }

        public static string RestoreText
        {
            get
            {
                return ResourceManager.GetString("RestoreText", resourceCulture);
            }
        }

        public static string RestoreTitle
        {
            get
            {
                return ResourceManager.GetString("RestoreTitle", resourceCulture);
            }
        }
    }
}

