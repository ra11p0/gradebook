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
import getApplicationLanguageRedux from '../../Redux/ReduxQueries/account/getApplicationLanguageRedux';
import getCurrentUserIdRedux from '../../Redux/ReduxQueries/account/getCurrentUserIdRedux';

const API_URL: string = import.meta.env.VITE_APP_API_URL ?? 'api';

async function logIn(
  request: LoginRequest
): Promise<AxiosResponse<LoginResponse>> {
  return await axios.post(`${API_URL}/Account/login`, request, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
}

async function forgotPassword(email: string): Promise<AxiosResponse<string>> {
  return await axios.post(`${API_URL}/Account/RemindPassword`, email, {
    headers: { 'Content-Type': 'application/json' },
  });
}

async function setNewPassword(
  password: string,
  confirmPassword: string,
  userId: string,
  authCode: string
): Promise<AxiosResponse<string>> {
  return await axios.post(
    `${API_URL}/Account/SetNewPassword/${userId}/${authCode}`,
    { password, confirmPassword }
  );
}

async function register(request: RegisterRequest): Promise<AxiosResponse<any>> {
  const language = getApplicationLanguageRedux();
  return await axios.post(API_URL + '/Account/register', request, {
    params: { language },
  });
}

async function verifyEmailAddress(
  userId: string,
  activationCode: string
): Promise<AxiosResponse<LoginResponse>> {
  return await axios.post(
    `${API_URL}/Account/${userId}/emailActivation/${activationCode}`
  );
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
  userGuid: string = getCurrentUserIdRedux()
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

const getDefaultSchool = async (): Promise<string> => {
  return (await getUserSettings()).data.defaultSchool;
};

const setDefaultSchool = async (
  defaultSchool: string
): Promise<AxiosResponse> => {
  return await setSettings({ defaultSchool });
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
  verifyEmailAddress,
  getRelatedPeople,
  getMe,
  getAccessibleSchools,
  logIn,
  refreshAccessToken,
  register,
  forgotPassword,
  setNewPassword,
  settings: {
    setLanguage,
    setSettings,
    setDefaultSchool,
    getUserSettings,
    getDefaultSchool,
  },
};
