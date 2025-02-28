import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ArticleListComponent } from './article-list/article-list.component';

const routers : Routes = [
  {path: 'list', component: ArticleListComponent}
]

@NgModule({
  declarations: [],
  imports: [
      RouterModule.forChild(routers)
  ],
  exports: [
    RouterModule
  ]
})
export class ArticlesRoutingModule { }
