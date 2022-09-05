import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  private loadingRequests: number;
  private readonly loading: Subject<boolean>;


  constructor() {
    this.loadingRequests = 0;
    this.loading = new BehaviorSubject<boolean>(false);
  }

  public onNewRequest(): void {
    this.loadingRequests++;
    this.loading.next(true);
  }

  public onFinishedRequest() {
    this.loadingRequests--;
    if (this.loadingRequests == 0) {
      this.loading.next(false);
    }
  }

  get isLoading(): Observable<boolean> {
    return this.loading;
  }

}
