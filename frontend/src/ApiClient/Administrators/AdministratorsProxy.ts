import axios, { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import NewAdministratorRequest from "./Definitions/NewAdministratorRequest";
import NewSchoolRequest from "./Definitions/NewSchoolRequest";

const API_URL = process.env.REACT_APP_API_URL;

const newAdministrator = (admin: NewAdministratorRequest): Promise<AxiosResponse<any>> => {
    return axiosApiAuthorized.post(API_URL + '/administrators', admin);
};

const newAdministratorWithSchool = (administrator: NewAdministratorRequest, school: NewSchoolRequest): Promise<AxiosResponse<any>> => {
    return axiosApiAuthorized.post(API_URL + '/administrators/withSchool', { administrator, school });
};

export default {
    newAdministrator,
    newAdministratorWithSchool
}
