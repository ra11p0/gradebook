import PersonResponse from '../../../People/Definitions/Responses/PersonResponse';

export default interface EducationCycleResponse {
  guid: string;
  schoolGuid: string;
  creatorGuid: string;
  createdDate: Date;
  name: string;
  stages: Stage[];
  creator: PersonResponse;
}

interface Stage {
  guid: string;
  name: string;
  order: number;
  subjects: Subject[];
}

interface Subject {
  guid: string;
  subjectGuid: string;
  hoursInStep: number;
  isMandatory: boolean;
  groupsAllowed: boolean;
  subjectName: string;
}
