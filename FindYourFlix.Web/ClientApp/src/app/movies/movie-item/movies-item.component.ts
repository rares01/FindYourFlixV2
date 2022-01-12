import {Component, Input, OnInit} from "@angular/core";
import {Movie} from "../../models/movie";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {Tag} from "../../models/tag";
import {TagsService} from "../../services/tags.service";
import {BehaviorSubject} from "rxjs";
import {UsersService} from "../../services/users.service";
import { faThumbsUp, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'movies-item',
  templateUrl: './movies-item.component.html',
  styleUrls: ['./movies-item.component.scss']
})
export class MoviesItemComponent implements OnInit {
  @Input('movie') movie: Movie;
  tagsModalRef: BsModalRef;
  currentUserTags: BehaviorSubject<Tag[]> = new BehaviorSubject<Tag[]>([]);
  newInsertedTag: string;
  likeIcon = faThumbsUp;
  plusIcon = faPlus;
  deleteIcon = faTrash;

  constructor(private modalService: BsModalService,
              private tagService: TagsService,
              private userService: UsersService) {
  }

  ngOnInit() {
    console.log(this.movie)
    this.movie.tags.forEach(tag => {
      if(tag.isUsersTag) {
        let array = this.currentUserTags.getValue();
        array = [{id: tag.id, name: tag.name}, ...array];
        this.currentUserTags.next(array);
      }
    })
  }

  insertTag() {
    let tag = new Tag();
    tag.movieId = this.movie.id;
    tag.userId = 'd85a9e31-da19-4ce2-a82f-34c90de56f42';
    tag.name = this.newInsertedTag;
    this.tagService.insert(tag).subscribe(id => {
      let array = this.currentUserTags.getValue();
      array = [{id: id, name: this.newInsertedTag}, ...array];
      this.currentUserTags.next(array);
      this.newInsertedTag = '';
    });
  }

  deleteTag(id: any) {
    this.tagService.delete(id).subscribe(() => {
      let array = this.currentUserTags.getValue();
      let index = array.findIndex(item => item.id === id);
      array.splice(index,1);
      this.currentUserTags.next(array);
    });
  }

  likeMovie() {
    this.userService.like(this.movie.id).subscribe(() => {
      this.movie.isLiked = !this.movie.isLiked;
    });
  }

  openTagsTemplate(tagsTemplate: any) {
    this.tagsModalRef = this.modalService.show(tagsTemplate, {
      class: 'tags-template',
      animated: false,
      keyboard: true,
      backdrop: true,
      ignoreBackdropClick: false
    });
  }

  closeTagsTemplate() {
    this.tagsModalRef.hide();
  }
}
