import '@testing-library/jest-dom';
import { vi } from 'vitest';
import HubProxy from '../../../ApiClient/SignalR/HubProxy';
import setLogOutRedux from '../../../Redux/ReduxCommands/account/setLogOutRedux';

localStorage.removeItem = vi.fn();

describe('setLogOutRedux', () => {
  it('Should disconnect signalr hubs when loging out', async () => {
    const disconnectMock = vi.spyOn(HubProxy.prototype, 'disconnect');
    await setLogOutRedux();
    expect(disconnectMock).toBeCalled();
  });

  it('Should remove access token and refresh token from storage', async () => {
    const removeItemMock = vi.spyOn(Storage.prototype, 'removeItem');
    await setLogOutRedux();
    expect(removeItemMock).toBeCalledWith('access_token');
    expect(removeItemMock).toBeCalledWith('refresh_token');
  });
});
