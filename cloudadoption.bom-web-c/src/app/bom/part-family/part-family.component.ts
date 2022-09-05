import { Component, OnDestroy, OnInit } from '@angular/core';
import { Bom, Part, PartFamily } from '../../common/api/orm';
import { BomService } from '../bom.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { MessageService } from '../../common/util/message.service';
import { PartFamilyService } from '../part-family.service';

@Component({
  selector: 'app-part-family',
  templateUrl: './part-family.component.html',
  styleUrls: [ './part-family.component.scss' ]
})
export class PartFamilyComponent implements OnInit, OnDestroy {

  partFamily!: PartFamily;
  billOfMaterialId!: string;
  subscriptions: Subscription[]
  partFamilyId?: number;
  parts!: Part[];

  constructor(private bomService: BomService,
              private partFamilyService: PartFamilyService,
              private route: ActivatedRoute,
              private messageService: MessageService,
              private router: Router) {
    this.subscriptions = [];
    const subscription = this.route.params.subscribe(params => {
      this.billOfMaterialId = params['bom-id'];
      this.partFamilyId = params['part-family-id'];
      this.bomService.loadBom(this.billOfMaterialId);
    });
    this.subscriptions.push(subscription);
  }

  ngOnInit(): void {
    this.partFamilyService.getParts()
      .then(parts => this.parts = parts);
    this.partFamilyService.getPartFamily(this.partFamilyId)
      .then(partFamily => this.partFamily = partFamily);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(subscription => subscription.unsubscribe())
  }

  onAddPart(): void {
    const part = {} as Part;
    this.partFamily.parts.push(part)
  }

  onSave(): void {
    console.log("before save", this.partFamily)
    if (this.partFamily.id) {
      this.partFamilyService.updatePartFamily(this.partFamily)
        .then(_ => this.onBack())
    } else {
      this.partFamilyService.createPartFamily(this.billOfMaterialId, this.partFamily)
        .then(_ => this.onBack())
    }
  }

  onBack(): void {
    this.router.navigate([ '/boms', this.billOfMaterialId ]);
  }

  onSelectPart(partNumber: string, part: Part) {
    const partDetails = this.parts?.find(part => part.partNumber === partNumber);
    if (partDetails) {
      part.id = partDetails?.id;
      part.unitType = partDetails?.unitType;
      part.assembled = partDetails?.assembled;
    }
    console.log('before 0', this.partFamily, part);
  }

  onDelete(part: Part) {
    const index = this.partFamily.parts.findIndex($ => $ === part);
    if (index >= 0) {
      this.partFamily.parts.splice(index, 1);
    }
  }
}
