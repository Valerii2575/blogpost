import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { SharedService } from '../shared.service';
import { AccountService } from 'src/app/pages/account/account.service';
import { map, Observable } from 'rxjs';
import { UserDto } from '../models/userDto';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })

export class AuthorizationGuard {

  constructor(private accountService: AccountService,
            private sharedService: SharedService,
            private router: Router
  ){}

  canActivate(route: ActivatedRouteSnapshot,
            state: RouterStateSnapshot) : Observable<boolean>
  {

    return this.accountService.user$.pipe(
          map((user: UserDto | null) => {

            if(user){
              return true;
            }
            else{
              this.sharedService.showNotification(false, 'Restricted Area', 'Leave immediately!');
              this.router.navigate(['account/login'], {queryParams: { reurnUrl : state.url}});
              return false;
            }
          })
        );
  }
}
