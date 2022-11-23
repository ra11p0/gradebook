export default interface NewEducationCycleRequest {
    guid?: string;
    name: string;
    stages: EducationCycleStepRequest[];
}

export interface EducationCycleStepRequest {
    guid?: string;
    name: string;
    subjects: EducationCycleStepSubjectRequest[];
}

export interface EducationCycleStepSubjectRequest {
    guid?: string;
    subjectGuid: string;
    isMandatory: boolean;
    canUseGroups: boolean;
}
