export default interface StudentInSchoolResponse {
    guid: string;
    creatorguid: string;
    groupGuid: string;
    classGuid: string;
    name: string;
    surname: string;
    schoolRole: number;
    birthday: Date;
    isActive: boolean;
    userGuid: string;
}