import PermissionLevelEnum from "../../Common/Enums/Permissions/PermissionLevelEnum";
import getPermissionsReduxProxy from "../../Redux/ReduxQueries/account/getPermissionsRedux";
import { connect } from "react-redux";

import React, { useEffect, useState } from "react";

type Props = {
  currentPermissions: PermissionLevelEnum[];
  allowingPermissions: PermissionLevelEnum[];
  children?: React.ReactNode;
};

function PermissionsBlocker(props: Props) {
  const [canSee, setCanSee] = useState<boolean>(false);
  useEffect(() => {
    setCanSee(false);
    let canSee = false;
    props.currentPermissions.forEach((permission) => {
      if (props.allowingPermissions.includes(permission)) canSee = true;
    });
    setCanSee(canSee);
  }, [props.currentPermissions]);
  return <>{canSee && <div>{props.children}</div>}</>;
}

export default connect(
  (state) => ({
    currentPermissions: getPermissionsReduxProxy(state),
  }),
  () => ({})
)(PermissionsBlocker);
