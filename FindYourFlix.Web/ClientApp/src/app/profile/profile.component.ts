import {Component, OnInit} from "@angular/core";
import {User} from "../models/user";
import {UsersService} from "../services/users.service";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  model: User;
  form: FormGroup;
  changePassTemplate: BsModalRef;
  oldPassword: string;
  newPassword: string;
  changedPasswordMessage: string;

  constructor(private userService: UsersService,
              private formBuilder: FormBuilder,
              private modalService: BsModalService) {

  }

  ngOnInit() {
    this.userService.get().subscribe(result => {
      this.model = result;
      this.initForm();
    })
  }

  submit() {
    let user = this.form.value;
    this.userService.put(user).subscribe();
  }

  initForm() {
    this.form = new FormGroup({
      firstName: new FormControl(this.model.firstName),
      lastName: new FormControl(this.model.lastName),
      userName: new FormControl(this.model.userName),
      email: new FormControl(this.model.email),
    });
  }

  changePassword() {
    this.userService.updatePassword(this.oldPassword, this.newPassword).subscribe(result => {
       this.changedPasswordMessage = result ? 'Password updated successfully!' : 'Wrong old password inserted!';
    });
  }

  openChangePassTemplate(template: any) {
    this.changePassTemplate = this.modalService.show(template, {
      class: 'change-password',
      animated: false,
      keyboard: true,
      backdrop: true,
      ignoreBackdropClick: false
    });
  }

  closeTemplate() {
    this.changePassTemplate.hide();
    this.changedPasswordMessage = '';
    this.oldPassword = '';
    this.newPassword = '';
  }
}
