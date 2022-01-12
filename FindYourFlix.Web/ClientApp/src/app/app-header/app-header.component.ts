import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import {UsersService} from "../services/users.service";

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.scss']
})
export class AppHeaderComponent implements OnInit{
  urls = {
    movies: () => '/movies',
    profile: () => '/profile',
    admin: () => '/admin'
  };

  isAdmin: boolean;

  logOutIcon = faSignOutAlt;

  constructor(private router: Router,
              private authenticationService: AuthenticationService,
              private userService: UsersService) {
  }

  ngOnInit() {
    this.userService.get().subscribe(result => {
      this.isAdmin = result.isAdmin;
    })
  }

  navigate(url: string) {
    this.router.navigate([url]);
  }

  logOut() {
    this.authenticationService.logout();
    this.navigate('/login')
  }
}
