import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
const mapStateToProps = (state: any) => ({ });
const mapDispatchToProps = (dispatch: any) => ({ });
interface ManageStudentsMenuProps{ }
const ManageStudentsMenu = (props: ManageStudentsMenuProps): ReactElement => {
    const {t} = useTranslation();
    return (
        <div className='d-flex flex-wrap gap-3'>
            <Link to='studentsList' className='btn btn-outline-primary w-100'>{t("studentsList")}</Link>
            <Link to='invitations' className='btn btn-outline-primary w-100'>{t("invitations")}</Link>
            <Link to='invitations' className='btn btn-outline-primary w-100'>C</Link>
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(ManageStudentsMenu);