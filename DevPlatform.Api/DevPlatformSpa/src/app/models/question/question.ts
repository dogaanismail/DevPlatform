export class Question {
    Id: number;
    text: string;
    description:string;
    createdByUserName: string;
    createdByUserPhoto: string;
    createdDate: Date;
    comments: any = [];
}