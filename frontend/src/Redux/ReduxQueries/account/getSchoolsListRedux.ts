import GetSchoolResponse from "../../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse"

export default (state: any): GetSchoolResponse[] => {
    if (!state.common.schoolsList) return [];
    return state.common.schoolsList.map((e: any) => e.school)
}
