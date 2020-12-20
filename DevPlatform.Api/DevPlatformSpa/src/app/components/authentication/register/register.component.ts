import { Component, OnInit } from '@angular/core';
import { RegisterUser } from '../../../models/user/registerUser';
import { STEP_ITEMS } from '../../../core/costants/multi-step-form';
import { Router } from '@angular/router';
import { AlertifyService } from '../../../services/alertify/alertify.service';
import { AuthService } from '../../../services/user/auth/auth.service';

/* NgRx */
import { Store } from "@ngrx/store";
import * as fromUser from "../../../core/ngrx/selectors/user.selectors";
import * as userActions from "../../../core/ngrx/actions/user.actions";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private alertifyService: AlertifyService,
    private userStore: Store<fromUser.State>,
    private router: Router,
  ) { }


  formContent: any;
  formData: any;
  activeStepIndex: number;
  registerUser: RegisterUser;

  ngOnInit(): void {
    this.formContent = STEP_ITEMS;
    this.formData = {};
  }

  onFormSubmit(formData: any): void {
    this.formData = formData;
    this.registerUser = Object.assign({}, this.formData);
    this.authService.register(this.registerUser).subscribe((data: any) => {
      if (data.result.status === false) {
        this.alertifyService.error(data.result.message.toString());
        this.userStore.dispatch(new userActions.RegisterFail(data.result.message));
      }
      else {
        this.userStore.dispatch(new userActions.RegisterSuccess(data.result.message));
        this.alertifyService.success("You can login !");
        this.router.navigate(["/account/login"]);
      }
    })

  }

}
