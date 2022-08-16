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

interface SubjectProps{
    onLogIn?: ()=>{},
    isLoggedIn: boolean
}

class Subject extends React.Component<SubjectProps> {
    render(): React.ReactNode {
        return (
            <div>
                d-board Subject
            </div>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Subject);
