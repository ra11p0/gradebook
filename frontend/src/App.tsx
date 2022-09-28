import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Header from "./Components/Shared/Header";
import Index from "./Routes/Index";
import Dashboard from "./Routes/Dashboard";
import { connect } from "react-redux";
import Account from "./Routes/Account";
import ActivateAccount from "./Components/Account/ActivateAccount";
import RegisterForm from "./Components/Account/RegisterForm";
import { ReactNotifications } from "react-notifications-component";
import School from "./Routes/School";
import Student from "./Routes/Student";
import Person from "./Routes/Person";
import Class from "./Routes/Class";
import { isLoggedInProxy } from "./Redux/ReduxProxy/getIsLoggedInReduxProxy";
import { isUserActivatedProxy } from "./Redux/ReduxProxy/getIsUserActivatedReduxProxy";
import { appLoadWrapper } from "./Redux/ReduxWrappers/setAppLoadReduxWrapper";

const mapStateToProps = (state: any) => {
  return {
    appLoaded: state.common.appLoaded,
    isLoggedIn: isLoggedInProxy(state),
    isUserActivated: isUserActivatedProxy(state),
  };
};

const mapDispatchToProps = (dispatch: any) => ({
  onLoad: () => appLoadWrapper(dispatch),
});
interface AppProps {
  onLoad: () => {};
  appLoaded: boolean;
  isLoggedIn: boolean;
  isUserActivated: boolean;
}

class App extends React.Component<AppProps> {
  componentDidMount() {
    this.props.onLoad();
  }
  render(): React.ReactNode {
    return (
      <div>
        <BrowserRouter>
          <ReactNotifications />
          <Header />
          <Routes>
            {
              // only for logged in and activated
              this.props.isLoggedIn && this.props.isUserActivated && (
                <>
                  <Route path="*" element={<Dashboard />} />
                  <Route path="/*" element={<Dashboard />} />
                  <Route path="/account/*" element={<Account />} />
                  <Route path="/dashboard/*" element={<Dashboard />} />
                  <Route path="/school/*" element={<School />} />
                  <Route path="/student/*" element={<Student />} />
                  <Route path="/person/*" element={<Person />} />
                  <Route path="/class/*" element={<Class />} />
                </>
              )
            }
            {
              //Only for logged in and inactive
              this.props.isLoggedIn && !this.props.isUserActivated && <Route path="*" element={<ActivateAccount />} />
            }
            {
              //Public
              !this.props.isLoggedIn && (
                <>
                  <Route path="/account/register" element={<RegisterForm />} />
                  <Route path="*" element={<Index />} />
                </>
              )
            }
          </Routes>
        </BrowserRouter>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(App);
