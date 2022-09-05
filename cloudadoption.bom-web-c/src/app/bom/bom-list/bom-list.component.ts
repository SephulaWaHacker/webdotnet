import {Component, OnInit} from '@angular/core';
import {Bom} from '../../common/api/orm';
import {BomService} from '../bom.service';
import {Router} from '@angular/router';
import {ColDef, SelectionChangedEvent} from 'ag-grid-community';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'app-bom-list',
  templateUrl: './bom-list.component.html',
  styleUrls: ['./bom-list.component.scss']
})
export class BomListComponent implements OnInit {

  boms: Bom[];
  defaultColDef: ColDef;
  columnDefs: ColDef[] = [];

  constructor(private router: Router,
              private translator: TranslateService,
              private bomService: BomService) {
    this.boms = [];
    this.defaultColDef = defaultColDef;
   
  }

  ngOnInit(): void {
    this.bomService.subscribeToBoms(boms => this.boms = boms)
    this.bomService.loadBoms();
  }

  onCreate() {
    this.bomService.createBom();
    this.router.navigate(['/boms', 'create']);
  }

  onSelectionChanged(event: SelectionChangedEvent) {

  }
}

const columnDefs = [
  {
    headerName: 'BILL_OF_MATERIAL_ID',
    field: 'billOfMaterialId'
  },
  {
    headerName: 'VEHICLE_ID',
    field: 'vehicleId'
  },
  {
    headerName: 'START_DATE',
    field: 'startDate'
  },
  {
    headerName: 'END_DATE',
    field: 'endDate'
  },
  {
    headerName: 'STATUS',
    field: 'status'
  },
];

const defaultColDef = {
  flex: 1,
  sortable: false,
};
