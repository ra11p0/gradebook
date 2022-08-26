import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
const mapStateToProps = (state: any) => ({ 
    isSuperAdmin: state.common.session.roles.includes('SuperAdmin')
});
const mapDispatchToProps = (dispatch: any) => ({ });
interface SuperAdminOnlyProps{ 
    children: any,
    isSuperAdmin: boolean
}
const SuperAdminOnly = (props: SuperAdminOnlyProps): ReactElement => {
    const {t} = useTranslation();
    return (
        <>
            { props.isSuperAdmin && props.children}
        </>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(SuperAdminOnly);