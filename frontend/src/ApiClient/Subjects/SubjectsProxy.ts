import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import SubjectResponse from "./Definitions/SubjectResponse";

const API_URL = process.env.REACT_APP_API_URL;

const getSubject = (guid: string): Promise<AxiosResponse<SubjectResponse>> => {
    return axiosApiAuthorized.get(`${API_URL}/subjects/${guid}`);
}




export default {
    getSubject,
}