import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";
import {Movie} from "../models/movie";
import {MoviesService} from "../services/movies.service";
import {UsersService} from "../services/users.service";
import {User} from "../models/user";

@Component({
  selector: 'admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  model: Movie;
  form: FormGroup;
  users: User[];

  constructor(private movieService: MoviesService,
              private userService: UsersService) {

  }

  ngOnInit() {
    this.userService.getUsers().subscribe(result => {
      this.users = result;
    });
    this.form = new FormGroup({
      name: new FormControl(''),
      firstGenre: new FormControl(''),
      secondGenre: new FormControl(''),
      thirdGenre: new FormControl(''),
    });
  }

  submit() {
    let genres = [];
    if(this.form.value.firstGenre) {
      genres.push(this.form.value.firstGenre);
    }
    if(this.form.value.secondGenre) {
      genres.push(this.form.value.secondGenre);
    }
    if(this.form.value.thirdGenre) {
      genres.push(this.form.value.thirdGenre);
    }
    this.movieService.insert({name: this.form.value.name, genres: genres}).subscribe();
  }

  onRoleAction(user: User) {
    console.log(user.id)
    this.userService.updateRole(user.id).subscribe();
  }
}
