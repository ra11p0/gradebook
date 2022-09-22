import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import InvitationResponse from "../Invitations/Definitions/InvitationResponse";
import StudentResponse from "../Students/Definitions/StudentResponse";
import ClassResponse from "./Definitions/ClassResponse";
import GetSchoolResponse from "./Definitions/GetSchoolResponse";
import InviteMultiplePeopleRequest from "./Definitions/InviteMultiplePeopleRequest";
import NewClassRequest from "./Definitions/NewClassRequest";
import NewSchoolRequest from "./Definitions/NewSchoolRequest";
import NewStudentRequest from "./Definitions/NewStudentRequest";
import NewTeacherRequest from "./Definitions/NewTeacherRequest";
import PersonResponse from "./Definitions/PersonResponse";
import StudentInSchoolResponse from "./Definitions/StudentInSchoolResponse";
import TeacherInSchoolResponse from "./Definitions/TeacherInSchoolResponse";

const API_URL = process.env.REACT_APP_API_URL;

const addNewSchool = (school: NewSchoolRequest): Promise<AxiosResponse> => {
    return axiosApiAuthorized.post(API_URL + '/schools', school);
};

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

const addNewStudent = (student: NewStudentRequest, schoolGuid: string): Promise<AxiosResponse<any>> => {
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

const addNewClass = (classRequest: NewClassRequest, schoolGuid: string): Promise<AxiosResponse<any>> => {
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


export default {
    addNewSchool,
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
    addNewTeacher
}
