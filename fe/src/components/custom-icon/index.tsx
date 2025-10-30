import React from 'react';
import Icon from '@ant-design/icons';
import type { GetProps } from 'antd';

import { KeySvg } from './key.icon';
import AdminSvg from './admin.icon';
import StudentSvg from './student.icon';

type CustomIconComponentProps = GetProps<typeof Icon>;

const KeyIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={KeySvg} {...props} />;
const AdminIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={AdminSvg} {...props} />;
const StudentIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={StudentSvg} {...props} />;

export { KeyIcon, AdminIcon, StudentIcon };
