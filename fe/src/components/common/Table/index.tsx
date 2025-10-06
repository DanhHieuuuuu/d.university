'use client';

import React, { useState } from 'react';
import { Button, Checkbox, Modal, Popover, Space, Table, TableProps } from 'antd';
import type { CheckboxOptionType } from 'antd';
import { SettingOutlined, MoreOutlined, EllipsisOutlined } from '@ant-design/icons';
import { IAction, IColumn } from '@models/common/table.model';
import '@styles/table.style.scss';

interface AppTableProps<T> extends TableProps<T> {
  columns: IColumn<T>[];
  listActions?: IAction[];
}

const AppTable = <T extends object>(props: AppTableProps<T>) => {
  const [open, setOpen] = useState(false);
  const { columns, listActions, ...rest } = props;

  const openPopup = () => {
    setOpen(true);
  };

  const closePopup = () => {
    setOpen(false);
  };

  const configColumn: IColumn<T> = {
    key: 'config',
    title: <Button type="text" icon={<SettingOutlined />} onClick={openPopup} />,
    dataIndex: '__config',
    width: 50,
    fixed: 'right',
    render: (value, record, index) => {
      if (listActions?.length) {
        const actions = listActions.map((act, idx) => {
          return (
            <Button
              size="middle"
              key={idx}
              title={act?.tooltip ?? act.label}
              color={act.color ?? 'default'}
              variant="dashed"
              icon={act.icon}
              onClick={() => act.command(record)}
            >
              {act.label}
            </Button>
          );
        });

        const content = (
          <Space.Compact size="middle" direction="vertical">
            {actions}
          </Space.Compact>
        );

        return (
          <Popover trigger="click" placement="bottomRight" arrow={true} content={content}>
            <Button type="text" title="Xem thêm" icon={<EllipsisOutlined />} />
          </Popover>
        );
      }
      return null;
    }
  };

  const defaultCheckedList = columns?.map((item) => item.key as string) ?? [];

  const [checkedList, setCheckedList] = useState<string[]>(defaultCheckedList);

  const options: CheckboxOptionType[] =
    columns?.map(({ key, title, showOnConfig }) => ({
      label: title as string,
      value: key as string,
      disabled: showOnConfig === false
    })) ?? [];

  const newColumns = [
    ...(columns?.filter((item) => item.showOnConfig === false || checkedList.includes(item.key as string)) ?? []),
    configColumn
  ];

  return (
    <>
      <Table<T> size="small" columns={newColumns} scroll={{ x: 'max-content' }} {...rest} />
      <Modal width={250} title="Cấu hình hiển thị" open={open} onCancel={closePopup} footer={null}>
        <Checkbox.Group
          style={{ flexDirection: 'column' }}
          value={checkedList}
          options={options}
          onChange={(value) => {
            setCheckedList(value as string[]);
          }}
        />
      </Modal>
    </>
  );
};

export default AppTable;
