export class SignedUser {
    userId: number;
    userName: string;
    coverPhotoUrl: string;
    profilePhotoUrl: string;
    registeredDate: Date;
    accessToken: string;
    refreshToken: string;
    expires: number;
}