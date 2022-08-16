import React from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Common/common';
import { Button, Nav, NavLink } from 'react-bootstrap';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: () => dispatch(logIn)
});

interface DashboardNavigationProps{
    onLogIn?: ()=>{},
    isLoggedIn: boolean
}

class DashboardNavigation extends React.Component<DashboardNavigationProps> {
    render(): React.ReactNode {
        return (
                <Nav className='d-flex gap-2 justify-content-end'>
                    <Link to='grades' className='btn btn-outline-primary'> oceny</Link>
                    <Link to='absence' className='btn btn-outline-primary'> nieobecnosci</Link>
                    <Link to='subject' className='btn btn-outline-primary'> przedmioty</Link>
                    <Link to='timetable' className='btn btn-outline-primary'> plan lekcji</Link>
                </Nav>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(DashboardNavigation);
