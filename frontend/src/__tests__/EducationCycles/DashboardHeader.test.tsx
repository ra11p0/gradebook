import { render } from '@testing-library/react';
import { I18nextProvider } from 'react-i18next';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum';
import Header from '../../Components/Dashboard/Manage/EducationCycle/CyclesList/Header';
import i18n from '../../i18n/config';
import setPermissionsRedux from '../../Redux/ReduxCommands/account/setPermissionsRedux';
import { store } from '../../store';

describe('<Header />', () => {
  it('Should not show `add new education cycle` button - EducationCycles_ViewOnly', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_ViewOnly],
    });
    const { queryByTestId } = render(
      <Provider store={store}>
        <BrowserRouter>
          <I18nextProvider i18n={i18n}>
            <Header />
          </I18nextProvider>
        </BrowserRouter>
      </Provider>
    );
    expect(queryByTestId('addNewEducationCycleButton')).toBeNull();
  });
  it('Should not show `add new education cycle` button - EducationCycles_NoAccess', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_NoAccess],
    });
    const { queryByTestId } = render(
      <Provider store={store}>
        <BrowserRouter>
          <I18nextProvider i18n={i18n}>
            <Header />
          </I18nextProvider>
        </BrowserRouter>
      </Provider>
    );
    expect(queryByTestId('addNewEducationCycleButton')).toBeNull();
  });
  it('Should show `add new education cycle` button - EducationCycles_CanCreateAndDelete', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_CanCreateAndDelete],
    });
    const { queryByTestId } = render(
      <Provider store={store}>
        <BrowserRouter>
          <I18nextProvider i18n={i18n}>
            <Header />
          </I18nextProvider>
        </BrowserRouter>
      </Provider>
    );
    expect(queryByTestId('addNewEducationCycleButton')).toBeTruthy();
  });
});
