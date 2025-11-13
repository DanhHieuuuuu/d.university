export interface IMenu {
  label: string;
  routerLink: string;
  icon?: React.ReactNode;
  permissionKeys?: string[];
  items?: IMenu[];
}
