import { store } from "../../../store";
import getCurrentSchoolRedux from "./getCurrentSchoolRedux"

export default (state: any = store.getState()): CurrentPersonProxyResult | null => {

    let currentSchool = getCurrentSchoolRedux(state);
    if (!currentSchool) return null;
    let peopleArr = state.common.schoolsList.filter((schoolPerson: any) => schoolPerson.school.guid == currentSchool?.schoolGuid);
    if (peopleArr.length == 0) return null
    return peopleArr[0].person;
}

export interface CurrentPersonProxyResult {
    guid: string;
    name: string;
    surname: string;
    schoolRole: number;
    birthday: Date;
}