import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import GetPermissionsResponse from "./Definitions/Responses/GetPermissionsResponse";
import PersonResponse, { ClassResponse } from "./Definitions/Responses/PersonResponse";
import SetPermissionsRequest from "./Definitions/Requests/SetPermissionsRequest";
import SubjectsForTeacherResponse from "./Definitions/Responses/SubjectsForTeacherResponse";

const API_URL = process.env.REACT_APP_API_URL;

const activatePerson = (activationCode: string): Promise<AxiosResponse> => {
    return axiosApiAuthorized.post(API_URL + `/people/activation/code/${activationCode}`);
}
const removePerson = (personGuid: string) => {
    return axiosApiAuthorized.delete(API_URL + `/people/${personGuid}`);
}
const getPerson = (personGuid: string): Promise<AxiosResponse<PersonResponse>> => {
    return axiosApiAuthorized.get(API_URL + `/people/${personGuid}`);
}

const getClassesForPerson = (personGuid: string, page: number): Promise<AxiosResponse<ClassResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/people/${personGuid}/Classes`, {
        params: {
            page
        }
    });
}

const getPermissions = (personGuid: string): Promise<AxiosResponse<GetPermissionsResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/people/${personGuid}/permissions`);
}
const setPermissions = (personGuid: string, permissions: SetPermissionsRequest[]): Promise<AxiosResponse<GetPermissionsResponse[]>> => {
    return axiosApiAuthorized.post(API_URL + `/people/${personGuid}/permissions`, permissions);
}

const getSubjectsForTeacher = (personGuid: string, page: number = 0): Promise<AxiosResponse<SubjectsForTeacherResponse[]>> => {
    return axiosApiAuthorized.get(`${API_URL}/people/${personGuid}/subjects`, { params: { page } });
}

export default {
    getPerson,
    activatePerson,
    removePerson,
    getClassesForPerson,
    permissions: {
        getPermissions, setPermissions
    },
    subjects: {
        getSubjectsForTeacher
    }
}
