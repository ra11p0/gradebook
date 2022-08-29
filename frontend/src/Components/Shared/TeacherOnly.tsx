import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
const mapStateToProps = (state: any) => ({ 
    isTeacher: state.common.session.roles.includes('Teacher')
});
const mapDispatchToProps = (dispatch: any) => ({ });
interface TeacherOnlyProps{ 
    children: any,
    isTeacher: boolean
}
const TeacherOnly = (props: TeacherOnlyProps): ReactElement => {
    const {t} = useTranslation();
    return (
        <>
            { props.isTeacher && props.children}
        </>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(TeacherOnly);