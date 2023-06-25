import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MicrowaveTemplateComponent } from './microwave-template.component';

const routes: Routes = [{ path: '', component: MicrowaveTemplateComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MicrowaveTemplateRoutingModule { }
