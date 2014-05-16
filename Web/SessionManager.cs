using DTO.System;
using System.Web;

namespace Web
{
    public class SessionManager
    {
        public const string memberInfo = "sessionMemberInfo";

        public static void create(int memberId, string memberName, string memberLang, int memberTimeZoneMinsOffset)
        {
            SessionMemberInfoDTO enSessionMemberInfo = new SessionMemberInfoDTO();

            enSessionMemberInfo.id = memberId;
            enSessionMemberInfo.name = memberName;
            enSessionMemberInfo.lang = memberLang;
            enSessionMemberInfo.timeZoneMinsOffset = memberTimeZoneMinsOffset;

            sessionMemberInfo = enSessionMemberInfo;
        }

        public static SessionMemberInfoDTO sessionMemberInfo
        {
            get
            {
                if (HttpContext.Current.Session[memberInfo] == null)
                {
                    return null;
                }
                else
                {
                    return (SessionMemberInfoDTO)HttpContext.Current.Session[memberInfo];
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