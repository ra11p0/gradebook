import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import ClassResponse from './Definitions/Responses/ClassResponse';
import EducationCyclesInClassResponse from './Definitions/Responses/EducationCyclesInClassResponse';
import StudentInClassResponse from './Definitions/Responses/StudentInClassResponse';
import TeachersInClassResponse from './Definitions/Responses/TeachersInClassResponse';

const API_URL = process.env.REACT_APP_API_URL!;

const removeClass = async (classGuid: string): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.delete(API_URL + `/Classes/${classGuid}`);
};

const getClass = async (
  classGuid: string
): Promise<AxiosResponse<ClassResponse>> => {
  return await axiosApiAuthorized.get(API_URL + `/Classes/${classGuid}`);
};

const addStudentsToClass = async (
  classGuid: string,
  studentsGuids: string[]
): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.post(
    API_URL + `/Classes/${classGuid}/Students`,
    studentsGuids
  );
};

const getStudentsInClass = async (
  classGuid: string
): Promise<AxiosResponse<StudentInClassResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/Classes/${classGuid}/Students`
  );
};

const addTeachersToClass = async (
  classGuid: string,
  teachersGuids: string[]
): Promise<AxiosResponse<TeachersInClassResponse[]>> => {
  return await axiosApiAuthorized.post(
    API_URL + `/Classes/${classGuid}/Teachers`,
    teachersGuids
  );
};

const getTeachersInClass = async (
  classGuid: string
): Promise<AxiosResponse<TeachersInClassResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/Classes/${classGuid}/Teachers`
  );
};

const searchStudentsCandidatesToClassWithCurrent = async (
  classGuid: string,
  query: string,
  page: number
): Promise<AxiosResponse<StudentInClassResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/Classes/${classGuid}/Students/Candidates/Search`,
    {
      params: {
        query,
        page,
      },
    }
  );
};

async function getEducationCyclesInClass(
  classGuid: string
): Promise<AxiosResponse<EducationCyclesInClassResponse>> {
  return await axiosApiAuthorized.get(
    API_URL + `/Classes/${classGuid}/EducationCycles`
  );
}

export default {
  addTeachersToClass,
  getTeachersInClass,
  removeClass,
  getClass,
  addStudentsToClass,
  getStudentsInClass,
  searchStudentsCandidatesToClassWithCurrent,
  educationCycles: {
    getEducationCyclesInClass,
  },
};
