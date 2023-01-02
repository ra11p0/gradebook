import SchoolRolesEnum from '../../../../Common/Enums/SchoolRolesEnum';

export default interface PeoplePickerData {
  schoolGuid: string;
  query?: string;
  includeDeleted?: boolean;
  activeClassGuid?: string;
  birthdaySince?: Date;
  birthdayUntil?: Date;
  schoolRole?: SchoolRolesEnum;
}
