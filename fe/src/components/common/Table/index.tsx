'use client';

import React, { useState } from 'react';
import { Button, Checkbox, Modal, Table, TableProps } from 'antd';
import type { CheckboxOptionType } from 'antd';
import { SettingOutlined } from '@ant-design/icons';
import { IColumn } from '@models/common/table.model';
import '@styles/table.style.scss';

interface AppTableProps<T> extends TableProps<T> {
  columns: IColumn<T>[];
}

const AppTable = <T extends object>(props: AppTableProps<T>) => {
  const [open, setOpen] = useState(false);
  const { columns, ...rest } = props;

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
    render: () => null
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
      <Table<T> bordered size="middle" columns={newColumns} scroll={{ x: 'max-content', y: 250 }} {...rest} />
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
