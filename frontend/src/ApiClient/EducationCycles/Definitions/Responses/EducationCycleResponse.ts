export default interface EducationCycleResponse {
    guid: string;
    schoolGuid: string;
    creatorGuid: string;
    createdDate: Date;
    name: string;
    Stages: Stage[];
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
}