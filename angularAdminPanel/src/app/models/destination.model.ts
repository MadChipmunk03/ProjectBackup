export class Destination {
  id: number;
  configId: number;
  // 0 - local, 1 - NET, 2 - FTP
  saveType: number;
  iP_Address: string | null;
  username: string | null;
  password: string | null;
  path: string;
  saveFile: string;
}
