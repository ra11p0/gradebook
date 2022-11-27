import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum';
import getPermissionsReduxProxy from '../../Redux/ReduxQueries/account/getPermissionsRedux';
import { connect } from 'react-redux';

import React, { ReactElement, useEffect, useState } from 'react';

interface Props {
  currentPermissions: PermissionLevelEnum[];
  allowingPermissions: PermissionLevelEnum[];
  children?: React.ReactNode;
}

function PermissionsBlocker(props: Props): ReactElement {
  const [canSee, setCanSee] = useState<boolean>(false);
  useEffect(() => {
    setCanSee(false);
    let canSee = false;
    props.currentPermissions.forEach((permission) => {
      if (props.allowingPermissions.includes(permission)) canSee = true;
    });
    setCanSee(canSee);
  }, [props.currentPermissions]);
  return <>{canSee && <>{props.children}</>}</>;
}

export default connect(
  (state) => ({
    currentPermissions: getPermissionsReduxProxy(state),
  }),
  () => ({})
)(PermissionsBlocker);
