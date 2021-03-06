﻿using DTO.Preferences;
using Service.Preferences;
using System;
using System.Web;
using Utilities;

namespace Web.Site.Preferences
{
    public partial class Account : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadPreferences();
            }
        }

        private void loadPreferences()
        {
            try
            {
                PreferencesService preferencesService = new PreferencesService(SessionManager.sessionMemberInfo);
                MemberPreferencesDTO memberPreferences = preferencesService.getMemberPreferences();

                if (memberPreferences != null)
                {
                    txtEmail.Text = memberPreferences.email;
                    txtRealName.Text = memberPreferences.realName;
                    txtDisplayName.Text = memberPreferences.displayName;
                }
                else
                {
                    showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
                }
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }
        }

        private void showError(string message)
        {
            cvEditPreferences.IsValid = false;
            cvEditPreferences.ErrorMessage = message;
        }

        protected void cmdEditPreferences_Click(object sender, EventArgs e)
        {
            try
            {
                PreferencesService preferencesService = new PreferencesService(SessionManager.sessionMemberInfo);
                MemberPreferencesDTO enMemberPreferences = new MemberPreferencesDTO();

                enMemberPreferences.memberId = SessionManager.sessionMemberInfo.id;
                enMemberPreferences.realName = txtRealName.Text;
                enMemberPreferences.displayName = txtDisplayName.Text;

                preferencesService.updateMemberPreferences(enMemberPreferences);
                InfoMessage.setMessage(HttpContext.GetGlobalResourceObject("Resource", "PreferenciasActualizadas").ToString(), Enums.infoMessageType.Success);
                loadPreferences();
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }
        }
    }
}