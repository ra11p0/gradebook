import '@testing-library/jest-dom';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { AxiosResponse } from 'axios';
import { act } from 'react-dom/test-utils';
import { I18nextProvider } from 'react-i18next';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { vi } from 'vitest';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import LanguageSelect from '../../Components/Shared/Header/LanguageSelect';
import i18n from '../../i18n/config';
import * as setApplicationLanguageRedux from '../../Redux/ReduxCommands/account/setApplicationLanguageRedux';
import * as getIsLoggedInRedux from '../../Redux/ReduxQueries/account/getIsLoggedInRedux';
import { store } from '../../store';

describe('<LanguageSelect/>', () => {
  it('Should send request with language changed', async () => {
    const setLanguageMock = vi
      .spyOn(AccountsProxy.settings, 'setLanguage')
      .mockResolvedValueOnce({} as AxiosResponse);
    const setLanguageReduxMock = vi
      .spyOn(setApplicationLanguageRedux, 'default')
      .mockResolvedValueOnce();
    const getIsLoggedInReduxMock = vi
      .spyOn(getIsLoggedInRedux, 'default')
      .mockReturnValueOnce(true);
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <LanguageSelect />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.click(screen.getByRole('button'));
    });
    await act(async () => {
      await userEvent.click(
        await screen.findByRole('button', { name: 'polish (Polish)' })
      );
    });
    expect(setLanguageMock).toBeCalledTimes(1);
    expect(setLanguageReduxMock).toBeCalledTimes(1);
    expect(getIsLoggedInReduxMock).toBeCalledTimes(1);
  });
  it('Should not send request, only local change - user not logged in', async () => {
    const setLanguageMock = vi
      .spyOn(AccountsProxy.settings, 'setLanguage')
      .mockResolvedValueOnce({} as AxiosResponse);
    const setLanguageReduxMock = vi
      .spyOn(setApplicationLanguageRedux, 'default')
      .mockResolvedValueOnce();
    const getIsLoggedInReduxMock = vi
      .spyOn(getIsLoggedInRedux, 'default')
      .mockReturnValueOnce(false);
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <LanguageSelect />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.click(screen.getByRole('button'));
    });
    await act(async () => {
      userEvent.click(
        await screen.findByRole('button', { name: 'polish (Polish)' })
      );
    });
    expect(getIsLoggedInReduxMock).toBeCalledTimes(1);
    expect(setLanguageMock).toBeCalledTimes(0);
    expect(setLanguageReduxMock).toBeCalledTimes(1);
  });
});
