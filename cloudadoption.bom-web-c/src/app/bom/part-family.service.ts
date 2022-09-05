import { Injectable } from '@angular/core';
import { PartFamilyClientService } from '../common/api/part-family-client.service';
import { Part, PartFamily } from '../common/api/orm';
import { forkJoin, lastValueFrom, map, mergeMap, of, tap } from 'rxjs';
import { BomClientService } from '../common/api/bom-client.service';
import { MessageService } from '../common/util/message.service';
import { PartClientService } from '../common/api/part-client.service';

@Injectable({
  providedIn: 'root'
})
export class PartFamilyService {

  constructor(private partFamilyClientService: PartFamilyClientService,
              private partClientService: PartClientService,
              private bomClientService: BomClientService,
              private messageService: MessageService) {
  }

  createPartFamily(bomId: string, partFamily: PartFamily): Promise<PartFamily> {
    const partFamilyObservable = this.partFamilyClientService.createPartFamily(partFamily)
      .pipe(
        mergeMap(partFamily => {
          // @ts-ignore
          return this.bomClientService.savePartFamily(bomId, partFamily.id);
        }),
        mergeMap(partFamilyId => {
          return forkJoin(partFamily.parts.map(part => this.partFamilyClientService.savePart(partFamilyId, part)));
        }),
        map(_ => partFamily),
        tap({
          next: partFamily => this.messageService.showInfo(`Part Family ${partFamily.name} saved successfully`),
          error: err => this.messageService.showError(err.error?.message ?? err.message)
        }),
      );
    return lastValueFrom(partFamilyObservable);
  }

  updatePartFamily(partFamily: PartFamily): Promise<PartFamily> {
    const partFamilyObservable = this.partFamilyClientService.updatePartFamily(partFamily)
      .pipe(
        mergeMap(_ => this.partFamilyClientService.getPartFamily(partFamily.id)),
        mergeMap(savedPartFamily => {
          if (savedPartFamily.parts.length === 0) {
            return of(savedPartFamily);
          }
          return forkJoin(savedPartFamily.parts.map(part => {
            // @ts-ignore
            return this.partFamilyClientService.deletePart(savedPartFamily.id, part.partNumber);
          }));
        }),
        mergeMap(_ => {
          return forkJoin(partFamily.parts.map(part => {
            // @ts-ignore
            return this.partFamilyClientService.savePart(partFamily.id, part);
          }));
        }),
        map(_ => partFamily),
        tap({
          next: partFamily => this.messageService.showInfo(`Part Family ${partFamily.name} updated successfully`),
          error: err => this.messageService.showError(err.error?.message ?? err.message)
        }),
      )
    return lastValueFrom(partFamilyObservable);
  }

  getParts(): Promise<Part[]> {
    return lastValueFrom(this.partClientService.getParts()
      .pipe(
        map((next: any[]) => next.map(part => {
          return {
            id: part.id,
            partNumber: part.partNumber,
            unitType: part.unitType,
            assembled: part.assembled,
          } as Part;
        }))
      ));
  }

  getPartFamily(partFamilyId?: number): Promise<PartFamily> {
    if (partFamilyId) {
      return lastValueFrom(this.partFamilyClientService.getPartFamily(partFamilyId).pipe(
        tap({
          error: err => this.messageService.showError(err.error?.message ?? err.message)
        })
      ));
    }
    return Promise.race([ PartFamilyService.newPartFamily() ])
  }

  private static newPartFamily(): PartFamily {
    return {
      name: '',
      parts: []
    };
  }
}
