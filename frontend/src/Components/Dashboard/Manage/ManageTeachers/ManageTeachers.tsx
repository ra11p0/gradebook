import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
const mapStateToProps = (state: any) => ({ });
const mapDispatchToProps = (dispatch: any) => ({ });
interface ManageTeachersProps{ }
const ManageTeachers = (props: ManageTeachersProps): ReactElement => {
    const {t} = useTranslation();
    return (
        <div>
            ManageTeachers
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(ManageTeachers);