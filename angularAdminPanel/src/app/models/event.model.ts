import { Time } from "@angular/common";

export class Event {
  Id: number;
  connectionId: number;
  message: string;
  time: string;
  //status: true - active, false - outdated
  status: boolean;
  //types: 0 - successfulBackup, 1 - error, 2- info
  type: number;
}
