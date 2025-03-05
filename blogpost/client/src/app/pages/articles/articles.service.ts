import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GetArticlesResult } from 'src/app/shared/models/articles/articles';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  baseUrl = environment.appUrl;
    api = 'api/article';
    userKey = environment.userKey;

  constructor(private http: HttpClient,
              private router: Router) {}

  getArticles() : Observable<GetArticlesResult>{
    return this.http.get<GetArticlesResult>(`${this.baseUrl}/${this.api}`)
  }
}
