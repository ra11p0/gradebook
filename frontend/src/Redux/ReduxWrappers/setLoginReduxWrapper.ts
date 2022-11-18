import AccountsProxy from "../../ApiClient/Accounts/AccountsProxy";
import { connectAllHubs } from "../../ApiClient/SignalR/HubsResolver";
import { LOG_IN } from "../../Constraints/accountActionTypes";
import ActionTypes from "../ActionTypes/accountActionTypes";
import setSchoolReduxWrapper from "./setSchoolReduxWrapper";
import setSchoolsListReduxWrapper from "./setSchoolsListReduxWrapper";
import setUserReduxWrapper from "./setUserReduxWrapper";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

const logIn = {
    type: ActionTypes.LogIn
}

export interface logInAction {
    accessToken: string;
    refreshToken: string;
}

export default (dispatch: any, action: logInAction): Promise<void> => {
    return new Promise((resolve) => {
        localStorage.setItem("access_token", action.accessToken);
        localStorage.setItem("refresh_token", action.refreshToken);

        dispatch({ ...logIn, payload: { ...action } });
        AccountsProxy.getMe()
            .then((getMeResponse) => {

                setUserReduxWrapper(dispatch, { userId: getMeResponse.data.userId });
                setSchoolsListReduxWrapper(dispatch, { schoolsList: getMeResponse.data.schools });
                let defaultSchool = getMeResponse.data.schools.find(() => true);
                setSchoolReduxWrapper(dispatch, {
                    schoolGuid: defaultSchool?.school.guid ?? "",
                    schoolName: defaultSchool?.school.name ?? ""
                });
            })
            .then(() => {
                connectAllHubs();
            })
            .then(resolve);
    })
};
