import React, { useRef, useState, useCallback } from 'react';
import { Select, Spin, Avatar } from 'antd';
import type { SelectProps } from 'antd';
import { useDebouncedCallback } from '@hooks/useDebounce';

export interface DebounceSelectOption {
  label: React.ReactNode;
  value: string | number;
}

export interface DebounceSelectProps<T extends DebounceSelectOption>
  extends Omit<SelectProps<T | T[]>, 'options' | 'children'> {
  fetchOptions: (search: string) => Promise<T[]>;
  debounceTimeout?: number;
}

export function DebounceSelect<T extends DebounceSelectOption>({
  fetchOptions,
  debounceTimeout = 300,
  ...props
}: DebounceSelectProps<T>) {
  const [options, setOptions] = useState<T[]>([]);
  const [fetching, setFetching] = useState(false);
  const fetchRef = useRef(0);

  const loadOptions = useCallback(
    async (search: string) => {
      fetchRef.current += 1;
      const fetchId = fetchRef.current;

      setFetching(true);

      try {
        const data = await fetchOptions(search);
        if (fetchId === fetchRef.current) {
          setOptions(data);
        }
      } finally {
        if (fetchId === fetchRef.current) {
          setFetching(false);
        }
      }
    },
    [fetchOptions]
  );

  const { debounced: onSearch } = useDebouncedCallback(loadOptions, debounceTimeout);

  return (
    <Select
      showSearch
      filterOption={false}
      onSearch={onSearch}
      notFoundContent={fetching ? <Spin size="small" /> : 'No data'}
      options={options}
      {...props}
      optionRender={(option) => <div style={{ display: 'flex', alignItems: 'center' }}>{option.label}</div>}
    />
  );
}
