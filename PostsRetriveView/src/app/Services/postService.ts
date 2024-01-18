import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPost } from '../Models/posts';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiUrl = 'https://localhost:7202/Posts'; 

  constructor(private http: HttpClient) { }

  getPosts(tags: string[], sortBy: string, direction: string): Observable<any[]> {
    let params = new HttpParams()
      .set('sortBy', sortBy)
      .set('direction', direction);

    // Add a separate parameter for each tag
    tags.forEach(tag => {
      params = params.append('tags', tag);
    });

    return this.http.get<IPost[]>(this.apiUrl, { params });
  }
}