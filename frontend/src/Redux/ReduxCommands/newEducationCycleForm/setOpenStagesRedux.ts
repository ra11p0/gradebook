import { store } from "../../../store";
import ActionTypes from "../../ActionTypes/newEducationCycleFormActionTypes";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

export default (open: string[], dispatch: any = store.dispatch) => {

    dispatch({ type: ActionTypes.SetOpenStages, payload: { open } })
};