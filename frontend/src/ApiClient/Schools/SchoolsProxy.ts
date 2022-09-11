import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import InvitationResponse from "../Invitations/Definitions/InvitationResponse";
import StudentResponse from "../Students/Definitions/StudentResponse";
import GetSchoolResponse from "./Definitions/GetSchoolResponse";
import InviteMultiplePeopleRequest from "./Definitions/InviteMultiplePeopleRequest";
import NewSchoolRequest from "./Definitions/NewSchoolRequest";
import NewStudentRequest from "./Definitions/NewStudentRequest";
import StudentInSchoolResponse from "./Definitions/StudentInSchoolResponse";

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

const inviteMultiplePeople = (request: InviteMultiplePeopleRequest, schoolGuid: string): Promise<AxiosResponse<string[]>> => {
    return axiosApiAuthorized.post(API_URL + `/schools/${schoolGuid}/Invitations`, request);
};

const addNewStudent = (student: NewStudentRequest, schoolGuid: string): Promise<AxiosResponse<any>> => {
    return axiosApiAuthorized.post(API_URL + `/schools/${schoolGuid}/students`, student);
};

const getInvitationsInSchool = (schoolGuid: string, page: number = 1): Promise<AxiosResponse<InvitationResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/Invitations`, { params: { page } });
};

const getInactiveAccessibleStudentsInSchool = (schoolGuid: string): Promise<AxiosResponse<StudentResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/schools/${schoolGuid}/students/inactive`);
}


export default {
    addNewSchool,
    removeSchool,
    getSchool,
    getStudentsInSchool,
    addNewStudent,
    inviteMultiplePeople,
    getInvitationsInSchool,
    getInactiveAccessibleStudentsInSchool
}
