export default interface LoginResponse{
    token: string;
    refreshToken: string;
    expiration: Date;
}