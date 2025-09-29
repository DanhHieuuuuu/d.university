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

  const updateQuery = (partialQuery: Partial<T>) => {
    setQuery((prev) => {
      const newQuery = { ...prev, ...partialQuery };
      const isSame = JSON.stringify(prev) === JSON.stringify(newQuery);
      return isSame ? prev : newQuery;
    });
  };

  const handleChangePage = useCallback((page: number, pageSize: number) => {
    updateQuery({
      SkipCount: (page - 1) * pageSize,
      MaxResultCount: pageSize
    } as Partial<T>);
  }, []);

  const handleFilterChange = useCallback((filters: Partial<T>) => {
    updateQuery({
      ...filters,
      SkipCount: 0
    });
  }, []);

  const pagination: PaginationProps = {
    total,
    current: Math.floor((query.SkipCount ?? 0) / (query.MaxResultCount ?? 10)) + 1,
    pageSize: query.MaxResultCount ?? 10,
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

  return {
    query,
    setQuery: updateQuery,
    onFilterChange: handleFilterChange,
    pagination
  };
};
