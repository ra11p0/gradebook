import '@testing-library/jest-dom';
import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import * as setApplicationLanguageRedux from '../../../Redux/ReduxCommands/account/setApplicationLanguageRedux';
import { AxiosResponse } from 'axios';
import setUserRedux from '../../../Redux/ReduxCommands/account/setUserRedux';

describe('setUserRedux', () => {
  it('Should get user settings and set language', async () => {
    const setLanguageReduxMock = vi
      .spyOn(setApplicationLanguageRedux, 'default')
      .mockResolvedValueOnce();
    const setLanguageMock = vi
      .spyOn(AccountsProxy.settings, 'getUserSettings')
      .mockResolvedValueOnce({
        data: {
          language: 'fakeLanguage',
        },
      } as AxiosResponse);

    await setUserRedux({
      userId: 'fakeUserId',
      isActive: true,
    });
    expect(setLanguageMock).toBeCalledTimes(1);
    expect(setLanguageReduxMock).toBeCalledTimes(1);
  });
});
