import '@testing-library/jest-dom';
import { act, fireEvent, render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { I18nextProvider } from 'react-i18next';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { openMenu, select } from 'react-select-event';
import { vi } from 'vitest';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import SubjectsProxy from '../../ApiClient/Subjects/SubjectsProxy';
import ConfigureEducationCycleForm from '../../Components/EducationCycle/ConfigureEducationCycle/ConfigureEducationCycleForm';
import i18n from '../../i18n/config';
import { store } from '../../store';

describe('<ConfigureEducationCycleForm />', () => {
  it('Should validate start and end dates empty', async () => {
    vi.spyOn(EducationCyclesProxy, 'getEducationCycle').mockResolvedValueOnce({
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

  it('Should send api request', async () => {
    vi.spyOn(EducationCyclesProxy, 'getEducationCycle').mockResolvedValueOnce({
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
    vi.spyOn(SubjectsProxy, 'getTeachersForSubject').mockResolvedValueOnce({
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

    const mockedConfigureEducationCycleForClass = vi
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
