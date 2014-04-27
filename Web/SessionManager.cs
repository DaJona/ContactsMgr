using DTO.System;
using System.Web;

namespace Web
{
    public class SessionManager
    {
        public const string memberInfo = "sessionMemberInfo";

        public static void create(int memberId, string memberName, string memberLang, int memberTimeZoneMinsOffset)
        {
            SessionMemberInfo enSessionMemberInfo = new SessionMemberInfo();

            enSessionMemberInfo.id = memberId;
            enSessionMemberInfo.name = memberName;
            enSessionMemberInfo.lang = memberLang;
            enSessionMemberInfo.timeZoneMinsOffset = memberTimeZoneMinsOffset;

            sessionMemberInfo = enSessionMemberInfo;
        }

        public static SessionMemberInfo sessionMemberInfo
        {
            get
            {
                if (HttpContext.Current.Session[memberInfo] == null)
                {
                    return null;
                }
                else
                {
                    return (SessionMemberInfo)HttpContext.Current.Session[memberInfo];
                }
            }
            set
            {
                HttpContext.Current.Session[memberInfo] = value;
            }
        }

        public static bool hasActiveSession
        {
            get
            {
                if (sessionMemberInfo != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}