import React from 'react';
import { render, screen, fireEvent, getByTestId } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import { act } from 'react-dom/test-utils';
import i18n from '../../i18n/config';
import { I18nextProvider } from 'react-i18next';
import LoginForm from '../../Components/Account/Login/LoginForm';
import userEvent from '@testing-library/user-event';
import '@testing-library/jest-dom';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';

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

describe('<LoginForm/>', () => {
  it('Should disable login button while logging in', async () => {
    jest.spyOn(AccountsProxy, 'logIn').mockImplementationOnce(() => {
      return new Promise(async (res, rej) => {
        await new Promise((res) => setTimeout(res, 1000));
        rej({});
      });
    });

    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <LoginForm />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'Email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'Login' }));
    });
    expect(screen.getByRole('button', { name: 'Login' })).toBeDisabled();
  });

  it('Should validate filled email empty', async () => {
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <LoginForm />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'Login' }));
    });
    expect(screen.getByRole('textbox', { name: 'Email' })).toHaveClass(
      'is-invalid'
    );
  });

  it('Should validate filled password empty', async () => {
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <LoginForm />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'Email' }),
        'fake@email.on'
      );
      fireEvent.click(screen.getByRole('button', { name: 'Login' }));
    });
    expect(screen.getByTestId('password')).toHaveClass('is-invalid');
  });

  it('Should show login failed', async () => {
    jest.spyOn(AccountsProxy, 'logIn').mockRejectedValueOnce({});

    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <LoginForm />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'Email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'Login' }));
    });
    expect(await screen.findByText('Login failed')).toBeTruthy();
  });
});
