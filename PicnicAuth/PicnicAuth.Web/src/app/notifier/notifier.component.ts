import { Component } from '@angular/core';
import { NotifierService } from '../base/notifier.service';

@Component({
  selector: 'fg-notifier',
  templateUrl: './notifier.component.html',
  styleUrls: ['./notifier.component.sass']
})
export class NotifierComponent {
  notifications: Array<Object>;

  constructor(private notifier: NotifierService) {


    this.notifications = this.notifier.get();

  }

  close(notif) {
    this.notifier.remove(notif);
  }
}
