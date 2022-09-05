import { Injectable } from '@angular/core';
import { BomClientService } from '../common/api/bom-client.service';
import { BehaviorSubject, lastValueFrom, Subscription, tap } from 'rxjs';
import { Bom } from '../common/api/orm';
import { MessageService } from '../common/util/message.service';

@Injectable({
  providedIn: 'root'
})
export class BomService {

  private readonly bomsSubject: BehaviorSubject<Bom[]>;
  private readonly bomSubject: BehaviorSubject<Bom>;

  constructor(private bomClient: BomClientService,
              private messageService: MessageService) {
    this.bomsSubject = new BehaviorSubject<Bom[]>([]);
    this.bomSubject = new BehaviorSubject<Bom>(this.createNewBomObject());
  }

  private createNewBomObject(): Bom {
    return {
      billOfMaterialId: '',
      vehicleId: '',
      status: 'DRAFT',
      partFamilies: []
    };
  }

  loadBoms() {
    lastValueFrom(this.bomClient.getBoms())
      .then(boms => this.bomsSubject.next(boms))
      .catch(error => this.messageService.showError(error.message));
  }

  loadBom(bomId: string) {
    if (!bomId) {
      this.createBom();
      return;
    }
    lastValueFrom(this.bomClient.getBom(bomId))
      .then(bom => this.bomSubject.next(bom))
      .catch(error => this.messageService.showError(error.message));
  }

  subscribeToBoms(next: (value: Bom[]) => void): Subscription {
    return this.bomsSubject.subscribe(next);
  }

  subscribeToBom(next: (value: Bom) => void): Subscription {
    return this.bomSubject.subscribe(next);
  }

  createBom() {
    this.bomSubject.next(this.createNewBomObject());
  }

  saveBom(bom: Bom): Promise<Bom> {
    const observer = {
      next: (bom: Bom) => this.messageService.showInfo(`Bom ${bom.billOfMaterialId} saved successfully`),
      error: (error: any) => this.messageService.showError(error.message)
    };
    const observable = bom.billOfMaterialId ? this.bomClient.updateBom(bom) : this.bomClient.createBom(bom);
    return lastValueFrom(observable.pipe(
      tap(observer)
    ));
  }

  deleteBom(bomId: string): Promise<string> {
    const observer = {
      next: () => this.messageService.showInfo(`Bom ${bomId} successfully deleted`),
      error: (error: any) => this.messageService.showError(error.message)
    };
    return lastValueFrom(this.bomClient.deleteBom(bomId).pipe(
      tap(observer)
    ));
  }

  publishBom(bomId: string): Promise<string> {
    const observer = {
      next: () => this.messageService.showInfo(`Bom ${bomId} successfully activated`),
      error: (error: any) => this.messageService.showError(error.message)
    };
    return lastValueFrom(this.bomClient.publishBom(bomId).pipe(
      tap(observer)
    ));
  }
}
