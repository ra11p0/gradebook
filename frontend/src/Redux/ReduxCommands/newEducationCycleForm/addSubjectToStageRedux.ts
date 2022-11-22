import { store } from "../../../store";
import ActionTypes from "../../ActionTypes/newEducationCycleFormActionTypes";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

type Subject = {
    uuid: string
}

export default (subject: Subject, stageUuid: string, dispatch: any = store.dispatch) => {

    dispatch({ type: ActionTypes.AddSubjectToStage, payload: { stageUuid, subject } })
};