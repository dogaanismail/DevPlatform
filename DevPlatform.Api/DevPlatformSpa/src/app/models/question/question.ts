import { TagModel } from "ngx-chips/core/accessor";

export class Question {
    Id: number;
    title: string;
    description: string;
    tags: string[] = [];
    createdByUserName: string;
    createdByUserPhoto: string;
    createdDate: Date;
    comments: any = [];
}