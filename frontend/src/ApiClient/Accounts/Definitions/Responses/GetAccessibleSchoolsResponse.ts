export default interface GetAccessibleSchoolsResponse {
    school: School;
    person: Person;
}

interface School {
    guid: string;
    name: string;
    addressLine1: string;
    addressLine2: string;
    postalCode: string;
    city: string;
}

interface Person {
    guid: string;
    name: string;
    addressLine1: string;
    addressLine2: string;
    postalCode: string;
    city: string;
}
