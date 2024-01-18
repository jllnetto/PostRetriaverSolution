import { Component } from '@angular/core';
import { IPost } from './Models/posts';
import { PostService } from './Services/postService';

@Component({
  selector: 'pr-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  pageTitle = 'Posts View';
  posts :IPost[]=[];
  private _search = '';
  get search(): string {
    return this._search;
  }
  set search(value: string) {
    this._search = value;    
  }
  sortByOptions = ['id', 'reads', 'likes', 'popularity'];
  directionOptions = [
    { label: 'Ascending', value: 'asc' },
    { label: 'Descending', value: 'desc' }
  ];

  selectedSortBy: string = 'id';
  selectedDirection: string = 'asc';
  onSortByChange(sortBy: string): void {
    this.selectedSortBy = sortBy;   
  }

  onDirectionChange(direction: string): void {
    this.selectedDirection = direction;
  }



  constructor(private postservice:PostService) {}


  SerchPosts() {
      if(this._search.trim() === '')
      {
        alert("should add at least one tag");
      }
      else
      {
        var tags = this._search.split(' ');

        this.postservice.getPosts(tags, this.selectedSortBy, this.selectedDirection).subscribe(
          (data) => {
          this.posts = data;
          },
          (error) => {
            console.error('Error fetching posts:', error);
          }
        );
      }    
    }  


}
