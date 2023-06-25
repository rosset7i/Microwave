import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AuthGuard} from "./authentication/auth.guard";

const routes: Routes = [
  {
    path: 'microwave-view',
    loadChildren: () => import('./microwave-view/microwave-view.module').then(m => m.MicrowaveViewModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'microwave-template',
    loadChildren: () => import('./microwave-template/microwave-template.module').then(m => m.MicrowaveTemplateModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'authentication',
    loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule)
  },
  {
    path: '**',
    redirectTo: 'authentication'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
