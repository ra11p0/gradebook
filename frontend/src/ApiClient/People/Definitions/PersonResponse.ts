import SchoolRolesEnum from "../../../Common/Enums/SchoolRolesEnum";

export default interface PersonResponse {
    guid: string;
    name: string;
    surname: string;
    schoolRole: SchoolRolesEnum;
    birthday: Date;
}