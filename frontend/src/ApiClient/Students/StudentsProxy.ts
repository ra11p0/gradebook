import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import NewStudentRequest from "../Schools/Definitions/Requests/NewStudentRequest";
import StudentResponse from "./Definitions/Responses/StudentResponse";

const API_URL = process.env.REACT_APP_API_URL;

const getAccessibleStudents = (): Promise<AxiosResponse<StudentResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + '/students');
}

const getInactiveAccessibleStudents = (): Promise<AxiosResponse<StudentResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + '/students/inactive');
}


export default {
    getAccessibleStudents,
    getInactiveAccessibleStudents
}
