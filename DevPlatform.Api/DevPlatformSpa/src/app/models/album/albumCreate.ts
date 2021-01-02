export class AlbumCreate {
    name: string;
    place: string;
    date: Date;
    tag: string;
    images: File[] = [];
}