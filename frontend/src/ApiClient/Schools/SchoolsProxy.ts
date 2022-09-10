import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import NewSchoolRequest from "./Definitions/NewSchoolRequest";

const API_URL = process.env.REACT_APP_API_URL;

const addNewSchool = (school: NewSchoolRequest): Promise<AxiosResponse<any>> => {
    return axiosApiAuthorized.post(API_URL + '/schools', school);
};

export default {
    addNewSchool
}
