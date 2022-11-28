import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import StudentResponse from './Definitions/Responses/StudentResponse';

const API_URL = process.env.REACT_APP_API_URL!;

const getAccessibleStudents = async (): Promise<
  AxiosResponse<StudentResponse[]>
> => {
  return await axiosApiAuthorized.get(API_URL + '/students');
};

const getInactiveAccessibleStudents = async (): Promise<
  AxiosResponse<StudentResponse[]>
> => {
  return await axiosApiAuthorized.get(API_URL + '/students/inactive');
};

export default {
  getAccessibleStudents,
  getInactiveAccessibleStudents,
};
