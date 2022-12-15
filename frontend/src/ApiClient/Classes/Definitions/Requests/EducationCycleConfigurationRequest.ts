export default interface EducationCycleConfigurationRequest {
  educationCycleGuid: string;
  dateSince: Date;
  dateUntil: Date;
  stages: Stage[];
}

interface Stage {
  order: number;
  educationCycleStageGuid: string;
  dateSince?: Date;
  dateUntil?: Date;
  subjects: Subject[];
}

interface Subject {
  educationCycleStageSubjectGuid: string;
  teacherGuid: string;
}
