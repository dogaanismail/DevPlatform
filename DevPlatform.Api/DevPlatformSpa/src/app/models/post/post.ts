import { PostComment } from "./postComment";

export class Post {
    Id: number;
    text: string;
    createdByUserName: string;
    createdByUserPhoto: string;
    imageUrlList: string[] = [];
    videoUrl: string;
    createdDate: Date;
    postType: number;
    comments: PostComment[] = [];
    fancyboxData: string;
}