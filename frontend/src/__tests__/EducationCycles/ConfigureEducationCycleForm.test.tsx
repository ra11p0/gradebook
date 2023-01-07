import '@testing-library/jest-dom';
import { act, fireEvent, render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { Provider } from 'react-redux';
import { openMenu, select } from 'react-select-event';
import { vi } from 'vitest';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import SubjectsProxy from '../../ApiClient/Subjects/SubjectsProxy';
import ConfigureEducationCycleForm from '../../Components/EducationCycle/ConfigureEducationCycle/ConfigureEducationCycleForm';
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
          <ConfigureEducationCycleForm
            educationCycleGuid={'fakeEducationCycleGuid'}
            classGuid={'fakeClassGuid'}
            onSubmit={() => {}}
          />
        </Provider>
      );
    });
    await act(() => {
      fireEvent.click(screen.getByRole('button', { name: 'confirm' }));
    });

    expect(
      await screen.findByRole('textbox', { name: 'dateSince' })
    ).toHaveClass('is-invalid');
    expect(
      await screen.findByRole('textbox', { name: 'dateUntil' })
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
          <ConfigureEducationCycleForm
            educationCycleGuid={'fakeEducationCycleGuid'}
            classGuid={'fakeClassGuid'}
            onSubmit={() => {}}
          />
        </Provider>
      );
    });
    await act(async () => {
      userEvent.type(
        (await screen.findAllByRole('textbox', { name: 'dateUntil' }))[0],
        '04/04/2022{enter}'
      );
    });
    await act(async () => {
      userEvent.type(
        (await screen.findAllByRole('textbox', { name: 'dateSince' }))[0],
        '01/01/2022{enter}'
      );
    });
    await act(async () => {
      userEvent.type(
        (await screen.findAllByRole('textbox', { name: 'dateUntil' }))[1],
        '03/03/2022{enter}'
      );
    });
    await act(async () => {
      userEvent.type(
        (await screen.findAllByRole('textbox', { name: 'dateSince' }))[1],
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
      fireEvent.click(screen.getByRole('button', { name: 'confirm' }));
    });

    expect(mockedConfigureEducationCycleForClass).toBeCalledTimes(1);
  });
});
