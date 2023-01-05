import '@testing-library/jest-dom';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { act } from 'react-dom/test-utils';
import { I18nextProvider } from 'react-i18next';
import { Provider } from 'react-redux';
import * as routerDom from 'react-router-dom';
import Swal from 'sweetalert2';
import { vi } from 'vitest';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import ResetPassword from '../../Components/Service/ResetPassword';
import i18n from '../../i18n/config';
import { store } from '../../store';

vi.mock('react-router-dom', async () => ({
  ...((await vi.importActual('react-router-dom')) as any),
  useParams: () => ({ accountGuid: 'aguid', activationCode: 'actcode' }),
  useNavigate: vi.fn(),
}));

describe('<ResetPassword/>', () => {
  it('Should send api request', async () => {
    const mockedUseNavigate = vi
      .spyOn(routerDom, 'useNavigate')
      .mockImplementation(
        ((await vi.importActual('react-router-dom')) as any).useNavigate
      );
    const mockedSetNewPassword = vi
      .spyOn(AccountsProxy, 'setNewPassword')
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
              <ResetPassword />
            </I18nextProvider>
          </routerDom.BrowserRouter>
        </Provider>
      );
    });

    await act(async () => {
      userEvent.type(screen.getByTestId('newPassword'), '!QAZ2wsx');
      userEvent.type(screen.getByTestId('newPasswordConfirm'), '!QAZ2wsx');
    });
    await act(async () => {
      screen.getByRole('button', { name: 'setNewPassword' }).click();
    });
    expect(mockedSetNewPassword).toBeCalledTimes(1);
    expect(swalMock).toBeCalledWith({
      icon: 'success',
      title: 'passwordChanged',
    });
    expect(mockedUseNavigate).toBeCalled();
  });
  it('Should validate password confirmed', async () => {
    const mockedSetNewPassword = vi
      .spyOn(AccountsProxy, 'setNewPassword')
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
            <ResetPassword />
          </routerDom.BrowserRouter>
        </Provider>
      );
    });

    await act(async () => {
      userEvent.type(screen.getByTestId('newPassword'), '!QAZ2wsx');
      userEvent.type(screen.getByTestId('newPasswordConfirm'), '!QAZ2wsx!');

      screen.getByRole('button', { name: 'setNewPassword' }).click();
    });
    expect(mockedSetNewPassword).toBeCalledTimes(0);
    expect(swalMock).toBeCalledTimes(0);
    expect(screen.getByTestId('newPasswordConfirm')).toHaveClass('is-invalid');
  });
});
