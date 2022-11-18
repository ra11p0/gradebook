import axios from 'axios';
import getSessionRedux from '../Redux/ReduxQueries/getSessionRedux';
import setLoginReduxWrapper from '../Redux/ReduxCommands/setLoginRedux';
import { store } from '../store'
import AccountProxy from './Accounts/AccountsProxy';

const axiosApiAuthorized = axios.create();

// Request interceptor for API calls
axiosApiAuthorized.interceptors.request.use(
  async config => {
    const accessToken = await getAccessToken();
    config.headers = {
      'Authorization': `Bearer ${accessToken}`,
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    }
    return config;
  },
  error => {
    Promise.reject(error)
  });

// Response interceptor for API calls
axiosApiAuthorized.interceptors.response.use((response) => {
  return response
}, async function (error) {
  const originalRequest = error.config;
  if (error.response.status === 401 && !originalRequest._retry) {
    originalRequest._retry = true;
    const access_token = await refreshAccessToken();
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + access_token;
    return axiosApiAuthorized(originalRequest);
  }
  return Promise.reject(error);
});

async function refreshAccessToken(): Promise<string> {
  const session = getSessionRedux();
  if (!session) throw ('session should not be null')
  const refreshResponse = await AccountProxy.refreshAccessToken(session.accessToken, session.refreshToken);
  setLoginReduxWrapper(store.dispatch, {
    accessToken: refreshResponse.data.access_token,
    refreshToken: refreshResponse.data.refresh_token
  });
  return refreshResponse.data.access_token;
}

function getAccessToken(): string {
  const session = getSessionRedux();
  if (!session) throw ('session should not be null')
  return session.accessToken;
}

export {
  axiosApiAuthorized
}
