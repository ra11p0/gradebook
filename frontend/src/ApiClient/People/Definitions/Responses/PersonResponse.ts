import SchoolRolesEnum from "../../../../Common/Enums/SchoolRolesEnum";

export default interface PersonResponse {
    guid: string;
    name: string;
    surname: string;
    schoolRole: SchoolRolesEnum;
    birthday: Date;
    activeClassGuid: string;
    activeClass: ClassResponse | undefined;
}

export interface ClassResponse {
    guid: string;
    name: string;
    description?: string;
    createdDate: Date;
}