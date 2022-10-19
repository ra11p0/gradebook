import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import Forms from './Components/Forms';
import ApplicationModes from './Constraints/ApplicationModes';

export { Forms, ApplicationModes };
export * from './'

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
