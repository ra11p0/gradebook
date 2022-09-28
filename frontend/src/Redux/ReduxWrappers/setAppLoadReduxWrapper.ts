import { APP_LOAD } from "../../Constraints/actionTypes";

const appLoad = {
    type: APP_LOAD,
    isAppLoaded: true
};
export const appLoadWrapper = (dispatch: any) => dispatch({ ...appLoad });
