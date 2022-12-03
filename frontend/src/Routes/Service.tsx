import React, { ReactElement } from 'react';
import { Route, Routes } from 'react-router-dom';
import EmailActivation from '../Components/Service/EmailActivation';
import NavigateToUrl from '../Components/Shared/NavigateToUrl';

function Service(): ReactElement {
  return (
    <Routes>
      <Route
        path="account/:accountGuid/activation/:activationCode"
        element={<EmailActivation />}
      ></Route>
      <Route path="*" element={<NavigateToUrl url={'/'} />} />
    </Routes>
  );
}

export default Service;
