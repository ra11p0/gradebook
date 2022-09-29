import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import GetPermissionsResponse from "./Definitions/GetPermissionsResponse";
import SetPermissionsRequest from "./Definitions/SetPermissionsRequest";

const API_URL = process.env.REACT_APP_API_URL;

const activatePerson = (activationCode: string): Promise<AxiosResponse> => {
    return axiosApiAuthorized.post(API_URL + `/people/activation/code/${activationCode}`);
}
const removePerson = (personGuid: string) => {
    return axiosApiAuthorized.delete(API_URL + `/people/${personGuid}`);
}
const getPermissions = (personGuid: string): Promise<AxiosResponse<GetPermissionsResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/people/${personGuid}/permissions`);
}
const setPermissions = (personGuid: string, permissions: SetPermissionsRequest[]) => {
    return axiosApiAuthorized.post(API_URL + `/people/${personGuid}/permissions`, permissions);
}

export default {
    activatePerson,
    removePerson,
    permissions: {
        getPermissions, setPermissions
    }
}
