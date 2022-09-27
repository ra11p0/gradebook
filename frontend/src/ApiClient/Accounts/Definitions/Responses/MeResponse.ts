import GetAccessibleSchoolsResponse from "./GetAccessibleSchoolsResponse";

export default interface MeResponse {
    userId: string;
    schools: GetAccessibleSchoolsResponse[];
}
