import { EStatusResonse } from './common';

export interface IResponseList<Item, Summary = any> {
  code: number;
  data: {
    items: Item[];
    totalItem: number;
    summary?: Summary;
  };
  message: string;
  status: EStatusResonse;
}

export interface IResponseListNodeJS<Item> {
  limit: number;
  page: number;
  results: Item[];
  totalPages: number;
  totalResults: number;
}

export interface IResponseItem<Item> {
  code: number;
  data: Item;
  message: string;
  status: EStatusResonse;
}

export interface IResponseDialog<Data> {
  data: Data;
  accept: boolean;
}
