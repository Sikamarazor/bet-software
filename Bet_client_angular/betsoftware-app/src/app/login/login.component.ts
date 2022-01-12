import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private accountService: AccountService, private toast: ToastrService, private router: Router) { }

  login(): void {
    console.log("Login ", this.model);

    this.accountService.login(this.model).subscribe(data => {
      console.log('Res ', data);

      this.router.navigateByUrl('/home');

    }, error => {
      console.log('Error found ', error);
      this.toast.error(error.error);
    });
  }

  ngOnInit(): void {
  }

}
