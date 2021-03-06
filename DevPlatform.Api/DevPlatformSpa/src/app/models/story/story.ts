export class Story {
    Id: number;
    text: string;
    createdByUserName: string;
    createdByUserPhoto: string;
    imageUrl: string;
    videoUrl: string;
    createdDate: Date;
    storyType: number;
    comments: any = []
}