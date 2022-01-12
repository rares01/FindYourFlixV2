import {Component, OnInit} from "@angular/core";
import {Movie} from "../../models/movie";
import {MoviesService} from "../../services/movies.service";

@Component({
  selector: 'movies-list',
  templateUrl: './movies-list.component.html',
  styleUrls: ['./movies-list.component.scss']
})
export class MoviesListComponent implements OnInit {
  models: Movie[];

  constructor(private movieService: MoviesService) {

  }

  ngOnInit() {
    this.movieService.getList().subscribe(results => {
      this.models = results;
    })
  }
}
