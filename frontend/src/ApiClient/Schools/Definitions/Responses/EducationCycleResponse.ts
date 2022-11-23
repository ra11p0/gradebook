import SchoolRolesEnum from "../../../../Common/Enums/SchoolRolesEnum";

export default interface EducationCycleResponse {
    name: string;
    createdDate: Date;
    creatorGuid: string;
    creator: Creator;
}

interface Creator {
    guid: string;
    name: string;
    surname: string;
    schoolRole: SchoolRolesEnum;
    birthday: Date;
}
