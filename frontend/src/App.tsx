import React from 'react';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import Header from './Components/Shared/Header';
import Index from './Routes/Index';
import Dashboard from './Routes/Dashboard';
import {appLoad} from './Actions/Account/accountActions';
import { connect } from 'react-redux';
import Account from './Routes/Account';
import ActivateAccount from './Components/Account/ActivateAccount';
import RegisterForm from './Components/Account/RegisterForm';
import { ReactNotifications } from 'react-notifications-component';

const mapStateToProps = (state: any) => {
  return {
    appLoaded: state.common.appLoaded,
    isLoggedIn: state.common.isLoggedIn,
    isUserActivated: state.common.session?.roles.length != 0
  }};

const mapDispatchToProps = (dispatch: any) => ({
  onLoad: () =>
    dispatch(appLoad),
});
interface AppProps{
  onLoad: ()=>{};
  appLoaded: boolean;
  isLoggedIn: boolean,
  isUserActivated: boolean
}

class App extends React.Component<AppProps>{
  componentDidMount() {
    this.props.onLoad();
  }
  render(): React.ReactNode {
    return (
      <div>
        <BrowserRouter>
        <ReactNotifications/>
          <Header/>
          <Routes>
            {// only for logged in and activated
              this.props.isLoggedIn && this.props.isUserActivated &&
              <>
                <Route path="*" element={<Dashboard />} />
                <Route path="/*" element={<Dashboard />} />
                <Route path="/account/*" element={<Account />} />
                <Route path="/dashboard/*" element={<Dashboard />} />
              </>
            }
            {//Only for logged in and inactive
              this.props.isLoggedIn && !this.props.isUserActivated &&
              <Route path='*' element={<ActivateAccount/>}/>
            }
            {//Public
              !this.props.isLoggedIn &&
              <>
                <Route path="/account/register" element={<RegisterForm />} />
                <Route path='*' element={<Index/>}/>
              </>
            }
          </Routes>
        </BrowserRouter>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(App);;
