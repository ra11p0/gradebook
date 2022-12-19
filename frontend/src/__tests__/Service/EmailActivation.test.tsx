import React from 'react';
import { render } from '@testing-library/react';
import * as routerDom from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import { act } from 'react-dom/test-utils';
import i18n from '../../i18n/config';
import { I18nextProvider } from 'react-i18next';
import '@testing-library/jest-dom';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import Swal from 'sweetalert2';
import EmailActivation from '../../Components/Service/EmailActivation';

vi.mock('react-router-dom', async () => ({
  ...(await vi.importActual('react-router-dom')),
  useParams: () => ({ accountGuid: 'aguid', activationCode: 'actcode' }),
  useNavigate: vi.fn(),
}));

describe('<EmailActivation/>', () => {
  it('Should show notification success', async () => {
    vi.spyOn(routerDom, 'useNavigate').mockImplementation(
      (await vi.importActual('react-router-dom')).useNavigate
    );
    vi.spyOn(AccountsProxy, 'verifyEmailAddress').mockResolvedValue({} as any);
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
              <EmailActivation />
            </I18nextProvider>
          </routerDom.BrowserRouter>
        </Provider>
      );
    });
    expect(swalMock).toBeCalledTimes(1);
  });
  it('Should show notification failed', async () => {
    vi.spyOn(routerDom, 'useNavigate').mockImplementation(
      (await vi.importActual('react-router-dom')).useNavigate
    );
    vi.spyOn(AccountsProxy, 'verifyEmailAddress').mockRejectedValue({} as any);
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('error');
        return await Promise.resolve({} as any);
      });

    await act(async () => {
      render(
        <Provider store={store}>
          <routerDom.BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <EmailActivation />
            </I18nextProvider>
          </routerDom.BrowserRouter>
        </Provider>
      );
    });
    expect(swalMock).toBeCalledTimes(1);
  });
  it('Should redirect home on error', async () => {
    vi.spyOn(AccountsProxy, 'verifyEmailAddress').mockRejectedValue({} as any);
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('error');
        return await Promise.resolve({} as any);
      });
    let hasBeenCalledNavigate = false;
    vi.spyOn(routerDom, 'useNavigate').mockImplementation(() => {
      return (to: any) => {
        hasBeenCalledNavigate = true;
        expect(to).toEqual('/');
      };
    });
    await act(async () => {
      render(
        <Provider store={store}>
          <routerDom.BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <EmailActivation />
            </I18nextProvider>
          </routerDom.BrowserRouter>
        </Provider>
      );
    });
    expect(hasBeenCalledNavigate).toBeTruthy();
    expect(swalMock).toBeCalledTimes(1);
  });
  it('Should redirect home on success', async () => {
    vi.spyOn(AccountsProxy, 'verifyEmailAddress').mockResolvedValueOnce(
      {} as any
    );
    const swalMock = vi
      .spyOn(Swal, 'fire')
      .mockImplementation(async (e: any) => {
        expect(e.icon).toEqual('success');
        return await Promise.resolve({} as any);
      });
    let hasBeenCalledNavigate = false;
    vi.spyOn(routerDom, 'useNavigate').mockImplementation(() => {
      return (to: any) => {
        hasBeenCalledNavigate = true;
        expect(to).toEqual('/');
      };
    });
    await act(async () => {
      render(
        <Provider store={store}>
          <routerDom.BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <EmailActivation />
            </I18nextProvider>
          </routerDom.BrowserRouter>
        </Provider>
      );
    });
    expect(hasBeenCalledNavigate).toBeTruthy();
    expect(swalMock).toBeCalledTimes(1);
  });
});
