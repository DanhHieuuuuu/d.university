export interface IMenu {
  label: string;
  routerLink: string;
  icon?: React.ReactNode;
  hidden?: boolean;
  items?: IMenu[];
}
