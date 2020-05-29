import { Component, OnInit, Input } from '@angular/core';
import { UserToken } from '../../core/models/response/user-token';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})

export class UserProfileComponent {
  @Input() model: UserToken;
}
