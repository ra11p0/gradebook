import { Box, Tab, Tabs } from '@mui/material';
import React, { ReactElement } from 'react';
import { useTranslation } from 'react-i18next';
import TabPanel, { a11yProps } from '../../../Shared/TabPanel';
import AccountSettingsIndex from './AccountSettings/AccountSettingsIndex';
import PermissionsSettings from './Permissions/PermissionsSettings';
import SystemSettings from './SystemSettings/SystemSettings';

function SettingsIndex(): ReactElement {
  const [tab, setTab] = React.useState(0);
  const { t } = useTranslation('settings');

  const handleTabChange = (
    _event: React.SyntheticEvent,
    newTab: number
  ): void => {
    setTab(newTab);
  };
  return (
    <>
      <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
        <Tabs value={tab} onChange={handleTabChange}>
          <Tab label={t('accountSettings')} {...a11yProps(0)} />
          <Tab label={t('permissionsSettings')} {...a11yProps(1)} />
          <Tab label={t('systemSettings')} {...a11yProps(2)} />
        </Tabs>
      </Box>
      <TabPanel value={tab} index={0}>
        <AccountSettingsIndex />
      </TabPanel>
      <TabPanel value={tab} index={1}>
        <PermissionsSettings />
      </TabPanel>
      <TabPanel value={tab} index={2}>
        <SystemSettings />
      </TabPanel>
    </>
  );
}

export default SettingsIndex;
