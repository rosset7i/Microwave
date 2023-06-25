import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MicrowaveViewComponent } from './microwave-view.component';

const routes: Routes = [
  {
    path: '',
    component: MicrowaveViewComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MicrowaveViewRoutingModule { }
