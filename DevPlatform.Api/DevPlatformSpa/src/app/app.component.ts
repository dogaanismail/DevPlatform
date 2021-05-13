import { Component, OnInit } from '@angular/core';

/* Models */
import { SignedUser } from './models/user/signedUser';

/* Rxjs */
import { Observable } from 'rxjs';

/* NgRx */
import { Store, select } from '@ngrx/store';
import * as fromUser from './core/ngrx/selectors/user.selectors';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'DevPlatformSpa';
  signedUser$: Observable<SignedUser>;

  constructor(private userStore: Store<fromUser.State>) {
  }

  ngOnInit(): void {
    this.signedUser$ = this.userStore.pipe(select(fromUser.getSignedUser)) as Observable<SignedUser>;
  }

}
