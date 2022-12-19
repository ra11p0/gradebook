import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import ClassResponse from '../Classes/Definitions/Responses/ClassResponse';
import EducationCycleResponse from './Definitions/Responses/EducationCycleResponse';

const API_URL: string = import.meta.env.VITE_APP_API_URL ?? 'api';

const getEducationCycle = async (
  educationCycleGuid: string
): Promise<AxiosResponse<EducationCycleResponse>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/educationCycles/${educationCycleGuid}`
  );
};

const getClassesForEducationCycle = async (
  educationCycleGuid: string,
  page: number = 0
): Promise<AxiosResponse<ClassResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/educationCycles/${educationCycleGuid}/Classes`,
    { params: { page } }
  );
};

const editClassesInEducationCycle = async (
  educationCycleGuid: string,
  classesGuids: string[]
): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.post(
    API_URL + `/educationCycles/${educationCycleGuid}/Classes`,
    classesGuids
  );
};

const setEducationCycleForClass = async (
  educationCycleGuid: string,
  classGuid: string
): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.put(
    API_URL + `/educationCycles/${educationCycleGuid}/Classes/${classGuid}`
  );
};

async function getAvailableClassesWithAssignedForEducationCycle(
  educationCycleGuid: string,
  page: number,
  query: string
): Promise<AxiosResponse<ClassResponse[]>> {
  return await axiosApiAuthorized.get(
    API_URL + `/educationCycles/${educationCycleGuid}/Classes/Available`,
    {
      params: {
        page,
        query,
      },
    }
  );
}

export default {
  getEducationCycle,
  getClassesForEducationCycle,
  editClassesInEducationCycle,
  setEducationCycleForClass,
  getAvailableClassesWithAssignedForEducationCycle,
};
