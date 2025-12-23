import React from 'react';
import { Form, Typography } from 'antd';
import type { FormItemProps } from 'antd';
import dayjs from 'dayjs';

const Text = Typography.Text;

export interface RenderFieldOptions {
  isEdit: boolean;
  displayValueFormatter?: (value: any) => React.ReactNode;
  displayType?: 'date' | 'time'; // thêm kiểu hiển thị
}

export function renderField(
  name: FormItemProps['name'],
  initialValue: any,
  input: React.ReactElement,
  options: RenderFieldOptions
): React.ReactElement {
  const { isEdit, displayValueFormatter } = options;

  const child = isEdit
    ? input 
    : React.createElement(
        Text,
        null,
        displayValueFormatter ? displayValueFormatter(initialValue) : formatDisplayValue(initialValue, options.displayType)
      );

  return React.createElement(
    Form.Item,
    {
      name,
      initialValue,
      style: { margin: 0 }
    },
    child
  );
}

function formatDisplayValue(value: any, type?: 'date' | 'time'): string {
  if (value === null || value === undefined) return '-';

  if (dayjs.isDayjs(value)) {
    if (type === 'time') return value.format('HH:mm');      // chỉ giờ
    if (type === 'date') return value.format('DD/MM/YYYY'); // chỉ ngày
    return value.format('DD/MM/YYYY HH:mm');               // default: ngày + giờ
  }

  if (typeof value === 'number') return value.toLocaleString();

  return String(value);
}
