import axios, { AxiosResponse } from "axios";
import { axiosApiAuthorized } from "../AxiosInterceptor";
import { InvitationDetailsResponse } from "./Definitions/InvitationDetailsResponse";

const API_URL = process.env.REACT_APP_API_URL;

const getInvitationDetails = (activationCode: string): Promise<AxiosResponse<InvitationDetailsResponse>> => {
    return axiosApiAuthorized.get(API_URL + `/Invitations/Activation/Code?activationCode=${activationCode}`);
};


export default {
    getInvitationDetails
}
