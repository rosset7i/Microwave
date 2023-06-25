import {Component, OnInit} from '@angular/core';
import {RegisterRequest} from "../models/register-request";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../authentication.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService) {
  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]],
      password: ['', [Validators.required, Validators.pattern('^([a-zA-Z0-9@*#]{8,15})$')]],
      repeatPassword: ['', Validators.required]
    })
  }

  canSave() {
    return !this.form.valid;
  }

  mapFormValue() {
    const firstName = this.form.controls['firstName'].value;
    const lastName = this.form.controls['lastName'].value;
    const email = this.form.controls['email'].value;
    const password = this.form.controls['password'].value;

    return new RegisterRequest(firstName, lastName, email, password);
  }

  save() {
    const password = this.form.controls['password'].value;
    const repeatPassword = this.form.controls['repeatPassword'].value;

    const user = this.mapFormValue();

    if (this.form.valid && password == repeatPassword) {
      return this.authenticationService.register(user)
        .subscribe(() => this.router.navigate(['authentication/login']));
    }
    return alert('A senhas não conhecidem')
  }

}
