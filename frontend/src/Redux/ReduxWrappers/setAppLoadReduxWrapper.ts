import { APP_LOAD } from "../../Constraints/actionTypes";

const appLoad = {
    type: APP_LOAD,
    isAppLoaded: true
};
export default (dispatch: any) => dispatch({ ...appLoad });
