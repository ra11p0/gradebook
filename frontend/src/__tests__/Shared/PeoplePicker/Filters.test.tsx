import { act, render, screen } from '@testing-library/react';
import React from 'react';
import { I18nextProvider } from 'react-i18next';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import i18n from '../../../i18n/config';
import { store } from '../../../store';
import Filters from '../../../Components/Shared/PeoplePicker/Filters';
import PeoplePickerData from '../../../ApiClient/People/Definitions/Requests/PeoplePickerData';
import SchoolProxy from '../../../ApiClient/Schools/SchoolsProxy';
import { vi } from 'vitest';
import { openMenu, select } from 'react-select-event';

describe('<Filters />', () => {
  it('Class filter should call back on change', async () => {
    let mockedFn = vi.fn();
    vi.spyOn(SchoolProxy, 'getClassesInSchool').mockResolvedValue({
      data: [
        {
          name: 'first',
          guid: '1',
        },
        {
          name: 'second',
          guid: '2',
        },
        {
          name: 'third',
          guid: '3',
        },
      ],
    } as any);
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <Filters
                onChange={function (pickerData: PeoplePickerData): void {
                  mockedFn();
                }}
              />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    expect(mockedFn).toBeCalledTimes(1);

    const classFilterField = screen
      .getAllByRole('combobox')
      .find((e) => e.id == 'classFilter');

    await act(async () => {
      openMenu(classFilterField!);
    });
    await act(async () => {
      await select(classFilterField!, 'first');
    });
    expect(mockedFn).toBeCalledTimes(2);
  });

  it('Role filter should call back on change', async () => {
    let mockedFn = vi.fn();
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <Filters
                onChange={function (pickerData: PeoplePickerData): void {
                  mockedFn();
                }}
              />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    expect(mockedFn).toBeCalledTimes(1);

    const classFilterField = screen
      .getAllByRole('combobox')
      .find((e) => e.id == 'classFilter');

    await act(async () => {
      openMenu(classFilterField!);
    });
    await act(async () => {
      await select(classFilterField!, 'first');
    });
    expect(mockedFn).toBeCalledTimes(2);
  });
});
