export default interface EducationCycleConfigurationRequest {
  guid: string;
  dateSince: Date;
  dateUntil: Date;
  stages: Stage[];
}

interface Stage {
  guid: string;
  dateSince?: Date;
  dateUntil?: Date;
  subjects: Subject[];
}

interface Subject {
  guid: string;
  teacherGuid: string;
}
