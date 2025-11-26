import React from 'react';
import Icon from '@ant-design/icons';
import type { GetProps } from 'antd';

import { KeySvg } from './key.icon';
import AdminSvg from './admin.icon';
import StudentSvg from './student.icon';
import BellSvg from './bell.icon';
import DelegationSvg from './delegation.icon';
import SuggestSvg from './suggest.icon';

type CustomIconComponentProps = GetProps<typeof Icon>;

const KeyIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={KeySvg} {...props} />;
const AdminIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={AdminSvg} {...props} />;
const StudentIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={StudentSvg} {...props} />;
const BellIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={BellSvg} {...props} />;
const DelegationIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={DelegationSvg} {...props} />;
const SuggestIcon = (props: Partial<CustomIconComponentProps>) => <Icon component={SuggestSvg} {...props} />;

export { KeyIcon, AdminIcon, StudentIcon, BellIcon, DelegationIcon, SuggestIcon };
