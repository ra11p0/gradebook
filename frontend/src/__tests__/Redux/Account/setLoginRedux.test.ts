import '@testing-library/jest-dom';
import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import * as setApplicationLanguageRedux from '../../../Redux/ReduxCommands/account/setApplicationLanguageRedux';
import HubProxy from '../../../ApiClient/SignalR/HubProxy';
import setLoginRedux from '../../../Redux/ReduxCommands/account/setLoginRedux';

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

localStorage.setItem = jest.fn();

describe('setLogInRedux', () => {
  it('Should connect signalr hubs when loging in', async () => {
    jest.spyOn(AccountsProxy, 'logIn').mockResolvedValue({} as any);
    jest
      .spyOn(AccountsProxy, 'getMe')
      .mockResolvedValue({ data: { userId: 'fakeuid', schools: [] } } as any);
    jest
      .spyOn(AccountsProxy.settings, 'getUserSettings')
      .mockResolvedValue({ data: { language: 'en' } } as any);
    const connectMock = jest.spyOn(HubProxy.prototype, 'connect');
    await setLoginRedux({
      accessToken: 'fakeAccessToken',
      refreshToken: 'fakeRefreshToken',
    });
    expect(connectMock).toBeCalled();
  });

  it('Should set user language on log in', async () => {
    jest.spyOn(AccountsProxy, 'logIn').mockResolvedValue({} as any);
    jest
      .spyOn(AccountsProxy, 'getMe')
      .mockResolvedValue({ data: { userId: 'fakeuid', schools: [] } } as any);
    jest
      .spyOn(AccountsProxy.settings, 'getUserSettings')
      .mockResolvedValue({ data: { language: 'testLanguage' } } as any);
    const setApplicationLanguageReduxMocked = jest.spyOn(
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
    jest.spyOn(AccountsProxy, 'logIn').mockResolvedValue({} as any);
    jest
      .spyOn(AccountsProxy, 'getMe')
      .mockResolvedValue({ data: { userId: 'fakeuid', schools: [] } } as any);
    jest
      .spyOn(AccountsProxy.settings, 'getUserSettings')
      .mockResolvedValue({ data: { language: 'testLanguage' } } as any);
    const setItemMock = jest.spyOn(Storage.prototype, 'setItem');
    await setLoginRedux({
      accessToken: 'fakeAccessToken',
      refreshToken: 'fakeRefreshToken',
    });
    expect(setItemMock).toBeCalledWith('access_token', 'fakeAccessToken');
    expect(setItemMock).toBeCalledWith('refresh_token', 'fakeRefreshToken');
  });
});
