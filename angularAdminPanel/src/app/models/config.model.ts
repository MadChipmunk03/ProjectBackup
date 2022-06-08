import { Destination } from './destination.model';
import { Source } from './source.model';

export class Config {
  id: number;
  alias: string;
  // 0 - undefined, 1 - full, 2 - differential, 3 - incremental
  backupType: number;
  packageSize: number;
  packageCount: number;
  timePeriod: string;
  email: string;
  mailTimeCron: string;
  sources: Source[];
  destinations: Destination[];
  active: boolean | null;
}
