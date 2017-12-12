import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;
  confirmpassword: string;
  submitted = false;
  constructor() { }

  ngOnInit() {

  }

  login(): void {
    var a = this.username;
    var b = a;
  }

  onSubmit() {
    this.submitted = true;
  }
}
