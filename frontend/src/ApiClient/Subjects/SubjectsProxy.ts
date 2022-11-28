import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import SubjectResponse from './Definitions/Responses/SubjectResponse';
import TeachersForSubjectResponse from './Definitions/Responses/TeachersForSubjectResponse';

const API_URL = process.env.REACT_APP_API_URL!;

const getSubject = async (
  guid: string
): Promise<AxiosResponse<SubjectResponse>> => {
  return await axiosApiAuthorized.get(`${API_URL}/subjects/${guid}`);
};
const getTeachersForSubject = async (
  guid: string,
  page: number = 0
): Promise<AxiosResponse<TeachersForSubjectResponse[]>> => {
  return await axiosApiAuthorized.get(`${API_URL}/subjects/${guid}/teachers`, {
    params: { page },
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
