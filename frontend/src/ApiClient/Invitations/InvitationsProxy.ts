import { AxiosResponse } from 'axios';
import { axiosApiAuthorized } from '../AxiosInterceptor';
import { InvitationDetailsResponse } from './Definitions/Responses/InvitationDetailsResponse';
import InvitationResponse from './Definitions/Responses/InvitationResponse';

const API_URL = process.env.REACT_APP_API_URL!;

const getInvitationDetailsForStudent = async (
  activationCode: string
): Promise<AxiosResponse<InvitationDetailsResponse>> => {
  return await axiosApiAuthorized.get(
    API_URL + `/Invitations/Activation/Code`,
    {
      params: {
        activationCode,
        method: 'student',
      },
    }
  );
};

const getUsersInvitations = async (): Promise<
  AxiosResponse<InvitationResponse[]>
> => {
  return await axiosApiAuthorized.get(API_URL + `/Invitations`);
};

export default {
  getInvitationDetailsForStudent,
  getUsersInvitations,
};
