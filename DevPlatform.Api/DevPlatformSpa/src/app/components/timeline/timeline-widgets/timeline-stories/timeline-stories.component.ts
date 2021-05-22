import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';

/*Models */
import { SignedUser } from '../../../../models/user/signedUser';
import { Story } from '../../../../models/story/story';

/*Services */
import { ModalService } from '../../../../services/modal/modal.service';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation, NgxGalleryImageSize } from 'ngx-gallery-9';
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

    console.log(this.posts);

    this.galleryOptions = [
      {
        width: '100%',
        height: '254px',
        imageAutoPlay: true,
        imageAutoPlayPauseOnHover: true,
        imageSize: NgxGalleryImageSize.Contain,
        imageAnimation: NgxGalleryAnimation.Slide,
        thumbnails: true,
        preview: true
      },
      // max-width 544
      {
        breakpoint: 544,
        width: '100%',
        height: '250px',
        imageSwipe: true,
        imageArrowsAutoHide: true,
        imageAutoPlay: true,
        imageAutoPlayPauseOnHover: true,
        imageSize: NgxGalleryImageSize.Cover,
        imageAnimation: NgxGalleryAnimation.Slide,
        thumbnails: true,
        preview: true
      },
      {
        breakpoint: 544,
        width: '100%',
        height: '250px',
        imageSwipe: true,
        imageArrowsAutoHide: true,
        imageAutoPlay: true,
        imageAutoPlayPauseOnHover: true,
        imageSize: NgxGalleryImageSize.Cover,
        imageAnimation: NgxGalleryAnimation.Slide,
        thumbnails: true,
        preview: true
      }
    ];

    this.galleryImages = [
      {
        small: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        medium: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        big: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png'
      },
      {
        small: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        medium: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        big: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png'
      },
      {
        small: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        medium: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        big: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png'
      }
      ,
      {
        small: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        medium: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        big: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png'
      },
      ,
      {
        small: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        medium: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        big: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png'
      },
      ,
      {
        small: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        medium: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        big: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png'
      },
      ,
      {
        small: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        medium: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        big: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png'
      },
      ,
      {
        small: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        medium: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png',
        big: 'https://res.cloudinary.com/dkkr1ddp3/image/upload/v1620896972/vvknqckcansuzyjxqx8b.png'
      },
    ];
  }


  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
