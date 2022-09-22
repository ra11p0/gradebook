import { LOG_OUT } from "../Constraints/actionTypes";

const logOut = {
    type: LOG_OUT,
}

export const logOutWrapper = (dispatch: any) => dispatch({ ...logOut });
