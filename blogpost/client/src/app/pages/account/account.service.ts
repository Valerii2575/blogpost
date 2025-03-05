import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of, ReplaySubject } from 'rxjs';
import { GetRefreshUserTokenQueryResult, LoginCommand, LoginCommandResult } from 'src/app/shared/models/login';

import { RegisterCommand, RegisterCommandResult, RegisterDto } from 'src/app/shared/models/register';
import { UserDto } from 'src/app/shared/models/userDto';
import { environment } from 'src/environments/environment.development';
import { LoginComponent } from './login/login.component';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.appUrl;
  api = 'api/account';
  userKey = environment.userKey;

  private userSource = new ReplaySubject<UserDto | null>(1);
  user$ = this.userSource.asObservable();

  constructor(private http: HttpClient, private router : Router) { }

  register(model: RegisterCommand) : Observable<RegisterCommandResult>{

    return this.http.post<RegisterCommandResult>(`${this.baseUrl}/${this.api}/register`, model);
  }

  login(model: LoginCommand) {
    return this.http.post<LoginCommandResult>(`${this.baseUrl}/${this.api}/login`, model).pipe(
      map((userResult: LoginCommandResult) => {
        if(userResult.status == 0){
          this.setUser(userResult.user);
        }
      })
    );
  }

  logout(){
    localStorage.removeItem(this.userKey);
    this.userSource.next(null);
    this.router.navigateByUrl('/');
  }

  refreshUser(jwt: string| null){
    if(jwt === null){
      this.userSource.next(null)
      return of(undefined);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http.get<GetRefreshUserTokenQueryResult>(`${this.baseUrl}/${this.api}/refresh-user-token`, {headers}).pipe(
      map((refreshToken: GetRefreshUserTokenQueryResult) => {
        if(refreshToken){
          this.setUser(refreshToken.userDto);
        }
      })
    );
  }

  getJWT(){
    const key = localStorage.getItem(this.userKey);
    if(key){
      const user : UserDto = JSON.parse(key);
      return user.jwt;
    }
    else{
      return null;
    }
  }

  private setUser(userDto: UserDto){
    localStorage.setItem(this.userKey, JSON.stringify(userDto));
    this.userSource.next(userDto);
  }
}

