import {Component, OnInit} from '@angular/core';
import {MicrowaveService} from "./microwave.service";
import {MicrowaveOutput} from "./models/microwave-output";
import {MatDialog} from "@angular/material/dialog";
import {CreateTemplateModalComponent} from "./create-template-modal/create-template-modal.component";

@Component({
  selector: 'app-microwave-template',
  templateUrl: './microwave-template.component.html',
  styleUrls: ['./microwave-template.component.css'],
})
export class MicrowaveTemplateComponent implements OnInit{
  microwaves: MicrowaveOutput[];

  constructor(
    private microwaveService: MicrowaveService,
    private dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.microwaveService.getAll()
      .subscribe(microwaves => {this.microwaves = microwaves});
  }

  openMicrowaveModal(microwave?: MicrowaveOutput) {
    this.dialog.open(CreateTemplateModalComponent, {data: microwave})
      .afterClosed()
      .subscribe(() => this.getData());
  }

  deleteMicrowave(id: string) {
    this.microwaveService.deleteMicrowave(id)
      .subscribe(() => this.getData());
  }
}
