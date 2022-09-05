import { Injectable } from '@angular/core';
import { init as initApm, Transaction } from '@elastic/apm-rum';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApmService {
  readonly apm: any;

  constructor() {
    this.apm = initApm({

      // Set required service name (allowed characters: a-z, A-Z, 0-9, -, _, and space)
      serviceName: 'Vehicle BOM Web',

      // Set custom APM Server URL (default: http://localhost:8200)
      serverUrl: 'http://localhost:8200',
      active: environment.apm.active,
      instrument: true
    })
  }

  public monitor(event?: Event): Transaction | undefined {
    const target = event?.target as HTMLInputElement;
    const actorId = target?.id;
    const txName = 'Click - ' + target.tagName + '[' + actorId + ']';
    const tx = actorId ? this.apm.startTransaction(txName) : this.apm.startTransaction();
    return tx;
  }
}
