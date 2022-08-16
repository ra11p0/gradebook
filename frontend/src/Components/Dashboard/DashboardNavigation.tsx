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
                    <a className='btn btn-outline-primary'> oceny</a>
                    <a className='btn btn-outline-primary'> nieobecnosci</a>
                    <a className='btn btn-outline-primary'> przedmioty</a>
                    <a className='btn btn-outline-primary'> plan lekcji</a>
                    <a className='btn btn-outline-primary'> cos tam</a>
                </Nav>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(DashboardNavigation);
