import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import GetAccessibleSchoolsResponse from "./Definitions/GetAccessibleSchoolsResponse";

const API_URL = process.env.REACT_APP_API_URL;

const getAccessibleSchools = (personGuid: string): Promise<AxiosResponse<GetAccessibleSchoolsResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/people/${personGuid}/schools`);
};

export default {
    getAccessibleSchools
}
