import {Component, Inject, OnInit} from '@angular/core';
import {MicrowaveOutput} from "../models/microwave-output";
import {MicrowaveService} from "../microwave.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-create-template-modal',
  templateUrl: './create-template-modal.component.html',
  styleUrls: ['./create-template-modal.component.css']
})
export class CreateTemplateModalComponent implements OnInit{
  form: FormGroup;

  constructor(
    private microwaveService: MicrowaveService,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: MicrowaveOutput,
    private dialog: MatDialogRef<any>) {
  }

  ngOnInit(): void {
    this.initForm();
  }

  get canSave(){
    return this.form.valid && this.form.touched;
  }

  initForm() {
    this.form = this.formBuilder.group({
      name: [null, Validators.required],
      food: [null, Validators.required],
      potency: [null, Validators.compose([Validators.min(1), Validators.max(10), Validators.required])],
      warmingChar: [null, Validators.compose([Validators.required])],
      time: [null, Validators.required],
      instructions: [null],
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  createMicrowave() {
    const microwave: MicrowaveOutput = this.form.value;
    if (this.data != undefined) {
      microwave.id = this.data.id;
    }

    return microwave;
  }

  saveMicrowave() {
    const microwave = this.createMicrowave();

    if (microwave.id) {
      this.microwaveService.updateMicrowave(microwave.id, microwave)
        .subscribe(() => this.dialog.close());
    } else {
      this.microwaveService.createMicrowave(microwave)
        .subscribe(() => this.dialog.close());
    }
  }
}
