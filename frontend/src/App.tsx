import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from './Components/Shared/Header';
import Index from './Routes/Index';
import Dashboard from './Routes/Dashboard';
import {appLoad} from './Actions/Account/accountActions';
import { connect } from 'react-redux';
import Account from './Routes/Account';

const mapStateToProps = (state: any) => {
  return {
    appLoaded: state.common.appLoaded
  }};

const mapDispatchToProps = (dispatch: any) => ({
  onLoad: () =>
    dispatch(appLoad),
});
interface AppProps{
  onLoad: ()=>{};
  appLoaded: boolean;
}

class App extends React.Component<AppProps>{
  componentDidMount() {
    this.props.onLoad();
  }
  render(): React.ReactNode {
    return (
      <div>
        <BrowserRouter>
          <Header/>
          <Routes>
            <Route path='*' element={<Index/>}/>
            <Route path="/dashboard/*" element={<Dashboard />} />
            <Route path="/account/*" element={<Account />} />
          </Routes>
        </BrowserRouter>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(App);;
