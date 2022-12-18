export default interface EducationCyclesInClassResponse {
  activeEducationCycleInstance?: EducationCycleInstance;
  activeEducationCycleGuid?: string;
  activeEducationCycle?: EducationCycle;
  hasPreparedActiveEducationCycle: boolean;
  educationCyclesInstances?: EducationCycleInstance[];
  currentStepInstance?: EducationCycleStepInstance;
  previousStepInstance?: EducationCycleStepInstance;
  nextStepInstance?: EducationCycleStepInstance;
}

interface EducationCycle {
  guid: string;
  name: string;
}

interface EducationCycleInstance {
  guid: string;
  educationCycleGuid: string;
  educationCycleName: string;
  dateSince: Date;
  dateUntil: Date;
  creatorGuid: string;
  educationCycleStepInstances: EducationCycleStepInstance[];
}

export interface EducationCycleStepInstance {
  dateSince?: Date;
  started: boolean;
  educationCycleStepName: string;
  dateUntil?: Date;
  guid: string;
  educationCycleInstanceGuid: string;
  order: number;
  educationCycleStepSubjectInstances: EducationCycleStepSubjectInstance[];
}

interface EducationCycleStepSubjectInstance {
  guid: string;
  assignedTeacherGuid: string;
  teacherName: string;
  teacherLastName: string;
  subjectGuid: string;
  educationCycleStepInstanceGuid: string;
  subjectName: string;
  hoursInStep: number;
  isMandatory: boolean;
  groupsAllowed: boolean;
}
