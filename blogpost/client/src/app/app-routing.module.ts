import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './shared/components/errors/not-found/not-found.component';
import { AuthorizationGuard } from './shared/guards/authorization.guard';
import { ArticleListComponent } from './pages/articles/article-list/article-list.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthorizationGuard],
    children: [
      {path: 'articles', loadChildren: () => import('./pages/articles/articles.module').then(module => module.ArticlesModule)}
    ]
  },
  // implementing lazy loading by the following format
  {path: 'account', loadChildren: () => import('./pages/account/account.module').then(module => module.AccountModule)},
  //{path: 'articles', component: ArticleListComponent},
  //{path: 'articles', loadChildren: () => import('./pages/articles/articles.module').then(module => module.ArticlesModule)},
  {path: 'not-found', component: NotFoundComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
