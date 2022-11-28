import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import NewAdministratorRequest from './Definitions/Requests/NewAdministratorRequest';
import NewSchoolRequest from './Definitions/Requests/NewSchoolRequest';

const API_URL = process.env.REACT_APP_API_URL!;

const newAdministrator = async (
  admin: NewAdministratorRequest
): Promise<AxiosResponse<any>> => {
  return await axiosApiAuthorized.post(API_URL + '/administrators', admin);
};

const newAdministratorWithSchool = async (
  administrator: NewAdministratorRequest,
  school: NewSchoolRequest
): Promise<AxiosResponse<any>> => {
  return await axiosApiAuthorized.post(API_URL + '/administrators/withSchool', {
    administrator,
    school,
  });
};

export default {
  newAdministrator,
  newAdministratorWithSchool,
};
