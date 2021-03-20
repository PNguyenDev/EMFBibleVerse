export class Verse implements IVerse {
  public id: string | '';
  public verseText: string | '';
  public imageLink: string | '';
  public book: string | '';
  public chapter: string | '';
  public verseNumbers: string | '';
  public isFavorite: boolean | false;

  constructor() { }

  getCitationInfo(): string {
    let returnVal = `${this.book} ${this.chapter}:${this.verseNumbers}`;
    return returnVal;
  }
}

export interface IVerse {
  id: string;
  verseText: string;
  imageLink: string;
  book: string;
  chapter: string;
  verseNumbers: string;
  isFavorite: boolean;
  getCitationInfo(): string;
}

export interface IGetVerseResponse {
  verses: Array<Verse>,
  pageSize: number,
  pageNumber: number,
  hasMorePages: boolean
}
