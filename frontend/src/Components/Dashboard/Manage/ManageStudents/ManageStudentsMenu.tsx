import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
const mapStateToProps = (state: any) => ({});
const mapDispatchToProps = (dispatch: any) => ({});
interface ManageStudentsMenuProps { }
const ManageStudentsMenu = (props: ManageStudentsMenuProps): ReactElement => {
    const [activeTab, setActiveTab] = useState('studentsList');
    const { t } = useTranslation('manageStudentsMenu');
    return (
        <div className='d-flex flex-wrap gap-3'>
            <Link
                to='studentsList'
                className={'btn btn-outline-primary w-100 ' + (activeTab == 'studentsList' ? 'active' : '')}
                onClick={() => { setActiveTab('studentsList') }}>
                {t("studentsList")}
            </Link>
            <Link
                to='invitations'
                className={'btn btn-outline-primary w-100 ' + (activeTab == 'invitations' ? 'active' : '')}
                onClick={() => { setActiveTab('invitations') }}>
                {t("invitations")}
            </Link>
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(ManageStudentsMenu);