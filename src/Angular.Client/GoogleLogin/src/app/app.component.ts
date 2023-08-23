import { Component } from '@angular/core';
import { AuthenticateService } from './core/services/authenticate.service';
import { Subscription } from 'rxjs';
import { UserToken } from './core/models/response/user-token';
import { environment } from 'src/environments/environment';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'Google Login';
  user: SocialUser | undefined;
  logged = false;
  model: UserToken = null;
  subscription: Subscription;
  webApiUrl: string = `${environment.baseUrl}`;


  constructor(private authService: AuthenticateService, private socialLoginService: SocialAuthService) {
  }

  ngOnInit(): void {

    this.socialLoginService.authState.subscribe((user) => {
      this.user = user;
      if (this.user) {
        this.authService.googleLogin(user)
          .subscribe((data) => {
            this.model = data;
            this.logged = true;
          },
            (error) => {
              debugger
              alert(error)
            });
      }
    });

  }

  ngOnDestroy() {
  }

  logout() {
    this.socialLoginService.signOut();
    this.authService.logout();
    this.logged = false;

  }
}