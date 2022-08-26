import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
const mapStateToProps = (state: any) => ({ });
const mapDispatchToProps = (dispatch: any) => ({ });
interface ManageSchoolProps{ }
const ManageSchool = (props: ManageSchoolProps): ReactElement => {
    const {t} = useTranslation();
    return (
        <div>
            Manage school
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(ManageSchool);