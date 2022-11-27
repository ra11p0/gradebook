import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import InvitationResponse from "../Invitations/Definitions/Responses/InvitationResponse";
import StudentResponse from "../Students/Definitions/Responses/StudentResponse";
import ClassResponse from "./Definitions/Responses/ClassResponse";
import GetSchoolResponse from "./Definitions/Responses/GetSchoolResponse";
import InviteMultiplePeopleRequest from "./Definitions/Requests/InviteMultiplePeopleRequest";
import NewClassRequest from "./Definitions/Requests/NewClassRequest";
import NewStudentRequest from "./Definitions/Requests/NewStudentRequest";
import NewTeacherRequest from "./Definitions/Requests/NewTeacherRequest";
import PersonResponse from "./Definitions/Responses/PersonResponse";
import StudentInSchoolResponse from "./Definitions/Responses/StudentInSchoolResponse";
import TeacherInSchoolResponse from "./Definitions/Responses/TeacherInSchoolResponse";
import SubjectResponse from "./Definitions/Responses/SubjectResponse";
import NewSubjectRequest from "./Definitions/Requests/NewSubjectRequest";
import getCurrentSchoolRedux from "../../Redux/ReduxQueries/account/getCurrentSchoolRedux";
import NewEducationCycleRequest from "./Definitions/Requests/NewEducationCycleRequest";
import EducationCycleResponse from './Definitions/Responses/EducationCycleResponse';

const API_URL = process.env.REACT_APP_API_URL;


const removeSchool = (schoolGuid: string): Promise<AxiosResponse> => {
    return axiosApiAuthorized.delete(API_URL + `/schools/${schoolGuid}`);
}

const getSchool = (schoolGuid: string): Promise<AxiosResponse<GetSchoolResponse>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}`);
}

const getStudentsInSchool = (schoolGuid: string, page: number = 1): Promise<AxiosResponse<StudentInSchoolResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/students`, {
        params: {
            page
        }
    });
}

const getTeachersInSchool = (schoolGuid: string, page: number = 1): Promise<AxiosResponse<TeacherInSchoolResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/teachers`, {
        params: {
            page
        }
    });
}

const inviteMultiplePeople = (request: InviteMultiplePeopleRequest, schoolGuid: string): Promise<AxiosResponse<string[]>> => {
    return axiosApiAuthorized.post(API_URL + `/schools/${schoolGuid}/Invitations`, request);
};

const addNewStudent = (student: NewStudentRequest, schoolGuid: string): Promise<AxiosResponse<string>> => {
    return axiosApiAuthorized.post(API_URL + `/schools/${schoolGuid}/students`, student);
};

const addNewTeacher = (teacher: NewTeacherRequest, schoolGuid: string): Promise<AxiosResponse<any>> => {
    return axiosApiAuthorized.post(API_URL + `/schools/${schoolGuid}/teachers`, teacher);
};

const getInvitationsInSchool = (schoolGuid: string, page: number = 1): Promise<AxiosResponse<InvitationResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/Invitations`, { params: { page } });
};

const getInactiveAccessibleStudentsInSchool = (schoolGuid: string): Promise<AxiosResponse<StudentResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/students/inactive`);
}

const addNewClass = (classRequest: NewClassRequest, schoolGuid: string): Promise<AxiosResponse<string>> => {
    return axiosApiAuthorized.post(API_URL + `/schools/${schoolGuid}/Classes`, classRequest);
}

const getClassesInSchool = (schoolGuid: string, page: number): Promise<AxiosResponse<ClassResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/Classes`, { params: { page } });
}

const searchPeople = (schoolGuid: string, discriminator: string, query: string, page: number): Promise<AxiosResponse<PersonResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/People/Search`, {
        params: {
            discriminator, query, page
        }
    });
}

const addNewSubject = (newSubjectRequest: NewSubjectRequest, schoolGuid: string = getCurrentSchoolRedux()?.schoolGuid ?? ''): Promise<AxiosResponse<string>> => {
    return axiosApiAuthorized.post(API_URL + `/schools/${schoolGuid}/Subjects`, newSubjectRequest);
}

const getSubjectsInSchool = (page: number, query: string | undefined = undefined, schoolGuid: string = getCurrentSchoolRedux()?.schoolGuid ?? ''): Promise<AxiosResponse<SubjectResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/Subjects`, { params: { page, query } });
}

const addEducationCycle = (educationCycle: NewEducationCycleRequest, schoolGuid: string = getCurrentSchoolRedux()?.schoolGuid!): Promise<AxiosResponse<string>> => {
    return axiosApiAuthorized.post(`${API_URL}/schools/${schoolGuid}/educationCycles`, educationCycle);
}

const getEducationCyclesInSchool = (page: number = 0, schoolGuid: string = getCurrentSchoolRedux()?.schoolGuid!): Promise<AxiosResponse<EducationCycleResponse[]>> => {
    return axiosApiAuthorized.get(`${API_URL}/schools/${schoolGuid}/educationCycles`, { params: { page } });
}


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
    searchPeople,
    getTeachersInSchool,
    addNewTeacher,
    subjects: { getSubjectsInSchool, addNewSubject },
    educationCycles: {
        addEducationCycle,
        getEducationCyclesInSchool
    }
}
