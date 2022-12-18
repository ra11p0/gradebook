import React from 'react';
import { render } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import setPermissionsRedux from '../../Redux/ReduxCommands/account/setPermissionsRedux';
import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum';
import DashboardNavigation from '../../Components/Dashboard/DashboardNavigation';
import i18n from '../../i18n/config';
import { I18nextProvider } from 'react-i18next';

describe('<DashboardNavigation />', () => {
  it('Should not show `education cycle` button - EducationCycles_NoAccess', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_NoAccess],
    });
    const { queryByTestId } = render(
      <Provider store={store}>
        <BrowserRouter>
          <I18nextProvider i18n={i18n}>
            <DashboardNavigation />
          </I18nextProvider>
        </BrowserRouter>
      </Provider>
    );
    expect(queryByTestId('educationCycleButton')).toBeNull();
  });
  it('Should show `education cycle` button - EducationCycles_ViewOnly', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_ViewOnly],
    });
    const { queryByTestId } = render(
      <Provider store={store}>
        <BrowserRouter>
          <I18nextProvider i18n={i18n}>
            <DashboardNavigation />
          </I18nextProvider>
        </BrowserRouter>
      </Provider>
    );
    expect(queryByTestId('educationCycleButton')).toBeTruthy();
  });
  it('Should show `education cycle` button - EducationCycles_CanCreateAndDelete', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_CanCreateAndDelete],
    });
    const { queryByTestId } = render(
      <Provider store={store}>
        <BrowserRouter>
          <I18nextProvider i18n={i18n}>
            <DashboardNavigation />
          </I18nextProvider>
        </BrowserRouter>
      </Provider>
    );
    expect(queryByTestId('educationCycleButton')).toBeTruthy();
  });
});
