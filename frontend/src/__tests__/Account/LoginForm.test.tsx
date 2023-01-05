import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
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
import Swal from 'sweetalert2';
import { vi } from 'vitest';
import { wait } from '@testing-library/user-event/dist/utils';

vi.mock('axios', () => ({
  default: {
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
  },
}));

describe('<LoginForm/>', () => {
  it('Should disable login button while logging in', async () => {
    vi.spyOn(AccountsProxy, 'logIn').mockImplementationOnce(
      async () => (await wait(1000)) as any
    );

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
    vi.spyOn(AccountsProxy, 'logIn').mockRejectedValueOnce({
      response: { status: 400 },
    });
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('error');
        return await Promise.resolve({} as any);
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
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'Email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'Login' }));
    });

    expect(swalMock).toBeCalledTimes(1);
  });

  it('Should show account inactive', async () => {
    vi.spyOn(AccountsProxy, 'logIn').mockRejectedValueOnce({
      response: { status: 302 },
    });
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('warning');
        return await Promise.resolve({} as any);
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
    await act(async () => {
      userEvent.type(
        screen.getByRole('textbox', { name: 'Email' }),
        'fake@email.on'
      );
      userEvent.type(screen.getByTestId('password'), 'fake@email.on');
      fireEvent.click(screen.getByRole('button', { name: 'Login' }));
    });

    expect(swalMock).toBeCalledTimes(1);
  });
});
