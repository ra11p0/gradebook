import React from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: () => dispatch(logIn)
});

interface GradesProps{
    onLogIn?: ()=>{},
    isLoggedIn: boolean
}

class Grades extends React.Component<GradesProps> {
    render(): React.ReactNode {
        return (
            <div>
                d-board Grades
            </div>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Grades);
