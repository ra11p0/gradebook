import '@testing-library/jest-dom';
import HubProxy from '../../../ApiClient/SignalR/HubProxy';
import setLogOutRedux from '../../../Redux/ReduxCommands/account/setLogOutRedux';

localStorage.removeItem = jest.fn();

describe('setLogOutRedux', () => {
  it('Should disconnect signalr hubs when loging out', async () => {
    const disconnectMock = jest.spyOn(HubProxy.prototype, 'disconnect');
    await setLogOutRedux();
    expect(disconnectMock).toBeCalled();
  });

  it('Should remove access token and refresh token from storage', async () => {
    const removeItemMock = jest.spyOn(Storage.prototype, 'removeItem');
    await setLogOutRedux();
    expect(removeItemMock).toBeCalledWith('access_token');
    expect(removeItemMock).toBeCalledWith('refresh_token');
  });
});
