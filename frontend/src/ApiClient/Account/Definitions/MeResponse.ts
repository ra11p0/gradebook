export default interface MeResponse {
    id: string;
    userName: string;
    personGuid: string;
    roles?: string[];
    name?: string;
    surname?: string;
    birthday?: Date;
    schoolRole?: number;
}
