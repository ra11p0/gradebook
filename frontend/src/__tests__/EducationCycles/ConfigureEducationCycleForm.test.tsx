import React from 'react';
import { render, screen, fireEvent, act } from '@testing-library/react';
import '@testing-library/jest-dom';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import { I18nextProvider } from 'react-i18next';
import i18n from '../../i18n/config';
import userEvent from '@testing-library/user-event';
import ConfigureEducationCycleForm from '../../Components/EducationCycle/ConfigureEducationCycle/ConfigureEducationCycleForm';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import SubjectsProxy from '../../ApiClient/Subjects/SubjectsProxy';
import { select, openMenu } from 'react-select-event';

describe('<ConfigureEducationCycleForm />', () => {
  it('Should validate start and end dates empty', async () => {
    jest
      .spyOn(EducationCyclesProxy, 'getEducationCycle')
      .mockResolvedValueOnce({
        data: {
          guid: '',
          stages: [],
        },
      } as any);

    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <ConfigureEducationCycleForm
                educationCycleGuid={'fakeEducationCycleGuid'}
                classGuid={'fakeClassGuid'}
                onSubmit={() => {}}
              />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(() => {
      fireEvent.click(screen.getByRole('button', { name: 'Confirm' }));
    });

    await expect(
      await screen.findByRole('textbox', { name: 'Start date' })
    ).toHaveClass('is-invalid');
    await expect(
      await screen.findByRole('textbox', { name: 'End date' })
    ).toHaveClass('is-invalid');
  });
  jest.setTimeout(10000);
  it('Should send api request', async () => {
    jest
      .spyOn(EducationCyclesProxy, 'getEducationCycle')
      .mockResolvedValueOnce({
        data: {
          guid: '',
          stages: [
            {
              guid: 'string',
              name: 'string',
              order: 1,
              subjects: [
                {
                  guid: 'string',
                  subjectGuid: 'string',
                  hoursInStep: 20,
                  isMandatory: false,
                  groupsAllowed: true,
                  subjectName: 'string',
                },
              ],
            },
          ],
        },
      } as any);
    jest.spyOn(SubjectsProxy, 'getTeachersForSubject').mockResolvedValueOnce({
      data: [
        {
          guid: 'fakeGuid',
          creatorGuid: 'string',
          userGuid: 'string',
          name: 'fakeName',
          surname: 'fakeSurname',
          schoolRole: 1,
          isActive: true,
        },
      ],
    } as any);

    const mockedConfigureEducationCycleForClass = jest
      .spyOn(ClassesProxy.educationCycles, 'configureEducationCycleForClass')
      .mockResolvedValue({} as any);

    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <ConfigureEducationCycleForm
                educationCycleGuid={'fakeEducationCycleGuid'}
                classGuid={'fakeClassGuid'}
                onSubmit={() => {}}
              />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        (await screen.findAllByRole('textbox', { name: 'End date' }))[0],
        '04/04/2022{enter}'
      );
    });
    await act(async () => {
      userEvent.type(
        (await screen.findAllByRole('textbox', { name: 'Start date' }))[0],
        '01/01/2022{enter}'
      );
    });
    await act(async () => {
      userEvent.type(
        (await screen.findAllByRole('textbox', { name: 'End date' }))[1],
        '03/03/2022{enter}'
      );
    });
    await act(async () => {
      userEvent.type(
        (await screen.findAllByRole('textbox', { name: 'Start date' }))[1],
        '05/03/2022{enter}'
      );
    });
    const selectField = await screen.findByRole('combobox', { name: '' });
    await act(async () => {
      await openMenu(selectField);
    });
    await act(async () => {
      await select(selectField, 'fakeName fakeSurname');
    });
    await act(async () => {
      fireEvent.click(screen.getByRole('button', { name: 'Confirm' }));
    });

    expect(mockedConfigureEducationCycleForClass).toBeCalledTimes(1);
  });
});
