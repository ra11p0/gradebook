import GetSchoolResponse from "../../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse"
import { store } from "../../../store";

type Stage = {
    uuid: string;
    subjects: {
        uuid: string;
    }[]
}

export default (state = store.getState()): Stage[] => {
    return state.newEducationCycleForm.stages;
}
