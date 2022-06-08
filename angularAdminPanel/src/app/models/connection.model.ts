import { Event } from './event.model';

export class Connection {
  id: number;
  deamonId: number;
  configId: number;
  connected: boolean;
  events: Event[];
}
