import axios from 'axios';
import { store } from '../store'
import AccountRepository from './Account/AccountProxy';
import { logOut, refreshToken as refreshTokenAction } from '../Actions/Account/accountActions';

const axiosApiAuthorized = axios.create();

// Request interceptor for API calls
axiosApiAuthorized.interceptors.request.use(
  async config => {
    const accessToken = await getAccessToken();
    config.headers = { 
      'Authorization': `Bearer ${accessToken}`,
      'Accept': 'application/json',
      'Content-Type': 'application/x-www-form-urlencoded'
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
  if (error.response.status === 403 || error.response.status === 401 && !originalRequest._retry) {
    originalRequest._retry = true;
    const access_token = await refreshAccessToken();            
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + access_token;
    return axiosApiAuthorized(originalRequest);
  }
  return Promise.reject(error);
});

async function refreshAccessToken(): Promise<string>{
    const accessToken: string = store.getState().common.session.token;
    const refreshToken: string = store.getState().common.session.refreshToken;
    const refreshResponse = await AccountRepository.refreshAccessToken(accessToken, refreshToken);
    store.dispatch({
      ...refreshTokenAction,
      token: refreshResponse.data.accessToken,
      refreshToken: refreshResponse.data.refreshToken
    });
    return refreshResponse.data.accessToken;
}

function getAccessToken(): string{
    return store.getState().common.session.token;
}

export {
  axiosApiAuthorized
}
