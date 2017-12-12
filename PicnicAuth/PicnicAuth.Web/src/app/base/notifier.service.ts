import { Injectable } from '@angular/core';

@Injectable()
export class NotifierService {
  private time: number;
  private notifications: Array<Object>;

  constructor() {
    this.time = 4000;
    this.notifications = [];
  }

  get() {
    return this.notifications;
  }

  add(notif) {
    this.notifications.push(notif);

    if (typeof notif.fixed === "undefined")
      this.clear(notif);
  }

  clearAll() {
    this.notifications.length = 0;
  }

  remove(notif) {
    var i = this.notifications.indexOf(notif);
    if (i !== -1) {
      this.notifications.splice(i, 1);
    }
  }

  clear(notif) {
    setTimeout(() => this.remove(notif), this.time);
  }

  success(text, fixed = undefined) {
    this.add({ type: 'success', text: text, fixed })
  }

  warning(text, fixed = undefined) {
    this.add({ type: 'warning', text: text, fixed })
  }

  error(text, fixed = undefined) {
    if (typeof text === "object") {
      let body = JSON.parse(text._body);
      text = body.Message || '';
      if (body.ValidationResult)
        for (var e in body.ValidationResult.Errors) {
          console.log(body.ValidationResult.Errors[e]);
          console.log(body.ValidationResult.Errors[e].ErrorMessage);
          text += ' ' + body.ValidationResult.Errors[e].ErrorMessage;
        }
      if (body.error_description)
        text += ' ' + body.error_description;
    }

    this.add({ type: 'error', text: text, fixed })
  }
}
