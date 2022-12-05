import React from 'react';
import { fireEvent, render, screen } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import { act } from 'react-dom/test-utils';
import i18n from '../../i18n/config';
import { I18nextProvider } from 'react-i18next';
import '@testing-library/jest-dom';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import { AxiosResponse } from 'axios';
import SettingsIndex from '../../Components/Dashboard/Manage/Settings/SettingsIndex';
import { ReactNotifications } from 'react-notifications-component';

describe('<SettingsIndex/>', () => {
  it('Should send request with settings changed', async () => {
    const setSettingsMock = jest
      .spyOn(AccountsProxy.settings, 'setSettings')
      .mockResolvedValueOnce({} as AxiosResponse);
    jest
      .spyOn(AccountsProxy.settings, 'getUserSettings')
      .mockResolvedValueOnce({
        data: { defaultSchool: 'ds' },
      } as AxiosResponse);
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <ReactNotifications />
              <SettingsIndex />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      fireEvent.click(screen.getByRole('button', { name: 'Save changes' }));
    });
    expect(setSettingsMock).toBeCalledTimes(1);
  });
});
