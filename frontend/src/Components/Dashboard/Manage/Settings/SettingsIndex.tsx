import { LoadingButton } from '@mui/lab';
import { Box, Tab, Tabs } from '@mui/material';
import { Formik } from 'formik';
import React, { ReactElement, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import AccountsProxy from '../../../../ApiClient/Accounts/AccountsProxy';
import UserSettings from '../../../../ApiClient/Accounts/Definitions/Responses/UserSettings';
import Notifications from '../../../../Notifications/Notifications';
import LoadingScreen from '../../../Shared/LoadingScreen';
import TabPanel, { a11yProps } from '../../../Shared/TabPanel';
import AccountSettingsIndex from './AccountSettings/AccountSettingsIndex';
import PermissionsSettings from './Permissions/PermissionsSettings';
import SystemSettings from './SystemSettings/SystemSettings';

function SettingsIndex(): ReactElement {
  const [tab, setTab] = useState(0);
  const [settings, setSettings] = useState<UserSettings | undefined>(undefined);
  const [savingSettings, setSavingSettings] = useState(false);
  const { t } = useTranslation('settings');

  useEffect(() => {
    void (async () => {
      setSettings((await AccountsProxy.settings.getUserSettings()).data);
    })();

    return () => {};
  }, []);

  const handleTabChange = (
    _event: React.SyntheticEvent,
    newTab: number
  ): void => {
    setTab(newTab);
  };
  return (
    <>
      <LoadingScreen isReady={!!settings}>
        <>
          <Formik
            initialValues={{ ...settings }}
            onSubmit={async (values) => {
              await AccountsProxy.settings.setSettings(values);
            }}
          >
            {(formik) => (
              <>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                  <Tabs value={tab} onChange={handleTabChange}>
                    <Tab label={t('accountSettings')} {...a11yProps(0)} />
                    <Tab label={t('permissionsSettings')} {...a11yProps(1)} />
                    <Tab label={t('systemSettings')} {...a11yProps(2)} />
                  </Tabs>
                </Box>
                <TabPanel value={tab} index={0}>
                  <AccountSettingsIndex formik={formik} />
                </TabPanel>
                <TabPanel value={tab} index={1}>
                  <PermissionsSettings />
                </TabPanel>
                <TabPanel value={tab} index={2}>
                  <SystemSettings />
                </TabPanel>
                <div className="d-flex justify-content-end">
                  <LoadingButton
                    variant="outlined"
                    loading={savingSettings}
                    onClick={async () => {
                      setSavingSettings(true);
                      await formik
                        .submitForm()
                        .then(() => {
                          Notifications.showChangesSavedNotification();
                        })
                        .catch(Notifications.showApiError);

                      setSavingSettings(false);
                    }}
                  >
                    {t('saveChanges')}
                  </LoadingButton>
                </div>
              </>
            )}
          </Formik>
        </>
      </LoadingScreen>
    </>
  );
}

export default SettingsIndex;
