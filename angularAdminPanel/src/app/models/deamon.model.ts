export class Deamon {
  public id: number;
  public alias: string;
  public iP_Address: string;
  //0 - unsynced, 1 - synced, 2 - disabeled while unsynced, 3 - disabeled while synced
  public status: number;
}
