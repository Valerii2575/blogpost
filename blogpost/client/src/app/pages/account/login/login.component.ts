import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginCommand } from 'src/app/shared/models/login';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { UserDto } from 'src/app/shared/models/userDto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup = new FormGroup({});
  submitted = false;
  errorMessages : string | undefined ;
  returnUrl: string = '';

  constructor(private accountService: AccountService,
              private router: Router,
              private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute){
    this.accountService.user$.pipe(take(1)).subscribe({
      next: (user: UserDto | null) => {
        if(user){
          this.router.navigateByUrl('/');
        }
        else{
          this.activatedRoute.queryParamMap.subscribe({
            next: (params: any) => {
              if(params){
                this.returnUrl = params.get('returnUrl');
              }
            }
          })
        }
      }
    })
  }

  initializeForm(){
      this.loginForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
      })
    }

  ngOnInit(): void {
    this.initializeForm();
  }

  login(){
    this.submitted = true;
    this.errorMessages = "";
    if(this.loginForm.valid){
      const formValue = this.loginForm.value;
      const loginCommand : LoginCommand = {
        LoginDto: {
          email : formValue.email,
          password : formValue.password
        }
      };

      this.accountService.login(loginCommand).subscribe({
        next: (response: any) => {
          if(this.returnUrl){
              this.router.navigateByUrl(this.returnUrl);
          }
          else{
            this.router.navigateByUrl('/');
          }
        }
      })

    }
  }
}
