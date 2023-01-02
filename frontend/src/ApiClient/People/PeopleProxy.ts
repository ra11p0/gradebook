import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import GetPermissionsResponse from './Definitions/Responses/GetPermissionsResponse';
import PersonResponse, {
  ClassResponse,
  SimplePersonResponse,
} from './Definitions/Responses/PersonResponse';
import SetPermissionsRequest from './Definitions/Requests/SetPermissionsRequest';
import SubjectsForTeacherResponse from './Definitions/Responses/SubjectsForTeacherResponse';
import PeoplePickerData from './Definitions/Requests/PeoplePickerData';

const API_URL: string = import.meta.env.VITE_APP_API_URL ?? 'api';

const activatePerson = async (
  activationCode: string
): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.post(
    API_URL + `/people/activation/code/${activationCode}`
  );
};
const removePerson = async (personGuid: string): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.delete(API_URL + `/people/${personGuid}`);
};
const getPerson = async (
  personGuid: string
): Promise<AxiosResponse<PersonResponse>> => {
  return await axiosApiAuthorized.get(API_URL + `/people/${personGuid}`);
};

const getClassesForPerson = async (
  personGuid: string,
  page: number
): Promise<AxiosResponse<ClassResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/people/${personGuid}/Classes`,
    {
      params: {
        page,
      },
    }
  );
};

const getPermissions = async (
  personGuid: string
): Promise<AxiosResponse<GetPermissionsResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/people/${personGuid}/permissions`
  );
};
const setPermissions = async (
  personGuid: string,
  permissions: SetPermissionsRequest[]
): Promise<AxiosResponse<GetPermissionsResponse[]>> => {
  return await axiosApiAuthorized.post(
    API_URL + `/people/${personGuid}/permissions`,
    permissions
  );
};

const getSubjectsForTeacher = async (
  personGuid: string,
  page: number = 0
): Promise<AxiosResponse<SubjectsForTeacherResponse[]>> => {
  return await axiosApiAuthorized.get(
    `${API_URL}/people/${personGuid}/subjects`,
    { params: { page } }
  );
};

async function getPeopleDetails(
  peopleGuids: string[],
  page: number = 0
): Promise<AxiosResponse<PersonResponse[]>> {
  return await axiosApiAuthorized.post(
    `${API_URL}/people/details`,
    peopleGuids,
    {
      params: { page },
    }
  );
}

async function searchPeople(
  peoplePickerData: PeoplePickerData,
  page: number = 0
): Promise<AxiosResponse<SimplePersonResponse[]>> {
  return await axiosApiAuthorized.post(
    `${API_URL}/people/search`,
    peoplePickerData,
    {
      params: { page },
    }
  );
}

export default {
  getPerson,
  activatePerson,
  removePerson,
  getClassesForPerson,
  getPeopleDetails,
  searchPeople,
  permissions: {
    getPermissions,
    setPermissions,
  },
  subjects: {
    getSubjectsForTeacher,
  },
};
