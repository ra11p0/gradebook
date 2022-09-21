export default interface LoginResponse {
    access_token: string;
    refreshToken: string;
    username: string;
    userId: string;
    name: string;
    surname: string;
    personGuid: string;
    roles: string[]
}