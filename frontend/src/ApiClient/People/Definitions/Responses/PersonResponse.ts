import SchoolRolesEnum from '../../../../Common/Enums/SchoolRolesEnum';

export default interface PersonResponse extends SimplePersonResponse {
  schoolRole: SchoolRolesEnum;
  activeClassGuid: string;
  activeClass: ClassResponse | undefined;
}

export interface SimplePersonResponse {
  guid: string;
  name: string;
  surname: string;
  schoolRole: SchoolRolesEnum;
  birthday: Date;
}

export interface ClassResponse {
  guid: string;
  name: string;
  description?: string;
  createdDate: Date;
}
