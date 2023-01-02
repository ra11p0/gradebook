import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import InvitationResponse from '../Invitations/Definitions/Responses/InvitationResponse';
import StudentResponse from '../Students/Definitions/Responses/StudentResponse';
import ClassResponse from './Definitions/Responses/ClassResponse';
import GetSchoolResponse from './Definitions/Responses/GetSchoolResponse';
import InviteMultiplePeopleRequest from './Definitions/Requests/InviteMultiplePeopleRequest';
import NewClassRequest from './Definitions/Requests/NewClassRequest';
import NewStudentRequest from './Definitions/Requests/NewStudentRequest';
import NewTeacherRequest from './Definitions/Requests/NewTeacherRequest';
import StudentInSchoolResponse from './Definitions/Responses/StudentInSchoolResponse';
import TeacherInSchoolResponse from './Definitions/Responses/TeacherInSchoolResponse';
import SubjectResponse from './Definitions/Responses/SubjectResponse';
import NewSubjectRequest from './Definitions/Requests/NewSubjectRequest';
import getCurrentSchoolRedux from '../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import NewEducationCycleRequest from './Definitions/Requests/NewEducationCycleRequest';
import EducationCycleResponse from './Definitions/Responses/EducationCycleResponse';

const API_URL: string = import.meta.env.VITE_APP_API_URL ?? 'api';

const removeSchool = async (schoolGuid: string): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.delete(API_URL + `/schools/${schoolGuid}`);
};

const getSchool = async (
  schoolGuid: string
): Promise<AxiosResponse<GetSchoolResponse>> => {
  return await axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}`);
};

const getStudentsInSchool = async (
  schoolGuid: string,
  page: number = 1
): Promise<AxiosResponse<StudentInSchoolResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/schools/${schoolGuid}/students`,
    {
      params: {
        page,
      },
    }
  );
};

const getTeachersInSchool = async (
  schoolGuid: string,
  page: number = 1
): Promise<AxiosResponse<TeacherInSchoolResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/schools/${schoolGuid}/teachers`,
    {
      params: {
        page,
      },
    }
  );
};

const inviteMultiplePeople = async (
  request: InviteMultiplePeopleRequest,
  schoolGuid: string
): Promise<AxiosResponse<string[]>> => {
  return await axiosApiAuthorized.post(
    API_URL + `/schools/${schoolGuid}/Invitations`,
    request
  );
};

const addNewStudent = async (
  student: NewStudentRequest,
  schoolGuid: string
): Promise<AxiosResponse<string>> => {
  return await axiosApiAuthorized.post(
    API_URL + `/schools/${schoolGuid}/students`,
    student
  );
};

const addNewTeacher = async (
  teacher: NewTeacherRequest,
  schoolGuid: string
): Promise<AxiosResponse<any>> => {
  return await axiosApiAuthorized.post(
    API_URL + `/schools/${schoolGuid}/teachers`,
    teacher
  );
};

const getInvitationsInSchool = async (
  schoolGuid: string,
  page: number = 1
): Promise<AxiosResponse<InvitationResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/schools/${schoolGuid}/Invitations`,
    { params: { page } }
  );
};

const getInactiveAccessibleStudentsInSchool = async (
  schoolGuid: string
): Promise<AxiosResponse<StudentResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/schools/${schoolGuid}/students/inactive`
  );
};

const addNewClass = async (
  classRequest: NewClassRequest,
  schoolGuid: string
): Promise<AxiosResponse<string>> => {
  return await axiosApiAuthorized.post(
    API_URL + `/schools/${schoolGuid}/Classes`,
    classRequest
  );
};

const getClassesInSchool = async (
  schoolGuid: string = getCurrentSchoolRedux()!.schoolGuid,
  page: number = 0,
  query: string = ''
): Promise<AxiosResponse<ClassResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/schools/${schoolGuid}/Classes`,
    { params: { page, query } }
  );
};

const addNewSubject = async (
  newSubjectRequest: NewSubjectRequest,
  schoolGuid: string = getCurrentSchoolRedux()?.schoolGuid ?? ''
): Promise<AxiosResponse<string>> => {
  return await axiosApiAuthorized.post(
    API_URL + `/schools/${schoolGuid}/Subjects`,
    newSubjectRequest
  );
};

const getSubjectsInSchool = async (
  page: number,
  query: string | undefined = undefined,
  schoolGuid: string = getCurrentSchoolRedux()?.schoolGuid ?? ''
): Promise<AxiosResponse<SubjectResponse[]>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/schools/${schoolGuid}/Subjects`,
    { params: { page, query } }
  );
};

const addEducationCycle = async (
  educationCycle: NewEducationCycleRequest,
  schoolGuid: string | undefined = getCurrentSchoolRedux()?.schoolGuid
): Promise<AxiosResponse<string>> => {
  if (!schoolGuid) throw new Error('schoolGuid was undefined');
  return await axiosApiAuthorized.post(
    `${API_URL}/schools/${schoolGuid}/educationCycles`,
    educationCycle
  );
};

const getEducationCyclesInSchool = async (
  page: number = 0,
  query: string = '',
  schoolGuid: string | undefined = getCurrentSchoolRedux()?.schoolGuid
): Promise<AxiosResponse<EducationCycleResponse[]>> => {
  if (!schoolGuid) throw new Error('schoolGuid was undefined');
  return await axiosApiAuthorized.get(
    `${API_URL}/schools/${schoolGuid}/educationCycles`,
    { params: { page, query } }
  );
};

export default {
  removeSchool,
  getSchool,
  getStudentsInSchool,
  addNewStudent,
  inviteMultiplePeople,
  getInvitationsInSchool,
  getInactiveAccessibleStudentsInSchool,
  addNewClass,
  getClassesInSchool,
  getTeachersInSchool,
  addNewTeacher,
  subjects: { getSubjectsInSchool, addNewSubject },
  educationCycles: {
    addEducationCycle,
    getEducationCyclesInSchool,
  },
};
