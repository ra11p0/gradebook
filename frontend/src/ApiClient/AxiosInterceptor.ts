import axios from 'axios';
import moment from 'moment';
import setLoginReduxWrapper from '../Redux/ReduxCommands/account/setLoginRedux';
import getSessionRedux from '../Redux/ReduxQueries/account/getSessionRedux';
import AccountProxy from './Accounts/AccountsProxy';

const axiosApiAuthorized = axios.create();

// Request interceptor for API calls
axiosApiAuthorized.interceptors.request.use(
  async (config) => {
    const accessToken = await getAccessToken();
    config.headers = {
      Authorization: `Bearer ${accessToken}`,
      Accept: 'application/json',
      'Content-Type': 'application/json',
    };
    return config;
  },
  (error) => {
    void Promise.reject(error);
  }
);

// Response interceptor for API calls
axiosApiAuthorized.interceptors.response.use(
  (response) => {
    return response;
  },
  async function (error) {
    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      const accessToken = await refreshAccessToken();
      axios.defaults.headers.common.Authorization = 'Bearer ' + accessToken;
      return await axiosApiAuthorized(originalRequest);
    }
    return await Promise.reject(error);
  }
);

async function refreshAccessToken(): Promise<string> {
  const session = getSessionRedux();
  if (!session) throw new Error('session should not be null');
  const refreshResponse = await AccountProxy.refreshAccessToken(
    session.accessToken,
    session.refreshToken
  );
  await setLoginReduxWrapper({
    accessToken: refreshResponse.data.access_token,
    refreshToken: refreshResponse.data.refresh_token,
    expiresIn: moment().add(refreshResponse.data.expires_in, 'seconds'),
  });
  return refreshResponse.data.access_token;
}

async function getAccessToken(): Promise<string> {
  const session = getSessionRedux();
  if (!session) throw new Error('session should not be null');
  if (session.expiresIn.isBefore(moment())) return await refreshAccessToken();
  return session.accessToken;
}

export { axiosApiAuthorized };
