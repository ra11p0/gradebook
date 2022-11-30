import axios, { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import GetAccessibleSchoolsResponse from './Definitions/Responses/GetAccessibleSchoolsResponse';
import LoginRequest from './Definitions/Requests/LoginRequest';
import LoginResponse from './Definitions/Responses/LoginResponse';
import MeResponse from './Definitions/Responses/MeResponse';
import RefreshTokenResponse from './Definitions/Responses/RefreshTokenResponse';
import RegisterRequest from './Definitions/Requests/RegisterRequest';
import RelatedPersonResponse from './Definitions/Responses/RelatedPersonResponse';
import SettingsRequest from './Definitions/Requests/SettingsRequest';
import UserSettings from './Definitions/Responses/UserSettings';

const API_URL = process.env.REACT_APP_API_URL!;

async function logIn(
  request: LoginRequest
): Promise<AxiosResponse<LoginResponse>> {
  return await axios.post(`${API_URL}/Account/login`, request, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
}

async function register(request: RegisterRequest): Promise<AxiosResponse<any>> {
  return await axios.post(API_URL + '/Account/register', request);
}

async function refreshAccessToken(
  accessToken: string,
  refreshToken: string
): Promise<AxiosResponse<RefreshTokenResponse>> {
  return await axios.post(API_URL + '/Account/refresh-token', {
    accessToken,
    refreshToken,
  });
}

const getAccessibleSchools = async (
  userGuid: string
): Promise<AxiosResponse<GetAccessibleSchoolsResponse[]>> => {
  return await axiosApiAuthorized.get(API_URL + `/account/${userGuid}/schools`);
};

const getMe = async (): Promise<AxiosResponse<MeResponse>> => {
  return await axiosApiAuthorized.get(API_URL + `/account/Me`);
};

const getRelatedPeople = async (
  userGuid: string
): Promise<AxiosResponse<RelatedPersonResponse[]>> => {
  return await axiosApiAuthorized.get(API_URL + `/account/${userGuid}/people`);
};

// #region settings

async function getUserSettings(): Promise<AxiosResponse<UserSettings>> {
  return await axiosApiAuthorized.get(API_URL + `/account/settings`);
}

const getDefaultPerson = async (): Promise<string> => {
  return (await getUserSettings()).data.defaultPersonGuid;
};

const setDefaultPerson = async (
  defaultPersonGuid: string
): Promise<AxiosResponse> => {
  return await setSettings({ defaultPersonGuid });
};

const setLanguage = async (language: string): Promise<AxiosResponse> => {
  return await setSettings({ language });
};

const setSettings = async (
  settings: SettingsRequest
): Promise<AxiosResponse> => {
  return await axiosApiAuthorized.post(API_URL + `/account/settings`, settings);
};

// #endregion settings

export default {
  getRelatedPeople,
  getMe,
  getAccessibleSchools,
  logIn,
  refreshAccessToken,
  register,
  settings: {
    setLanguage,
    setSettings,
    setDefaultPerson,
    getUserSettings,
    getDefaultPerson,
  },
};
