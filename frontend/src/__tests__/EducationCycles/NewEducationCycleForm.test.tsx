import React from 'react';
import { render, screen, fireEvent, getByTestId } from '@testing-library/react';
import '@testing-library/jest-dom';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import setPermissionsRedux from '../../Redux/ReduxCommands/account/setPermissionsRedux';
import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum';
import NewCycleForm from '../../Components/Dashboard/Manage/EducationCycle/NewCycleForm/NewCycleForm';
import { I18nextProvider } from 'react-i18next';
import i18n from '../../i18n/config';
import userEvent from '@testing-library/user-event';
import { act } from 'react-dom/test-utils';

describe('<NewCycleForm />', () => {
  it('Should validate name not empty', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_ViewOnly],
    });

    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <NewCycleForm />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      const nameField = screen.getByTestId('educationCycleName');
      const form = screen.getByTestId('newEducationCycleForm');
      if (!nameField) throw new Error('nameField is undefined');
      fireEvent.click(getByTestId(form, 'submit'));
    });
    await expect(await screen.findByTestId('educationCycleName')).toHaveClass(
      'is-invalid'
    );
  });

  it('Should validate name length', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_ViewOnly],
    });

    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <NewCycleForm />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      const nameField = screen.getByTestId('educationCycleName');
      userEvent.type(nameField, 're{enter}');
    });
    await expect(await screen.findByTestId('educationCycleName')).toHaveClass(
      'is-invalid'
    );
  });

  it('Should validate at least one stage', async () => {
    setPermissionsRedux(store.dispatch, {
      permissions: [PermissionLevelEnum.EducationCycles_ViewOnly],
    });

    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <NewCycleForm />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      const form = screen.getByTestId('newEducationCycleForm');
      const submit = getByTestId(form, 'submit');
      fireEvent.click(submit);
    });
    await expect(
      await screen.findByText('At least one step required')
    ).toBeTruthy();
  });
});
