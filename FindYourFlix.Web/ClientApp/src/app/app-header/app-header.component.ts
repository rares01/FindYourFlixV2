import {Component} from "@angular/core";
import {Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.scss']
})
export class AppHeaderComponent {
  urls = {
    movies: () => '/movies',
    profile: () => '/profile'
  };

  logOutIcon = faSignOutAlt;

  constructor(private router: Router,
              private authenticationService: AuthenticationService) {
  }

  navigate(url: string) {
    this.router.navigate([url]);
  }

  logOut() {
    this.authenticationService.logout();
    this.navigate('/login')
  }
}
