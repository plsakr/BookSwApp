import {Component, OnDestroy, OnInit} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup, FormGroupDirective, NgForm,
  Validators
} from '@angular/forms';
import {ApplicationUser, AuthService} from '../services/auth.service';
import {ErrorStateMatcher} from '@angular/material/core';
import {Subscription} from 'rxjs';
import {Router} from '@angular/router';

type LoginRequest = {
  email: string,
  password: string
};

type RegisterRequest = {
  name: string,
  email: string,
  password: {
    password: string,
    repassword: string
  }
};

// tells the 'confirm password' field when to update its status
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.parent?.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);

    return (invalidCtrl || invalidParent);
  }
}

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit, OnDestroy {
  subscription$: Subscription = new Subscription();
  loginForm = this.fb.group({
    email: ['', [Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
    password: ['']
  });

  passCheckGroup = this.fb.group({
    password: ['', Validators.minLength(6)],
    repassword: ['', [Validators.required]]
  }, {validator: this.checkPasswords});
  registerForm = this.fb.group({
    name: ['', Validators.required],
    email: ['', [Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
    password: this.passCheckGroup,
  });
  passMatcher = new MyErrorStateMatcher();

  private getAuth(): AuthService { return this.auth; }

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {
    this.subscription$ = this.getAuth().user$.subscribe(this.onLoginSuccesful);
  }

  onLoginSuccesful = (user: ApplicationUser) => {
    if (user.role === 'USER') {
      this.router.navigate(['library']);
      console.log('IM LOGGED IN');
    } else if (user.role === 'LIBRARIAN') {
      this.router.navigate(['user']);
      console.log('IM A LIBRARIAN');
    } else if (user.role === 'NONE') {
      // didnt try logging in yet
      console.log('going to try logging in!');
      console.log(this.auth);
      this.getAuth().testLogin();
    }
  }

  onLoginFailed(): void {
    console.log('IM NOT LOGGED IN');
  }

  onLogin(): void {
    if (this.loginForm.valid) {
      const data = this.loginForm.getRawValue() as LoginRequest;
      console.log(data);
      this.auth.login(data.email, data.password, this.onLoginFailed);
    }
  }

  onRegister(): void {
    if (this.registerForm.valid) {
      const data = this.registerForm.getRawValue() as RegisterRequest;
      console.log(data);
      this.getAuth().register(data.name, data.password.password, data.email);
    }
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.subscription$.unsubscribe();
  }

  checkPasswords(group: FormGroup): any { // here we have the 'passwords' group
    const pass = group.get('password')?.value;
    const confirmPass = group.get('repassword')?.value;
    if (pass === null) {
      return null;
    }
    return pass === confirmPass ? null : { notSame: true };
  }

}
