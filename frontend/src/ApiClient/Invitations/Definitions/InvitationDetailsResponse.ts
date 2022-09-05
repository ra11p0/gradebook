export interface Person{
    name: string;
    surname: string;
    birthday: Date;
    class: any;
    group: any;
}

export interface InvitationDetailsResponse{
    person: Person;
}
