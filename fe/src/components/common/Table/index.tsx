'use client';

import React, { useState } from 'react';
import { Button, Checkbox, Modal, Popover, Space, Table, TableProps } from 'antd';
import type { CheckboxOptionType } from 'antd';
import { SettingOutlined, EllipsisOutlined } from '@ant-design/icons';
import { IAction, IColumn } from '@models/common/table.model';
import '@styles/table.style.scss';
import { ETableColumnType } from '@/constants/e-table.consts';
import { DelegationStatusConst } from '@/app/(home)/delegation/consts/delegation-status.consts';

interface AppTableProps<T> extends TableProps<T> {
  columns: IColumn<T>[];
  listActions?: IAction[];
}

const AppTable = <T extends object>(props: AppTableProps<T>) => {
  const [openConfig, setOpenConfig] = useState<boolean>(false);
  const [openActionIndex, setOpenActionIndex] = useState<number | null>(null);
  const { columns, listActions, ...rest } = props;

  const openPopupConfig = () => {
    setOpenConfig(true);
  };

  const closePopupConfig = () => {
    setOpenConfig(false);
  };
  const renderStatusColumn = (value: any) => {
    const info = DelegationStatusConst.getInfo(value);

    if (!info || typeof info !== 'object') return value;

    return <span className={info.class}>{info.name}</span>;
  };

  const enhancedColumns = columns.map((col) => {
    if (col.type === ETableColumnType.STATUS) {
      return {
        ...col,
        fixed: col.fixed ?? 'right',
        width: col.width ?? 200,
        render: (value: any, record: any, index: number) => {
          if (col.render) {
            return col.render(value, record, index);
          }

          return renderStatusColumn(value);
        }
      };
    }

    return col;
  });

  const configColumn: IColumn<T> = {
    key: 'config',
    title: <Button type="text" icon={<SettingOutlined style={{ color: 'white' }} />} onClick={openPopupConfig} />,
    dataIndex: '__config',
    width: 50,
    fixed: 'right',
    render: (value, record, index) => {
      if (listActions?.length) {
        const actions = listActions
          .filter((act) => !act.hidden?.(record))
          .map((act, idx) => {
            return (
              <Button
                key={idx}
                size="middle"
                title={act?.tooltip ?? act.label}
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
            );
          });

        const content = (
          <Space.Compact size="middle" direction="vertical">
            {actions}
          </Space.Compact>
        );

        return (
          <Popover
            key={index}
            className="actions"
            trigger="click"
            open={openActionIndex === index}
            onOpenChange={(visible) => setOpenActionIndex(visible ? index : null)}
            placement="bottomRight"
            arrow={true}
            content={content}
          >
            <Button className="btn-more-action" type="text" title="Xem thêm" icon={<EllipsisOutlined />} />
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
    ...(enhancedColumns?.filter((item) => item.showOnConfig === false || checkedList.includes(item.key as string)) ??
      []),
    configColumn
  ];

  return (
    <>
      <Table<T> size="small" columns={newColumns} scroll={{ x: 'max-content' }} {...rest} />
      <Modal width={250} title="Cấu hình hiển thị" open={openConfig} onCancel={closePopupConfig} footer={null}>
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
