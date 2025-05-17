import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { SharedService } from 'src/app/shared/shared.service';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { UserDto } from 'src/app/shared/models/account/userDto';
import { ConfirmEmail } from 'src/app/shared/models/account/confirmEmails';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit{

  success: boolean = false;

  constructor(private accountService : AccountService,
              private sharedService: SharedService,
              private router: Router,
              private activationRoute: ActivatedRoute
  ){

  }

  ngOnInit(): void {
      this.accountService.user$.pipe(take(1)).subscribe({
        next: (user: UserDto | null) => {
          if(user){
            this.router.navigateByUrl('/');
          }
          else{
            this.activationRoute.queryParamMap.subscribe({
              next: (params: any) => {
                const confirmEmail : ConfirmEmail = {
                  token: params.get('token'),
                  email: params.get('email')
                }

                this.accountService.confirmEmail(confirmEmail).subscribe({
                  next: (response: any) => {
                    this.sharedService.showNotification(true, response.value.title, response.value.message);
                    this.success = true;
                  }
                })
              }
            })
          }
        }
      })
  }

  resendEmailConfirmationLink(){

  }
}
