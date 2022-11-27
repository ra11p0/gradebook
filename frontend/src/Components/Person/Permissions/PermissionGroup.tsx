import React, { ReactElement } from 'react';
import PermissionEnum from '../../../Common/Enums/Permissions/PermissionEnum';
import PermissionLevelEnum from '../../../Common/Enums/Permissions/PermissionLevelEnum';
import PermissionField from './PermissionField';

interface Props {
  permissions: Array<{
    permissionId: PermissionEnum;
    permissionLevel: PermissionLevelEnum;
    permissionLevels: PermissionLevelEnum[];
  }>;
  onChange: (
    permissionId: PermissionEnum,
    permissionLevel: PermissionLevelEnum
  ) => void;
}

function PermissionGroup(props: Props): ReactElement {
  return (
    <div>
      {props.permissions.map((permission, index) => (
        <PermissionField
          key={index}
          permission={permission}
          onChange={props.onChange}
        />
      ))}
    </div>
  );
}

export default PermissionGroup;
