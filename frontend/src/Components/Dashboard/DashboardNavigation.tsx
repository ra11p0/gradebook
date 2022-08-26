import React from 'react';
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

interface DashboardNavigationProps{
    onLogIn?: ()=>{};
    isLoggedIn: boolean;
    t: any;
    isStudent: boolean;
}

class DashboardNavigation extends React.Component<DashboardNavigationProps> {
    render(): React.ReactNode {
        const { t } = this.props;
        return (
            <div>
                <Nav className='d-flex gap-2 justify-content-end'>
                    <TeacherOnly>
                        <Link to='grades' className='btn btn-outline-primary'> {t('grades')}</Link>
                        <Link to='absence' className='btn btn-outline-primary'> {t('absence')}</Link>
                        <Link to='subject' className='btn btn-outline-primary'> {t('subjects')}</Link>
                        <Link to='timetable' className='btn btn-outline-primary'> {t('timetable')}</Link>
                    </TeacherOnly>
                    {
                        this.props.isStudent &&
                        <>
                            <Link to='grades' className='btn btn-outline-primary'> {t('grades')}</Link>
                            <Link to='absence' className='btn btn-outline-primary'> {t('absence')}</Link>
                            <Link to='subject' className='btn btn-outline-primary'> {t('subjects')}</Link>
                            <Link to='timetable' className='btn btn-outline-primary'> {t('timetable')}</Link>
                        </>
                    }
                    <SuperAdminOnly>
                            <Link to='manageStudents' className='btn btn-outline-primary'> {t('manageStudents')}</Link>
                            <Link to='manageTeachers' className='btn btn-outline-primary'> {t('manageTeachers')}</Link>
                            <Link to='manageSchool' className='btn btn-outline-primary'> {t('manageSchool')}</Link>
                    </SuperAdminOnly>
                </Nav>
            </div>
          );
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(DashboardNavigation));
