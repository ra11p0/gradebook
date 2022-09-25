import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";

const API_URL = process.env.REACT_APP_API_URL;

const activatePerson = (activationCode: string): Promise<AxiosResponse> => {
    return axiosApiAuthorized.post(API_URL + `/people/activation/code/${activationCode}`);
}
const removePerson = (personGuid: string) => {
    return axiosApiAuthorized.delete(API_URL + `/people/${personGuid}`);
}

export default {
    activatePerson,
    removePerson
}
