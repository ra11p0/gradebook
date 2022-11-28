import PersonResponse from './PersonResponse';

export default interface InvitationResponse {
  guid: string;
  createdDate: Date;
  exprationDate?: Date;
  invitationCode: string;
  isUsed: boolean;
  creatorGuid: string;
  usedDate?: Date;
  invitedPersonGuid?: string;
  schoolRole?: string;
  invitedPerson?: PersonResponse;
}
