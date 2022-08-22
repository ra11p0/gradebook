import axios from "axios";
import { axiosApiAuthorized } from "../../../AxiosInterceptor";
import LoginRequestDto from "../Definitions/LoginRequestDto";

const API_URL = process.env.REACT_APP_API_URL;

function logIn(request: LoginRequestDto):any{
    return axios.post(API_URL + '/Account/login', request, {
        headers:{
            'Content-Type':'multipart/form-data'
        }
    });
}

function refreshAccessToken(accessToken: string, refreshToken: string):any{
    return axios.post(API_URL + '/Account/refresh-token', {
        accessToken: accessToken,
        refreshToken: refreshToken
    });
}

function getWeather():Promise<any>{
    return axiosApiAuthorized.get(API_URL + '/Weather');
}

export default {
    logIn,
    refreshAccessToken,
    getWeather
}
