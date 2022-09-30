import { Box, Button, Tab, Tabs } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import GetPermissionsResponse from "../../ApiClient/People/Definitions/GetPermissionsResponse";
import PeopleProxy from "../../ApiClient/People/PeopleProxy";
import PermissionEnum from "../../Common/Enums/Permissions/PermissionEnum";
import PermissionLevelEnum from "../../Common/Enums/Permissions/PermissionLevelEnum";
import Notifications from "../../Notifications/Notifications";
import TabPanel, { a11yProps } from "../Shared/TabPanel";
import PermissionGroup from "./Permissions/PermissionGroup";

type Props = {
  personGuid: string;
};

const permissionsToSave = new Map<PermissionEnum, PermissionLevelEnum>();

function PersonPermissions(props: Props) {
  const [tab, setTab] = useState(0);
  const [permissions, setPermissions] = useState<GetPermissionsResponse[]>([]);
  const { t } = useTranslation("permissions");

  const handleTabChange = (event: React.SyntheticEvent, newTab: number) => {
    setTab(newTab);
  };

  useEffect(() => {
    PeopleProxy.permissions.getPermissions(props.personGuid).then((permissionsResponse) => {
      setPermissions(permissionsResponse.data);
    });
  }, [props.personGuid]);

  const onPermissionChanged = (permissionId: PermissionEnum, permissionLevel: PermissionLevelEnum) => {
    permissionsToSave.set(permissionId, permissionLevel);
    permissions.find((p) => p.permissionId === permissionId)!.permissionLevel = permissionLevel;
  };

  return (
    <>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs value={tab} onChange={handleTabChange}>
          <Tab label={t("system")} {...a11yProps(0)} />
          <Tab className="administrationPermissions" label={t("administration")} {...a11yProps(1)} />
        </Tabs>
      </Box>
      <TabPanel value={tab} index={tab}>
        <PermissionGroup
          permissions={permissions.filter((permission) => permission.permissionGroup === tab + 1)}
          onChange={onPermissionChanged}
        />
      </TabPanel>
      <div className="d-flex justify-content-end m-2 p-2">
        <Button
          className="savePermissionsButton"
          variant="outlined"
          onClick={() => {
            PeopleProxy.permissions
              .setPermissions(
                props.personGuid,
                Array.from(permissionsToSave).map((permission) => ({ permissionId: permission[0], permissionLevel: permission[1] }))
              )
              .then(() => Notifications.showSuccessNotification("success", "permissionsSaved"))
              .catch(Notifications.showApiError);
          }}
        >
          {t("save")}
        </Button>
      </div>
    </>
  );
}

export default PersonPermissions;
