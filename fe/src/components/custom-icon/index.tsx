import React from 'react';
import Icon from '@ant-design/icons';
import type { GetProps } from 'antd';
import { KeySvg } from './key.icon';

type CustomIconComponentProps = GetProps<typeof Icon>;

const KeyIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={KeySvg} {...props} />;

export { KeyIcon };
