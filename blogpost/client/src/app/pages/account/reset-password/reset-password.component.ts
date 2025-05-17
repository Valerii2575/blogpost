import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { SharedService } from 'src/app/shared/shared.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { UserDto } from 'src/app/shared/models/account/userDto';
import { ResetPassword } from 'src/app/shared/models/account/resetPassword';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordForm: FormGroup = new FormGroup({});
  token: string | undefined;
  email: string | undefined;
  submitted = false;
  errorMessages: string[] = [];

  constructor(
                private accountService: AccountService,
                private sharedService: SharedService,
                private formBuilder: FormBuilder,
                private router: Router,
                private activatedRoute: ActivatedRoute
  ){

  }

  ngOnInit(): void {
    this.accountService.user$.pipe(take(1)).subscribe({
      next: (user: UserDto | null) => {
                if(user){
                  this.router.navigateByUrl('/')
                }
                else{
                  const mode = this.activatedRoute.queryParamMap.subscribe({
                    next: (params: any) => {
                      this.token = params.get('token');
                      this.email = params.get('email');

                      if(this.token && this.email){
                        this.initializeFrom(this.email);
                      }
                      else{
                        this.router.navigateByUrl('/account/login');
                      }
                    }
                  })
                }
              }
    })
  }

  initializeFrom(username: string){
    this.resetPasswordForm = this.formBuilder.group({
      email: [{value: username, disabled: true}],
      newPassword: ['', Validators.required, Validators.minLength(6), Validators.maxLength(15)]
    })
  }

  resetPassword(){
    this.submitted = true;
    this.errorMessages = [];

    if(this.resetPasswordForm.valid && this.email && this.token){
      const model: ResetPassword = {
        token: this.token,
        email: this.email,
        newPassword: this.resetPasswordForm.get('newPassword')?.value
      }

      this.accountService.resetPassword(model).subscribe({
        next: (response: any) => {
          this.sharedService.showNotification(true, response.value.title, response.value.message);
          this.router.navigateByUrl('/account/login');
        },
        error: error => {

        }
      })
    }
  }
}
