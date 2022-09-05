import { Injectable } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiSubject: BehaviorSubject<string>;

  constructor() {
    const api = localStorage.getItem('api');
    this.apiSubject = new BehaviorSubject<string>(api ?? 'java');
  }

  subscribe(next: (value: string) => void): Subscription {
    return this.apiSubject.subscribe(next);
  }

  next(api: string) {
    if (api !== this.currentApi()) {
      this.apiSubject.next(api);
      localStorage.setItem('api', api);
      location.reload();
    }
  }

  currentApi(): string {
    return this.apiSubject.value;
  }
}
