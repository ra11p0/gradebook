import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import * as routerDom from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../../store';
import { act } from 'react-dom/test-utils';
import i18n from '../../../i18n/config';
import { I18nextProvider } from 'react-i18next';
import '@testing-library/jest-dom';
import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import Swal from 'sweetalert2';
import ChangePasswordLoggedIn from '../../../Components/Account/ChangePassword/ChangePasswordLoggedIn';
import { vi } from 'vitest';
import userEvent from '@testing-library/user-event';

vi.mock('react-router-dom', async () => ({
  ...((await vi.importActual('react-router-dom')) as any),
  useParams: () => ({ accountGuid: 'aguid', activationCode: 'actcode' }),
  useNavigate: vi.fn(),
}));

describe('<ChangePasswordLoggedIn/>', () => {
  it('Should send api request', async () => {
    const mockedSetNewPassword = vi
      .spyOn(AccountsProxy, 'setNewPasswordAuthorized')
      .mockResolvedValue({} as any);
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('success');
        return await Promise.resolve({} as any);
      });

    await act(async () => {
      render(
        <Provider store={store}>
          <routerDom.BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <ChangePasswordLoggedIn onRequestCompleted={() => {}} />
            </I18nextProvider>
          </routerDom.BrowserRouter>
        </Provider>
      );
    });

    await act(async () => {
      userEvent.type(screen.getByTestId('oldPassword'), '!QAZ2wsx');
      userEvent.type(screen.getByTestId('newPassword'), '!QAZ2wsx');
      userEvent.type(screen.getByTestId('newPasswordConfirm'), '!QAZ2wsx');
    });
    await act(async () => {
      screen.getByRole('button', { name: 'submit' }).click();
    });
    expect(mockedSetNewPassword).toBeCalledTimes(1);
    expect(swalMock).toBeCalledWith({
      icon: 'success',
      title: 'Password changed.',
    });
  });
  it('Should validate confirm password', async () => {
    const mockedSetNewPassword = vi
      .spyOn(AccountsProxy, 'setNewPasswordAuthorized')
      .mockResolvedValue({} as any);
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('success');
        return await Promise.resolve({} as any);
      });

    await act(async () => {
      render(
        <Provider store={store}>
          <routerDom.BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <ChangePasswordLoggedIn onRequestCompleted={() => {}} />
            </I18nextProvider>
          </routerDom.BrowserRouter>
        </Provider>
      );
    });

    await act(async () => {
      userEvent.type(screen.getByTestId('oldPassword'), '!QAZ2wsx');
      userEvent.type(screen.getByTestId('newPassword'), '!QAZ2wsx');
      userEvent.type(screen.getByTestId('newPasswordConfirm'), '!QAZ2wsx1');
    });
    await act(async () => {
      screen.getByRole('button', { name: 'submit' }).click();
    });
    expect(mockedSetNewPassword).toBeCalledTimes(0);
    expect(swalMock).toBeCalledTimes(0);
    expect(screen.getByTestId('newPasswordConfirm')).toHaveClass('is-invalid');
  });
  it('Should show invalid password', async () => {
    const mockedSetNewPassword = vi
      .spyOn(AccountsProxy, 'setNewPasswordAuthorized')
      .mockRejectedValue({ status: 403 } as any);
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('success');
        return await Promise.resolve({} as any);
      });

    await act(async () => {
      render(
        <Provider store={store}>
          <routerDom.BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <ChangePasswordLoggedIn onRequestCompleted={() => {}} />
            </I18nextProvider>
          </routerDom.BrowserRouter>
        </Provider>
      );
    });

    await act(async () => {
      userEvent.type(screen.getByTestId('oldPassword'), '!QAZ2wsx');
      userEvent.type(screen.getByTestId('newPassword'), '!QAZ2wsx');
      userEvent.type(screen.getByTestId('newPasswordConfirm'), '!QAZ2wsx');
    });
    await act(async () => {
      screen.getByRole('button', { name: 'submit' }).click();
    });
    expect(mockedSetNewPassword).toBeCalledTimes(1);
    expect(swalMock).toBeCalledTimes(0);
    expect(screen.getByTestId('oldPassword')).toHaveClass('is-invalid');
  });
});
