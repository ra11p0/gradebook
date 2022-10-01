import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum'
import getPermissionsReduxProxy from '../../Redux/ReduxProxy/getPermissionsReduxProxy'
import { connect } from 'react-redux'

import React, { useEffect, useState } from 'react'

type Props = {
    currentPermissions: PermissionLevelEnum[];
    permissions: PermissionLevelEnum[];
    children?: React.ReactNode;
}

function PermissionsBlocker(props: Props) {
    const [canSee, setCanSee] = useState<boolean>(false);
    useEffect(() => {
        setCanSee(false);
        let canSee = true;
        props.permissions.forEach((permission) => {
            if (!props.currentPermissions.includes(permission)) canSee = false;
        })
        setCanSee(canSee);
    }, [props.currentPermissions]);
    return (
        <>
            {
                canSee && <div>
                    {props.children}
                </div>
            }
        </>
    )
}

export default connect((state) => ({
    currentPermissions: getPermissionsReduxProxy(state)
}), () => ({}))(PermissionsBlocker);