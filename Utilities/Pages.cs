using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Pages
    {
        public static Func<string, string> ResolveUrl;

        public static string getLogin()
        {
            return ResolveUrl("~/Login");
        }

        public static string getHomeDefault()
        {
            return ResolveUrl("~/Site/Default");
        }

        #region Preferences

        public static string getPreferencesDefault()
        {
            return ResolveUrl("~/Site/Preferences/Default");
        }

        public static string getPreferencesAccount()
        {
            return ResolveUrl("~/Site/Preferences/Account");
        }

        public static string getPreferencesPassword()
        {
            return ResolveUrl("~/Site/Preferences/Password");
        }

        #endregion

        #region Contacts

        public static string getContactsDefault()
        {
            return ResolveUrl("~/Site/Contacts/Default");
        }

        public static string getContactsCreate()
        {
            return ResolveUrl("~/Site/Contacts/Create");
        }

        public static string getContactsEdit(int contactId)
        {
            return ResolveUrl("~/Site/Contacts/Edit?" + Parameters.id + "=" + contactId.ToString());
        }

        #endregion
    }
}
