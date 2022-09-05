import { Component, OnDestroy, OnInit } from '@angular/core';
import { Bom } from '../../common/api/orm';
import { ActivatedRoute, Router } from '@angular/router';
import { BomService } from '../bom.service';
import { Subscription } from 'rxjs';
import { MessageService } from '../../common/util/message.service';

@Component({
  selector: 'app-bom-detail',
  templateUrl: './bom-detail.component.html',
  styleUrls: [ './bom-detail.component.scss' ]
})
export class BomDetailComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[];

  bom!: Bom;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private messageService: MessageService,
              private bomService: BomService) {
    this.subscriptions = [];
  }

  ngOnInit(): void {
    let subscription = this.route.params.subscribe(params => {
      let bomId = params['bom-id'];
      this.bomService.loadBom(bomId);
    });
    this.subscriptions.push(subscription);
    subscription = this.bomService.subscribeToBom(bom => this.bom = bom);
    this.subscriptions.push(subscription);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  onSave() {
    const endDate: any = this.bom.endDate;
    if (endDate instanceof Date) {
      this.bom.endDate = endDate.toISOString().split('T')[0];
    }
    const startDate: any = this.bom.startDate;
    if (startDate instanceof Date) {
      this.bom.startDate = startDate.toISOString().split('T')[0]
    }
    this.bomService.saveBom(this.bom)
      .then(bom => this.router.navigate([ '/boms', bom.billOfMaterialId ]))
  }

  onBack() {
    this.router.navigate([ '/boms' ]);
  }
}
