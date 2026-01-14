'use client';

import React, { useState } from 'react';
import { Button, Checkbox, Modal, Popover, Space, Table, TableProps, Tag } from 'antd';
import type { CheckboxOptionType } from 'antd';
import { SettingOutlined, EllipsisOutlined } from '@ant-design/icons';
import { IAction, IColumn } from '@models/common/table.model';
import { ETableColumnType } from '@/constants/e-table.consts';
import '@styles/table.style.scss';

interface AppTableProps<T> extends TableProps<T> {
  columns: IColumn<T>[];
  listActions?: IAction[];
  rowSelection?: TableProps<T>['rowSelection'];
}

const AppTable = <T extends object>(props: AppTableProps<T>) => {
  const [openConfig, setOpenConfig] = useState<boolean>(false);
  const [openActionIndex, setOpenActionIndex] = useState<number | null>(null);
  const { columns, listActions, rowSelection, ...rest } = props;

  const indexColumn : IColumn<T> = {
    key: 'stt',
    dataIndex: 'stt',
    title: 'STT',
    align: 'center',
    width: 60,
    fixed: 'left',
    showOnConfig: false,
    render: (value, record, index) => index + 1
  };

  const openPopupConfig = () => {
    setOpenConfig(true);
  };

  const closePopupConfig = () => {
    setOpenConfig(false);
  };

  const renderStatusColumn = (value: any, col: IColumn<T>, record?: T) => {
    if (!col.getTagInfo) return value;

    const info = col.getTagInfo(value, record);
    if (!info) return value;

    return (
      <Tag bordered={false} className={info.className} color={info.color}>
        {info.label}
      </Tag>
    );
  };

  const enhancedColumns: IColumn<T>[] = columns.map((col) => {
    if (col.type === ETableColumnType.STATUS) {
      return {
        ...col,
        fixed: col.fixed ?? 'right',
        width: col.width ?? 150,
        render: (value: any, record: T, index: number) => {
          if (col.render) {
            return col.render(value, record, index);
          }

          return renderStatusColumn(value, col, record);
        }
      };
    } else {
      return {
        ...col,
        minWidth: col.minWidth ?? 100
      };
    }
  });

  const configColumn: IColumn<T> = {
    key: 'config',
    title: <Button type="text" icon={<SettingOutlined style={{ color: 'white' }} />} onClick={openPopupConfig} />,
    dataIndex: '__config',
    width: 50,
    fixed: 'right',
    onCell: (_record: T, index?: number) => {
      const rowStyle = typeof rest.onRow === 'function' ? rest.onRow(_record)?.style : undefined;

      return rowStyle ? { style: rowStyle } : {};
    },
    render: (_, record, index) => {
      if (!listActions?.length) return null;

      const actions = listActions
        .filter((act) => !act.hidden?.(record))
        .map((act, idx) => (
          <Button
            key={idx}
            size="middle"
            title={act.tooltip ?? act.label}
            color={act.color ?? 'default'}
            variant="dashed"
            icon={act.icon}
            onClick={() => {
              act.command(record);
              setOpenActionIndex(null);
            }}
          >
            {act.label}
          </Button>
        ));

      if (!actions.length) return null;

      return (
        <Popover
          key={index}
          className="actions"
          trigger="click"
          open={openActionIndex === index}
          onOpenChange={(visible) => setOpenActionIndex(visible ? index : null)}
          placement="bottomRight"
          content={<Space direction="vertical">{actions}</Space>}
        >
          <Button type="text" icon={<EllipsisOutlined />} />
        </Popover>
      );
    }
  };

  const defaultCheckedList = columns.map((item) => item.key as string);
  const [checkedList, setCheckedList] = useState<string[]>(defaultCheckedList);

  const options: CheckboxOptionType[] = columns.map(({ key, title, showOnConfig }) => ({
    label: title as string,
    value: key as string,
    disabled: showOnConfig === false
  }));

  const newColumns = [
    indexColumn,
    ...enhancedColumns.filter((item) => item.showOnConfig === false || checkedList.includes(item.key as string)),
    configColumn
  ];

  return (
    <>
      <Table<T>
        size="small"
        tableLayout="fixed"
        rowKey="stt"
        columns={newColumns}
        scroll={{ x: 'max-content' }}
        rowSelection={rowSelection}
        {...rest}
      />

      <Modal width={250} title="Cấu hình hiển thị" open={openConfig} onCancel={closePopupConfig} footer={null}>
        <Checkbox.Group
          style={{ flexDirection: 'column' }}
          value={checkedList}
          options={options}
          onChange={(value) => setCheckedList(value as string[])}
        />
      </Modal>
    </>
  );
};

export default AppTable;
