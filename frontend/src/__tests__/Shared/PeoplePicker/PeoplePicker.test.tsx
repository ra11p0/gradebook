import { act, render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { vi } from 'vitest';
import PeoplePickerData from '../../../ApiClient/People/Definitions/Requests/PeoplePickerData';
import { SimplePersonResponse } from '../../../ApiClient/People/Definitions/Responses/PersonResponse';
import * as PeopleProxy from '../../../ApiClient/People/PeopleProxy';
import SchoolRolesEnum from '../../../Common/Enums/SchoolRolesEnum';
import PeoplePicker from '../../../Components/Shared/PeoplePicker/PeoplePicker';
import * as getCurrentSchoolRedux from '../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import { store } from '../../../store';

describe('<PeoplePicker/>', () => {
  it('Should show selected people on left panel', async () => {
    vi.spyOn(getCurrentSchoolRedux, 'default').mockReturnValue({
      schoolName: 'schoolName',
      schoolGuid: 'schoolGuid',
    });

    const mockedGetPeopleDetails = vi
      .spyOn(PeopleProxy.default, 'getPeopleDetails')
      .mockImplementation((async (peopleGuids: string[]) => {
        return {
          data: peopleGuids.map((el) => ({
            guid: el,
          })),
        };
      }) as any);
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <PeoplePicker
              onHide={() => {
                throw new Error('Function not implemented.');
              }}
              onConfirm={(peopleGuids: string[]): void | Promise<void> => {
                throw new Error('Function not implemented.');
              }}
              getPeople={async (
                pickerData: PeoplePickerData,
                page: number
              ): Promise<SimplePersonResponse[]> => {
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
              show={true}
            />
          </BrowserRouter>
        </Provider>
      );
    });
    expect(mockedGetPeopleDetails).toBeCalledWith([], 1);
    await act(async () => {
      const checkbox = screen
        .getAllByRole('checkbox')
        .find((el) => el.id === 'personCheckbox-a');

      userEvent.click(checkbox!);
    });
    expect(mockedGetPeopleDetails).toBeCalledWith(['a'], 1);
    expect(await screen.findByTestId(`personComponent-a`)).toBeTruthy();
  });
});
