import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import SubjectResponse from './Definitions/Responses/SubjectResponse';
import TeachersForSubjectResponse from './Definitions/Responses/TeachersForSubjectResponse';

const API_URL: string = import.meta.env.VITE_APP_API_URL ?? 'api';

const getSubject = async (
  guid: string
): Promise<AxiosResponse<SubjectResponse>> => {
  return await axiosApiAuthorized.get(`${API_URL}/subjects/${guid}`);
};
const getTeachersForSubject = async (
  guid: string,
  page: number = 0,
  query: string = ''
): Promise<AxiosResponse<TeachersForSubjectResponse[]>> => {
  return await axiosApiAuthorized.get(`${API_URL}/subjects/${guid}/teachers`, {
    params: { page, query },
  });
};
const editTeachersInSubject = async (
  guid: string,
  teachers: string[]
): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.post(`${API_URL}/subjects/${guid}/teachers`, [
    ...teachers,
  ]);
};

export default {
  getSubject,
  getTeachersForSubject,
  editTeachersInSubject,
};
