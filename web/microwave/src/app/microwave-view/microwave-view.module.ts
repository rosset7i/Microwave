import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {MicrowaveViewRoutingModule} from './microwave-view-routing.module';
import {MicrowaveViewComponent} from './microwave-view.component';
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    MicrowaveViewComponent
  ],
  imports: [
    CommonModule,
    MicrowaveViewRoutingModule,
    FormsModule,
  ]
})
export class MicrowaveViewModule { }
