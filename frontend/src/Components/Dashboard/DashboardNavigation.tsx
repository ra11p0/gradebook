import React from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';
import { Nav } from 'react-bootstrap';
import { withTranslation } from 'react-i18next';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: () => dispatch(logIn)
});

interface DashboardNavigationProps{
    onLogIn?: ()=>{},
    isLoggedIn: boolean,
    t: any
}

class DashboardNavigation extends React.Component<DashboardNavigationProps> {
    render(): React.ReactNode {
        const { t } = this.props;
        return (
                <Nav className='d-flex gap-2 justify-content-end'>
                    <Link to='grades' className='btn btn-outline-primary'> {t('grades')}</Link>
                    <Link to='absence' className='btn btn-outline-primary'> {t('absence')}</Link>
                    <Link to='subject' className='btn btn-outline-primary'> {t('subjects')}</Link>
                    <Link to='timetable' className='btn btn-outline-primary'> {t('timetable')}</Link>
                </Nav>
          );
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(DashboardNavigation));
