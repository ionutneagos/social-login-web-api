import { Component, Input } from '@angular/core';
import { GoogleLoginProvider } from "angularx-social-login";
import {  SocialAuthService  } from "angularx-social-login";
import { AuthenticateService } from "../../core/services/authenticate.service";

@Component({
  selector: 'app-google-login',
  templateUrl: './google-login.component.html',
  styleUrls: ['./google-login.component.css']
})

export class GoogleLoginComponent {
  @Input() capture: string;

  isLogging: boolean;

  constructor(private authService: AuthenticateService,
    private socialLoginService: SocialAuthService) { }

  signInWithGoogle(): void {
    this.isLogging = true;
    this.socialLoginService.signIn(GoogleLoginProvider.PROVIDER_ID).then(googleUser => {
      this.authService.googleLogin(googleUser)
        .subscribe((data) => {
          this.isLogging = false;
        });
    });
  }
}
