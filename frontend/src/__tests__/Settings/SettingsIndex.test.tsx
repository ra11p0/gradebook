import '@testing-library/jest-dom';
import { fireEvent, render, screen } from '@testing-library/react';
import { AxiosResponse } from 'axios';
import { act } from 'react-dom/test-utils';
import { ReactNotifications } from 'react-notifications-component';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { vi } from 'vitest';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import SettingsIndex from '../../Components/Dashboard/Manage/Settings/SettingsIndex';
import { store } from '../../store';

describe('<SettingsIndex/>', () => {
  it('Should send request with settings', async () => {
    const setSettingsMock = vi
      .spyOn(AccountsProxy.settings, 'setSettings')
      .mockResolvedValueOnce({} as AxiosResponse);
    vi.spyOn(AccountsProxy.settings, 'getUserSettings').mockResolvedValueOnce({
      data: { defaultSchool: 'ds' },
    } as AxiosResponse);
    await act(() => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <ReactNotifications />
            <SettingsIndex />
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      fireEvent.click(screen.getByRole('button', { name: 'saveChanges' }));
    });
    expect(setSettingsMock).toBeCalledWith({ defaultSchool: 'ds' });
    expect(setSettingsMock).toBeCalledTimes(1);
  });
});
