import { Injectable } from '@angular/core';
import { Part, PartFamily } from './orm';
import { Observable } from 'rxjs';
import { PartFamilyUrl } from './url';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class PartFamilyClientService {

  private api: string;

  constructor(private http: HttpClient,
              private apiService: ApiService) {
    this.api = apiService.currentApi();
    this.apiService.subscribe(api => this.api = api);
  }

  getPartFamilies(): Observable<PartFamily[]> {
    const url = this.api + PartFamilyUrl.PART_FAMILIES;
    return this.http.get<PartFamily[]>(url);
  }

  getPartFamily(partFamilyId?: number): Observable<PartFamily> {
    const url = this.api + PartFamilyUrl.PART_FAMILY.replace(/\{partFamilyId\}/g, String(partFamilyId));
    return this.http.get<PartFamily>(url);
  }

  createPartFamily(partFamily: PartFamily): Observable<PartFamily> {
    const url = this.api + PartFamilyUrl.PART_FAMILIES;
    return this.http.post<PartFamily>(url, partFamily);
  }

  updatePartFamily(partFamily: PartFamily): Observable<PartFamily> {
    const url = this.api + PartFamilyUrl.PART_FAMILY.replace(/\{partFamilyId\}/g, String(partFamily.id));
    return this.http.put<PartFamily>(url, partFamily);
  }

  deletePartFamily(partFamilyId: string): Observable<string> {
    const url = this.api + PartFamilyUrl.PART_FAMILY;
    return this.http.delete<string>(url);
  }

  savePart(partFamilyId: number, part: Part) {
    const url = this.api + PartFamilyUrl.PART_FAMILY_PARTS
      .replace(/\{partFamilyId\}/g, String(partFamilyId));
    return this.http.post<PartFamily>(url, { part_number: part.partNumber, quantity: part.quantity });
  }

  deletePart(partFamilyId: number, partId: number) {
    const url = this.api + PartFamilyUrl.PART_FAMILY_PART
      .replace(/\{partFamilyId\}/g, String(partFamilyId))
      .replace(/\{partId\}/g, String(partId));
    return this.http.delete<PartFamily>(url, );
  }
}
