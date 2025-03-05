import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterCommand } from 'src/app/shared/models/register';
import { SharedService } from 'src/app/shared/shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{

registerForm: FormGroup = new FormGroup({});
submitted = false;
errorMessages : string | undefined ;

  constructor(private accountService: AccountService,
    private sharedService: SharedService,
    private formBuilder: FormBuilder,
    private router: Router
  ){

  }
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      //confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
    })
  }

  register(){
    this.submitted = true;
    this.errorMessages = "";

    if(this.registerForm.valid){
      const formValue = this.registerForm.value;
      const registerCommand: RegisterCommand = {
        RegisterDto: {
          firstName: formValue.firstName,
          lastName: formValue.lastName,
          email: formValue.email,
          password: formValue.password
        }
      };
      this.accountService.register(registerCommand).subscribe({
        next: (response) => {
          if(response.status != 0){
            this.errorMessages = response.message
          }
          else{
            this.sharedService.showNotification(true, 'Register', response.message);
            this.router.navigateByUrl('/account/login');
          }
        },
        error: error => {
          console.log(error);
        }
      })
    }

  }
}
