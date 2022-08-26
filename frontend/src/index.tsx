import 'bootstrap/dist/css/bootstrap.css';
import 'react-notifications-component/dist/theme.css'
import 'animate.css/animate.min.css';
import 'react-datepicker/dist/react-datepicker-cssmodules.css';
import "react-datepicker/dist/react-datepicker.css";
import './i18n/config';
import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { Provider } from 'react-redux';
import { store } from './store';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <Provider store={store}>
    <App/>
  </Provider>
);
