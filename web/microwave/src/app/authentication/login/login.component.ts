import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthenticationService} from "../authentication.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private router: Router) {
    this.initFrom();
  }

  initFrom() {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]],
      password: ['', [Validators.required, Validators.pattern('^([a-zA-Z0-9@*#]{8,15})$')]]
    });
  }

  canSave() {
    return !this.form.valid;
  }

  save() {
    this.authenticationService.login(this.form.value)
      .subscribe(authResponse => {
        if (authResponse.token) {
          alert('Autenticado com sucesso!');
          localStorage.setItem('token', authResponse.token);
          this.authenticationService.isAuthenticated = true;
          this.router.navigate(['microwave-view']);
        }
      });
  }
}
