import axios, { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import GetAccessibleSchoolsResponse from "./Definitions/GetAccessibleSchoolsResponse";
import LoginRequest from "./Definitions/LoginRequest";
import LoginResponse from "./Definitions/LoginResponse";
import MeResponse from "./Definitions/MeResponse";
import RefreshTokenResponse from "./Definitions/RefreshTokenResponse";
import RegisterRequest from "./Definitions/RegisterRequest";

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


export default {
    getMe,
    getAccessibleSchools,
    logIn,
    refreshAccessToken,
    register
}
