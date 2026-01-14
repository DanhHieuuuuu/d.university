/**
 * COMMOM MODEL FOR REQUEST PAGING
 */
export type IQueryPaging = {
  PageIndex: number;
  PageSize: number;
  Keyword?: string;
  Queryable?: string[];

  search?: string | null;
  filter?: string | null;
  sort?: string | null;
};

/**
 * COMMON MODEL ITEM FOR SELECT OPTION COMPONENT
 */
export type IItemDropdown = {
  label: string;
  value: string | number;
};
