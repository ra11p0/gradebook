import { APP_LOAD } from "../../Constraints/actionTypes";

const appLoad = {
    type: APP_LOAD,
};
export default (dispatch: any, isAppLoaded: boolean) => dispatch({ ...appLoad, isAppLoaded });
