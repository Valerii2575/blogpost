import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { SharedService } from 'src/app/shared/shared.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { UserDto } from 'src/app/shared/models/account/userDto';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.css']
})
export class SendEmailComponent implements OnInit{

  emailForm: FormGroup = new FormGroup({});
  submitted = false;
  mode: string | undefined;
  errorMessages: string[] = [];
  navigateToLogin: string = '/account/login';

  constructor(
                  private accountService: AccountService,
                  private sharedService: SharedService,
                  private formBuilder: FormBuilder,
                  private router: Router,
                  private activatedRoute: ActivatedRoute
              )
  {

  }

  ngOnInit(): void {
      this.accountService.user$.pipe(take(1)).subscribe({
        next: (user: UserDto | null) => {
          if(user){
            this.router.navigateByUrl('/')
          }
          else{
            const mode = this.activatedRoute.snapshot.paramMap.get('mode');
            if(mode){
              this.mode = mode;
              console.log(mode);
              this.initializeForm();
            }
          }
        }
      })
  }

  initializeForm(){
    this.emailForm =this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    })
  }

  sendEmail(){
    this.submitted = true;
    this.errorMessages = [];

    if(this.emailForm.valid && this.mode)
      if(this.mode.includes('resend-email-confirmation-link')){
        this.accountService.resendEmailConfirmationLink(this.emailForm.get('email')?.value).subscribe({
          next: (response: any) => {
            this.sharedService.showNotification(true, response.value.title, response.value.message);
            this.router.navigateByUrl(this.navigateToLogin);
          },
          error: error => {
            // TODO if get error
          }
        })
      }
      else if(this.mode.includes('forgot-username-or-password')){
        this.accountService.forgotUsernameOrPassword(this.emailForm.get('email')?.value).subscribe({
          next: (response: any) => {
            this.sharedService.showNotification(true, response.value.tilte, response.value.message);
            this.router.navigateByUrl(this.navigateToLogin);
          },
          error: error => {
            // TODO if get error
          }
        })
      }
  }

  cancel(){
    this.router.navigateByUrl(this.navigateToLogin);
  }
}
