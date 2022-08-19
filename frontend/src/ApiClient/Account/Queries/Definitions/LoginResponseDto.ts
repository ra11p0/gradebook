export default interface LoginResponseDto{
    token: string;
    refreshToken: string;
    expiration: Date;
}