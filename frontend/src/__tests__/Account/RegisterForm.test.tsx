import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import { act } from 'react-dom/test-utils';
import i18n from '../../i18n/config';
import { I18nextProvider } from 'react-i18next';
import RegisterForm from '../../Components/Account/Register/RegisterForm';
import userEvent from '@testing-library/user-event';
import '@testing-library/jest-dom';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import Notifications from '../../Notifications/Notifications';
import { ReactNotifications } from 'react-notifications-component';
import { vi } from 'vitest';

describe('<RegisterForm/>', () => {
  it('Should disable register button while registering', async () => {
    vi.spyOn(AccountsProxy, 'register').mockImplementationOnce(async () => {
      return await new Promise((resolve, reject) => {
        void new Promise((resolve) => setTimeout(resolve, 1000)).then(() => {
          reject(new Error());
        });
      });
    });

    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <RegisterForm />
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
      userEvent.type(screen.getByTestId('password'), 'fake@emaI2l.on');
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'I accept terms and conditions.' })
      );
      fireEvent.click(screen.getByRole('button', { name: 'Register' }));
    });
    expect(screen.getByRole('button', { name: 'Register' })).toBeDisabled();
  });

  it('Should validate filled email empty', async () => {
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <RegisterForm />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      userEvent.type(screen.getByTestId('password'), 'fake@emaI2l.on');
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'I accept terms and conditions.' })
      );
      fireEvent.click(screen.getByRole('button', { name: 'Register' }));
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
              <RegisterForm />
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
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'I accept terms and conditions.' })
      );
      fireEvent.click(screen.getByRole('button', { name: 'Register' }));
    });
    expect(screen.getByTestId('password')).toHaveClass('is-invalid');
  });

  it('Should validate filled passwords not matched', async () => {
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <RegisterForm />
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
      userEvent.type(screen.getByTestId('password'), 'fake@emaI2l.on');
      userEvent.type(screen.getByTestId('password2'), 'fake@emaId2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', {
          name: 'I accept terms and conditions.',
        })
      );
      fireEvent.click(screen.getByRole('button', { name: 'Register' }));
    });
    expect(screen.getByTestId('password2')).toHaveClass('is-invalid');
  });

  it('Should validate filled password empty', async () => {
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <RegisterForm />
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
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'I accept terms and conditions.' })
      );
      fireEvent.click(screen.getByRole('button', { name: 'Register' }));
    });
    expect(screen.getByTestId('password')).toHaveClass('is-invalid');
  });

  it('Should show error alert', async () => {
    const mockekdAccountsProxy = vi
      .spyOn(AccountsProxy, 'register')
      .mockRejectedValueOnce({ response: { data: 'fakeError' } });
    const mockedNotifications = vi.spyOn(Notifications, 'showApiError');
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <ReactNotifications />
              <RegisterForm />
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
      userEvent.type(screen.getByTestId('password'), 'fake@emaI2l.on');
      userEvent.type(screen.getByTestId('password2'), 'fake@emaI2l.on');
      fireEvent.click(
        screen.getByRole('checkbox', { name: 'I accept terms and conditions.' })
      );
      fireEvent.click(screen.getByRole('button', { name: 'Register' }));
    });
    expect(mockekdAccountsProxy).toBeCalledTimes(1);
    expect(mockedNotifications).toBeCalledTimes(1);
  });
});
