export default interface NewSchoolRequest {
    name: string;
    addressLine1: string;
    addressLine2?: string;
    postalCode: string;
    city: string;
}
