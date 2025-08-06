/**
 * COMMOM MODEL FOR REQUEST PAGING
 */
export type IQueryPaging = {
  SkipCount?: number;
  MaxResultCount?: number;
  Sorting?: string;
  PageCount?: number;
  Queryable?: string[];
  Keyword?: string;

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
