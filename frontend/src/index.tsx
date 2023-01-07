import 'animate.css/animate.min.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'moment/dist/locale/pl';
import React, { Suspense } from 'react';
import 'react-datepicker/dist/react-datepicker-cssmodules.css';
import 'react-datepicker/dist/react-datepicker.css';
import ReactDOM from 'react-dom/client';
import { I18nextProvider } from 'react-i18next';
import 'react-notifications-component/dist/theme.css';
import { Provider } from 'react-redux';
import 'tippy.js/animations/scale.css';
import 'tippy.js/dist/tippy.css';
import './Common/Styles/Style.css';
import LoadingScreen from './Components/Shared/LoadingScreen';
import i18n from './i18n/config';
import { store } from './store';

const App = React.lazy(async () => await import('./App'));

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <Provider store={store}>
    <I18nextProvider i18n={i18n}>
      <Suspense fallback={<LoadingScreen isReady={false} />}>
        <App />
      </Suspense>
    </I18nextProvider>
  </Provider>
);
