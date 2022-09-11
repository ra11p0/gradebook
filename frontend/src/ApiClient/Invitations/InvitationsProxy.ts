import { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import { InvitationDetailsResponse } from "./Definitions/InvitationDetailsResponse";
import InvitationResponse from "./Definitions/InvitationResponse";

const API_URL = process.env.REACT_APP_API_URL;

const getInvitationDetailsForStudent = (activationCode: string): Promise<AxiosResponse<InvitationDetailsResponse>> => {
    return axiosApiAuthorized.get(API_URL + `/Invitations/Activation/Code`, {
        params: {
            activationCode,
            method: 'student'
        }
    });
};

const getUsersInvitations = (): Promise<AxiosResponse<InvitationResponse[]>> => {
    return axiosApiAuthorized.get(API_URL + `/Invitations`);
};


export default {
    getInvitationDetailsForStudent,
    getUsersInvitations
}
