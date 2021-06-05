import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { AuthService } from "../user/auth/auth.service";
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map, shareReplay } from 'rxjs/operators';
import { Question } from '../../models/question/question';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  private questionUrl = 'api/questions/';

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) { }

  createQuestion(question: any): Observable<Question> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken
      });
    return this.http.post(this.questionUrl + "createquestion", question, { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        catchError(this.handleError)
      );
  }

  getQuestions(): Observable<Question[]> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken,
        'Content-Type': 'application/json'
      });

    return this.http.get<Question[]>(this.questionUrl + "questionlist", { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        shareReplay(1),
        catchError(this.handleError)
      );
  }

  createComment(comment: any): Observable<Question> {
    const headers = new HttpHeaders
      ({
        "Authorization": "Bearer " + this.authService.getToken,
        'Content-Type': 'application/json'
      });
    return this.http.post(this.questionUrl + "createcomment", comment, { headers: headers })
      .pipe(
        tap((data: any) => {
        }),
        catchError(this.handleError)
      );
  }

  private handleError(err: any) {
    return throwError(err);
  }

}
