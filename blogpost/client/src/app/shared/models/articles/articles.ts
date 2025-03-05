export interface GetArticlesResult{
  articles: ArticleDto[];
}

export interface ArticleDto{
  id: string;
  title: string;
  description: string;
  content: string;
  author: string;
}
