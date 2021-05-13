export class Post {
    Id: number;
    text: string;
    createdByUserName: string;
    createdByUserPhoto: string;
    imageUrlList: string[] = [];
    videoUrl: string;
    createdDate: Date;
    postType: number;
    comments: any = [];
    fancyboxData: string;
}