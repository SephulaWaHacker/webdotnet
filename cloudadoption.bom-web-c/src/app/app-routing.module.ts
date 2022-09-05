import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {NotFoundComponent} from './common/not-found/not-found.component';
import {BomListComponent} from './bom/bom-list/bom-list.component';
import {BomDetailComponent} from './bom/bom-detail/bom-detail.component';
import {HomeComponent} from './common/home/home.component';

const routes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: HomeComponent},
  {
    path: 'boms', children: [
      {path: '', component: BomListComponent},
      {path: 'create', component: BomDetailComponent}
    ]
  },
  {path: '404', component: NotFoundComponent},
  {path: '**', redirectTo: '/404'},
];

//** Create Routes as modules */
// path [:bom-id], children paths ['', modify, part-family], component [BomOverviewComponent, BomDetailComponent], 
// path [part-family], children paths ['', :part-family-id], component [PartFamilyComponent, PartFamilyComponent]


@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
