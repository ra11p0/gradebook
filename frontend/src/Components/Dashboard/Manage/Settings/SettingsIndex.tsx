import { Box, Tab, Tabs, Typography } from "@mui/material";
import React from "react";
import { useTranslation } from "react-i18next";
import AccountSettingsIndex from "./AccountSettings/AccountSettingsIndex";
import PermissionsSettings from "./Permissions/PermissionsSettings";
import SystemSettings from "./SystemSettings/SystemSettings";

type Props = {};

function SettingsIndex({}: Props) {
  const [value, setValue] = React.useState(0);
  const { t } = useTranslation("settings");

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  return (
    <>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs value={value} onChange={handleChange} aria-label="basic tabs example">
          <Tab label={t("accountSettings")} {...a11yProps(0)} />
          <Tab label={t("permissionsSettings")} {...a11yProps(1)} />
          <Tab label={t("systemSettings")} {...a11yProps(2)} />
        </Tabs>
      </Box>
      <TabPanel value={value} index={0}>
        <AccountSettingsIndex />
      </TabPanel>
      <TabPanel value={value} index={1}>
        <PermissionsSettings />
      </TabPanel>
      <TabPanel value={value} index={2}>
        <SystemSettings />
      </TabPanel>
    </>
  );
}

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div role="tabpanel" hidden={value !== index} id={`simple-tabpanel-${index}`} aria-labelledby={`simple-tab-${index}`} {...other}>
      {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
    </div>
  );
}

function a11yProps(index: number) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

export default SettingsIndex;
