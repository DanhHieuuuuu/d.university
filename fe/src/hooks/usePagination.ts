import { useState, useCallback, useEffect, useRef } from 'react';
import { PaginationProps } from 'antd';
import { IQueryPaging } from '@models/common/model.common';

interface UsePaginationWithFilterProps<T> {
  total: number;
  initialQuery: T;
  onQueryChange: (query: T) => void;
  triggerFirstLoad?: boolean;
}

export const usePaginationWithFilter = <T extends IQueryPaging>({
  total,
  initialQuery,
  onQueryChange,
  triggerFirstLoad = false
}: UsePaginationWithFilterProps<T>) => {
  const [query, setQuery] = useState<T>(initialQuery);
  const isFirstRender = useRef(true);

  const updateQuery = useCallback((partialQuery: Partial<T>) => {
    setQuery((prev) => {
      const newQuery = { ...prev, ...partialQuery };
      let changed = false;
      for (const key in newQuery) {
        if (newQuery[key] !== prev[key]) {
          changed = true;
          break;
        }
      }
      return changed ? (newQuery as T) : prev;
    });
  }, []);

  // Xử lý phân trang - pagination thay đổi
  const handleChangePage = useCallback((page: number, pageSize: number) => {
    updateQuery({
      PageSize: pageSize,
      PageIndex: page
    } as Partial<T>);
  }, []);

  // Xứ lý khi các bộ lọc - filters có sự thay đổi
  const handleFilterChange = useCallback((filters: Partial<T>) => {
    updateQuery({
      ...filters,
      PageIndex: 1 // reset về trang 1
    });
  }, []);

  const pagination: PaginationProps = {
    total,
    current: query.PageIndex ?? 1,
    pageSize: query.PageSize ?? 10,
    showSizeChanger: true,
    pageSizeOptions: ['10', '20', '50'],
    showTotal: (total, range) => `${range[0]}-${range[1]} của ${total} mục`,
    onChange: handleChangePage,
    onShowSizeChange: handleChangePage
  };

  useEffect(() => {
    if (isFirstRender.current) {
      isFirstRender.current = false;
      if (triggerFirstLoad) {
        onQueryChange(query);
      }
    } else {
      onQueryChange(query);
    }
  }, [query]);

  // xử lý reset filter
  const resetFilter = () => {
    const defaultQuery = {
      PageIndex: 1,
      PageSize: 10,
      Keyword: '',
      ...initialQuery
    };

    setQuery(defaultQuery);
    onQueryChange?.(defaultQuery);
  };

  return {
    query,
    setQuery: updateQuery,
    onFilterChange: handleFilterChange,
    pagination,
    resetFilter
  };
};
