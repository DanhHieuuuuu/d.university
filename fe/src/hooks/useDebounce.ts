import { useRef, useCallback } from 'react';

export function useDebouncedCallback<T extends (...args: any[]) => void>(callback: T, delay: number = 300) {
  const timeoutRef = useRef<NodeJS.Timeout | null>(null);

  const debounced = useCallback(
    (...args: Parameters<T>) => {
      if (timeoutRef.current) {
        clearTimeout(timeoutRef.current);
      }

      timeoutRef.current = setTimeout(() => {
        callback(...args);
      }, delay);
    },
    [callback, delay]
  );

  const cancel = () => {
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
      timeoutRef.current = null;
    }
  };

  return { debounced, cancel };
}
