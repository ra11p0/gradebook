import React from 'react';
import { render, screen } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import { act } from 'react-dom/test-utils';
import i18n from '../../i18n/config';
import { I18nextProvider } from 'react-i18next';
import userEvent from '@testing-library/user-event';
import '@testing-library/jest-dom';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import LanguageSelect from '../../Components/Shared/Header/LanguageSelect';

jest.mock('axios', () => ({
  create: () => ({
    interceptors: {
      response: {
        use: () => ({}),
      },
      request: {
        use: () => ({}),
      },
    },
  }),
}));

describe('<LanguageSelect/>', () => {
  it('Should send request with language changed', async () => {
    const setLanguageMock = jest
      .spyOn(AccountsProxy.settings, 'setLanguage')
      .mockResolvedValue({});

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
        await screen.findByRole('button', { name: 'Polish (Polish)' })
      );
    });
    expect(setLanguageMock).toBeCalledTimes(1);
  });
});
