export class RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;

  constructor(firstName: string, secondName: string, email: string, password: string) {
    this.firstName = firstName;
    this.lastName = secondName;
    this.email = email;
    this.password = password;
  }
}
