import '@testing-library/jest-dom';
import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import * as setApplicationLanguageRedux from '../../../Redux/ReduxCommands/account/setApplicationLanguageRedux';
import HubProxy from '../../../ApiClient/SignalR/HubProxy';
import setLoginRedux from '../../../Redux/ReduxCommands/account/setLoginRedux';

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

vi.mock('@microsoft/signalr', () => ({
  HubConnectionBuilder: vi.fn().mockImplementation(() => ({
    withUrl: () => ({
      withAutomaticReconnect: () => ({
        build: () => ({
          on: () => ({}),
          stop: () => ({}),
          start: () =>
            new Promise<void>((resolve, reject) => {
              resolve();
            }),
        }),
      }),
    }),
  })),
}));

localStorage.setItem = vi.fn();

describe('setLogInRedux', () => {
  it('Should connect signalr hubs when loging in', async () => {
    vi.spyOn(AccountsProxy, 'logIn').mockResolvedValue({} as any);
    vi.spyOn(AccountsProxy, 'getMe').mockResolvedValue({
      data: { userId: 'fakeuid', schools: [] },
    } as any);
    vi.spyOn(AccountsProxy.settings, 'getUserSettings').mockResolvedValue({
      data: { language: 'en' },
    } as any);
    const connectMock = vi.spyOn(HubProxy.prototype, 'connect');
    await setLoginRedux({
      accessToken: 'fakeAccessToken',
      refreshToken: 'fakeRefreshToken',
    });
    expect(connectMock).toBeCalled();
  });

  it('Should set user language on log in', async () => {
    vi.spyOn(AccountsProxy, 'logIn').mockResolvedValue({} as any);
    vi.spyOn(AccountsProxy, 'getMe').mockResolvedValue({
      data: { userId: 'fakeuid', schools: [] },
    } as any);
    vi.spyOn(AccountsProxy.settings, 'getUserSettings').mockResolvedValue({
      data: { language: 'testLanguage' },
    } as any);
    const setApplicationLanguageReduxMocked = vi.spyOn(
      setApplicationLanguageRedux,
      'default'
    );
    await setLoginRedux({
      accessToken: 'fakeAccessToken',
      refreshToken: 'fakeRefreshToken',
    });
    expect(setApplicationLanguageReduxMocked).toBeCalledTimes(1);
    expect(setApplicationLanguageReduxMocked).toBeCalledWith('testLanguage');
  });

  it('Should save tokens to storage', async () => {
    vi.spyOn(AccountsProxy, 'logIn').mockResolvedValue({} as any);
    vi.spyOn(AccountsProxy, 'getMe').mockResolvedValue({
      data: { userId: 'fakeuid', schools: [] },
    } as any);
    vi.spyOn(AccountsProxy.settings, 'getUserSettings').mockResolvedValue({
      data: { language: 'testLanguage' },
    } as any);
    const setItemMock = vi.spyOn(Storage.prototype, 'setItem');
    await setLoginRedux({
      accessToken: 'fakeAccessToken',
      refreshToken: 'fakeRefreshToken',
    });
    expect(setItemMock).toBeCalledWith('access_token', 'fakeAccessToken');
    expect(setItemMock).toBeCalledWith('refresh_token', 'fakeRefreshToken');
  });
});
