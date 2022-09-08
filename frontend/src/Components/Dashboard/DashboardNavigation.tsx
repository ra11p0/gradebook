import React, { useState } from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';
import { Nav } from 'react-bootstrap';
import { withTranslation } from 'react-i18next';
import SuperAdminOnly from '../Shared/SuperAdminOnly';
import TeacherOnly from '../Shared/TeacherOnly';

const mapStateToProps = (state: any) => ({
    isLoggedIn: state.common.isLoggedIn,
    isStudent: state.common.session?.roles.includes('Student')
});

const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: () => dispatch(logIn)
});

interface DashboardNavigationProps {
    onLogIn?: () => {};
    isLoggedIn: boolean;
    t: any;
    isStudent: boolean;
}
interface DashboardNavigationState {
    activeTab: string;
}

class DashboardNavigation extends React.Component<DashboardNavigationProps, DashboardNavigationState> {
    constructor(props: DashboardNavigationProps) {
        super(props);
        this.state = {
            activeTab: ''
        };
    }
    setActiveTab(tab: string) {
        this.setState({
            ...this.state,
            activeTab: tab
        });
    }
    render(): React.ReactNode {
        const { t } = this.props;
        return (
            <div>
                <Nav className='d-flex gap-2 justify-content-end'>
                    <TeacherOnly>
                        <Link
                            to='grades'
                            className={'btn btn-outline-primary ' + (this.state.activeTab == 'grades' ? 'active' : '')}
                            onClick={() => { this.setActiveTab('grades') }}>
                            {t('grades')}
                        </Link>
                        <Link
                            to='absence'
                            className={'btn btn-outline-primary ' + (this.state.activeTab == 'absence' ? 'active' : '')}
                            onClick={() => { this.setActiveTab('absence') }}>
                            {t('absence')}
                        </Link>
                        <Link
                            to='subject'
                            className={'btn btn-outline-primary ' + (this.state.activeTab == 'subject' ? 'active' : '')}
                            onClick={() => { this.setActiveTab('subject') }}>
                            {t('subjects')}
                        </Link>
                        <Link
                            to='timetable'
                            className={'btn btn-outline-primary ' + (this.state.activeTab == 'timetable' ? 'active' : '')}
                            onClick={() => { this.setActiveTab('timetable') }}>
                            {t('timetable')}
                        </Link>
                    </TeacherOnly>
                    {
                        this.props.isStudent &&
                        <>
                            <Link
                                to='grades'
                                className={'btn btn-outline-primary ' + (this.state.activeTab == 'grades' ? 'active' : '')}
                                onClick={() => { this.setActiveTab('grades') }}>
                                {t('grades')}
                            </Link>
                            <Link
                                to='absence'
                                className={'btn btn-outline-primary ' + (this.state.activeTab == 'absence' ? 'active' : '')}
                                onClick={() => { this.setActiveTab('absence') }}>
                                {t('absence')}
                            </Link>
                            <Link
                                to='subject'
                                className={'btn btn-outline-primary ' + (this.state.activeTab == 'subject' ? 'active' : '')}
                                onClick={() => { this.setActiveTab('subject') }}>
                                {t('subjects')}
                            </Link>
                            <Link to='timetable' className='btn btn-outline-primary'> {t('timetable')}</Link>
                        </>
                    }
                    <SuperAdminOnly>
                        <Link
                            to='manageStudents'
                            className={'btn btn-outline-primary ' + (this.state.activeTab == 'manageStudents' ? 'active' : '')}
                            onClick={() => { this.setActiveTab('manageStudents') }}>
                            {t('manageStudents')}
                        </Link>
                        <Link
                            to='manageTeachers'
                            className={'btn btn-outline-primary ' + (this.state.activeTab == 'manageTeachers' ? 'active' : '')}
                            onClick={() => { this.setActiveTab('manageTeachers') }}>
                            {t('manageTeachers')}
                        </Link>
                        <Link
                            to='manageSchool'
                            className={'btn btn-outline-primary ' + (this.state.activeTab == 'manageSchool' ? 'active' : '')}
                            onClick={() => { this.setActiveTab('manageSchool') }}>
                            {t('manageSchool')}
                        </Link>
                    </SuperAdminOnly>
                </Nav>
            </div>
        );
    }
}

export default withTranslation('dashboardNavigation')(connect(mapStateToProps, mapDispatchToProps)(DashboardNavigation));
