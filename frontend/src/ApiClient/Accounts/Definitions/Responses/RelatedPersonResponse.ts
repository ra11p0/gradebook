import SchoolRolesEnum from "../../../../Common/Enums/SchoolRolesEnum";

export default interface RelatedPersonResponse {
    guid: string;
    surname: string;
    name: string;
    schoolRole: SchoolRolesEnum;
    birthday: Date;
}