import {Component, OnInit} from '@angular/core';
import {ProfileService} from './common/profile/profile.service';
import {Profile} from 'oidc-client';
import {MessageService} from './common/util/message.service';
import {LoadingService} from './common/util/loading.service';
import {Observable} from 'rxjs';
import {ApiService} from './common/api/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [ './app.component.scss' ]
})
export class AppComponent implements OnInit {
  title = 'Vehicle BOM';

  profile: Promise<Profile | null>;

  infoMessages: string[];
  errorMessages: string[];
  isLoading: Observable<boolean>;
  api: string;
  apis: string[];

  constructor(private profileService: ProfileService,
              private messageService: MessageService,
              private apiService: ApiService,
              private loadingService: LoadingService) {
    this.profile = profileService.profile;
    this.infoMessages = [];
    this.errorMessages = [];
    this.apis = [ 'java', 'csharp', 'python' ]
    this.api = apiService.currentApi();
    this.isLoading = this.loadingService.isLoading;
  }

  ngOnInit(): void {
    this.messageService.infoMessages.subscribe(info => this.infoMessages.push(info));
    this.messageService.errorMessages.subscribe(info => this.errorMessages.push(info));
  }

  clearInfoMessages() {
    this.infoMessages = [];
  }

  clearErrorMessages() {
    this.errorMessages = [];
  }

  setApi(api: string) {
    this.apiService.next(api);
  }
}
