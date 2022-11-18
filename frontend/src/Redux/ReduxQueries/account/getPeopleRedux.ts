import GetSchoolResponse from "../../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse"

interface GetPeopleResponse {
    guid: string;
    name: string;
    surname: string;
    schoolGuid: string;
}

export default (state: any): GetPeopleResponse[] => {
    if (!state.common.schoolsList) return [];
    return state.common.schoolsList.map((e: any) => ({ ...e.person, schoolGuid: e.school.guid }))
}
