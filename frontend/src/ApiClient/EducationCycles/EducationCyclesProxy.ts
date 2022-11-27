import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import EducationCycleResponse from './Definitions/Responses/EducationCycleResponse';
const API_URL = process.env.REACT_APP_API_URL;

const getEducationCycle = (educationCycleGuid: string): Promise<AxiosResponse<EducationCycleResponse>> => {
    return axiosApiAuthorized.get(API_URL + `/educationCycles/${educationCycleGuid}`);
}

export default {
    getEducationCycle
}