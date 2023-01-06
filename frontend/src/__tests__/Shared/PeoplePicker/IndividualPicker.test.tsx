import { act, render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { openMenu, select } from 'react-select-event';
import { vi } from 'vitest';
import PeoplePickerData from '../../../ApiClient/People/Definitions/Requests/PeoplePickerData';
import { SimplePersonResponse } from '../../../ApiClient/People/Definitions/Responses/PersonResponse';
import SchoolsProxy from '../../../ApiClient/Schools/SchoolsProxy';
import SchoolRolesEnum from '../../../Common/Enums/SchoolRolesEnum';
import IndividualPicker from '../../../Components/Shared/PeoplePicker/IndividualPicker';
describe('<IndividualPicker/>', () => {
  it('Should not show filters button', async () => {
    await act(async () => {
      render(
        <IndividualPicker
          selectedPeople={[]}
          setSelectedPeople={function (
            setFn: (e: string[]) => string[]
          ): void {}}
          getPeople={async function (
            pickerData: PeoplePickerData,
            page: number
          ): Promise<SimplePersonResponse[]> {
            return [];
          }}
        />
      );
    });
    expect(screen.queryByTestId('filterButton')).toBeFalsy();
  });

  it('Should show filters button', async () => {
    await act(async () => {
      render(
        <IndividualPicker
          showFilters
          selectedPeople={[]}
          setSelectedPeople={function (setFn: (e: string[]) => string[]): void {
            //throw new Error('Function not implemented.');
          }}
          getPeople={async function (
            pickerData: PeoplePickerData,
            page: number
          ): Promise<SimplePersonResponse[]> {
            return [];
            //throw new Error('Function not implemented.');
          }}
        />
      );
    });
    expect(screen.queryByTestId('filterButton')).toBeTruthy();
  });

  it('Should check already selected people', async () => {
    await act(async () => {
      render(
        <IndividualPicker
          currentSchoolGuid="schoolguid"
          selectedPeople={['a', 'b', 'c']}
          setSelectedPeople={function (setFn: (e: string[]) => string[]): void {
            //throw new Error('Function not implemented.');
          }}
          getPeople={async function (
            pickerData: PeoplePickerData,
            page: number
          ): Promise<SimplePersonResponse[]> {
            return [
              {
                guid: 'a',
                name: 'testName-a',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'b',
                name: 'testName-b',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'c',
                name: 'testName-c',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'd',
                name: 'testName-d',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
            ];
          }}
        />
      );
    });

    expect(
      (
        screen
          .getAllByRole('checkbox')
          .find((el) => el.id === 'personCheckbox-a') as HTMLInputElement
      ).checked
    ).toBeTruthy();

    expect(
      (
        screen
          .getAllByRole('checkbox')
          .find((el) => el.id === 'personCheckbox-d') as HTMLInputElement
      ).checked
    ).toBeFalsy();
  });

  it('Should invoke set selected people', async () => {
    const mockedFn = vi.fn();

    await act(async () => {
      render(
        <IndividualPicker
          currentSchoolGuid="schoolguid"
          selectedPeople={[]}
          setSelectedPeople={(setFn: (e: string[]) => string[]) => {
            const res = setFn([]);
            mockedFn(res);
          }}
          getPeople={async function (
            pickerData: PeoplePickerData,
            page: number
          ): Promise<SimplePersonResponse[]> {
            return [
              {
                guid: 'a',
                name: 'testName-a',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'b',
                name: 'testName-b',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'c',
                name: 'testName-c',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'd',
                name: 'testName-d',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
            ];
          }}
        />
      );
    });

    await act(() => {
      const checkbox = screen
        .getAllByRole('checkbox')
        .find((el) => el.id === 'personCheckbox-a');

      userEvent.click(checkbox!);
    });

    expect(mockedFn).toBeCalled();

    expect(mockedFn).toBeCalledWith(['a']);
  });

  it('Should use filters', async () => {
    const mockedFn = vi.fn();
    vi.spyOn(SchoolsProxy, 'getClassesInSchool').mockResolvedValue({
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
        <IndividualPicker
          showFilters
          currentSchoolGuid="schoolguid"
          selectedPeople={[]}
          setSelectedPeople={(setFn: (e: string[]) => string[]) => {
            setFn([]);
          }}
          getPeople={async function (
            pickerData: PeoplePickerData,
            page: number
          ): Promise<SimplePersonResponse[]> {
            mockedFn(pickerData);
            return [
              {
                guid: 'a',
                name: 'testName-a',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'b',
                name: 'testName-b',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'c',
                name: 'testName-c',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
              {
                guid: 'd',
                name: 'testName-d',
                surname: 'testSurname',
                schoolRole: SchoolRolesEnum.Student,
                birthday: new Date(),
              },
            ];
          }}
        />
      );
    });

    expect(mockedFn).toBeCalledWith({ schoolGuid: 'schoolguid', query: '' });

    const searchQueryInput = screen
      .getAllByRole('textbox')
      .find((el) => el.id === 'searchQueryInput');

    await act(async () => {
      userEvent.type(searchQueryInput!, 'query');
    });

    expect(mockedFn).toBeCalledWith({
      schoolGuid: 'schoolguid',
      query: 'query',
    });

    await act(async () => {
      userEvent.click(screen.getByRole('button', { name: 'filters' }));
    });
    const rolesCombo = screen
      .queryAllByRole('combobox')
      .find((e) => e.id === 'classFilter');
    await act(async () => {
      openMenu(rolesCombo!);
    });
    await act(async () => {
      await select(rolesCombo!, 'first');
    });
    expect(mockedFn).toBeCalledWith({
      schoolGuid: 'schoolguid',
      query: 'query',
      activeClassGuid: '1',
    });
  });
});
