import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { PartFamilyUrl, PartUrl } from './url';
import { PartFamily } from './orm';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PartClientService {

  private api: string;

  constructor(private http: HttpClient,
              private apiService: ApiService) {
    this.api = apiService.currentApi();
    this.apiService.subscribe(api => this.api = api);
  }

  getParts(): Observable<any[]> {
    const url = this.api + PartUrl.PARTS;
    return this.http.get<any[]>(url);
  }
}
