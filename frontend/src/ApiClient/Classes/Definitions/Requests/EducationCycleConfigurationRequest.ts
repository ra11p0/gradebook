export default interface EducationCycleConfigurationRequest {
  guid: string;
  dateSince: Date;
  dateUntil: Date;
  stages: Stage[];
}

interface Stage {
  order: number;
  guid: string;
  dateSince?: Date;
  dateUntil?: Date;
  subjects: Subject[];
}

interface Subject {
  guid: string;
  teacherGuid: string;
}
