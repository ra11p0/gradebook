import React from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Common/common';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: () => dispatch(logIn)
});

interface DashboardIndexProps{
    onLogIn?: ()=>{},
    isLoggedIn: boolean
}

class DashboardIndex extends React.Component<DashboardIndexProps> {
    render(): React.ReactNode {
        return (
            <div>
                d-board index
            </div>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(DashboardIndex);
