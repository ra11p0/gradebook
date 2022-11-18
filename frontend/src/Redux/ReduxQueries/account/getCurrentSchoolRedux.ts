import { store } from "../../../store";

export default (state: any = store.getState()): getCurrentSchoolReduxProxyResult | null => {
    if (state.common?.school == null) {
        if (state.common?.schoolsList && state.common?.schoolsList?.length != 0) {
            return {
                schoolName: state.common.schoolsList[0].school.name,
                schoolGuid: state.common.schoolsList[0].school.guid,
            }
        }
        return null;
    }
    return state.common?.school;
};

interface getCurrentSchoolReduxProxyResult {
    schoolName: string;
    schoolGuid: string;
}
