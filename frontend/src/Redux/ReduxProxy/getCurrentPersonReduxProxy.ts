import { currentSchoolProxy } from "./getCurrentSchoolReduxProxy"

export const currentPersonProxy = (state: any): CurrentPersonProxyResult | null => {

    let currentSchool = currentSchoolProxy(state);
    if (!currentSchool) return null;
    let peopleArr = state.common.schoolsList.filter((schoolPerson: any) => schoolPerson.school.guid == currentSchool?.schoolGuid);
    if (peopleArr.length == 0) return null
    return peopleArr[0].person;
}

interface CurrentPersonProxyResult {
    guid: string;
    name: string;
    surname: string;
    schoolRole: number;
    birthday: Date;
}
