import SchoolRolesEnum from '../../../../Common/Enums/SchoolRolesEnum';

export default interface InviteMultiplePeopleRequest {
  invitedPersonGuidArray: string[];
  role: SchoolRolesEnum;
}
