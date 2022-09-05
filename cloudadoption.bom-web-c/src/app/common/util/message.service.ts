import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private readonly _infoMessages: Subject<string>
  private readonly _errorMessages: Subject<string>

  constructor() {
    this._infoMessages = new Subject<string>();
    this._errorMessages = new Subject<string>();
  }

  public showInfo(message: string, logIt: boolean = false) {
    if (logIt) {
      // tslint:disable-next-line:no-console
      console.info(message);
    }
    this._infoMessages.next(message);
  }

  public showError(message: string, logIt: boolean = true) {
    if (logIt) {
      console.error(message);
    }
    this._errorMessages.next(message);
  }

  public showResponseMessage(response: HttpResponse<any>, logIt: boolean = true) {
    const isError = response.status < 200 || response.status >= 300;
    if (isError) {
      // @ts-ignore
      return this.showError(response['error'] || response.statusText, logIt);
    } else {
      return this.showInfo(response.statusText, logIt);
    }
  }

  get infoMessages(): Observable<string> {
    return this._infoMessages;
  }

  get errorMessages(): Observable<string> {
    return this._errorMessages;
  }

}
