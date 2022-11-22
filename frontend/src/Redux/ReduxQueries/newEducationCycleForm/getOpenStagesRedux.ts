import { store } from "../../../store";

export default (state = store.getState()): string[] => {
    return state.newEducationCycleForm.openStages;
}
