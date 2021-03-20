import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { IGetVerseResponse, Verse } from '../models/verse';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  public verses: Array<Verse>;
  public favoriteVerses: Array<Verse>;
  public startDate: FormControl;
  public pageSize: FormControl;
  private baseUrl: string;
  private http: HttpClient;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
    this.getFavoriteVerses();
    this.startDate = new FormControl();
    this.pageSize = new FormControl(1);
  }

  public getBibleVerses() {
    let getVerseRequest = { StartDate: this.startDate.value, PageSize: this.pageSize.value };
    this.http.post(this.baseUrl + 'Home/GetVerse', getVerseRequest, this.httpOptions).subscribe((result: IGetVerseResponse) => {
      this.verses = result.verses;
    }, error => console.error(error));
  }

  public saveFavoriteVerse(verse: Verse, makeFavorite: boolean) {
    let found = this.verses.find(v => v.id === verse.id),
      saveFavoriteVerseRequest = { Verse: verse, MakeFavorite: makeFavorite };
    found.isFavorite = makeFavorite;
    this.http.post(this.baseUrl + 'Home/SaveFavoriteVerse', saveFavoriteVerseRequest, this.httpOptions)
      .subscribe((result: boolean) => {
        if (result) {
          this.getFavoriteVerses();
        }
      }, error => console.error(error));
  }

  public getFavoriteVerses() {
    this.http.get(this.baseUrl + 'Home/GetFavoriteVerses', this.httpOptions).subscribe((result: IGetVerseResponse) => {
      this.favoriteVerses = result.verses;
    }, error => console.error(error));
  }

  public showFavoriteVerses(): boolean {
    return this.favoriteVerses && this.favoriteVerses.length > 0;
  }

  public showGetVerses(): boolean {
    return this.verses && this.verses.length > 0;
  }
}
