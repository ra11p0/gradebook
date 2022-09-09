import axios, { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import LoginRequest from "./Definitions/LoginRequest";
import MeResponse from "./Definitions/MeResponse";
import RegisterRequest from "./Definitions/RegisterRequest";

const API_URL = process.env.REACT_APP_API_URL;

function logIn(request: LoginRequest): Promise<AxiosResponse<any>> {
    return axios.post(API_URL + '/Account/login', request, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    });
}

function register(request: RegisterRequest): Promise<AxiosResponse<any>> {
    return axios.post(API_URL + '/Account/register', request);
}

function refreshAccessToken(accessToken: string, refreshToken: string): Promise<AxiosResponse<any>> {
    return axios.post(API_URL + '/Account/refresh-token', {
        accessToken: accessToken,
        refreshToken: refreshToken
    });
}

function getMe(): Promise<AxiosResponse<MeResponse>> {
    return axiosApiAuthorized.get(API_URL + '/Account/Me');
}


export default {
    logIn,
    refreshAccessToken,
    register,
    getMe
}
