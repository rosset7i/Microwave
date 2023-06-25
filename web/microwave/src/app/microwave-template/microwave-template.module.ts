import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {MicrowaveTemplateRoutingModule} from './microwave-template-routing.module';
import {MicrowaveTemplateComponent} from './microwave-template.component';
import {ReactiveFormsModule} from "@angular/forms";
import { CreateTemplateModalComponent } from './create-template-modal/create-template-modal.component';
import {MatDialogModule} from "@angular/material/dialog";

@NgModule({
  declarations: [
    MicrowaveTemplateComponent,
    CreateTemplateModalComponent
  ],
  imports: [
    CommonModule,
    MicrowaveTemplateRoutingModule,
    ReactiveFormsModule,
    MatDialogModule
  ]
})
export class MicrowaveTemplateModule { }
