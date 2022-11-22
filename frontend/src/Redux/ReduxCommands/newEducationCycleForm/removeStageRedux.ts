import { store } from "../../../store";
import ActionTypes from "../../ActionTypes/newEducationCycleFormActionTypes";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

export default (uuid: string, dispatch: any = store.dispatch) => {

    dispatch({ type: ActionTypes.RemoveStage, payload: { uuid } })
};