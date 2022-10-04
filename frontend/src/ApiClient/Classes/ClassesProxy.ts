import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import ClassResponse from "./Definitions/Responses/ClassResponse";
import StudentInClassResponse from "./Definitions/Responses/StudentInClassResponse";
import TeachersInClassResponse from "./Definitions/Responses/TeachersInClassResponse";

const API_URL = process.env.REACT_APP_API_URL;

const removeClass = (classGuid: string): Promise<AxiosResponse> => {
    return axiosApiAuthorized.delete(API_URL + `/Classes/${classGuid}`);
}

const getClass = (classGuid: string): Promise<AxiosResponse<ClassResponse>> => {
    return axiosApiAuthorized.get(API_URL + `/Classes/${classGuid}`);
}

const addStudentToClass = (classGuid: string, studentsGuids: string[]): Promise<AxiosResponse> => {
    return axiosApiAuthorized.post(API_URL + `/Classes/${classGuid}/Students`, studentsGuids);
}

const getStudentsInClass = (classGuid: string): Promise<AxiosResponse<StudentInClassResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/Classes/${classGuid}/Students`);
}

const addTeacherToClass = (classGuid: string, teachersGuids: string[]): Promise<AxiosResponse> => {
    return axiosApiAuthorized.post(API_URL + `/Classes/${classGuid}/Teachers`, teachersGuids);
}

const getTeachersInClass = (classGuid: string): Promise<AxiosResponse<TeachersInClassResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/Classes/${classGuid}/Teachers`);
}

export default {
    addTeacherToClass,
    getTeachersInClass,
    removeClass,
    getClass,
    addStudentToClass,
    getStudentsInClass
}
