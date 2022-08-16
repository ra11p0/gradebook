import { connect } from 'react-redux';
import React from 'react';
import { Navigate, Outlet, Route, Routes } from 'react-router-dom';
import RegisterForm from '../Components/Account/RegisterForm';

const mapStateToProps = (state: any) =>({
    isLoggedIn: state.common.isLoggedIn
});

const mapDispatchToProps = (dispatch: any) => ({

});

interface AccountProps{
    isLoggedIn?: boolean
}

class Account extends React.Component<AccountProps>{
    render(){
        return (
            <div className='m-3 card'>
                <div className='card-header'>
                    <label className='h4'> Konto </label>
                </div>
                <div className='card-body'>
                <Routes>
                    <Route path="register" element={<RegisterForm />} />
                </Routes>
                <Outlet/>
                </div>
            </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Account);
