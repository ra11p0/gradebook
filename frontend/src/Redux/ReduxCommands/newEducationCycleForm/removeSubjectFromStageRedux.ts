import { store } from "../../../store";
import ActionTypes from "../../ActionTypes/newEducationCycleFormActionTypes";
import getStagesRedux from "../../ReduxQueries/newEducationCycleForm/getStagesRedux";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

export default (subjectUuid: string, dispatch: any = store.dispatch) => {
    const stages = getStagesRedux();
    const stageUuid = stages.find(st => st.subjects.map(sb => sb.uuid).includes(subjectUuid))?.uuid;
    dispatch({ type: ActionTypes.RemoveSubjectFromStage, payload: { stageUuid, subjectUuid } })
};