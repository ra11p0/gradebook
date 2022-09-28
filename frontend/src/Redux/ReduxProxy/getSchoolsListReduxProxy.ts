import GetSchoolResponse from "../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse"

export default (state: any): GetSchoolResponse[] | null => {
    if (!state.common.schoolsList) return null;
    return state.common.schoolsList.map((e: any) => e.school)
}
