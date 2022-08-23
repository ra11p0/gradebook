import { connect } from 'react-redux';
import React from 'react';
import { Route, Routes } from 'react-router-dom';
import RegisterForm from '../Components/Account/RegisterForm';
import Profile from '../Components/Account/Profile';

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
                    <Route path="profile" element={<Profile />} />
                </Routes>
                </div>
            </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Account);
