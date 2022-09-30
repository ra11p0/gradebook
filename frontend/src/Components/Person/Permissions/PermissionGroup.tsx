import React from "react";
import { useTranslation } from "react-i18next";
import PermissionEnum from "../../../Common/Enums/Permissions/PermissionEnum";
import PermissionLevelEnum from "../../../Common/Enums/Permissions/PermissionLevelEnum";
import PermissionField from "./PermissionField";

type Props = {
  permissions: {
    permissionId: PermissionEnum;
    permissionLevel: PermissionLevelEnum;
    permissionLevels: PermissionLevelEnum[];
  }[];
  onChange: (permissionId: PermissionEnum, permissionLevel: PermissionLevelEnum) => void;
};

function PermissionGroup(props: Props) {
  const { t } = useTranslation("permissions");
  return (
    <div>
      {props.permissions.map((permission, index) => (
        <PermissionField key={index} permission={permission} onChange={props.onChange} />
      ))}
    </div>
  );
}

export default PermissionGroup;
