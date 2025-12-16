import { DelegationStatusConst } from "@/app/(home)/delegation/consts/delegation-status.consts";

export const getStatusName = (value: number) => {
  const info = DelegationStatusConst.getInfo(value);
  return info ? (info as { name: string }).name : '-';
};

export const getStatusClass = (value: number) => {
  const info = DelegationStatusConst.getInfo(value);
  return info ? (info as { class: string }).class : '';
};
