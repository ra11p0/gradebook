export interface Class {
    name: string;
}

export interface Group {
    name: string;
}

export interface Person {
    name: string;
    surname: string;
    birthday: Date;
    class: any;
    group: any;
}

export interface InvitationDetailsResponse {
    person: Person;
    class: Class;
    group: Group;
}
