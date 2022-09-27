import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import ClassResponse from "./Definitions/Responses/ClassResponse";

const API_URL = process.env.REACT_APP_API_URL;

const removeClass = (classGuid: string): Promise<AxiosResponse> => {
    return axiosApiAuthorized.delete(API_URL + `/classes/${classGuid}`);
}

const getClass = (classGuid: string): Promise<AxiosResponse<ClassResponse>> => {
    return axiosApiAuthorized.get(API_URL + `/classes/${classGuid}`);
}

export default {
    removeClass,
    getClass
}
