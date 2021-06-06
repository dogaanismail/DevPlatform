import { TagModel } from "ngx-chips/core/accessor";

export class QuestionCreate {
    title: string;
    description: string;
    tags: TagModel[] = [];
}