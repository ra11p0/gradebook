import axios, { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import NewStudentRequest from "./Definitions/NewStudentRequest";
import StudentResponse from "./Definitions/StudentResponse";

const API_URL = process.env.REACT_APP_API_URL;

const addNewStudent = (student: NewStudentRequest): Promise<AxiosResponse<any>> => {
    return axiosApiAuthorized.post(API_URL + '/students', student);
};

const getAccessibleStudents = (): Promise<AxiosResponse<StudentResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + '/students');
}

const getInactiveAccessibleStudents = (): Promise<AxiosResponse<StudentResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + '/students/inactive');
}


export default {
    addNewStudent,
    getAccessibleStudents,
    getInactiveAccessibleStudents
}
