import { Component, OnDestroy, OnInit } from '@angular/core';
import {Bom, PartFamily} from '../../common/api/orm';
import {ActivatedRoute, Router} from '@angular/router';
import {BomService} from '../bom.service';
import {Subscription} from 'rxjs';
import {ColDef, SelectionChangedEvent} from 'ag-grid-community';
import {TranslateService} from '@ngx-translate/core';
import {BaseColDefParams} from 'ag-grid-community/dist/lib/entities/colDef';
import {MessageService} from '../../common/util/message.service';

@Component({
  selector: 'app-bom-overview',
  templateUrl: './bom-overview.component.html',
  styleUrls: ['./bom-overview.component.scss']
})
export class BomOverviewComponent implements OnInit, OnDestroy {

  private subscriptions: Subscription[];

  bom!: Bom;
  columnDefs: ColDef[];
  defaultColDef: ColDef;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private messageService: MessageService,
              private translator: TranslateService,
              private bomService: BomService) {
    this.subscriptions = [];
    this.defaultColDef = defaultColDef;
    this.columnDefs = columnDefs.map(field => {
      return {...field, headerName: translator.instant(field.headerName)}
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  ngOnInit(): void {
    let subscription = this.route.params.subscribe(params => {
      const bomId = params['bom-id'];
      this.bomService.loadBom(bomId);
    });
    this.subscriptions.push(subscription);
    subscription = this.bomService.subscribeToBom(bom => this.bom = bom);
    this.subscriptions.push(subscription);
  }

  onModifyBom(): void {
    this.router.navigate(['/boms', this.bom.billOfMaterialId, 'modify'])
  }

  onPublishBom() {
    this.bomService.publishBom(this.bom.billOfMaterialId)
      .then(_ => this.onBack())
  }

  onDeleteBom() {
    this.bomService.deleteBom(this.bom.billOfMaterialId)
      .then(_ => this.onBack())
  }

  onAddPartFamily(): void {
    this.router.navigate(['/boms', this.bom.billOfMaterialId, 'part-family'])
  }

  onBack() {
    this.router.navigate(['/boms']);
  }

  onSelectionChanged(event: SelectionChangedEvent) {
    const selectedPartFamily = event.api.getSelectedRows()[0] as PartFamily;
    this.router.navigate([ '/boms', this.bom.billOfMaterialId, 'part-family', selectedPartFamily.id ]);
  }
}

const columnDefs = [
  {
    headerName: 'NAME',
    field: 'name'
  },
  {
    headerName: 'PART_FAMILY_ID',
    field: 'partFamilyId'
  },
  {
    headerName: 'PARTS_COUNT',
    valueGetter: (params: BaseColDefParams) => params.data.parts?.length ?? 0
  }
];

const defaultColDef = {
  flex: 1,
  sortable: false,
};
