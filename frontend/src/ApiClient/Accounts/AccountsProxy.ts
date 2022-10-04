import axios, { AxiosResponse } from "axios";
import SettingsEnum from "../../Common/Enums/SettingsEnum";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import GetAccessibleSchoolsResponse from "./Definitions/Responses/GetAccessibleSchoolsResponse";
import LoginRequest from "./Definitions/Requests/LoginRequest";
import LoginResponse from "./Definitions/Responses/LoginResponse";
import MeResponse from "./Definitions/Responses/MeResponse";
import RefreshTokenResponse from "./Definitions/Responses/RefreshTokenResponse";
import RegisterRequest from "./Definitions/Requests/RegisterRequest";
import RelatedPersonResponse from "./Definitions/Responses/RelatedPersonResponse";
import SettingsRequest from "./Definitions/Requests/SettingsRequest";

const API_URL = process.env.REACT_APP_API_URL;

function logIn(request: LoginRequest): Promise<AxiosResponse<LoginResponse>> {
    return axios.post(API_URL + '/Account/login', request, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    });
}

function register(request: RegisterRequest): Promise<AxiosResponse<any>> {
    return axios.post(API_URL + '/Account/register', request);
}

function refreshAccessToken(accessToken: string, refreshToken: string): Promise<AxiosResponse<RefreshTokenResponse>> {
    return axios.post(API_URL + '/Account/refresh-token', {
        accessToken, refreshToken
    });
}

const getAccessibleSchools = (userGuid: string): Promise<AxiosResponse<GetAccessibleSchoolsResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/account/${userGuid}/schools`);
};

const getMe = (): Promise<AxiosResponse<MeResponse>> => {
    return axiosApiAuthorized.get(API_URL + `/account/Me`);
}

const getRelatedPeople = (userGuid: string): Promise<AxiosResponse<RelatedPersonResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/account/${userGuid}/people`);
}


//#region settings

const getDefaultPerson = (userGuid: string): Promise<AxiosResponse<string>> => {
    return axiosApiAuthorized.get(API_URL + `/account/${userGuid}/settings/${SettingsEnum.DefaultPersonGuid}`);
}

const setDefaultPerson = (userGuid: string, defaultPersonGuid: string): Promise<AxiosResponse> => {
    return axiosApiAuthorized.post(API_URL + `/account/${userGuid}/settings/${SettingsEnum.DefaultPersonGuid}`, { defaultPersonGuid });
}

const setSettings = (userGuid: string, settings: SettingsRequest): Promise<AxiosResponse> => {
    return axiosApiAuthorized.post(API_URL + `/account/${userGuid}/settings`, settings);
}

//#endregion settings


export default {
    getRelatedPeople,
    getMe,
    getAccessibleSchools,
    logIn,
    refreshAccessToken,
    register,
    settings: {
        setSettings,
        getDefaultPerson, setDefaultPerson
    }
}
