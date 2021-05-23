import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';

/*Models */
import { SignedUser } from '../../../../models/user/signedUser';
import { Story } from '../../../../models/story/story';

/*Services */
import { ModalService } from '../../../../services/modal/modal.service';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation, NgxGalleryImageSize, NgxGalleryLayout } from 'ngx-gallery-9';
import { Post } from 'src/app/models/post/post';

@Component({
  selector: 'app-timeline-stories',
  templateUrl: './timeline-stories.component.html',
  styleUrls: ['./timeline-stories.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TimelineStoriesComponent implements OnInit {

  constructor(private modalService: ModalService) { }

  @Input() signedUser: SignedUser;
  @Input() stories: Story[];
  @Input() posts: Post[];
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];


  ngOnInit(): void {

    this.galleryOptions = [
      {
        width: '600px',
        height: '100px',
        thumbnailsColumns: 4,
        arrowPrevIcon: 'fa fa-chevron-left',
        arrowNextIcon: 'fa fa-chevron-right',
        imageAnimation: NgxGalleryAnimation.Fade,
        previewZoom: true,
        thumbnailsRemainingCount: true,
        previewCloseOnEsc: true,
        previewRotate: true,
        image: false,
        layout: NgxGalleryLayout.ThumbnailsBottom,
        previewDownload: true,
        thumbnailsArrows: true
      },
      {
        breakpoint: 500,
        width: '100%',
        height: '100px',
        imagePercent: 80,
        thumbnailsPercent: 20,
        thumbnailsMargin: 20,
        thumbnailMargin: 20
      },
      {
        breakpoint: 400,
        preview: false
      }
    ];

    this.posts.forEach((item, index) => {
      if (item.imageUrlList.length > 0) {
        this.galleryImages.push(new NgxGalleryImage({
          small: item.imageUrlList[0],
          medium: item.imageUrlList[0],
          big: item.imageUrlList[0],
        }))
      }
    });
  }


  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
