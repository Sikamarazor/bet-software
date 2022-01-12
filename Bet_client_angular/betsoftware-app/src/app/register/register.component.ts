import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  confirmPassword: string = "";

  constructor(private accountService: AccountService, private toast: ToastrService) { }

  register() {

    if (this.confirmPassword != this.model.password) {
      this.toast.error('Password do not match');
    } else {
      this.accountService.register(this.model).subscribe(response => {
        console.log('Registerd ', response);
        // this.cancel();
      }, error => {
        this.toast.error(error.error);
      });
    }
    
  }

  /* cancel() {
    console.log('Cancelled ');

    this.cancelRegister.emit(false);
  } */


  ngOnInit(): void {
  }

}
