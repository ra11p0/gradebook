import PersonResponse from "./PersonResponse";

export interface Class {
    name: string;
}

export interface Group {
    name: string;
}

export interface InvitationDetailsResponse {
    person: PersonResponse;
    class: Class;
    group: Group;
}
