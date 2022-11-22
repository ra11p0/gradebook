import { store } from "../../../store";
import ActionTypes from "../../ActionTypes/newEducationCycleFormActionTypes";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

type Stage = {
    uuid: string
}

export default (stages: Stage[], dispatch: any = store.dispatch) => {

    dispatch({ type: ActionTypes.SetStages, payload: { stages } })
};