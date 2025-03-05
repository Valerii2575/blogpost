import { Component, OnInit } from '@angular/core';
import { ArticlesService } from '../articles.service';
import { ArticleDto } from 'src/app/shared/models/articles/articles';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css']
})
export class ArticleListComponent implements OnInit {

  articleDtos : ArticleDto[] | any;
  constructor(private articleService: ArticlesService){

  }

  ngOnInit(): void {
    debugger;
    this.getArticles();
  }

  getArticles(){
    return this.articleService.getArticles().subscribe({
      next: result => {
        this.articleDtos = result.articles;
      }
    })
  }
}
