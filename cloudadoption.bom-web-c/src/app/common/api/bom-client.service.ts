import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Bom } from './orm';
import { BomUrl } from './url';
import { map, Observable } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class BomClientService {

  private api: string;

  constructor(private http: HttpClient, private apiService: ApiService) {
    this.api = apiService.currentApi();
    this.apiService.subscribe(api => this.api = api);
  }

  getBoms(): Observable<Bom[]> {
    const url = this.api + BomUrl.BOMS;
    return this.http.get<Bom[]>(url);
  }

  getBom(bomId: string): Observable<Bom> {
    const url = this.api + BomUrl.BOM.replace(/\{bomId\}/g, bomId);
    return this.http.get<Bom>(url);
  }

  createBom(bom: Bom): Observable<Bom> {
    const url = this.api + BomUrl.BOMS;
    return this.http.post<Bom>(url, bom);
  }

  updateBom(bom: Bom): Observable<Bom> {
    const url = this.api + BomUrl.BOM.replace(/\{bomId\}/g, String(bom.billOfMaterialId));
    return this.http.put<Bom>(url, bom);
  }

  deleteBom(bomId: string): Observable<string> {
    const url = this.api + BomUrl.BOM.replace(/\{bomId\}/g, bomId);
    return this.http.delete<string>(url);
  }

  publishBom(bomId: string): Observable<string> {
    const url = this.api + BomUrl.BOM_PUBLISH.replace(/\{bomId\}/g, bomId);
    return this.http.post<string>(url, {});
  }

  savePartFamily(bomId: string, partFamilyId: number): Observable<number> {
    const url = this.api + BomUrl.PART_FAMILY
      .replace(/\{bomId\}/g, bomId)
      .replace(/\{partFamilyId\}/g, String(partFamilyId));
    return this.http.post<void>(url, {})
      .pipe(map(_ => partFamilyId));
  }
}
