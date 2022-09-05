import { Component, Input } from '@angular/core';
import { Profile } from 'oidc-client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: [ './header.component.scss' ]
})
export class HeaderComponent {

  @Input()
  profile?: Profile | null;

  constructor(private router: Router) {
  }

  goHome() {
    this.router.navigate([ '/' ]);
  }
}
