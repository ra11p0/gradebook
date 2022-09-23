export const currentSchoolProxy = (state: any): CurrentSchoolProxyResult | null => {
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

interface CurrentSchoolProxyResult {
    schoolName: string;
    schoolGuid: string;
}
