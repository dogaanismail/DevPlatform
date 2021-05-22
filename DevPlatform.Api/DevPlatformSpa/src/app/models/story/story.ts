export class Story {
    Id: number;
    title: string;
    description: string;
    createdByUserName: string;
    createdByUserPhoto: string;
    imageUrl: string;
    videoUrl: string;
    createdDate: Date;
    storyType: number;
    comments: any = []
}