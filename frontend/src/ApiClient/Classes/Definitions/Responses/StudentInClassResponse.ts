import SchoolRolesEnum from "../../../../Common/Enums/SchoolRolesEnum";

export default interface StudentInClassResponse {
    guid: string;
    creatorGuid: string;
    userGuid: string;
    name: string;
    surname: string;
    schoolRole: SchoolRolesEnum;
    birthday: Date;
    isActive: boolean;
}